using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Testimomal : BaseEntity
    {

        [Required]
        public string Description { get; set; }

        [Required]
        public string PersonName { get; set; }

        [Required]

        public string PersonJob { get; set; }

        public string Picture { get; set; }
    }
}
