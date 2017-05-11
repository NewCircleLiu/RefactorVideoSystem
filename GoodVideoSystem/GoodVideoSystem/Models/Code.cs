using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class Code :BaseModel
    {
        [Key]
        public int CodeID { get; set; }
        [Required]
        public string CodeValue { get; set; }
        [Required]
        public int CodeStatus { get; set; }


        public int UserID { get; set; }
        [Required]
        public int VideoID { get; set; }


        public virtual Video Video { get; set; }
    }
}