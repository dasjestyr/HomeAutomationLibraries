using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HomeAutomation.PhilipsHue.Bridge;
using HomeAutomation.PhilipsHue.Configuration;
using HomeAutomation.PhilipsHue.Lights;
using HomeAutomation.PhilipsHue.Services;
using HomeAutomation.Wpf.Common;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace HomeAutomation.Wpf.Model
{
    public class HomeModel : BindableBase
    {
        private const int CommitTimeoutMilliseconds = 7000;
        private const int MaxHistorySize = 10;
        
        private readonly LightService _lightService;
        private readonly ApplicationConfigurationRepository _appConfigRepo;
        private readonly BindableMementoStack<Dictionary<int, HueLight>> _lightSetupHistory;

        private ObservableCollection<HueBridge> _bridges;
        private HueBridge _selectedBridge;
        
        private HueLight _selectedLight;
        private ObservableCollection<HueLight> _lights;
        private System.Timers.Timer _commitTimer;
        private Scene _selectedScene;
        private ObservableCollection<Scene> _savedScenes;

        public Action<string> LogAction { get; set; }

        private void Log(string message) => LogAction?.Invoke(message);

        public ObservableCollection<HueBridge> Bridges
        {
            get => _bridges;
            set => SetProperty(ref _bridges, value);
        }

        public ObservableCollection<HueLight> Lights
        {
            get => _lights;
            set => SetProperty(ref _lights, value);
        }

        public HueBridge SelectedBridge
        {
            get => _selectedBridge;
            set => SetProperty(ref _selectedBridge, value);
        }

        public HueLight SelectedLight
        {
            get => _selectedLight;
            set => SetProperty(ref _selectedLight, value);
        }

        public ObservableCollection<Scene> SavedScenes
        {
            get => _savedScenes;
            set => SetProperty(ref _savedScenes, value);
        }

        public Scene SelectedScene
        {
            get => _selectedScene;
            set => SetProperty(ref _selectedScene, value);
        }

        public ICommand ApplyLights => new DelegateCommand(UpdateLights);

        public ICommand ApplyScene => new DelegateCommand(() => Log($"Applying {SelectedScene.Name} (stub)..."));

        public ICommand Undo => new DelegateCommand(UndoChange);

        public ICommand Redo => new DelegateCommand(RedoChange);

        public HomeModel()
        {
            // TODO: figure out how to create a windsor activator and inject this bitches
            _appConfigRepo = new ApplicationConfigurationRepository();
            _lightService = new LightService();
            _lightSetupHistory = new BindableMementoStack<Dictionary<int, HueLight>>(MaxHistorySize);
        }

        public async void UndoChange()
        {
            _lightSetupHistory.Undo();
            await ApplyLightConfig(_lightSetupHistory.Current).ConfigureAwait(false);
        }

        public async void RedoChange()
        {
            _lightSetupHistory.Redo();
            await ApplyLightConfig(_lightSetupHistory.Current);
        }

        public async void UpdateLights()
        {
            var newConfig = Lights.ToDictionary(
                x => x.Id,
                x => x);

            await ApplyLightConfig(newConfig).ConfigureAwait(false);
        }

        public async Task ApplyLightConfig(Dictionary<int, HueLight> config)
        {
            var tasks = config
                .Select(kvp => kvp.Value)
                .Select(light =>
                {
                    var adjustment = new LightStateAdjustment
                    {
                        Hue = light.State.Hue,
                        Saturation = light.State.Saturation,
                        Brightness = light.State.Brightness
                    };

                    return _lightService.SetLightState(light.Id, _selectedBridge, adjustment);
                }).ToList();

            Log($"Telling bridge ({SelectedBridge.IpAddress}) to update lights...");

            await Task.WhenAll(tasks).ConfigureAwait(false);

            Log("Lights updated.");

            ResetCommitTimeout(config);
        }

        public async Task Load()
        {
            var config = await _appConfigRepo.GetAsync().ConfigureAwait(false);

            Log($"Found {config.HueBridges.Count} bridge(s).");

            Bridges = new ObservableCollection<HueBridge>(config.HueBridges);
            SelectedBridge = Bridges.FirstOrDefault();
            
            var lights = await _lightService.GetLightSetup(SelectedBridge).ConfigureAwait(false);
            _lightSetupHistory.Apply(lights);
            
            Log($"Loaded {lights.Count} lights!");

            Lights = new ObservableCollection<HueLight>(lights.Select(kv => kv.Value));
            SelectedLight = Lights.FirstOrDefault();

            // TODO
            SavedScenes = new ObservableCollection<Scene>(new List<Scene>
            {
                new Scene { Name = "Scene 1"},
                new Scene { Name = "Scene 2"},
                new Scene { Name = "Scene 3"}
            });
        }

        private void ResetCommitTimeout(Dictionary<int, HueLight> config)
        {
            if (_commitTimer != null && _commitTimer.Enabled)
                _commitTimer.Enabled = false;
            
            _commitTimer = new System.Timers.Timer(CommitTimeoutMilliseconds) {Enabled = true, AutoReset = false};
            Log($"Current configuration will be committed in {CommitTimeoutMilliseconds}ms");

            _commitTimer.Elapsed += (sender, args) =>
            {
                _lightSetupHistory.Apply(config);
                Log("Current configuration was committed.");
            };
        }
    }
}
