using System.Threading.Tasks;

namespace HomeAutomation.PhilipsHue.Configuration.ApplicationConfiguration
{
    public class AppConfigurationManager : ApplicationFileManager
    {
        private readonly string _deviceType;
        private const string ConfigFileName = "user_config.json";
        
        public AppConfigurationData AppConfiguration { get; }

        public AppConfigurationManager(string deviceType)
        {
            _deviceType = deviceType;

            VerifyUserConfig();
            AppConfiguration = LoadFromJson<AppConfigurationData>(ConfigFileName).Result;
        }

        public async Task SaveConfiguration()
        {
            await SaveFile(ConfigFileName, AppConfiguration);
        }

        private void VerifyUserConfig()
        {
            if (FileExists(ConfigFileName))
                return;

            var userData = new AppConfigurationData
            {
                ApplicationName = ApplicationName,
                DeviceType = _deviceType
            };

            SaveFile(ConfigFileName, userData).Wait();
        }
    }
}