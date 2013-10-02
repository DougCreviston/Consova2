<%@ Page Language="C#"   AutoEventWireup="true" CodeBehind="Campaign.aspx.cs" Inherits="Web_LynxClient.home" %>
<%@ Import Namespace="System.Web.Security" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Styles/BasicStyle.css" />
    <link rel="stylesheet" type="text/css" href="Styles/home.css" />
</head>
<body>
   <div class="body">
   <div class="page">
    <div class="header">
     <div class="LynxClient_title">
       <h1 class="version">CCI Text Master SMS </h1>
     </div>
     </div>


     <form id="form1" runat="server">
     
   
    <div class="horizontal_bar">
    
   
     <table>
     <tr>
       <td>
     <asp:Label ID="CompanyName" Text=" CompanyName " runat="server" />
     </td>
     </tr>
     <tr>
     <td>
     <asp:Label ID="textallowance" Text="TextAllowance" runat="server" /> 
     </td>
     </tr>
     </table>
   
    </div>
      <div class="control_panel">
     <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Vertical">
                    <Items>
                        <asp:MenuItem NavigateUrl="create_campaign.aspx" Text="Create New Campaign"/>
                        <asp:MenuItem NavigateUrl="Modify_Campaign.aspx" Text="Modify Existing Campaign"/>
                        <asp:MenuItem NavigateUrl="View_Campaign.aspx" Text="View Upcomming Campaign"/>
                        <asp:MenuItem NavigateUrl="Delete_Campaign.aspx" Text="Delete Campaign"/>
              
                    </Items>
                </asp:Menu>
    </div>
    
    
      
     <div class="label1div">
     <asp:Label runat="server" ID="Running_Campaign" Text="Running Campaigns"></asp:Label>
     </div>
     <div class="RCL">
     <asp:Label runat="server" ID="Running_Campaign_list" Text="Running Campaign list"></asp:Label>
     </div> 
     
     </form>
   
     </div>
 
    </div>
    
</body>
</html>
