using FinalProjectMVC.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectMVC.Configurations
{
    public class FAQConfiguration : IEntityTypeConfiguration<FAQ>
    {
        public void Configure(EntityTypeBuilder<FAQ> builder)
        {
            

            builder.Property(f => f.Question).IsRequired().HasMaxLength(200);
            builder.Property(f => f.Answer).IsRequired().HasMaxLength(500);
        }
    }
}
