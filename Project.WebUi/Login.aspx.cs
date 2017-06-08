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
using Project.Model;
using Project.Bll;
using Project.Common;

namespace Project.WebUi
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //登陆
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string uName = this.txtAdminName.Text.Trim();
            string uPwd = this.txtAdminPwd.Text.Trim();
            string vCode = this.txtVcode.Text.Trim().ToLower();
            string code = Session["vcode"] == null ? "" : Session["vcode"].ToString().Trim().ToLower();
            if (vCode == "" || vCode != code)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('验证码错误！');</script>");
                return;
            }

            UserInfoBll bll = new UserInfoBll();
            UserInfo model = bll.GetModel(uName, CommonFun.GetMD5(uPwd));
            if (model != null)
            {
                if (model.IsLock == 1)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('帐户被锁定，不能登陆，请与管理员联系！');</script>");
                    return;
                }
                else
                {
                    LoginUserInfo user = new LoginUserInfo();
                    user.UserId = model.UserId;
                    user.UserName = model.UserName;
                    user.RoleId = model.RoleId;

                    CommonFun.UserLoginSetCookie(user.UserName, this.Page, DateTime.Now.AddMinutes(30), user);
                    Response.Write("<script type='text/javascript'>window.location = 'Default.aspx'</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('用户名或密码错误！');</script>");
                return;
            }
        }
    }
}
