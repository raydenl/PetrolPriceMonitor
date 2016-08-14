using System.Collections.Generic;
using PetrolPriceMonitor.Models;
using System.Threading.Tasks;
using Amazon.CognitoIdentity;
using PetrolPriceMonitor.Constants;
using Amazon;
using Amazon.DynamoDBv2.DocumentModel;
using System.Linq;
using Amazon.DynamoDBv2;
using PetrolPriceMonitor.Enums;

namespace PetrolPriceMonitor.Repositories
{
    public class StationRepository : IStationRepository
    {
        private AmazonDynamoDBClient _client;

        public StationRepository()
        {
            var credentials = new CognitoAWSCredentials(
                Authentication.IdentityPoolId,
                RegionEndpoint.USEast1
            );

            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig();
            config.RegionEndpoint = RegionEndpoint.USEast1;

            _client = new AmazonDynamoDBClient(credentials, config);
        }

        async public Task<IEnumerable<Station>> GetStationsByFuelType(FuelType fuelType, params string[] stationId)
        {
            var stations = GetStations(stationId);
            var stationFuelOptions = GetStationFuelOptions(fuelType, stationId);

            var awaitedStations = (await stations);
            var awaitedStationFuelOptions = (await stationFuelOptions);

            return awaitedStations.GroupJoin(awaitedStationFuelOptions,
                s => s["Id"],
                sfo => sfo["StationId"],
                (s, options) =>
                {
                    var station = MapToStation(s);

                    foreach (var option in options)
                    {
                        var fuelOption = MapToFuelOption(option);

                        station.FuelOptions.Add(fuelOption);
                    }

                    return station;
                });
        }

        async private Task<List<Document>> GetStations(params string[] stationId)
        {
            var station = Table.LoadTable(_client, TableName.Station);

            var batch = station.CreateBatchGet();

            stationId.ToList().All(k =>
            {
                batch.AddKey(k);
                return true;
            });

            batch.ConsistentRead = true;
            batch.AttributesToGet = new List<string>
            {
                "Id",
                "Type",
                "Name",
                "Address",
                "Phone",
                "Lat",
                "Lng"
            };

            await batch.ExecuteAsync();

            return batch.Results;
        }

        async private Task<List<Document>> GetStationFuelOptions(FuelType fuelType, params string[] stationId)
        {
            var fuel = Table.LoadTable(_client, TableName.FuelOption);

            var batch = fuel.CreateBatchGet();

            stationId.ToList().All(k =>
            {
                batch.AddKey(k, (int)fuelType);
                return true;
            });

            batch.ConsistentRead = true;
            batch.AttributesToGet = new List<string>
            {
                "StationId",
                "Type",
                "Price"
            };

            await batch.ExecuteAsync();

            return batch.Results;
        }

        private Station MapToStation(Document station)
        {
            return new Station(
                station["Id"].AsGuid(), 
                station["Type"].AsInt(), 
                station["Name"], 
                station["Address"], 
                station["Phone"], 
                station["Lat"].AsDouble(), 
                station["Lng"].AsDouble());
        }

        private FuelOption MapToFuelOption(Document fuelOption)
        {
            return new FuelOption(
                fuelOption["Type"].AsInt(),
                fuelOption["Price"].AsDecimal());
        }
    }
}
