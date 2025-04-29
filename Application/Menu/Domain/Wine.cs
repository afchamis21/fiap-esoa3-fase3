using System.Globalization;

namespace Fiap.Agnello.CLI.Application.Menu.Domain
{
    internal class Wine
    {
        public Wine(string maker, string name, string country, string grape, float price, int year)
        {
            Maker = maker;
            Name = name;
            Country = country;
            Grape = grape;
            Price = price;
            Year = year;
        }

        public int? Id { get; set; }
        public string Maker { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Grape { get; set; }
        public float Price { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            var brCulture = new CultureInfo("pt-BR");

            return $"ID: {Id} | Nome: {Name} | Fabricante: {Maker} | País: {Country} | Uva: {Grape} | Preço: {Price.ToString("C", brCulture)} | Ano: {Year}";
        }
    }
}