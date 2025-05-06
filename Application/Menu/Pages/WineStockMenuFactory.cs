using Fiap.Agnello.CLI.Application.Menu.Adapter;
using Fiap.Agnello.CLI.Application.Menu.Core;
using Fiap.Agnello.CLI.Application.Services;

namespace Fiap.Agnello.CLI.Application.Menu.Pages
{
    /// <summary>
    /// Fábrica de menu responsável por construir o menu de controle de estoque de vinhos.
    /// Oferece funcionalidades para entrada, saída e visualização do estoque.
    /// </summary>
    internal class WineStockMenuFactory : IMenuFactory
    {
        /// <summary>
        /// Constrói e retorna o menu de estoque de vinhos.
        /// </summary>
        /// <returns>Uma instância de <see cref="MenuLevel"/> com as opções de estoque.</returns>
        public MenuLevel Build()
        {
            WineServiceConsoleAdapter service = new();
            return new("Vinhos (Estoque)", [
                new MenuOption("Voltar para Home", MenuNavigator.Navigate<HomeMenuFactory>),
                new MenuOption("Registrar entrada", service.RegisterStockIn),
                new MenuOption("Registrar saída", service.RegisterStockOut),
                new MenuOption("Visualizar estoque", service.PrintStocks)
                ]);
        }
    }
}
