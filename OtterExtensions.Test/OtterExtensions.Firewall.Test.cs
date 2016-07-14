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
    public class OtterExtensions
    {
		[Test]
	    public void T()
	    {

            var firewallPolicy = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

		   Console.WriteLine(firewallPolicy.Name);
           Console.WriteLine(firewallPolicy.ApplicationName);

        }
    }
}
