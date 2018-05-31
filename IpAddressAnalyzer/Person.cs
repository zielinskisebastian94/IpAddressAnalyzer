using System;
using System.Net;
using Newtonsoft.Json;

namespace IpAddressAnalyzer
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class Person
    {
        public Person(string name, string surname, string email, string coutry, string ipAddress)
        {
            Name = name;
            Surname = surname;
            Email = email;
            IpAddress = ipAddress;
            Country = coutry;
        }

        public Person()
        {
                
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }  
        public string IpAddress { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", Name, Surname, Email, Country, IpAddress);
        }
    }


}
