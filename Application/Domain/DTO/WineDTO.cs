using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Agnello.CLI.Application.Domain.DTO
{
    internal record WineDTO(string maker, string name, string country, string grape, float price, int year)
    {
    }
}
