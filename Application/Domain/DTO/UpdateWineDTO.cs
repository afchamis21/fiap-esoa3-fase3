namespace Fiap.Agnello.CLI.Application.Domain.DTO
{
    /// <summary>
    /// Representa um DTO utilizado para atualizar parcialmente os dados de um vinho.
    /// Todos os campos são opcionais.
    /// </summary>
    internal record UpdateWineDTO(string? Maker, string? Name, string? Country, string? Grape, string? Price, string? Year)
    {
    }
}
