using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Inedo.Diagnostics;
using Inedo.Documentation;
using Inedo.Otter.Extensibility;
using Inedo.Otter.Extensibility.Configurations;
using Inedo.Otter.Extensibility.Operations;
using NetFwTypeLib;

namespace OtterExtensions.System
{
    [DisplayName("Ensure Firewall Port")]
    [Description("Ensures Firewall Port.")]
    [ScriptNamespace("OtterExtensions")]
    [ScriptAlias("Ensure-FwPort")]
    [Tag("system")]
    public class EnsureFwPortOperation : EnsureOperation<FwPortConfiguration>
    {
        protected override ExtendedRichDescription GetDescription(IOperationConfiguration config)
        {

            return new ExtendedRichDescription(new RichDescription("Ensure firewall Port ", new Hilite(config[nameof(FwPortConfiguration.Name)]), " ", new Hilite(config[nameof(FwPortConfiguration.Protocal)]), " ", new Hilite(config[nameof(FwPortConfiguration.Port)])));
        }

        public override async Task<PersistedConfiguration> CollectAsync(IOperationExecutionContext context)
        {
            this.LogInformation($"Ensure firewall port {this.Template.Name} {this.Template.Protocal} {this.Template.Port}");
            //创建firewall管理类的实例
            var netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));

            var conf = new FwPortConfiguration
            {
                Name = this.Template.Name
            };

            NET_FW_IP_PROTOCOL_ protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP; 
            if (this.Template.Protocal.ToUpper() == "TCP")
            {
                protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            }
            

            foreach (INetFwOpenPort mPort in netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts)
            {
                if (Equals(mPort.Name.ToUpper(), this.Template.Name.ToUpper()) && Equals(mPort.Protocol, protocol) && this.Template.Port == mPort.Port)
                {
                    conf.Name = mPort.Name;
                    conf.Port = mPort.Port;
                    conf.Protocal = mPort.Protocol == NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP?"TCP":"UDP";
                    break;
                }
            }

            return conf;
        }

        public override async Task ConfigureAsync(IOperationExecutionContext context)
        {
            //创建firewall管理类的实例  
            INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));

            INetFwOpenPort objPort = (INetFwOpenPort)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwOpenPort"));

            objPort.Name = this.Template.Name;
            objPort.Port = this.Template.Port;
            if (this.Template.Protocal.ToUpper() == "TCP")
            {
                objPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            }
            else
            {
                objPort.Protocol = NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
            }
            objPort.Scope = NET_FW_SCOPE_.NET_FW_SCOPE_ALL;
            objPort.Enabled = true;

            bool exist = false;
            //加入到防火墙的管理策略  
            foreach (INetFwOpenPort mPort in netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts)
            {
                if (Equals(mPort.Name.ToUpper(), this.Template.Name.ToUpper()) && Equals(mPort.Protocol, objPort.Protocol) && objPort.Port == mPort.Port)
                {
                    exist = true;
                    break;
                }
            }
            if (exist)
                this.LogInformation($"firewall port exists");
            else
            {
                this.LogInformation($"set firewall port {this.Template.Name} {this.Template.Protocal} {this.Template.Port}");

                netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Add(objPort);
            }
        }
    }
}
