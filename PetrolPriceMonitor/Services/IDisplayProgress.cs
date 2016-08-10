namespace PetrolPriceMonitor.Services
{
    public interface IDisplayProgress
    {
        void Show(string message);

        void Hide();
    }
}
