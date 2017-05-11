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
        public int ProductId { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public string Url { get; set; }
    }
}