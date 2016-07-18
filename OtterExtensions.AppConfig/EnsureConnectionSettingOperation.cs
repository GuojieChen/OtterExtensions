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
    [DisplayName("Ensure ConnectionString")]
    [Description("Ensures connectionstring.")]
    [ScriptNamespace("OtterExtensions")]
    [ScriptAlias("Ensure-ConnectionString")]
    [Tag("dotnet")]
    public class EnsureConnectionSettingOperation : EnsureOperation<ConnectionSettingConfiguration>
    {
        protected override ExtendedRichDescription GetDescription(IOperationConfiguration config)
        {

            return new ExtendedRichDescription(new RichDescription("Ensure connectionstring ", new Hilite(config[nameof(ConnectionSettingConfiguration.ConnectionString)]), " of ", new Hilite(config[nameof(ConnectionSettingConfiguration.Name)])));
        }

        public override async Task<PersistedConfiguration> CollectAsync(IOperationExecutionContext context)
        {

            this.LogInformation($"config file : ${this.Template.File}.");
            this.LogInformation($"name : ${this.Template.File}.");

            XDocument document = XDocument.Load(this.Template.File);
            var connectionString = document.Descendants("connectionStrings")
                       .Descendants("add")
                       .First(x => x.Attribute("name").Value == this.Template.Name).Attribute("connectionString").Value;

            return new ConnectionSettingConfiguration
            {
                File = this.Template.File,
                Name = this.Template.Name,
                ConnectionString = connectionString
            };
        }

        public override async Task ConfigureAsync(IOperationExecutionContext context)
        {
            this.LogInformation($"config file : ${this.Template.File}.");
            this.LogInformation($"name : ${this.Template.File}.");

            XDocument document = XDocument.Load(this.Template.File);

            var appsettingnode = document.Descendants("configuration").Descendants("connectionStrings").FirstOrDefault();
            if (appsettingnode == null)
            {
                this.LogError("connectionStrings node null");

                return;
            }

            var node = appsettingnode.Descendants("add").FirstOrDefault(item => item.Attribute("name").Value == this.Template.Name);
            if (node == null)
            {
                this.LogInformation("node not exists");
                node = new XElement("add");
                node.Attribute("name").Value = this.Template.Name;
                node.Attribute("value").Value = this.Template.ConnectionString;

                appsettingnode.Add(node);
            }
            else
            {
                node.Attribute("value").Value = this.Template.ConnectionString;
            }


            document.Save(this.Template.File);

        }
    }
}
