<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuEdit.aspx.cs" Inherits="Project.WebUi.SystemManage.MenuEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>菜单编辑</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <td align="right">菜单名称：</td>
            <td align="left"><asp:TextBox ID="txtMenuName" runat="server"/></td>
        </tr>
        <tr>
            <td align="right">上级菜单：</td>
            <td align="left">
                <asp:DropDownList ID="ddlParent" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">菜单地址：</td>
            <td align="left"><asp:TextBox ID="txtMenuAddress" runat="server"/></td>
        </tr>
        <tr>
            <td align="right">菜单排序：</td>
            <td align="left">
                <asp:TextBox ID="txtMenuOrder" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">左侧导航菜单：</td>
            <td align="left">
                <asp:RadioButtonList ID="rdoList" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="是" Value="1"></asp:ListItem>
                    <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnSave" runat="server"  Text="保存" onclick="btnSave_Click" />  
              </td>
        </tr>
    </table>
    
    </form>
</body>
</html>