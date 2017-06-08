using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Project.Model;
using Project.Common;
using System.Collections.Generic;
using Project.Bll;

namespace Project.WebUi.App_Code
{
    public class MyBasePage : System.Web.UI.Page
    {

        /// <summary>
        /// 当前登陆用户信息
        /// </summary>
        protected LoginUserInfo loginUser = null;

        private List<string> list = null;

        //重写OnLoad方法(这里来判断是否有进入页面的权限)
        protected override void OnLoad(EventArgs e)
        {
            loginUser = CommonFun.GetCookieUserData<LoginUserInfo>(this.Page);
            UserMenuBll bll = new UserMenuBll();
            list = bll.GetRoleMenuAddress(loginUser.RoleId);//有人会说这里有损性能，每进一次页面都要读数据库，其实可以用缓存解决，这里我暂时就不添加缓存了

            if (!RoleHasMenuAddress(list))//如果没有进入当前页面的权限
            {
                Response.Write("<script>alert('您没有进入该页面的权限！');</script>");
                Response.End();
            }
            else
            {
                base.OnLoad(e);
            }
        }

        //重写OnPreRenderComplete方法(这里来判断是否有页面中的按钮的权限)
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);
            ProcessPermission(GetOperationControls(), list);  //权限按钮处理         
        }


        //判断当前角色是否拥有进入当前页面的权限
        private bool RoleHasMenuAddress(List<string> list)
        {
            bool hasPermission = false;//是否拥有权限bool变量
            if (list != null)
            {
                string path = Request.Path.Substring(Request.Path.LastIndexOf("/") + 1);//获得如 UserManage.aspx这样的当前的页面地址

                for (int i = 0; i < list.Count; i++)
                {
                    if (path == list[i].Substring(list[i].LastIndexOf("/") + 1))//当前角色是否拥有进入这个页面地址的权限
                    {
                        hasPermission = true;
                        break;
                    }
                }
            }
            return hasPermission;
        }


        /// <summary>
        /// 获取当前页面上所有的权限按钮(也包括Repeater中的)(由于作者在项目中只用Repaeter来显示列表数据所以此方法只有Repeater中的按钮处理，您可以自行完善此方法)
        /// </summary>
        private List<Control> GetOperationControls()
        {
            List<Control> list = new List<Control>();
            ControlCollection controls = this.Page.Form.Controls;
            if (controls != null)
            {
                for (int i = 0; i < controls.Count; i++)//有人会说这里有损性能，其实一个页面的控件就那么多，而且这点性能损失所换来的代码可维护性和可扩展性太值了
                {
                    if (controls[i].ID != null)
                    {
                        if (controls[i] is Repeater)
                        {
                            Repeater rpt = controls[i] as Repeater;
                            for (int x = 0; x < rpt.Items.Count; x++)
                            {
                                for (int y = 0; y < rpt.Items[x].Controls.Count; y++)
                                    if (rpt.Items[x].Controls[y].ClientID.IndexOf("__") != -1)
                                        list.Add(rpt.Items[x].Controls[y]);
                            }
                        }
                        else if (controls[i].ID.IndexOf("__") != -1)
                        {
                            list.Add(controls[i]);
                        }
                    }
                }
            }
            return list;
        }


        //权限按钮的处理
        private void ProcessPermission(List<Control> list, List<string> listMenu)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    string s1 = list[i].ID.Substring(list[i].ID.IndexOf("__") + 2);
                    string s2 = "";
                    bool hasPermission = false;//是否拥有权限bool变量
                    int index = 0;
                    if (s1.IndexOf("_") != -1)
                    {
                        s2 = s1.Substring(s1.IndexOf("_") + 1);
                        s1 = s1.Replace("_" + s2, ".aspx");
                    }
                    else
                        s1 += ".aspx";

                    for (int k = 0; k < listMenu.Count; k++)
                    {
                        string page = listMenu[k].Substring(listMenu[k].LastIndexOf("/") + 1);

                        if (s2 == "")
                        {
                            if (s1 == page)
                            {
                                hasPermission = true;
                                index = i;
                                break;
                            }
                        }
                        else
                        {
                            if ((s1 + "?type=" + s2) == page)
                            {
                                hasPermission = true;
                                index = i;
                                break;
                            }
                        }
                        index = i;
                    }
                    if (!hasPermission)
                        list[index].Visible = false;
                }
            }
        }

    }
}
