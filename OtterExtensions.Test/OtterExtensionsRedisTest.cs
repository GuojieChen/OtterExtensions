using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OtterExtensions.Test
{
	[TestFixture]
    public class OtterExtensionsRedisTest
    {
		[Test]
		public void WriteValue()
	    {
            var lines = File.ReadLines($"{System.AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}files{Path.DirectorySeparatorChar}redis.windows.conf");
		    var key = "port";
            string value = string.Empty;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;
                var str = line.Trim();
                if (string.IsNullOrEmpty(str))
                    continue;
                if (line.Trim().TrimStart('#').StartsWith(key))
                {
                    var keylen = key.Length;

                    value = line.Substring(keylen+1, line.Length - keylen-1).Trim();
					Console.WriteLine(value);
                    break;
                }
            }

			Console.WriteLine("over");
        }
    }
}
