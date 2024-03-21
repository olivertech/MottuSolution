using Mottu.Application.Messaging;
using Newtonsoft.Json;
using System.Text;

namespace Mottu.Api.Services
{
    /// <summary>
    /// Classe que chama controller 
    /// que vai enviar notificação do pedido
    /// para o broker de mensageria
    /// </summary>
    public class OrderNotificationService : IOrderNotificationService
    {
        public void OnOrderNotificationAsync(INotificationMessage notification)
        {
            try
            {
                var objAsJson = JsonConvert.SerializeObject(notification);
                var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
                var httpClient = new HttpClient();
                var response = httpClient.PostAsync("https://localhost:7297/api/PlaceOrderNotification/Send", content);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
