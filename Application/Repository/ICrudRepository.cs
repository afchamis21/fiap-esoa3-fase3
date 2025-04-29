using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Agnello.CLI.Application.Repository
{
    internal interface ICrudRepository<T, ID>
    {
        abstract T Save(T entity);
        abstract bool Delete(ID id);
        abstract List<T> GetAll();
        abstract T? GetById(ID id);
    }
}
