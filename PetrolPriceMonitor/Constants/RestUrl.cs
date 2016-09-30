namespace PetrolPriceMonitor.Constants
{
    public class RestUrl
    {
        public const string GooglePlaceAutocompleteUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json?input={0}&types={1}&language={2}&components={3}&key={4}";

        public static string GooglePlaceAutocomplete(string input, string types, string language, string components, string key)
        {
            return string.Format(GooglePlaceAutocompleteUrl, input, types, language, components, key);
        }
    }
}
