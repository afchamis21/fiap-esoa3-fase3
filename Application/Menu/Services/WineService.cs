using Fiap.Agnello.CLI.Application.Menu.Domain;
using Fiap.Agnello.CLI.Application.Menu.Repository;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Services
{
    internal class WineService
    {
        private readonly WineInMemmoryRepository repo = new();
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
            wine = repo.Save(wine);
            ConsoleUtil.SystemMessage($"Vinho salvo. ID [{wine.Id}]");
        }

        public void Update()
        {
            int id = ConsoleUtil.PromptInt("Qual o ID do vinho a editar? ");
            Wine? wine = repo.GetById(id);
            if (wine == null)
            {
                ConsoleUtil.SystemMessage($"Nenhum vinho encontrado para o id [{id}]");
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

            if (float.TryParse(priceInput, out float price))
                wine.Price = price;

            if (!string.IsNullOrWhiteSpace(maker))
                wine.Maker = maker;

            if (!string.IsNullOrWhiteSpace(grape))
                wine.Grape = grape;

            if (int.TryParse(yearInput, out int year))
                wine.Year = year;

            if (!string.IsNullOrWhiteSpace(country))
                wine.Country = country;

            repo.Save(wine);
            ConsoleUtil.SystemMessage($"Vinho salvo. ID [{wine.Id}]");
        }

        public void Delete()
        {
            int id = ConsoleUtil.PromptInt("Qual o ID do vinho a deletar? ");
            repo.Delete(id);
            ConsoleUtil.SystemMessage("Vinho deletado com sucesso!");
        }

        public void PrintAll()
        {
            List<Wine> wines = repo.GetAll();
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
            int id = ConsoleUtil.PromptInt("Qual o ID do vinho? ");
            Wine? wine = repo.GetById(id);
            if (wine == null)
            {
                ConsoleUtil.SystemMessage("Vinho não encontrado!");
                return;
            }

            ConsoleUtil.SystemMessage("Vinho encontrado!");
            Console.WriteLine("".PadLeft(4, ' ') + wine);
        }
    }
}
