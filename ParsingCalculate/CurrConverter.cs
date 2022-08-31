using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ParsingCalculate
{
    public class CurrConverter
    {
        public static string[] Currency()
        {
            return new string[] {"EUR", "USD", "JPY", "BGN", "CZK", "DKK", "GBP", "HUF", "PLN", "RON", "SEK", "CHF", "ISK", "NOK",
                "HRK", "TRY", "AUD", "BRL", "CAD", "CNY", "HKD", "IDR", "ILS", "INR", "KRW", "MXN", "MYR", "NZD", "PHP",
                "SGD", "ZAR"};
        }


        //problem!!!
        public static float CurrencyEuro(string currency)
        {
            
            if(currency.ToLower() == "")
            {
                throw new ArgumentException("Error! Currency can't empty!");
            }
            if(currency.ToLower() == "eur")
            {
                throw new ArgumentException("Error! Can't exchange Euro to Euro");
            }
            

            try
            {
                string rssUrl = string.Concat("http://www.ecb.int/rss/fxref-", currency.ToLower() + ".html");
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(rssUrl);

                //xml namespace
                
                System.Xml.XmlNamespaceManager nsmgr = new System.Xml.XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("rdf", "http://purl.org/rss/1.0/");
                nsmgr.AddNamespace("cb", "http://www.cbwiki.net/wiki/index.php/Specification_1.1");

                //exchange selected currnecy and eur
                System.Xml.XmlNodeList nodeList = doc.SelectNodes("//rdf:item", nsmgr);

                
                foreach (System.Xml.XmlNode node in nodeList)
                {
                    //CultureInfo
                    CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.CurrencyDecimalSeparator = ".";

                    try
                    {
                        float exchangeRate = float.Parse(
                            node.SelectSingleNode("//cb:statistics//cb:exchangeRate//cb:value", nsmgr).InnerText,
                            NumberStyles.Any,
                            ci);

                        return exchangeRate;
                    }
                    catch { }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static float Exchange(string from, string to, float amount = 1)
        {
            if (from == null || to == null)
            {
                return 0;
            }
            if (from.ToLower() == "eur" && to.ToLower() == "eur")
            {
                return amount;
            }

            try
            {
                float toRate = CurrencyEuro(to);
                float fromRate = CurrencyEuro(from);

                if (from.ToLower() == "eur")
                {
                    return (amount * toRate);
                }
                else if (to.ToLower() == "eur")
                {
                    return (amount / fromRate);
                }
                else
                {
                    return (amount * toRate) / fromRate;
                }
            }
            catch { return 0; }
        }
    }
}
