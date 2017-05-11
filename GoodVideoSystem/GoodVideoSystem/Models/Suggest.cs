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
        public int SuggestId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string UserPhone { get; set; }

        public int UserId { get; set; }
        [Required]
        public virtual User user { get; set; }
    }
}