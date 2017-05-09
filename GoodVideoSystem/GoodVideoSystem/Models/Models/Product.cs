using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class Product:BaseModel
    {
        [Key]
        public int productId { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string img { get; set; }
        [Required]
        public string url { get; set; }
    }
}