using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Repository;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Services
{
    /// <summary>
    /// Serviço responsável por gerenciar o estoque de vinhos.
    /// Permite registrar entradas, saídas e visualizar os estoques atuais.
    /// </summary>
    internal class WineStockService(IWineRepository wineRepository)
    {
        private readonly WineService _wineService = new(wineRepository);
        private readonly IWineRepository _repo = wineRepository;

        /// <summary>
        /// Registra a entrada de estoque (adiciona unidades a um vinho).
        /// </summary>
        public void RegisterStockIn(int id, int add)
        {
            Wine? wine = _wineService.FindById(id);
            if (wine == null) return;

            RegisterStockIn(wine, add);
        }

        public void RegisterStockIn(Wine wine, int add)
        {
            if (add <= 0)
            {
                ConsoleUtil.SystemMessage("A quantidade deve ser maior que 0!");
                return;
            }

            wine.Stock += add;
            _repo.Save(wine);
        }

        /// <summary>
        /// Registra a saída de estoque (remove unidades de um vinho).
        /// </summary>
        public void RegisterStockOut(int id, int minus)
        {
            Wine? wine = _wineService.FindById(id);
            if (wine == null) return;

            RegisterStockOut(wine, minus);
        }

        public void RegisterStockOut(Wine wine, int minus)
        {
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
    }
}
