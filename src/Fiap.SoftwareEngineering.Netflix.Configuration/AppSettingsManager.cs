using Microsoft.Extensions.Configuration;

namespace Fiap.SoftwareEngineering.Netflix.Configuration
{
    public class AppSettingsManager<TSettings> where TSettings : class
    {
        public static TSettings Settings { get; private set; }

        public AppSettingsManager()
        {}

        public AppSettingsManager(IConfiguration configuration) : this(configuration, string.Empty)
        { }

        public AppSettingsManager(IConfiguration configuration, string sectionName) => Load(configuration, sectionName);

        public static void Load(IConfiguration configuration) => Load(configuration, string.Empty);

        public static void Load(IConfiguration configuration, string sectionName)
        {
            if (!string.IsNullOrEmpty(sectionName))
            {
                Settings = configuration.GetSection(sectionName).Get<TSettings>();
                return;
            }

            Settings = configuration.Get<TSettings>();
        }

        public static void Clear() => Settings = null;
    }
}
