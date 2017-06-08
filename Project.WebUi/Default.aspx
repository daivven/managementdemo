<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Project.WebUi._Default" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>后台</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
    $(document).ready(function() { 
        //关闭打开左栏目
	    $("#sysBar").toggle(function(){
		    $("#mainLeft").hide();
		    $("#barImg").attr("src","images/butOpen.gif");
	    },function(){
		    $("#mainLeft").show();
		    $("#barImg").attr("src","images/butClose.gif");
	    });
    });

     function sh(id){
        var obj = document.getElementById(id);
        if(obj.style.display=="block")
            obj.style.display="none";
        else
            obj.style.display="block";
     }
    </script>
</head>

<body>
    <form id="form1" runat="server" style="height:100%">

 <table border="0" cellpadding="0" cellspacing="0" width="100%" style="background:#EBF5FC; height:100% ; vertical-align:top">
 
  <tr style="height:60px">
    <td  colspan="3" style="background:url(head_bg.gif) repeat-x;  vertical-align:top">
       <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="24%" ></td>
            <td width="76%" valign="bottom"></td>
          </tr>
       </table>
    </td>
  </tr>
  
  <tr style="height:30px; vertical-align:top">
    <td  colspan="3" style="padding:0px 10px;font-size:12px;background:url(navsub_bg.gif) repeat-x; vertical-align:middle">
    <div style="float:right;line-height:20px;"> <a href='<%=ResolveUrl("UserExit.aspx")%>' target="_self">安全退出</a></div>
    <div style="padding-left:20px;line-height:20px;background:url(siteico.gif) 0px 0px no-repeat;">当前登录用户：<font color="#FF0000">
    <asp:Label ID="lblAdminName" runat="server" Text=""/></font>您好，欢迎光临。</div>
    </td>
  </tr>


  <tr>
    <td  id="mainLeft" valign="top" style="background:#FFF; width:185px; vertical-align:top; ">
	  <div style="text-align:left;width:185px;font-size:12px; ">

        <div style="padding-left:10px;height:29px;line-height:29px;background:url(menu_bg.gif) no-repeat;">
          <span style="padding-left:15px;font-weight:bold;color:#039;background:url(menu_dot.gif) no-repeat;">网站后台管理</span>
        </div>
   
         <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <%--<div class="left_menu">
          <div class="ti" onclick="sh(1)">用户管理</div>
          <ul id="1" style="display:block">
            <li><a href="UserManage/AdminList.aspx" target="sysMain">用户列表</a></li>
            <li><a href="UserManage/RoleList.aspx" target="sysMain">角色管理</a></li>
            <li><a href="UserManage/MenuList.aspx" target="sysMain">菜单管理</a></li>
            <li><a href="UserManage/UpdatePassword.aspx" target="sysMain">修改密码</a></li>
          </ul>
        </div> --%>    
           
      </div>
	</td>
	<td style="width:8px;background:url(main_cen_bg.gif) repeat-x;  vertical-align:middle">
      <div id="sysBar" style="cursor:pointer;"><img id="barImg" src="images/butClose.gif" alt="关闭/打开左栏" /></div>
	</td>
	<td  style="width:100%; height:100%;" valign="top">
      <iframe frameborder="0" id="sysMain" name="sysMain" scrolling="auto" src="Welcome.aspx" style="height:100%; width:100%;"></iframe>
	</td>
  </tr>
  
  <tr style="height:28px;">
    <td height="28px" colspan="3" bgcolor="#EBF5FC" style="padding:0px 10px;font-size:10px;color:#2C89AD;background:url(foot_bg.gif) repeat-x;"></td>
  </tr>
  
</table>
</form>
</body>
</html>