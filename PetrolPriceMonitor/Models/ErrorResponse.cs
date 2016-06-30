using System.Net;

namespace PetrolPriceMonitor.Models
{
    public class ErrorResponse : IResponse
    {
        public HttpStatusCode? HttpStatusCode { get; private set; }

        public string FriendlyErrorMessage { get; private set; }
        
        public ErrorResponse(HttpStatusCode httpStatusCode, string friendlyErrorMessage)
        {
            HttpStatusCode = httpStatusCode;
            FriendlyErrorMessage = friendlyErrorMessage;
        }

        public ErrorResponse(string friendlyErrorMessage)
        {
            HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            FriendlyErrorMessage = friendlyErrorMessage;
        }
    }
}
