using System.Text.Json;
using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.db.Adapters;

namespace Fiap.Agnello.CLI.Application.Repository
{
    internal class WineRepository : ICrudRepository<Wine, int>
    {
        private static readonly string DB_FILE_NAME = "db.json";
        private static WineRepository? instance;
        private readonly FileDbAdapter<Wine, int> fileDbAdapter = new(DB_FILE_NAME);
        private readonly Dictionary<int, Wine> _items = [];
        private int _count = 1;
        
        private WineRepository()
        {
            var savedItems = fileDbAdapter.LoadFromFile();
            if (savedItems != null)
            {
                _items = savedItems;
                _count = savedItems.Count + 1;
            }
        }

        public static WineRepository GetInstance()
        {
            instance ??= new WineRepository();
            return instance;
        }

        private Wine Create(Wine entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entity.Id = _count;

            _items[(int)entity.Id] = entity;

            _count += 1;

            return entity;
        }
        private Wine Update(Wine entity)
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

        public bool Delete(int id)
        {
            bool res = _items.Remove(id);
            Persist();
            return res;

        }

        public List<Wine> GetAll()
        { 
            return [.. _items.Values];
        }

        public Wine? GetById(int id)
        {
            _items.TryGetValue(id, out Wine? wine);

            return wine;
        }

        public Wine Save(Wine entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            Wine wine;
            if (entity.Id == null)
            {
                wine = Create(entity);
            } else
            {
                wine = Update(entity);
            }

            Persist();
            return wine;
        }

        private void Persist()
        {
            fileDbAdapter.SaveToFile(_items);
        }
    }
}
