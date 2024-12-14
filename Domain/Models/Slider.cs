using Domain.Common;

namespace Domain.Models
{
    public class Slider : BaseEntity
    {
        public string Title { get; set; }

        public bool IsMain { get; set; } = false;

        public string Subtitle { get; set; }


    }
}
