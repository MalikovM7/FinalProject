using Domain.Models;
using Domain.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasOne(r => r.Car)
                .WithMany() 
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.AppUser)
                .WithMany() 
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(r => r.StartDate).IsRequired();
            builder.Property(r => r.EndDate).IsRequired();
            builder.Property(r => r.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
