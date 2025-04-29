
namespace Fiap.Agnello.CLI.Util
{
    /// <summary>
    /// Utilitário para manipulação e exibição de conteúdo no console.
    /// Contém métodos estáticos para mostrar mensagens, prompts e títulos formatados.
    /// </summary>
    internal static class ConsoleUtil
    {
        /// <summary>
        /// Exibe um prompt no console e retorna a entrada do usuário.
        /// </summary>
        /// <param name="prompt">Texto a ser exibido para o usuário.</param>
        /// <returns>A entrada do usuário como string. Pode ser nula se o usuário pressionar Enter sem digitar nada.</returns>
        public static string Prompt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? result = Console.ReadLine();
                if (result != null)
                {
                    return result;
                }

                Console.WriteLine("Digite um valor!");
            }
        }

        /// <summary>
        /// Solicita um número inteiro ao usuário via console até que uma entrada válida seja fornecida.
        /// </summary>
        /// <param name="prompt">Texto a ser exibido para o usuário.</param>
        /// <returns>Um número inteiro inserido pelo usuário.</returns>
        public static int PromptInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? value = Console.ReadLine();
                if (int.TryParse(value, out int result))
                {
                    return result;
                }

                Console.WriteLine("Digite um número inteiro válido!");
            }
        }


        /// <summary>
        /// Solicita um número decimal (float) ao usuário via console até que uma entrada válida seja fornecida.
        /// </summary>
        /// <param name="prompt">Texto a ser exibido para o usuário.</param>
        /// <returns>Um número decimal inserido pelo usuário.</returns>
        public static float PromptFloat(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? value = Console.ReadLine();
                if (float.TryParse(value?.Replace(".", ","), out float result))
                {
                    return result;
                }

                Console.WriteLine("Digite um número decimal válido!");
            }
        }

        /// <summary>
        /// Exibe uma mensagem do sistema no console, com o prefixo ">".
        /// </summary>
        /// <param name="message">Mensagem a ser exibida.</param>
        public static void SystemMessage(string message)
        {
            Console.WriteLine($"> {message}");
        }

        /// <summary>
        /// Exibe um título formatado no console, com bordas horizontais e verticais.
        /// </summary>
        /// <param name="title">Título a ser exibido.</param>
        internal static void WriteTitle(string title)
        {
            int size = title.Length + 4;
            string horizontalBorder = "".PadLeft(size, '═');
            Console.WriteLine($"╔{horizontalBorder}╗");
            Console.WriteLine($"║  {title}  ║");
            Console.WriteLine($"╚{horizontalBorder}╝");
        }
    }
}
