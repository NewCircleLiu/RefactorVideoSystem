using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class ExceptionLog:BaseModel
    {
        [Key]
        public int ExceptionLogId { get; set; }
        [Required]
        public string Context { get; set; }
    }
}