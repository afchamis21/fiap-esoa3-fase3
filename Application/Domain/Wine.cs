using System.Globalization;
using System.Text.Json.Nodes;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.db.Adapters;

namespace Fiap.Agnello.CLI.Application.Domain
{
    internal class Wine(string maker, string name, string country, string grape, float price, int year) : IJsonSerializable
    {
        public int? Id { get; set; }
        public string Maker { get; set; } = maker;
        public string Name { get; set; } = name;
        public string Country { get; set; } = country;
        public string Grape { get; set; } = grape;
        public float Price { get; set; } = price;
        public int Year { get; set; } = year;
        public int Stock { get; set; } = 0;

        public override string ToString()
        {
            var brCulture = new CultureInfo("pt-BR");

            return $"ID: {Id,-5} | Nome: {Name,-15} | Fabricante: {Maker,-10} | País: {Country,-10} | Uva: {Grape,-15} | Preço: {Price.ToString("C", brCulture),-15} | Ano: {Year}";
        }

        public static Wine FromDTO(WineDTO dto)
        {
            return new(dto.maker, dto.name, dto.country, dto.grape, dto.price, dto.year);
        }
    }
}