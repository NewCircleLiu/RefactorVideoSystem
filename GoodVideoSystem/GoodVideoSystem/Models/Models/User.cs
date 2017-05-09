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
        public int userId { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string userUniqueCode { get; set; }

        public ICollection<ActionLog> actionLogs { get; set; }
        public ICollection<Suggest> suggests { get; set; }
    }
}