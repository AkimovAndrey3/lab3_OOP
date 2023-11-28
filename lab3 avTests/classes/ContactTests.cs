using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab3_av;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Configuration;

namespace lab3_av.Tests
{
    [TestClass()]
    public class ContactTests
    {
        [TestMethod()]
        public void ContactTest()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                Contact contact = new Contact(new ContactType("name", "note"), "description", "information", new DateTime(2300, 2, 2), new DateTime(2200, 2, 2));
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {     
                Contact contact = new Contact(new ContactType("name", "note"), "description", "information", new DateTime(1800, 2, 8), new DateTime(2100, 11, 1));
                contact.EndDate = new DateTime(1700, 5, 8);
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                Contact contact = new Contact(new ContactType("name", "note"), "description", "information", new DateTime(1900, 11, 9), new DateTime(1500, 4, 17));
                contact.BeginDate = new DateTime(1600, 6, 9);
            });
        }

    }
}
