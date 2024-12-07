using FinalProjectMVC.Common;

namespace FinalProjectMVC.Models
{
    public class AboutUsViewModel : BaseEntity
    {
        
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public List<string> Points { get; set; }
        public List<string> ImageUrls { get; set; }




    }
}
