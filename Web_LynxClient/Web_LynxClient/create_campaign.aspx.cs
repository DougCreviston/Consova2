using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Web_LynxClient
{
    public partial class create_campaign : System.Web.UI.Page
    {
        MembershipUser currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*

            try
            {
                currentUser = Membership.GetUser();
            }
            catch (ArgumentException e)
            {
                Response.Redirect("login.aspx");
            }

            string username = currentUser.UserName; */


        }

        protected void b1_Click(object sender, EventArgs e)
        {
            //textboxes 
            string campaign_name = ((TextBox)this.FindControl("campaign_name")).Text;
            string start_date = ((TextBox)this.FindControl("start_date")).Text;
            string shr = ((TextBox)this.FindControl("shr")).Text;
            string smin = ((TextBox)this.FindControl("smin")).Text;
            string TextBox1 = ((TextBox)this.FindControl("TextBox1")).Text;
            string TextBox2 = ((TextBox)this.FindControl("TextBox2")).Text;
            string TextBox3 = ((TextBox)this.FindControl("TextBox3")).Text;
            string campaign_description_text_area = ((TextBox)this.FindControl("campaign_description_text_area")).Text;
            string pace = ((TextBox)this.FindControl("pace")).Text;
            string TextBox5 = ((TextBox)this.FindControl("pace")).Text; //SMS OFFER

            //drop down menu
            string opt_in_list = ((DropDownList)this.FindControl("opt_in_list")).Text;

            //checkbox
            bool opt_in_checkbox =((CheckBox) this.FindControl("opt_in_checkbox")).Checked
        }
    }
}