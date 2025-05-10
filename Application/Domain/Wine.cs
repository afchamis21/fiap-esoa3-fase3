using System.Globalization;
using System.Text.Json.Nodes;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.db.Adapters;


namespace Fiap.Agnello.CLI.Application.Domain
{
    /// <summary>
    /// Representa um vinho com informações como fabricante, nome, uva, país, preço e ano de produção.
    /// </summary>
    internal class Wine(string maker, string name, string country, string grape, float price, int year) : IJsonSerializable
    {
        /// <summary>
        /// Obtém ou define o identificador único do vinho.
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Obtém ou define o nome do fabricante do vinho.
        /// </summary>
        public string Maker { get; set; } = maker;

        /// <summary>
        /// Obtém ou define o nome do vinho.
        /// </summary>
        public string Name { get; set; } = name;

        /// <summary>
        /// Obtém ou define o país de origem do vinho.
        /// </summary>
        public string Country { get; set; } = country;

        /// <summary>
        /// Obtém ou define o tipo de uva utilizado no vinho.
        /// </summary>
        public string Grape { get; set; } = grape;

        /// <summary>
        /// Obtém ou define o preço do vinho.
        /// </summary>
        public float Price { get; set; } = price;

        /// <summary>
        /// Obtém ou define o ano de produção do vinho.
        /// </summary>
        public int Year { get; set; } = year;

        /// <summary>
        /// Obtém ou define a quantidade em estoque do vinho.
        /// </summary>
        public int Stock { get; set; } = 0;

        /// <summary>
        /// Retorna uma representação textual formatada do vinho.
        /// </summary>
        /// <returns>Uma string contendo os detalhes do vinho formatados.</returns>
        public override string ToString()
        {
            var brCulture = new CultureInfo("pt-BR");

            return $"ID: {Id} | Nome: {Name} | Fabricante: {Maker} | País: {Country} | Uva: {Grape} | Preço: {Price.ToString("C", brCulture)} | Ano: {Year}";
        }

        /// <summary>
        /// Cria uma instância de <see cref="Wine"/> a partir de um <see cref="WineDTO"/>.
        /// </summary>
        /// <param name="dto">Objeto DTO contendo os dados do vinho.</param>
        /// <returns>Uma nova instância da classe <see cref="Wine"/>.</returns>
        public static Wine FromDTO(WineDTO dto)
        {
            return new(dto.Maker, dto.Name, dto.Country, dto.Grape, dto.Price, dto.Year);
        }
    }
}