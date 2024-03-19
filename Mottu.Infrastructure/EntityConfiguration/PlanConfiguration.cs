using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Mottu.Domain.Entities;

namespace Mottu.Infrastructure.EntityConfiguration
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasColumnName("Description").HasMaxLength(500);
            builder.Property(x => x.NumDays).HasColumnName("Num_Days").IsRequired();
            builder.Property(x => x.DailyValue).HasColumnName("Daily_Value").IsRequired();
            builder.Property(x => x.FinePercentage).HasColumnName("Fine_Percentage").IsRequired();
            builder.ToTable("Plan");

            //Seed
            builder.HasData(
                new Plan(Guid.NewGuid(), "Plano 7 dias", "Plano de locação de 7 dias", 7, 30.00f, 20),
                new Plan(Guid.NewGuid(), "Plano 15 dias", "Plano de locação de 15 dias", 15, 28.00f, 40),
                new Plan(Guid.NewGuid(), "Plano 30 dias", "Plano de locação de 30 dias", 30, 22.00f, 60));
        }
    }
}
