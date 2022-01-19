using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Naloga1.Models
{
    [Serializable]
    public class Dobavitelj
    {
        [XmlAttribute]
        int idDoba;
        [XmlAttribute]
        string naziv;
        [XmlAttribute]
        string naslov;
        [XmlAttribute]
        string davcna;
        [XmlAttribute]
        string kontakt;
        [XmlAttribute]
        string opis;

        public int IdDobavitelj { get { return idDoba; } set { idDoba = value; } }
        public string NazivDobavitelj { get { return naziv; } set { naziv = value; } }
        public string Naslov { get { return naslov; } set { naslov = value; } }
        public string DavcnaSt { get { return davcna; } set { davcna = value; } }
        public string Kontakt { get { return kontakt; } set { kontakt = value; } }
        public string Opis { get { return opis; } set { opis = value; } }

        public Dobavitelj() { }

        public Dobavitelj(int id, string naz, string nasl, string davc, string kont, string op)
        {
            IdDobavitelj = id;
            NazivDobavitelj = naz;
            Naslov = nasl;
            DavcnaSt = davc;
            Kontakt = kont;
            Opis = op;
        }

        public override string ToString()
        {
            return $"Id: {IdDobavitelj} | Naziv: {NazivDobavitelj} | Naslov: {Naslov} | Davcna številka: {DavcnaSt} | Kontakt: {Kontakt} | Opis: {Opis}";
        }
    }
}
