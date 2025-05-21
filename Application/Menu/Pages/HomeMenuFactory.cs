using Fiap.Agnello.CLI.Application.Menu.Core;

namespace Fiap.Agnello.CLI.Application.Menu.Pages
{
    internal class HomeMenuFactory : IMenuFactory
    {
        public MenuLevel Build() => new ("Home", [
            new MenuOption("Ir para Marcas (ADMIN)", MenuNavigator.Navigate<BrandAdminMenuFactory>),
            new MenuOption("Ir para Vinhos (ADMIN)", MenuNavigator.Navigate<WineAdminMenuFactory>),
            new MenuOption("Ir para Vinhos (Estoque)", MenuNavigator.Navigate<WineStockMenuFactory>)
        ]);
    }
}
