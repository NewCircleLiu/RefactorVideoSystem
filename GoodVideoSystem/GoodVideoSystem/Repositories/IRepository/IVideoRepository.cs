﻿using RefactorVideoSystem.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodVideoSystem.Repositories.Repository
{
    public interface IVideoRepository
    {
        Video getVideo(string inviteCode);
    }
}
