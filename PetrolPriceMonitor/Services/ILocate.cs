using PetrolPriceMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.Services
{
    public interface ILocate
    {
        Task<IEnumerable<Station>> GetStationsWithinRadius(IEnumerable<Station> stations, int kilometreRadius);
    }
}
