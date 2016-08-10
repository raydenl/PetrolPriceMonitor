using System;

namespace PetrolPriceMonitor.Models
{
    public class Station
    {
        public Guid Id { protected set; get; }

        public string CompanyName { protected set; get; }

        public string StationName { protected set; get; }

        public decimal Price { protected set; get; }

        public GeoPoint Location { protected set; get; }
        
        public Station(Guid id, string companyName, string stationName, decimal price, double latitude, double longitude)
        {
            Id = id;
            CompanyName = companyName;
            StationName = stationName;
            Price = price;
            Location = new GeoPoint(latitude, longitude);
        }
    }
}
