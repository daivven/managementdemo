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
    public partial class RoleEdit : MyBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowData();
            }
        }


        private void ShowData()
        {
            string id = Request["RoleId"];
            if (!string.IsNullOrEmpty(id))
            {
                UserRoleBll bll = new UserRoleBll();
                UserRole model = bll.GetModel(Convert.ToInt32(id));
                if (model != null)
                {
                    this.txtRoleName.Text = model.RoleName;
                    this.txtRoleDesc.Text = model.RoleDesc;
                }
            }
        }


        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string id = Request["RoleId"] == null ? "" : Request["RoleId"].Trim();
            UserRoleBll bll = new UserRoleBll();
            UserRole model = new UserRole();
            if (!string.IsNullOrEmpty(id))//如果是修改操作
            {
                model.RoleId = Convert.ToInt32(id);

            }

            model.RoleName = this.txtRoleName.Text.Trim();
            model.RoleDesc = this.txtRoleDesc.Text.Trim();

            int n = 0;
            if (!string.IsNullOrEmpty(id))//如果是修改操作
            {
                n = bll.Update(model, null);
            }
            else //如果是新增操作
            {
                n = bll.Insert(model, null);
            }

            if (n > 0)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('保存成功！');frameElement.lhgDG.curWin.location.reload();</script>");
            else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('保存失败！');frameElement.lhgDG.curWin.location.reload();</script>");
        }
    }
}
