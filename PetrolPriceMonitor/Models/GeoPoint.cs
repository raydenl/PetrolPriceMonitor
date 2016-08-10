namespace PetrolPriceMonitor.Models
{
    public class GeoPoint
    {
        public double Latitude { protected set; get; }

        public double Longitude { protected set; get; }

        public GeoPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
