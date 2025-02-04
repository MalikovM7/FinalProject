﻿using Domain.Common;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class SliderImage : BaseEntity
    {
        public string Image { get; set; }

        [NotMapped]
        [Required]
        public List<IFormFile> Photos { get; set; }


    }
}
