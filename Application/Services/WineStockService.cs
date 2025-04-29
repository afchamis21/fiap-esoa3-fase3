using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Repository;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Services
{
    internal class WineStockService
    {
        private readonly WineService _wineService = new();
        private readonly WineRepository _repo = WineRepository.GetInstance();
        public void RegisterStockIn()
        {
            Wine? wine = _wineService.FindById();
            if (wine == null) return;

            int add = ConsoleUtil.PromptInt("Informe a quantidade a adicionar: ");
            if (add <= 0)
            {
                ConsoleUtil.SystemMessage("A quantidade deve ser maior que 0!");
                return;
            }

            wine.Stock += add;
            _repo.Save(wine);
        }

        public void RegisterStockOut()
        {
            Wine? wine = _wineService.FindById();
            if (wine == null) return;

            int minus = ConsoleUtil.PromptInt("Informe a quantidade a retirar: ");
            if (minus <= 0)
            {
                ConsoleUtil.SystemMessage("A quantidade deve ser maior que 0!");
                return;
            }

            if (minus > wine.Stock)
            {
                ConsoleUtil.SystemMessage("A retirada não pode ser maior que o estoque atual!");
                return;
            }

            wine.Stock -= minus;
            _repo.Save(wine);
        }

        public void PrintStocks()
        {
            List<Wine> wines = _repo.GetAll();
            if (wines.Count == 0)
            {
                ConsoleUtil.SystemMessage("Nenhum vinho para exibir");
                return;
            }

            ConsoleUtil.SystemMessage("Estoque: ");
            foreach (var wine in wines)
            {
                Console.WriteLine($"{wine.Id?.ToString().PadRight(5)} | {wine.Name,-20} | {wine.Stock}");
            }
        }
    }
}
