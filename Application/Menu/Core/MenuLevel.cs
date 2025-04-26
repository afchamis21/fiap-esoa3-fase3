using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Core
{
    /// <summary>
    /// Representa um nível de menu com opções para o usuário escolher.
    /// </summary>
    /// <remarks>
    /// Construtor da classe <see cref="MenuLevel"/>.
    /// Inicializa o nome do menu e suas opções.
    /// </remarks>
    /// <param name="name">Nome do menu.</param>
    /// <param name="optionArray">Lista de opções do menu.</param>
    internal class MenuLevel(string name, List<MenuOption> optionArray)
    {
        private readonly SortedDictionary<int, MenuOption> options = InitOptions(optionArray);
        public string Name { get; } = name;

        /// <summary>
        /// Inicializa as opções de menu, atribuindo um número de chave para cada opção.
        /// A opção de saída é sempre adicionada com a chave 0.
        /// </summary>
        /// <param name="optionArray">Lista de opções de menu.</param>
        /// <returns>Um dicionário ordenado de opções de menu.</returns>
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

        /// <summary>
        /// Exibe as opções disponíveis no menu.
        /// </summary>
        public void ShowOptions()
        {
            Console.WriteLine("\nEscolha uma opção: ");

            // Exibe as opções com suas respectivas chaves
            foreach (var item in options)
            {
                Console.WriteLine($"{item.Key} - {item.Value.Name}");
            }
        }

        /// <summary>
        /// Solicita ao usuário que escolha uma opção válida. Retorna a opção escolhida.
        /// </summary>
        /// <returns>A opção escolhida pelo usuário.</returns>
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
