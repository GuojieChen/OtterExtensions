using System;
using System.ComponentModel;
using Inedo.Documentation;
using Inedo.Otter.Extensibility;
using Inedo.Otter.Extensibility.Configurations;
using Inedo.Serialization;

namespace OtterExtensions.Redis
{
    [Serializable]
    [DisplayName("Ensure Redis Setting")]
    public class RedisSettingConfiguration : PersistedConfiguration
    {
        [ConfigurationKey]
        [Persistent]
        [Required]
        [ScriptAlias("Key")]
        [DisplayName("Redis setting key")]
        [Description("The configuration key name represented by the ensuring operation.")]
        public string Key { get; set; }

        [Persistent]
        [Required]
        [ScriptAlias("File")]
        [DisplayName("Config file path")]
        [Description("The file path of the configuration file, typically redis.windows.conf.")]
        public string File { get; set; }

        [Persistent]
        [ScriptAlias("Value")]
        [DisplayName("Redis setting value")]
        [Description("The expected value used by the ensuring operation that causes the Configure operation to run if it does not match the value returned from the Collect phase.")]
        public string Value { get; set; }


        [Persistent]
        [ScriptAlias("Enabled")]
        [DisplayName("Enabled")]
        [Description("Determines whether the configuration described by the ensuring operation should be enabled in redis setting. The default value is true. Ensuring operations with this value set to false will cause the object to be disabled from the redis conf file.")]
        public bool Enabled { get; set; }

        public RedisSettingConfiguration()
        {
            Enabled = true;
        }
    }
}
