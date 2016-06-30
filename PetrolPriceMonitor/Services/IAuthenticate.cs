using PetrolPriceMonitor.Models;
using System.Threading.Tasks;

namespace PetrolPriceMonitor.Services
{
    public interface IAuthenticate
    {
        Task<IResponse> LogInUsingFacebook();

        Task<IResponse> LogInAsGuest();

        Task<IResponse> LogIn(string email, string password);

        void LogOut();

        Task<IResponse> SignUp(string email, string password);

        bool IsAuthenticated { get; }
    }
}
