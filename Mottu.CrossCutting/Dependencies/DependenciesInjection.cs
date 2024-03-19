using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mottu.Api.Services;
using Mottu.CrossCutting.Messaging;
using Mottu.Domain.Interfaces;
using Mottu.Infrastructure.Context;
using Mottu.Infrastructure.Repositories;

namespace Mottu.CrossCutting.Dependencies
{
    /// <summary>
    /// Classe estática que concentra as configurações
    /// da conexão com o banco e os registros de injeções
    /// </summary>
    public static class DependenciesInjection
    {
        public static IServiceCollection AddDependenciesInjection(this IServiceCollection services, IConfiguration configuration) 
        {
            //Database configuration
            services.AddDbContext<AppDbContext>(options =>
                                                options.UseNpgsql(
                                                    configuration.GetConnectionString("DefaultConnection"))
                                                );

            //Services injections configuration
            services.AddScoped<IAcceptedOrderRepository, AcceptedOrderRepository>();
            services.AddScoped<IBikeRepository, BikeRepository>();
            services.AddScoped<ICnhTypeRepository, CnhTypeRepository>();
            services.AddScoped<INotificatedUserRepository, NotificatedUserRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IStatusOrderRepository, StatusOrderRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INotificationMessage, NotificationMessage>();
            services.AddScoped<IOrderNotificationService, OrderNotificationService>();
            services.AddScoped<IDeliveredOrderRepository, DeliveredOrderRepository>();

            return services;
        }
    }
}
