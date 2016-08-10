using System;

namespace PetrolPriceMonitor.Extensions
{
    public static class NumericalExtensions
    {
        public static double ToRadians(this double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
