using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using RefactorVideoSystem.Models.Models;

namespace GoodVideoSystem.Models.Abstract
{
    public interface IExportExcel
    {

        DataTable MakeDataTable(Code[] codeArray);
        byte[] WriteExcel(Code[] codeArray);
    }
}
