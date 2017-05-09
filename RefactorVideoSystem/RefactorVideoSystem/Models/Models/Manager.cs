using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class Manager
    {
        [Key]
        public int ManagerID { get; set; }
        [Required]
        public string ManagerAccount { get; set; }
        [Required]
        public string ManagerPassword { get; set; }
        [Required]
        public string ManagerEmail { get; set; }
        [Required]
        public string ManagerPhone { get; set; }
    }
}