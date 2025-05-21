namespace Fiap.Agnello.CLI.Application.Repository.Contracts
{
    internal interface IEntity<TID>
    {
        TID? GetPrimaryKey();
    }
}
