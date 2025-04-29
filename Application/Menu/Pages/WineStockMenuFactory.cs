using Fiap.Agnello.CLI.Application.Menu.Core;
using Fiap.Agnello.CLI.Application.Menu.Services;

namespace Fiap.Agnello.CLI.Application.Menu.Pages
{
    internal class WineStockMenuFactory : IMenuFactory
    {
        public MenuLevel Build()
        {
            WineStockService service = new();
            return new("Vinhos (Estoque)", [
                new MenuOption("Voltar para Home", MenuNavigator.Navigate<HomeMenuFactory>),
                new MenuOption("Registrar entrada", service.RegisterStockIn),
                new MenuOption("Registrar saída", service.RegisterStockOut),
                new MenuOption("Visualizar estoque", service.PrintStocks)
                ]);
        }
    }
}
