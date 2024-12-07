using FinalProjectMVC.Common;

namespace FinalProjectMVC.Models
{
    public class HomePreview : BaseEntity
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public bool IsSelected { get; set; }


    }
}
