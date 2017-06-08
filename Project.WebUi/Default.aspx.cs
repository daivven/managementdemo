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

namespace Project.WebUi
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoginUserInfo loginUser = CommonFun.GetCookieUserData<LoginUserInfo>(this.Page);
                if (loginUser != null)
                {
                    this.lblAdminName.Text = loginUser.UserName;
                    this.Literal1.Text = GetRoleMenu(loginUser.RoleId);
                }
            }
        }

        //原html代码
        //<div class="left_menu">
        //  <div class="ti" onclick="sh(1)">用户管理</div>
        //  <ul id="1" style="display:block">
        //    <li><a href="UserManage/AdminList.aspx" target="sysMain">用户列表</a></li>
        //    <li><a href="UserManage/RoleList.aspx" target="sysMain">角色管理</a></li>
        //    <li><a href="UserManage/MenuList.aspx" target="sysMain">菜单管理</a></li>
        //    <li><a href="UserManage/UpdatePassword.aspx" target="sysMain">修改密码</a></li>
        //  </ul>
        //</div> 
        protected string GetRoleMenu(string roleId)
        {
            UserMenuBll bll = new UserMenuBll();
            List<UserMenu> allList = bll.GetRoleMenuList(roleId);//先获取该角色拥有的所有菜单
            StringBuilder sb = new StringBuilder();
            if (allList != null)
            {
                List<UserMenu> parentList = allList.FindAll(delegate(UserMenu m) { return m.ParentId == 0 && m.IsNavigation == 1; });//获得所有一级菜单  
                for (int i = 0; i < parentList.Count; i++)
                {
                    sb.Append("<div class=\"left_menu\">");
                    sb.AppendFormat("<div class=\"ti\" onclick=\"sh({0})\"><a href='javascript:' style='color:black'>{1}</a></div>", parentList[i].MenuId, parentList[i].MenuName);
                    sb.AppendFormat("<ul id=\"{0}\" style=\"display:none\">", parentList[i].MenuId);
                    List<UserMenu> childList = allList.FindAll(delegate(UserMenu m) { return m.ParentId == parentList[i].MenuId && m.IsNavigation == 1; });//获得某个一级菜单下的所有二级菜单
                    for (int k = 0; k < childList.Count; k++)
                    {
                        sb.AppendFormat("<li><a href=\"{0}\" target=\"sysMain\">{1}</a></li>", Page.ResolveUrl("~/" + childList[k].MenuAddress), childList[k].MenuName);
                    }
                    sb.AppendFormat("</ul></div>");
                }
            }
            return sb.ToString();
        }
    }
}
