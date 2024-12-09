using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {


            builder.ToTable("Cars");


            // Properties
            builder.Property(c => c.Brand)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Year)
                .IsRequired();

            builder.Property(c => c.Color)
                .HasMaxLength(50);

            builder.Property(c => c.Fueltype)
                .HasMaxLength(50);

            builder.Property(c => c.LicensePlate)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.PricePerDay)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.ImagePath)
                .HasMaxLength(250);

            builder.Property(c => c.Location)
                .HasMaxLength(200);

            // Relationships
            builder.HasOne(c => c.Category)
                .WithMany(cc => cc.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


              
        }
    }
}