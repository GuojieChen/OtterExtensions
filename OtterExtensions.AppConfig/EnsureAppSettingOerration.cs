using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Inedo.Diagnostics;
using Inedo.Documentation;
using Inedo.Otter.Extensibility;
using Inedo.Otter.Extensibility.Configurations;
using Inedo.Otter.Extensibility.Operations;

namespace OtterExtensions.AppConfig
{
    [DisplayName("Ensure AppSetting(V2)")]
    [Description("Ensures a .NET application configuration file has the specified appSetting key/value pair.")]
    [ScriptNamespace("OtterExtensions")]
    [ScriptAlias("Ensure-AppSetting-V2")]
    [Tag("dotnet")]
    public class EnsureAppSettingOerration : EnsureOperation<AppSettingConfiguration>
    {
        protected override ExtendedRichDescription GetDescription(IOperationConfiguration config)
        {
            return new ExtendedRichDescription(new RichDescription("Ensure ", new Hilite(config[nameof(AppSettingConfiguration.Key)]), " = ", new Hilite(config[nameof(AppSettingConfiguration.Value)])));
        }

        public override async Task<PersistedConfiguration> CollectAsync(IOperationExecutionContext context)
        {

            this.LogInformation($"Ensure {this.Template.Key} = {this.Template.Value} in {this.Template.File}");

            XDocument document = XDocument.Load(this.Template.File);
            var value = document.Descendants("appSettings")
                       .Descendants("add")
                       .First(x => x.Attribute("key").Value == this.Template.Key).Attribute("value").Value;

            return new AppSettingConfiguration
            {
                Key = this.Template.Key,
                File = this.Template.File,
                Value = value
            };
        }

        public override async Task ConfigureAsync(IOperationExecutionContext context)
        {
            this.LogInformation($"AppSetting \"{this.Template.Key}\" set to \"{this.Template.Value}\" in \"{this.Template.File}\"");

            XDocument document = XDocument.Load(this.Template.File);
            document.Descendants("appSettings")
                       .Descendants("add")
                       .First(x => x.Attribute("name").Value == this.Template.Key).Attribute("value").Value = this.Template.Value;
            document.Save(this.Template.File);

        }
    }
}
