using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Menu.Core;
using Fiap.Agnello.CLI.Application.Services;
using Fiap.Agnello.CLI.Util;
using Fiap.Agnello.CLI.Application.Repository;
using Fiap.Agnello.CLI.Application.Menu.Helpers;

namespace Fiap.Agnello.CLI.Application.Menu.Pages
{
    /// <summary>
    /// Fábrica de menu responsável por construir o menu de administração de vinhos.
    /// Fornece opções administrativas como criação, edição, exclusão e visualização.
    /// </summary>
    internal class WineAdminMenuFactory  : IMenuFactory
    {
        /// <summary>
        /// Constrói e retorna o menu de administração de vinhos.
        /// </summary>
        /// <returns>Uma instância de <see cref="MenuLevel"/> com as opções disponíveis.</returns>
        public MenuLevel Build()
        {
            BrandService _brandService = new(Repositories.GetRepository<Brand, int>());
            WineService _wineService = new(Repositories.GetRepository<Wine, int>());

            BrandConsoleHelper _brandConsoleHelper = new(_brandService);
            WineConsoleHelper _adapter = new(_wineService, _brandConsoleHelper);

            return new("Vinhos (ADMIN)", [
                new MenuOption("Voltar para Home", MenuNavigator.Navigate<HomeMenuFactory>),
                new MenuOption("Cadastrar", () => {
                    CreateWineDTO wineDTO = _adapter.PromptCreateWineDTO();

                    ConsoleUtil.SystemMessage("Salvando dados...");
                    var result = _wineService.Create(wineDTO);

                    result
                        .IfSuccess((wine) => ConsoleUtil.SystemMessage($"Vinho salvo. ID [{wine.Id}]"))
                        .IfError(err => ConsoleUtil.SystemMessage(err.Message));                

                    return MenuStatus.WAIT;
                }),
                new MenuOption("Editar", () => {
                    _adapter.PrintAll();

                    _adapter.WithExistingWine(wine =>
                    {
                        UpdateWineDTO wineDTO = _adapter.PromptUpdateWineDTO();
                        var res = _wineService.Update(wine, wineDTO);
                        res
                            .IfSuccess(() => ConsoleUtil.SystemMessage($"Vinho salvo. ID [{wine.Id}]"))
                            .IfError((err) => ConsoleUtil.SystemMessage(err.Message));
                    });

                    return MenuStatus.WAIT;
                }),
                new MenuOption("Deletar", () => {
                    _adapter.PrintAll();

                    int id = ConsoleUtil.PromptInt("Qual o ID do vinho a deletar? ");
                    var result = _wineService.Delete(id);

                    result
                        .IfSuccess(() => ConsoleUtil.SystemMessage("Vinho deletado com sucesso!"))
                        .IfError((err) => ConsoleUtil.SystemMessage(err.Message));

                    return MenuStatus.WAIT;                
                }),
                new MenuOption("Listar", () => {
                    _adapter.PrintAll();

                    return MenuStatus.WAIT;
                }),
                new MenuOption("Buscar por ID", () => {
                    _adapter.WithExistingWine(wine =>
                    {
                        ConsoleUtil.SystemMessage("Vinho encontrado!");
                        Console.WriteLine("".PadLeft(4, ' ') + wine);
                    });
                    
                    return MenuStatus.WAIT;
                }),
            ]);
        }
    }
}
