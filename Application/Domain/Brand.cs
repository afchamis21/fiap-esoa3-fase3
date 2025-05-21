using Fiap.Agnello.CLI.Application.Contracts;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.Application.Repository.Contracts;

namespace Fiap.Agnello.CLI.Application.Domain
{
    internal class Brand(string name, string country) : IJsonSerializable, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string Country { get; set; } = country;

        public int GetPrimaryKey()
        {
            return Id;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Nome: {Name} | País: {Country}";
        }

        public static Brand FromDTO(CreateBrandDTO dto)
        {
            return new(dto.Name, dto.Country);
        }
    }
}
