using System;
using System.ComponentModel;
using Inedo.Documentation;
using Inedo.Otter.Extensibility;
using Inedo.Otter.Extensibility.Configurations;
using Inedo.Serialization;

namespace OtterExtensions.AppConfig
{
    [Serializable]
    [DisplayName("ConnectionSettings")]
    public sealed class ConnectionSettingConfiguration : PersistedConfiguration
    {
        [ConfigurationKey]
        [Persistent]
        [Required]
        [ScriptAlias("Name")]
        [DisplayName("Connection Name")]
        [Description("The configuration key name represented by the ensuring operation.")]
        public string Name { get; set; }


        [Persistent]
        [Required]
        [ScriptAlias("File")]
        [DisplayName("Config file path")]
        [Description("The file path of the configuration file, typically web.config or app.config.")]
        public string File { get; set; }


        [Persistent]
        [Required]
        [ScriptAlias("ConnectionString")]
        [DisplayName("ConnectionString")]
        [Description("The expected connectionstring used by the ensuring operation that causes the Configure operation to run if it does not match the value returned from the Collect phase.")]
        public string ConnectionString { get; set; }
    }
}
