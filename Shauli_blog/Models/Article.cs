using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shauli_blog.Models
{
    public class Article
    {
    
        public long id { get; set; }
        [Required]
        public string title{get;set;}

        public string author{get;set;}

        public DateTime pubtime { get; set; }
        
        public string content{ get; set; }

        public Article() {
            pubtime = DateTime.Now;
            
        }
    }
}