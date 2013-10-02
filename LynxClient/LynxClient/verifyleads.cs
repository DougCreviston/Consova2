using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading;
using LynxLib;



namespace LynxClient
{
    public class verifyleads
    {
        Main Parent;

        public verifyleads(Main P)
        {
            this.Parent = P;
        }


        public void start()
        {
            int deactive = 0;
            SMSSQL smsSQL = new SMSSQL();
            LDBSQL ldbSQL = new LDBSQL();
            DataTable record = ldbSQL.query("SELECT Telephone_Number FROM Leads  ");
           

            DataTable table = smsSQL.query("SELECT Phone FROM Deactivates");

            
            foreach (DataRow r in record.Rows)
            {
                
                foreach (DataRow row in table.Rows)
                {
                    if (r["Telephone_Number"].ToString().CompareTo(row["Phone"].ToString())==0)
                    {
                        ldbSQL.edit("DELETE FROM Leads where Telephone_Number='" + r["Telephone_Number"].ToString() + "'");
                        deactive++;
                    }
                    else
                    {
                        //MessageBox.Show("A row with the primary key of " + s + " could not be found");
                    }
                }

            }

            Parent.pSetimpDeactive();
        }

    }
}