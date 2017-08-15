using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RefactorVideoSystem.Models.Models;

namespace VideoSystem.Concrete
{
    public class GetCode //因为查找的时候是contains
    {
        public static List<Code> getCodeArray(Code[] codeArray, int userID)
        {
            List<Code> getCode = new List<Code>();

            foreach (Code code in codeArray)
            {
                    if (code.UserID==userID)
                    {
                        getCode.Add(code);
                    }
            }

            return getCode;
        }
    }
}