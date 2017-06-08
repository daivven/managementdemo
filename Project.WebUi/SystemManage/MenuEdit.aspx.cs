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
    public partial class MenuEdit : MyBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindMenu();
                ShowData();
            }
        }

        protected void ShowData()
        {
            string id = Request["MenuId"];
            if (!string.IsNullOrEmpty(id))
            {
                UserMenuBll bll = new UserMenuBll();
                UserMenu model = bll.GetModel(Convert.ToInt32(id));
                if (model != null)
                {
                    this.txtMenuName.Text = model.MenuName;
                    this.txtMenuAddress.Text = model.MenuAddress;
                    this.txtMenuOrder.Text = model.MenuOrder.ToString();
                    this.rdoList.SelectedValue = model.IsNavigation.ToString();
                    if (model.ParentId == 0)
                        this.ddlParent.Enabled = false;
                    else
                        this.ddlParent.SelectedValue = model.ParentId.ToString();
                }
            }
        }

        //绑定顶级菜单
        protected void BindMenu()
        {
            UserMenuBll bll = new UserMenuBll();
            List<UserMenu> list = bll.GetTopList();
            if (list != null)
            {
                this.ddlParent.DataSource = list;
                this.ddlParent.DataTextField = "MenuName";
                this.ddlParent.DataValueField = "MenuId";
                this.ddlParent.DataBind();
                this.ddlParent.Items.Insert(0, new ListItem("顶级菜单", "0"));
            }
        }
        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string id = Request["MenuId"];
            UserMenuBll bll = new UserMenuBll();
            UserMenu model = null;
            if (!string.IsNullOrEmpty(id))//如果是修改操作
            {
                model = bll.GetModel(Convert.ToInt32(id));

            }
            else //如果是新增操作
            {
                model = new UserMenu();
            }
            model.MenuName = this.txtMenuName.Text.Trim();
            model.MenuAddress = this.txtMenuAddress.Text.Trim();
            model.MenuOrder = Convert.ToInt32(this.txtMenuOrder.Text.Trim());
            model.ParentId = Convert.ToInt32(this.ddlParent.SelectedValue);
            model.IsNavigation = Convert.ToInt32(this.rdoList.SelectedValue);

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
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('保存失败！');frameElement.lhgDG.curWin.location.reload();</script>");//刷新，lhgDG是个第三方的JS弹出窗
        }
    }
}
