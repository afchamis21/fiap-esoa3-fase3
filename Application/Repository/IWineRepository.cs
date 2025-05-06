using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Repository.Contracts;

namespace Fiap.Agnello.CLI.Application.Repository
{
    internal interface IWineRepository: ICrudRepository<Wine, int>
    {
    }
}
