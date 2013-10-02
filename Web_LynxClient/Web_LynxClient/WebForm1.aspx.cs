using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LynxLib;
using System.Data;


namespace Web_LynxClient
{
    public partial class WebForm1 : System.Web.UI.Page
    {
      

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void new_campaign_Click(object sender, EventArgs e)
        {
          
        }

        private bool Login(string login, string Password)
        {
            LDBSQL sql = new LDBSQL();
            string query = "select Name,permissions from users where Name='" + login + "' and Password='" + Password + "'" ;
            DataTable table = sql.query(query);

            return true;
        }
       

    }
}