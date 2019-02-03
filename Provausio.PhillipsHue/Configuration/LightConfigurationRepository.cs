using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Provausio.PhillipsHue.Configuration.ApplicationConfiguration;

namespace Provausio.PhillipsHue.Configuration
{
    public class LightConfigurationRepository : IRepository<LightConfiguration>
    {
        private const string Filename = "light-configuration";
        private readonly ApplicationFileManager _fileManager;

        private static List<LightConfiguration> _cachedConfiguration;

        public LightConfigurationRepository(string applicationName)
        {
            _fileManager = new ApplicationFileManager(applicationName);
        }

        public async Task<List<LightConfiguration>> GetAllAsync()
        {
            if (_cachedConfiguration != null)
                return _cachedConfiguration;

            var config = await _fileManager.LoadFromJson<List<LightConfiguration>>(Filename);
            _cachedConfiguration = config;
            return config;
        }

        public async Task<LightConfiguration> GetAsync(string key)
        {
            var configurations = await GetAllAsync();
            return configurations.FirstOrDefault(
                config => config.Name.Equals(key, StringComparison.OrdinalIgnoreCase));
        }

        public async Task SaveAsync(LightConfiguration input)
        {
            var configs = await GetAllAsync();

            configs.RemoveAll(config => config.Name.Equals(input.Name));
            configs.Add(input);

            await _fileManager.SaveFile(Filename, configs);
            _cachedConfiguration = configs;
        }

        public async Task DeleteAsync(string key)
        {
            var configs = await GetAllAsync();
            configs.RemoveAll(config => config.Name.Equals(key));

            await _fileManager.SaveFile(Filename, configs);
            _cachedConfiguration = configs;
        }
    }
}