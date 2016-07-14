using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace OtterExtensions.Test
{
    [TestFixture]
    public class OtterExtensionsSystemTest
    {
        [Test]
        public void DriveInfo()
        {
            foreach (var drive in System.IO.DriveInfo.GetDrives())
            {
                Console.WriteLine(drive.AvailableFreeSpace);
            }
        }
    }
}
