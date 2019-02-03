using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Provausio.PhillipsHue.Lights;

namespace Provausio.PhillipsHue.Configuration.ApplicationConfiguration
{
    public class ApplicationFileManager
    {
        public readonly string ApplicationDirectory;
        public readonly string ApplicationName;

        public ApplicationFileManager(string applicationName)
        {
            ApplicationName = applicationName;
            ApplicationDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);

            VerifyDirectory();
        }

        public async Task<T> LoadFromJson<T>(string fileName)
            where T : class, new()
        {
            var filePath = Path.Combine(ApplicationDirectory, fileName);
            if(!File.Exists(filePath))
                throw new FileNotFoundException("Could not find file.", filePath);

            T result = null;

            await Task.Run(() =>
            {
                var jsonText = File.ReadAllText(filePath);
                result = JsonConvert.DeserializeObject<T>(jsonText);
            }).ConfigureAwait(false);

            return result;
        }

        public async Task SaveFile(string fileName, object file)
        {
            var filePath = Path.Combine(ApplicationDirectory, fileName);
            await Task.Run(() =>
            {
                var asJson = JsonConvert.SerializeObject(file, Formatting.Indented);
                File.WriteAllText(filePath, asJson);
            });
        }

        public bool FileExists(string filename)
        {
            return File.Exists(Path.Combine(ApplicationName, filename));
        }

        private void VerifyDirectory()
        {
            if (Directory.Exists(ApplicationDirectory))
                return;

            Directory.CreateDirectory(ApplicationDirectory);
        }
    }

    public class LightConfiguration
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("lights")]
        public Dictionary<int, HueLight> Lights { get; set; }
    }
}
