using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class Video :BaseModel
    {
        [Key]
        public int VideoID { get; set; }
        [Required]
        public int ls_video_id { get; set; }
        [Required]
        public string ls_video_uuid { get; set; }
        [Required]
        public string VideoName { get; set; }
        [Required]
        public int CodeCounts { get; set; }
        [Required]
        public int CodeUsed { get; set; }
        [Required]
        public int CodeNotUsed { get; set; }
        [Required]
        public string VideoImageLocal { get; set; }

        public ICollection<Code> Code { get; set; }
    }
}