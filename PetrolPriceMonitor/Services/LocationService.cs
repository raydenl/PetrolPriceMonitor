using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using PetrolPriceMonitor.Models;
using System;
using PetrolPriceMonitor.Extensions;

namespace PetrolPriceMonitor.Services
{
    public class LocationService : ILocate
    {
        private IGeolocator _locator;

        public LocationService()
        {
            _locator = CrossGeolocator.Current;
            _locator.AllowsBackgroundUpdates = false;
        }

        async public Task<GeoPoint> GetCurrentLocation()
        {
            try
            {
                var position = await Task.Run(() => new { Latitide = -42.45278d, Longitude = 171.209269d }); //await _locator.GetPositionAsync();

                await Task.Delay(TimeSpan.FromSeconds(5));

                return new GeoPoint(position.Latitide, position.Longitude);
            }
            catch (GeolocationException ex) when (ex.Error == GeolocationError.PositionUnavailable)
            {
                return null;
            }
            catch (GeolocationException ex) when (ex.Error == GeolocationError.Unauthorized)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets stations that lie within a kilometre radius of the current location.
        /// </summary>
        /// <param name="stations"></param>
        /// <param name="kilometreRadius"></param>
        /// <returns></returns>
        async public Task<IEnumerable<Station>> GetStationsWithinRadius(IEnumerable<Station> stations, int kilometreRadius)
        {
            try
            {
                var position = await Task.Run(() => new { Latitide = -42.45278d, Longitude = 171.209269d }); //await _locator.GetPositionAsync();

                await Task.Delay(TimeSpan.FromSeconds(5));

                var currentLocation = new GeoPoint(position.Latitide, position.Longitude);

                var stationsInRadius = new List<Station>();

                foreach(var station in stations)
                {
                    var distance = GetDistance(currentLocation, station.Location);

                    if (distance <= kilometreRadius)
                    {
                        stationsInRadius.Add(station);
                    }
                }

                return stationsInRadius;
            }
            catch (GeolocationException ex) when (ex.Error == GeolocationError.PositionUnavailable)
            {
                return Enumerable.Empty<Station>();
            }
            catch (GeolocationException ex) when (ex.Error == GeolocationError.Unauthorized)
            {
                return Enumerable.Empty<Station>();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Station>();
            }
        }

        /// <summary>
        /// Gets the distance in kilometres between two geo points.
        /// </summary>
        /// <param name="location1"></param>
        /// <param name="location2"></param>
        /// <returns></returns>
        private static double GetDistance(GeoPoint location1, GeoPoint location2)
        {
            const int earthRadius = 6371; // radius of earth in km

            return Math.Acos(
                Math.Sin(location1.Latitude.ToRadians()) * Math.Sin(location2.Latitude.ToRadians()) +
                Math.Cos(location1.Latitude.ToRadians()) * Math.Cos(location2.Latitude.ToRadians()) * Math.Cos(location2.Longitude.ToRadians() - location1.Longitude.ToRadians())) * earthRadius;
        }
    }
}
