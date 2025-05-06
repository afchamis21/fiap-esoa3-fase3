using Fiap.Agnello.CLI.Application.Menu.Adapter;
using Fiap.Agnello.CLI.Application.Menu.Core;
using Fiap.Agnello.CLI.Application.Services;

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
            WineServiceConsoleAdapter service = new();
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
