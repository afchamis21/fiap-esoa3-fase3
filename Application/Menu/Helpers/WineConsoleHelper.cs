using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.Application.Services;
using Fiap.Agnello.CLI.Application.Services.Helpers;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Helpers
{
    /// <summary>
    /// Adaptador para interações via console com o serviço de gerenciamento de vinhos.
    /// Oferece operações para criação, edição, remoção, listagem e controle de estoque.
    /// </summary>
    internal class WineConsoleHelper(WineService wineService, BrandConsoleHelper brandConsoleHelper)
    {
        /// <summary>
        /// Solicita ao usuário um ID e retorna o vinho correspondente.
        /// </summary>
        /// <returns>Instância de <see cref="Wine"/> ou <c>null</c> se não encontrado.</returns>
        private Result<Wine> FindById()
        {
            int id = ConsoleUtil.PromptInt("Qual o ID do vinho? ");
            return wineService.FindById(id);
        }

        /// <summary>
        /// Executa uma ação se o vinho existir, com base no ID informado pelo usuário.
        /// </summary>
        /// <param name="action">Ação a ser executada com o vinho encontrado.</param>
        public void WithExistingWine(Action<Wine> action)
        {
            var result = FindById();
            result.IfSuccess(action).IfError(err => ConsoleUtil.SystemMessage(err.Message));
        }

        /// <summary>
        /// Solicita os dados necessários para criar um novo vinho.
        /// </summary>
        /// <returns>Instância de <see cref="CreateWineDTO"/> preenchida com os dados do usuário.</returns>
        public CreateWineDTO PromptCreateWineDTO()
        {
            ConsoleUtil.SystemMessage("Digite os parametros solicitados");

            brandConsoleHelper.PrintAllBrands(); // TODO improve

            int makerId = ConsoleUtil.PromptInt("Id da marca: ");
            string name = ConsoleUtil.Prompt("Nome: ");
            decimal price = ConsoleUtil.PromptDecimal("Preço: ");
            string grape = ConsoleUtil.Prompt("Uva: ");
            int year = ConsoleUtil.PromptInt("Ano: ");

            return new CreateWineDTO(makerId, name, grape, price, year);
        }

        /// <summary>
        /// Solicita os dados opcionais para atualizar um vinho existente.
        /// </summary>
        /// <returns>Instância de <see cref="UpdateWineDTO"/> com os campos preenchidos ou vazios.</returns>
        public UpdateWineDTO PromptUpdateWineDTO()
        {
            brandConsoleHelper.PrintAllBrands();

            ConsoleUtil.SystemMessage("Digite os parametros solicitados (deixe em branco para não editar)");
            string maker = ConsoleUtil.Prompt("Id do fabricante: ");
            string name = ConsoleUtil.Prompt("Nome: ");
            string grape = ConsoleUtil.Prompt("Uva: ");
            string price = ConsoleUtil.Prompt("Preço: ");
            string year = ConsoleUtil.Prompt("Ano: ");

            return new UpdateWineDTO(maker, name, grape, price, year);
        }

        /// <summary>
        /// Exibe todos os vinhos cadastrados no sistema.
        /// </summary>
        public void PrintAll()
        {
            ConsoleUtil.SystemMessage("Buscando vinhos...");
            var result = wineService.GetAll();

            result
                .IfError((err) => ConsoleUtil.SystemMessage(err.Message))
                .IfSuccess((wines) =>
                {
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

                    Console.WriteLine();
                });
        }
    }

}
