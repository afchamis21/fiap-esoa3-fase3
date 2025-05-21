using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Repository.Contracts;
using Fiap.Agnello.CLI.db.Adapters;

namespace Fiap.Agnello.CLI.Application.Repository
{
    /// <summary>
    /// Repositório responsável por gerenciar objetos do tipo <see cref="Wine"/>.
    /// Implementa persistência em arquivo usando <see cref="FileDbAdapter{T, ID}"/>.
    /// </summary>
    internal class WineFileRepository : ICrudRepository<Wine, int>
    {
        private static readonly string DB_TABLE_NAME = "wine";
        private static WineFileRepository? instance;
        private readonly FileDbAdapter<Wine, int> fileDbAdapter = new(DB_TABLE_NAME);
        private readonly Dictionary<int, Wine> _items = [];
        private int _count = 1;

        /// <summary>
        /// Construtor privado. Carrega os dados do arquivo, se existirem.
        /// </summary>
        private WineFileRepository()
        {
            var savedItems = fileDbAdapter.LoadFromFile();
            if (savedItems != null)
            {
                _items = savedItems;
                _count = savedItems.Keys.Max() + 1;
            }
        }

        /// <summary>
        /// Retorna a instância singleton de <see cref="WineFileRepository"/>.
        /// </summary>
        public static WineFileRepository GetInstance()
        {
            instance ??= new WineFileRepository();
            return instance;
        }

        /// <summary>
        /// Cria um novo objeto <see cref="Wine"/> e adiciona ao repositório.
        /// </summary>
        public Wine Create(Wine entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entity.Id = _count;

            _items[(int)entity.Id] = entity;

            _count += 1;

            return entity;
        }

        /// <summary>
        /// Atualiza um <see cref="Wine"/> existente no repositório.
        /// </summary>
        public Wine Update(Wine entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            ArgumentNullException.ThrowIfNull(entity.Id);

            if (!_items.ContainsKey((int)entity.Id))
            {
                throw new ArgumentException("Wine not in repository");
            }
            _items[(int)entity.Id] = entity;

            return entity;
        }

        /// <summary>
        /// Remove um vinho com o ID especificado.
        /// </summary>
        /// <param name="id">ID do vinho a ser removido.</param>
        /// <returns>True se removido com sucesso, false caso contrário.</returns>

        public bool Delete(int id)
        {
            bool res = _items.Remove(id);
            Persist();
            return res;

        }

        /// <summary>
        /// Retorna uma lista com todos os vinhos armazenados.
        /// </summary>
        public List<Wine> GetAll()
        { 
            return [.. _items.Values];
        }

        /// <summary>
        /// Retorna um vinho a partir do ID.
        /// </summary>
        /// <param name="id">ID do vinho.</param>
        /// <returns>O vinho correspondente ou null se não encontrado.</returns>

        public Wine? GetById(int id)
        {
            _items.TryGetValue(id, out Wine? wine);

            return wine;
        }


        /// <summary>
        /// Persiste os dados em arquivo usando <see cref="fileDbAdapter"/>.
        /// </summary>
        private void Persist()
        {
            fileDbAdapter.SaveToFile(_items);
        }
    }
}
