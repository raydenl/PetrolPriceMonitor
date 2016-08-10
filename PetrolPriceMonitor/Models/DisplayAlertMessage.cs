using System;

namespace PetrolPriceMonitor.Models
{
    public class DisplayAlertMessage
    {
        public DisplayAlertMessage(string title, string message)
        {
            Title = title;
            Message = message;
            Cancel = "OK";
        }

        public string Title { get; private set; }
        public string Message { get; private set; }
        public string Cancel { get; set; }
        public string Accept { get; set; }

        public Action<bool> OnCompleted { get; set; }
    }
}
