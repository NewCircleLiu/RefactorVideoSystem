using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RefactorVideoSystem.Models.Models;

namespace VideoSystem.Concrete
{
    public class getInviteCode //因为查找的时候是contains
    {
        public static List<Code> getInviteCodeArray(Code[] codeArray, int userID)
        {
            List<Code> getInviteCode = new List<Code>();

            foreach (Code code in codeArray)
            {
                    if (code.UserID==userID)
                    {
                        getInviteCode.Add(code);
                    }
            }

            return getInviteCode;
        }
    }
}