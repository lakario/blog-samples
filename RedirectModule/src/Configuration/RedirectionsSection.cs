using System.Configuration;

namespace RedirectModule.Configuration
{
    public class RedirectionsSection : ConfigurationSection
    {
        public const string SectionName = "redirections";

        private static RedirectionsSection _settings;

        public static RedirectionsSection Settings {
            get {
                if (_settings == null) {
                    object section = ConfigurationManager.GetSection(SectionName);
                    if (section != null) {
                        _settings = section as RedirectionsSection;
                    }
                }
                return _settings;
            }
        }

        public static bool HasRedirects
        { 
            get { return Settings != null && Settings.Redirections != null && Settings.Redirections.Count > 0;  }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        public RedirectionElementCollection Redirections {
            get { return (RedirectionElementCollection)base[""]; }
        }
    }

    public enum UrlType
    {
        Relative,
        Absolute
    }
}
