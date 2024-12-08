

using Domain.Common;

namespace Domain.Models
{
    public class Statistic : BaseEntity
    {
       
        public string IconClass { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

    }
}
