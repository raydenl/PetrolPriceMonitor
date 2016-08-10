using System.Net;

namespace PetrolPriceMonitor.Models
{
    public interface IResponse
    {
        HttpStatusCode? HttpStatusCode { get; }

        string FriendlyErrorMessage { get; }
    }
}
