
using Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Domain.Models
{
    public class Category : BaseEntity
    {
      
        [Required]
        public string? Name { get; set; }

       
        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
