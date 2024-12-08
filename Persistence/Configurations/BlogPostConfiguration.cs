
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Persistence.Configurations
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            

            builder.Property(bp => bp.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(bp => bp.Content)
                .IsRequired();

            

            // Foreign key relationship with Car
            builder.HasOne(bp => bp.Car)
                .WithMany()
                .HasForeignKey(bp => bp.CarId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: defines delete behavior
        }
    }
}
