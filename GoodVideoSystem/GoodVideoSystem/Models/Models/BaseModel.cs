using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RefactorVideoSystem.Models.Models
{
    public class BaseModel
    {
        

        [Required]
        public DateTime createTime { get; set; }

        [Required]
        public DateTime modifyTime { get; set; }
    }
}