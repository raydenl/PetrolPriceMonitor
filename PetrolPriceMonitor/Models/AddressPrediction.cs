using Newtonsoft.Json;
using System.Collections.Generic;

namespace PetrolPriceMonitor.Models
{
    public class AddressPrediction
    {
        [JsonProperty("predictions")]
        public List<Prediction> Predictions { get; set; }
    }

    public class Prediction
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
    }
}
