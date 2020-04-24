using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CreativeCoders.SmartHal.Kernel.Base.Repositories
{
    [PublicAPI]
    public interface IRepositoryBase<T> : IEnumerable<T>
        where T : class
    {
        Task AddAsync(T item);

        Task RemoveAsync(T item);

        Task ClearAsync();
    }
}