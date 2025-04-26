
namespace Fiap.Agnello.CLI.Util
{
    internal static class ConsoleUtil
    {
        public static string? Prompt(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public static void SystemMessage(string message)
        {
            Console.WriteLine($"> {message}");

        }

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
