﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RefactorVideoSystem.Models.Models
{
    public class Suggest:BaseModel
    {
        [Required]
        public string SuggestText { get; set; }
        [Required]
        public string UserPhone { get; set; }
    }
}