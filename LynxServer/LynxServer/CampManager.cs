using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using LynxLib;
using System.Data;
using System.Runtime.InteropServices;

namespace LynxServer
{
    class CampManager : Main
    {
       
        public 
        TimeSpan span;
        bool campRunning = false;
        Main Main;
        private Object thisLock = new Object();
        private Queue LeadList;
        public CampManager(Main myForm, TimeSpan interval)
        {
            LeadList = new Queue();
            Main = myForm;
            span = interval;
        }
        int campID;
        public void ID(int ID)
        {
            this.campID = ID;
        }


        public DataRow dequeueLeadList()
        {
            lock (thisLock)
            {
                return (DataRow)LeadList.Dequeue();
            }
        }

        public void EnqueLeadlist(DataRow r)
        {

            lock (thisLock)
            {
                LeadList.Enqueue(r);
            }
        }

        public int Count()
        {
            return LeadList.Count;
        }

        public bool Size()
        {
            return LeadList.Count > 0 ? true : false;
        }

        public void start()
        {
            LDBSQL SQL = new LDBSQL();
            DataTable table = new DataTable();
            
           
        
            //GridDelegate d3 = new GridDelegate(updateGrid);
       //     Main.Invoke(d3);
            table = SQL.query("SELECT ID,Rate,Run FROM Campaigns WHERE ID = " + campID.ToString());
            int i = 0;
            if (table.TableName != "Error")
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row["Run"].ToString() == "1" && (i + Int32.Parse(row["Rate"].ToString()) <= 600) && Int32.Parse(row["Rate"].ToString()) > 0)
                    {
                        LDBSQL ldbSQL = new LDBSQL();
                        ldbSQL.edit("INSERT INTO Message_Sent (ID,Numsent) VALUES('"+campID.ToString()+"',"+0 +")");
                        MsgManager msg  = new MsgManager(Main);
                    /*   string sources = "SELECT Sources FROM Campaigns where ID ='" + campID+"'";
                        DataTable record=ldbSQL.query(sources);
                        string source = "";
                        foreach (DataRow r in record.Rows)
                        {
                            source = r["Sources"].ToString();
                        }
                        char[] delimiter = { ',' };
                        string[] Source = source.Split(delimiter);
                        string str;
                      /*  string metrics_select = "SELECT * FROM Metrics where CampID='" + campID + "' and convert(char(10),date_sent,101) = '" + DateTime.Today.ToString("MM/dd/yyyy") + "'";
                        DataTable metricstable =ldbSQL.query(metrics_select);
                        if(metricstable.TableName != "Error"){
                            if (metricstable.Rows.Count == 0)
                            {
                                for (int x = 0; x < Source.Length; x++)
                                {

                                    //str = "INSERT INTO Metrics(CampID,FileID,total_messages_sent,date) values (" + campID + "," + Convert.ToInt32(Source[x]) + "," + 0 + ",  getDate()  )";
                                    ldbSQL.edit("INSERT INTO Metrics(CampID,FileID,total_messages_sent,date_sent) values ('" + campID + "','" + Source[x] + "','" + 0 + "', getDate())");
                                }
                            }
                        }*/
                        msg.getCamp(campID);
                        msg.getParent(this);
                        Producer producer = new Producer(campID, this);
                        Thread oThread2 = new Thread(new ThreadStart(producer.start));
                        Thread oThread = new Thread(new ThreadStart(msg.MsgStart));

                        oThread2.Start();
                        Thread.Sleep(10000);
                        oThread.Start();
                  //      Thread.Sleep(span);

                        
                        while (!oThread.IsAlive) ;
                        while (!oThread2.IsAlive) ;
                    
                  //      msg.requeststop();
                        campRunning = true;
                        WriteToConsole("Text Message: " + campRunning, 1);
                    }
                }
            }
            while (campRunning)
            {
                WriteToConsole("while (campRunning)", 1);
                i = 0;
                table = SQL.query("SELECT ID,Rate,Run FROM Campaigns Order By ID");
                if (table.TableName != "Error")
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if (row["Run"].ToString() == "1" && (i + Int32.Parse(row["Rate"].ToString()) <= 600) && Int32.Parse(row["Rate"].ToString()) > 0)
                        {
                            i += Int32.Parse(row["Rate"].ToString());
                        }
                        if (row["Run"].ToString() == "0" && row["ID"].ToString() == campID.ToString())
                        {
                            campRunning = false;
                        }
                    }
                }

                    // It's on a different thread, so use Invoke.
            //    ProgressDelegate d = new ProgressDelegate(setProgress);
              //  Main.Invoke(d, new object[] { Int32.Parse(Math.Ceiling((decimal.Parse(i.ToString()) / 600) * 100).ToString()) });

           //     LabelDelegate d2 = new LabelDelegate(setLabel);
            //    Main.Invoke(d2, new object[] { i.ToString() });


                //setProgress(i);
                //setLabel(i.ToString());
                //Thread.Sleep(1000);
            }
            
            // It's on a different thread, so use Invoke.
    //        ProgressDelegate d4 = new ProgressDelegate(setProgress);
      //      Main.Invoke(d4, new object[] { 0 });

       //     LabelDelegate d5 = new LabelDelegate(setLabel);
        //    Main.Invoke(d5, new object[] { "TextRate" });
            
        }
        
    }
}
