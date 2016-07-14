using System;
using System.ComponentModel;
using Inedo.Documentation;
using Inedo.Otter.Extensibility;
using Inedo.Otter.Extensibility.Configurations;
using Inedo.Serialization;

namespace OtterExtensions.Firewall
{
    [Serializable]
    [DisplayName("Ensure FwPorts")]
    public class FwPortConfiguration : PersistedConfiguration
    {
        [ConfigurationKey]
        [Persistent]
        [Required]
        [ScriptAlias("Name")]
        [DisplayName("AppSetting key")]
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
        [DisplayName("Specific local ports")]
        [Description("Example:80,443,5000-5010")]
        public string Ports { get; set; }

        
    }
}
