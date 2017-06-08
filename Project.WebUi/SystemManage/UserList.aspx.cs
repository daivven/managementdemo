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
using System.Text;
using Project.WebUi.App_Code;

namespace Project.WebUi.SystemManage
{
    public partial class UserList : MyBasePage
    {
        protected string pagetext = "";//分页html代码
        protected List<UserRole> roleList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                roleList = new UserRoleBll().GetList();
                SetSql();
                BindRpt();
            }
        }

        //获取角色名称
        protected string GetRoleNames(string roleId)
        {
            string names="";
            if (!string.IsNullOrEmpty(roleId))
            {
                string[] ids = roleId.Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    for (int j = 0; j < roleList.Count; j++)
                    {
                        if (ids[i] == roleList[j].RoleId.ToString())
                        {
                            names += roleList[j].RoleName + "  ";
                            break;
                        }
                    }
                }
            }
            return names;
        }

        //绑定列表数据
        protected void BindRpt()
        {
            int totalRecord = 0;//总记录条数
            int pageIndex = Request["pageindex"] == null ? 1 : Convert.ToInt32(Request["pageindex"]);//当前页码
            int pageSize = 15;//每页条数
            string parms = "";//传递给下一页的条件
            string sql = GetSql(out parms);//传递给SQL的查询条件
            UserInfoBll bll = new UserInfoBll();
            DataTable list = bll.GetList(pageIndex, pageSize, sql, out totalRecord);
            PagingHelper pa = new PagingHelper(pageIndex, pageSize, totalRecord, "UserList.aspx", parms, 10);
            pagetext = pa.CreatePageHtml();//生成分页html
            if (list != null)
            {
                this.rptList.DataSource = list;
                this.rptList.DataBind();
            }
        }


        //获取查询条件
        protected string GetSql(out string parms)
        {
            StringBuilder sql = new StringBuilder();//传递给SQL的查询条件
            StringBuilder url = new StringBuilder();//传递给下一页的条件
            string userName = this.txtUserName.Text.Trim();//用户名
            if (!string.IsNullOrEmpty(userName))
            {
                sql.AppendFormat(" and userName like '%{0}%' ", userName);
                url.Append("&userName=" + userName);
            }

            parms = url.ToString();
            return sql.ToString();
        }

        //设置查询条件
        protected void SetSql()
        {
            string userName = Request["userName"];//用户名
            if (!string.IsNullOrEmpty(userName))
                this.txtUserName.Text = userName;
        }

        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "del")//如果是删除命令
            {
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                UserInfoBll bll = new UserInfoBll();

                if (bll.Delete(id, null) > 0)
                {
                    Response.Write("<script>alert('删除成功！');window.location.href='UserList.aspx'</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('删除失败！');</script>");
                }
            }
        }


        //查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string userName = this.txtUserName.Text.Trim();
            Response.Redirect(string.Format("UserList.aspx?userName={0}", userName));
        }
    }
}
