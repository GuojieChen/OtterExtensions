using System;
using System.ComponentModel;
using System.IO;
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
    public class EnsureAppSettingOperation : EnsureOperation<AppSettingConfiguration>
    {
        protected override ExtendedRichDescription GetDescription(IOperationConfiguration config)
        {
            return
                new ExtendedRichDescription(new RichDescription("Ensure AppSetting ",
                    new Hilite(config[nameof(AppSettingConfiguration.Key)]), " = ",
                    new Hilite(config[nameof(AppSettingConfiguration.Value)])));
        }

        public override async Task<PersistedConfiguration> CollectAsync(IOperationExecutionContext context)
        {

            this.LogInformation($"Ensure AppSetting {this.Template.Key} = {this.Template.Value} in {this.Template.File}");

            if(!File.Exists(this.Template.File))
                return new AppSettingConfiguration
                {
                    Key = this.Template.Key,
                };

            try
            {
                var document = XDocument.Load(this.Template.File);
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
            catch (Exception)
            {

                return new AppSettingConfiguration
                {
                    Key = this.Template.Key,
                    File = this.Template.File,
                    Value = "Error!"
                };
            }
        }

        public override async Task ConfigureAsync(IOperationExecutionContext context)
        {
            this.LogInformation($"AppSetting \"{this.Template.Key}\" set to \"{this.Template.Value}\" in \"{this.Template.File}\"");

            var document = XDocument.Load(this.Template.File);
            var appsettingnode = document.Descendants("configuration").Descendants("appSettings").FirstOrDefault();
            if (appsettingnode == null)
            {
                this.LogError("AppSettings node null");

                return; 
            }
            var node = appsettingnode.Descendants("add").FirstOrDefault(item => item.Attribute("key").Value == this.Template.Key);
            if (node == null)
            {
                this.LogInformation("node not exists");
                node = new XElement("add",new XAttribute("key",this.Template.Key),new XAttribute("value",this.Template.Value));

                appsettingnode.Add(node);
            }
            else
            {
                node.Attribute("value").Value = this.Template.Value;
            }


            document.Save(this.Template.File);

        }
    }
}
