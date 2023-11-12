using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpitTree_MVC.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; }

        //NAVIGATION PROPERTIES
        public List<Post> Posts { get; set; }
    }
}