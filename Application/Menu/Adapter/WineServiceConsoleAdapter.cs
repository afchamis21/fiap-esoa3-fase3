using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.Application.Menu.Core;
using Fiap.Agnello.CLI.Application.Repository;
using Fiap.Agnello.CLI.Application.Services;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Adapter
{
    /// <summary>
    /// Adaptador para interações via console com o serviço de gerenciamento de vinhos.
    /// Oferece operações para criação, edição, remoção, listagem e controle de estoque.
    /// </summary>
    internal class WineServiceConsoleAdapter
    {
        private readonly WineService wineService = new(WineFileRepository.GetInstance());
        private readonly WineStockService wineStockService = new(WineFileRepository.GetInstance());

        /// <summary>
        /// Solicita ao usuário um ID e retorna o vinho correspondente.
        /// </summary>
        /// <returns>Instância de <see cref="Wine"/> ou <c>null</c> se não encontrado.</returns>
        private Wine? FindById()
        {
            int id = ConsoleUtil.PromptInt("Qual o ID do vinho? ");
            Wine? wine = wineService.FindById(id);

            if (wine == null)
            {
                ConsoleUtil.SystemMessage($"Nenhum vinho encontrado para o id [{id}]");
            }

            return wine;
        }

        /// <summary>
        /// Executa uma ação se o vinho existir, com base no ID informado pelo usuário.
        /// </summary>
        /// <param name="action">Ação a ser executada com o vinho encontrado.</param>
        private void WithExistingWine(Action<Wine> action)
        {
            Wine? wine = FindById();
            if (wine != null)
            {
                action(wine);
            }
        }

        /// <summary>
        /// Solicita os dados necessários para criar um novo vinho.
        /// </summary>
        /// <returns>Instância de <see cref="WineDTO"/> preenchida com os dados do usuário.</returns>
        private static WineDTO PromptWineDTO()
        {
            string name = ConsoleUtil.Prompt("Nome: ");
            float price = ConsoleUtil.PromptFloat("Preço: ");
            string maker = ConsoleUtil.Prompt("Fabricante: ");
            string grape = ConsoleUtil.Prompt("Uva: ");
            int year = ConsoleUtil.PromptInt("Ano: ");
            string country = ConsoleUtil.Prompt("País: ");

            return new WineDTO(maker, name, country, grape, price, year);
        }

        /// <summary>
        /// Solicita os dados opcionais para atualizar um vinho existente.
        /// </summary>
        /// <returns>Instância de <see cref="UpdateWineDTO"/> com os campos preenchidos ou vazios.</returns>
        private static UpdateWineDTO PromptUpdateWineDTO()
        {
            string maker = ConsoleUtil.Prompt("Fabricante: ");
            string name = ConsoleUtil.Prompt("Nome: ");
            string country = ConsoleUtil.Prompt("País: ");
            string grape = ConsoleUtil.Prompt("Uva: ");
            string price = ConsoleUtil.Prompt("Preço: ");
            string year = ConsoleUtil.Prompt("Ano: ");

            return new UpdateWineDTO(maker, name, country, grape, price, year);
        }

        /// <summary>
        /// Cria um novo vinho com os dados informados pelo usuário.
        /// </summary>
        public MenuStatus Create()
        {
            ConsoleUtil.SystemMessage("Digite os parametros solicitados");
            WineDTO wineDTO = PromptWineDTO();

            ConsoleUtil.SystemMessage("Salvando dados...");
            Wine wine = wineService.Create(wineDTO);
            ConsoleUtil.SystemMessage($"Vinho salvo. ID [{wine.Id}]");

            return MenuStatus.WAIT;
        }

        /// <summary>
        /// Atualiza um vinho existente com os dados fornecidos pelo usuário.
        /// </summary>
        public MenuStatus Update()
        {
            WithExistingWine(wine =>
            {
                ConsoleUtil.SystemMessage("Digite os parametros solicitados (deixe em branco para não editar)");
                UpdateWineDTO wineDTO = PromptUpdateWineDTO();
                wineService.Update(wine, wineDTO);
                ConsoleUtil.SystemMessage($"Vinho salvo. ID [{wine.Id}]");
            });

            return MenuStatus.WAIT;
        }

        /// <summary>
        /// Remove um vinho do sistema com base no ID informado.
        /// </summary>
        public MenuStatus Delete()
        {
            int id = ConsoleUtil.PromptInt("Qual o ID do vinho a deletar? ");
            bool res = wineService.Delete(id);

            if (res)
            {
                ConsoleUtil.SystemMessage("Vinho deletado com sucesso!");
            }
            else
            {
                ConsoleUtil.SystemMessage("Erro deletando vinho...");
            }

            return MenuStatus.WAIT;
        }

        /// <summary>
        /// Exibe todos os vinhos cadastrados no sistema.
        /// </summary>
        public MenuStatus PrintAll()
        {
            List<Wine> wines = wineService.GetAll();
            if (wines.Count == 0)
            {
                ConsoleUtil.SystemMessage("Nenhum vinho para exibir");
                return MenuStatus.WAIT;
            }

            ConsoleUtil.SystemMessage("Vinhos: ");
            foreach (var wine in wines)
            {
                Console.WriteLine("".PadLeft(4, ' ') + wine);
            }

            return MenuStatus.WAIT;
        }

        /// <summary>
        /// Exibe um vinho específico com base no ID informado.
        /// </summary>
        public MenuStatus PrintById()
        {
            WithExistingWine(wine =>
            {
                ConsoleUtil.SystemMessage("Vinho encontrado!");
                Console.WriteLine("".PadLeft(4, ' ') + wine);
            });

            return MenuStatus.WAIT;
        }

        /// <summary>
        /// Adiciona quantidade ao estoque de um vinho.
        /// </summary>
        public MenuStatus RegisterStockIn()
        {
            WithExistingWine(wine =>
            {
                int add = ConsoleUtil.PromptInt("Informe a quantidade a adicionar: ");
                string? errorMessage = wineStockService.RegisterStockIn(wine, add);

                if (errorMessage != null)
                {
                    ConsoleUtil.SystemMessage(errorMessage);
                }
            });

            return MenuStatus.WAIT;
        }

        /// <summary>
        /// Remove quantidade do estoque de um vinho.
        /// </summary>
        public MenuStatus RegisterStockOut()
        {
            WithExistingWine(wine =>
            {
                int minus = ConsoleUtil.PromptInt("Informe a quantidade a retirar: ");
                string? errorMessage = wineStockService.RegisterStockOut(wine, minus);

                if (errorMessage != null)
                {
                    ConsoleUtil.SystemMessage(errorMessage);
                }
            });

            return MenuStatus.WAIT;
        }

        /// <summary>
        /// Exibe o estoque de todos os vinhos cadastrados.
        /// </summary>
        public MenuStatus PrintStocks()
        {
            List<Wine> wines = wineService.GetAll();
            if (wines.Count == 0)
            {
                ConsoleUtil.SystemMessage("Nenhum vinho para exibir");
                return MenuStatus.WAIT;
            }
            ConsoleUtil.SystemMessage("Estoque: ");
            Console.WriteLine($"\n{"ID",-5} | {"Nome",-20} | {"Estoque"}");
            Console.WriteLine(new String('-', 38));
            foreach (var wine in wines)
            {
                Console.WriteLine($"{wine.Id?.ToString().PadRight(5)} | {wine.Name,-20} | {wine.Stock}");
            }

            return MenuStatus.WAIT;
        }
    }

}
