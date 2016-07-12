using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Inedo.Diagnostics;
using Inedo.Documentation;
using Inedo.Otter.Extensibility.Configurations;
using Inedo.Otter.Extensibility.Operations;

namespace OtterExtensions.WebConfig
{
    public class EnsureConnectionSettings : EnsureOperation<ConnectionSettingConfiguration>
    {
        protected override ExtendedRichDescription GetDescription(IOperationConfiguration config)
        {
            if (string.IsNullOrEmpty(config[nameof(ConnectionSettingConfiguration.Name)]))
            {
                return new ExtendedRichDescription(new RichDescription("Ensure connectionstring of ", new Hilite(config[nameof(ConnectionSettingConfiguration.Name)]), " from Chocolatey is installed"));
            }
            return new ExtendedRichDescription(new RichDescription("Ensure connectionstring ", new Hilite(config[nameof(ConnectionSettingConfiguration.ConnectionString)]), " of ", new Hilite(config[nameof(ConnectionSettingConfiguration.Name)])));
        }

        public override async Task<PersistedConfiguration> CollectAsync(IOperationExecutionContext context)
        {

            this.LogInformation($"config file : ${this.Template.FileName}.");
            this.LogInformation($"name : ${this.Template.FileName}.");

            XDocument document = XDocument.Load(this.Template.FileName);
            var connectionString = document.Descendants("connectionStrings")
                       .Descendants("add")
                       .First(x => x.Attribute("name").Value == this.Template.Name).Attribute("connectionString").Value;

            return new ConnectionSettingConfiguration
            {
                FileName = this.Template.FileName,
                Name = this.Template.Name,
                ConnectionString = connectionString
            };
        }

        public override async Task ConfigureAsync(IOperationExecutionContext context)
        {
            this.LogInformation($"config file : ${this.Template.FileName}.");
            this.LogInformation($"name : ${this.Template.FileName}.");

            XDocument document = XDocument.Load(this.Template.FileName);
            document.Descendants("connectionStrings")
                       .Descendants("add")
                       .First(x => x.Attribute("name").Value == this.Template.Name).Attribute("connectionString").Value = this.Template.ConnectionString;
            document.Save(this.Template.FileName);

        }
    }
}
