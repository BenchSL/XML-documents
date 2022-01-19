using System;
using Naloga1.Models;
using Naloga1.Methods;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace Naloga1
{
    class Program
    {
       
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity != XmlSeverityType.Warning)
            {
                Console.WriteLine("\tValidation error: " + args.Message);
            }             
            else if(sender == null)
            {
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            }
            else
            {
                Console.WriteLine("Document has been validated!");
            }
        }

        static void Main(string[] args)
        {
            string Validation = "";

            List<Izdelek> Izdelki = new List<Izdelek>();

            List<Dobavitelj> Dobavitelji = new List<Dobavitelj>();

            List<TestIzdelek> IzdelkiSeriaList = new List<TestIzdelek>();

            Method m = new Method();

            Console.WriteLine("Zaprite program z 'xx'");

            Console.WriteLine();

            string line;

            while ((line = Console.ReadLine()) != "xx")
            {
                Console.WriteLine("Izberite kaj želite narediti:");
                Console.WriteLine();
                Console.WriteLine("Shranjevanje izdelkov: 1");
                Console.WriteLine("Specifične izdelke dobavitelja: 2");
                Console.WriteLine("Znižanje cene za izbran odstotek: 3");
                Console.WriteLine("Izpis vseh artiklov: 4");
                Console.WriteLine("Izpis podatkov iz dokumenta: 5");
                Console.WriteLine("Dodajanje dobavitelja: 6");
                Console.WriteLine("Urejanje dobaviteljev: 7");
                Console.WriteLine("Izpis vseh dobaviteljev: 8");
                Console.WriteLine("Validacija XML z XSD: 9");
                Console.WriteLine("Izpis z uporabo XPATH: 10");
                Console.WriteLine("XML to HTML converter: 11");
                Console.WriteLine("XML to PDF converter: 12");
                Console.WriteLine("XML to DOCX converter: 13");
                Console.WriteLine("Serializacija in deserializacija xml dokumentov: 14");

                int opt = int.Parse(Console.ReadLine());

                if (opt == 1)
                {
                    Console.WriteLine("Za prenehanje vnašanja pritisnite 0");
                    Console.WriteLine("Za nadeljevanje vnašanja pritisnite 2");

                    int stop = 1;

                    for (int i = 0; stop != 0; i++)
                    {
                        m.sezn = Izdelki;

                        Console.WriteLine("Id izdelka: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.WriteLine("Ime izdelka: ");
                        string naz = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Cena izdelka: ");
                        double cena = double.Parse(Console.ReadLine());

                        Console.WriteLine("Zaloga: ");
                        int zal = int.Parse(Console.ReadLine());

                        Console.WriteLine("Dobavitelj ID: ");
                        int doba = int.Parse(Console.ReadLine());

                        Izdelki = m.FillList(id, naz, cena, zal, doba);

                        Console.WriteLine();

                        Console.WriteLine("Pregled izdelkov: ");

                        foreach (var q in m.sezn)
                        {
                            Console.WriteLine(q);
                        }



                        Console.WriteLine("Želite shraniti izdelke? 1 - DA | 2 - NE");
                        int save = int.Parse(Console.ReadLine());

                        //Console.WriteLine("Želite validirati xml dokument? 1 - DA | 2 - NE");
                        //int validation = int.Parse(Console.ReadLine());

                        string doc = string.Empty;

                        if (save == 1)
                        {
                            Console.WriteLine("Shranjevanje izdelkov v tekstovni dokument");
                            Console.WriteLine("Poimenovanje dokumenta:");
                            doc = Convert.ToString(Console.ReadLine());

                            m.SaveItems(doc);


                        }

                        Console.WriteLine("Želite nadeljevati?");
                        stop = int.Parse(Console.ReadLine());

                        //if (validation == 1) //Uporabnik želi validacijo dokumenta
                        //{
                        //    //XmlReaderSettings ArtikliSettings = new XmlReaderSettings();               
                        //    //ArtikliSettings.ValidationType = ValidationType.Schema;
                        //    //ArtikliSettings.Schemas.Add("http://www.contoso.com/Artikli", "XmlSchema.xsd");
                        //    //ArtikliSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                        //    //ArtikliSettings.ValidationEventHandler += new ValidationEventHandler(ArtikliValidationEventHandler);

                        //    //XmlReader Artikli = XmlReader.Create(doc + ".xml", ArtikliSettings);

                        //    //while (Artikli.Read()) { }

                        //    XmlReaderSettings ProductSettings = new XmlReaderSettings();
                        //    ProductSettings.Schemas.Add("http://www.contoso.com/Artikli", "XmlSchema.xsd");
                        //    ProductSettings.ValidationType = ValidationType.Schema;
                        //    ProductSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                        //    ProductSettings.ValidationEventHandler += new ValidationEventHandler(ArtikliValidationEventHandler);
                        //    XmlReader Products = XmlReader.Create(doc + ".xml", ProductSettings);
                        //    while (Products.Read()) { }

                        //    //XmlSchemaSet schema = new XmlSchemaSet();
                        //    //schema.Add("http://www.contoso.com/Artikli", "XmlSchema.xsd");
                        //    //XmlReader Artikli = XmlReader.Create(doc + ".xml");
                        //    //XDocument dock = XDocument.Load(Artikli);
                        //    //dock.Validate(schema, ArtikliValidationEventHandler);

                        //    // Check whether the document is valid or invalid.
                        //    Console.WriteLine("Želite nadeljevati?");
                        //    stop = int.Parse(Console.ReadLine());
                        //}
                    }
                }

                else if (opt == 2)
                {
                    Console.WriteLine("Vpišite id dobavitelja:");
                    int doba = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Console.WriteLine("Vpišite zalogo:");
                    int z = int.Parse(Console.ReadLine());
                    Console.WriteLine();

                    int stop = 1;

                    List<Izdelek> Filter = new List<Izdelek>();

                    Filter = m.FilterList(z, doba);

                    foreach (var a in Filter)
                    {
                        Console.WriteLine(a);
                    }

                    m.seznDobaMinCena = Filter;

                    Console.WriteLine("Želite shraniti filtrirane izdelke? 1 - DA | 2 - NE");
                    int save = int.Parse(Console.ReadLine());

                    if (save == 1)
                    {
                        Console.WriteLine("Shranjevanje izdelkov v tekstovni dokument");
                        Console.WriteLine("Poimenovanje dokumenta:");
                        string doc = Convert.ToString(Console.ReadLine());

                        m.SaveFilteredItems(doc);
                    }
                }

                else if (opt == 3)
                {
                    Console.WriteLine("Vpišite id dobavitelja:");
                    int doba = int.Parse(Console.ReadLine());
                    Console.WriteLine("Vpišite odstotek popusta: ");
                    int odst = int.Parse(Console.ReadLine());

                    List<Izdelek> DiscountedItems = new List<Izdelek>();

                    DiscountedItems = m.DiscountPrice(m.sezn, doba, odst);

                    Console.WriteLine("Pregled znižanih izdelkov: ");
                    foreach (var i in DiscountedItems)
                    {
                        Console.WriteLine(i);
                    }

                    //Dodaj shranjevanje znižanih izdelkov
                    Console.WriteLine();
                    Console.WriteLine("Želite shraniti dokument? DA - 1 | NE - 2");
                    int save = int.Parse(Console.ReadLine());

                    if (save == 1)
                    {
                        Console.WriteLine("Vpišite ime dokumenta: ");
                        string nameDoc = Convert.ToString(Console.ReadLine());
                        m.SaveDiscount(DiscountedItems, nameDoc);
                    }
                }

                else if (opt == 4)
                {
                    foreach (var q in m.sezn)
                    {
                        Console.WriteLine(q);
                    }
                }

                else if (opt == 5) //Izpis iz doc
                {
                    Console.WriteLine("Ime dokumenta: ");
                    string NameDoc = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Vpišite tip podatkovnega objekta: ");
                    string TypeName = Convert.ToString(Console.ReadLine());

                    List<string> DocItems = m.DocItem(NameDoc, TypeName);

                    string AllStr = string.Join(Environment.NewLine, DocItems);

                    Console.WriteLine(AllStr);
                }

                else if (opt == 6)
                {
                    Console.WriteLine("Dodajanje dobaviteljev: ");
                    Console.WriteLine();

                    Console.WriteLine("Za prenehanje vnašanja pritisnite 0");
                    Console.WriteLine("Za nadeljevanje vnašanja pritisnite 2");


                    int stop = 1;

                    for (int i = 0; stop != 0; i++)
                    {
                        m.seznDoba = Dobavitelji;

                        Console.WriteLine("Id dobavitelja:");
                        int idDoba = int.Parse(Console.ReadLine());
                        Console.WriteLine("Naziv dobavitelja: ");
                        string NazivDoba = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Naslov dobavitelja: ");
                        string NaslovDoba = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Davcna stevilka dobavitelja: ");
                        string davcnaSt = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Kontakt dobavitelja: ");
                        string KontDoba = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Opis dobavitelja: ");
                        string OpisDoba = Convert.ToString(Console.ReadLine());

                        Dobavitelji = m.FillDobavaList(idDoba, NazivDoba, davcnaSt, KontDoba, OpisDoba, NaslovDoba);

                        Console.WriteLine();

                        Console.WriteLine("Pregled dobaviteljev: ");

                        foreach (var d in m.seznDoba)
                        {
                            Console.WriteLine(d);
                        }

                        Console.WriteLine();
                        Console.WriteLine("Želite nadeljevati?");
                        stop = int.Parse(Console.ReadLine());

                        Console.WriteLine("Želite shraniti dokument? DA - 1 | NE - 2");
                        int save = int.Parse(Console.ReadLine());

                        if (save == 1)
                        {
                            Console.WriteLine("Vpišite ime dokumenta: ");
                            string nameDoc = Convert.ToString(Console.ReadLine());
                            m.SaveDobava(nameDoc);
                        }
                    }
                }

                else if (opt == 7)
                {
                    Console.WriteLine("Vpišite ID dobavitelja: ");
                    int idDoba = int.Parse(Console.ReadLine());
                    bool t1 = m.dobaExist(idDoba);

                    if (t1 == true) //dobavitelj obstaja
                    {
                        Console.WriteLine("Vpišite naziv: ");
                        string nazivD = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Vpišite davčno št: ");
                        string dav = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Vpišite naslov dobavitelja: ");
                        string naslov = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Vpišite kontakt dobavitelja: ");
                        string kon = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Vpišite opis dobavitelja: ");
                        string opis = Convert.ToString(Console.ReadLine());

                        Dobavitelji = m.EditList(idDoba, nazivD, dav, naslov, kon, opis);
                    }

                    else if (t1 == false)
                    {
                        Console.WriteLine("Dobavitelja ni bilo mogoče najti v zbirki podatkov. Se vpisali pravilen id?");
                    }
                }

                else if (opt == 8)
                {
                    List<Dobavitelj> dobaP = m.ReturnDoba();

                    foreach (var i in dobaP)
                    {
                        Console.WriteLine(i);
                    }
                }

                else if (opt == 9)
                {
                    Console.WriteLine("Vpišite ime xml dokumenta: ");
                    string document = Convert.ToString(Console.ReadLine());
                    XmlSchemaSet schema = new XmlSchemaSet();
                    schema.Add("", "XmlSchema.xsd");

                    XDocument doc = XDocument.Load(document + ".xml");

                    bool validationErrors = false;

                    doc.Validate(schema, (s, e) =>
                    {
                        Console.WriteLine(e.Message);
                        validationErrors = true;
                    });



                    if (validationErrors)
                    {
                        Console.WriteLine("Validacija neuspešna!");
                    }

                    else
                    {
                        Console.WriteLine("Validacija uspešna!");
                    }
                }

                else if (opt == 10) //Izpis stvari z uporabo xPath
                {
                    int opti = 0;
                    Console.WriteLine("Vpišite ime xml dokumenta: ");
                    string doc = Convert.ToString(Console.ReadLine());

                    XmlDocument docXml = new XmlDocument();

                    docXml.Load(doc + ".xml");

                    Console.WriteLine("Izpis nazivov vseh izdelkov iz XML dokumenta: 1");
                    Console.WriteLine("Izpis nazivov vseh artiklov, katerih cena je večja od pogoja: 2");
                    Console.WriteLine("Vsoto cen vseh artiklov: 3");
                    Console.WriteLine("Povprečno vrednost cen artiklov: 4");
                    Console.WriteLine("Izpis prvega artikla v xml dokumentu: 5");
                    Console.WriteLine("Izpis zadnjega artikla v xml dokumentu: 6");
                    Console.WriteLine("Število elementov v xml dokumentu: 7");
                    Console.WriteLine("Seštevek vseh cen: 8");
                    Console.WriteLine("Povprečje cen artiklov: 10");

                    opti = int.Parse(Console.ReadLine());

                    Console.WriteLine();

                    if (opti == 1) //Nazivi vseh artiklov v dokumentu
                    {
                        XmlNodeList nodes = docXml.SelectNodes("//ArrayOfIzdelek/Izdelek/NazivIzdelka/text()");

                        foreach (XmlNode i in nodes)
                        {
                            Console.WriteLine("Naziv: " + i.Value.ToString());
                        }
                    }

                    else if (opti == 2) //Nazivi artiklov kjer cena > pogoj
                    {
                        Console.WriteLine("Vnesite želeni pogoj: ");
                        int pogoj = int.Parse(Console.ReadLine());

                        XmlNodeList nodes = docXml.SelectNodes($"//ArrayOfIzdelek/Izdelek[CenaIzdelka>{pogoj}]/NazivIzdelka/text()");

                        foreach (XmlNode i in nodes)
                        {
                            Console.WriteLine("Naziv: " + i.Value.ToString());
                        }
                    }

                    else if (opti == 3) //vsota cen artiklov
                    {
                        int sum = 0;

                        XmlNodeList nodes = docXml.SelectNodes("//ArrayOfIzdelek/Izdelek/CenaIzdelka/text()");

                        foreach (XmlNode i in nodes)
                        {
                            string sumStr = i.Value.ToString();
                            sum += int.Parse(sumStr);
                        }

                        Console.WriteLine("Vsota cen izdelkov: " + sum);
                    }

                    else if (opti == 4) //Povprečna cena artiklov
                    {
                        int sum = 0;

                        XmlNodeList nodes = docXml.SelectNodes("//ArrayOfIzdelek/Izdelek/CenaIzdelka/text()");

                        foreach (XmlNode i in nodes)
                        {
                            string sumStr = i.Value.ToString();
                            sum += int.Parse(sumStr);
                        }

                        int count = nodes.Count;

                        int avg = sum / count;

                        Console.WriteLine("Povprečna vrenost cen izdelkov je: " + avg);
                    }

                    else if (opti == 5) //Prvi artikel v xml dokumentu
                    {
                        XmlNode node = docXml.SelectSingleNode("//ArrayOfIzdelek/Izdelek[1]/NazivIzdelka");

                        string nodeStr = string.Empty;

                        foreach (XmlNode i in node)
                        {
                            nodeStr = i.InnerText;
                        }

                        Console.WriteLine("Naziv prvega izdelka v dokumentu: " + nodeStr);
                    }

                    else if (opti == 6) //zadnji artikel v xml dokumentu
                    {
                        XmlNode node = docXml.SelectSingleNode("//ArrayOfIzdelek/Izdelek[last()]/NazivIzdelka");

                        string nodeStr = string.Empty;

                        foreach (XmlNode i in node)
                        {
                            nodeStr = i.InnerText;
                        }

                        Console.WriteLine("Naziv zadnjega izdelka v dokumentu: " + nodeStr);
                    }

                    else if (opti == 7) //število elementov v xml dokumentu
                    {
                        XmlNodeList nodes = docXml.SelectNodes("//ArrayOfIzdelek/Izdelek");

                        int count = 0;

                        count = nodes.Count;

                        Console.WriteLine("Število elementov v dokumentu: " + count);
                    }

                    else if (opti == 8) //zmnožek cen artiklov
                    {
                        XmlNodeList nodes = docXml.SelectNodes("//ArrayOfIzdelek/Izdelek/CenaIzdelka/text()");

                        int mult = 1;

                        foreach (XmlNode i in nodes)
                        {
                            mult *= int.Parse(i.Value);
                        }

                        Console.WriteLine("Zmnožek cen artiklov: " + mult);
                    }



                    else if (opti == 10)
                    {
                        int sum = 0;

                        XmlNodeList nodes = docXml.SelectNodes("//ArrayOfIzdelek/Izdelek/CenaIzdelka/text()");

                        foreach (XmlNode i in nodes)
                        {
                            string sumStr = i.Value.ToString();
                            sum += int.Parse(sumStr);
                        }

                        int count = nodes.Count;

                        int avg = sum / count;

                        Console.WriteLine("Povprečna vrenost cen izdelkov je: " + avg);
                    }


                }

                else if (opt == 11)
                {
                    Console.WriteLine("Vpišite ime xml dokumenta, ki ga želite pretvoriti: ");
                    string xmlDoc = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Vpišite ime dokumenta pod katerega želite shraniti html stran: ");
                    string htmlDoc = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Konverzija v html stran / artikli v navadni tabeli: 1");
                    Console.WriteLine("Konverzija v html stran / artikli urejeni po ceni padajoče: 2");
                    Console.WriteLine("Konverzija v html stran / artikli manjši od pogoja: 3");
                    Console.WriteLine("Konverzija v html stran / najdražji artikel: 4");
                    Console.WriteLine("Konverzija v html stran / najcenejši artikel: 5");
                    int opti = int.Parse(Console.ReadLine());

                    if (opti == 1)
                    {
                        try { m.ConvertXML(xmlDoc, htmlDoc); }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error v pretvorbi: " + e);
                        }
                    }

                    else if (opti == 2)
                    {
                        try { m.ConvertXMLDescending(xmlDoc, htmlDoc); }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error v pretvorbi: " + e);
                        }
                    }

                    else if (opti == 3)
                    {
                        Console.WriteLine("Vnesite pogoj: ");
                        int pogoj = int.Parse(Console.ReadLine());

                        try { m.ConvertXMLPog(xmlDoc, htmlDoc, pogoj); }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error v pretvorbi: " + e);
                        }
                    }

                    else if (opti == 4)
                    {
                        try
                        {

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error v pretvorbi: " + e);
                        }
                    }

                }

                else if (opt == 12)
                {
                    Console.WriteLine("Vpišite ime xml dokumenta, ki ga želite pretvoriti: ");
                    string xmlDoc = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Vpišite ime pdf dokumenta, ki ga želite shraniti po pretvorbi: ");
                    string pdfDoc = Convert.ToString(Console.ReadLine());

                    m.ConvertXMLhtml(xmlDoc, pdfDoc);

                    Console.WriteLine("Za pridobitev pdf pritisnite enter!");
                    Console.ReadLine();
                    m.ConvertHtmlPdf(pdfDoc, pdfDoc);
                }

                else if (opt == 13)
                {
                    string nameX;
                    Console.WriteLine("Vpišite ime xml dokumenta, ki ga ža želite pretvoriti: ");
                    string xmlDoc = Convert.ToString(Console.ReadLine());

                    Console.WriteLine("Vpišite ime docx dokumenta, ki ga želite shraniti po pretvorbi: ");
                    string htmlDoc = Convert.ToString(Console.ReadLine());

                    m.ConvertXMLhtml(xmlDoc, htmlDoc);

                    Console.WriteLine("Za pridobitev docx pritisnite enter!");
                    Console.ReadLine();
                    m.ConvertDocX(htmlDoc, htmlDoc);

                    Console.WriteLine("Želite konverzijo DOCX v XML?: 1 - DA 2 - NE");
                    int g = int.Parse(Console.ReadLine());

                    if (g == 1)
                    {
                        Console.WriteLine("Vpišite ime xml dokumenta, ki ga želite: ");
                        nameX = Convert.ToString(Console.ReadLine());

                        m.ConvertXMLDocx(htmlDoc, nameX);
                    }

                    Console.WriteLine("Želite konverzijo vašega XML dokumenta v html in pdf?: 1 - DA 2 - NE ");
                    int select = int.Parse(Console.ReadLine()); 

                    if(select == 1)
                    {
                        Console.WriteLine("Vpišite ime html izhodnega dokumenta");
                        string outDoc2 = Convert.ToString(Console.ReadLine());
                        Console.WriteLine("Vpišite ime pdf izhodnega dokumenta");
                        string outDoce = Convert.ToString(Console.ReadLine());

                        Console.WriteLine("Za pridobitev html pritisnite enter");
                        Console.ReadKey();
                        
                        m.ConvertXMLhtml("Artikli", outDoc2);

                        Console.WriteLine("Za pridobitev pdf pritisnite enter");
                        Console.ReadKey();

                        m.ConvertHtmlPdf(outDoc2, outDoce);
                    }
                }

                else if(opt == 14)
                {
                    Console.WriteLine("Vpišite ime XML dokumenta: ");
                    string xmlDoc = Console.ReadLine() + ".xml";

                    XmlDocument docXml = new XmlDocument();
                    docXml.Load(xmlDoc);
                    XmlNodeList nodesNazivi = docXml.SelectNodes("//ArrayOfIzdelek/Izdelek/NazivIzdelka/text()");
                    XmlNodeList nodesCene = docXml.SelectNodes("//ArrayOfIzdelek/Izdelek/CenaIzdelka/text()");
                    XmlNodeList nodesZaloga = docXml.SelectNodes("//ArrayOfIzdelek/Izdelek/Zaloga/text()");
                }
            }
        }

        private static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
        }

        static void ArtikliValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + e.Message);
            else
                Console.WriteLine("\tValidation error: " + e.Message);
        }
    }
}
