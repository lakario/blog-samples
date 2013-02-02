using System.Configuration;
using System.Text.RegularExpressions;

namespace RedirectModule.Configuration
{
    public class RedirectionElement : ConfigurationElement
    {
        private const string TargetUrlKeyName = "targetUrl";
        private const string DestinationUrlKeyName = "destinationUrl";
        private const string IgnoreCaseKeyName = "ignoreCase";
        private const string PermanentKeyName = "permanent";

        [ConfigurationProperty(TargetUrlKeyName, IsRequired = true, IsKey = true)]
        public string TargetUrl {
            get { return (string)this[TargetUrlKeyName]; }
            set { this[TargetUrlKeyName] = value; }
        }

        [ConfigurationProperty(DestinationUrlKeyName, IsRequired = true)]
        public string DestinationUrl {
            get { return (string)this[DestinationUrlKeyName]; }
            set { this[DestinationUrlKeyName] = value; }
        }

        [ConfigurationProperty(IgnoreCaseKeyName, IsRequired = false, DefaultValue = false)]
        public bool IgnoreCase {
            get { return (bool)this[IgnoreCaseKeyName]; }
            set { this[IgnoreCaseKeyName] = value; }
        }

        [ConfigurationProperty(PermanentKeyName, IsRequired = false, DefaultValue = false)]
        public bool Permanent {
            get { return (bool)this[PermanentKeyName]; }
            set { this[PermanentKeyName] = value; }
        }

        private bool _isAbsoluteUrl;
        public bool IsAbsoluteUrl {
            get { return _isAbsoluteUrl; }
            set { _isAbsoluteUrl = value; }
        }

        private Regex _regex;
        public Regex Regex {
            get { return _regex; }
            set { _regex = value; } 
        }

        public RedirectionElement(string targetUrl, string destinationUrl, bool permanent, bool ignoreCase) {
            TargetUrl = targetUrl;
            DestinationUrl = destinationUrl;
            IgnoreCase = ignoreCase;
            Permanent = permanent;
        }

        public RedirectionElement() { }
    }

    public class RedirectionElementCollection : ConfigurationElementCollection
    {
        public new RedirectionElement this[string targetUrl] {
            get { return (RedirectionElement)BaseGet(targetUrl); }
        }

        public RedirectionElement this[int index] {
            get { return (RedirectionElement)BaseGet(index); }
            set {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement() {
            return new RedirectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element) {
            return ((RedirectionElement)element).TargetUrl;
        }
    }
}