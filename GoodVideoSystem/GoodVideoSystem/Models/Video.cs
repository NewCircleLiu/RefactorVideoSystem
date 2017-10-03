using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class Video : BaseModel
    {
        [Key]
        public virtual int vid { get; set; }                //视频ID
        public virtual string polyVid { get; set; }         //POLY视频ID
        [Required]
        public virtual string VideoName { get; set; }       //视频名称
        [Required]
        public virtual string coverImage { get; set; }      //视频封面图片
        [Required]
        public virtual int CodeCounts { get; set; }         //邀请码总数
        [Required]
        public virtual int CodeUsed { get; set; }           //视频是否被使用
        [Required]
        public virtual int CodeNotUsed { get; set; }        //视频是否被使用  

        public virtual ICollection<Code> Code { get; set; }
    }
}