using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks.Dataflow;
using System.Xml.Linq;
using Formatting = Newtonsoft.Json.Formatting;

namespace IpAddressAnalyzer
{
    public class DataSerializer
    {
        public DataSerializer(string xmlPath, string jsonPAth)
        {
            Directory.CreateDirectory(@"c:\analyzer");
            XmlPath = xmlPath;
            JsonPAth = jsonPAth;
        }

        public string XmlPath { get; set; }
        public string JsonPAth { get; set; }
        public ICollection<Person> People { get; set; }

        public async Task SerializeToJson(ISourceBlock<Person> source)
        {
            using (FileStream fs = File.Create(JsonPAth))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;
                jw.WriteStartObject();
                jw.WritePropertyName("People");
                jw.WriteStartArray();
                string temp = string.Empty;
                while (await source.OutputAvailableAsync())
                {
                    var person = source.Receive();
                    temp = JsonConvert.SerializeObject(person);
                    jw.WriteValue(temp);

                }
                jw.WriteEndArray();
                jw.WriteEndObject();

                //serializer.Serialize(jw, People);
            }
        }
        public async Task SerializeToXml(ISourceBlock<Person> source)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlSerializer xml = new XmlSerializer(typeof(Person));
            using (FileStream fs = File.Create(XmlPath))
            using (StreamWriter sw = new StreamWriter(fs))
            using (XmlWriter xw = XmlTextWriter.Create(sw, settings))
            {
                xw.WriteStartDocument();
                xw.WriteStartElement("People");
                string temp = string.Empty;
                while (await source.OutputAvailableAsync())
                {
                    var person = source.Receive();

                    xw.WriteStartElement("Person");
                    xw.WriteElementString("Name", person.Name);
                    xw.WriteElementString("Surname", person.Surname);
                    xw.WriteElementString("Countrt", person.Country);
                    xw.WriteElementString("Email", person.Email);
                    xw.WriteElementString("IpAddress", person.IpAddress);
                    xw.WriteEndElement();

                    

                }
                xw.WriteEndElement();
                xw.WriteEndDocument();


            }
        }
    }
}