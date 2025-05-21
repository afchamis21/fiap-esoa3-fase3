using System.Globalization;
using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.Application.Repository.Contracts;
using Fiap.Agnello.CLI.Application.Services.Helpers;

namespace Fiap.Agnello.CLI.Application.Services
{
    /// <summary>
    /// Serviço responsável por interagir com o repositório de vinhos e lidar com a lógica de entrada do usuário.
    /// </summary>
    internal class WineService(ICrudRepository<Wine, int> wineRepository)
    {
        private readonly ICrudRepository<Wine, int> _repo = wineRepository;

        /// <summary>
        /// Cria um novo vinho a partir dos dados fornecidos pelo usuário via terminal.
        /// </summary>
        public Result<Wine> Create(CreateWineDTO wineDTO)
        {
            try
            {
                return Result<Wine>.Ok(_repo.Create(Wine.FromDTO(wineDTO)));
            } catch (Exception ex)
            {
                return Result<Wine>.Fail(new($"Erro cadastrando vinho: [{ex.Message}]"));
            }
        }

        /// <summary>
        /// Solicita um ID ao usuário e busca o vinho correspondente.
        /// </summary>
        /// <returns>O vinho encontrado ou null se não existir.</returns>
        public Result<Wine> FindById(int id)
        {
            Wine? wine = _repo.GetById(id);
            if (wine == null)
            {
                return Result<Wine>.Fail(new($"Nenhum vinho encontrado para o ID [{id}]"));
            }
            return Result<Wine>.Ok(wine);
        }

        public Result Update(int id, UpdateWineDTO wineDTO)
        {
            Result<Wine> result = FindById(id);
            if (!result.Success)
            {
                return Result.Fail(result.Error!);
            }

            Update(result.Value!, wineDTO);

            return Result.Ok();
        }

        /// <summary>
        /// Atualiza os dados de um vinho existente com base em inputs do usuário.
        /// </summary>
        public Result Update(Wine wine, UpdateWineDTO wineDTO)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(wineDTO.Name))
                    wine.Name = wineDTO.Name;

                if (decimal.TryParse(wineDTO.Price?.Replace(".", ","), NumberStyles.Float, new CultureInfo("pt-BR"), out decimal price))
                    wine.Price = price;

                if (int.TryParse(wineDTO.MakerId, out int makerId))
                {
                    wine.MakerId = makerId;
                    wine.Maker = null!;
                }

                if (!string.IsNullOrWhiteSpace(wineDTO.Grape))
                    wine.Grape = wineDTO.Grape;

                if (int.TryParse(wineDTO.Year, out int year))
                    wine.Year = year;

                _repo.Update(wine);
            } catch (Exception ex)
            {
                return Result.Fail(new($"Erro atualizando vinho: [{ex.Message}]"));
            }

            return Result.Ok();
        }

        /// <summary>
        /// Deleta um vinho a partir do ID fornecido pelo usuário.
        /// </summary>
        public Result Delete(int id)
        {
           var success = _repo.Delete(id);

            if (success)
            {
                return Result.Ok();
            }

            return Result.Fail(new BussinessError("Erro deletando vinho"));
        }

        /// <summary>
        /// Exibe todos os vinhos armazenados no repositório.
        /// </summary>
        public Result<List<Wine>> GetAll()
        {
            try
            {
                List<Wine> wines = _repo.GetAll();
                return Result<List<Wine>>.Ok(wines);
            } catch (Exception ex)
            {
                return Result<List<Wine>>.Fail(new($"Erro buscando vinhos [{ex.Message}]"));
            }
        }
    }
}
