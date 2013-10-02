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

    class Producer
    {
        bool _isrunning;
        Main Main;
        CampManager Parent;
        int camp;

        public Producer(int camp2, CampManager man)
        {
            camp = camp2;
            _isrunning = true;
            Parent = man;
        }


        public void stop()
        {
            _isrunning = false;
        }

        public void start()
        {
            LDBSQL SQL = new LDBSQL();
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

            string StateDeletes = "";




            while (_isrunning)
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
                   Thread.Sleep(Int32.Parse(Math.Ceiling((((60 / Decimal.Parse(LS.Rows[0]["Rate"].ToString()))) * 600)).ToString()));
                    }
                    else
                    {*/


                    // }

                    MessageTable = SQL.query("SELECT Message FROM Messages where ID=" + messageID);

                    if (run == 1)
                    {
                        bool send = true;
                        string[] statedelete_area_codes = { };
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
                        }


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
                        //string str = "SELECT TOP 1 ID, Telephone_Number FROM Leads WHERE CampId = 0  and FileID in (" + leads + ") and left(Telephone_Number,3) in (" + StateDeletes + ") Order By DateLoaded DESC";
                        String str = "SELECT TOP 1 Leads.ID, Leads.Telephone_Number,AreaCodeTZ.TimeZones FROM Leads INNER JOIN AreaCodeTZ ON left(Leads.Telephone_Number,3) = AreaCodeTZ.AreaCode and TimeZones in ('EST','CST','MST','PST','ALA') WHERE CampId = 0  and FileID in (" + leads + ") and left(Telephone_Number,3) in (" + StateDeletes + ") Order By DateLoaded,TimeZones DESC";
                        Record = SQL.query(str);
                        LeadID = Int32.Parse(Record.Rows[0]["ID"].ToString());
                        
                            SQL.edit("UPDATE Leads  SET CampId =" + camp.ToString() + "WHERE ID = " + LeadID);
                            Parent.EnqueLeadlist(Record.Rows[0]);
                        

                    }
                    else
                    {
                        while (Parent.Size())
                        {
                            DataRow r = Parent.dequeueLeadList();
                            LeadID = Int32.Parse(r["ID"].ToString());
                            SQL.edit("UPDATE Leads  SET CampId = 0 WHERE ID = " + LeadID);
                        }
                        _isrunning = false;
                    }


                }


            }
        }
    }
}
