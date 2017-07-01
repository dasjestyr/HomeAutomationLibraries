using System.Diagnostics;
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
        private CellphoneMonitor _cellphoneMonitor;

        public bool Start(HostControl hostControl)
        {
            _cellphoneMonitor = new CellphoneMonitor();

            _cellphoneMonitor.PhoneLoggedOff += (sender, args) => Trace.WriteLine("Phone logged off!");
            _cellphoneMonitor.PhoneLoggedOn += (sender, args) => Trace.WriteLine("Phone logged on!");

            _cellphoneMonitor.Start();

            return true;
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
