using System;
using LynxClient;
using LynxLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SMSLib;
using System.Data;
using System.Runtime.InteropServices;


public class Campaign
{
    bool campRunning = false;
    Main Main;
    int campID;

    public Campaign(Main myForm)
	{
        Main = myForm;
	}

    
    public void ID(int ID)
    {
        this.campID = ID;
    }

    public void start()
    {
        SQLinterface SQL = new SQLinterface();
        DataTable table = new DataTable();

    //    GridDelegate d3 = new GridDelegate(updateGrid);
      //  Main.Invoke(d3);
        table = SQL.query("SELECT ID,Rate,Run FROM TextCampaign WHERE ID = " + campID.ToString());
        int i = 0;
        if (table.TableName != "Error")
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["Run"].ToString() == "1" && (i + Int32.Parse(row["Rate"].ToString()) <= 600) && Int32.Parse(row["Rate"].ToString()) > 0)
                {

                    Messenger msg = new Messenger();
                    msg.getCamp(campID);
                    Thread oThread = new Thread(new ThreadStart(msg.MsgStart));
                    oThread.Start();
                    while (!oThread.IsAlive) ;
                    campRunning = true;
                }
            }
        }
        while (campRunning)
        {
            i = 0;
            table = SQL.query("SELECT ID,Rate,Run FROM TextCampaign Order By ID");
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
      //      ProgressDelegate d = new ProgressDelegate(setProgress);
        //    Main.Invoke(d, new object[] { Int32.Parse(Math.Ceiling((decimal.Parse(i.ToString()) / 600) * 100).ToString()) });

         //   LabelDelegate d2 = new LabelDelegate(setLabel);
          //  Main.Invoke(d2, new object[] { i.ToString() });


            //setProgress(i);
            //setLabel(i.ToString());
            //Thread.Sleep(1000);
        }

        // It's on a different thread, so use Invoke.
      //  ProgressDelegate d4 = new ProgressDelegate(setProgress);
      //  Main.Invoke(d4, new object[] { 0 });

       // LabelDelegate d5 = new LabelDelegate(setLabel);
       // Main.Invoke(d5, new object[] { "TextRate" });
    }

}
