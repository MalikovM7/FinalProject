using FinalProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProjectMVC.Data.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

           

            // Configure properties
            builder.Property(r => r.StartDate)
                .IsRequired();

            builder.Property(r => r.EndDate)
                .IsRequired();

            builder.Property(r => r.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(r => r.Status)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}