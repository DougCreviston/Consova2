using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Net.Sockets;
using System.IO;
using LynxLib;

namespace LynxServer
{
    class CommandProcessor
    {
        Hashtable heap;
        string command = "";
        string parameters = "";
        Main Parent;
        Socket s;
        QueueProcessor MessageQueue;
        public CommandProcessor(string cmdStr,Socket s,QueueProcessor q, Main Parent)
        {
            this.command = cmdStr.Split('|')[0];
            this.parameters = cmdStr.Split('|')[1];
            this.Parent = Parent;
            this.s = s;
            this.MessageQueue = q;
            heap = new Hashtable();
        }
        public void Process()
        {
            if (Parse())
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes("The command: [" + command + "] was successfully executed."));
                Parent.WriteToConsole("The command: [" + command + "] was successfully executed.", 2);
            }
            else
            {
                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes("The command: [" + command + "] was unsuccessfully executed."));
                Parent.WriteToConsole("The command: [" + command + "] was unsuccessfully executed.", 2); 
            }
            Parent.WriteToConsole("Sent Acknowledgement", 4);
            s.Close();
        }
        private bool Parse()
        {
            switch (command)
            {
                case "Add":
                    return cmdAdd();
                case "Send":
                    return cmdSend();
                case "Force_cmp_start":
                    return Force_cmp_start(1);
                case "Force_cmp_abort":
                    return Force_cmp_start(0);
                default:
                    return false;
            }
        }
        private bool cmdAdd()
        {
            string number = "";
            string msg = "";
            foreach (string s in parameters.Split('&'))
            {
                if (s.Split('=')[0] == "number")
                {
                    number = s.Split('=')[1];
                }
                else if (s.Split('=')[0] == "msg")
                {
                    msg = s.Split('=')[1];
                }
            }
            MessageQueue.QueueSMS(1, number, msg,"","");
            Thread.Sleep(5000);
            return true;
        }
        private bool cmdSend()
        {
            SMS SMS1 = MessageQueue.getSMS();
            Parent.WriteToConsole("Queue:" +SMS1.xmessage + "@" + SMS1.PhoneNumber,0);
            Thread.Sleep(5000);
            return true;
        }

        private bool Force_cmp_start(int x)
        {
            string ID = "";
            string msg = "";
            foreach (string s in parameters.Split('&'))
            {
                if (s.Split('=')[0] == "ID")
                {
                    ID = s.Split('=')[1];
                }
                else if (s.Split('=')[0] == "msg")
                {
                    msg = s.Split('=')[1];
                }
            }
            if (x == 1)
            {
              
                    DateTime today = new DateTime();
                    today = DateTime.Now;
                    DateTime end = Convert.ToDateTime("09:00 PM");
                    TimeSpan span = new TimeSpan();
                    span = end - today;
                    CampManager cmp = new CampManager(Parent, span);
                    LDBSQL SQL = new LDBSQL();
                    DataTable CS = SQL.query("SELECT Run FROM Campaigns where Run = 1 and ID= " + ID);
                    if (CS.TableName != "Error")
                    {
                        if (CS.Rows.Count == 0)
                        {
                            SQL.edit("UPDATE Campaigns SET Run = 1 WHERE ID = " + ID);
                            cmp.ID(Int32.Parse(ID));
                            Thread oThread = new Thread(new ThreadStart(cmp.start));
                            oThread.Start();
                            oThread.Name = "Forced_Start";
                            while (!oThread.IsAlive) ;
                        }
                    }
                    
                
            }
            else
            {
                Parent.WriteToConsole( ID  , 2); 
                    LDBSQL SQL = new LDBSQL();
                    SQL.edit("UPDATE Campaigns SET Run = 0 WHERE ID = " + ID);
                
            }
            return true;
        }

        private bool Force_cmp_abort()
        {
            string ID = "";
            string msg = "";
            foreach (string s in parameters.Split('&'))
            {
                if (s.Split('=')[0] == "ID")
                {
                    ID = s.Split('=')[1];
                }
                else if (s.Split('=')[0] == "msg")
                {
                    msg = s.Split('=')[1];
                }
            }

            

            return true;
        }
    }
}
