using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Testimonial : BaseEntity
    {
        
        public string UserId { get; set; }

       
        public int Rating { get; set; } // Rating out of 5

        public bool IsApproved { get; set; }

     
        public string Comment { get; set; }

    }
}
