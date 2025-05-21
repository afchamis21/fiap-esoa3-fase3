using Fiap.Agnello.CLI.Application.Services;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Helpers
{
    internal class WineStockConsoleHelper(WineService wineService)
    {

        /// <summary>
        /// Exibe o estoque de todos os vinhos cadastrados.
        /// </summary>
        public void PrintStocks()
        {
            var result = wineService.GetAll();

            result
                .IfError(err => ConsoleUtil.SystemMessage(err.Message))
                .IfSuccess(wines =>
                {
                    if (wines.Count == 0)
                    {
                        ConsoleUtil.SystemMessage("Nenhum vinho para exibir");
                        return;
                    }
                    ConsoleUtil.SystemMessage("Estoque: ");
                    Console.WriteLine($"\n{"ID",-5} | {"Nome",-20} | {"Estoque"}");
                    Console.WriteLine(new String('-', 38));
                    foreach (var wine in wines)
                    {
                        Console.WriteLine($"{wine.Id,-5} | {wine.Name,-20} | {wine.Stock}");
                    }

                    Console.WriteLine();
                });
        }
    }
}
