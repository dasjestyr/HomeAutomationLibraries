using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Renci.SshNet;

namespace Provausio.CellphoneMonitor
{
    public class MonitorSettings
    {
        public string CellphoneMacAddress { get; set; }
        public string RouterAddress { get; set; }
        public string RouterLogin { get; set; }
        public string RouterPassword { get; set; }
    }

    public class CellphoneMonitor : IDisposable
    {
        private readonly MonitorSettings _settings;
        private const string ListCommand = "/usr/sbin/wl -i eth2 assoclist";
        
        private bool _isRunning;
        private readonly SshClient _client;

        public event EventHandler PhoneLoggedOn;
        public event EventHandler PhoneLoggedOff;

        public CellphoneMonitor(MonitorSettings settings)
        {
            _settings = settings;
            _client = new SshClient(
                settings.RouterAddress, 
                settings.RouterLogin, 
                settings.RouterPassword);
        }

        public async void Start()
        {
            if(!_client.IsConnected)
                _client.Connect();

            _isRunning = true;
            var isOnline = false;

            while (_isRunning)
            {
                var isStillLoggedOn = IsStillLoggedOn();

                if (isOnline && !isStillLoggedOn)
                {
                    isOnline = false;
                    OnLoggedOff();
                }
                else if (!isOnline && isStillLoggedOn)
                {
                    isOnline = true;
                    OnLoggedOn();
                }

                await Task.Delay(1000);
            }
        }

        private void OnLoggedOff()
        {
            PhoneLoggedOff?.Invoke(this, EventArgs.Empty);
        }

        private void OnLoggedOn()
        {
            PhoneLoggedOn?.Invoke(this, EventArgs.Empty);
        }

        private bool IsStillLoggedOn()
        {
            var result = _client.RunCommand(ListCommand);
            var addresses = ListAllMacAddresses(result.Result);
            return addresses.Contains(_settings.CellphoneMacAddress);
        }

        private static IEnumerable<string> ListAllMacAddresses(string input)
        {
            var lines = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            return lines
                .Select(line => line.Split(' '))
                .Select(parts => parts[1])
                .ToList();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            if(_client.IsConnected)
                _client.Disconnect();

            _client.Dispose();
        }
    }
}
