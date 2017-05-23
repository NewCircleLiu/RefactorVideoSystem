using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RefactorVideoSystem.Models.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            DateTime now = DateTime.Now;
            this.CreateTime = now;
            this.ModifyTime = now;
        }

        [Required]
        public DateTime CreateTime { get; set; }

        [Required]
        public DateTime ModifyTime { get; set; }
    }
}