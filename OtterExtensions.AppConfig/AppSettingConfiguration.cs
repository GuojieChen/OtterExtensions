using System;
using System.ComponentModel;
using Inedo.Documentation;
using Inedo.Otter.Extensibility;
using Inedo.Otter.Extensibility.Configurations;
using Inedo.Serialization;

namespace OtterExtensions.AppConfig
{
    [Serializable]
    [DisplayName("Ensure AppSetting(V2)")]
    public class AppSettingConfiguration : PersistedConfiguration
    {
        [ConfigurationKey]
        [Persistent]
        [Required]
        [ScriptAlias("Key")]
        [DisplayName("AppSetting key")]
        [Description("The configuration key name represented by the ensuring operation.")]
        public string Key { get; set; }

        [Persistent]
        [Required]
        [ScriptAlias("File")]
        [DisplayName("Config file path")]
        [Description("The file path of the configuration file, typically web.config or app.config.")]
        public string File { get; set; }

        [Persistent]
        [Required]
        [ScriptAlias("Value")]
        [DisplayName("AppSetting value")]
        [Description("The expected value used by the ensuring operation that causes the Configure operation to run if it does not match the value returned from the Collect phase.")]
        public string Value { get; set; }
    }
}
