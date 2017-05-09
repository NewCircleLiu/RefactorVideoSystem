using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RefactorVideoSystem.Models.Models
{
    public class BaseModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public DateTime createTime { get; set; }

        [Required]
        public DateTime modifyTime { get; set; }

        [Required]
        public int isDelete{ get; set; }
    }
}