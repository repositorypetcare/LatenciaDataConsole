using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int id = 1;
            int minuto = 15;
            DateTime dl = new DateTime(2022, 08, 31, 23, 59, 00);
            DateTime hi = DateTime.Now;
            DateTime hb = hi;
            byte flag = 1;
            string s = "<?xml-stylesheet type='text/xsl' href='lst_agenda.xslt'?>";
            string intervalos = string.Format(" date1='{0}'", hb.ToString("dd/MM/yyyy"));
            intervalos += string.Format(" date2='{0}'", hb.AddDays(1).ToString("dd/MM/yyyy"));
            intervalos += string.Format(" date3='{0}'", hb.AddDays(2).ToString("dd/MM/yyyy"));
            s += "<root " + intervalos + ">\n";
            int i = 1;
            while (i <= 2)
            {
                hb = hi;
                if (hb.Minute <= 15)
                    hb = new DateTime(hb.Year, hb.Month, hb.Day, hb.Hour, 15, 00);
                if (hb.Minute > 15 && hb.Minute <= 30)
                    hb = new DateTime(hb.Year, hb.Month, hb.Day, hb.Hour, 30, 00);
                if (hb.Minute > 30 && hb.Minute <= 45)
                    hb = new DateTime(hb.Year, hb.Month, hb.Day, hb.Hour, 45, 00);
                if (hb.Minute > 45)
                    hb = new DateTime(hb.Year, hb.Month, hb.Day, hb.Hour + 1, 0, 00);
                string nome = "João da Silva";
                if (i == 2)
                {
                    nome = "Carlos Lima";
                }
                s += string.Format("<prof idprof='{0}' nome='{1}'>\n", i, nome);
                while (hb < dl)
                {

                    s += string.Format("<item id='{0}' idlocal='{1}' date='{2}' flag='{3}' />\n", id, 1, hb.ToString(), DateEval(hi, hb));
                    id++;
                    hb = hb.AddMinutes(minuto);
                }
                s += "</prof>\n";
                i++;
            }

            hb = hi;
            minuto = 40;
            if (hb.Minute <= 15)
                hb = new DateTime(hb.Year, hb.Month, hb.Day, hb.Hour, 15, 00);
            if (hb.Minute > 15 && hb.Minute <= 30)
                hb = new DateTime(hb.Year, hb.Month, hb.Day, hb.Hour, 30, 00);
            if (hb.Minute > 30 && hb.Minute <= 45)
                hb = new DateTime(hb.Year, hb.Month, hb.Day, hb.Hour, 45, 00);
            if (hb.Minute > 45)
                hb = new DateTime(hb.Year, hb.Month, hb.Day, hb.Hour + 1, 0, 00);



            i = 3;
            while (i == 3)
            {
                hb = hi;
                if (hb.Minute > 40)
                    hb = new DateTime(hb.Year, hb.Month, hb.Day, hb.Hour + 1, 0, 00);

                string nome = "Adriano Jesus";
                s += string.Format("<prof idprof='{0}' nome='{1}'>\n", i, nome);
                while (hb < dl)
                {
                    int k = (int)hb.DayOfWeek;
                    if (i != 6)
                    {
                        s += string.Format("<item id='{0}' idlocal='{1}' date='{2}' flag='{3}'/>\n", id, 3, hb.ToString(), DateEval(hi, hb));
                        id++;
                    }
                    hb = hb.AddMinutes(minuto);

                }
                s += "</prof>\n";
                i++;
            }

            //for (int i = 2; i <=2; i++)
            //{
            //    for (int j = 2; j <= 2; j++)
            //    {

            //        s += string.Format("<item id='{0}' idprof='{1}' idlocal='{2}' date='{3}'>", id, j, 3, hb.ToString());
            //        id++;
            //        hb.AddMinutes(minuto);

            //    }
            //}

            s += "</root>";

            //File.WriteAllText("d:\\agenda.xml", s);
            string path = "C:\\Users\\guilherme.tomaz\\source\\repos\\AppointmentsPoc\\AppointmentsPoc\\App_Data\\";
            File.WriteAllText(path + "agenda.html", Transform(s, path + "lst_agenda.xslt"));
            Console.WriteLine(s);

            Console.ReadKey();

        }
        /// Transforma uma string XML com base em um arquivo de transformação XSLT

        /// </summary>

        /// <param name="XmlString">String Xml</param>

        /// <param name="XsltFile">Arquivo Xslt</param>

        /// <returns>string transformada</returns>

        public static string Transform(string XmlString, string XsltFile)

        
        {

            StringBuilder sb = new StringBuilder();


            if (XmlString != "")

            {

                try

                {

                    XslCompiledTransform xslDoc = new XslCompiledTransform();

                    XsltSettings settings = new XsltSettings(false, true);

                    XsltArgumentList Args = new XsltArgumentList();

                    settings.EnableDocumentFunction = true;

                    xslDoc.Load(XsltFile, settings, new XmlUrlResolver());

                    StringWriter stringWriter = new StringWriter(sb);

                    XsltUtil obj = new XsltUtil();

                    Args.AddExtensionObject("urn:util", obj);

                    xslDoc.Transform(XmlReader.Create(new StringReader(XmlString)), Args, stringWriter);

                    stringWriter.Close();

                    return sb.ToString();

                }

                catch (Exception Error)

                {

                }

            }

            return "";

        }

        private static byte DateEval(DateTime p1, DateTime p2)
        {
            p1 = new DateTime(p1.Year, p1.Month, p1.Day, 0, 0, 0);
            p2 = new DateTime(p2.Year, p2.Month, p2.Day, 0, 0, 0);
            TimeSpan ts = p2 - p1;
            if (ts.Days == 0)
            {
                return 1;
            }
            if (ts.Days == 1)
            {
                return 2;
            }
            if (ts.Days == 2)
            {
                return 3;
            }
            return 0;
        }
    }
    public class XsltUtil
    {
        public string FormatDate(string d)
        {
            DateTime y = Convert.ToDateTime(d);
            return y.ToString("HH:mm");
        }
    }
}
