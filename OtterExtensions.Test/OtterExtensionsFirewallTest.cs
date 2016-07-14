using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;
using NUnit.Framework;

namespace OtterExtensions.Test
{
	[TestFixture]
    public class OtterExtensionsFirewallTest
    {
		[Test]
	    public void T()
	    {

            //创建firewall管理类的实例
            INetFwMgr netFwMgr = (INetFwMgr)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));

            INetFwOpenPort objPort = (INetFwOpenPort)Activator.CreateInstance(
                Type.GetTypeFromProgID("HNetCfg.FwOpenPort"));

            objPort.Name = "pokemonx.redis";
            objPort.Port = 6379;
		    var protocol = "TCP";
            if (protocol.ToUpper() == "TCP")
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

            foreach (INetFwOpenPort mPort in netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts)
            {
                Console.WriteLine(mPort);

                if(Equals(mPort.Name,objPort.Name) && Equals(mPort.Port,objPort.Port))
                {
                    exist = true;
                    break;
                }
            }

            if(!exist)
                netFwMgr.LocalPolicy.CurrentProfile.GloballyOpenPorts.Add(objPort);

        }
    }
}
