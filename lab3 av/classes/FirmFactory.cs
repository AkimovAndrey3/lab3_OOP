﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace lab3_av
{
    public class FirmFactory
    {

        public readonly string FieldName1 = "field1";
        public readonly string FieldName2 = "field2";
        public readonly string FieldName3 = "field3";
        public readonly string FieldName4 = "field4";
        public readonly string FieldName5 = "field5";
        //public Dictionary<string, string> UserFields { get; set; } = new Dictionary<string, string>();
        private const string MainFirmName = "Main Firm";

        public FirmFactory()
        {
        }

        public Firm Create(string country, string region,
            string town, string street, string postIndex, string email,
            string websiteUrl, DateTime enterDate,
            string bossName, string officialBossName, string phoneNumber)
        {
            Firm firm = new Firm(MainFirmName, country, region, town, street,
                postIndex, email, websiteUrl, enterDate, bossName,
                officialBossName, phoneNumber);

            FillUserFields(firm);
            return firm;
        }

        //Фирмы можно было создавать не только через статическое обращение FirmFactory, но и через любой объект

        public void FillUserFields(Firm firm)
        {
            firm.AddField(FieldName1);
            firm.AddField(FieldName2);
            firm.AddField(FieldName3);
            firm.AddField(FieldName4);
            firm.AddField(FieldName5);
        }
    }
}
