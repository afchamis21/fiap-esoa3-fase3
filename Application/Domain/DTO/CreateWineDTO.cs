namespace Fiap.Agnello.CLI.Application.Domain.DTO
{
    internal record CreateWineDTO(int MakerId, string Name, string Grape, decimal Price, int Year)
    {
    }
}
