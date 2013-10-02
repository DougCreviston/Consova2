<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="create_campaign.aspx.cs" Inherits="Web_LynxClient.create_campaign" %>
<%@ Import Namespace="System.Web.Security" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Styles/BasicStyle.css" />
    
    <link rel="stylesheet" type="text/css" href="Styles/new_campaign.css"/>
   
</head>
<body>
    <form id="form1" runat="server">
    <div class="body">
    <div class="page">
     <div class="header">
     <div class="LynxClient_title">
       <h1 class="version">CCI Text Master SMS </h1>
     </div>
     </div>
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
    <div class="page_title">
     <asp:Label runat="server" Text="New Campaign"></asp:Label>
    </div>

    <div class="campaign_name">
     <p>Campaign Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox id="campaign_name" runat="server"> </asp:TextBox>
        </p> 
    </div>

    <div class="campaign_start_time">
     <p>Campaign Start on:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox id="start_date" runat="server"> </asp:TextBox>
        <img src="img/icon.gif" /> &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox id="shr" runat="server" 
             Width="33px"></asp:TextBox> HR : &nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox id="smin" width="33px" runat="server"></asp:TextBox> MIN</p> 
        &nbsp;</div>

     <div class="campaign_end_time">
     <p>Campaign End on:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox id="TextBox1" runat="server"> </asp:TextBox>
        <img src="img/icon.gif" /> &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox id="TextBox2" runat="server" 
             Width="33px"></asp:TextBox> HR : &nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox id="TextBox3" width="33px" runat="server"></asp:TextBox> MIN</p> 
        &nbsp;</div>

    <div class="campaign_description">
    <asp:Label  runat="server" Text="Campaign Description" ></asp:Label>
    </div>

    <div class="campaign_description_text_area_div">
     <asp:TextBox id="campaign_description_text_area" runat="server" Height="200px" Width="400px" TextMode="MultiLine"></asp:TextBox>
    
    </div>

    <div class="opt_in_list_div">
      <p> opt in list  <asp:DropDownList id="opt_in_list" runat="server" ></asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="opt_check_box" runat="server"/>All </p>
    </div>

    <div class="pace_div">
      <p> PACE: &nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox runat="server" Width="33px" ID="pace"></asp:TextBox> / hour &nbsp;&nbsp;&nbsp;&nbsp; <asp:CheckBox ID="blast" runat="server" /> Blast</p>
    </div>

    <div class="SMS_Offer_label">
      <p> SMS Offer:</p>
    </div>
    <div class="SMS_Offer_Textbox">
    <p> <asp:TextBox runat="server" width="230px" height="66px" ID="TextBox5" TextMode="MultiLine"></asp:TextBox>  &nbsp;&nbsp;&nbsp;&nbsp;  <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;165 characters or less</p>
    </div>
    <div class="submit">
     <asp:Button id="b1" Text="Submit" runat="server" onclick="b1_Click" />
    </div>
    </div>
    </div>
    </form>
</body>
</html>
