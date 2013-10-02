<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View_Campaign.aspx.cs" Inherits="Web_LynxClient.View_Campaign" %>
<%@ Import Namespace="System.Web.Security" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Styles/BasicStyle.css" />
     <link rel="stylesheet" type="text/css" href="Styles/view_campaign.css" />
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

    <div class="upcoming_campaigns_div">
    <asp:Label runat="server" CssClass="" id="page_title" Text="Upcomming Campaigns"></asp:Label>
    
    </div>
    <div class="upcoming_campaigns_List_div">
    
    <asp:Label runat="server" CssClass="" id="campaign_table" Text="campaign_table"></asp:Label>
    </div>
    <div class="view_upcomming_campaign_div">
     <p>view upcomming campaigns:<br /><asp:DropDownList runat="server" id="upcomming_list"></asp:DropDownList></p>
    </div>
    </form>
    </div>
   </div>
</body>
</html>
