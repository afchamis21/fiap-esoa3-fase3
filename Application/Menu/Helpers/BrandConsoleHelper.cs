using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Domain.DTO;
using Fiap.Agnello.CLI.Application.Services;
using Fiap.Agnello.CLI.Util;

namespace Fiap.Agnello.CLI.Application.Menu.Helpers
{
    internal class BrandConsoleHelper(BrandService brandService)
    {
        public void WithExistingBrand(Action<Brand> action)
        {
            int id = ConsoleUtil.PromptInt("Qual O ID da marca? ");
            var result = brandService.FindById(id);
            result
                .IfSuccess(action)
                .IfError(err => ConsoleUtil.SystemMessage(err.Message));
        }

        public void PrintAllBrands()
        {
            ConsoleUtil.SystemMessage("Buscando marcas...");
            var result = brandService.GetAll();

            result
                .IfError(err => ConsoleUtil.SystemMessage(err.Message))
                .IfSuccess(brands =>
                {
                    if (brands.Count == 0)
                    {
                        ConsoleUtil.SystemMessage("Nenhuma marca para exibir");
                        return;
                    }

                    ConsoleUtil.SystemMessage("Marcas:");
                    foreach (var brand in brands)
                    {
                        Console.WriteLine("".PadLeft(4, ' ') + brand);
                    }

                    Console.WriteLine();
                });
        }

        public CreateBrandDTO PromptCreateDTO() 
        {
            ConsoleUtil.SystemMessage("Digite os parametros solicitados");

            string name = ConsoleUtil.Prompt("Nome: ");
            string country = ConsoleUtil.Prompt("País: ");
 
            return new CreateBrandDTO(name, country);
        }
        public UpdateBrandDTO PromptUpdateDTO() {
            ConsoleUtil.SystemMessage("Digite os parametros solicitados (deixe em branco para não editar)");

            string name = ConsoleUtil.Prompt("Id da marca: ");
            string country = ConsoleUtil.Prompt("Nome: ");

            return new UpdateBrandDTO(name, country);
        }
    }
}
