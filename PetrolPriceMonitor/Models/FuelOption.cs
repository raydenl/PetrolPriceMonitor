using PetrolPriceMonitor.Enums;

namespace PetrolPriceMonitor.Models
{
    public class FuelOption
    {
        public FuelType Type { protected set; get; }

        public decimal Price { protected set; get; }

        public FuelOption(int type, decimal price)
        {
            Type = (FuelType)type;
            Price = price;
        }
    }
}
