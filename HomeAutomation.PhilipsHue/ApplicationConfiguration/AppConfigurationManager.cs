using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace HomeAutomation.PhilipsHue.ApplicationConfiguration
{
    public class AppConfigurationManager
    {
        private readonly string _applicationName;
        private readonly string _deviceType;
        private const string ConfigFileName = "user_config.json";

        private readonly string _filePath;
        
        public AppConfigurationData AppConfiguration { get; private set; }

        public AppConfigurationManager(string applicationName, string deviceType)
        {
            _applicationName = applicationName;
            _deviceType = deviceType;

            var appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _filePath = Path.Combine(appDataDirectory, _applicationName, ConfigFileName);

            LoadFromFile();
        }

        private void LoadFromFile()
        {
            AppConfiguration = GetConfigFromFile(_filePath);
        }

        public void SaveConfiguration()
        {
            var asJson = JsonConvert.SerializeObject(AppConfiguration, Formatting.Indented);
            File.WriteAllText(_filePath, asJson);
        }

        private AppConfigurationData GetConfigFromFile(string filePath)
        {
            VerifyUserConfig(filePath);

            var file = File.ReadAllText(filePath);
            var config = JsonConvert.DeserializeObject<AppConfigurationData>(file);
            return config;
        }

        private void VerifyUserConfig(string filePath)
        {
            if (File.Exists(filePath))
                return;

            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var userData = new AppConfigurationData();
            userData.ApplicationName = _applicationName;
            userData.DeviceType = _deviceType;

            var asJson = JsonConvert.SerializeObject(userData, Formatting.Indented);
            File.WriteAllLines(filePath, new List<string> { asJson }, Encoding.UTF8);
        }
    }
}