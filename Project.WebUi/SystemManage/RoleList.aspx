<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="Project.WebUi.SystemManage.RoleList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑角色</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../js/lhgcore.min.js" type="text/javascript"></script>
    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    function opdg(url,width,height){
        var dg = new J.dialog({autoSize:true,btnBar:false, maxBtn:false,cover:true, rang:true,page:url,title:'编辑角色',height:height,width:width });
        dg.ShowDialog();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     
    <div class="navigation"><span class="add"><a id="aAdd__RoleEdit" runat="server" href="#" onclick="opdg('SystemManage/RoleEdit.aspx',300,150);">新增角色</a></span><b></b></div>
    <table width="60%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr align="center">
        <th>角色名称</th>
        <th>角色说明</th>
        <th>操作</th>
      </tr>
     <asp:Repeater ID="rptList" runat="server" onitemcommand="rptList_ItemCommand">           
            <ItemTemplate>
              <tr>
                <td><%#Eval("RoleName")%></td>
                <td><%#Eval("RoleDesc")%></td>
                <td><a id="aUpdate__RoleEdit" runat="server" href="#" onclick='<%# "opdg(\"SystemManage/RoleEdit.aspx?RoleId="+ Eval("RoleId")+"\",300,150)" %>'>修改</a> 
                    <asp:LinkButton ID="lkBtnDelete__RoleList_Delete" runat="server" CommandName="del" CommandArgument='<%# Eval("RoleId") %>' OnClientClick="return confirm('确定要删除吗？')">删除</asp:LinkButton>
                    <a id="aPermission__PermissionSet" runat="server" href='<%# "PermissionSet.aspx?RoleId="+ Eval("RoleId") %>'>分配权限</a> 
                </td>
              </tr>
            </ItemTemplate>
        </asp:Repeater>
      </table>
    </form>
</body>
</html>