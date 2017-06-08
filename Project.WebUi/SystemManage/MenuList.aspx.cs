using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Project.Bll;
using Project.Model;
using Project.Common;
using System.Collections.Generic;
using Project.WebUi.App_Code;

namespace Project.WebUi.SystemManage
{
    public partial class MenuList : MyBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRpt();
            }
        }

        //显示列表数据
        protected void BindRpt()
        {
            UserMenuBll bll = new UserMenuBll();
            List<UserMenu> list = bll.GetList();
            if (list != null)
            {
                this.rptList.DataSource = bll.GetOrderedList(list);
                this.rptList.DataBind();
            }
        }


        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "del")//如果是删除命令
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                UserMenuBll bll = new UserMenuBll();

                if (bll.Delete(id, null) > 0)
                {
                    Response.Write("<script>alert('删除成功！');window.location.href='MenuList.aspx'</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除失败！');</script>");
                }
            }
        }
    }
}
