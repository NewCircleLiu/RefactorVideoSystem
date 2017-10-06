using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class Code :BaseModel
    {
        [Key]
        public int CodeID { get; set; } //视频邀请码的ID
        [Required]
        public string CodeValue { get; set; } //视频邀请码
        [Required]
        public int CodeStatus { get; set; }   //视频邀请码的三种状态：未激活(0)、激活(1)、已使用(2)
        [Required]
        public int BindedDeviceCount { get; set; } //视频邀绑定的设备总数
        public string DeviceUniqueCode { get; set; } //统一视频邀请码对应的最多3个设备，用逗号分隔

        public int UserID { get; set; } //暂时不用
        [Required]
        public int vid { get; set; } //视频邀请码对应的唯一视频


        public virtual Video Video { get; set; }
    }
}