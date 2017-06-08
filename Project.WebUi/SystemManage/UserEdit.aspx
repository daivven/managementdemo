<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="Project.WebUi.SystemManage.UserEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑用户</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../js/lhgcore.min.js" type="text/javascript"></script>
    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    function CheckForm(){
        if(document.getElementById("txtUserName").value==""){alert("请输入用户名");return false;}
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <td align="right">用户名称：</td>
            <td align="left"><asp:TextBox ID="txtUserName" runat="server"/></td>
        </tr>
        <tr>
            <td align="right">用户角色：</td>
            <td align="left">
                <asp:CheckBoxList ID="chkListRole" runat="server" RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr id="trPwd" runat="server">
            <td align="right">初始密码：</td>
            <td align="left"><asp:TextBox ID="txtUserPwd" runat="server" Text="111111"/>&nbsp;用户登陆后可自己修改</td>
        </tr>
        <tr>
            <td align="right">是否锁定：</td>
            <td align="left">
                <asp:RadioButtonList ID="rdoList" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="启用" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="禁用(注：禁用后他将不能登陆)" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="right">用户备注：</td>
            <td align="left"><asp:TextBox ID="txtDesc" runat="server" TextMode="MultiLine"/></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" runat="server"  Text="保存" OnClientClick="return CheckForm()" onclick="btnSave_Click" />
                <input id="btnBack" type="button" value="返回" onclick="javascript:window.location.href='UserList.aspx'" />
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>