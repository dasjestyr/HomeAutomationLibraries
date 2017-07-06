using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        public ObservableCollection<HueBridge> Bridges
        {
            get => _bridges;
            set => SetProperty(ref _bridges, value);
        }

        public HueBridge SelectedBridge
        {
            get => _selectedBridge;
            set => SetProperty(ref _selectedBridge, value);
        }

        public ushort SelectedHue
        {
            get => _selectedHue;
            set => SetProperty(ref _selectedHue, value);
        }

        public byte SelectedBrightness
        {
            get => _selectedBrightness;
            set => SetProperty(ref _selectedBrightness, value);
        }

        public byte SelectedSaturation
        {
            get => _selectedSaturation;
            set => SetProperty(ref _selectedSaturation, value);
        }

        public ICommand UpdateLights => new DelegateCommand(UpdateColor);

        public HomeModel()
        {
            _appConfigRepo = new ApplicationConfigurationRepository();
            _lightService = new LightService();

            var config = _appConfigRepo.Get().Result;

            Bridges = new ObservableCollection<HueBridge>(config.HueBridges);
            SelectedBridge = Bridges.FirstOrDefault();

            // TODO: set values to whatever they are at currently
            SelectedHue = ushort.MaxValue;
            SelectedBrightness = byte.MaxValue;
            SelectedSaturation = byte.MaxValue;
        }

        public async void UpdateColor()
        {
            var adjustment = new LightStateAdjustment
            {
                Hue = SelectedHue,
                Saturation = SelectedSaturation,
                Brightness = SelectedBrightness
            };

            await _lightService.SetLightState(12, SelectedBridge, adjustment).ConfigureAwait(false);
        }
    }
}
