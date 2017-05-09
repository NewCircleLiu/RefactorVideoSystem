using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class Suggest:BaseModel
    {
        [Key]
        public int suggestId { get; set; }
        [Required]
        public string text { get; set; }
        [Required]
        public string userPhone { get; set; }

        public int userId { get; set; }
        [Required]
        public virtual User user { get; set; }
    }
}