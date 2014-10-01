using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shauli_blog.Models
{
    public class Comment
    {   
       [Required]
        public long id{get;set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime pubTime{get;set;}

       
        [Required]
        public long ArticleId { get; set; }
        
        [Required]
        [StringLength(200)]
        [Display(Name="Comment")]
        [DataType(DataType.MultilineText)]
        public string description{get;set;}
       
        [Required]
        [Display(Name="Name")]
        public string name{get;set;}

        [Url]
        [Display(Name="Web-Site")]
        public string website { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name="Email")]
        public string email { get; set; }

        public Comment() 
        {
            pubTime = DateTime.Now;
        }

    }
}