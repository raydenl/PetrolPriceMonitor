using PetrolPriceMonitor.Enums;
using PetrolPriceMonitor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.Repositories
{
    public interface IStationRepository
    {
        Task<IEnumerable<Station>> GetStationsByFuelType(FuelType fuelType, params string[] stationId);
    }
}
