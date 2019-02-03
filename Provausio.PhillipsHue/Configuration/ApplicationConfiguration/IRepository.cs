using System.Collections.Generic;
using System.Threading.Tasks;

namespace Provausio.PhillipsHue.Configuration.ApplicationConfiguration
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(string key = null);

        Task SaveAsync(T input);

        Task DeleteAsync(string key = null);
    }
}