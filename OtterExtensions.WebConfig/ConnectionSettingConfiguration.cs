using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inedo.Documentation;
using Inedo.Otter.Extensibility;
using Inedo.Otter.Extensibility.Configurations;
using Inedo.Serialization;


namespace OtterExtensions.WebConfig
{
    [Serializable]
    [DisplayName("ConnectionSettings")]
    public sealed class ConnectionSettingConfiguration : PersistedConfiguration
    {
        [ConfigurationKey]
        [Persistent]
        [Required]
        [ScriptAlias("name")]
        [DisplayName("Connection Name")]
        public string Name { get; set; }


        [Persistent]
        [Required]
        [ScriptAlias("filename")]
        [DisplayName("FileName")]
        public string FileName { get; set; }


        [Persistent]
        [Required]
        [ScriptAlias("connectionstring")]
        [DisplayName("connectionstring")]
        [Description("connectionstring")]
        public string ConnectionString { get; set; }
    }
}
