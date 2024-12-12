using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class News : BaseEntity
    {

        public string NewsCategory {  get; set; }

        public string AuthorName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string NewsDate { get; set; }

        public bool IsSelected { get; set; }


    }
}
