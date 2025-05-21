using Fiap.Agnello.CLI.Application.Domain;
using Fiap.Agnello.CLI.Application.Repository.Contracts;
using Fiap.Agnello.CLI.DB;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Agnello.CLI.Application.Repository
{
    internal class WineDbRepository : ICrudRepository<Wine, int>
    {
        private readonly DatabaseContext dbContext = new();

        public bool Delete(int id)
        {
            var wine = dbContext.Wines.Find(id);
            if (wine is null)
            {
                return false;
            }

            dbContext.Wines.Remove(wine);
            dbContext.SaveChanges();

            return true;
        }

        public List<Wine> GetAll()
        {
            return [.. dbContext.Wines.Include(w => w.Maker)];
        }

        public Wine? GetById(int id)
        {
            return dbContext.Wines.Include(w => w.Maker).FirstOrDefault(w => w.Id == id);
        }

        public Wine Create(Wine entity)
        {
            dbContext.Wines.Add(entity);

            dbContext.SaveChanges();

            return entity;
        }

        public Wine Update(Wine entity)
        {
            dbContext.Update(entity);

            dbContext.SaveChanges();

            return entity;
        }
    }
}
