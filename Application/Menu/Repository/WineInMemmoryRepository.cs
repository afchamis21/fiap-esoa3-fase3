using Fiap.Agnello.CLI.Application.Menu.Domain;

namespace Fiap.Agnello.CLI.Application.Menu.Repository
{
    internal class WineInMemmoryRepository : ICrudRepository<Wine, int>
    {
        private readonly Dictionary<int, Wine> _items = [];
        private int _count = 0;

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
            return _items.Remove(id);
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
            if (entity.Id == null)
            {
                return Create(entity);
            }

            return Update(entity);
        }
    }
}
