using Newtonsoft.Json;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.Services
{
    public interface IConsume
    {
        Task<T> GetAsync<T>(string url, JsonSerializerSettings settings = null);
    }
}
