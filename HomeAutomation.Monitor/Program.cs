using System;
using Topshelf;

namespace HomeAutomation.Monitor
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<HostingConfiguration>();
                x.RunAsLocalSystem();
                x.SetDescription("Home Automation Monitor");
                x.SetDisplayName("Provausio.HomeAutomation.Monitor");
                x.SetServiceName("Provausio.HomeAutomation.Monitor");
            });

            return (int) exitCode;
        }
    }

    public class HostingConfiguration : ServiceControl
    {
        private string _appName = "provausio_automation_svc";
        private string _deviceType = "destkop jeremy";

        private CellphoneMonitor _cellphoneMonitor;
        private LightService _lightSvc;
        private HueBridge _bridge;

        public HostingConfiguration()
        {
            var configManager = new AppConfigurationManager(_deviceType);
            _bridge = configManager.AppConfiguration.HueBridges[0];
            _lightSvc = new LightService();
        }

        public bool Start(HostControl hostControl)
        {
            _cellphoneMonitor = new CellphoneMonitor();

            _cellphoneMonitor.PhoneLoggedOff += SignalImAway;
            _cellphoneMonitor.PhoneLoggedOn += SignalThatImHome;

            _cellphoneMonitor.Start();

            return true;
        }

        private void SignalThatImHome(object sender, EventArgs eventArgs)
        {
            var setting = new LightStateAdjustment
            {
                IsOn = true,
                Saturation = byte.MaxValue,
                Hue = 25500
            };

            _lightSvc.SetLightState(12, _bridge, setting).Wait();
        }

        private void SignalImAway(object o, EventArgs eventArgs)
        {
            var setting = new LightStateAdjustment
            {
                IsOn = true,
                Saturation = byte.MaxValue,
                Hue = 0
            };

            _lightSvc.SetLightState(12, _bridge, setting).Wait();
        }


        public bool Stop(HostControl hostControl)
        {
            if (_cellphoneMonitor == null)
                return false;

            _cellphoneMonitor.Stop();
            return true;
        }
    }
}
