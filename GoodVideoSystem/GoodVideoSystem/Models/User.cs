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
        public int UserId { get; set; }  //用户ID
        public string Username { get; set; } //用户名
        [Required]
        public string Phone { get; set; } //用户的手机号
        [Required]
        public string InviteCodes { get; set; } //用户所有的邀请码，格式："code1,code2...coden"
        public ICollection<ActionLog> ActionLogs { get; set; } //用户相关的LOG
    }
}