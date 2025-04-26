using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Core
{

    /// <summary>
    /// Representa uma opção dentro de um menu, associando um nome e uma ação executável.
    /// </summary>
    /// <remarks>
    /// Instancia uma nova opção de menu com o nome e a ação especificados.
    /// </remarks>
    /// <param name="name">Nome da opção de menu.</param>
    /// <param name="action">Ação que será executada quando a opção for selecionada.</param>
    internal class MenuOption(string name, Action action)
    {
        /// <summary>
        /// Obtém o nome da opção de menu.
        /// </summary>
        public string Name { get; } = name;

        /// <summary>
        /// Obtém a ação associada a esta opção de menu.
        /// </summary>
        public Action Action { get; } = action;

        /// <summary>
        /// Representa a opção de saída (QUIT) do menu. Ao ser selecionada, o programa será encerrado.
        /// </summary>
        public static MenuOption QUIT { get; } = new("Sair", () =>
        {
            ConsoleUtil.SystemMessage("Saindo...");
            Environment.Exit(0);
        });
    };
}
