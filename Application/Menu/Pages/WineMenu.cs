using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fiap.Agnello.CLI.Application.Menu.Core;

namespace Fiap.Agnello.CLI.Application.Menu.Pages
{
    internal class WineMenu : IMenuPage
    {
        public MenuLevel Build() => new("Vinhos", [
            new MenuOption("Voltar para Home", MenuNavigator.Navigate<HomeMenu>)
            ]
        );
    }
}
