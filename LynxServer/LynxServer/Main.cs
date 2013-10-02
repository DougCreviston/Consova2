using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using SQLinterface;
using SMSLib;
using LynxLib;




namespace LynxServer
{
    public partial class Main : Form
    {
        private Object thisLock = new Object();
        System.Threading.Thread Thread;
        bool serverRun = false;
        Hashtable heap2;
        QueueProcessor MessageQueue = new QueueProcessor();
        


        public Main()
        {
          
                InitializeComponent();
                UserInit();
                heap2 = new Hashtable();
            
            
        }
        private void UserInit()
        {
            IPADDR.Text = Properties.Settings.Default.IP;
            port.Text = Properties.Settings.Default.Port.ToString();
            verbose.SelectedIndex = Properties.Settings.Default.Verbose;
        }

        public QueueProcessor getMessageQueue(){
            return MessageQueue;
        }

        private void serverStart()
        {
            try
            {
                timer2.Start();
                IPAddress ipAd = IPAddress.Parse(IPADDR.Text);
                // use local m/c IP address, and 

                // use the same in the client


                /* Initializes the Listener */
                TcpListener myList = new TcpListener(ipAd, Properties.Settings.Default.Port);

                /* Start Listeneting at the specified port */
                serverRun = true;
                while (serverRun)
                {
                    myList.Start();

                    WriteToConsole("The server is running at port " + Properties.Settings.Default.Port.ToString() + ".", 4);
                    WriteToConsole("The local End point is: " +
                                      myList.LocalEndpoint ,4);
                    WriteToConsole("Waiting for a connection...",4);

                    Socket s = myList.AcceptSocket();
                    WriteToConsole("Connection accepted from " + s.RemoteEndPoint,4);

                    byte[] b = new byte[100];
                    int k = s.Receive(b);
                    string rec = "";
                    for (int i = 0; i < k; i++)
                        rec += Convert.ToChar(b[i]);

                    WriteToConsole("Recieved command: ["+getCommand(rec)+"]", 2);
                    CommandProcessor newCommand = new CommandProcessor(rec,s,MessageQueue, this);
                    Thread nThread = new Thread(new ThreadStart(newCommand.Process));
                    nThread.Name = "Process Request";
                    nThread.Start();
                    
                    

                    
                    /* clean up */
                    
                    myList.Stop();
                }

            }
            catch (Exception e)
            {
                WriteToConsole("Error: " + e.StackTrace ,1);
            }
        }

        public void WriteToConsole(string text,int verbose)
        {
            if (verbose <= Properties.Settings.Default.Verbose)
            {
                if (this.console.InvokeRequired)
                {
                    // It's on a different thread, so use Invoke.
                    AppendConsoleText d = new AppendConsoleText(AppendText);
                    this.Invoke
                        (d, new object[] { text + "\n" });
                }
                else
                {
                    // It's on the same thread, no need for Invoke
                    this.console.AppendText(text + "\n");
                }
            }
        }

        public void stopHeap2Thread(string ID)
        {
            if (heap2.Contains(ID))
            {
                Thread othread = (Thread)heap2[ID];
                othread.Abort();
                heap2.Remove(ID);
                
            }
        }

        public bool Heap2Contains(string ID)
        {
            return heap2.Contains(ID);
        }

        public delegate void AppendConsoleText(string text);
        private void AppendText(string text)
        {
            this.console.AppendText(text);
        }

        private void Start(object sender, EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.serverStart));
            this.Thread.Start();
            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
            WriteToConsole("Server Started",1);
        }
        private void Stop(object sender, EventArgs e)
        {
            serverRun = false;
            startToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = false;
            WriteToConsole("Server Stopped",1);
        }
        private string getLocalIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;

        }
        private string getCommand(string cmdStr)
        {
            return cmdStr.Split('|')[0];
        }
        private void Exit(object sender, EventArgs e)
        {
            if (Application.AllowQuit)
            {
                Application.Exit();
            }
        }

        private void updateDatabase()
        {
            LDBSQL ldbSQL = new LDBSQL();
            LDBSQL SQL = new LDBSQL();
            DateTime starttime1 = new DateTime();
            DateTime starttime2 = new DateTime();
            DateTime starttime3 = new DateTime();
            DateTime starttime4 = new DateTime();
            DateTime starttime5 = new DateTime();
            DateTime starttime6 = new DateTime();
            DateTime starttime7 = new DateTime();
            DateTime Endtime1 = new DateTime();
            DateTime Endtime2 = new DateTime();
            DateTime Endtime3 = new DateTime();
            DateTime Endtime4 = new DateTime();
            DateTime Endtime5 = new DateTime();
            DateTime Endtime6 = new DateTime();
            DateTime Endtime7 = new DateTime();
            //int ID = 0;
            DataTable record = SQL.query("Select * FROM Schedule");
            if (record.TableName != "Error")
            {
                foreach (DataRow r in record.Rows)
                {
                    if (r["repeat"].ToString() == "1")
                    {
                        starttime1 = Convert.ToDateTime(r["Start_Date"].ToString() + " " + r["Start_Time"].ToString());
                        starttime2 = Convert.ToDateTime(r["Start_Date2"].ToString() + " " + r["Start_Time2"].ToString());
                        starttime3 = Convert.ToDateTime(r["Start_Date3"].ToString() + " " + r["Start_Time3"].ToString());
                        starttime4 = Convert.ToDateTime(r["Start_Date4"].ToString() + " " + r["Start_Time4"].ToString());
                        starttime5 = Convert.ToDateTime(r["Start_Date5"].ToString() + " " + r["Start_Time5"].ToString());
                        starttime6 = Convert.ToDateTime(r["Start_Date6"].ToString() + " " + r["Start_Time6"].ToString());
                        starttime7 = Convert.ToDateTime(r["Start_Date7"].ToString() + " " + r["Start_Time7"].ToString());
                        Endtime1 = Convert.ToDateTime(r["End_Date"].ToString() + " " + r["End_Time"].ToString());
                        Endtime2 = Convert.ToDateTime(r["End_Date2"].ToString() + " " + r["End_Time2"].ToString());
                        Endtime3 = Convert.ToDateTime(r["End_Date3"].ToString() + " " + r["End_Time3"].ToString());
                        Endtime4 = Convert.ToDateTime(r["End_Date4"].ToString() + " " + r["End_Time4"].ToString());
                        Endtime5 = Convert.ToDateTime(r["End_Date5"].ToString() + " " + r["End_Time5"].ToString());
                        Endtime6 = Convert.ToDateTime(r["End_Date6"].ToString() + " " + r["End_Time6"].ToString());
                        string str = r["End_Date7"].ToString() + " " + r["End_Time7"].ToString();
                        Endtime7 = Convert.ToDateTime(str);
                        starttime1=starttime1.AddDays(7.0);
                        starttime2=starttime2.AddDays(7.0);
                        starttime3=starttime3.AddDays(7.0);
                        starttime4=starttime4.AddDays(7.0);
                        starttime5=starttime5.AddDays(7.0);
                        starttime6=starttime6.AddDays(7.0);
                        starttime7=starttime7.AddDays(7.0);
                        Endtime1 = Endtime1.AddDays(7.0);
                        Endtime2 = Endtime2.AddDays(7.0);
                        Endtime3 = Endtime3.AddDays(7.0);
                        Endtime4 = Endtime4.AddDays(7.0);
                        Endtime5 = Endtime5.AddDays(7.0);
                        Endtime6 = Endtime6.AddDays(7.0);
                        Endtime7 = Endtime7.AddDays(7.0);
                        ldbSQL.edit("UPDATE Schedule SET Start_Date ='" + starttime1.ToString("MM/dd/yyyy") + "', Start_Time = '" + starttime1.ToString("hh:mm tt") + "',End_Date = '" + Endtime1.ToString("MM/dd/yyyy") + "',End_Time ='" + Endtime1.ToString("hh:mm tt") + "',Start_Date2 ='" + starttime2.ToString("MM/dd/yyyy") + "', Start_Time2 = '" + starttime2.ToString("hh:mm tt") + "',End_Date2 = '" + Endtime2.ToString("MM/dd/yyyy") + "',End_Time2 ='" + Endtime2.ToString("hh:mm tt") + "', Start_Date3 ='" + starttime3.ToString("MM/dd/yyyy") + "', Start_Time3 = '" + starttime3.ToString("hh:mm tt") + "',End_Date3 = '" + Endtime3.ToString("MM/dd/yyyy") + "',End_Time3 ='" + Endtime3.ToString("hh:mm tt") + "',Start_Date4 ='" + starttime4.ToString("MM/dd/yyyy") + "', Start_Time4 = '" + starttime4.ToString("hh:mm tt") + "' ,End_Date4 = '" + Endtime4.ToString("MM/dd/yyyy") + "',End_Time4 ='" + Endtime4.ToString("hh:mm tt") + "',Start_Date5 ='" + starttime5.ToString("MM/dd/yyyy") + "', Start_Time5 = '" + starttime5.ToString("hh:mm tt") + "',End_Date5 = '" + Endtime5.ToString("MM/dd/yyyy") + "',End_Time5 ='" + Endtime5.ToString("hh:mm tt") + "',Start_Date6 ='" + starttime6.ToString("MM/dd/yyyy") + "', Start_Time6 = '" + starttime6.ToString("hh:mm tt") + "',End_Date6 = '" + Endtime6.ToString("MM/dd/yyyy") + "',End_Time6 ='" + Endtime6.ToString("hh:mm tt") + "',Start_Date7 ='" + starttime7.ToString("MM/dd/yyyy") + "', Start_Time7 = '" + starttime7.ToString("hh:mm tt") + "',End_Date7 = '" + Endtime7.ToString("MM/dd/yyyy") + "',End_Time7 ='" + Endtime7.ToString("MM/dd/yyyy") + "',repeat=" + Convert.ToInt32(r["repeat"].ToString()) + " WHERE ID = '" + Convert.ToInt32(r["ID"].ToString()) + "'");
                        
                    }
                    else
                    {
                        //do nothing
                    }
                }
            }

        }

        private void saveSettings(object sender, EventArgs e)
        {
            Properties.Settings.Default.IP = IPADDR.Text;
            Properties.Settings.Default.Port = Int32.Parse(port.Text);
            Properties.Settings.Default.Verbose = verbose.SelectedIndex;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan span = new TimeSpan();
            DateTime today = new DateTime();
            DateTime Sunday = Convert.ToDateTime("3/25/2012 Sunday 11:59:59 PM");
           today = DateTime.Now;
        //   today = Convert.ToDateTime("3/4/2012 Sunday 11:59:59 PM");
         //   WriteToConsole(today.ToString("dddd hh:mm tt"), 1);
            DateTime starttime1 = new DateTime();
            DateTime starttime2 = new DateTime();
            DateTime starttime3 = new DateTime();
            DateTime starttime4 = new DateTime();
            DateTime starttime5 = new DateTime();
            DateTime starttime6 = new DateTime();
            DateTime starttime7 = new DateTime();
            DateTime Endtime1 = new DateTime();
            DateTime Endtime2 = new DateTime();
            DateTime Endtime3 = new DateTime();
            DateTime Endtime4 = new DateTime();
            DateTime Endtime5 = new DateTime();
            DateTime Endtime6 = new DateTime();
            DateTime Endtime7 = new DateTime();
            int enable1 = 0;
            int enable2 = 0;
            int enable3 = 0;
            int enable4 = 0;
            int enable5 = 0;
            int enable6 = 0;
            int enable7 = 0;

            LDBSQL SQL = new LDBSQL();
            LDBSQL ldbSQL = new LDBSQL();
            DataTable cmpStartimes = new DataTable();

            string sunday_string = Sunday.ToString("dddd hh:mm:ss tt");
            cmpStartimes = ldbSQL.query("SELECT Campaigns.ID, Schedule,Start_Date,Start_Time,Start_Date2,Start_Time2,Start_Date3,Start_Time3,Start_Date4,Start_Time4,Start_Date5,Start_Time5,Start_Date6,Start_Time6,Start_Date7,Start_Time7,End_Date,End_Time,End_Date2,End_Time2,End_Date3,End_Time3,End_Date4,End_Time4,End_Date5,End_Time5,End_Date6,End_Time6,End_Date7,End_Time7,Run,Rate,Enable1,Enable2,Enable3,Enable4,Enable5,Enable6,Enable7 FROM Campaigns INNER JOIN Schedule ON Campaigns.Schedule=Schedule.ID where Campaigns.Inactive=0");
            
            DataTable Schd = new DataTable();
            //Schd = SQL.query("SELECT ID,Run FROM Campaigns");
            if (cmpStartimes.TableName != "Error")
            {
                foreach (DataRow schtime in cmpStartimes.Rows)
                {
                   
                    
                    starttime1=Convert.ToDateTime(schtime["Start_Date"].ToString()+ " "+ schtime["Start_Time"].ToString());
                    starttime2 = Convert.ToDateTime(schtime["Start_Date2"].ToString() + " " + schtime["Start_Time2"].ToString());
                    starttime3 = Convert.ToDateTime(schtime["Start_Date3"].ToString() + " " + schtime["Start_Time3"].ToString());
                    starttime4 = Convert.ToDateTime(schtime["Start_Date4"].ToString() + " " + schtime["Start_Time4"].ToString());
                    starttime5 = Convert.ToDateTime(schtime["Start_Date5"].ToString() + " " + schtime["Start_Time5"].ToString());
                    starttime6 = Convert.ToDateTime(schtime["Start_Date6"].ToString() + " " + schtime["Start_Time6"].ToString());
                    starttime7 = Convert.ToDateTime(schtime["Start_Date7"].ToString() + " " + schtime["Start_Time7"].ToString());
                    Endtime1 = Convert.ToDateTime(schtime["End_Date"].ToString() + " " + schtime["End_Time"].ToString());
                    Endtime2 = Convert.ToDateTime(schtime["End_Date2"].ToString() + " " + schtime["End_Time2"].ToString());
                    Endtime3 = Convert.ToDateTime(schtime["End_Date3"].ToString() + " " + schtime["End_Time3"].ToString());
                    Endtime4 = Convert.ToDateTime(schtime["End_Date4"].ToString() + " " + schtime["End_Time4"].ToString());
                    Endtime5 = Convert.ToDateTime(schtime["End_Date5"].ToString() +" " + schtime["End_Time5"].ToString());
                    Endtime6 = Convert.ToDateTime(schtime["End_Date6"].ToString() + " " + schtime["End_Time6"].ToString());
                    Endtime7 = Convert.ToDateTime(schtime["End_Date7"].ToString() + " " + schtime["End_Time7"].ToString());
                    enable1 = Convert.ToInt32(schtime["Enable1"].ToString());
                    enable2 = Convert.ToInt32(schtime["Enable2"].ToString());
                    enable3 = Convert.ToInt32(schtime["Enable3"].ToString());
                    enable4 = Convert.ToInt32(schtime["Enable4"].ToString());
                    enable5 = Convert.ToInt32(schtime["Enable5"].ToString());
                    enable6 = Convert.ToInt32(schtime["Enable6"].ToString());
                    enable7 = Convert.ToInt32(schtime["Enable7"].ToString());

           //      WriteToConsole(starttime1.ToString("dddd hh:mm tt"), 1);

                    if (starttime1.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt") && (enable1 != 1))
                    {
                        span = Endtime1 - starttime1;
                    }
                    else if (starttime2.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt") && (enable2 != 1))
                    {
                        span = Endtime2 - starttime2;
                    }
                    else if (starttime3.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt") && (enable3 != 1))
                    {
                        span = Endtime3 - starttime3;
                    }
                    else if (starttime4.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt") && (enable4 != 1))
                    {
                        span = Endtime4 - starttime4;
                    }
                    else if (starttime5.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt") && (enable5 != 1))
                    {
                        span = Endtime5 - starttime5;
                    }
                    else if (starttime6.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt") && (enable6 != 1))
                    {
                        span = Endtime6 - starttime6;
                    }
                    else if (starttime7.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt") && (enable7 != 1))
                    {
                        span = Endtime7 - starttime7;
                    }

                    if (((starttime1.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (starttime2.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (starttime3.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (starttime4.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (starttime5.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (starttime6.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (starttime7.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt"))) && schtime["Run"].ToString() == "0")
                        {
                            
                            WriteToConsole("Text Message: " + "starting campaign", 1);
                            // serviceRunning = true;
                            SQL.edit("UPDATE Campaigns SET Run = 1 WHERE ID = " + schtime["ID"].ToString());
                            CampManager cmp = new CampManager(this,span);
                            cmp.ID(Int32.Parse(schtime["ID"].ToString()));                            
                            Thread oThread = new Thread(new ThreadStart(cmp.start));
                            oThread.Start();
                            heap2.Add(schtime["ID"].ToString(), oThread);
                            while (!oThread.IsAlive) ;
                   
                            // stopGo.Image = global::TextMasterLive.Properties.Resources.green;
                        }
                    if (((Endtime1.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (Endtime2.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (Endtime3.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (Endtime4.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (Endtime5.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (Endtime6.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt")) || (Endtime7.ToString("dddd hh:mm tt") == today.ToString("dddd hh:mm tt"))) && schtime["Run"].ToString() == "1")
                        {
                          
                        SQL.edit("UPDATE Campaigns SET Run = 0 WHERE ID = " + schtime["ID"].ToString());
                  /*      DataTable table =SQL.query("SELECT Sources FROM Campaigns where ID= " + schtime["ID"].ToString());
                        string FileID = "";
                        foreach (DataRow r in table.Rows)
                        {
                            FileID = r["Sources"].ToString();
                            
                        }
                        char[] delimitors={','};
                        string[] Sources= FileID.Split(delimitors);
                        for (int i = 0; i < Sources.Length; i++ )
                            SQL.edit("UPDATE Metrics SET total_messages_sent= where CampID='" + schtime["ID"].ToString() + "' and FileID='"+Sources[i]+"'");*/

                            //     serviceRunning = false;
                            // stopGo.Image = global::TextMasterLive.Properties.Resources.red;
                        }
                    if (today.ToString("hh:mm:ss tt") == "11:59:59 PM")
                    {
                        SQL.edit("UPDATE Message_Sent SET Numsent=0");
                    }

                    if(today.ToString("dddd hh:mm:ss tt") == Sunday.ToString("dddd hh:mm:ss tt")){
                        updateDatabase();
                    }
                    
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lock (thisLock)
            {
                if (MessageQueue.Size() > 0)
                {
                    SMS tempSMS = MessageQueue.getSMS();
                //    WriteToConsole("Text Message: " + tempSMS.xmessage +" Phone: " +  tempSMS.PhoneNumber , 1);
                   int sent = tempSMS.send();
               //     WriteToConsole("Text Message: " + sent, 1);
                //    WriteToConsole("SIZE: " + MessageQueue.Size(), 1);
                    if (MessageQueue.Size() == 0)
                    {
                        
                    }


                }
              
            }
        

        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            if (MessageQueue.Size() > 0)
            {
                SMS tempSMS = MessageQueue.getSMS();
                //    WriteToConsole("Text Message: " + tempSMS.xmessage +" Phone: " +  tempSMS.PhoneNumber , 1);
          //      int sent = tempSMS.send();
                //     WriteToConsole("Text Message: " + sent, 1);
                //    WriteToConsole("SIZE: " + MessageQueue.Size(), 1);
                if (MessageQueue.Size() == 0)
                {

                }
                Message MSG = new Message();
                counter.Text = MessageQueue.Size().ToString();
                ThreadPool.QueueUserWorkItem(MSG.Send,tempSMS);
                
            }
        }
    }
}
