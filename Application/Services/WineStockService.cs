using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Repository.Contracts;
using Fiap.Agnello.CLI.Application.Services.Helpers;

namespace Fiap.Agnello.CLI.Application.Services
{
    /// <summary>
    /// Serviço responsável por gerenciar o estoque de vinhos.
    /// Permite registrar entradas, saídas e visualizar os estoques atuais.
    /// </summary>
    internal class WineStockService(ICrudRepository<Wine, int> wineRepository)
    {
        private readonly WineService _wineService = new(wineRepository);
        private readonly ICrudRepository<Wine, int> _repo = wineRepository;

        /// <summary>
        /// Registra a entrada de estoque (adiciona unidades a um vinho).
        /// </summary>
        public Result RegisterStockIn(int id, int add)
        {
            var result = _wineService.FindById(id);
            if (!result.Success) return Result.Fail(result.Error!);

            return RegisterStockIn(result.Value!, add);
        }

        public Result RegisterStockIn(Wine wine, int add)
        {
            if (add <= 0)
            {
                return Result.Fail(new("A quantidade deve ser maior que 0!"));
            }

            wine.Stock += add;
            _repo.Update(wine);

            return Result.Ok();
        }

        /// <summary>
        /// Registra a saída de estoque (remove unidades de um vinho).
        /// </summary>
        public Result RegisterStockOut(int id, int minus)
        {
            var result = _wineService.FindById(id);
            if (!result.Success) return Result.Fail(result.Error!);

            return RegisterStockOut(result.Value!, minus);
        }

        public Result RegisterStockOut(Wine wine, int minus)
        {
            if (minus <= 0)
            {
                return Result.Fail(new("A quantidade deve ser maior que 0!"));
            }

            if (minus > wine.Stock)
            {
                return Result.Fail(new("A retirada não pode ser maior que o estoque atual!"));
            }

            wine.Stock -= minus;
            _repo.Update(wine);

            return Result.Ok();
        }
    }
}
