namespace Mottu.Application.Dependencies
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

            //Repository injections
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
            services.AddScoped<IDeliveredOrderRepository, DeliveredOrderRepository>();

            //Service injections
            services.AddScoped<IAcceptedOrderService, AcceptedOrderService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IBikeService, BikeService>();
            services.AddScoped<ICnhTypeService, CnhTypeService>();
            services.AddScoped<IDeliveredOrderService, DeliveredOrderService>();
            services.AddScoped<INotificatedUserService, NotificatedUserService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IStatusOrderService, StatusOrderService>();
            services.AddScoped<IUserTypeService, UserTypeService>();
            services.AddScoped<IOrderNotificationService, OrderNotificationService>();

            return services;
        }
    }
}
