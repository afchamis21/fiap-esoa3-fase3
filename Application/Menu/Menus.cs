using Fiap.Agnello.CLI.Application.Menu.Core;

namespace Fiap.Agnello.CLI.Application.Menu
{
    internal class Menus
    {
        public static MenuLevel Home { get; } = new("Home", [
            new MenuOption("Ir para Vinhos", () => Current = Vinhos)
            ]
        );

        public static MenuLevel Vinhos { get; } = new("Home", [
            new MenuOption("Voltar para Home", () => Current = Home)
            ]
        );

        public static MenuLevel Current { get; set; } = Home;
    }
}
