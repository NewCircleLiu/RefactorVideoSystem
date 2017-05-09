using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class Code:BaseModel
    {
        [Required]
        public string CodeValue { get; set; }
        [Required]
        public int CodeStatus { get; set; }
        [Required]
        public int UseCount { get; set; }

        public string UserID { get; set; }
        [Required]
        public int VideoID { get; set; }
    }
}