using System.Globalization;
using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.Application.Repository;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Services
{
    /// <summary>
    /// Serviço responsável por interagir com o repositório de vinhos e lidar com a lógica de entrada do usuário.
    /// </summary>
    internal class WineService(IWineRepository wineRepository)
    {
        private readonly IWineRepository _repo = wineRepository;

        /// <summary>
        /// Cria um novo vinho a partir dos dados fornecidos pelo usuário via terminal.
        /// </summary>
        public Wine Create(WineDTO wineDTO)
        {
            return _repo.Save(Wine.FromDTO(wineDTO));
        }

        /// <summary>
        /// Solicita um ID ao usuário e busca o vinho correspondente.
        /// </summary>
        /// <returns>O vinho encontrado ou null se não existir.</returns>
        public Wine? FindById(int id)
        {
            Wine? wine = _repo.GetById(id);
            return wine;
        }

        public void Update(int id, UpdateWineDTO wineDTO)
        {
            Wine? wine = FindById(id);
            if (wine == null)
            {
                return;
            }

            Update(wine, wineDTO);
        }

        /// <summary>
        /// Atualiza os dados de um vinho existente com base em inputs do usuário.
        /// </summary>
        public void Update(Wine wine, UpdateWineDTO wineDTO)
        {
            if (!string.IsNullOrWhiteSpace(wineDTO.Name))
                wine.Name = wineDTO.Name;

            if (float.TryParse(wineDTO.Price?.Replace(".", ","), NumberStyles.Float, new CultureInfo("pt-BR"), out float price))
                wine.Price = price;
            
            if (!string.IsNullOrWhiteSpace(wineDTO.Maker))
                wine.Maker = wineDTO.Maker;

            if (!string.IsNullOrWhiteSpace(wineDTO.Grape))
                wine.Grape = wineDTO.Grape;

            if (int.TryParse(wineDTO.Year, out int year))
                wine.Year = year;

            if (!string.IsNullOrWhiteSpace(wineDTO.Country))
                wine.Country = wineDTO.Country;

            _repo.Save(wine);
        }

        /// <summary>
        /// Deleta um vinho a partir do ID fornecido pelo usuário.
        /// </summary>
        public bool Delete(int id)
        {
           return _repo.Delete(id);
        }

        /// <summary>
        /// Exibe todos os vinhos armazenados no repositório.
        /// </summary>
        public List<Wine> GetAll()
        {
            List<Wine> wines = _repo.GetAll();
            return wines;
        }
    }
}
