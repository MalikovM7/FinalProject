using FinalProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectMVC.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            

            builder.Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.Year)
                .IsRequired();

            builder.Property(c => c.Color)
                .HasMaxLength(30);

            builder.Property(c => c.Fueltype)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.LicensePlate)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.PricePerDay)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(c => c.IsAvailable)
                .IsRequired();

            builder.Property(c => c.ImagePath)
                .HasMaxLength(200);

            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.AvailabilityStart)
                .IsRequired();

            builder.Property(c => c.AvailabilityEnd)
                .IsRequired();
        }
    }
}