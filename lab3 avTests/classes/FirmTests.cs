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
    public class FirmTests
    {
        public FirmFactory FirmFactory { get; } = new FirmFactory();

        private Contact _contact = new Contact(new ContactType("", ""), "", "", new DateTime(), new DateTime());
        [TestMethod()]
        public void AddSubFirmTest()
        {
            SubFirm subFirm = new SubFirm(new SubFirmType(false, "qev;e"), "qev;e", "QWrgergw'e", "qerlgqer;", "-74346758076", "qefihwdf.nbv");
            Firm firm = FirmFactory.Create("Kazakhstan", "qebe", "Astana", "NurSultan",
                "143585", "efvw;ijbrb", ";wjlefhb;wrtb;", new DateTime(1345, 6, 7),
                "SUPERBOSS", "SUPER SUPERBOSS", "+712845734346");
            SubFirm createdSubFirm = firm.AddSubFirm(subFirm.Type, subFirm.Name, subFirm.BossName, subFirm.OfficialBossName, subFirm.PhoneNumber, subFirm.Email);
            Assert.IsNotNull(createdSubFirm);
        }

        [TestMethod()]
        public void RenameFieldTest()
        {
            Firm firm = FirmFactory.Create("qekfle", "qebe", "Astana", "NurSultan",
                "143585", "efvw;ijbrb", ";wjlefhb;wrtb;", new DateTime(1345, 6, 7),
                "SUPERBOSS", "SUPER SUPERBOSS", "+712845734346");
            string newFieldNameTest = "adkfhf";
            string value = "value";
            firm.SetField(FirmFactory.FieldName1, value);
            firm.RenameField(FirmFactory.FieldName1, newFieldNameTest);

            string newValue = firm.GetField(newFieldNameTest);
            Assert.AreEqual(newValue, value);
        }
        [TestMethod()]
        public void AddContactTestMain()
        {
            Firm firm = FirmFactory.Create("qekfle", "qebe", "Astana", "NurSultan",
                "143585", "efvw;ijbrb", ";wjlefhb;wrtb;", new DateTime(1345, 6, 7),
                "SUPERBOSS", "SUPER SUPERBOSS", "+712845734346");
           
            Contact addedContact = firm.AddContact(_contact);
            Assert.AreNotSame(_contact, addedContact);
            Assert.IsTrue(firm.IsContactExists(_contact));
            Assert.IsTrue(firm.GetContacts(_contact).Count != 0);

            Assert.IsTrue(addedContact == _contact);

        }
        [TestMethod()]
        public void AddContactTest()
        {
            Firm firm = FirmFactory.Create("qekfle", "qebe", "Astana", "NurSultan",
                "143585", "efvw;ijbrb", ";wjlefhb;wrtb;", new DateTime(1345, 6, 7),
                "SUPERBOSS", "SUPER SUPERBOSS", "+712845734346");
            Contact contact = new Contact(new ContactType("name", "note"), "description", "information", new DateTime(1990, 2, 2), new DateTime(2000, 2, 2));
            Contact addedContact = firm.AddContact(contact);
            Assert.IsNotNull(addedContact);
            Assert.AreNotSame(contact, addedContact);
            Assert.IsTrue(firm.Main.IsContactExists(contact));

        }
        [TestMethod()]
        public void AddContactToSubFirmTest()
        {
            Firm firm = FirmFactory.Create("qekfle", "qebe", "Astana", "NurSultan",
                "143585", "efvw;ijbrb", ";wjlefhb;wrtb;", new DateTime(1345, 6, 7),
                "SUPERBOSS", "SUPER SUPERBOSS", "+712845734346");
            SubFirm subFirm = new SubFirm(new SubFirmType(false, "name"), "name", "bossName", "officialBossName", "phoneNumber", "email");

            Contact contact = new Contact(new ContactType("name", "note"), "description", "information", new DateTime(1990, 2, 2), new DateTime(2000, 2, 2));

            firm.AddSubFirm(subFirm.Type, subFirm.Name, subFirm.BossName, subFirm.OfficialBossName, subFirm.PhoneNumber, subFirm.Email);
            Contact addedContact = firm.AddContactToSubFirm(subFirm.Type, contact);
            Assert.IsNotNull(addedContact);
            Assert.AreNotSame(contact, addedContact);
            SubFirm addedSubFirm = firm.GetSubFirm(subFirm.Type);
            Assert.IsNotNull(addedSubFirm);
            Assert.IsTrue(addedSubFirm.IsContactExists(contact));
            Contact gotContact = firm.GetSubFirmContact(subFirm.Type, contact)[0];
            Assert.IsNotNull(gotContact);
            
            Assert.IsNotNull(gotContact);
            Assert.AreNotSame(contact, gotContact);
        }


        //----------------------------

        //Создаем 2 подразделения разного типа, добавляем им контакт, возвращаете от фирмы все контакты по контакту их должно оказаться 2 и сравниваем с оригиналом
        [TestMethod()]
        public void Add2ContactsToSubFirmsTest()
        {
            SubFirm subFirm1 = new SubFirm(new SubFirmType(false, "type1"), "1", "1", "1", "1", "1");
            SubFirm subFirm2 = new SubFirm(new SubFirmType(false, "type2"), "2", "2", "2", "2", "2");

            Contact contact = new Contact(new ContactType("1", "1"), "1", "1", new DateTime(2020, 1, 1), new DateTime(2020, 2, 2));

            Firm firm = FirmFactory.Create("Kazakhstan", "qebe", "Astana", "NurSultan",
                "143585", "efvw;ijbrb", ";wjlefhb;wrtb;", new DateTime(1345, 6, 7),
                "SUPERBOSS", "SUPER SUPERBOSS", "+712845734346");

            firm.AddSubFirm(subFirm1.Type, subFirm1.Name,
                            subFirm1.BossName, subFirm1.OfficialBossName, subFirm1.PhoneNumber, subFirm1.Email);

            firm.AddSubFirm(subFirm2.Type, subFirm2.Name,
                subFirm2.BossName, subFirm2.OfficialBossName, subFirm2.PhoneNumber, subFirm2.Email);

            firm.AddContactToSubFirm(subFirm1.Type, contact);
            firm.AddContactToSubFirm(subFirm2.Type, contact);

            Assert.IsTrue(firm.ContactsAmount == 2);

            var contacts = firm.GetContacts(contact);
            foreach (var item in contacts)
            {
                Assert.AreEqual(item, contact);
                Assert.AreNotSame(item, contact);
            }
        }

        [TestMethod()]
        public void Add2DifferentContactsToSubFirmsTest()
        {
            SubFirm subFirm1 = new SubFirm(new SubFirmType(false, "type1"), "1", "1", "1", "1", "1");
            SubFirm subFirm2 = new SubFirm(new SubFirmType(false, "type2"), "2", "2", "2", "2", "2");

            Contact contact1 = new Contact(new ContactType("1", "1"), "1", "1", new DateTime(2020, 1, 1), new DateTime(2020, 2, 2));
            Contact contact2 = new Contact(new ContactType("2", "2"), "2", "2", new DateTime(2020, 2, 2), new DateTime(2020, 3, 3));

            Firm firm = FirmFactory.Create("Kazakhstan", "qebe", "Astana", "NurSultan",
                "143585", "efvw;ijbrb", ";wjlefhb;wrtb;", new DateTime(1345, 6, 7),
                "SUPERBOSS", "SUPER SUPERBOSS", "+712845734346");

            firm.AddSubFirm(subFirm1.Type, subFirm1.Name,
                            subFirm1.BossName, subFirm1.OfficialBossName, subFirm1.PhoneNumber, subFirm1.Email);

            firm.AddSubFirm(subFirm2.Type, subFirm2.Name,
                subFirm2.BossName, subFirm2.OfficialBossName, subFirm2.PhoneNumber, subFirm2.Email);

            firm.AddContactToSubFirm(subFirm1.Type, contact1);
            firm.AddContactToSubFirm(subFirm2.Type, contact2);

            Assert.IsTrue(firm.ContactsAmount == 2);

            List<Contact> gotContactList1 = firm.GetContacts(contact1);
            Assert.IsTrue(gotContactList1.Count == 1);
            Contact gotContact1 = gotContactList1[0];
            Assert.IsNotNull(gotContact1);

            List<Contact> gotContactList2 = firm.GetContacts(contact2);
            Assert.IsTrue(gotContactList2.Count == 1);
            Contact gotContact2 = gotContactList2[0];
            Assert.IsNotNull(gotContact2);

            Assert.AreEqual(gotContact1, contact1);
            Assert.AreNotSame(gotContact1, contact1);

            Assert.AreEqual(gotContact2, contact2);
            Assert.AreNotSame(gotContact2, contact2);

        }
        //----------------------------

        //У фирмы запрашиваем подразделение по типу, возвращается подразделение и сравниваем его на равенство
        [TestMethod()]
        public void GetSubfirmByTypeTest()
        {
            SubFirm subFirm = new SubFirm(new SubFirmType(false, "qev;e"), "qev;e", "QWrgergw'e", "qerlgqer;", "-74346758076", "qefihwdf.nbv");
            Firm firm = FirmFactory.Create("Kazakhstan", "qebe", "Astana", "NurSultan",
                "143585", "efvw;ijbrb", ";wjlefhb;wrtb;", new DateTime(1345, 6, 7),
                "SUPERBOSS", "SUPER SUPERBOSS", "+712845734346");

            SubFirm addedSubFirm = firm.AddSubFirm(subFirm.Type, subFirm.Name,
                            subFirm.BossName, subFirm.OfficialBossName, subFirm.PhoneNumber, subFirm.Email);

            Assert.IsNotNull(addedSubFirm);
            Assert.AreNotSame(addedSubFirm, subFirm);

            Assert.AreEqual(subFirm.Name, addedSubFirm.Name);
            Assert.AreEqual(subFirm.BossName, addedSubFirm.BossName);
            Assert.AreEqual(subFirm.OfficialBossName, addedSubFirm.OfficialBossName);
            Assert.AreEqual(subFirm.PhoneNumber, addedSubFirm.PhoneNumber);
            Assert.AreEqual(subFirm.Email, addedSubFirm.Email);
        }

        //Проверка увеличения кол-ва подфирм
        [TestMethod()]
        public void ChangeSubFirmAmountTest()
        {
            SubFirm subFirm = new SubFirm(new SubFirmType(false, "qev;e"), "qev;e", "QWrgergw'e", "qerlgqer;", "-74346758076", "qefihwdf.nbv");
            Firm firm = FirmFactory.Create("Kazakhstan", "qebe", "Astana", "NurSultan",
                "143585", "efvw;ijbrb", ";wjlefhb;wrtb;", new DateTime(1345, 6, 7),
                "SUPERBOSS", "SUPER SUPERBOSS", "+712845734346");

            Assert.IsTrue(firm.SubFirmsAmount == 1);

            SubFirm addedSubFirm = firm.AddSubFirm(subFirm.Type, subFirm.Name,
                            subFirm.BossName, subFirm.OfficialBossName, subFirm.PhoneNumber, subFirm.Email);

            Assert.IsTrue(firm.SubFirmsAmount == 2);

            Assert.AreEqual(subFirm.Name, addedSubFirm.Name);
            Assert.AreEqual(subFirm.BossName, addedSubFirm.BossName);
            Assert.AreEqual(subFirm.OfficialBossName, addedSubFirm.OfficialBossName);
            Assert.AreEqual(subFirm.PhoneNumber, addedSubFirm.PhoneNumber);
            Assert.AreEqual(subFirm.Email, addedSubFirm.Email);

            SubFirm addedSubFirm2 = firm.AddSubFirm(subFirm.Type, subFirm.Name,
                subFirm.BossName, subFirm.OfficialBossName, subFirm.PhoneNumber, subFirm.Email);

            Assert.IsTrue(firm.SubFirmsAmount == 2);
            Assert.AreSame(addedSubFirm, addedSubFirm2);
        }

        [TestMethod()]
        public void GetDifferentSubFirmByTypeTest()
        {
            SubFirm subFirm1 = new SubFirm(new SubFirmType(false, "type1"), "1", "1", "1", "1", "1");
            SubFirm subFirm2 = new SubFirm(new SubFirmType(false, "type2"), "2", "2", "2", "2", "2");

            Firm firm = FirmFactory.Create("Kazakhstan", "qebe", "Astana", "NurSultan",
                "143585", "efvw;ijbrb", ";wjlefhb;wrtb;", new DateTime(1345, 6, 7),
                "SUPERBOSS", "SUPER SUPERBOSS", "+712845734346");

            Assert.IsTrue(firm.SubFirmsAmount == 1);

            SubFirm addedSubFirm1 = firm.AddSubFirm(subFirm1.Type, subFirm1.Name,
                            subFirm1.BossName, subFirm1.OfficialBossName, subFirm1.PhoneNumber, subFirm1.Email);

            Assert.IsTrue(firm.SubFirmsAmount == 2);

            SubFirm addedSubFirm2 = firm.AddSubFirm(subFirm2.Type, subFirm2.Name,
                subFirm2.BossName, subFirm2.OfficialBossName, subFirm2.PhoneNumber, subFirm2.Email);

            Assert.IsTrue(firm.SubFirmsAmount == 3);
            Assert.AreNotSame(addedSubFirm1, addedSubFirm2);

            SubFirm gotSubFirm1 = firm.GetSubFirm(subFirm1.Type);
            SubFirm gotSubFirm2 = firm.GetSubFirm(subFirm2.Type);

            Assert.AreEqual(subFirm1.Name, gotSubFirm1.Name);
            Assert.AreEqual(subFirm1.BossName, gotSubFirm1.BossName);
            Assert.AreEqual(subFirm1.OfficialBossName, gotSubFirm1.OfficialBossName);
            Assert.AreEqual(subFirm1.PhoneNumber, gotSubFirm1.PhoneNumber);
            Assert.AreEqual(subFirm1.Email, gotSubFirm1.Email);

            Assert.AreEqual(subFirm2.Name, gotSubFirm2.Name);
            Assert.AreEqual(subFirm2.BossName, gotSubFirm2.BossName);
            Assert.AreEqual(subFirm2.OfficialBossName, gotSubFirm2.OfficialBossName);
            Assert.AreEqual(subFirm2.PhoneNumber, gotSubFirm2.PhoneNumber);
            Assert.AreEqual(subFirm2.Email, gotSubFirm2.Email);

            Assert.AreNotSame(subFirm1, gotSubFirm1);
            Assert.AreNotSame(subFirm2, gotSubFirm2);
        }
    }
    //Создаете фирму, добавляете в нее 2 подразделения разного типа и по типу получаем подразделение
}