using Fiap.Agnello.CLI.Application.Menu.Pages;

namespace Fiap.Agnello.CLI.Application.Menu.Core
{
    /// <summary>
    /// Classe responsável pela navegação entre os diferentes menus da aplicação.
    /// Fornece métodos para alterar o menu atual e navegar entre diferentes páginas de menu.
    /// </summary>
    internal static class MenuNavigator
    {
        /// <summary>
        /// Propriedade que armazena o menu atual da aplicação.
        /// Inicializa com o menu inicial da aplicação (HomeMenu).
        /// </summary>
        public static MenuLevel Current { get; private set; } = new HomeMenuFactory().Build();

        /// <summary>
        /// Navega para o menu de tipo específico, criando uma nova instância da página de menu correspondente.
        /// O tipo da página de menu é determinado pelo parâmetro genérico <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Tipo da página de menu que implementa <see cref="IMenuFactory"/>.</typeparam>
        public static MenuStatus Navigate<T>() where T : IMenuFactory, new()
        {
            Current = new T().Build();
            return MenuStatus.PROCEED;
        }
    }
}
