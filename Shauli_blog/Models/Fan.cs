using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shauli_blog.Models
{
    public class Fan
    {
        [Required]
        public long id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }


        public DateTime BirthDate { get; set; }

        [Display(Name = "Image")]
        public String ImagePath { get; set; }

    }
}