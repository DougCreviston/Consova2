<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Web_LynxClient.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LynxClient 2.0</title>
    <link rel="stylesheet" type="text/css" href="Styles/BasicStyle.css" />
    <style type="text/css">
        .style1
        {
            height: 66px;
        }
        .style2
        {
            height: 66px;
            width: 67px;
        }
        .style3
        {
            width: 67px;
        }
    </style>
</head>
<body>
   <div class="body">
   <div class="page">
    <div class="header">
     <form id="form1" runat="server">
     <div class="LynxClient_title">
      <h1 class="version">CCI Text Master SMS </h1>
     <div class="hideSkiplink">
      <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                        <asp:MenuItem NavigateUrl="~/Default.aspx" Text="Home"/>
                        <asp:MenuItem NavigateUrl="~/About.aspx" Text="Reports"/>
                        <asp:MenuItem NavigateUrl="~/About.aspx" Text="Data Import"/>
                        <asp:MenuItem NavigateUrl="~/About.aspx" Text="Reports"/>
                        <asp:MenuItem NavigateUrl="~/About.aspx" Text="Master Panel"/>
                    </Items>
                </asp:Menu>

              
     </div>
       </div>
       <div class="loginlink">
           <a class="loginanchor" href="">help</a>
           <a class="loginanchor" href="">Logout</a>
       </div>
     </div>
   
    <div class="horizontal_bar">
    </div>
    
      
       <div class="logintable">
       
       <table>
       <tr> <th colspan="2"> Credentials </th></tr>
       <tr>
       <td class="style2">Login:</td> 
       <td class="style1">
       <asp:TextBox runat="server"> </asp:TextBox>
       </td>
      </tr>
      
      <tr>
      <td class="style3">Password:</td>
      <td>
       <asp:TextBox runat="server"> </asp:TextBox>
       </td>
      </tr>
      <tr>
      <td></td>
      <td colspan="2">
      <asp:button ID="Button1" Text="Submit" runat="server"/> 
      </td>
      </tr>
      </table>
      
      </div>
      
    
       </form>
   
     </div>
 
    </div>
    
</body>
</html>
