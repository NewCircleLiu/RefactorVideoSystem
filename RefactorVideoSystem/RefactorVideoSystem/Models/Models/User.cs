using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        public string UserPhone { get; set; }
        [Required]
        public string UserBrowser1 { get; set; }
    }
}