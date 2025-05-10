namespace Fiap.Agnello.CLI.Application.Repository.Contracts
{
    internal interface ICrudRepository<T, ID>
    {
        abstract T Save(T entity);
        abstract bool Delete(ID id);
        abstract List<T> GetAll();
        abstract T? GetById(ID id);
    }
}
