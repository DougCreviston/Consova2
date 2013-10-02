using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using LynxLib;



namespace LynxClient
{
    class CampaignProcessor
    { 
        string command = "";
        Main Parent;
        public CampaignProcessor(string cmd, Main p)
        {
            this.command = cmd;
            this.Parent = p;

        }
        public void Process()
        {
            //Parent.WriteToConsole("test", 0);
            if (Parse())
            {
                Parent.DialogShow("Campaign " + command + " Successful.");
            }
            else
            {

            }
        }
        private bool Parse()
        {
            try
            {
                switch (command)
                {
                    case "Add":
                        return AddCampaign();
                    case "Delete":
                        return DelCampaign();
                    case "Modify":
                        return ModCampaign();
                    case "Sched":
                        return SelectCampaignSchedule();
                    case "SaveSched":
                        return SaveSelectedCampainSchedule();
                    case "CreateSched":
                        return CreateSched();
                    case "DelSch":
                        return DelSched();
                       
                    default:
                        return false;
                }
            }

            catch (Exception e)
            {
                Parent.WriteToConsole(e.Message, 0);
                return false;
            }
        }
        private bool AddCampaign()
        {
            try
            {
                LDBSQL SQL = new LDBSQL();
                if ((SQL.edit("INSERT INTO Campaigns (Name,Sources,StateDeletes,Schedule,Message,Rate,Run,LiveID,LivePC,LDB,Inactive,hasBadCarriers) VALUES ('" + Parent.pGetCmpAddName() + "','" + Parent.pGetCmpAddLS() + "','" + Parent.pGetCmpAddSD() + "'," + Parent.pGetCmpAddSch() + "," + Parent.pGetCmpAddMsg() + "," + 1 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ","+Parent.pGetBCRadioButton01()+ ")") == 1))
                {
                  
                    
                    Parent.WriteToConsole("Campaign Added: " + Parent.cmpAddName.Text, 1);
                    Parent.myUpdateCmpTabs();

                    DataTable StateDeletes_DataTable = SQL.query("SELECT INAreaCodes FROM StateAreaCodes WHERE ID IN (" + Parent.pGetCmpAddSD() + ")");
                    string[] statedelete_area_codes = { };

                    char[] trim = { ',' };
                    if (StateDeletes_DataTable.TableName != "Error")
                    {
                        for (int x = 0; x < StateDeletes_DataTable.Rows.Count; x++)
                        {


                            string[] area_arry = StateDeletes_DataTable.Rows[x]["INAreaCodes"].ToString().Split(trim);
                            for (int i = 0; i < area_arry.Length; i++)
                            {

                                SQL.edit("UPDATE Leads  SET CampId =" + Parent.pGetCmpAddName() + ", QueueTime='" + DateTime.Now + "' WHERE area_code = " + area_arry[i] + " and FileID in (SELECT ID FROM Imported WHERE LSID IN (" + Parent.pGetCmpAddLS() + "))");
                            }
                        }
                    }

                    return true;
            }
                else
                {
                    Parent.WriteToConsole("Campaign Add Failed: " + Parent.cmpAddName.Text, 1);
                    
                    return false;
                }

                
            }
            catch (Exception e)
            {
                Parent.WriteToConsole(e.Message, 0);
                return false;
            }
            
        }
        private bool DelCampaign()
        {
            try
            {
             
                LDBSQL SQL = new LDBSQL();
                if (SQL.edit("UPDATE Campaigns SET Inactive= 1 where ID="+ Parent.pGetID()) == 1)
                {
                    
                    
                    
                    Parent.WriteToConsole("Campaign Added: " + Parent.cmpAddName.Text, 1);
                    Parent.myUpdateCmpTabs();
                   // Parent.updateTabs();
                    return true;
                }
                else
                {
                   // Parent.prefreshcmbDelbox1();
                    Parent.WriteToConsole("Campaign Add Failed: " + Parent.cmpAddName.Text, 1);
                   // Parent.updateTabs();
                    return false;
                }

       

              //  System.IO.File.WriteAllText(@"C:\Users\doug\Documents\Visual Studio 2010\Projects\textfiles\IDS.txt", Parent.pGetID());
               // return true;
            }
            catch (Exception e)
            {
                Parent.WriteToConsole(e.Message, 0);
                return false;
            }

        }
        private bool ModCampaign()
        {
            try
            {
                LDBSQL SQL = new LDBSQL();
                string str = "UPDATE Campaigns SET Sources='" + Parent.pGetCmpModSource() + "', StateDeletes='" + Parent.pGetCmpModSD() + "', Message='" + Parent.pGetCmpModMessage() + "', Schedule='" + Parent.pGetCmpModSch()+ "', hasBadCarriers="+Parent.pGetBCRadioButton02()+"   where ID=" + Parent.pGetModID();
                if ((SQL.edit(str) == 1))
                {
                    Parent.WriteToConsole("Campaign Added: " + Parent.cmpAddName.Text, 1);
                   // Parent.updateTabs();

                    DataTable StateDeletes_DataTable = SQL.query("SELECT INAreaCodes FROM StateAreaCodes WHERE ID IN (" + Parent.pGetCmpModSD() + ")");
                    string[] statedelete_area_codes = { };

                    char[] trim = { ',' };
                    if (StateDeletes_DataTable.TableName != "Error")
                    {
                        for (int x = 0; x < StateDeletes_DataTable.Rows.Count; x++)
                        {


                            string[] area_arry = StateDeletes_DataTable.Rows[x]["INAreaCodes"].ToString().Split(trim);
                            for (int i = 0; i < area_arry.Length; i++)
                            {

                                SQL.edit("UPDATE Leads  SET CampId =" + Parent.pGetModID() + ", QueueTime='" + DateTime.Now + "' WHERE area_code = " + area_arry[i] + " and FileID in (SELECT ID FROM Imported WHERE LSID IN (" + Parent.pGetCmpModSource() + "))");
                            }
                        }
                    }


                    return true;
                }
                else
                {
                    //Parent.updateTabs();
                    Parent.WriteToConsole("Campaign Add Failed: " + Parent.cmpAddName.Text, 1);
                    return false;
                }



                
            }
            catch (Exception e)
            {
                Parent.WriteToConsole(e.Message, 0);
                return false;
            }



        }

        private bool CreateSched()
        {
            try
            {
                LDBSQL SQL = new LDBSQL();
                string str = "INSERT INTO Schedule (Start_Date,Start_Time,End_Date,End_Time,Start_Date2,Start_Time2,End_Date2,End_Time2,Start_Date3,Start_Time3,End_Date3,End_Time3,Start_Date4,Start_Time4,End_Date4,End_Time4,Start_Date5,Start_Time5,End_Date5,End_Time5,Start_Date6,Start_Time6,End_Date6,End_Time6,Start_Date7,Start_Time7,End_Date7,End_Time7,repeat,Enable1,Enable2,Enable3,Enable4,Enable5,Enable6,Enable7,Inactive,Name)  VALUES ('" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + DateTime.Today.ToString("MM/dd/yyyy") + "','" + DateTime.Now.ToString("hh:mm tt") + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ",'" + Parent.pGetschname() + "')";
                if ((SQL.edit("INSERT INTO Schedule (Start_Date,Start_Time,End_Date,End_Time,Start_Date2,Start_Time2,End_Date2,End_Time2,Start_Date3,Start_Time3,End_Date3,End_Time3,Start_Date4,Start_Time4,End_Date4,End_Time4,Start_Date5,Start_Time5,End_Date5,End_Time5,Start_Date6,Start_Time6,End_Date6,End_Time6,Start_Date7,Start_Time7,End_Date7,End_Time7,repeat,Enable1,Enable2,Enable3,Enable4,Enable5,Enable6,Enable7,Inactive,Name)  VALUES ('" + Parent.pGetSchedCmpStartDate() + "','" + Parent.pGetSchedcmpStartTime() + "','" + Parent.pGetSchedCmpEndDate() + "','" + Parent.pGetSchedcmpEndTime() + "','" + Parent.pGetSchedCmpStartDate2() + "','" + Parent.pGetSchedcmpStartTime2() + "','" + Parent.pGetSchedCmpEndDate2() + "','" + Parent.pGetSchedcmpEndTime2() + "','" + Parent.pGetSchedCmpStartDate3() + "','" + Parent.pGetSchedcmpStartTime3() + "','" + Parent.pGetSchedCmpEndDate3() + "','" + Parent.pGetSchedcmpEndTime3() + "','" + Parent.pGetSchedCmpStartDate4() + "','" + Parent.pGetSchedcmpStartTime4() + "','" + Parent.pGetSchedCmpEndDate4() + "','" + Parent.pGetSchedcmpEndTime4() + "','" + Parent.pGetSchedCmpStartDate5() + "','" + Parent.pGetSchedcmpStartTime5() + "','" + Parent.pGetSchedCmpEndDate5() + "','" + Parent.pGetSchedcmpEndTime5() + "','" + Parent.pGetSchedCmpStartDate6() + "','" + Parent.pGetSchedcmpStartTime6() + "','" + Parent.pGetSchedCmpEndDate6() + "','" + Parent.pGetSchedcmpEndTime6() + "','" + Parent.pGetSchedCmpStartDate7() + "','" + Parent.pGetSchedcmpStartTime7() + "','" + Parent.pGetSchedCmpEndDate7() + "','" + Parent.pGetSchedcmpEndTime7() + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + ",'" + Parent.pGetschname() + "')") == 1))
                {
                    Parent.myUpdateCmpTabs();
                    Parent.WriteToConsole("Schedule Added: " + Parent.cmpAddName.Text, 1);
                    return true;
                }
                else
                {
                  //  Parent.updateTabs();
                    Parent.WriteToConsole("Schedule Add Failed: " + Parent.cmpAddName.Text, 1);
                    return false;
                }
            }
            catch (Exception e)
            {
                Parent.WriteToConsole(e.Message, 0);
                return false;
            }

            return true;
        }

        private bool SaveSelectedCampainSchedule()
        {
            try
            {
               

                LDBSQL SQL = new LDBSQL();

                /*if ((SQL.edit("UPDATE Schedule SET Start_Date ='" + Parent.pGetSchedcmpStartTime() + "', Start_Time = '" + Parent.pGetSchedCmpStartDate() + "',End_Date = '" + Parent.pGetSchedCmpEndDate() + "',End_Time ='" + Parent.pGetSchedcmpEndTime() + "',repeat = " + Parent.pGetrepeat1() + " ,weekly= " + Parent.pGetWeekly1() + " WHERE ID = '" + Parent.pGetSchedID()+"'") == 1))
                {
                    Parent.WriteToConsole("Campaign Added: " + Parent.cmpAddName.Text, 1);
                    return true;
                }
                else
                {
                    Parent.WriteToConsole("Campaign Add Failed: " + Parent.cmpAddName.Text, 1);
                    return false;
                }*/
                Parent.WriteToConsole("UPDATE Schedule SET Start_Date ='" + Parent.pGetSchedCmpStartDatea() + "', Start_Time = '" + Parent.pGetSchedcmpStartTimea() + "',End_Date = '" + Parent.pGetSchedCmpEndDatea() + "',End_Time ='" + Parent.pGetSchedcmpEndTimea() + "',Start_Date2 ='" + Parent.pGetSchedCmpStartDate2a() + "', Start_Time2 = '" + Parent.pGetSchedcmpStartTime2a() + "',End_Date2 = '" + Parent.pGetSchedCmpEndDate2a() + "',End_Time2 ='" + Parent.pGetSchedcmpEndTime2a() + "', Start_Date3 ='" + Parent.pGetSchedCmpStartDate3a() + "', Start_Time3 = '" + Parent.pGetSchedcmpStartTime3a() + "',End_Date3 = '" + Parent.pGetSchedCmpEndDate3a() + "',End_Time3 ='" + Parent.pGetSchedcmpEndTime3a() + "',Start_Date4 ='" + Parent.pGetSchedCmpStartDate4a() + "', Start_Time4 = '" + Parent.pGetSchedcmpStartTime4a() + "' ,End_Date4 = '" + Parent.pGetSchedCmpEndDate4a() + "',End_Time4 ='" + Parent.pGetSchedcmpEndTime4a() + "',Start_Date5 ='" + Parent.pGetSchedCmpStartDate5a() + "', Start_Time5 = '" + Parent.pGetSchedcmpStartTime5a() + "',End_Date5 = '" + Parent.pGetSchedCmpEndDate5a() + "',End_Time5 ='" + Parent.pGetSchedcmpEndTime5a() + "',Start_Date6 ='" + Parent.pGetSchedCmpStartDate6a() + "', Start_Time6 = '" + Parent.pGetSchedcmpStartTime6a() + "',End_Date6 = '" + Parent.pGetSchedCmpEndDate6a() + "',End_Time6 ='" + Parent.pGetSchedcmpEndTime6a() + "',Start_Date7 ='" + Parent.pGetSchedCmpStartDate7a() + "', Start_Time7 = '" + Parent.pGetSchedcmpStartTime7a() + "',End_Date7 = '" + Parent.pGetSchedCmpEndDate7a() + "',End_Time7 ='" + Parent.pGetSchedcmpEndTime7a() + "',repeat=" + Parent.pGetrepeat1a() + ",Enable1=" + Parent.pGetcheckBox1a() + ",Enable2=" + Parent.pGetcheckBox2a() + ",Enable3=" + Parent.pGetcheckBox3a() + ",Enable4=" + Parent.pGetcheckBox4a() + ",Enable5=" + Parent.getCheckBox5a() + ",Enable6=" + Parent.getCheckBox6a() + ",Enable7=" + Parent.getCheckBox7a() + " WHERE ID = '" + Parent.pGetSchedID() + "'", 1);
                SQL.edit("UPDATE Schedule SET Start_Date ='" + Parent.pGetSchedCmpStartDatea() + "', Start_Time = '" + Parent.pGetSchedcmpStartTimea() + "',End_Date = '" + Parent.pGetSchedCmpEndDatea() + "',End_Time ='" + Parent.pGetSchedcmpEndTimea() + "',Start_Date2 ='" + Parent.pGetSchedCmpStartDate2a() + "', Start_Time2 = '" + Parent.pGetSchedcmpStartTime2a() + "',End_Date2 = '" + Parent.pGetSchedCmpEndDate2a() + "',End_Time2 ='" + Parent.pGetSchedcmpEndTime2a() + "', Start_Date3 ='" + Parent.pGetSchedCmpStartDate3a() + "', Start_Time3 = '" + Parent.pGetSchedcmpStartTime3a() + "',End_Date3 = '" + Parent.pGetSchedCmpEndDate3a() + "',End_Time3 ='" + Parent.pGetSchedcmpEndTime3a() + "',Start_Date4 ='" + Parent.pGetSchedCmpStartDate4a() + "', Start_Time4 = '" + Parent.pGetSchedcmpStartTime4a() + "' ,End_Date4 = '" + Parent.pGetSchedCmpEndDate4a() + "',End_Time4 ='" + Parent.pGetSchedcmpEndTime4a() + "',Start_Date5 ='" + Parent.pGetSchedCmpStartDate5a() + "', Start_Time5 = '" + Parent.pGetSchedcmpStartTime5a() + "',End_Date5 = '" + Parent.pGetSchedCmpEndDate5a() + "',End_Time5 ='" + Parent.pGetSchedcmpEndTime5a() + "',Start_Date6 ='" + Parent.pGetSchedCmpStartDate6a() + "', Start_Time6 = '" + Parent.pGetSchedcmpStartTime6a() + "',End_Date6 = '" + Parent.pGetSchedCmpEndDate6a() + "',End_Time6 ='" + Parent.pGetSchedcmpEndTime6a() + "',Start_Date7 ='" + Parent.pGetSchedCmpStartDate7a() + "', Start_Time7 = '" + Parent.pGetSchedcmpStartTime7a() + "',End_Date7 = '" + Parent.pGetSchedCmpEndDate7a() + "',End_Time7 ='" + Parent.pGetSchedcmpEndTime7a() + "',repeat="+ Parent.pGetrepeat1a() + ",Enable1="+Parent.pGetcheckBox1a()+",Enable2="+Parent.pGetcheckBox2a()+",Enable3="+Parent.pGetcheckBox3a()+",Enable4="+Parent.pGetcheckBox4a()+",Enable5="+Parent.getCheckBox5a()+",Enable6="+Parent.getCheckBox6a()+",Enable7="+Parent.getCheckBox7a()+" WHERE ID = '" + Parent.pGetSchedID() + "'");
             //   Parent.updateTabs();
                return true;

            }
            catch (Exception e)
            {
                Parent.WriteToConsole(e.Message, 0);
                return false;
            }
        }

        private bool SelectCampaignSchedule()
        {
        
            try
            {
                LDBSQL SQL = new LDBSQL();

              /*  if (SQL.edit("SELECT * FROM Schedule WHERE ID=" + Parent.pGetSchedID()) == 1)
                {
                    Parent.WriteToConsole("Campaign Added: " + Parent.cmpAddName.Text, 1);
                }
                else
                {
                    Parent.WriteToConsole("Schedule Failed: " + Parent.cmpAddName.Text, 1);
                    return false;
                }*/
              //  System.IO.File.WriteAllText(@"C:\Users\doug\Documents\Visual Studio 2010\Projects\textfiles\IDS.txt", Parent.pGetSchedID());
                DataTable table = new DataTable();
                string str = "SELECT Start_time,Start_date, End_time, End_date, Start_time2, Start_date2, End_time2, End_date2, Start_time3, Start_date3, End_time3, End_date3, Start_time4, Start_date4, End_time4, End_date4, Start_time5, Start_date5, End_time5, End_date5, Start_time6, Start_date6, End_time6, End_date6, Start_time7, Start_date7, End_time7, End_date7, repeat, Enable1, Enable2, Enable3, Enable4, Enable5, Enable6, Enable7, MaxText FROM Schedule WHERE ID=" + Parent.pGetSchedID();
                table = SQL.query("SELECT Start_time,Start_date, End_time, End_date, Start_time2, Start_date2, End_time2, End_date2, Start_time3, Start_date3, End_time3, End_date3, Start_time4, Start_date4, End_time4, End_date4, Start_time5, Start_date5, End_time5, End_date5, Start_time6, Start_date6, End_time6, End_date6, Start_time7, Start_date7, End_time7, End_date7, repeat, Enable1, Enable2, Enable3, Enable4, Enable5, Enable6, Enable7 FROM Schedule WHERE ID=" + Parent.pGetSchedID());
                foreach (DataRow r in table.Rows)
                {
                    Parent.setSchedulesettings(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(), r[6].ToString(), r[7].ToString(), r[8].ToString(), r[9].ToString(), r[10].ToString(), r[11].ToString(), r[12].ToString(), r[13].ToString(), r[14].ToString(), r[15].ToString(), r[16].ToString(), r[17].ToString(), r[18].ToString(), r[19].ToString(), r[20].ToString(), r[21].ToString(), r[22].ToString(), r[23].ToString(), r[24].ToString(), r[25].ToString(), r[26].ToString(), r[27].ToString(), (int)r[28], (int)r[29], (int)r[30], (int)r[31], (int)r[32], (int)r[33], (int)r[34], (int)r[35]);
                }
               // Parent.updateTabs();
                return true;
            }
            catch (Exception e)
            {
                Parent.WriteToConsole(e.Message, 0);
                return false;
            }

        }

        private bool DelSched()
        {

            try
            {
                LDBSQL SQL = new LDBSQL();
                string query = "UPDATE Schedule Set Inactive=1 where ID='" + Parent.pGetSchedID() + "'";
                if (SQL.edit(query) == 1)
                {
                    Parent.myUpdateCmpTabs();
                }
            }
            catch
            {
            }
            return true;
        }
    }
}
