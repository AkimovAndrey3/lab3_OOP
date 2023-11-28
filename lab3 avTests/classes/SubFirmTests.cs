using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab3_av;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_av.Tests
{
    [TestClass()]
    public class SubFirmTests
    {
        [TestMethod()]
        public void IsEqualTypeTest()
        {
            SubFirmType newSubFirmType1 = new SubFirmType(false, "commerical");
            SubFirmType newSubFirmType2 = new SubFirmType(false, "commerical");
            SubFirm newSubFirm = new SubFirm(newSubFirmType1, "1", "2", "3", "4", "%");
            bool isEquals = newSubFirm.IsEqualType(newSubFirmType2);

            Assert.IsTrue(isEquals);
        }
    }
}