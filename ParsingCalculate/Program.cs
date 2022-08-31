// See https://aka.ms/new-console-template for more information
using ParsingCalculate;

/*
using System.Xml;

XmlReader xmlReader = XmlReader.Create("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
while (xmlReader.Read())
{
    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Cube"))
    {
        if (xmlReader.HasAttributes)
            Console.WriteLine(xmlReader.GetAttribute("currency") + ": " + xmlReader.GetAttribute("rate"));
    }
}
Console.ReadKey();
*/

string fromCurrency = "EUR";
string toCurrency = "USD";
int amount = 1;

string[] availableCurrency = CurrConverter.Currency();
Console.WriteLine("The following currencies are available to you: ");
Console.WriteLine(string.Join(",", availableCurrency));
Console.WriteLine("\n");

Console.WriteLine("Please input currency you want to convert FROM");
fromCurrency = Console.ReadLine();
Console.WriteLine("\n");

Console.WriteLine("Please input currency you want to convert TO");
toCurrency = Console.ReadLine();
Console.WriteLine("\n");

float exchangeRate = CurrConverter.Exchange(fromCurrency, toCurrency, amount);
Console.WriteLine("FROM " + amount + " " + fromCurrency.ToUpper() + " TO " + toCurrency.ToUpper() + " = " + exchangeRate);

Console.ReadLine();