
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
        public static string? Prompt(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
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
