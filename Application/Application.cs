using Fiap.Agnello.CLI.Application.Menu.Core;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application
{
    /// <summary>
    /// Classe principal responsável pela execução da aplicação CLI.
    /// Gerencia o ciclo de vida da aplicação, exibe o menu e processa a interação do usuário.
    /// </summary>
    internal class Application
    {

        /// <summary>
        /// Exibe a mensagem de boas-vindas no console.
        /// Mostra um título artístico e uma mensagem de boas-vindas.
        /// </summary>
        private static void Welcome()
        {
            string title = @"
░█████╗░░██████╗░███╗░░██╗███████╗██╗░░░░░██╗░░░░░░█████╗░  ░█████╗░██╗░░░░░██╗
██╔══██╗██╔════╝░████╗░██║██╔════╝██║░░░░░██║░░░░░██╔══██╗  ██╔══██╗██║░░░░░██║
███████║██║░░██╗░██╔██╗██║█████╗░░██║░░░░░██║░░░░░██║░░██║  ██║░░╚═╝██║░░░░░██║
██╔══██║██║░░╚██╗██║╚████║██╔══╝░░██║░░░░░██║░░░░░██║░░██║  ██║░░██╗██║░░░░░██║
██║░░██║╚██████╔╝██║░╚███║███████╗███████╗███████╗╚█████╔╝  ╚█████╔╝███████╗██║
╚═╝░░╚═╝░╚═════╝░╚═╝░░╚══╝╚══════╝╚══════╝╚══════╝░╚════╝░  ░╚════╝░╚══════╝╚═╝
";
            Console.WriteLine(title);
            Console.WriteLine("Bem vindo à Agnello CLI!\n");
        }

        /// <summary>
        /// Inicia a execução da aplicação CLI.
        /// Exibe o menu inicial, processa as interações do usuário e navega pelas opções do menu.
        /// </summary>
        public void Execute()
        {
            Welcome();
            while (true)
            {
                MenuLevel level = MenuNavigator.Current;

                ConsoleUtil.WriteTitle(level.Name);
                level.ShowOptions();
                MenuOption option = level.ChooseOption();
                option.Action();
                Console.WriteLine();
            }
        }
    }
}
