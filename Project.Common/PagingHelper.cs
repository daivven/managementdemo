using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common
{
    /// <summary>
    /// 分页类
    /// </summary>
    public class PagingHelper
    {

        private int pageIndex;
        private int pageSize;
        private int totalPage;
        private int totalRecord;
        private string url;
        private string urlparms;
        private int itemCount = 0;
        private string firstname = "首页";
        private string prename = "上一页";
        private string nextname = "下一页";
        private string lastname = "尾页";
        private bool _showCustomerInfo = true;

        public bool ShowCustomerInfo
        {
            get { return _showCustomerInfo; }
            set { _showCustomerInfo = value; }
        }

        /// <summary>
        /// 首页名称
        /// </summary>
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        /// <summary>
        /// 上一页名称
        /// </summary>
        public string PreName
        {
            get { return prename; }
            set { prename = value; }
        }

        /// <summary>
        /// 下一页名称
        /// </summary>
        public string NextName
        {
            get { return nextname; }
            set { nextname = value; }
        }

        /// <summary>
        /// 末页名称
        /// </summary>
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        /// <summary>
        /// 项数，如项数等于5则显示(上一页，1，2，3，4，5，下一页)
        /// </summary>
        public int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageindex">当前页码</param>
        /// <param name="pagesize">每页要显示的记录数</param>
        /// <param name="totalRecord">当前条件下的总记录数</param> 
        /// <param name="url">Url地址</param>
        public PagingHelper(int pageindex, int pagesize, int totalRecord, string url)
        {
            this.PageIndex = pageindex;
            this.PageSize = pagesize;
            this.TotalRecord = totalRecord;
            this.Url = url;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="pageindex">当前页码</param>
        /// <param name="pagesize">每页要显示的记录数</param>
        /// <param name="totalRecord">当前条件下的总记录数</param> 
        /// <param name="url">Url地址</param>
        /// <param name="urlParms">Url查询参数</param>
        /// <param name="itemCount">项数,如项数等于5则显示(上一页，1，2，3，4，5，下一页)</param>
        public PagingHelper(int pageindex, int pagesize, int totalRecord, string url, string parms, int itemCount)
        {
            this.PageIndex = pageindex;
            this.PageSize = pagesize;
            this.TotalRecord = totalRecord;
            this.Url = url;
            this.UrlParms = parms;
            this.ItemCount = itemCount;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (this.pageIndex < 1)
                {
                    return 1;
                }
                return this.pageIndex;
            }
            set
            {
                if (value < 1)
                    this.pageIndex = 1;
                else
                    this.pageIndex = value;
            }
        }

        /// <summary>
        /// 每页要显示的记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {
                if (value > 0)
                {
                    this.pageSize = value;
                }
            }
        }

        /// <summary>
        /// 总页数(只读)
        /// </summary>
        public int TotalPage
        {
            get
            {
                return this.totalPage;
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecord
        {
            get
            {
                return this.totalRecord;
            }
            set
            {
                if (value > 0)
                {
                    this.totalRecord = value;
                    this.totalPage = (int)Math.Ceiling((double)((value * 1.0) / ((double)this.pageSize)));
                }
            }
        }

        /// <summary>
        /// Url地址
        /// </summary>
        public string Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        /// <summary>
        /// Url查询参数(格试如下:&name=james&........)
        /// </summary>
        public string UrlParms
        {
            get { return urlparms; }
            set { urlparms = value; }
        }

        /// <summary>
        /// 创建分页Html代码
        /// </summary>
        /// <returns></returns>
        public string CreatePageHtml()
        {
            #region 服务器端生成
            if (totalRecord <= 0)
                return "";
            string go = @"<input id='pagerParms' type='hidden' value='" + this.UrlParms + "' >" +
                "<script type=\"text/javascript\">function GOPAGER(i){var varParms = document.getElementById(\"pagerParms\");window.location.href=\"" + this.Url + "?pageindex=\"+i+\"" + this.UrlParms + " \"}</script>";
            string urlFormat = @"<li><a href=""javascript:{0}"">{1}</a></li>";
            string urlFormat2 = @"<li class=""pager current""><a href=""javascript:{0}"" >{1}</a></li>";
            string info = "<ul class=\"pager\">";
            StringBuilder sb = new StringBuilder();
            sb.Append(go);
            sb.Append(info);
            if (_showCustomerInfo)
                sb.AppendFormat("<li class=\"msg\">共{0}条记录，当前第{1}/{2}页，每页{3}条</li>", this.totalRecord, this.pageIndex, this.totalPage, this.pageSize);

            if (pageIndex <= 1)//如果当前为第一页
            {
                //sb.AppendFormat(urlDisFormat, this.firstname);
                //sb.AppendFormat(urlDisFormat, this.prename);
            }
            else
            {
                sb.AppendFormat(urlFormat, "GOPAGER(1)", this.firstname);//首页
                sb.AppendFormat(urlFormat, "GOPAGER(" + (pageIndex - 1) + ")", this.prename);//上一页
            }

            if (itemCount > 0 && totalPage > 1)//如果设置了项数
            {
                int actCount = itemCount < totalPage ? itemCount : totalPage;//如果设置的项数大于总页数，则实际项数设置为总页数
                int duan = totalPage / actCount; //总页数除以项数等于总段数
                int curDuan = (int)Math.Ceiling((pageIndex * 1.0) / (actCount * 1.0)); //当前所处的段=当前页除以项数


                for (int i = 1; i <= actCount; i++)
                {
                    int index = (curDuan - 1) * actCount + i;
                    sb.AppendFormat(index != pageIndex ? urlFormat : urlFormat2, "GOPAGER(" + index + ")", index);
                    if (index == totalPage)
                        break;
                }

                if (duan > 1 && (curDuan >= 1 && curDuan < duan))//如果总段数大于1且当前所处的段大于1
                {
                    sb.AppendFormat(urlFormat, "GOPAGER(" + (curDuan * actCount + 1) + ")", "...");
                }
            }

            if (pageIndex == totalPage)
            {
                //sb.AppendFormat(urlDisFormat, this.nextname);
                //sb.AppendFormat(urlDisFormat, this.lastname);
            }
            else
            {
                sb.AppendFormat(urlFormat, "GOPAGER(" + (pageIndex + 1) + ")", this.nextname);//下一页
                sb.AppendFormat(urlFormat, "GOPAGER(" + totalPage + ")", this.lastname);//尾页
            }
            sb.Append("</ul>");
            return sb.ToString();
            #endregion
        }

    }
}
