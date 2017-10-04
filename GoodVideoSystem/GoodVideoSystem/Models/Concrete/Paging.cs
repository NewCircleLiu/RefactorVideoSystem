using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoodVideoSystem.Models.Abstract;

namespace GoodVideoSystem.Models.Concrete
{
    public class Paging : IPaging
    {

        //page表示目标页
        //当前请求的页码
        public int request_page;
        //单页显示的页码链接数目
        private int page_code_num = 5;
        //每页显示数据条数
        public int every_page_items = 15;
        //数据总条数
        private int total_itenms;
        //总页数
        public int total_pages;
        //显示的起始页码
        public int start_page_num;
        //显示的结束页码
        public int end_page_num;
        //返回的数据
        public Object[] returnData;

        public Object[] GetCurrentPageData(IEnumerable<Object> data, int page)
        {
            request_page = page;
            //获取数据总条数
            total_itenms = data.Count();
            //计算总页数
            total_pages = (int)Math.Ceiling(total_itenms / (every_page_items * 1.0));
            //判断目标页是否超出范围
            if (page <= 0)
            {
                page = 1;
            }
            if (page > total_pages)
            {
                page = total_pages;
            }
            //获取请求的数据
            returnData = data.Skip((page - 1) * every_page_items).Take(every_page_items).ToArray();
            //计算页码显示
            start_page_num = ((page - 1) / page_code_num) * page_code_num + 1;
            end_page_num = Math.Min(start_page_num + page_code_num - 1, total_pages);

            return returnData;
        }
    }
}