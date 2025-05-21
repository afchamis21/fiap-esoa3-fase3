using System.Globalization;
using Fiap.Agnello.CLI.Application.Contracts;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.Application.Repository.Contracts;


namespace Fiap.Agnello.CLI.Application.Domain
{
    /// <summary>
    /// Representa um vinho com informações como fabricante, nome, uva, país, preço e ano de produção.
    /// </summary>
    internal class Wine(string name, string grape, decimal price, int year) : IJsonSerializable, IEntity<int>
    {
        /// <summary>
        /// Obtém ou define o identificador único do vinho.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém ou define o nome do fabricante do vinho.
        /// </summary>
        public required Brand Maker { get; set; }


        /// <summary>
        /// Obtém ou define o nome do fabricante do vinho.
        /// </summary>
        public int MakerId { get; set; }

        /// <summary>
        /// Obtém ou define o nome do vinho.
        /// </summary>
        public string Name { get; set; } = name;

        /// <summary>
        /// Obtém ou define o tipo de uva utilizado no vinho.
        /// </summary>
        public string Grape { get; set; } = grape;

        /// <summary>
        /// Obtém ou define o preço do vinho.
        /// </summary>
        public decimal Price { get; set; } = price;

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

            return $"ID: {Id} | Nome: {Name} | Fabricante: {Maker.Name} | País: {Maker.Country} | Uva: {Grape} | Preço: {Price.ToString("C", brCulture)} | Ano: {Year}";
        }

        /// <summary>
        /// Cria uma instância de <see cref="Wine"/> a partir de um <see cref="CreateWineDTO"/>.
        /// </summary>
        /// <param name="dto">Objeto DTO contendo os dados do vinho.</param>
        /// <returns>Uma nova instância da classe <see cref="Wine"/>.</returns>
        public static Wine FromDTO(CreateWineDTO dto)
        {
            return new(dto.Name, dto.Grape, dto.Price, dto.Year) { MakerId = dto.MakerId, Maker = null! };
        }

        public int GetPrimaryKey()
        {
            return Id;
        }
    }
}