using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using LynxLib;
using CCITexting;

namespace Web_LynxClient
{
  

    public partial class home : System.Web.UI.Page
    {
        MembershipUser currentUser;


        public void Page_Load()
        {
            LDBSQL sql = new LDBSQL();
            CCITextingSQL ccitsql = new CCITextingSQL();
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
            string UserId = "";
            string org="";
            string today = DateTime.Today.ToString("MM/dd/yyyy");
            string camp_name = "";
            string start_time = "";
            int count = 0;
            string rate = "";
            string sent = "";
            string output = "<table>";
            DataTable table = ccitsql.query("select UserId from aspnet_Users where UserName='mbrandenburg'");
            if (table.TableName != "Error")
            {
                if (table.Rows.Count != 0)
                {

                  //  ((Label)this.FindControl("Running_Campaign_list")).Text= table.Rows[0]["UserId"].ToString() ;
                      UserId = table.Rows[0]["UserId"].ToString();
                      table = ccitsql.query("select Organization from Organization_Membership where UserId='"+UserId+"'");
                      if (table.Rows.Count != 0)
                      {
                          if (table.Rows.Count != 0)
                          {
                              org = table.Rows[0]["Organization"].ToString();
                              table = sql.query("select Campaign_Name from Organization_Campaign where Organization_Name='" + table.Rows[0]["Organization"].ToString() + "'");
                              if (table.TableName != "Error")
                              {
                                  if (table.Rows.Count  != 0)
                                  {
                                      foreach (DataRow r in table.Rows)
                                      {
                                          DataTable campaigntable = sql.query("select * from Campaigns inner join Message_sent on Campaigns.ID=Message_sent.ID inner join Schedule on Campaigns.Schedule=Schedule.ID where Campaigns.Run=1 and Campaigns.Name='" + r["Campaign_Name"].ToString() + "'");
                                          if (campaigntable.TableName != "Error")
                                          {
                                              if (campaigntable.Rows.Count != 0)
                                              {
                                                  
                                                  camp_name = campaigntable.Rows[0]["Name"].ToString();
                                                  rate = campaigntable.Rows[0]["Rate"].ToString();
                                                  sent = campaigntable.Rows[0]["Numsent"].ToString();
                                                  for (int i = 1; i < 8; i++)
                                                  {
                                                      if (i == 1)
                                                      {
                                                          if (campaigntable.Rows[0]["Start_Date"].ToString() == today)
                                                          {
                                                              start_time = campaigntable.Rows[0]["Start_Time"].ToString();
                                                          }
                                                      }
                                                      else
                                                      {
                                                          if (campaigntable.Rows[0]["Start_Date"+i.ToString()].ToString() == today)
                                                          {
                                                              start_time = campaigntable.Rows[0]["Start_Time"+i.ToString()].ToString();
                                                          }
                                                      }

                                                
                                                  }
                                                  if (Convert.ToInt32(sent) != 0)
                                                  {
                                                      count += 1;
                                                      output += "<tr>";
                                                      output += "<td>" + org + "</td><td>" + camp_name + "</td><td>" + start_time + "</td><td> " + rate + "</td><td> " + sent + "</td><td></td><td><img src=\"img/graph.gif\" /></td><td></td><td><img src=\"img/folder.gif\" /></td>";
                                                      output += "</tr>";
                                                  }
                                              }
                                          }
                                      }
                                  }
                              }
                          }
                      }
                }
            }

            ((Label)this.FindControl("Running_Campaign")).Text = "Running Campaign|" + count.ToString() +" campaigns" ;
            count = 0;
            output += "<tr><td colspan=\"5\"> <p class=\"label2div\">Stopped Campaigns|XXXXXX </p><td></tr>";
            
            table = ccitsql.query("select UserId from aspnet_Users where UserName='mbrandenburg'");
            if (table.TableName != "Error")
            {
                if (table.Rows.Count != 0)
                {

                    //  ((Label)this.FindControl("Running_Campaign_list")).Text= table.Rows[0]["UserId"].ToString() ;
                    UserId = table.Rows[0]["UserId"].ToString();
                    table = ccitsql.query("select Organization from Organization_Membership where UserId='" + UserId + "'");
                    if (table.Rows.Count != 0)
                    {
                        if (table.Rows.Count != 0)
                        {
                            org = table.Rows[0]["Organization"].ToString();
                            table = sql.query("select Campaign_Name from Organization_Campaign where Organization_Name='" + table.Rows[0]["Organization"].ToString() + "'");
                            if (table.TableName != "Error")
                            {
                                if (table.Rows.Count != 0)
                                {
                                    foreach (DataRow r in table.Rows)
                                    {
                                        DataTable campaigntable = sql.query("select * from Campaigns inner join Message_sent on Campaigns.ID=Message_sent.ID inner join Schedule on Campaigns.Schedule=Schedule.ID where Campaigns.Run=0 and Campaigns.Name='" + r["Campaign_Name"].ToString() + "'");
                                        if (campaigntable.TableName != "Error")
                                        {
                                            if (campaigntable.Rows.Count != 0)
                                            {
                                               
                                                camp_name = campaigntable.Rows[0]["Name"].ToString();
                                                rate = campaigntable.Rows[0]["Rate"].ToString();
                                                sent = campaigntable.Rows[0]["Numsent"].ToString();
                                                for (int i = 1; i < 8; i++)
                                                {
                                                    if (i == 1)
                                                    {
                                                        if (campaigntable.Rows[0]["Start_Date"].ToString() == today)
                                                        {
                                                            start_time = campaigntable.Rows[0]["Start_Time"].ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (campaigntable.Rows[0]["Start_Date" + i.ToString()].ToString() == today)
                                                        {
                                                            start_time = campaigntable.Rows[0]["Start_Time" + i.ToString()].ToString();
                                                        }
                                                    }


                                                }
                                                if(Convert.ToDateTime(start_time) < DateTime.Now){
                                                    count += 1;
                                                output += "<tr>";
                                                output += "<td>" + org + "</td><td>" + camp_name + "</td><td>" + start_time + "</td><td> " + rate + "</td><td> " + sent + "</td><td></td><td><img src=\"img/graph.gif\" /></td><td></td><td><img src=\"img/folder.gif\" /></td>"; 
                                                output += "</tr>";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            output = output.Replace("XXXXXX", count.ToString() + " campaigns");
            count = 0;
            bool flag = false;
            output += "<tr><td colspan=\"5\"> <p class=\"label2div\">View Scheduled Campaigns|XXXXXX </p><td></tr>";

            table = ccitsql.query("select UserId from aspnet_Users where UserName='mbrandenburg'");
            if (table.TableName != "Error")
            {
                if (table.Rows.Count != 0)
                {

                    //  ((Label)this.FindControl("Running_Campaign_list")).Text= table.Rows[0]["UserId"].ToString() ;
                    UserId = table.Rows[0]["UserId"].ToString();
                    table = ccitsql.query("select Organization from Organization_Membership where UserId='" + UserId + "'");
                    if (table.Rows.Count != 0)
                    {
                        if (table.Rows.Count != 0)
                        {
                            org = table.Rows[0]["Organization"].ToString();
                            table = sql.query("select Campaign_Name from Organization_Campaign where Organization_Name='" + table.Rows[0]["Organization"].ToString() + "'");
                            if (table.TableName != "Error")
                            {
                                if (table.Rows.Count != 0)
                                {
                                    foreach (DataRow r in table.Rows)
                                    {
                                        DataTable campaigntable = sql.query("select * from Campaigns inner join Message_sent on Campaigns.ID=Message_sent.ID inner join Schedule on Campaigns.Schedule=Schedule.ID where Campaigns.Run=0 and Campaigns.Name='" + r["Campaign_Name"].ToString() + "'");
                                        if (campaigntable.TableName != "Error")
                                        {
                                            if (campaigntable.Rows.Count != 0)
                                            {
                                                camp_name = campaigntable.Rows[0]["Name"].ToString();
                                                rate = campaigntable.Rows[0]["Rate"].ToString();
                                                sent = campaigntable.Rows[0]["Numsent"].ToString();
                                                for (int i = 1; i < 8; i++)
                                                {
                                                    if (i == 1)
                                                    {
                                                        if (campaigntable.Rows[0]["Start_Date"].ToString() == today)
                                                        {
                                                            DateTime Start_Time = Convert.ToDateTime(campaigntable.Rows[0]["Start_Time"].ToString());
                                                            if(DateTime.Now < Start_Time){
                                                                start_time = campaigntable.Rows[0]["Start_Time"].ToString();
                                                                flag = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (campaigntable.Rows[0]["Start_Date" + i.ToString()].ToString() == today)
                                                        {
                                                            DateTime Start_Time = Convert.ToDateTime(campaigntable.Rows[0]["Start_Time" + i.ToString()].ToString());
                                                            if (DateTime.Now < Start_Time)
                                                            {
                                                                start_time = campaigntable.Rows[0]["Start_Time" + i.ToString()].ToString();
                                                                flag = true;
                                                            }
                                                        }
                                                    }


                                                }
                                                if (flag)
                                                {
                                                    count += 1;
                                                    output += "<tr>";
                                                    output += "<td>" + org + "</td><td>" + camp_name + "</td><td>" + start_time + "</td><td> " + rate + "</td><td> " + sent + "</td><td></td><td><img src=\"img/graph.gif\" /></td><td></td><td><img src=\"img/folder.gif\" /></td>";
                                                    output += "</tr>";
                                                }
                                                flag = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            


            output += "</table>";
            output = output.Replace("XXXXXX", count.ToString()+ " campaigns");
            ((Label)this.FindControl("Running_Campaign_list")).Text = output;
        }


      

       
    }
}