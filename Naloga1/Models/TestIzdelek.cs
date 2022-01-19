using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Naloga1.Models
{
    [Serializable]
    public class TestIzdelek
    {
        [XmlElement]
        public string NazivIzdelka { get; set; }
        [XmlElement]
        public string Cena { get; set; }
        [XmlElement]
        public string Zaloga { get; set; }

        public TestIzdelek()
        {

        }

        //public TestIzdelek(string naziv, double vred, string method)
        //{
        //    NazivIzdelka = naziv;
        //    vrednost = vred;
        //    MethodUse = method;
        //}
    }
}
