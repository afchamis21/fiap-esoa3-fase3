using System.Collections.Immutable;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Core
{
    internal class MenuLevel(string name, List<MenuOption> optionArray)
    {
        private readonly SortedDictionary<int, MenuOption> options = InitOptions(optionArray);
        public string Name { get; } = name;

        private static SortedDictionary<int, MenuOption> InitOptions(List<MenuOption> optionArray)
        {
            var dict = new SortedDictionary<int, MenuOption>();

            int counter = 1;
            foreach (var option in optionArray)
            {
                dict.Add(counter, option);
                counter++;
            }

            dict.Add(0, MenuOption.QUIT);

            return dict;
        }

        public void ShowOptions()
        {
            Console.WriteLine("\nEscolha uma opção: ");

            foreach (var item in options)
            {
                Console.WriteLine($"{item.Key} - {item.Value.Name}");
            }
        }

        public MenuOption ChooseOption()
        {
            while (true)
            {
                Console.WriteLine();
                string? input = ConsoleUtil.Prompt("Escolha uma opção: ");

                if (input == null)
                {
                    ConsoleUtil.SystemMessage("Escolha uma opção!");
                    continue;
                }

                if (!int.TryParse(input, out int key))
                {
                    ConsoleUtil.SystemMessage("Opção inválida! Digite o número correspondente a uma das opções!");
                    continue;
                }

                if (!options.TryGetValue(key, out MenuOption? option))
                {
                    ConsoleUtil.SystemMessage("Opção inválida! Escolha uma das opções apresentadas");
                    continue;
                }

                return option;
            }
        }
    }
}
