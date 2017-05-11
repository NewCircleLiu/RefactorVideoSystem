using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class User:BaseModel
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string UserUniqueCode { get; set; }

        public ICollection<ActionLog> ActionLogs { get; set; }
        public ICollection<Suggest> Suggests { get; set; }
    }
}