using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.Xsl;
using Naloga1.Models;
using Aspose.Html;
using Aspose.Html.Converters;
using Aspose.Html.Saving;

namespace Naloga1.Methods
{
    public class Method
    {
        public List<Izdelek> sezn { get; set; }
        public List<Izdelek> seznPopust { get; set; }
        public List<Izdelek> seznDobaMinCena { get; set; }
        public List<Izdelek> seznDoc { get; set; }
        public List<Dobavitelj> seznDoba { get; set; }
        public List<TestIzdelek> SerializationList = new List<TestIzdelek>();
        XmlDocument xDoc = new XmlDocument();   

        public List<Izdelek> FillList(int id, string naziv, double cena, int zaloga, int idDob)
        {
             //Dobavitelj d1 = seznDoba.Find(x => x.IdDobavitelj == dobaId);

             Izdelek I = new Izdelek(id, naziv, cena, zaloga, idDob);
             
             sezn.Add(I);

             return sezn;
        }

        public List<TestIzdelek> FillListSerializ(string naziv, double vrednost, string method)
        {
            //Dobavitelj d1 = seznDoba.Find(x => x.IdDobavitelj == dobaId);

            TestIzdelek I = new TestIzdelek();

            SerializationList.Add(I);

            return SerializationList;
        }

        public List<Izdelek> FilterList(int zal, int Iddobav)
        {
            List<Izdelek> Filtrirani = new List<Izdelek>();

            Filtrirani = sezn.Where(x => x.IdDobavitelj == Iddobav && x.Zaloga < zal).ToList();

            return Filtrirani;
        }

        public List<string> DocItem(string nameDoc, string Type)
        {
            try
            {
                if(Type == "Izdelek")
                {
                    XmlSerializer reader = new XmlSerializer(typeof(List<Izdelek>));
                    StreamReader file = new StreamReader(nameDoc);
                    List<Izdelek> overview = (List<Izdelek>)reader.Deserialize(file);
                    file.Close();

                    string ov = string.Empty;

                    List<string> sezStr = new List<string>();

                    foreach(var q in overview)
                    {
                        ov = $"Id izdelka: {q.IdIzdelek} | Naziv izdelka: {q.NazivIzdelka} | Cena izdelka: {q.CenaIzdelka} | Zaloga izdelka: {q.Zaloga} | Dobavitelj: {q.IdDobavitelj}";
                        sezStr.Add(ov);
                    }

                    return sezStr;
                }
                
                else if(Type == "Dobavitelj")
                {
                    XmlSerializer reader = new XmlSerializer(typeof(List<Dobavitelj>));
                    StreamReader file = new StreamReader(nameDoc);
                    List<Dobavitelj> overview = (List<Dobavitelj>)reader.Deserialize(file);
                    file.Close();

                    string ov = string.Empty;
                    List<string> sezStr = new List<string>();

                    foreach(var q in overview)
                    {
                        ov = $"Id dobavitelj: {q.IdDobavitelj} | Naziv dobavitelj: {q.NazivDobavitelj} | Naslov dobavitelj: {q.Naslov} | Davcna stevilka: {q.DavcnaSt} | Kontakt dobavitelja: {q.Kontakt}";
                        sezStr.Add(ov);
                    }

                    return sezStr;
                }
            }
            
            catch(IOException e)
            {
                List<string> documentItems = new List<string>();
                documentItems.Add("Dokument ni mogoče prebrati");
                return documentItems;
            }

            return new List<string>();
        }
        public void SaveItemsSerialization(string nameDoc)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";

            using (XmlWriter xmlWriter = XmlWriter.Create(nameDoc + ".xml", settings))
            {
                xmlWriter.WriteStartDocument();
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Izdelek>));
                serialiser.Serialize(xmlWriter, SerializationList);
            }
        }
        public void SaveItems(string nameDoc)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";

            using (XmlWriter xmlWriter = XmlWriter.Create(nameDoc + ".xml", settings))
            {
                xmlWriter.WriteStartDocument();        
                XmlSerializer serialiser = new XmlSerializer(typeof(List<Izdelek>));
                serialiser.Serialize(xmlWriter, sezn);
            }
        }

        public void SaveDobava(string nameDoc)
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(List<Dobavitelj>));
            TextWriter Filestream = new StreamWriter(nameDoc + ".xml");
            serialiser.Serialize(Filestream, seznDoba);
            Filestream.Close();
        }

        public void SaveFilteredItems(string nameDoc)
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(List<Izdelek>));
            TextWriter Filestream = new StreamWriter(nameDoc + ".xml");
            serialiser.Serialize(Filestream, seznDoc);
            Filestream.Close();
        }

        public List<Izdelek> DiscountPrice(List<Izdelek> sezn, int doba, int ProcentPopusta)
        {
            List<Izdelek> ListToDiscount = new List<Izdelek>();

            ListToDiscount = sezn.Where(x => x.IdDobavitelj == doba).ToList();

            double ValDiscount;

            foreach(var i in ListToDiscount)
            {
                ValDiscount = 0.0;

                ValDiscount = (i.CenaIzdelka * ProcentPopusta) / 100;

                i.CenaIzdelka -= ValDiscount; 
            }

            return ListToDiscount;
        }

        public void SaveDiscount(List<Izdelek> DisItems, string nameDoc)
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(List<Izdelek>));
            TextWriter Filestream = new StreamWriter(nameDoc + ".xml");
            serialiser.Serialize(Filestream, seznPopust);
            Filestream.Close();
        }

        public List<Dobavitelj> FillDobavaList(int idDoba, string naziv, string dav, string kon, string opis, string nasl)
        {
            Dobavitelj d1 = new Dobavitelj(idDoba, naziv, nasl, dav, kon, opis);

            seznDoba.Add(d1);

            return seznDoba;
        }

        public List<Dobavitelj> EditList(int id, string naziv, string dav, string kon, string opis, string naslov)
        {
            var d1 = seznDoba.Find(x => x.IdDobavitelj == id);

            seznDoba.Remove(d1);

            d1.IdDobavitelj = id;
            d1.NazivDobavitelj = naziv;
            d1.DavcnaSt = dav;
            d1.Kontakt = kon;
            d1.Opis = opis;
            d1.Naslov = naslov;

            seznDoba.Add(d1);

            return seznDoba;
        }

        public bool dobaExist(int id)
        {
            var d1 = seznDoba.Find(x => x.IdDobavitelj == id);

            if(d1 != null)
            {
                return true;
            }

            else if(d1 == null)
            {
                return false;
            }

            return true;
        }

        public List<Dobavitelj> ReturnDoba()
        {
            return seznDoba;
        }

        public void ConvertXML(string InDoc, string OutDoc)
        {
            XDocument result = new XDocument(
                      new XElement("table", new XAttribute("border", 5), new XAttribute("style", "margin-left:auto; margin-right:auto; margin-top:100px; text-align:center;"),
                          new XElement("thead", new XElement("tr",
                              new XElement("th", "IdIzelek"),
                              new XElement("th", "Naziv"),
                              new XElement("th", "Cena"),
                              new XElement("th", "Zaloga"),
                              new XElement("th", "IdDobavitelj"),
                                  new XAttribute("style", "background-color:#C7F25D;"))),
                          new XElement("tbody",
                              from artikel in XDocument.Load(InDoc + ".xml").Descendants("Izdelek")
                              select new XElement("tr",
                                 new XElement("td", artikel.Element("IdIzdelek").Value, new XAttribute("style", "background-color:#F73054")),
                                 new XElement("td", artikel.Element("NazivIzdelka").Value),
                                 new XElement("td", artikel.Element("CenaIzdelka").Value),
                                 new XElement("td", artikel.Element("Zaloga").Value),
                                 new XElement("td", artikel.Element("IdDobavitelj").Value)))));

            result.Save(OutDoc + ".htm");
        }

        public void ConvertXMLDescending(string InDoc, string OutDoc)
        {
            XDocument result = new XDocument(
                      new XElement("table", new XAttribute("border", 5), new XAttribute("style", "margin-left:auto; margin-right:auto; margin-top:100px; text-align:center;"),
                          new XElement("thead", new XElement("tr",
                              new XElement("th", "IdIzelek"),
                              new XElement("th", "Naziv"),
                              new XElement("th", "Cena"),
                              new XElement("th", "Zaloga"),
                              new XElement("th", "IdDobavitelj"),
                                 new XAttribute("style", "background-color:#C7F25D;"))),
                          new XElement("tbody",
                              from artikel in XDocument.Load(InDoc + ".xml").Descendants("Izdelek").OrderByDescending(s => s.Element("CenaIzdelka").Value)
                              select new XElement("tr",
                                 new XElement("td", artikel.Element("IdIzdelek").Value, new XAttribute("style", "background-color:#F73054")),
                                 new XElement("td", artikel.Element("NazivIzdelka").Value),
                                 new XElement("td", artikel.Element("CenaIzdelka").Value),
                                 new XElement("td", artikel.Element("Zaloga").Value),
                                 new XElement("td", artikel.Element("IdDobavitelj").Value)))));

            result.Save(OutDoc + ".htm");
        }

        public void ConvertXMLPog(string InDoc, string OutDoc, int pog)
        {
            XDocument result = new XDocument(
              new XElement("table", new XAttribute("border", 5), new XAttribute("style", "margin-left:auto; margin-right:auto; margin-top:100px; text-align:center;"), 
                  new XElement("thead", new XElement("tr",
                      new XElement("th", "IdIzelek"),
                      new XElement("th", "Naziv"),
                      new XElement("th", "Cena"),
                      new XElement("th", "Zaloga"),
                      new XElement("th", "IdDobavitelj"),
                         new XAttribute("style", "background-color:#C7F25D;"))),
                  new XElement("tbody",
                      from artikel in XDocument.Load(InDoc + ".xml").Descendants("Izdelek").Where(s => int.Parse(s.Element("CenaIzdelka").Value) < pog)
                      select new XElement("tr",
                         new XElement("td", artikel.Element("IdIzdelek").Value, new XAttribute("style", "background-color:#F73054")),
                         new XElement("td", artikel.Element("NazivIzdelka").Value),
                         new XElement("td", artikel.Element("CenaIzdelka").Value),
                         new XElement("td", artikel.Element("Zaloga").Value),
                         new XElement("td", artikel.Element("IdDobavitelj").Value)))));

            result.Save(OutDoc + ".htm");
        }

        public void ConvertXMLhtml(string xmlDoc, string htmlDoc)
        {
            XmlDocument oXML = new XmlDocument();

            oXML.Load(xmlDoc + ".xml");

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("xsl.xml");

            xslt.Transform(xmlDoc + ".xml", htmlDoc + ".html");
        }

     
        public void ConvertHtmlPdf(string inDoc, string outDoc)
        {
            string documentPath = Path.Combine(@"C:/Users/timzu/OneDrive/Desktop/Faks/Razvoj Informacijskih Storitev/Naloga1/Naloga1/Naloga1/bin/Debug/net5.0", inDoc + ".html");

            string savePath = Path.Combine(@"C:/Users/timzu/OneDrive/Desktop/Faks/Razvoj Informacijskih Storitev/Naloga1/Naloga1/Naloga1/bin/Debug/net5.0", outDoc + ".pdf");

            using var document = new HTMLDocument(documentPath);
 
            var options = new PdfSaveOptions();

            Converter.ConvertHTML(document, options, savePath);
        }

        public void ConvertDocX(string inDoc, string outDoc)
        {
            string documentPath = Path.Combine(@"C:/Users/timzu/OneDrive/Desktop/Faks/Razvoj Informacijskih Storitev/Naloga1/Naloga1/Naloga1/bin/Debug/net5.0", inDoc + ".html");
            string savePath = Path.Combine(@"C:/Users/timzu/OneDrive/Desktop/Faks/Razvoj Informacijskih Storitev/Naloga1/Naloga1/Naloga1/bin/Debug/net5.0", outDoc + ".docx");

            using var document = new HTMLDocument(documentPath);

            var options = new DocSaveOptions();

            Converter.ConvertHTML(document, options, savePath);
        }

        public void ConvertXMLDocx(string inDoc, string outDoc)
        {
            SautinSoft.UseOffice useOffice = new SautinSoft.UseOffice();

            string inputFile = Path.GetFullPath(inDoc + ".docx");
            string outFile = Path.GetFullPath(outDoc + ".xml");

            int ret = useOffice.InitWord();

            ret = useOffice.ConvertFile(inputFile, outFile, SautinSoft.UseOffice.eDirection.DOCX_to_XML);

            useOffice.CloseWord();
        }
    }
}
