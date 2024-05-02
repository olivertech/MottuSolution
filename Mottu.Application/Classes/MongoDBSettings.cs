using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Application.Classes
{
    /// <summary>
    /// Classe que é carregada com os valores definidos no
    /// arquivo appsettings.json do projeto API.
    /// </summary>
    public class MongoDBSettings
    {
        //MongoDB Collections
        public string AcceptedOrderCollectionName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
        public string BikeCollectionName { get; set; } = null!;
        public string CnkTypeCollectionName { get; set; } = null!;
        public string DeliveredOrderCollectionName { get; set; } = null!;
        public string NotificatedUserCollectionName { get; set; } = null!;
        public string NotificationCollectionName { get; set; } = null!;
        public string OrderCollectionName { get; set; } = null!;
        public string PlanCollectionName { get; set; } = null!;
        public string RentalCollectionName { get; set; } = null!;
        public string StatusOrderCollectionName { get; set; } = null!;
        public string UserTypeCollectionName { get; set; } = null!;

        //MongoDB ConnectionString
        public string ConnectionString { get; set; } = null!;

        //MongoDB DatabaseName
        public string DatabaseName { get; set; } = null!;

    }
}
