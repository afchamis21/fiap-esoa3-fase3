using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.Application.Repository;
using Fiap.Agnello.CLI.Application.Services;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Adapter
{
    internal class WineServiceConsoleAdapter
    {
        private readonly WineService wineService = new(WineFileRepository.GetInstance());
        private readonly WineStockService wineStockService = new(WineFileRepository.GetInstance());

        private Wine? FindById()
        {
            int id = ConsoleUtil.PromptInt("Qual o ID do vinho? ");
            return wineService.FindById(id);
        }

        public void Create()
        {
            ConsoleUtil.SystemMessage("Digite os parametros solicitados");

            string name = ConsoleUtil.Prompt("Nome: ");
            float price = ConsoleUtil.PromptFloat("Preço: ");
            string maker = ConsoleUtil.Prompt("Fabricante: ");
            string grape = ConsoleUtil.Prompt("Uva: ");
            int year = ConsoleUtil.PromptInt("Ano: ");
            string country = ConsoleUtil.Prompt("País: ");
            WineDTO wineDTO = new(maker, name, country, grape, price, year);

            wineService.Create(wineDTO);
        }

        public void Update()
        {
            Wine? wine = FindById();
            if (wine == null)
            {
                return;
            }

            ConsoleUtil.SystemMessage("Digite os parametros solicitados (deixe em branco para não editar)");

            string maker = ConsoleUtil.Prompt("Fabricante: ");
            string name = ConsoleUtil.Prompt("Nome: ");
            string country = ConsoleUtil.Prompt("País: ");
            string grape = ConsoleUtil.Prompt("Uva: ");
            string price = ConsoleUtil.Prompt("Preço: ");
            string year = ConsoleUtil.Prompt("Ano: ");

            UpdateWineDTO wineDTO = new(maker, name, country, grape, price, year);

            wineService.Update(wine, wineDTO);
        }

        public void Delete()
        {
            int id = ConsoleUtil.PromptInt("Qual o ID do vinho a deletar? ");
            wineService.Delete(id);
        }

        public void PrintAll()
        {
            List<Wine> wines = wineService.GetAll();
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

        public void RegisterStockIn()
        {
            Wine? wine = FindById();
            if (wine == null)
            {
                return;
            }

            int add = ConsoleUtil.PromptInt("Informe a quantidade a adicionar: ");
            wineStockService.RegisterStockIn(wine, add);

        }

        public void RegisterStockOut()
        {
            Wine? wine = FindById();
            if (wine == null)
            {
                return;
            }

            int minus = ConsoleUtil.PromptInt("Informe a quantidade a retirar: ");
            wineStockService.RegisterStockOut(wine, minus);
        }

        public void PrintStocks()
        {
            List<Wine> wines = wineService.GetAll();
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
