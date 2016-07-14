using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Inedo.Diagnostics;
using Inedo.Documentation;
using Inedo.Otter.Extensibility;
using Inedo.Otter.Extensibility.Configurations;
using Inedo.Otter.Extensibility.Operations;

namespace OtterExtensions.Redis
{
    [DisplayName("Ensure Redids Setting")]
    [Description("Ensures redis configuration file has the specified key/value pair.")]
    [ScriptNamespace("OtterExtensions")]
    [ScriptAlias("Ensure-RedisConf")]
    [Tag("dotnet")]
    public class EnsureRedisSettingOperation : EnsureOperation<RedisSettingConfiguration>
    {
        protected override ExtendedRichDescription GetDescription(IOperationConfiguration config)
        {
            return new ExtendedRichDescription(new RichDescription($"Ensure redis configuration ",$"{(Convert.ToBoolean(config[nameof(RedisSettingConfiguration.Enabled)])?"":"# ")}",new Hilite(config[nameof(RedisSettingConfiguration.Key)]), " ", new Hilite(config[nameof(RedisSettingConfiguration.Value)])));
        }

        public override async Task<PersistedConfiguration> CollectAsync(IOperationExecutionContext context)
        {
            this.LogInformation($"Ensure {this.Template.Key} = {this.Template.Value} in {this.Template.File}");

            var lines = File.ReadLines(this.Template.File);
            string value = string.Empty;
            bool enabled = false;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var str = line.Trim();
                if (string.IsNullOrEmpty(str))
                    continue;
                if (line.Trim().TrimStart('#').Trim().StartsWith(this.Template.Key))
                {
                    var keylen = this.Template.Key.Length;
                    var newline = line.Trim().TrimStart('#').Trim();

                    value = newline.Substring(keylen + 1, newline.Length - keylen - 1);

                    enabled = !line.Trim().StartsWith("#");
                    break;
                }
            }

            return new RedisSettingConfiguration
            {
                Key = this.Template.Key,
                File = this.Template.File,
                Value = value,
                Enabled = enabled
            };
        }

        public override async Task ConfigureAsync(IOperationExecutionContext context)
        {
            this.LogInformation($"Redis configuration \"{(this.Template.Enabled ? "" : "#")}{this.Template.Key} {this.Template.Value}\" in {this.Template.File}");

            var lines = File.ReadAllLines(this.Template.File);
            var saveOk = false;
            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];
                if (string.IsNullOrEmpty(line))
                    continue;
                var str = line.Trim();
                if (string.IsNullOrEmpty(str))
                    continue;
                if (line.Trim().TrimStart('#').Trim().StartsWith(this.Template.Key))
                {
                    //var keylen = this.Template.Key.Length;
                    //var value = line.Substring(keylen + 1, line.Length - keylen - 1).Trim();

                    if (this.Template.Enabled)
                        lines[i] = $"{this.Template.Key} {this.Template.Value}";
                    else
                        lines[i] = $"# {lines[i]}";

                    saveOk = true;
                    break;
                }
            }

            File.WriteAllLines(this.Template.File, lines);

            if (this.Template.Enabled && !saveOk)
            {
                this.LogInformation($"Append \"{this.Template.Key} {this.Template.Value}\" to {this.Template.File}");

                File.AppendAllLines(this.Template.File, new string[] { $"{this.Template.Key} {this.Template.Value}" });
            } 
        }
    }
}
