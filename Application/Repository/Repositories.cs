using Fiap.Agnello.CLI.Application.Repository.Contracts;

namespace Fiap.Agnello.CLI.Application.Repository
{
    internal class Repositories
    {
        private static readonly Dictionary<Type, object> _factories = [];

        public static void RegisterRepository<TEntity, TId>(
            Func<ICrudRepository<TEntity, TId>> factory
        ) where TEntity : IEntity<TId>
        {
            _factories[typeof(TEntity)] = factory;
        }
        public static ICrudRepository<TEntity, TId> GetRepository<TEntity, TId>()
            where TEntity : IEntity<TId>
        {
            if (_factories.TryGetValue(typeof(TEntity), out var factory))
                return ((Func<ICrudRepository<TEntity, TId>>)factory)();

            throw new InvalidOperationException($"Repository not registered for type [{typeof(TEntity)}]");
        }
    }
}
