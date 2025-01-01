using Domain.Common;

namespace Domain.Models
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
    }
}
