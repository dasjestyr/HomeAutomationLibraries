using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Renci.SshNet;

namespace HomeAutomation.Monitor
{
    public delegate void PhoneLoggedOn(object sender, EventArgs e);
    public delegate void PhoneLoggedOff(object sender, EventArgs e);

    public class CellphoneMonitor : IDisposable
    {
        private const string ListCommand = "/usr/sbin/wl -i eth2 assoclist";

        private readonly string _cellPhoneMacAddress = ConfigurationManager.AppSettings["CellphoneMacAddress"].ToUpper();
        private readonly string _routerAddress = ConfigurationManager.AppSettings["RouterAddress"];
        private readonly string _routerLogin = ConfigurationManager.AppSettings["RouterLogin"];
        private readonly string _routerPassword = ConfigurationManager.AppSettings["RouterPassword"];

        private bool _isRunning;
        private readonly SshClient _client;

        public event PhoneLoggedOn PhoneLoggedOn;
        public event PhoneLoggedOff PhoneLoggedOff;

        public CellphoneMonitor()
        {
            _client = new SshClient(_routerAddress, _routerLogin, _routerPassword);
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
            return addresses.Contains(_cellPhoneMacAddress);
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
