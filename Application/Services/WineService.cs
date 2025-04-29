using System.Globalization;
using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Repository;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Services
{
    internal class WineService
    {
        private readonly WineRepository _repo = WineRepository.GetInstance();
        public void Create()
        {
            ConsoleUtil.SystemMessage("Digite os parametros solicitados");

            string name = ConsoleUtil.Prompt("Nome: ");
            float price = ConsoleUtil.PromptFloat("Preço: ");
            string maker = ConsoleUtil.Prompt("Fabricante: ");
            string grape = ConsoleUtil.Prompt("Uva: ");
            int year = ConsoleUtil.PromptInt("Ano: ");
            string country = ConsoleUtil.Prompt("País: ");
            Wine wine = new(maker, name, country, grape, price, year);

            ConsoleUtil.SystemMessage("Salvando dados...");
            wine = _repo.Save(wine);
            ConsoleUtil.SystemMessage($"Vinho salvo. ID [{wine.Id}]");
        }

        public Wine? FindById()
        {
            int id = ConsoleUtil.PromptInt("Qual o ID do vinho a editar? ");
            Wine? wine = _repo.GetById(id);
            if (wine == null)
            {
                ConsoleUtil.SystemMessage($"Nenhum vinho encontrado para o id [{id}]");
            }
            return wine;
        }

        public void Update()
        {
            Wine? wine = FindById();
            if (wine == null)
            {
                return;
            }

            ConsoleUtil.SystemMessage("Digite os parametros solicitados (deixe em branco para não editar)");

            string name = ConsoleUtil.Prompt("Nome: ");
            string priceInput = ConsoleUtil.Prompt("Preço: ");
            string maker = ConsoleUtil.Prompt("Fabricante: ");
            string grape = ConsoleUtil.Prompt("Uva: ");
            string yearInput = ConsoleUtil.Prompt("Ano: ");
            string country = ConsoleUtil.Prompt("País: ");

            if (!string.IsNullOrWhiteSpace(name))
                wine.Name = name;

            if (float.TryParse(priceInput?.Replace(".", ","), NumberStyles.Float, new CultureInfo("pt-BR"), out float price))
                wine.Price = price;
            
            if (!string.IsNullOrWhiteSpace(maker))
                wine.Maker = maker;

            if (!string.IsNullOrWhiteSpace(grape))
                wine.Grape = grape;

            if (int.TryParse(yearInput, out int year))
                wine.Year = year;

            if (!string.IsNullOrWhiteSpace(country))
                wine.Country = country;

            _repo.Save(wine);
            ConsoleUtil.SystemMessage($"Vinho salvo. ID [{wine.Id}]");
        }

        public void Delete()
        {
            int id = ConsoleUtil.PromptInt("Qual o ID do vinho a deletar? ");
            _repo.Delete(id);
            ConsoleUtil.SystemMessage("Vinho deletado com sucesso!");
        }

        public void PrintAll()
        {
            List<Wine> wines = _repo.GetAll();
            if (wines.Count == 0)
            {
                ConsoleUtil.SystemMessage("Nenhum vinho para exibir");
                return;
            }

            ConsoleUtil.SystemMessage("Vinhos: ");
            foreach (var wine in wines)
            {
                Console.WriteLine("".PadLeft(4, ' ') + wine);
            }
        }

        public void PrintById()
        {
            Wine? wine = FindById();
            if (wine == null)
            {
                return;
            }

            ConsoleUtil.SystemMessage("Vinho encontrado!");
            Console.WriteLine("".PadLeft(4, ' ') + wine);
        }
    }
}
