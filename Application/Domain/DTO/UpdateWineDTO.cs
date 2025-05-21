namespace Fiap.Agnello.CLI.Application.Domain.DTO
{
    /// <summary>
    /// Representa um DTO utilizado para atualizar parcialmente os dados de um vinho.
    /// Todos os campos são opcionais.
    /// </summary>
    internal record UpdateWineDTO(string? MakerId, string? Name, string? Grape, string? Price, string? Year)
    {
    }
}
