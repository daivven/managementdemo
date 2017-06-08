<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Project.WebUi.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>登陆</title>
<style type="text/css">
    html,body{ height:100%; margin:0; padding:0}
    a{color:White;}
</style>
</head>

<body>
    <form id="form1" runat="server" style="height:100%">

 <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height:100% ; vertical-align:top;background-color:#24262B">
 
  <tr style="height:9%">
    <td style="background-color:Black"></td>
  </tr>
  
  <tr style="height:0.5%; background-color:White">
    <td></td>
  </tr>

 <tr >
   <td align="center" style="height:80%">
       <table style="font-size:14px; color:White;">
       <tr>
           <td>登陆名称：</td>
           <td align="left"><asp:TextBox ID="txtAdminName" runat="server" Width="140px"/></td>       
       </tr>
       <tr>
           <td>登陆密码：</td>
           <td align="left"><asp:TextBox ID="txtAdminPwd" runat="server" TextMode="Password" Width="140px"/></td>       
       </tr>
       <tr>
           <td>验证号码：</td>
           <td align="left" style="vertical-align:middle"><asp:TextBox ID="txtVcode" runat="server" Width="100px"/>&nbsp;<asp:Image ID="imgcode" runat="server" ImageUrl="~/Vcode.aspx" ImageAlign="AbsMiddle"/>
               <a id="txtChange" href="javascript:document.getElementById('imgcode').src='Vcode.aspx?'+new Date();void(0);"> 换一个</a></td>       
       </tr>
       <tr>
           <td colspan="2"><asp:Button ID="btnLogin" runat="server" Text="登陆" OnClick="btnLogin_Click" /></td>
    
       </tr>
       </table>
   </td>
 </tr>

  <tr style="height:0.5%; background-color:White">
    <td></td>
  </tr>
  <tr style="height:10%">
    <td style="background-color:Black"></td>
  </tr>

    </table>
    </form>
</body>
</html>
