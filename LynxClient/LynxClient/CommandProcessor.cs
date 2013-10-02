using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace LynxClient
{
    class CommandProcessor
    {
        string command = "";
        string parameters = "";
        Main Parent;
        public CommandProcessor(string cmdStr, Main Parent)
        {
            this.command = cmdStr.Split('|')[0];
            this.parameters = cmdStr.Split('|')[1];
            this.Parent = Parent;

        }
        public void Process()
        {
            //Parent```.WriteToConsole("test", 0);
            if (Parse())
            {

            }
            else
            {

            }
        }
        private bool Parse()
        {
            try
            {
                TcpClient tcpclnt = new TcpClient();
                Parent.WriteToConsole("Connecting...", 4);

                tcpclnt.Connect(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
                // use the ipaddress as in the server program


                Parent.WriteToConsole("Connected.", 4);

                String str = command+"|"+parameters;
                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                Parent.WriteToConsole("Transmitting command: [" + command + "]", 4);

                stm.Write(ba, 0, ba.Length);
                Parent.WriteToConsole("The command: [" + command + "] was sent successfully.", 1);
                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);
                string rec = "";
                for (int i = 0; i < k; i++)
                    rec += Convert.ToChar(bb[i]);
                Parent.WriteToConsole(rec, 1);
                tcpclnt.Close();
                return true;
            }

            catch (Exception e)
            {
                Parent.WriteToConsole("Error on command [" + command + "]: " + e.StackTrace, 0);
                return false;
            }
        }
    }
}
