using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Provausio.PhillipsHue.Configuration.ApplicationConfiguration;

namespace Provausio.PhillipsHue.Configuration
{
    public class ApplicationConfigurationRepository : IRepository<AppConfigurationData>
    {
        private const string Filename = "user_config.json";
        private readonly ApplicationFileManager _fileManager;

        private static AppConfigurationData _cachedConfig;

        public ApplicationConfigurationRepository(string applicationName)
        {
            _fileManager = new ApplicationFileManager(applicationName);
        }

        public async Task<List<AppConfigurationData>> GetAllAsync()
        {
            var config = await GetAsync();
            return new List<AppConfigurationData> {config};
        }

        public async Task<AppConfigurationData> GetAsync(string key = null)
        {
            if (_cachedConfig != null)
                return _cachedConfig;

            var configuration = await _fileManager
                .LoadFromJson<AppConfigurationData>(Filename)
                .ConfigureAwait(false);

            _cachedConfig = configuration;
            return configuration;
        }

        public async Task SaveAsync(AppConfigurationData input)
        {
            await _fileManager.SaveFile(Filename, input);
            _cachedConfig = input;
        }

        public Task DeleteAsync(string key = null)
        {
            throw new NotImplementedException("Deletion of application config is not supported.");
        }
    }
}