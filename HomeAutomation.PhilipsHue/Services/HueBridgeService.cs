using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DAS.Infrastructure.WebClient;
using HomeAutomation.PhilipsHue.ApplicationConfiguration;
using HomeAutomation.PhilipsHue.Bridge;
using HomeAutomation.PhilipsHue.Services.HueApi;
using HomeAutomation.PhilipsHue.Services.HueApi.Bridge;

namespace HomeAutomation.PhilipsHue.Services
{
    public class HueBridgeService
    {
        private readonly AppConfigurationManager _appConfigManager;
        private readonly WebClient _webClient;

        public HueBridgeService(AppConfigurationManager appConfigManager)
        {
            _appConfigManager = appConfigManager;
            _webClient = new WebClient();
        }

        public async Task<IEnumerable<HueBridge>> FindBridges(TimeSpan timeout)
        {
            var locateBridgeRequest = new LocateBridgesRequest();
            var result = await _webClient
                .SendAsync<List<HueBridge>>(locateBridgeRequest)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<HueBridgeConfiguration> GetBridgeConfiguration(HueBridge bridge)
        {
            var request = new GetBridgeConfigurationRequest(bridge);
            var response = await _webClient
                .SendAsync<HueBridgeConfiguration>(request)
                .ConfigureAwait(false);

            return response;
        }

        public async Task<List<PairingResponseMessage>> PairBridge(
            string appName, 
            string deviceName, 
            TimeSpan timeout,
            HueBridge bridge)
        {
            var pairingRequest = new PairingRequest(appName, deviceName, bridge);

            var start = DateTime.Now;
            bool succeeded;
            List<PairingResponseMessage> response;
            do
            {
                response = await _webClient
                    .SendAsync<List<PairingResponseMessage>>(pairingRequest)
                    .ConfigureAwait(false);

                succeeded = response.All(r => r.Error?.Type != 101);
                await Task.Delay(1000);

            } while (!succeeded && DateTime.Now - start < timeout);

            var first = response[0];
            if (first.Succcess != null)
            {
                bridge.Username = first.Succcess.Username;

                _appConfigManager.AppConfiguration.HueBridges.RemoveAll(b => b.IpAddress == bridge.IpAddress);
                _appConfigManager.AppConfiguration.HueBridges.Add(bridge);
                _appConfigManager.SaveConfiguration();
            }

            return response;
        }
    }

    public class GetBridgeConfigurationRequest : PhilipsHueRequest
    {
        public GetBridgeConfigurationRequest(HueBridge bridge) 
            : base(HttpMethod.Get, bridge)
        {
        }

        protected override void SetRequest(IResourceBuilder builder)
        {
            base.SetRequest(builder);
            builder.WithPath($"{Bridge.Username}/config");
        }
    }
}
