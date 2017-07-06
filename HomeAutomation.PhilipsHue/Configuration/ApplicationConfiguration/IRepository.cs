using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeAutomation.PhilipsHue.Configuration.ApplicationConfiguration
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> Get(string key = null);

        Task Save(T input);

        Task Delete(string key = null);
    }
}