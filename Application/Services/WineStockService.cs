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

        public string? RegisterStockIn(Wine wine, int add)
        {
            if (add <= 0)
            {
                return "A quantidade deve ser maior que 0!";
            }

            wine.Stock += add;
            _repo.Save(wine);

            return null;
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

        public string? RegisterStockOut(Wine wine, int minus)
        {
            if (minus <= 0)
            {
                return "A quantidade deve ser maior que 0!";
            }

            if (minus > wine.Stock)
            {
                return "A retirada não pode ser maior que o estoque atual!";
            }

            wine.Stock -= minus;
            _repo.Save(wine);

            return null;
        }
    }
}
