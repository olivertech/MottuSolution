using Mottu.Api.Classes;
//using Mottu.Api.Interfaces;
using Mottu.CrossCutting.Messaging;
using Mottu.Domain.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace Mottu.Api.Services
{
    /// <summary>
    /// Classe que define serviço de chamada da controller 
    /// que vai processar a notificação do pedido
    /// </summary>
    public class OrderNotificationService
    {
        private readonly IHttpClientFactory? _httpClientFactory;
        private readonly IUnitOfWork _unitOfWork;

        public OrderNotificationService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> OnOrderNotification(NotificationMessage notification)
        {
            try
            {
                //var newNotification = new Domain.Entities.Notification
                //{
                //    NotificationDate = DateOnly.FromDateTime(DateTime.Now),
                //    Order = new Domain.Entities.Order
                //    {
                //        DateOrder = DateOnly.FromDateTime(DateTime.Now),
                //        Description = notification.Description,
                //        StatusOrder = _unitOfWork!.statusOrderRepository.GetList(x => x.Name!.ToLower() == "disponível").Result!.FirstOrDefault(),
                //        ValueOrder = notification.ValueOrder
                //    }
                //};

                var objAsJson = JsonConvert.SerializeObject(notification);
                var content = new StringContent(objAsJson, Encoding.UTF8, "application/json");
                var httpClient = new HttpClient();
                //var httpClient = _httpClientFactory!.CreateClient("OrderNotification");
                var response = await httpClient.PostAsync("https://localhost:7297/api/PlaceOrderNotification/Send", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
