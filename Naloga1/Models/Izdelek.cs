using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Naloga1.Models
{
    [Serializable]
    public class Izdelek
    {
        [XmlElement]
        public int IdIzdelek { get; set; }
        [XmlElement]
        public string NazivIzdelka { get; set; }
        [XmlElement]
        public double CenaIzdelka { get; set; }
        [XmlElement]
        public int Zaloga { get; set; }
        [XmlElement]
        public int IdDobavitelj { get; set; }

        public Izdelek()
        {

        }

        public Izdelek(int id, string naziv, double cena, int zaloga, int idDob)
        {
            this.IdIzdelek = id;
            this.NazivIzdelka = naziv;
            this.CenaIzdelka = cena;
            this.Zaloga = zaloga;
            this.IdDobavitelj = idDob;
        }

        public override string ToString()
        {
            return $"Naziv izdelka: {NazivIzdelka} | Cena izdelka: {CenaIzdelka} | Zaloga: {Zaloga} | Id dobavitelj: {IdDobavitelj}";
        }
    }
}
