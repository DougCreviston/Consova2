using System;
using LynxLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SMSLib;
using System.Data;


public class Messenger
{
    public Messenger()
	{
	}

    int camp;
        public void getCamp(int ID)
        {
            this.camp = ID;
        }
        public void MsgStart()
        {
            SQLinterface SQL = new SQLinterface();
            DataTable LS = new DataTable();
            DataTable Record = new DataTable();
            int run = 1;
            int i = 0;
            int LeadID = 0;
            string LeadAbbr = "";
            string LiveID = "";
            string LivePC = "";
            bool AreRows = true;
            while (AreRows)
            {
                LeadID = 0;
                LeadAbbr = "";
                LiveID = "";
                LivePC = "";
                LS = SQL.query("SELECT * FROM TextCampaign WHERE ID = '" + camp.ToString() + "'");
                if (LS.TableName != "Error")
                {

                    run = Int32.Parse(LS.Rows[0]["Run"].ToString());
                    LiveID = LS.Rows[0]["LiveID"].ToString();
                    LivePC = LS.Rows[0]["LivePC"].ToString();

                    Thread.Sleep(Int32.Parse(Math.Ceiling(((60 / Decimal.Parse(LS.Rows[0]["Rate"].ToString())) * 1000)).ToString()));

                    if (run == 1)
                    {
                        bool send = true;
                        if (LS.Rows[0]["LDB"].ToString() == "SSILDB")
                        {
                            SSISQL LDBC = new SSISQL();
                            SendSMS SMS = new SendSMS();
                            SMS.sourceNum = "75612";
                            Record = LDBC.query("SELECT Top 1 LeadID,LeadAbbr, TelephoneNumber FROM vw" + LS.Rows[0]["Campaign"].ToString() + " WHERE SENT = 0 Order By NewID()");
                            if (Record.TableName != "Error")
                            {
                                if (Record.Rows.Count == 0)
                                {
                                    AreRows = false;
                                }
                                else
                                {
                                    LeadID = Int32.Parse(Record.Rows[0]["LeadID"].ToString());
                                    LeadAbbr = Record.Rows[0]["LeadAbbr"].ToString();
                                    SMS.PhoneNumber = "+1" + Record.Rows[0]["TelephoneNumber"].ToString();
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
                                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = -1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                                send = false;
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
                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = 1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                i++;
                                Message MSG = new Message();
                                ThreadPool.QueueUserWorkItem(MSG.Send, SMS);
                            }
                        }
                        else if (LS.Rows[0]["LDB"].ToString() == "BSMLDB")
                        {
                            BSMSQL LDBC = new BSMSQL();
                            SendSMS SMS = new SendSMS();
                            SMS.sourceNum = "75612";
                            Record = LDBC.query("SELECT Top 1 LeadID,LeadAbbr, TelephoneNumber FROM vw" + LS.Rows[0]["Campaign"].ToString() + " WHERE SENT = 0 Order By NewID()");
                            if (Record.TableName != "Error")
                            {
                                if (Record.Rows.Count == 0)
                                {
                                    AreRows = false;
                                }
                                else
                                {
                                    LeadID = Int32.Parse(Record.Rows[0]["LeadID"].ToString());
                                    LeadAbbr = Record.Rows[0]["LeadAbbr"].ToString();
                                    SMS.PhoneNumber = "+1" + Record.Rows[0]["TelephoneNumber"].ToString();
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
                                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = -1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                                send = false;
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
                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = 1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                i++;
                                Message MSG = new Message();
                                ThreadPool.QueueUserWorkItem(MSG.Send, SMS);
                            }
                        }
                        else if (LS.Rows[0]["LDB"].ToString() == "PHCLDB")
                        {
                            PHCSQL LDBC = new PHCSQL();
                            SendSMS SMS = new SendSMS();
                            SMS.sourceNum = "75612";
                            Record = LDBC.query("SELECT Top 1 LeadID,LeadAbbr, TelephoneNumber FROM vw" + LS.Rows[0]["Campaign"].ToString() + " WHERE SENT = 0 Order By NewID()");
                            if (Record.TableName != "Error")
                            {
                                if (Record.Rows.Count == 0)
                                {
                                    AreRows = false;
                                }
                                else
                                {
                                    LeadID = Int32.Parse(Record.Rows[0]["LeadID"].ToString());
                                    LeadAbbr = Record.Rows[0]["LeadAbbr"].ToString();
                                    SMS.PhoneNumber = "+1" + Record.Rows[0]["TelephoneNumber"].ToString();
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
                                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = -1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                                send = false;
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
                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = 1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                i++;
                                Message MSG = new Message();
                                ThreadPool.QueueUserWorkItem(MSG.Send, SMS);
                            }
                        }
                        else if (LS.Rows[0]["LDB"].ToString() == "MHLDB")
                        {
                            MHSQL LDBC = new MHSQL();
                            SendSMS SMS = new SendSMS();
                            SMS.sourceNum = "75612";
                            Record = LDBC.query("SELECT Top 1 LeadID,LeadAbbr, TelephoneNumber FROM vw" + LS.Rows[0]["Campaign"].ToString() + " WHERE SENT = 0 Order By NewID()");
                            if (Record.TableName != "Error")
                            {
                                if (Record.Rows.Count == 0)
                                {
                                    AreRows = false;
                                }
                                else
                                {
                                    LeadID = Int32.Parse(Record.Rows[0]["LeadID"].ToString());
                                    LeadAbbr = Record.Rows[0]["LeadAbbr"].ToString();
                                    SMS.PhoneNumber = "+1" + Record.Rows[0]["TelephoneNumber"].ToString();
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
                                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = -1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                                send = false;
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
                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = 1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                i++;
                                Message MSG = new Message();
                                ThreadPool.QueueUserWorkItem(MSG.Send, SMS);
                            }
                        }
                        else if (LS.Rows[0]["LDB"].ToString() == "TCRLDB")
                        {
                            TCRSQL LDBC = new TCRSQL();
                            SendSMS SMS = new SendSMS();
                            SMS.sourceNum = "75612";
                            Record = LDBC.query("SELECT Top 1 LeadID,LeadAbbr, TelephoneNumber FROM vw" + LS.Rows[0]["Campaign"].ToString() + " WHERE SENT = 0 Order By NewID()");
                            if (Record.TableName != "Error")
                            {
                                if (Record.Rows.Count == 0)
                                {
                                    AreRows = false;
                                }
                                else
                                {
                                    LeadID = Int32.Parse(Record.Rows[0]["LeadID"].ToString());
                                    LeadAbbr = Record.Rows[0]["LeadAbbr"].ToString();
                                    SMS.PhoneNumber = "+1" + Record.Rows[0]["TelephoneNumber"].ToString();
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
                                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = -1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                                send = false;
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
                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = 1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                i++;
                                Message MSG = new Message();
                                ThreadPool.QueueUserWorkItem(MSG.Send, SMS);
                            }
                        }
                        else if (LS.Rows[0]["LDB"].ToString() == "APLDB")
                        {
                            APSQL LDBC = new APSQL();
                            SendSMS SMS = new SendSMS();
                            SMS.sourceNum = "75612";
                            Record = LDBC.query("SELECT Top 1 LeadID,LeadAbbr, TelephoneNumber FROM vw" + LS.Rows[0]["Campaign"].ToString() + " WHERE SENT = 0 Order By NewID()");
                            if (Record.TableName != "Error")
                            {
                                if (Record.Rows.Count == 0)
                                {
                                    AreRows = false;
                                }
                                else
                                {
                                    LeadID = Int32.Parse(Record.Rows[0]["LeadID"].ToString());
                                    LeadAbbr = Record.Rows[0]["LeadAbbr"].ToString();
                                    SMS.PhoneNumber = "+1" + Record.Rows[0]["TelephoneNumber"].ToString();
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
                                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = -1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                                send = false;
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
                                LDBC.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = 1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                i++;
                                Message MSG = new Message();
                                ThreadPool.QueueUserWorkItem(MSG.Send, SMS);
                            }
                        }
                        else if (LS.Rows[0]["LDB"].ToString() == "LCLDB")
                        {
                            LDBSQL SQL1 = new LDBSQL();
                            SendSMS SMS = new SendSMS();
                            SMS.sourceNum = "75612";
                            Record = SQL1.query("SELECT Top 1 LeadID,LeadAbbr, TelephoneNumber FROM vw" + LS.Rows[0]["Campaign"].ToString() + " WHERE SENT = 0 Order By NewID()");
                            if (Record.TableName != "Error")
                            {
                                if (Record.Rows.Count == 0)
                                {
                                    AreRows = false;
                                }
                                else
                                {
                                    LeadID = Int32.Parse(Record.Rows[0]["LeadID"].ToString());
                                    LeadAbbr = Record.Rows[0]["LeadAbbr"].ToString();
                                    SMS.PhoneNumber = "+1" + Record.Rows[0]["TelephoneNumber"].ToString();
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
                                                SQL1.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = -1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                                send = false;
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
                                SQL1.edit("UPDATE vw" + LS.Rows[0]["Campaign"].ToString() + " SET Sent = 1 WHERE LeadID = " + LeadID + " AND LeadAbbr = '" + LeadAbbr + "'");
                                i++;
                                Message MSG = new Message();
                                ThreadPool.QueueUserWorkItem(MSG.Send, SMS);
                            }
                        }
                        else if (LS.Rows[0]["LDB"].ToString() == "LiveTextLDB")
                        {
                            LiveSQL LDBC = new LiveSQL();
                            SendSMS SMS = new SendSMS();
                            SMS.sourceNum = "75612";
                            if (LivePC == "ALL" || LivePC == "")
                            {
                                Record = LDBC.query("SELECT Top 1 ID, PhoneNumber, Received FROM LiveLead WHERE SENT IS NULL Order By Received DESC");
                            }
                            else
                            {
                                Record = LDBC.query("SELECT Top 1 ID, PhoneNumber, Received FROM LiveLead WHERE SENT IS NULL AND SourcePC IN (" + LivePC + ") Order By Received DESC");
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
                                                LDBC.edit("UPDATE LiveLead SET Sent = getDate(), Error = 1, Campaign = '" + camp.ToString() + "' WHERE ID = " + LeadID);
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
                                LDBC.edit("UPDATE LiveLead SET Sent = getDate(), CampaignID = '" + camp.ToString() + "' WHERE ID = " + LeadID);
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
                else
                {
                    AreRows = false;
                }
            }
        }
    }


