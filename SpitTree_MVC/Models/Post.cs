using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpitTree_MVC.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Location { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Display(Name = "Date Posted")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")] // Format as ShortDateTime
        public DateTime DatePosted { get; set; }
        [Display(Name = "Date Expired")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")] // Format as ShortDateTime
        public DateTime DateExpired { get; set; }


        //NAVIGATION PROPERTIES

        //USER
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        //CATEGORY
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}