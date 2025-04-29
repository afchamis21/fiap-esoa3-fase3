using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiap.Agnello.CLI.Application.Menu.Core;

namespace Fiap.Agnello.CLI.Application.Menu.Pages
{
    internal class HomeMenuFactory : IMenuFactory
    {
        public MenuLevel Build() => new ("Home", [
                new MenuOption("Ir para Vinhos", MenuNavigator.Navigate<WineMenuFactory>)
        ]);
    }
}
