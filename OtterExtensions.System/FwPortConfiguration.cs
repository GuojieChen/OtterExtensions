using System;
using System.ComponentModel;
using Inedo.Documentation;
using Inedo.Otter.Extensibility;
using Inedo.Otter.Extensibility.Configurations;
using Inedo.Serialization;

namespace OtterExtensions.System
{
    [Serializable]
    [DisplayName("FwPort")]
    public class FwPortConfiguration : PersistedConfiguration
    {
        [ConfigurationKey]
        [Persistent]
        [Required]
        [ScriptAlias("Name")]
        [DisplayName("key")]
        [Description("The configuration key name represented by the ensuring operation.")]
        public string Name { get; set; }

        [Persistent]
        [Required]
        [ScriptAlias("Protocal")]
        [DisplayName("TCP or UPD")]
        [Description("The configuration key name represented by the ensuring operation.")]
        public string Protocal { get; set; }

        [Persistent]
        [Required]
        [ScriptAlias("Ports")]
        [DisplayName("Port")]
        [Description("")]
        public int Port { get; set; }

        public FwPortConfiguration()
        {
            Protocal = "";
        }
    }
}
