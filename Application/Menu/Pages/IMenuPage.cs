using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiap.Agnello.CLI.Application.Menu.Core;

namespace Fiap.Agnello.CLI.Application.Menu.Pages
{
    /// <summary>
    /// Define a interface para páginas de menu que podem ser construídas e exibidas.
    /// Cada página de menu deve implementar este contrato para garantir que tenha um método para construir um menu.
    /// </summary>
    internal interface IMenuPage
    {
        /// <summary>
        /// Constrói o <see cref="MenuLevel"/> associado à página de menu.
        /// O método deve retornar um nível de menu que será exibido.
        /// </summary>
        /// <returns>Um objeto <see cref="MenuLevel"/> representando o menu a ser exibido.</returns>

        MenuLevel Build();
    }
}
