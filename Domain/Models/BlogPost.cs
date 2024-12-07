using FinalProjectMVC.Common;

namespace FinalProjectMVC.Models
{
    public class BlogPost : BaseEntity
    {
       
        public string Title { get; set; }
        public string Content { get; set; }
       
        public int CarId { get; set; } // Foreign key to Car

        // Navigation property
        public Car Car { get; set; }
    }
}
