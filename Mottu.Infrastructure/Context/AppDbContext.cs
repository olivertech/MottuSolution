namespace Mottu.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Entidades
        /// </summary>
        public DbSet<AcceptedOrder> AcceptedOrders { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<CnhType> CnhTypes { get; set; }
        public DbSet<NotificatedUser> NotificatedUsers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<StatusOrder> StatusOrders { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<DeliveredOrder> DeliveredOrders { get; set; }

        /// <summary>
        /// Faz referencia as classes de configurações das entidades
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.UseIdentityAlwaysColumns();

            modelBuilder.ApplyConfiguration(new AcceptedOrderConfiguration());
            modelBuilder.ApplyConfiguration(new BikeConfiguration());
            modelBuilder.ApplyConfiguration(new CnhTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NotificatedUserConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new PlanConfiguration());
            modelBuilder.ApplyConfiguration(new RentalConfiguration());
            modelBuilder.ApplyConfiguration(new StatusOrderConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveredOrderConfiguration());
        }
    }
}
