using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiap.Agnello.CLI.Application.Menu.Core;

namespace Fiap.Agnello.CLI.Application.Menu.Pages
{
    /// <summary>
    /// Interface para fábricas de menus que constroem instâncias de <see cref="MenuLevel"/>.
    /// Cada implementação é responsável por montar e retornar um nível de menu pronto para exibição e interação.
    /// </summary>
    internal interface IMenuFactory
    {
        /// <summary>
        /// Constrói e retorna uma instância de <see cref="MenuLevel"/>, representando uma página de menu da aplicação.
        /// </summary>
        /// <returns>Uma instância de <see cref="MenuLevel"/> configurada com título e opções.</returns>
        MenuLevel Build();
    }
}
