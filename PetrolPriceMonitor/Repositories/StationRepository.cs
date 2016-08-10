using System.Collections.Generic;
using PetrolPriceMonitor.Models;
using System.Threading.Tasks;
using Amazon.CognitoIdentity;
using PetrolPriceMonitor.Constants;
using Amazon;
using Amazon.DynamoDBv2.DocumentModel;
using System.Linq;
using Amazon.DynamoDBv2;

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

        async public Task<IEnumerable<Station>> GetFavourites(params string[] stationId)
        {
            var station = Table.LoadTable(_client, TableName.Station);

            var batch = station.CreateBatchGet();

            stationId.ToList().All(k =>
            {
                batch.AddKey(k);
                return true;
            });

            batch.AttributesToGet = new List<string>
            {
                "StationId",
                "CompanyName",
                "StationName",
                "Price",
                "Latitude",
                "Longitude"
            };

            await batch.ExecuteAsync();
            
            return batch.Results.Select(MapToStation);
        }

        private Station MapToStation(Document source)
        {
            return new Station(source["StationId"].AsGuid(), source["CompanyName"], source["StationName"], source["Price"].AsDecimal(), source["Latitude"].AsDouble(), source["Longitude"].AsDouble());
        }
    }
}
