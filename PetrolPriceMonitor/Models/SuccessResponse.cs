using System.Net;

namespace PetrolPriceMonitor.Models
{
    public class SuccessResponse : IResponse
    {
        public string FriendlyErrorMessage { get { return null; } }

        public HttpStatusCode? HttpStatusCode { get { return System.Net.HttpStatusCode.OK; } }
        
        public SuccessResponse() { }
    }
}
