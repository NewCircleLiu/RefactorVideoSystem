using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class ActionLog:BaseModel
    {
        [Key]
        public int actionLogId { get; set; }
        [Required]
        public String action { get; set; }

        [Required]
        public String userId {get;set;}

        public virtual User user { get; set; }
    }
}