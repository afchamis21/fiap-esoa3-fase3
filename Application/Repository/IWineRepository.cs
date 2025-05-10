using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Repository.Contracts;

namespace Fiap.Agnello.CLI.Application.Repository
{
    internal interface IWineRepository: ICrudRepository<Wine, int>
    {
    }
}
