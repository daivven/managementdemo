<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuList.aspx.cs" Inherits="Project.WebUi.SystemManage.MenuList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>菜单列表</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script src="../js/lhgcore.min.js" type="text/javascript"></script>
    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>
    <script type="text/javascript">
    function opdg(url){
        var dg = new J.dialog({autoSize:true,btnBar:false, maxBtn:false,cover:true, rang:true,page:url,title:'编辑菜单',height:160,width:450 });
        dg.ShowDialog();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="navigation"><span class="add"><a id="aAdd__MenuEdit" runat="server" href="#" onclick="opdg('SystemManage/MenuEdit.aspx')">新增菜单</a></span><b></b></div>

    <table width="80%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr align="center">
        <th>菜单名称</th>
        <th>菜单地址</th>
        <th>菜单排序</th>
        <th>是否为左侧导航菜单</th>
        <th>操作</th>
      </tr>
      <asp:Repeater ID="rptList" runat="server" onitemcommand="rptList_ItemCommand">
         <ItemTemplate>
          <tr>
            <td><%#Eval("ParentId").ToString() == "0" ? "<strong>"+Eval("MenuName").ToString()+"</strong>" : "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + Eval("MenuName").ToString()%></td>
            <td><%#Eval("MenuAddress")%></td>
            <td align="center"><%# Eval("MenuOrder")%></td>
            <td align="center"><%# Eval("IsNavigation").ToString().Trim()=="0"?"否":"是"%></td>
            <td><a id="aUpdate__MenuEdit" runat="server" href="#" onclick='<%# "opdg(\"SystemManage/MenuEdit.aspx?MenuId="+ Eval("MenuId")+"\")" %>'>修改</a> 
                <asp:LinkButton ID="lkBtnDelete__MenuList_Delete" runat="server" CommandName="del" CommandArgument='<%# Eval("MenuId") %>' OnClientClick="return confirm('确定要删除吗？')">删除</asp:LinkButton>
            </td>
          </tr>
         </ItemTemplate>
        </asp:Repeater> 
    </table>
    </form>
</body>
</html>