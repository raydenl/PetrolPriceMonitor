using PetrolPriceMonitor.Enums;
using System;
using System.Collections.Generic;

namespace PetrolPriceMonitor.Models
{
    public class Station
    {
        public Guid Id { protected set; get; }

        public StationType Type { protected set; get; }

        public string Name { protected set; get; }

        public string Address { protected set; get; }

        public string Phone { protected set; get; }
        
        public GeoPoint Location { protected set; get; }

        public List<FuelOption> FuelOptions { protected set; get; }
        
        public Station(Guid id, int type, string name, string address, string phone, double latitude, double longitude)
        {
            Id = id;
            Type = (StationType)type;
            Name = name;
            Address = address;
            Phone = phone;
            Location = new GeoPoint(latitude, longitude);

            FuelOptions = new List<FuelOption>();
        }
    }
}
