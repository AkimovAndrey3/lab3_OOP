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
    public class FirmFactoryTests
    {
        public FirmFactory FirmFactoryV2 { get; } = new FirmFactory();

        [TestMethod()]
        public void CreateTest()
        {

            const string name = "name";
            const string country = "country";
            const string region = "region";
            const string town = "town";
            const string street = "street";
            const string postIndex = "postIndex";
            const string email = "email";
            const string websiteUrl = "websiteUrl";
            DateTime enterDate = new DateTime(2004, 4, 7);
            const string bossName = "bossName";
            const string officialBossName = "officialBossName";
            const string phoneNumber = "phoneNumber";

            Firm createdFirm = FirmFactoryV2.Create(country, region, town, street, postIndex, email, websiteUrl,
                enterDate, bossName, officialBossName, phoneNumber);


            Assert.IsTrue(createdFirm.Country == country);
            Assert.IsTrue(createdFirm.Region == region);
            Assert.IsTrue(createdFirm.Town == town);
            Assert.IsTrue(createdFirm.Street == street);
            Assert.IsTrue(createdFirm.PostIndex == postIndex);
            Assert.IsTrue(createdFirm.Email == email);
            Assert.IsTrue(createdFirm.WebsiteUrl == websiteUrl);
            Assert.IsTrue(createdFirm.EnterDate == enterDate);
            Assert.IsTrue(createdFirm.Main.BossName == bossName);
            Assert.IsTrue(createdFirm.Main.OfficialBossName == officialBossName);
            Assert.IsTrue(createdFirm.Main.PhoneNumber == phoneNumber);

            string fieldValue = null;
            fieldValue = createdFirm.GetField(FirmFactoryV2.FieldName1);
            Assert.IsNotNull(fieldValue);
            fieldValue = createdFirm.GetField(FirmFactoryV2.FieldName2);
            Assert.IsNotNull(fieldValue);
            fieldValue = createdFirm.GetField(FirmFactoryV2.FieldName3);
            Assert.IsNotNull(fieldValue);
            fieldValue = createdFirm.GetField(FirmFactoryV2.FieldName4);
            Assert.IsNotNull(fieldValue);   
            fieldValue = createdFirm.GetField(FirmFactoryV2.FieldName5);
            Assert.IsNotNull(fieldValue);

            Assert.IsNotNull(createdFirm.Main);
            Assert.IsTrue(createdFirm.Main.Type.IsMain);
        }

        //Создаем 2 фирмы у одной из них изменяем значение польз полей, проверяем, что они оказались не равны
        [TestMethod()]
        public void CreateTest_2Firms()
        {
            const string name = "name";
            const string country = "country";
            const string region = "region";
            const string town = "town";
            const string street = "street";
            const string postIndex = "postIndex";
            const string email = "email";
            const string websiteUrl = "websiteUrl";
            DateTime enterDate = new DateTime(2004, 4, 7);
            const string bossName = "bossName";
            const string officialBossName = "officialBossName";
            const string phoneNumber = "phoneNumber";

            Firm firm1 = FirmFactoryV2.Create(country, region, town, street, postIndex, email, websiteUrl,
                enterDate, bossName, officialBossName, phoneNumber);

            Firm firm2 = FirmFactoryV2.Create(country, region, town, street, postIndex, email, websiteUrl,
                enterDate, bossName, officialBossName, phoneNumber);

            const string value1 = "value1";
            const string value2 = "value2";

            foreach (var field in FirmFactoryV2.UserFields)
            {
                firm1.SetField(field, value1);
                firm2.SetField(field, value1);    
            }

            foreach (var field in FirmFactoryV2.UserFields)
            {
                Assert.IsTrue(firm1.GetField(field)
                == firm2.GetField(field));
            }
            foreach (var field in FirmFactoryV2.UserFields)
            {
                firm2.SetField(field, value2);
            }

            foreach (var field in FirmFactoryV2.UserFields)
            {
                Assert.IsFalse(firm1.GetField(field)
                == firm2.GetField(field));
            }
        }

        //Пытаемся добавить в фирму шесте поле, должно появиться исключение
        [TestMethod()]
        public void AddExtraUserFieldTest()
        {
            const string name = "name";
            const string country = "country";
            const string region = "region";
            const string town = "town";
            const string street = "street";
            const string postIndex = "postIndex";
            const string email = "email";
            const string websiteUrl = "websiteUrl";
            DateTime enterDate = new DateTime(2004, 4, 7);
            const string bossName = "bossName";
            const string officialBossName = "officialBossName";
            const string phoneNumber = "phoneNumber";

            Firm firm1 = FirmFactoryV2.Create(country, region, town, street, postIndex, email, websiteUrl,
                enterDate, bossName, officialBossName, phoneNumber);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                firm1.AddField("FieldName6");
            });
        }
    }
}

