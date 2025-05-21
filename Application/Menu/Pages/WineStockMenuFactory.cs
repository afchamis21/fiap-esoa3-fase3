using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Menu.Core;
using Fiap.Agnello.CLI.Application.Menu.Helpers;
using Fiap.Agnello.CLI.Application.Repository;
using Fiap.Agnello.CLI.Application.Repository.Contracts;
using Fiap.Agnello.CLI.Application.Services;
using Fiap.Agnello.CLI.Application.Services.Helpers;
using Fiap.Agnello.CLI.Util;

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
            ICrudRepository<Wine, int> _wineRepository = Repositories.GetRepository<Wine, int>();
            WineStockService _stockService = new(_wineRepository);
            WineService _wineService = new(_wineRepository);
            BrandService _brandService = new(Repositories.GetRepository<Brand, int>());

            BrandConsoleHelper _brandConsoleHelper = new(_brandService);
            WineStockConsoleHelper _stockConsoleHelper = new(_wineService);
            WineConsoleHelper _wineConsoleHelper = new(_wineService, _brandConsoleHelper);

            return new("Vinhos (Estoque)", [
                new MenuOption("Voltar para Home", MenuNavigator.Navigate<HomeMenuFactory>),
                new MenuOption("Registrar entrada", () => {
                    _stockConsoleHelper.PrintStocks();
                    Console.WriteLine();
                    _wineConsoleHelper.WithExistingWine(wine =>
                    {
                        int add = ConsoleUtil.PromptInt("Informe a quantidade a adicionar: ");
                        var result = _stockService.RegisterStockIn(wine, add);

                        result.IfError((err) => ConsoleUtil.SystemMessage(err.Message));
                    });

                    return MenuStatus.WAIT;
                }),
                new MenuOption("Registrar saída", () => {
                    _stockConsoleHelper.PrintStocks();
                    _wineConsoleHelper.WithExistingWine(wine =>
                    {
                        int minus = ConsoleUtil.PromptInt("Informe a quantidade a retirar: ");
                        var result = _stockService.RegisterStockOut(wine, minus);

                        result.IfError((err) => ConsoleUtil.SystemMessage(err.Message));
                    });

                    return MenuStatus.WAIT;
                }),
                new MenuOption("Visualizar estoque", () => {
                    _stockConsoleHelper.PrintStocks();

                    return MenuStatus.WAIT;
                })
            ]);
        }
    }
}
