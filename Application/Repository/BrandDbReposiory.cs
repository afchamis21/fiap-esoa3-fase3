using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Repository.Contracts;
using Fiap.Agnello.CLI.DB;

namespace Fiap.Agnello.CLI.Application.Repository
{
    internal class BrandDbReposiory : ICrudRepository<Brand, int>
    {
        private readonly DatabaseContext _databaseContext = new();

        public bool Delete(int id)
        {
            var brand = _databaseContext.Brands.Find(id);
            if (brand is null)
            {
                return false;
            }

            _databaseContext.Brands.Remove(brand);
            _databaseContext.SaveChanges();
            return true;
        }

        public List<Brand> GetAll()
        {
            return [.._databaseContext.Brands];
        }

        public Brand? GetById(int id)
        {
            return _databaseContext.Brands.Find(id);
        }

        public Brand Create(Brand entity)
        {
            _databaseContext.Brands.Add(entity);

            _databaseContext.SaveChanges();

            return entity;
        }
        public Brand Update(Brand entity)
        {
            _databaseContext.Brands.Update(entity);

            _databaseContext.SaveChanges();

            return entity;
        }
    }
}
