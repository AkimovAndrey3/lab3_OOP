using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3_av
{

    public class ContactType
    {
        public string Name { get; private set; }
        public string Note { get; private set; }

        public ContactType(string name, string note)
        {
            Name = name;
            Note = note;
        }
    }
    
}
