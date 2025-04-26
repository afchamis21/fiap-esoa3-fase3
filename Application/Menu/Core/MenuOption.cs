using System.Net.Quic;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Core
{
    internal class MenuOption(string name, Action action)
    {
        public string Name { get; } = name;
        public Action Action { get; } = action;

        public static MenuOption QUIT { get; } = new("Sair", () =>
        {
            ConsoleUtil.SystemMessage("Saindo...");
            Environment.Exit(0);
        });
    };
}
