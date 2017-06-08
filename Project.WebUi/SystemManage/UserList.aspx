<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Project.WebUi.SystemManage.UserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>用户列表</title>
    <script src="../js/lhgcore.min.js" type="text/javascript"></script>
    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div class="navigation"><span class="add"><a id="aAdd__UserEdit" runat="server" href="UserEdit.aspx">新增用户</a></span><b></b></div>
    <div class="search">
    用户名：<asp:TextBox ID="txtUserName" runat="server" Width="100"/>
    &nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server" Text="查询" onclick="btnSearch_Click" />
    </div>
    
    <table width="60%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
          <tr align="center">
            <th>用户名称</th>
            <th>用户角色</th>
            <th>用户备注</th>
            <th>是否锁定</th>
            <th>操作</th>
          </tr>
       <asp:Repeater ID="rptList" runat="server" onitemcommand="rptList_ItemCommand">
            <ItemTemplate>
              <tr>
                <td><%#Eval("UserName")%></td>
                <td><%# GetRoleNames(Eval("RoleId").ToString())%></td>
                <td><%#Eval("Desc")%></td>
                <td align="center"><%# Eval("IsLock").ToString()=="0"?"否":"是"%></td>
                <td><a id="aUpdate__UserEdit" runat="server" href='<%# "UserEdit.aspx?UserId="+ Eval("UserId") %>' >修改</a> 
                    <asp:LinkButton ID="lkBtnDelete__UserList_Delete" runat="server" CommandName="del" CommandArgument='<%# Eval("UserId") %>' OnClientClick="return confirm('确定要删除吗？')">删除</asp:LinkButton>
                </td>
              </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr><td colspan="100" align="right"><%=pagetext %></td></tr>
      </table>
    </form>
</body>
</html>