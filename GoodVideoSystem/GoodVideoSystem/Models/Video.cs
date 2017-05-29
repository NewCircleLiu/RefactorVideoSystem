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
        public int VideoID { get; set; } //视频ID
        [Required]
        public string VideoImageLocal { get; set; } //视频封面图片
        [Required]
        public int ls_video_id { get; set; } //乐视视频信息
        [Required]
        public string ls_video_uuid { get; set; }//乐视视频信息
        [Required]
        public string VideoName { get; set; } //视频名称
        [Required]
        public int CodeCounts { get; set; } //邀请码总数
        [Required]
        public int CodeUsed { get; set; } //视频是否被使用
        [Required]
        public int CodeNotUsed { get; set; } //视频是否被使用
        

        public ICollection<Code> Code { get; set; }
    }
}