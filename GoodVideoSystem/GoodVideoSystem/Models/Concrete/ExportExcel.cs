using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using GoodVideoSystem.Models.Abstract;
using RefactorVideoSystem.Models.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace GoodVideoSystem.Models.Concrete
{
    public class ExportExcel : IExportExcel
    {
        //构造DataTable表格
        public DataTable MakeDataTable(Code[] codeArray)
        {
            DataTable dt = new DataTable("邀请码列表");
            dt.Columns.Add("视频编号");
            dt.Columns.Add("视频名称");
            dt.Columns.Add("邀请码编号");
            dt.Columns.Add("邀请码");

            foreach (Code c in codeArray)
            {
                dt.Rows.Add(c.Video.vid, c.Video.VideoName, c.CodeID, c.CodeValue);
            }

            return dt;
        }

        public byte[] WriteExcel(Code[] codeArray)
        {

            DataTable dt = MakeDataTable(codeArray);

            if (null != dt && dt.Rows.Count > 0)
            {
                HSSFWorkbook book = new HSSFWorkbook();
                ISheet sheet = book.CreateSheet(dt.TableName);

                sheet.SetColumnWidth(0, 10 * 256);
                sheet.SetColumnWidth(1, 80 * 256);
                sheet.SetColumnWidth(2, 10 * 256);
                sheet.SetColumnWidth(3, 20 * 256);

                IRow row = sheet.CreateRow(0);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row2 = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i][j]));
                    }
                }

                MemoryStream ms = new MemoryStream();
                book.Write(ms);
                return ms.ToArray();
            }
            return null;
        }
    }
}