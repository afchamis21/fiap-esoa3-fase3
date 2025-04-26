using Fiap.Agnello.CLI.Application.Menu.Pages;

namespace Fiap.Agnello.CLI.Application.Menu.Core
{
    internal static class MenuNavigator
    {
        public static MenuLevel Current { get; private set; } = new HomeMenu().Build();

        public static void Navigate<T>() where T : IMenuPage, new()
        {
            Current = new T().Build();
        }
    }
}
