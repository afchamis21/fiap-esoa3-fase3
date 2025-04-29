using Fiap.Agnello.CLI.Application.Menu.Core;
using Fiap.Agnello.CLI.Application.Menu.Services;

namespace Fiap.Agnello.CLI.Application.Menu.Pages
{
    internal class WineAdminMenuFactory  : IMenuFactory
    {
        public MenuLevel Build()
        {
            WineService service = new();
            return new("Vinhos (ADMIN)", [
                new MenuOption("Voltar para Home", MenuNavigator.Navigate<HomeMenuFactory>),
                new MenuOption("Cadastrar vinho", service.Create),
                new MenuOption("Editar vinho", service.Update),
                new MenuOption("Deletar vinho", service.Delete),
                new MenuOption("Listar todos os vinhos", service.PrintAll),
                new MenuOption("Buscar por ID", service.PrintById),
                ]);
        }
    }
}
