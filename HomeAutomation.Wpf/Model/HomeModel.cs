using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HomeAutomation.PhilipsHue.Bridge;
using HomeAutomation.PhilipsHue.Configuration;
using HomeAutomation.PhilipsHue.Lights;
using HomeAutomation.PhilipsHue.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace HomeAutomation.Wpf.Model
{
    public class HomeModel : BindableBase
    {
        private readonly ApplicationConfigurationRepository _appConfigRepo;
        private ObservableCollection<HueBridge> _bridges;
        private HueBridge _selectedBridge;
        private readonly LightService _lightService;

        private ushort _selectedHue;
        private byte _selectedBrightness;
        private byte _selectedSaturation;
        private HueLight _selectedLight;
        private ObservableCollection<HueLight> _lights;

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

        public ICommand UpdateLights => new DelegateCommand(UpdateColor);

        public HomeModel()
        {
            // TODO: figure out how to create a windsor activator and inject this bitches
            _appConfigRepo = new ApplicationConfigurationRepository();
            _lightService = new LightService();
        }

        public async void UpdateColor()
        {
            var adjustment = new LightStateAdjustment
            {
                Hue = SelectedLight.State.Hue,
                Saturation = SelectedLight.State.Saturation,
                Brightness = SelectedLight.State.Brightness
            };

            await _lightService.SetLightState(SelectedLight.Id, SelectedBridge, adjustment).ConfigureAwait(false);
        }

        public async Task Load()
        {
            var config = await _appConfigRepo.GetAsync().ConfigureAwait(false);
            Bridges = new ObservableCollection<HueBridge>(config.HueBridges);
            SelectedBridge = Bridges.FirstOrDefault();

            // TODO: have a light selector
            var lights = await _lightService.GetLightSetup(SelectedBridge).ConfigureAwait(false);
            
            Lights = new ObservableCollection<HueLight>(lights.Select(kv => kv.Value));
            SelectedLight = Lights.FirstOrDefault();
        }
    }
}
