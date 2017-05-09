using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public int ProductPrice { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductImg { get; set; }
        [Required]
        public string ProductUrl { get; set; }
    }
}