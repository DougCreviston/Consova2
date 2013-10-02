using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LynxLib;

namespace LynxClient
{
    class LSProcessor
    {
        string command = "";
        Main Parent;
        public LSProcessor(string cmd, Main p)
        {
            this.command = cmd;
            this.Parent = p;

        }
        public void Process()
        {
            //Parent.WriteToConsole("test", 0);
            if (Parse())
            {
                Parent.DialogShow("Source " + command + " Successful.");
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
                        return AddLS();
                    case "Delete":
                        return DelLS();
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
        private bool AddLS()
        {
            try
            {
                LDBSQL SQL = new LDBSQL();
                if (SQL.edit("INSERT INTO refLS (Name,DateCreated) VALUES ('" + Parent.LSAddName.Text + "',getDate())") == 1)
                {

                    Parent.myUpdateCmpTabs();
                    Parent.WriteToConsole("Source Added: " + Parent.LSAddName.Text, 1);

                    return true;
                }
                else
                {
                    Parent.WriteToConsole("Source Add Failed: " + Parent.LSAddName.Text, 1);
                    return false;
                }
            }
            catch (Exception e)
            {
                Parent.WriteToConsole(e.Message, 0);
                return false;
            }

        }
        private bool DelLS()
        {
            try
            {
                LDBSQL SQL = new LDBSQL();
                ListItem item = Parent.pGetManDelSourceSel();
                if (SQL.edit("UPDATE refLS SET Inactive= 1 WHERE ID = " +item.ID) == 1)
                {
                    SQL.edit("DROP TABLE [" + item.Name + "-Leads]; DROP TABLE [" + item.Name + "-Files];");
                    Parent.myUpdateCmpTabs();
                    Parent.WriteToConsole("Source Deleted: " + item.Name, 1);
                    return true;
                }
                else
                {
                    Parent.WriteToConsole("Source Delete Failed: " + item.Name, 1);
                    return false;
                }
            }
            catch (Exception e)
            {
                Parent.WriteToConsole(e.Message, 0);
                return false;
            }

        }
        private bool ModLS()
        {
            try
            {
                LDBSQL SQL = new LDBSQL();
                if (SQL.edit("INSERT INTO refLS (Name,LSTable) VALUES ('" + Parent.pGetLSAddName() + "','Source" + Parent.pGetLSAddName() + "')") == 1)
                {
                    
                   // Parent.myUpdateCmpTabs();
                    Parent.WriteToConsole("Source Added: " + Parent.LSAddName.Text, 1);
                    return true;
                }
                else
                {
                    Parent.WriteToConsole("Source Add Failed: " + Parent.LSAddName.Text, 1);
                    return false;
                }
            }
            catch (Exception e)
            {
                Parent.WriteToConsole(e.Message, 0);
                return false;
            }

        }
    }
}

