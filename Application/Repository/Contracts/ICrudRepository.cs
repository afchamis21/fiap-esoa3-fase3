using System.Security.Principal;

namespace Fiap.Agnello.CLI.Application.Repository.Contracts
{
    internal interface ICrudRepository<T, ID> where T : IEntity<ID>
    {
        T Create(T entity);
        T Update(T entity);
        bool Delete(ID id);
        List<T> GetAll();
        T? GetById(ID id);
    }
}
