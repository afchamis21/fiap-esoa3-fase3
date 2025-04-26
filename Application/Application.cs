using Fiap.Agnello.CLI.Application.Menu.Core;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application
{
    internal class Application
    {

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
