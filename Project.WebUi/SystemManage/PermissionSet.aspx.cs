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
    public partial class PermissionSet : MyBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Request["ids"] != null&& Request["RoleId"] != null)
                {
                    Response.Write(Save());
                    Response.End();
                }
                BindRpt();
            }
        }


        protected void BindRpt()
        {
            UserMenuBll bll = new UserMenuBll();
            List<UserMenu> list = bll.GetMenuRoleList(Convert.ToInt32(Request["RoleId"]));
            if (list != null && list.Count > 0)
            {
                List<UserMenu> parentList = list.FindAll(delegate(UserMenu m) { return m.ParentId == 0; });
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < parentList.Count; i++)
                {
                    sb.Append("<tr style=\"text-align:left; font-weight:bold\"><td>");
                    string id = "chkParent" + parentList[i].MenuId;
                    sb.AppendFormat("<input id='{0}' type='checkbox'  {1} onclick=\"{2}\"  value='{3}' name='chk' /><label for='{0}' />" + parentList[i].MenuName,
                        id, parentList[i].RoleHas > 0 ? "checked='checked'" : "", "chkParent(this,'menutd" + parentList[i].MenuId + "')", parentList[i].MenuId);
                    sb.Append("</td></tr>");


                    sb.AppendFormat("<tr style=\"text-align:left\"><td id=\'menutd{0}\'>", parentList[i].MenuId);
                    List<UserMenu> childList = list.FindAll(delegate(UserMenu m) { return m.ParentId == Convert.ToInt32(parentList[i].MenuId); });
                    for (int j = 0; j < childList.Count; j++)
                    {
                        string id2 = "chkChild" + childList[j].MenuId;
                        sb.AppendFormat("<span class=\"ChildSpan\"><input id='{0}' type='checkbox'  {1} onclick=\"{2}\"  value='{3}' name='chk' /><label for='{0}' />" + childList[j].MenuName + "</span>",
                                id2, childList[j].RoleHas > 0 ? "checked='checked'" : "", "chkChild(" + id + ",'menutd" + parentList[i].MenuId + "')", childList[j].MenuId);
                    }
                    sb.Append("</td></tr>");
                }
                litChild.Text = sb.ToString();
            }
        }


        //保存
        private string Save()
        {
            string ids = Request["ids"];
            if (ids.EndsWith(","))
                ids = ids.Substring(0, ids.Length - 1);
            UserPermissionBll bll = new UserPermissionBll();

            int roleId = Convert.ToInt32(Request["RoleId"]);
            int n = bll.Save(roleId, ids);
            if (n != -1)
                return "1";//保存成功
            else
                return "0";//保存失败

        }

    }
}
