namespace Mottu.Infrastructure.EntityConfiguration
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CreationDate).HasColumnName("Creation_Date").IsRequired();
            builder.Property(x => x.PredictionDate).HasColumnName("Predition_Date").IsRequired();
            builder.Property(x => x.EndDate).HasColumnName("End_Date").IsRequired();
            builder.Property(x => x.InitialDate).HasColumnName("Initial_Date").IsRequired();
            builder.Property(x => x.TotalValue).HasColumnName("Total_Value").IsRequired();
            builder.Property(x => x.NumMoreDailys).HasColumnName("Num_More_Dailys");
            builder.Property(x => x.IsActive).HasColumnName("Is_Active");
            builder.HasOne(x => x.Bike);
            builder.ToTable("Rental");
        }
    }
}
