using MassTransit;
using Microsoft.EntityFrameworkCore;
using Mottu.CrossCutting.Helpers;
using Mottu.CrossCutting.Messaging;
using Mottu.Domain.Entities;
using Mottu.Infrastructure.Context;
using Serilog;
using Serilog.Events;

namespace OrderConsumer.Classes
{
    public class OrderNotificationConsumer : IConsumer<NotificationMessage>, IDisposable
    {
        private AppDbContext? _dbContext;
        private DbSet<AppUser>? _users;
        private DbSet<Order>? _orders;
        private DbSet<Notification>? _notifications;
        private DbSet<AcceptedOrder>? _acceptedOrders;
        private DbSet<NotificatedUser>? _notificatedUsers;

        /// <summary>
        /// Classe Consumer que faz o tratamento das 
        /// mensagens enviadas pelo broker de mensageria
        /// e realiza as persistências necessárias
        /// para o negócio.
        /// 
        /// Para cada pedido postado, é criada uma notificação
        /// no sistema, que por sua vez, alimenta a tabela de
        /// usuários notificados, ou seja, somente aquele que 
        /// estão ativos na plataforma e que não estejam com um
        /// pedido aceito.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Consume(ConsumeContext<NotificationMessage> context)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logs/RabbitMQLogs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Entrando no Consumer para pedido de número '" + context.Message.Id + "' .");

            var connectionstring = "User ID=postgres;Password=12345;Host=localhost;Port=5432;Database=MottuDB;Pooling=true;";

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            optionsBuilder.UseNpgsql(connectionstring);

            _dbContext = new AppDbContext(optionsBuilder.Options);

            _users = _dbContext.Set<AppUser>();
            _orders = _dbContext.Set<Order>();
            _notifications = _dbContext.Set<Notification>();
            _acceptedOrders = _dbContext.Set<AcceptedOrder>();
            _notificatedUsers = _dbContext.Set<NotificatedUser>();

            try
            {
                var order = _orders.Where(x => x.Id == context!.Message.Id).FirstOrDefault();
                
                //Recupera a lista de usuários do tipo 'entregador' que estejam livres para serem notificados
                var listUsers = _users.Where(x => 
                                             x.IsActive == true && 
                                             x.IsDelivering == false && 
                                             x.UserType!.Name!.ToLower() == GetDescriptionFromEnum.GetFromUserTypeEnum(EnumUserType.Entregador).ToLower()).ToList();

                var newNotificationId = Guid.NewGuid();

                //Persiste notificação
                var newNotification = new Notification
                {
                    Id = newNotificationId,
                    NotificationDate = context.Message.DateOrder,
                    Order = order
                };

                var exist = _notifications.Where(x => x.Order!.Id == newNotification.Order!.Id).FirstOrDefault();

                if (exist == null)
                {
                    _notifications.Add(newNotification);
                    _dbContext.SaveChanges();

                    foreach (var user in listUsers!)
                    {
                        //Verifico se existe alguma ocorrência do usuário na tabela de pedidos aceitos no dia
                        var accetpedOrders = _acceptedOrders.Where(x => x.UserId == user.Id && x.AcceptedDate == DateOnly.FromDateTime(DateTime.Now)).FirstOrDefault();

                        // Se nulo indica que o usuário está livre
                        // então pode lançar o usuário na tabela de
                        // Notificated_User
                        if (accetpedOrders == null)
                        {
                            var newNotificatedUserId = Guid.NewGuid();

                            var notificatedUser = new NotificatedUser
                            {
                                Id = newNotificatedUserId,
                                NotificationDate = DateOnly.FromDateTime(DateTime.Now),
                                NotificationId = newNotification.Id,
                                UserId = user.Id
                            };

                            var existUser = _notificatedUsers.Where(x => x.UserId == user.Id && x.NotificationId == newNotificationId).FirstOrDefault();

                            if (existUser == null)
                            {
                                _notificatedUsers.Add(notificatedUser);
                            }
                        }
                    }

                    _dbContext.SaveChanges();
                }

                _dbContext.Dispose();
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                Log.Information("Saindo no Consumer.");
            }
        }

        public void Dispose()
        {
            _dbContext!.Dispose();
        }
    }
}
