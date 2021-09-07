using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CreativeCoders.Core;
using CreativeCoders.Core.Threading;

namespace CreativeCoders.SmartHal.Kernel.Base.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        private readonly IList<T> _items;

        protected RepositoryBase()
        {
            _items = new ConcurrentList<T>();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Task AddAsync(T item)
        {
            AddItem(item);
            
            return Task.CompletedTask;
        }

        protected virtual void AddItem(T item)
        {
            _items.Add(item);
        }

        public async Task RemoveAsync(T item)
        {
            await item.TryDisposeAsync().ConfigureAwait(false);

            RemoveItem(item);
        }

        protected virtual void RemoveItem(T item)
        {
            _items.Remove(item);
        }

        public async Task ClearAsync()
        {
            await _items.ForEachAsync(RemoveAsync).ConfigureAwait(false);
        }
    }
}