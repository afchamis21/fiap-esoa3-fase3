using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Menu.Core;
using Fiap.Agnello.CLI.Application.Menu.Helpers;
using Fiap.Agnello.CLI.Application.Repository;
using Fiap.Agnello.CLI.Application.Services;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Pages
{
    internal class BrandAdminMenuFactory : IMenuFactory
    {
        public MenuLevel Build()
        {
            BrandService _brandService = new(Repositories.GetRepository<Brand, int>());
            BrandConsoleHelper _brandConsoleHelper = new(_brandService);
            return new MenuLevel("Marcas (ADMIN)", [
                new("Voltar para Home", MenuNavigator.Navigate<HomeMenuFactory>),
                new("Cadastrar", () => {
                    var dto = _brandConsoleHelper.PromptCreateDTO();

                    ConsoleUtil.SystemMessage("Salvando dados...");
                    var result = _brandService.Create(dto);

                    result
                        .IfError(err => ConsoleUtil.SystemMessage(err.Message))
                        .IfSuccess((brand) => ConsoleUtil.SystemMessage($"Marca cadastrada com sucesso com o ID [{brand.Id}]"));

                    return MenuStatus.WAIT;
                }),
                new("Editar", () => {
                    _brandConsoleHelper.WithExistingBrand(brand => {
                        var dto = _brandConsoleHelper.PromptUpdateDTO();

                        ConsoleUtil.SystemMessage("Salvando dados...");
                        var result = _brandService.Update(brand, dto);

                        result
                            .IfError(err => ConsoleUtil.SystemMessage(err.Message))
                            .IfSuccess((brand) => ConsoleUtil.SystemMessage($"Marca atualizada com sucesso com o ID [{brand.Id}]"));
                    });

                    return MenuStatus.WAIT;
                }),
                new("Deletar", () => {
                    int id = ConsoleUtil.PromptInt("Qualo ID da marca? ");
                    var result = _brandService.Delete(id);

                    result
                        .IfSuccess(() => ConsoleUtil.SystemMessage($"Marca com ID [{id}] deletada"))
                        .IfError((err) => ConsoleUtil.SystemMessage(err.Message));

                    return MenuStatus.WAIT;
                }),
                new("Listar", () => {
                    ConsoleUtil.SystemMessage("Buscando...");
                    var result = _brandService.GetAll();
                    result
                        .IfError(err => ConsoleUtil.SystemMessage(err.Message))
                        .IfSuccess(brands => {
                            if (brands.Count == 0) {
                                ConsoleUtil.SystemMessage("Nenuma marca para exibir...");
                                return;
                            }

                            brands.ForEach(Console.WriteLine);
                        });
                    
                    return MenuStatus.WAIT;
                }),
                new("Buscar por ID", () => {
                    _brandConsoleHelper.WithExistingBrand(brand => {
                        Console.WriteLine(brand);
                    });

                    return MenuStatus.WAIT;
                }),
            ]);
        }
    }
}
