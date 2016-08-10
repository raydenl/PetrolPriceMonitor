using PetrolPriceMonitor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.Repositories
{
    public interface IStationRepository
    {
        Task<IEnumerable<Station>> GetFavourites(params string[] stationId);
    }
}
