using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SMSLib;
using LynxLib;

using System.Data;

namespace LynxServer
{
    class MsgManager
    {
        private volatile bool _isWorking;
        int camp;
        Main Main;
        CampManager Parent;
        private Object thisLock = new Object();


        public MsgManager(Main myform)
        {
            Main = myform;
        }

        public void getCamp(int ID)
        {
            this.camp = ID;
        }

        public void getParent(CampManager p)
        {
            Parent = p;
        }

        public void requeststop()
        {
            _isWorking = true;
        }
        public void MsgStart()
        {
            DateTime now1 = DateTime.Now;
            LDBSQL SQL = new LDBSQL();
            Stopwatch stopwatch = new Stopwatch();

            SendSMS SMS = new SendSMS();
            DataTable LS = new DataTable();
            Hashtable heap = new Hashtable();
            DataTable StateDeletes_DataTable = new DataTable();
            DataTable TimeZone_Area_codes = new DataTable();
            DataTable MessageTable = new DataTable();
            DataTable Record = new DataTable();
            DataTable SentMessages = new DataTable();
            int run = 1;
            int i = 0;
            int LeadID = 0;
            int FileID = 0;
            int messageID = 0;
            string leads = "";

            string LiveID = "";
            string LivePC = "";
            int sent = 0;
            int file_sent = 0;
            string StateDeletes = "";
            bool AreRows = true;
            string time="";
            
            while (AreRows)
            {
                LeadID = 0;
                LiveID = "";
                LivePC = "";

                LS = SQL.query("SELECT * FROM Campaigns WHERE ID = '" + camp.ToString() + "'");
                string str22 = "SELECT INAreaCodes FROM StateAreaCodes WHERE ID NOT IN (" + LS.Rows[0]["StateDeletes"] + ")";
                StateDeletes_DataTable = SQL.query("SELECT INAreaCodes FROM StateAreaCodes WHERE ID NOT IN (" + LS.Rows[0]["StateDeletes"] + ")");
                SentMessages = SQL.query("SELECT Numsent FROM Message_Sent where ID='" + camp.ToString() + "'");
                sent = Convert.ToInt32(SentMessages.Rows[0]["Numsent"].ToString());
                if (LS.TableName != "Error")
                {
                    messageID = (int)LS.Rows[0]["Message"];
                    leads = LS.Rows[0]["Sources"].ToString();
                    run = Int32.Parse(LS.Rows[0]["Run"].ToString());
                    LiveID = LS.Rows[0]["LiveID"].ToString();
                    LivePC = LS.Rows[0]["LivePC"].ToString();

                    char[] Delimiter = { ',' };
                    string[] lead_array = leads.Split(Delimiter);
                    leads = "";
                    for (int x = 0; x < lead_array.Length; x++)
                    {
                        leads += "'";
                        leads += lead_array[x];
                        leads += "'";
                        leads += ',';
                    }
                    if ((leads.Length > 0) && leads[leads.Length - 1] != '\'')
                    {
                        leads = leads.Substring(0, leads.Length - 1);
                    }
                    /*
                    if (60 < Convert.ToInt16(LS.Rows[0]["Rate"].ToString()))
                    {
                   
                    }
                    else
                    {*/
                        
                   
                    // }
                   
                    Thread.Sleep(Int32.Parse(Math.Ceiling((((60 / Decimal.Parse(LS.Rows[0]["Rate"].ToString()))) * 1000)).ToString()));
                    MessageTable = SQL.query("SELECT Message FROM Messages where ID=" + messageID);

                    if (run == 1 )
                    {
                        bool send = true;
                   /*     string[] statedelete_area_codes = { };
                        if (StateDeletes_DataTable.TableName != "Error")
                        {
                            for (int x = 0; x < StateDeletes_DataTable.Rows.Count; x++)
                            {
                                StateDeletes = StateDeletes + StateDeletes_DataTable.Rows[x]["INAreaCodes"].ToString();
                                StateDeletes = StateDeletes + ',';
                            }
                            char[] trim = { ',' };
                            StateDeletes = StateDeletes.TrimEnd(trim);
                            //string[] non_SD_area_codes = StateDeletes.Split(',');
                        }*/
                       
                        
                       // bool flag = false;
                       // char[] delimitors = { '\'' };
                        /*
                          for (int x = 0; x < statedelete_area_codes.Count() && !flag; x++)
                          {
                              string phone = Record.Rows[0]["Telephone_Number"].ToString().Substring(0, 3);
                              string areacode = statedelete_area_codes[x];
                              areacode = areacode.TrimStart(delimitors);
                              areacode = areacode.TrimEnd(delimitors);
                              //flag = (String.Compare(phone, areacode) == 0);
                          }*/

                       // DateTime GMT = DateTime.Now.ToUniversalTime();
                     //   string early_time_string = "7:00:00 AM";
                      //  string late_time_string = "9:00:00 PM";
                       // DateTime acceptable_morning_time = Convert.ToDateTime(early_time_string);
                       // DateTime acceptable_Night_time = Convert.ToDateTime(late_time_string);
                      //  DataTable areacodeTZ = SQL.query("SELECT * FROM AreaCodeTZ");
                       // string goodareacodes = "";
                        //string areacodes = "";
                        //int offset = 0;
                        /*foreach (DataRow r in areacodeTZ.Rows)
                        {
                            string stroffset = r["Offset"].ToString();
                            offset = Convert.ToInt32(stroffset);
                            areacodes = r["AreaCode"].ToString();
                            bool early = (DateTime.Compare(acceptable_morning_time, GMT.AddHours(offset)) < 0);
                            bool late = (DateTime.Compare(acceptable_Night_time, GMT.AddHours(offset)) > 0);
                            if ((early) && ((late)))
                            {

                                goodareacodes += "'";
                                goodareacodes += areacodes;
                                goodareacodes += "'";
                                goodareacodes += ',';

                            }
                          /*  if ((goodareacodes.Length > 0) && goodareacodes[goodareacodes.Length - 1] != '\'')
                            {
                                goodareacodes = goodareacodes.Substring(0, goodareacodes.Length - 1);
                            }
                          

                        }*/
                        // GMT = GMT.AddHours(offset);
                        char[] trim2 = { ',' };
                        //goodareacodes = goodareacodes.TrimEnd(trim2);

                        SMS.sourceNum = "75612";
                        /*
                        string str = "SELECT  ID, Telephone_Number FROM Leads WHERE CampId = 0  and FileID in (" + leads + ") and left(Telephone_Number,3) in (" + StateDeletes + ") Order By DateLoaded DESC";
                        Record = SQL.query(str);*/
                        if(Parent.Size()){
                        DataRow r=Parent.dequeueLeadList();
                   
                     //   Record = SQL.query("SELECT Top 1 ID, Telephone_Number FROM vwLead WHERE FileID in (" + leads + ") and left(Telephone_Number,3) in (" + StateDeletes + ") and (left(Telephone_Number,3)  in (" + goodareacodes + ") or left(Telephone_Number,3) not in(select distinct areacode from areacodetz))Order By DateLoaded DESC");
                        if (Record.TableName != "Error")
                        {
                            if (r["Telephone_Number"].ToString() == "")
                            {
                                AreRows = false;
                                SMS.PhoneNumber = "";
                            }
                            else
                            {
                               // Thread.Sleep(Int32.Parse(Math.Ceiling((((60 / Decimal.Parse(LS.Rows[0]["Rate"].ToString()))) * 600)).ToString()));
                                //foreach (DataRow r in Record.Rows)
                                //{
                                    DataTable LS2 = SQL.query("SELECT Rate FROM Campaigns WHERE ID = '" + camp.ToString() + "'");
                                  /*  if (time != LS.Rows[0]["Rate"].ToString())
                                    {
                                        decimal figure1 = 50 / Decimal.Parse(LS.Rows[0]["Rate"].ToString());
                                        decimal figure2 = ((1 - ((Decimal.Parse(LS.Rows[0]["Rate"].ToString()) * ((Convert.ToDecimal(.007))))))) * 1000;
                                        time = (Math.Ceiling(figure1 * figure2)).ToString();
                                    }
                                    Thread.Sleep(Int32.Parse(time));*/
                                    Thread.Sleep(Int32.Parse(Math.Ceiling((((60 / Decimal.Parse(LS2.Rows[0]["Rate"].ToString()))) * 1000)).ToString()));
                                  
                                    LeadID = Int32.Parse(r["ID"].ToString());
                                    //FileID = Int32.Parse(Record.Rows[0]["FileID"].ToString());
                                    //if (!heap.ContainsKey(FileID))
                                    // {
                                    //   heap.Add(FileID, 0);
                                    // }
                                    //LeadAbbr = Record.Rows[0]["LeadAbbr"].ToString();
                                   // SQL.edit("UPDATE Leads  SET CampId =" + camp.ToString() + "WHERE ID = " + LeadID);
                                    SMS.PhoneNumber = "+1" + r["Telephone_Number"].ToString();



                                    





                                    if (SMS.PhoneNumber != "")
                                    {
                                        SMSSQL smsSQL = new SMSSQL();
                                        DataTable Record1;
                                        string str = "SELECT CarrierID From preview WHERE TelephoneNumber = '" + SMS.PhoneNumber + "' Order By ID Desc";
                                        Record1 = smsSQL.query("SELECT CarrierID From preview WHERE TelephoneNumber = '" + SMS.PhoneNumber + "' Order By ID Desc");
                                        if (Record1.TableName != "Error")
                                        {
                                            if (Record.Rows.Count > 0)
                                            {
                                                SMS.carrier = Record1.Rows[0]["CarrierID"].ToString();
                                            }
                                            else
                                            {
                                                if (SMS.carrier == "")
                                                {
                                                    SMS.preview();
                                                    string str2 = "SELECT CarrierID From preview WHERE TelephoneNumber = '" + SMS.PhoneNumber + "' Order By ID Desc";
                                                    Record1 = smsSQL.query(str2);
                                                    if (Record1.TableName != "Error")
                                                    {

                                                        if (Record1.Rows.Count > 0)
                                                        {
                                                            SMS.carrier = Record1.Rows[0]["CarrierID"].ToString();

                                                            if (SMS.carrier == "")
                                                            {
                                                                SQL.edit("UPDATE Leads  SET CampId =-1 WHERE ID = " + LeadID);
                                                                send = false;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    /*else
                                    {
                                        send = false;
                                    }
                                    */
                                    if ((MessageTable.Rows[0]["Message"].ToString() == ""))
                                    {
                                        send = false;
                                    }
                                    else
                                    {
                                        SMS.xmessage = LS.Rows[0]["Message"].ToString();
                                    }
                                    if (send)
                                    {
                                        SQL.edit("UPDATE Leads  SET QueueTime='" + DateTime.Now + "' WHERE ID = " + LeadID);
                                        i++;
                                        //Message MSG = new Message();

                                        LDBSQL ldb = new LDBSQL();
                                        //         file_sent=Convert.ToInt32(heap[FileID]);
                                        sent++;
                                        //        file_sent++;
                                        //        heap[FileID] = file_sent;
                                        lock (thisLock)
                                        {
                                            ldb.edit("UPDATE Message_Sent SET Numsent=" + sent.ToString() + " where ID='" + camp.ToString() + "'");

                                            //    String Metrics_str = "UPDATE Metrics SET total_messages_sent=" + file_sent.ToString() + "  where CampID='" + camp.ToString() + "' and  FileID='" + FileID.ToString() + "' and convert(char(10),date_sent,101) = '" + DateTime.Today.ToString("MM/dd/yyyy") + "'";
                                            //    ldb.edit(Metrics_str);
                                            // string query = "UPDATE Metrics SET total_messages_sent="+file_sent+" where CampID="+camp+"and FileID="+FileID;
                                            //ldb.edit(query);

                                            Main.getMessageQueue().QueueSMS(camp, SMS.PhoneNumber, MessageTable.Rows[0]["Message"].ToString(), SMS.carrier, SMS.sourceNum);
                                        }

                                        //   Main.getMessageQueue().QueueSMS(camp, SMS.PhoneNumber, MessageTable.Rows[0]["Message"].ToString(), "77", "75612");



                                    }
                                }
                            }
                        }

                        else if (LS.Rows[0]["LDB"].ToString() == "LiveTextLDB")
                        {

                            SMS.sourceNum = "75612";
                            if (LivePC == "ALL" || LivePC == "")
                            {
                                Record = SQL.query("SELECT Top 1 ID, PhoneNumber, Received FROM LiveLead WHERE SENT IS NULL Order By Received DESC");
                            }
                            else
                            {
                                Record = SQL.query("SELECT Top 1 ID, PhoneNumber, Received FROM LiveLead WHERE SENT IS NULL AND SourcePC IN (" + LivePC + ") Order By Received DESC");
                            }
                            if (Record.TableName != "Error")
                            {
                                if (Record.Rows.Count == 0)
                                {
                                    Thread.Sleep(30000);
                                    send = false;
                                }
                                else
                                {
                                    LeadID = Int32.Parse(Record.Rows[0]["ID"].ToString());

                                    SMS.PhoneNumber = "+1" + Record.Rows[0]["PhoneNumber"].ToString();
                                }

                            }
                            if (SMS.PhoneNumber != "")
                            {
                                Record = SQL.query("SELECT CarrierID From Preview WHERE TelephoneNumber = '" + SMS.PhoneNumber + "' Order By ID Desc");
                                if (Record.TableName != "Error")
                                {
                                    if (Record.Rows.Count > 0)
                                    {
                                        SMS.carrier = Record.Rows[0]["CarrierID"].ToString();
                                    }
                                    if (SMS.carrier == "")
                                    {
                                        SMS.preview();
                                        Record = SQL.query("SELECT CarrierID From Preview WHERE TelephoneNumber = '" + SMS.PhoneNumber + "' Order By ID Desc");
                                        if (Record.TableName != "Error")
                                        {
                                            SMS.carrier = Record.Rows[0]["CarrierID"].ToString();
                                            if (SMS.carrier == "")
                                            {
                                                send = false;
                                                SQL.edit("UPDATE LiveLead SET Sent = getDate(), Error = 1, Campaign = '" + camp.ToString() + "' WHERE ID = " + LeadID);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                send = false;
                            }
                            if (LS.Rows[0]["Message"].ToString() == "")
                            {
                                send = false;
                            }
                            else
                            {
                                SMS.xmessage = LS.Rows[0]["Message"].ToString();
                            }
                            if (send)
                            {
                                SQL.edit("UPDATE LiveLead SET Sent = getDate(), CampaignID = '" + camp.ToString() + "' WHERE ID = " + LeadID);
                                i++;
                                Message MSG = new Message();
                                ThreadPool.QueueUserWorkItem(MSG.Send, SMS);

                            }
                        }

                    }
                    else
                    {
                        AreRows = false;
                    }
                }

            }

        }

    }
}
