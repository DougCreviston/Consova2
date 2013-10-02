using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Text;
using System.Xml;
using SMSLib;
using System.Text.RegularExpressions;

using LynxLib;
using System.Net.Mail;




namespace LynxClient
{

   


    public partial class Main : Form
    {
        int number = 1;
        System.Threading.Thread Thread;
        public Main()
        {
            
            InitializeComponent();
            UserInit();
            intiDatagrids();
            updateCmpTabs();
            updateImportTabs();
            updateSourceTabs();

           
                
          
        }

        

        //user inits
        private void UserInit()
        {
            serverIP.Text = Properties.Settings.Default.ServerIP;
            serverPort.Text = Properties.Settings.Default.ServerPort.ToString();
            verbose.SelectedIndex = Properties.Settings.Default.Verbose;
            
        }
        
        private void intiDatagrids(){
            DataGridViewColumn Source = new DataGridViewColumn();
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            Source.CellTemplate = cell;

            Source.HeaderText = "Source";
            Source.Name = "Source";
            Source.Visible = true;
            Source.Width = 100;
            dataGridView1.Columns.Add(Source);

            DataGridViewColumn RemainingLeads = new DataGridViewColumn();
            DataGridViewCell cell2 = new DataGridViewTextBoxCell();
            RemainingLeads.CellTemplate = cell2;

            RemainingLeads.HeaderText = "Remaining Leads";
            RemainingLeads.Name = "Remaining Leads";
            RemainingLeads.Visible = true;
            RemainingLeads.Width = 100;
            dataGridView1.Columns.Add(RemainingLeads);


            DataGridViewColumn Leads = new DataGridViewColumn();
            DataGridViewCell Sourcecell = new DataGridViewTextBoxCell();
            Leads.CellTemplate = Sourcecell;

            Leads.HeaderText = "Source";
            Leads.Name = "Source";
            Leads.Visible = true;
            Leads.Width = 100;
            dataGridView2.Columns.Add(Leads);


            DataGridViewColumn RemainingLeads2 = new DataGridViewColumn();
            DataGridViewCell RemainingLeadscell2 = new DataGridViewTextBoxCell();
            RemainingLeads2.CellTemplate = RemainingLeadscell2;

            RemainingLeads2.HeaderText = "Remaining Leads";
            RemainingLeads2.Name = "Remaining Leads";
            RemainingLeads2.Visible = true;
            RemainingLeads2.Width = 100;
            dataGridView2.Columns.Add(RemainingLeads2);

          /*  DataGridViewColumn StateDel2 = new DataGridViewColumn();
            DataGridViewCell StateDelcell2 = new DataGridViewTextBoxCell();
            StateDel2.CellTemplate = StateDelcell2;

            StateDel2.HeaderText = "State Deletes";
            StateDel2.Name = "State Deletes";
            StateDel2.Visible = true;
            StateDel2.Width = 100;
            dataGridView2.Columns.Add(StateDel2);*/


            DataGridViewColumn LeadsSent2 = new DataGridViewColumn();
            DataGridViewCell LeadsSentcell2 = new DataGridViewTextBoxCell();
            LeadsSent2.CellTemplate = LeadsSentcell2;

            LeadsSent2.HeaderText = "Leads Sent";
            LeadsSent2.Name = "Leads Sent";
            LeadsSent2.Visible = true;
            LeadsSent2.Width = 100;
            dataGridView2.Columns.Add(LeadsSent2);
            
        }

        private void cmdAdd()
        {
            CommandProcessor newCommand = new CommandProcessor("Add|number=+"+number+"&msg=test", this);
            Thread nThread = new Thread(new ThreadStart(newCommand.Process));
            nThread.Name = "Server Request";
            nThread.Start();
            number++;
        }
        private void Force_cmp_start()
        {
            CommandProcessor newCommand = new CommandProcessor("Force_cmp_start|ID=+" + this.pGetcmpSelDD1ID() + "&msg=test", this);
            Thread nThread = new Thread(new ThreadStart(newCommand.Process));
            nThread.Name = "Server Request";
            nThread.Start();
            number++;
        }

        private void Force_cmp_abort()
        {
            CommandProcessor newCommand = new CommandProcessor("Force_cmp_abort|ID=+" + this.pGetcmpSelDD1ID() + "&msg=test", this);
            Thread nThread = new Thread(new ThreadStart(newCommand.Process));
            nThread.Name = "Server Request";
            nThread.Start();
            number++;
        }
        private void cmdSend()
        {
            CommandProcessor nProcess = new CommandProcessor("Send|noparams=true", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "Server Request";
            nThread.Start();
        }
        private void SchDel()
        {
            CampaignProcessor nProcess = new CampaignProcessor("DelSch", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "Campaign Request";
            nThread.Start();
        }

        private void cmpAdd()
        {
            CampaignProcessor nProcess = new CampaignProcessor("Add", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "Campaign Request";
            nThread.Start();
        }
        private void cmpDel()
        {
            CampaignProcessor nProcess = new CampaignProcessor("Delete", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "Campaign Request";
            nThread.Start();
        }
        private void cmpSchedSelect()
        {

            CampaignProcessor nProcess = new CampaignProcessor("Sched", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "Campaign Request";
            nThread.Start();
        }
        private void  cmpSchedcreate()
        {

            CampaignProcessor nProcess = new CampaignProcessor("CreateSched", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "Campaign Request";
            nThread.Start();
        }
        private void cmpSchedSave()
        {

            CampaignProcessor nProcess = new CampaignProcessor("SaveSched", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "Campaign Request";
            nThread.Start();
        }
        private void cmpMod()
        {
            CampaignProcessor nProcess = new CampaignProcessor("Modify", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "Campaign Request";
            nThread.Start();
        }
     
        private void LSAdd()
        {
            LSProcessor nProcess = new LSProcessor("Add", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "LS Request";
            nThread.Start();
        }
        private void IPimport()
        {
            ImportProcessor nProcess = new ImportProcessor("Import", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "File Import Request";
            nThread.Start();
        }

        private void LSMod()
        {
            LSProcessor nProcess = new LSProcessor("Modify", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "LS Request";
            nThread.Start();
        }
        private void LSDel()
        {
            LSProcessor nProcess = new LSProcessor("Delete", this);
            Thread nThread = new Thread(new ThreadStart(nProcess.Process));
            nThread.Name = "LS Request";
            nThread.Start();
        }
        private string getCommand(string cmdStr)
        {
            return cmdStr.Split('|')[0];
        }

        
        //public methods

        public void WriteToimpImportConsole(string text, int verbose)
        {
            if( verbose <= Properties.Settings.Default.Verbose)
            {
                if (this.impImportConsole.InvokeRequired)
                {
                    // It's on a different thread, so use Invoke.
                    voidDelegate d = new voidDelegate(ImpAppendText);
                    this.Invoke
                        (d, new object[] { text + "\n" });
                }
                else
                {
                    // It's on the same thread, no need for Invoke
                    this.impImportConsole.AppendText(text + "\n");
                }
            }
        }

        public void WriteToConsole(string text, int verbose)
        {
            if( verbose <= Properties.Settings.Default.Verbose)
            {
                if (this.console.InvokeRequired)
                {
                    // It's on a different thread, so use Invoke.
                    voidDelegate d = new voidDelegate(AppendText);
                    this.Invoke
                        (d, new object[] { text + "\n" });
                }
                else
                {
                    // It's on the same thread, no need for Invoke
                    this.console.AppendText(text + "\n" );
                }
            }
        }
        public void DialogShow(string text)
        {
            if (this.console.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                voidDelegate d = new voidDelegate(OpenDialog);
                this.Invoke
                    (d, new object[] { text });
            }
            else
            {
                // It's on the same thread, no need for Invoke
                this.OpenDialog(text);
            }
        }

        private string getSchedCmpStartDate()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate()
        {
            
            if (this.SchedCmpStartDate.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate();
            }
        }

        private string getSchedCmpStartDatea()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDatea.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDatea()
        {

            if (this.SchedCmpStartDatea.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDatea);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDatea();
            }
        }

        private string getSchedCmpStartDate2a()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate2a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate2a()
        {

            if (this.SchedCmpStartDate2a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate2a);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate2a();
            }
        }

        private string getSchedCmpStartDate3a()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate3a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate3a()
        {

            if (this.SchedCmpStartDate3a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate3a);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate3a();
            }
        }

        private string getSchedCmpStartDate4a()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate4a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate4a()
        {

            if (this.SchedCmpStartDate4a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate4a);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate4a();
            }
        }

        private string getSchedCmpStartDate5a()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate5a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate5a()
        {

            if (this.SchedCmpStartDate5a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate5a);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate5a();
            }
        }

        private string getSchedCmpStartDate6a()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate6a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate6a()
        {

            if (this.SchedCmpStartDate6a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate6a);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate6a();
            }
        }


        private string getSchedCmpStartDate7a()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate7a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate7a()
        {

            if (this.SchedCmpStartDate7a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate7a);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate7a();
            }
        }

        private string getSchedcmpStartTimea()
        {
            return SchedCmpStartDatea.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTimea()
        {
            if (this.SchedCmpStartDatea.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTimea);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTimea();
            }
        }

        private string getSchedcmpStartTime2a()
        {
            return SchedCmpStartDate2a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime2a()
        {
            if (this.SchedCmpStartDate2a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime2a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime2a();
            }
        }


        private string getSchedcmpStartTime3a()
        {
            return SchedCmpStartDate3a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime3a()
        {
            if (this.SchedCmpStartDate3a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime3a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime3a();
            }
        }

        private string getSchedcmpStartTime4a()
        {
            return SchedCmpStartDate4a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime4a()
        {
            if (this.SchedCmpStartDate4a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime4a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime4a();
            }
        }


        private string getSchedcmpStartTime5a()
        {
            return SchedCmpStartDate5a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime5a()
        {
            if (this.SchedCmpStartDate5a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime5a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime5a();
            }
        }

        private string getSchedcmpStartTime6a()
        {
            return SchedCmpStartDate6a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime6a()
        {
            if (this.SchedCmpStartDate6a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime6a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime6a();
            }
        }

        private string getSchedcmpStartTime7a()
        {
            return SchedCmpStartDate7a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime7a()
        {
            if (this.SchedCmpStartDate7a.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime7a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime7a();
            }
        }

        private string getSchedcmpStartTime()
        {
            return SchedCmpStartDate.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime()
        {
            if (this.SchedCmpStartDate.InvokeRequired)
            {
                   // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime);
                return this.Invoke(d).ToString();

            }
            else{
                return this.getSchedcmpStartTime();
            }
        }


        private string getSchedCmpStartDate2()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate2.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate2()
        {

            if (this.SchedCmpStartDate2.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate2);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate2();
            }
        }

        private string getSchedcmpStartTime2()
        {
            return SchedCmpStartDate2.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime2()
        {
            if (this.SchedCmpStartDate2.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime2);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime2();
            }
        }


        private string getSchedCmpStartDate3()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate3.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate3()
        {

            if (this.SchedCmpStartDate3.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate3);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate3();
            }
        }

        private string getSchedcmpStartTime3()
        {
            return SchedCmpStartDate3.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime3()
        {
            if (this.SchedCmpStartDate3.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime3);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime3();
            }
        }


        private string getSchedCmpStartDate4()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate4.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate4()
        {

            if (this.SchedCmpStartDate4.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate4);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate4();
            }
        }

        private string getSchedcmpStartTime4()
        {
            return SchedCmpStartDate4.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime4()
        {
            if (this.SchedCmpStartDate4.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime4);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime4();
            }
        }


        private string getSchedCmpStartDate5()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate5.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate5()
        {

            if (this.SchedCmpStartDate5.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate5);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate5();
            }
        }

        private string getSchedcmpStartTime5()
        {
            return SchedCmpStartDate5.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime5()
        {
            if (this.SchedCmpStartDate5.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime5);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime5();
            }
        }


        private string getSchedCmpStartDate6()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate6.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate6()
        {

            if (this.SchedCmpStartDate6.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate6);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate6();
            }
        }

        private string getSchedcmpStartTime6()
        {
            return SchedCmpStartDate6.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime6()
        {
            if (this.SchedCmpStartDate6.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime6);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime6();
            }
        }


        private string getSchedCmpStartDate7()
        {
            //return SchedCmpStartDate.Text.ToString();
            return SchedCmpStartDate7.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpStartDate7()
        {

            if (this.SchedCmpStartDate7.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedCmpStartDate7);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getSchedCmpStartDate7();
            }
        }

        private string getSchedcmpStartTime7()
        {
            return SchedCmpStartDate7.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpStartTime7()
        {
            if (this.SchedCmpStartDate7.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getSchedcmpStartTime7);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpStartTime7();
            }
        }

        private string getSchedCmpEndDatea()
        {
            return SchedCmpEndDatea.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDatea()
        {
            if (this.SchedCmpEndDatea.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDatea);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDatea();
            }
        }

        private string getSchedCmpEndDate2a()
        {
            return SchedCmpEndDate2a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate2a()
        {
            if (this.SchedCmpEndDate2a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate2a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate2a();
            }
        }

        private string getSchedCmpEndDate3a()
        {
            return SchedCmpEndDate3a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate3a()
        {
            if (this.SchedCmpEndDate3a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate3a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate3a();
            }
        }

        private string getSchedCmpEndDate4a()
        {
            return SchedCmpEndDate4a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate4a()
        {
            if (this.SchedCmpEndDate4a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate4a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate4a();
            }
        }

        private string getSchedCmpEndDate5a()
        {
            return SchedCmpEndDate5a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate5a()
        {
            if (this.SchedCmpEndDate5a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate5a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate5a();
            }
        }

        private string getSchedCmpEndDate6a()
        {
            return SchedCmpEndDate6a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate6a()
        {
            if (this.SchedCmpEndDate6a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate6a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate6a();
            }
        }

        private string getSchedCmpEndDate7a()
        {
            return SchedCmpEndDate7a.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate7a()
        {
            if (this.SchedCmpEndDate7a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate7a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate7a();
            }
        }

        private string getSchedcmpEndTimea()
        {
            return SchedCmpEndDatea.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTimea()
        {
            if (this.SchedCmpEndDatea.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTimea);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTimea();
            }

        }

        private string getSchedcmpEndTime2a()
        {
            return SchedCmpEndDate2a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime2a()
        {
            if (this.SchedCmpEndDate2a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime2a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime2a();
            }

        }

        private string getSchedcmpEndTime3a()
        {
            return SchedCmpEndDate3a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime3a()
        {
            if (this.SchedCmpEndDate3a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime3a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime3a();
            }

        }

        private string getSchedcmpEndTime4a()
        {
            return SchedCmpEndDate4a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime4a()
        {
            if (this.SchedCmpEndDate4a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime4a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime4a();
            }

        }

        private string getSchedcmpEndTime5a()
        {
            return SchedCmpEndDate5a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime5a()
        {
            if (this.SchedCmpEndDate5a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime5a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime5a();
            }

        }

        private string getSchedcmpEndTime6a()
        {
            return SchedCmpEndDate6a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime6a()
        {
            if (this.SchedCmpEndDate6a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime6a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime6a();
            }

        }

        private string getSchedcmpEndTime7a()
        {
            return SchedCmpEndDate7a.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime7a()
        {
            if (this.SchedCmpEndDate7a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime7a);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime7a();
            }

        }

        private string getSchedCmpEndDate()
        {
            return SchedCmpEndDate.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate()
        {
            if (this.SchedCmpEndDate.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate();
            }
        }
        
        private string getSchedcmpEndTime()
        {
            return SchedCmpEndDate.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime()
        {
            if (this.SchedCmpEndDate.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime();
            }

        }

        private string getSchedCmpEndDate2()
        {
            return SchedCmpEndDate2.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate2()
        {
            if (this.SchedCmpEndDate2.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate2);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate2();
            }
        }

        private string getSchedcmpEndTime2()
        {
            return SchedCmpEndDate2.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime2()
        {
            if (this.SchedCmpEndDate2.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime2);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime2();
            }

        }


        private string getSchedCmpEndDate3()
        {
            return SchedCmpEndDate3.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate3()
        {
            if (this.SchedCmpEndDate3.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate3);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate3();
            }
        }

        private string getSchedcmpEndTime3()
        {
            return SchedCmpEndDate3.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime3()
        {
            if (this.SchedCmpEndDate3.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime3);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime3();
            }

        }

        private string getSchedCmpEndDate4()
        {
            return SchedCmpEndDate4.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate4()
        {
            if (this.SchedCmpEndDate4.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate4);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate4();
            }
        }

        private string getSchedcmpEndTime4()
        {
            return SchedCmpEndDate4.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime4()
        {
            if (this.SchedCmpEndDate4.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime4);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime4();
            }

        }

        private string getSchedCmpEndDate5()
        {
            return SchedCmpEndDate5.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate5()
        {
            if (this.SchedCmpEndDate5.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate5);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate5();
            }
        }

        private string getSchedcmpEndTime5()
        {
            return SchedCmpEndDate5.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime5()
        {
            if (this.SchedCmpEndDate5.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime5);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime5();
            }

        }

        private string getSchedCmpEndDate6()
        {
            return SchedCmpEndDate6.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate6()
        {
            if (this.SchedCmpEndDate6.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate6);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate6();
            }
        }

        private string getSchedcmpEndTime6()
        {
            return SchedCmpEndDate6.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime6()
        {
            if (this.SchedCmpEndDate6.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime6);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime6();
            }

        }

        private string getSchedCmpEndDate7()
        {
            return SchedCmpEndDate7.Value.ToString("MM/dd/yyyy");
        }

        public string pGetSchedCmpEndDate7()
        {
            if (this.SchedCmpEndDate7.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedCmpEndDate7);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getSchedCmpEndDate7();
            }
        }

        private string getSchedcmpEndTime7()
        {
            return SchedCmpEndDate7.Value.ToString("hh:mm tt");
        }

        public string pGetSchedcmpEndTime7()
        {
            if (this.SchedCmpEndDate7.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedcmpEndTime7);
                return this.Invoke(d).ToString();

            }
            else
            {
                return this.getSchedcmpEndTime7();
            }

        }

       /* public delegate void GridDelegate();
        public void updateGrid()
        {
            SQLinterface SQL = new SQLinterface();
            DataTable table = new DataTable();
            table = SQL.query("SELECT Run,Campaign,LDB,ID FROM TextCampaign ORDER BY ID");
            this.vwTextManagerBindingSource.DataSource = table;
            this.CampaignGrid.DataSource = vwTextManagerBindingSource;
            this.CampaignGrid.Refresh();
        }
        */


        public void setimpSuccess(){
            impSuccess.Text = (Convert.ToInt32(impSuccess.Text) + 1).ToString();
            impAddTotal.Text = (Convert.ToInt32(impAddTotal.Text) + 1).ToString();
        }

        public void setimpFail()
        {
            impFail.Text = (Convert.ToInt32(impFail.Text) + 1).ToString(); 
        }

        public void pSetimpSuccess()
        {
            if (this.impSuccess.InvokeRequired)
            {

              this.Invoke(new MethodInvoker(delegate() {  setimpSuccess();}));
                
            }
            else
            {
                this.setimpSuccess();
            }
        }


        public void pSetimpFail()
        {
            if (this.impFail.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate() { setimpFail(); }));
                

            }
            else
            {
                this.setimpFail();
            }
        }

        private string getrepeat1()
        {
            return repeat1.Checked ? "1" : "0";
        }

        public int pGetrepeat1()
        {
            if (this.repeat1.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getrepeat1);
                return Int32.Parse(this.Invoke(d).ToString());
            }
            else
            {
                return Int32.Parse(this.getrepeat1());
            }
        }

        private string getrepeat1a()
        {
            return repeat1a.Checked ? "1" : "0";
        }

        public int pGetrepeat1a()
        {
            if (this.repeat1a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getrepeat1a);
                return Int32.Parse(this.Invoke(d).ToString());
            }
            else
            {
                return Int32.Parse(this.getrepeat1a());
            }
        }

  /*
        private string getWeekly1()
        {
            return daily1.Checked ? "1" : "0";
        }

        public int pGetWeekly1()
        {
            if (this.daily1.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getWeekly1);
                return Int32.Parse(this.Invoke(d).ToString());
            }
            else
            {
                return Int32.Parse(this.getWeekly1());
            }
        }
        */
        public string pGetCmpAddName()
        {
            if (this.cmpAddName.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getCmpAddName);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getCmpAddName();
            }
        }

        public string getCmpModID()
        {
            return ((ListItem)this.modCmpbox1.SelectedItem).ID;
        }

        public string getCmpID()
        {
            return ((ListItem)this.cmbDelbox1.SelectedItem).ID;
        }

        public string getSchedID()
        {
            return ((ListItem)this.SchedCmpBox1.SelectedItem).ID;
        }

        public void refreshcmbDelbox1()
        {
            this.cmbDelbox1.Refresh();
        }

        public void prefreshcmbDelbox1()
        {
            if (this.cmbDelbox1.InvokeRequired)
            {

                this.Invoke(new MethodInvoker(delegate() { this.cmbDelbox1.Items.Clear(); updateCmpTabs(); })); 
            }
            else
            {
                this.refreshcmbDelbox1();

            }
        }

        public string pGetModID()
        {
            /*
               string IDs = "";
             ListItem item;
             item = (ListItem)this.cmbDelbox1.SelectedItem;
             IDs = item.ID;
             return IDs;
            */

            if (this.modCmpbox1.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCmpModID);
                return this.Invoke(d).ToString();
            }
            else
            {

                return this.getCmpModID();
            }
        }

/*
        public string getimpMapSourceID()
        {
            return ((ListItem)this.impMapSource.SelectedItem).ID;
        }

        public string pGetimpMapSourceID()
        {

            if (this.impMapSource.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getimpMapSourceID);
                return this.Invoke(d).ToString();
            }
            else
            {

                return this.impMapSource();
            }

        }
        */

        public string pGetID()
        {
           /*
              string IDs = "";
            ListItem item;
            item = (ListItem)this.cmbDelbox1.SelectedItem;
            IDs = item.ID;
            return IDs;
           */

            if (this.cmbDelbox1.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCmpID);
                return this.Invoke(d).ToString();
            }
            else
            {
               
                return this.getCmpID();
            }
        }

        public string getCheckBox1a()
        {
            return checkBox1a.Checked ? "1" : "0";
        }

        public string pGetcheckBox1a()
        {
            if (this.checkBox1a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox1a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox1a();
            }
        }

        public string getCheckBox2a()
        {
            return checkBox2a.Checked ? "1" : "0";
        }

        public string pGetcheckBox2a()
        {
            if (this.checkBox2a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox2a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox2a();
            }
        }

        public string getCheckBox3a()
        {
            return checkBox3a.Checked ? "1" : "0";
        }

        public string pGetcheckBox3a()
        {
            if (this.checkBox3a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox3a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox3a();
            }
        }

        public string getCheckBox4a()
        {
            return checkBox4a.Checked ? "1" : "0";
        }

        public string pGetcheckBox4a()
        {
            if (this.checkBox4a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox4a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox4a();
            }
        }

        public string getCheckBox5a()
        {
            return checkBox5a.Checked ? "1" : "0";
        }

        public string pGetcheckBox5a()
        {
            if (this.checkBox5a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox5a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox5a();
            }
        }

        public string getCheckBox6a()
        {
            return checkBox6a.Checked ? "1" : "0";
        }

        public string pGetcheckBox6a()
        {
            if (this.checkBox6a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox6a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox6a();
            }
        }

        public string getCheckBox7a()
        {
            return checkBox7a.Checked ? "1" : "0";
        }

        public string pGetcheckBox7a()
        {
            if (this.checkBox7a.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox7a);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox7a();
            }
        }

        public string getCheckBox1()
        {
            return checkBox1.Checked ? "1" : "0";
        }

        public string pGetcheckBox1()
        {
            if (this.checkBox1.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox1);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox1();
            }
        }

        public string getCheckBox2()
        {
            return checkBox2.Checked ? "1" : "0";
        }

        public string pGetcheckBox2()
        {
            if (this.checkBox2.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox2);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox2();
            }
        }

        public string getCheckBox3()
        {
            return checkBox3.Checked ? "1" : "0";
        }

        public string pGetcheckBox3()
        {
            if (this.checkBox3.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox3);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox3();
            }
        }

        public string getCheckBox4()
        {
            return checkBox4.Checked ? "1" : "0";
        }

        public string pGetcheckBox4()
        {
            if (this.checkBox4.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox4);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox4();
            }
        }

        public string getCheckBox5()
        {
            return checkBox5.Checked ? "1" : "0";
        }

        public string pGetcheckBox5()
        {
            if (this.checkBox5.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox5);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox5();
            }
        }

        public string getCheckBox6()
        {
            return checkBox6.Checked ? "1" : "0";
        }

        public string pGetcheckBox6()
        {
            if (this.checkBox6.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox6);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox6();
            }
        }

        public string getCheckBox7()
        {
            return checkBox7.Checked ? "1" : "0";
        }

        public string pGetcheckBox7()
        {
            if (this.checkBox7.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getCheckBox7);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getCheckBox7();
            }
        }

        private string getimpMapSourceName()
        {
            return ((ListItem)this.impMapSource.SelectedItem).Name;
        }

        public string pGetimpMapSourceName()
        {
            if (this.impMapSource.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getimpMapSourceName);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getimpMapSourceName();
            }
        }

        private string getimpMapSourceID(){
            return ((ListItem)this.impMapSource.SelectedItem).ID;
        }

        public string pGetimpMapSourceID()
        {
            if (this.impMapSource.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getimpMapSourceID);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getimpMapSourceID();
            }
        }

        private string getviewLeads()
        {
            return ((ListItem)this.viewLeads.SelectedItem).ID;
        }

        public string pGetviewLeads()
        {
            if (this.viewLeads.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getviewLeads);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getviewLeads();
            }
        }

        private string getImportHistory()
        {
            return ((ListItem)this.ImportHistory.SelectedItem).Name;
        }

        public string pGetImportHistory()
        {
            if (this.ImportHistory.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getImportHistory);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getImportHistory();
            }
        }


        private string getcmpSelDD1ID()
        {
            return ((ListItem)this.cmpSelDD1.SelectedItem).ID;
        }

        public string pGetcmpSelDD1ID()
        {
            if (this.cmpSelDD1.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getcmpSelDD1ID);
                return this.Invoke(d).ToString();
            }
            else
            {
                return this.getcmpSelDD1ID();
            }
        }

        public string pGetSchedID()
        {

            
            if (this.SchedCmpBox1.InvokeRequired)
            {
                stringDelegate d = new stringDelegate(getSchedID);
                return this.Invoke(d).ToString();
            }
            else
            {

                return this.getSchedID();
            }
        }

        private string getschname(){
            return schname.Text;
        }

        public string pGetschname()
        {
            if (this.schname.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getschname);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getschname();
            }

        }

        public string pGetCmpAddLS()
        {
            if (this.cmpAddSource.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getCmpAddLS);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getCmpAddLS();
            }
        }
        public string pGetCmpAddSD()
        {
            if (this.cmpAddSD.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getCmpAddSD);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getCmpAddSD();
            }
        }

        public string pGetCmpModSource()
        {
            if (this.modCmpSource.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getCmpModSource);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getCmpModSource();
            }
        }

        public string pGetCmpModSD()
        {
            if (this.modCmpSD.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getCmpModSD);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getCmpModSD();
            }
        }

        private string getCmpModSch()
        {
            string IDs = "";
            ListItem item;
            item = (ListItem)this.modSchedule.SelectedItem;
            IDs = item.ID;

            return IDs;
        }

        public string pGetCmpModSch()
        {
            if (this.modSchedule.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getCmpModSch);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getCmpModSch();
            }
        }

        private string getCmpModMessage()
        {
            string IDs = "";
            ListItem item;
            item = (ListItem)this.modMessages.SelectedItem;
            IDs = item.ID;

            return IDs;
        }

        public string pGetCmpModMessage()
        {
            if (this.modMessages.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getCmpModMessage);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getCmpModMessage();
            }
        }

        public string pGetCmpAddSch()
        {
            if (this.cmpAddMsg.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getCmpAddSch);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getCmpAddSch();
            }
        }

        public string pGetCmpAddMsg()
        {
            if (this.cmpAddMsg.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getCmpAddMsg);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getCmpAddMsg();
            }
        }
        public string pGetLSAddName()
        {
            if (this.LSAddName.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getLSAddName);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getLSAddName();
            }
        }


        private string getimpFilePath()
        {
            return impFilePath.Text;
        }

        public string pGetImpFilePath()
        {
            if (this.impFilePath.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getimpFilePath);
                return this.Invoke(d).ToString();
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getimpFilePath();
            }
        }


        public ListItem pGetImpMapFullName()
        {
            if (this.impMapFullName.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapFullName);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapFullName();
            }
        }
        public ListItem pGetImpMapFirstName()
        {
            if (this.impMapFName.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapFirstName);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapFirstName();
            }
        }
        public ListItem pGetImpMapLastName()
        {
            if (this.impMapLName.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapLastName);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapLastName();
            }
        }
        public ListItem pGetImpMapAddrOne()
        {
            if (this.impMapAddrOne.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapAddrOne);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapAddrOne();
            }
        }
        public ListItem pGetImpMapAddrTwo()
        {
            if (this.impMapAddrTwo.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapAddrTwo);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapAddrTwo();
            }
        }
        public ListItem pGetImpMapCity()
        {
            if (this.impMapCity.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapCity);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapCity();
            }
        }
        public ListItem pGetImpMapState()
        {
            if (this.impMapState.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapState);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapState();
            }
        }
        public ListItem pGetImpMapZipCode()
        {
            if (this.impMapZipCode.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapZipCode);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapZipCode();
            }
        }

        private string getBCRadioButton01()
        {
            return Convert.ToString( BCRadioButton01.Checked ? 1 : 0);
        }

        public string pGetBCRadioButton01()
        {
            if (this.BCRadioButton01.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getBCRadioButton01);
                return (string)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getBCRadioButton01();
            }
        }


        private string getBCRadioButton02()
        {
            return Convert.ToString(BCRadioButton02.Checked ? 1 : 0);
        }

        public string pGetBCRadioButton02()
        {
            if (this.BCRadioButton02.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                stringDelegate d = new stringDelegate(getBCRadioButton02);
                return (string)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getBCRadioButton02();
            }
        }


        public ListItem pGetImpMapPhone()
        {
            if (this.impMapPhone.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapPhone);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapPhone();
            }
        }
        public ListItem pGetImpMapURL()
        {
            if (this.impMapURL.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapURL);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapURL();
            }
        }
        public ListItem pGetImpMapOptIN()
        {
            if (this.impMapOptInDate.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapOptIN);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapOptIN();
            }
        }
        public ListItem pGetImpMapIPAddr()
        {
            if (this.impMapIPaddr.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getImpMapIPAddr);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getImpMapIPAddr();
            }
        }
        public ListItem pGetManDelSourceSel()
        {
            if (this.cmpAddName.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                ListItemDelegate d = new ListItemDelegate(getManDelSourceSel);
                return (ListItem)this.Invoke(d);
            }
            else
            {
                // It's on the same thread, no need for Invoke
                return this.getManDelSourceSel();
            }
        }
        //delegates
        public delegate void voidDelegate(string text);
        public delegate string stringDelegate();
        public delegate ListItem ListItemDelegate();
        public delegate bool boolDelegate();
        //private methods
        private void ImpAppendText(string text)
        {
            this.impImportConsole.AppendText(text);
        }

        private void AppendText(string text)
        {
            this.console.AppendText(text);
        }
        private void OpenDialog(string text)
        {
            DlgUpdate Dialog = new DlgUpdate();
            Dialog.DlgTxt.Text = text;
            Dialog.ShowDialog(this);
        }
        private string getCmpAddName()
        {
            return this.cmpAddName.Text;
        }

      

        private string getCmpAddLS()
        {
            string IDs = "";
            ListItem item;
            foreach (int i in this.cmpAddSource.CheckedIndices)
            {
                item = (ListItem)this.cmpAddSource.Items[i];
                IDs += item.ID + ",";
            }
            if (IDs.Length > 0)
            {
                IDs = IDs.Substring(0, IDs.Length - 1);
            }
            return IDs;
        }
        private string getCmpAddSD()
        {
            string IDs = "";
            ListItem item;
            foreach (int i in this.cmpAddSD.CheckedIndices)
            {
                item = (ListItem)this.cmpAddSD.Items[i];
                IDs += item.ID + ",";
            }
            if (IDs.Length > 0)
            {
                IDs = IDs.Substring(0, IDs.Length - 1);
            }
            return IDs;
        }

        private string getCmpModSource()
        {
            string IDs = "";
            ListItem item;
            foreach (int i in this.modCmpSource.CheckedIndices)
            {
                item = (ListItem)this.modCmpSource.Items[i];
                IDs += item.ID + ",";
            }
            if (IDs.Length > 0)
            {
                IDs = IDs.Substring(0, IDs.Length - 1);
            }
            return IDs;
        }

        private string getCmpModSD()
        {
            string IDs = "";
            ListItem item;
            foreach (int i in this.modCmpSD.CheckedIndices)
            {
                item = (ListItem)this.modCmpSD.Items[i];
                IDs += item.ID + ",";
            }
            if (IDs.Length > 0)
            {
                IDs = IDs.Substring(0, IDs.Length - 1);
            }
            return IDs;
        }

        private string getCmpAddSch()
        {
            string IDs = "";
            ListItem item;
            item = (ListItem)this.cmpAddSch1.SelectedItem;
            if (item == null)
            {
                this.cmpAddSch1.SelectedIndex = 0;
                item = (ListItem)this.cmpAddSch1.SelectedItem;
            }
            IDs = item.ID;

            return IDs;
        }

        private string getCmpAddMsg()
        {
            string IDs = "";
            ListItem item;
            item = (ListItem)this.cmpAddMsg.SelectedItem;
            IDs = item.ID;
            
            return IDs;
        }
        private string getLSAddName()
        {
            return this.LSAddName.Text;
        }
        private ListItem getImpMapFullName()
        {
            return (ListItem)this.impMapFullName.SelectedItem;
        }

        private ListItem getImpMapFirstName()
        {
            return (ListItem)this.impMapFName.SelectedItem;
        }
        private ListItem getImpMapLastName()
        {
            return (ListItem)this.impMapLName.SelectedItem;
        }
        private ListItem getImpMapAddrOne()
        {
            return (ListItem)this.impMapAddrOne.SelectedItem;
        }
        private ListItem getDeletecmp()
        {
            return (ListItem)this.cmbDelbox1.SelectedItem;
        }
        private ListItem getImpMapAddrTwo()
        {
            return (ListItem)this.impMapAddrTwo.SelectedItem;
        }
        private ListItem getImpMapCity()
        {
            return (ListItem)this.impMapCity.SelectedItem;
        }
        private ListItem getImpMapState()
        {
            return (ListItem)this.impMapState.SelectedItem;
        }
        private ListItem getImpMapZipCode()
        {
            return (ListItem)this.impMapZipCode.SelectedItem;
        }

        private ListItem getmsgDelDD()
        {
            return (ListItem)this.msgDelDD.SelectedItem;
        }

        private ListItem getmsgModDD()
        {
            return (ListItem)this.msgModDD.SelectedItem;
        }

        private ListItem getImpMapPhone()
        {
            return (ListItem)this.impMapPhone.SelectedItem;
        }
        private ListItem getImpMapIPAddr()
        {
            return (ListItem)this.impMapIPaddr.SelectedItem;
        }
        private ListItem getImpMapURL()
        {
            return (ListItem)this.impMapURL.SelectedItem;
        }
        private ListItem getImpMapOptIN()
        {
            return (ListItem)this.impMapOptInDate.SelectedItem;
        }
        private ListItem getManDelSourceSel()
        {
            return (ListItem)this.manDelSourceSel.SelectedItem;
        }
        //server commands
        private void launchAdd(object sender, EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.cmdAdd));
            this.Thread.Start();
        }
        private void launchSend(object sender, EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.cmdSend));
            this.Thread.Start();
        }
        private void Exit(object sender, EventArgs e)
        {
            if (Application.AllowQuit)
            {
                Application.Exit();
            }
        }
        private void saveSettings(object sender, EventArgs e)
        {
            Properties.Settings.Default.ServerIP = serverIP.Text;
            Properties.Settings.Default.ServerPort = Int32.Parse(serverPort.Text);
            Properties.Settings.Default.Verbose = verbose.SelectedIndex;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'lDBDataSet1.refLS' table. You can move, or remove it, as needed.
            
            

        }

        private void FindFile(object sender, EventArgs e)
        {
            impFileSelect.ShowDialog();
            updateImportTabs();
        }

        private void FileSelected(object sender, CancelEventArgs e)
        {
            impFilePath.Text = impFileSelect.FileName;
            impFilePath.ReadOnly = true;
            impBrowse.Enabled = false;
            FileMapping();
            impTabs.SelectedTab = impTabs.TabPages[1];
        }

        private void cmpSelB1_Click(object sender, EventArgs e)
        {
            LDBSQL SQL = new LDBSQL();
            string str = "";
            string sql = "SELECT ID,Message,Rate,Sources FROM Campaigns where ID=" + this.pGetcmpSelDD1ID();
            DataTable record=SQL.query(sql);
            foreach(DataRow r in record.Rows){
                trackBar1.Value = Convert.ToInt32(r["Rate"]);
                RateBox2.Text = r["Rate"].ToString();
                str = r["Message"].ToString();
                CampID.Text = r["ID"].ToString();
            }
            sql = "SELECT Message FROM Messages where ID='" + str+ "'";
            record = SQL.query(sql);
            foreach (DataRow r in record.Rows)
            {
                MsgTxt.Text = r["Message"].ToString();
            }
            /*
            DataGridViewColumn Source = new DataGridViewColumn();
            DataGridViewCell cell = new DataGridViewTextBoxCell();
            Source.CellTemplate = cell;

            Source.HeaderText = "Source";
            Source.Name = "Source";
            Source.Visible = true;
            Source.Width = 100;
            dataGridView1.Columns.Add(Source);

            DataGridViewColumn RemainingLeads = new DataGridViewColumn();
            DataGridViewCell cell2 = new DataGridViewTextBoxCell();
            RemainingLeads.CellTemplate = cell2;

            RemainingLeads.HeaderText = "Remaining Leads";
            RemainingLeads.Name = "Remaining Leads";
            RemainingLeads.Visible = true;
            RemainingLeads.Width = 100;
            dataGridView1.Columns.Add(RemainingLeads);

            

            this.dataGridView1.Rows[0].Cells[0].Value = "new value";*/

        }

        private void cmpAddB1_Click(object sender, EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.cmpAdd));
            this.Thread.Start();
        
            
           
        }

        private void manAddB1_Click(object sender, EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.LSAdd));
            this.Thread.Start();
            int n = dataGridView1.Rows.Add();

            dataGridView1.Rows[n].Cells[0].Value = "title";
            dataGridView1.Rows[n].Cells[1].Value = "dateTimeNow";
          
        }
        private void manDelB1_Click(object sender, EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.LSDel));
            this.Thread.Start();
          
        }
        private void FileMapping()
        {
            CsvFileReader Reader = new CsvFileReader(impFilePath.Text);
            CsvRow Row = new CsvRow();
            Reader.ReadRow(Row);
            int i = 0;
            ListItem iNA = new ListItem("-1", "N/A");
            impMapFullName.Items.Add(iNA);
            impMapFName.Items.Add(iNA);
            impMapLName.Items.Add(iNA);
            impMapAddrOne.Items.Add(iNA);
            impMapAddrTwo.Items.Add(iNA);
            impMapCity.Items.Add(iNA);
            impMapState.Items.Add(iNA);
            impMapZipCode.Items.Add(iNA);
            impMapPhone.Items.Add(iNA);
            impMapURL.Items.Add(iNA);
            impMapIPaddr.Items.Add(iNA);
            impMapOptInDate.Items.Add(iNA);
            impMapFullName.SelectedItem = iNA;
            impMapFName.SelectedItem = iNA;
            impMapLName.SelectedItem = iNA;
            impMapAddrOne.SelectedItem = iNA;
            impMapAddrTwo.SelectedItem = iNA;
            impMapCity.SelectedItem = iNA;
            impMapState.SelectedItem = iNA;
            impMapZipCode.SelectedItem = iNA;
            impMapPhone.SelectedItem = iNA;
            impMapURL.SelectedItem = iNA;
            impMapIPaddr.SelectedItem = iNA;
            impMapOptInDate.SelectedItem = iNA;
            foreach (string s in Row)
            {
                ListItem item = new ListItem(i.ToString(), s);
                impMapFullName.Items.Add(item);
                impMapFName.Items.Add(item);
                impMapLName.Items.Add(item);
                impMapAddrOne.Items.Add(item);
                impMapAddrTwo.Items.Add(item);
                impMapCity.Items.Add(item);
                impMapState.Items.Add(item);
                impMapZipCode.Items.Add(item);
                impMapPhone.Items.Add(item);
                impMapURL.Items.Add(item);
                impMapIPaddr.Items.Add(item);
                impMapOptInDate.Items.Add(item);
                i++;
            }

        }

        private void deFileMapping()
        {

            impMapFullName.Items.Clear();
            impMapFName.Items.Clear();
            impMapLName.Items.Clear();
            impMapAddrOne.Items.Clear();
            impMapAddrTwo.Items.Clear();
            impMapCity.Items.Clear();
            impMapState.Items.Clear();
            impMapZipCode.Items.Clear();
            impMapPhone.Items.Clear();
            impMapURL.Items.Clear();
            impMapIPaddr.Items.Clear();
            impMapOptInDate.Items.Clear();
        
           

        }

        private void updateMainTabs(object sender, EventArgs e)
        {
            switch(mainTabs.SelectedIndex)
            {
                case 0:
                    updateCmpTabs();
                   
                    break;
                case 5:
     //               updateImportTabs();
                    updateSourceTabs();
                    break;
                case 6:
                    updateSourceTabs();
                    break;
                default:
                    break;
            }
        }

        /*
        public void updateTabs()
        {
            manDelSourceSel.Items.Clear();
            msgDelDD.Items.Clear();
            msgModDD.Items.Clear();
            SchedCmpBox1.Items.Clear();
            cmpAddSch1.Items.Clear();
            cmpAddSource.Items.Clear();
            cmpAddSD.Items.Clear();
            cmpAddMsg.Items.Clear();
            cmpSelDD1.Items.Clear();
            ListItem iNone = new ListItem("-1", "None");
            cmpAddSource.Items.Add(iNone, false);
            cmpAddSD.Items.Add(iNone, false);
            cmpAddMsg.Items.Add(iNone);
            modMessages.Items.Add(iNone);
            modCmpSD.Items.Add(iNone);
            modSchedule.Items.Add(iNone);
            modCmpSource.Items.Add(iNone);
            cmpAddMsg.SelectedItem = iNone;
            LDBSQL SQL = new LDBSQL();
            DataTable table = new DataTable();
            table = SQL.query("SELECT ID,Name FROM refLS ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    cmpAddSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);
                    modCmpSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);

                }
            }

            

            table = SQL.query("SELECT ID,Name from Messages ORDER BY ID");
            if(table.TableName != "Error"){

                foreach(DataRow r in table.Rows){
                  modMessages.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    
                }

            }
            modMessages.SelectedIndex = 1;
            table = SQL.query("SELECT ID,Name from Schedule ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach(DataRow r in table.Rows){
                    modSchedule.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }

            }
            modSchedule.SelectedIndex = 1;
            table = SQL.query("SELECT ID,State FROM StateAreaCodes ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    cmpAddSD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);
                }
            }
            table = SQL.query("SELECT ID,State FROM StateAreaCodes ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    modCmpSD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);
                }
            }
            table = SQL.query("SELECT ID,Name FROM Messages ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    cmpAddMsg.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
            }
            table = SQL.query("SELECT ID,Name FROM Campaigns ORDER BY ID");
            if (table.TableName != "Error")
            {

                foreach (DataRow r in table.Rows)
                {
                    cmpSelDD1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
                cmpSelDD1.SelectedIndex = 1;
            }
            table = SQL.query("SELECT ID,Name FROM Campaigns ORDER BY ID");
            if (table.TableName != "Error")
            {

                foreach (DataRow r in table.Rows)
                {
                    cmbDelbox1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
                cmbDelbox1.SelectedIndex = 1;
            }
            table = SQL.query("SELECT ID,Name FROM Campaigns ORDER BY ID");
            if (table.TableName != "Error")
            {

                foreach (DataRow r in table.Rows)
                {
                    modCmpbox1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
                modCmpbox1.SelectedIndex = 1;
            }

            table = SQL.query("SELECT ID,Name FROM Schedule ORDER BY ID");
            if (table.TableName != "Error")
            {

                foreach (DataRow r in table.Rows)
                {
                    SchedCmpBox1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
                SchedCmpBox1.SelectedIndex = 1;
            }



            table = SQL.query("SELECT ID,Name FROM Messages where Inactive= 0 ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    msgDelDD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    msgModDD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));

                }
                msgModDD.SelectedIndex = 1;
                msgDelDD.SelectedIndex = 1;
            }

            table = SQL.query("SELECT ID,Name FROM Schedule ORDER BY ID");
            if (table.TableName != "Error")
            {

                foreach (DataRow r in table.Rows)
                {
                    cmpAddSch1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));


                }
                cmpAddSch1.SelectedIndex = 1;
            }

            updateImportTabs();
            updateSourceTabs();
        }
        */

      


        public void myUpdateCmpTabs()
        {
            if ((this.cmpAddMsg.InvokeRequired) && (this.cmpAddSch1.InvokeRequired) && (this.cmpSelDD1.InvokeRequired) && (cmpSelDD1.InvokeRequired) && (impMapSource.InvokeRequired) && (manDelSourceSel.InvokeRequired) && (SchedCmpBox1.InvokeRequired) && (modSchedule.InvokeRequired) && (manDelSourceSel.InvokeRequired) && (impMapSource.InvokeRequired))
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    viewLeads.Items.Clear();
                    ImportHistory.Items.Clear();
                    modSchedule.Items.Clear();
                    SchedCmpBox1.Items.Clear();
                    manDelSourceSel.Items.Clear();
                    impMapSource.Items.Clear(); 
                    cmpAddMsg.Items.Clear();
                    modCmpSource.Items.Clear();
                    cmpAddSch1.Items.Clear();
                    msgModDD.Items.Clear();
                    //   cmpAddSource.Items.Clear();
                    cmpSelDD1.Items.Clear();
                    cmbDelbox1.Items.Clear();
                    modCmpbox1.Items.Clear();
                    SchedCmpBox1.Items.Clear();
                    cmpAddSource.Items.Clear();
                    cmpAddSD.Items.Clear();
                    cmpAddMsg.Items.Clear();
                    cmpSelDD1.Items.Clear();
                    msgDelDD.Items.Clear();
                    modSchedule.Items.Clear();
                    modMessages.Items.Clear();
                    cmpAddSch1.Items.Clear();
                    ListItem iNone = new ListItem("-1", "None");
                    cmpAddSource.Items.Add(iNone, false);
                    cmpAddSD.Items.Add(iNone, false);
                    cmpAddMsg.Items.Add(iNone);
                    ImportHistory.Items.Add(iNone);
                    modCmpSD.Items.Add(iNone);
                    modMessages.Items.Add(iNone);
                    modSchedule.Items.Add(iNone);
                    modCmpSource.Items.Add(iNone);
                    SchedCmpBox1.Items.Add(iNone);
                    modCmpbox1.Items.Add(iNone);
                  //  cmpAddSD.Items.Add(iNone);
                    cmbDelbox1.Items.Add(iNone);
                    cmpSelDD1.Items.Add(iNone);
                    msgDelDD.Items.Add(iNone);
                    viewLeads.Items.Add(iNone);
                    impMapSource.Items.Add(iNone);
                    cmpAddSch1.Items.Add(iNone);
                    cmpAddMsg.SelectedItem = iNone;
                    msgModDD.Items.Add(iNone);
                    manDelSourceSel.Items.Add(iNone);
                    LDBSQL SQL = new LDBSQL();
                    DataTable table = new DataTable();
                    table = SQL.query("SELECT ID,Name FROM Campaigns where Inactive=0 ORDER BY ID");
                    if (table.TableName != "Error")
                    {

                        foreach (DataRow r in table.Rows)
                        {
                            cmpSelDD1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                        }
                        cmpSelDD1.SelectedIndex = 0;
                    }
                    table = SQL.query("SELECT ID,Name FROM Campaigns where Inactive=0 ORDER BY ID");
                    if (table.TableName != "Error")
                    {

                        foreach (DataRow r in table.Rows)
                        {
                            cmbDelbox1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                        }
                        cmbDelbox1.SelectedIndex = 0;
                    }
                    table = SQL.query("SELECT ID,Name FROM Campaigns where Inactive=0 ORDER BY ID");
                    if (table.TableName != "Error")
                    {

                        foreach (DataRow r in table.Rows)
                        {
                            modCmpbox1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                        }
                        modCmpbox1.SelectedIndex = 0;
                    }

                    table = SQL.query("SELECT ID,Name FROM refLS where Inactive=0 ORDER BY ID");
                    if (table.TableName != "Error")
                    {

                        foreach (DataRow r in table.Rows)
                        {
                            impMapSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                            manDelSourceSel.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                            
                        }
                        impMapSource.SelectedIndex = 0;
                        manDelSourceSel.SelectedIndex = 0;
                        
                    }

                    table = SQL.query("SELECT  ID,FileName  FROM Imported where DateImported in (SELECT MAX(DateImported) FROM  Imported GROUP BY FileName) ORDER BY ID");
                    if (table.TableName != "Error")
                    {

                        foreach (DataRow r in table.Rows)
                        {
                            
                            
                            viewLeads.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                        }
                        
                        
                        viewLeads.SelectedIndex = 0;
                    }
                    

                    table = SQL.query("SELECT  ID,FileName  FROM Imported where DateImported in (SELECT MAX(DateImported) FROM  Imported GROUP BY FileName) ORDER BY ID");
                    if (table.TableName != "Error")
                    {

                        foreach (DataRow r in table.Rows)
                        {


                            ImportHistory.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                        }


                        ImportHistory.SelectedIndex = 0;
                    }

                    table = SQL.query("SELECT ID,Name FROM Schedule where Inactive = 0 ORDER BY ID");
                    if (table.TableName != "Error")
                    {

                        foreach (DataRow r in table.Rows)
                        {
                            modSchedule.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                            SchedCmpBox1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                        }
                        modSchedule.SelectedIndex = 0;
                        SchedCmpBox1.SelectedIndex = 0;
                    }

                    table = SQL.query("SELECT ID,State FROM StateAreaCodes ORDER BY ID");
                    if (table.TableName != "Error")
                    {
                        foreach (DataRow r in table.Rows)
                        {
                            cmpAddSD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);
                        }
                    }
                    table = SQL.query("SELECT ID,State FROM StateAreaCodes ORDER BY ID");
                    if (table.TableName != "Error")
                    {
                        foreach (DataRow r in table.Rows)
                        {
                            modCmpSD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);
                        }
                    }


                    
                    
                    table = SQL.query("SELECT ID,Name FROM refLS ORDER BY ID");
                    if (table.TableName != "Error")
                    {
                        impMapSource.Items.Clear();
                        manDelSourceSel.Items.Clear();

                        foreach (DataRow r in table.Rows)
                        {
                            //   manModSourceSel.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                            manDelSourceSel.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                            impMapSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                        }

                    }


                    /*
                    table = SQL.query("SELECT ID,Name FROM refLS ORDER BY ID");
                    if (table.TableName != "Error")
                    {
                        impMapSource.Items.Clear();

                        foreach (DataRow r in table.Rows)
                        {
                            impMapSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                        }

                    }*/

                    table = SQL.query("SELECT ID,Name FROM refLS ORDER BY ID");
                    if (table.TableName != "Error")
                    {
                        foreach (DataRow r in table.Rows)
                        {
                            cmpAddSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);
                            modCmpSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);

                        }
                    }



                }));
            }
            else
            {
            }

        }


        

        private void updateCmpTabs()
        {
            manDelSourceSel.Items.Clear();
            cmbDelbox1.Items.Clear();
            SchedCmpBox1.Items.Clear();
            cmpAddSource.Items.Clear();
            cmpAddSD.Items.Clear();
            cmpAddMsg.Items.Clear();
            cmpSelDD1.Items.Clear();
            modSchedule.Items.Clear();
            modMessages.Items.Clear();
            cmpAddSch1.Items.Clear();
            msgDelDD.Items.Clear();
            modCmpbox1.Items.Clear();
            modCmpSource.Items.Clear();
            msgModDD.Items.Clear();
            ListItem iNone = new ListItem("-1", "None");
            cmpAddSource.Items.Add(iNone, false);
            cmpAddSD.Items.Add(iNone, false);
            cmpAddMsg.Items.Add(iNone);
            modCmpSD.Items.Add(iNone);
            modMessages.Items.Add(iNone);
            modSchedule.Items.Add(iNone);
            modCmpSource.Items.Add(iNone);
            SchedCmpBox1.Items.Add(iNone);
            modCmpbox1.Items.Add(iNone);
         
            cmbDelbox1.Items.Add(iNone);
            cmpSelDD1.Items.Add(iNone);
            msgDelDD.Items.Add(iNone);
            manDelSourceSel.Items.Add(iNone);
            impMapSource.Items.Add(iNone);
            cmpAddSch1.Items.Add(iNone);
            cmpAddMsg.SelectedItem = iNone;
            msgModDD.Items.Add(iNone);
            LDBSQL SQL = new LDBSQL();
            DataTable table = new DataTable();
            table = SQL.query("SELECT ID,Name FROM refLS ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    cmpAddSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);
                    modCmpSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);

                }
            }

          
            table = SQL.query("SELECT ID,Name from Messages where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {

                foreach (DataRow r in table.Rows)
                {
                    modMessages.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));

                }

            }
            modMessages.SelectedIndex = 0;
            table = SQL.query("SELECT ID,Name from Schedule where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    modSchedule.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }

            }
            modSchedule.SelectedIndex = 0;


            table = SQL.query("SELECT ID,State FROM StateAreaCodes ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    cmpAddSD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);
                }
            }
            table = SQL.query("SELECT ID,State FROM StateAreaCodes ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    modCmpSD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()), false);
                }
            }
            table = SQL.query("SELECT ID,Name FROM Messages where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    cmpAddMsg.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
            }
            cmpAddMsg.SelectedIndex = 0;
            table = SQL.query("SELECT ID,Name FROM Campaigns where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {
                
                foreach (DataRow r in table.Rows)
                {
                    cmpSelDD1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
                cmpSelDD1.SelectedIndex = 0;
            }
            table = SQL.query("SELECT ID,Name FROM Campaigns where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {
                    
                foreach (DataRow r in table.Rows)
                {
                    cmbDelbox1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
                cmbDelbox1.SelectedIndex = 0;
            }
            table = SQL.query("SELECT ID,Name FROM Campaigns where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {
              
                foreach (DataRow r in table.Rows)
                {
                    modCmpbox1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
                modCmpbox1.SelectedIndex = 0;
            }

            table = SQL.query("SELECT ID,Name FROM Schedule where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {
                
                foreach (DataRow r in table.Rows)
                {
                    SchedCmpBox1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
                SchedCmpBox1.SelectedIndex = 0;
            }
           /*
            SchedCmpStartDate.Format = DateTimePickerFormat.Custom;
            SchedCmpStartDate.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpStartDate2.Format = DateTimePickerFormat.Custom;
            SchedCmpStartDate2.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpStartDate3.Format = DateTimePickerFormat.Custom;
            SchedCmpStartDate3.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpStartDate4.Format = DateTimePickerFormat.Custom;
            SchedCmpStartDate4.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpStartDate5.Format = DateTimePickerFormat.Custom;
            SchedCmpStartDate5.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpStartDate6.Format = DateTimePickerFormat.Custom;
            SchedCmpStartDate6.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpStartDate7.Format = DateTimePickerFormat.Custom;
            SchedCmpStartDate7.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpEndDate.Format = DateTimePickerFormat.Custom;
            SchedCmpEndDate.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpEndDate2.Format = DateTimePickerFormat.Custom;
            SchedCmpEndDate2.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpEndDate3.Format = DateTimePickerFormat.Custom;
            SchedCmpEndDate3.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpEndDate4.Format = DateTimePickerFormat.Custom;
            SchedCmpEndDate4.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpEndDate5.Format = DateTimePickerFormat.Custom;
            SchedCmpEndDate5.CustomFormat = "MM/dd/yyyy hh:mm:ss";
            
            SchedCmpEndDate6.Format = DateTimePickerFormat.Custom;
            SchedCmpEndDate6.CustomFormat = "MM/dd/yyyy hh:mm:ss";

            SchedCmpEndDate7.Format = DateTimePickerFormat.Custom;
            SchedCmpEndDate7.CustomFormat = "MM/dd/yyyy hh:mm:ss";
           */

            DateTime startofweek = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
             SchedCmpStartDate.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate.CustomFormat = "hh:mm tt";
             SchedCmpStartDate.Text = startofweek.ToString();

             SchedCmpStartDate2.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate2.CustomFormat = "hh:mm tt";
             SchedCmpStartDate2.Text = startofweek.AddDays(1).ToString("MM/dd/yyyy hh:mm tt");

             SchedCmpStartDate3.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate3.CustomFormat = "hh:mm tt";
             SchedCmpStartDate3.Value = startofweek.AddDays(2);

             SchedCmpStartDate4.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate4.CustomFormat = "hh:mm tt";
             SchedCmpStartDate4.Value = startofweek.AddDays(3);

             SchedCmpStartDate5.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate5.CustomFormat = "hh:mm tt";
             SchedCmpStartDate5.Value = startofweek.AddDays(4);

             SchedCmpStartDate6.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate6.CustomFormat = "hh:mm tt";
             SchedCmpStartDate6.Value = startofweek.AddDays(5);

             SchedCmpStartDate7.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate7.CustomFormat = "hh:mm tt";
             SchedCmpStartDate7.Value = startofweek.AddDays(6);

             SchedCmpEndDate.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate.CustomFormat = "hh:mm tt";
             SchedCmpEndDate.Value = startofweek;

             SchedCmpEndDate2.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate2.CustomFormat = "hh:mm tt";
             SchedCmpEndDate2.Value = startofweek.AddDays(1);

             SchedCmpEndDate3.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate3.CustomFormat = "hh:mm tt";
             SchedCmpEndDate3.Value = startofweek.AddDays(2);

             SchedCmpEndDate4.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate4.CustomFormat = "hh:mm tt";
             SchedCmpEndDate4.Value = startofweek.AddDays(3);

             SchedCmpEndDate5.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate5.CustomFormat = "hh:mm tt";
             SchedCmpEndDate5.Value = startofweek.AddDays(4);

             SchedCmpEndDate6.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate6.CustomFormat = "hh:mm tt";
             SchedCmpEndDate6.Value = startofweek.AddDays(5);

             SchedCmpEndDate7.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate7.CustomFormat = "hh:mm tt";
             SchedCmpEndDate7.Value = startofweek.AddDays(6);


             SchedCmpStartDatea.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDatea.CustomFormat = "hh:mm tt";
             SchedCmpStartDatea.Value = startofweek;

             SchedCmpStartDate2a.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate2a.CustomFormat = "hh:mm tt";
             SchedCmpStartDate2a.Value = startofweek.AddDays(1);

             SchedCmpStartDate3a.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate3a.CustomFormat = "hh:mm tt";
             SchedCmpStartDate3a.Value = startofweek.AddDays(2);

             SchedCmpStartDate4a.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate4a.CustomFormat = "hh:mm tt";
             SchedCmpStartDate4a.Value = startofweek.AddDays(3);

             SchedCmpStartDate5a.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate5a.CustomFormat = "hh:mm tt";
             SchedCmpStartDate5a.Value = startofweek.AddDays(4);

             SchedCmpStartDate6a.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate6a.CustomFormat = "hh:mm tt";
             SchedCmpStartDate6a.Value = startofweek.AddDays(5);

             SchedCmpStartDate7a.Format = DateTimePickerFormat.Custom;
             SchedCmpStartDate7a.CustomFormat = "hh:mm tt";
             SchedCmpStartDate7a.Value = startofweek.AddDays(6);

             SchedCmpEndDatea.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDatea.CustomFormat = "hh:mm tt";
             SchedCmpEndDatea.Value = startofweek;

             SchedCmpEndDate2a.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate2a.CustomFormat = "hh:mm tt";
             SchedCmpEndDate2a.Value = startofweek.AddDays(1);

             SchedCmpEndDate3a.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate3a.CustomFormat = "hh:mm tt";
             SchedCmpEndDate3a.Value = startofweek.AddDays(2);

             SchedCmpEndDate4a.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate4a.CustomFormat = "hh:mm tt";
             SchedCmpEndDate4a.Value = startofweek.AddDays(3);

             SchedCmpEndDate5a.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate5a.CustomFormat = "hh:mm tt";
             SchedCmpEndDate5a.Value = startofweek.AddDays(4);

             SchedCmpEndDate6a.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate6a.CustomFormat = "hh:mm tt";
             SchedCmpEndDate6a.Value = startofweek.AddDays(5);

             SchedCmpEndDate7a.Format = DateTimePickerFormat.Custom;
             SchedCmpEndDate7a.CustomFormat = "hh:mm tt";
             SchedCmpEndDate7a.Value = startofweek.AddDays(6);
            

             SMSSQL sql = new SMSSQL();
             ROWS.Text = "1-20";
             DNTTextBox1.Text = "PHONE Number" + "\r\n";
             table = sql.query("Select TelephoneNumber FROM DoNotText where ID BETWEEN 3 AND 20 order by ID asc");
             
             foreach (DataRow r in table.Rows)
             {
                 DNTTextBox1.Text = DNTTextBox1.Text + r["TelephoneNumber"].ToString() + "\r\n";
             }


             
             table = SQL.query("SELECT ID,Name FROM Messages where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    msgDelDD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    msgModDD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                   
                   } 
                    msgModDD.SelectedIndex = 0;
                    msgDelDD.SelectedIndex = 0;
                }

            table = SQL.query("SELECT ID,Name FROM Schedule where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {
                
                foreach (DataRow r in table.Rows)
                {
                    cmpAddSch1.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    
                    
                }
                cmpAddSch1.SelectedIndex=0;
            }
            
            
        }






        private void updateImportTabs()
        {
            LDBSQL SQL = new LDBSQL();
            DataTable table = new DataTable();
            table = SQL.query("SELECT ID,Name FROM refLS ORDER BY ID");
            if (table.TableName != "Error")
            {
                impMapSource.Items.Clear();
                
                foreach (DataRow r in table.Rows)
                {
                    impMapSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
               
            }
        }
        private void updateSourceTabs()
        {
            ListItem iNone = new ListItem("-1", "None");
            LDBSQL SQL = new LDBSQL();
            DataTable table = new DataTable();
            table = SQL.query("SELECT ID,Name FROM refLS ORDER BY ID");
            if (table.TableName != "Error")
            {
                //impMapSource.Items.Clear();
                manDelSourceSel.Items.Clear();
                viewLeads.Items.Clear();
                ImportHistory.Items.Clear();
                viewLeads.Items.Add(iNone);
                foreach (DataRow r in table.Rows)
                {
                 //   manModSourceSel.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    manDelSourceSel.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    
         //           impMapSource.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }
               
            }

            table = SQL.query("SELECT  ID,FileName  FROM Imported where DateImported in (SELECT MAX(DateImported) FROM  Imported GROUP BY FileName) ORDER BY ID");
            if (table.TableName != "Error")
            {

                foreach (DataRow r in table.Rows)
                {


                    viewLeads.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }


                viewLeads.SelectedIndex = 0;
            }
            viewLeads.SelectedIndex = 0;


            viewLeads.SelectedIndex = 0;

            table = SQL.query("SELECT  ID,FileName  FROM Imported where DateImported in (SELECT MAX(DateImported) FROM  Imported GROUP BY FileName) ORDER BY ID");
            if (table.TableName != "Error")
            {

                foreach (DataRow r in table.Rows)
                {


                    ImportHistory.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }


                ImportHistory.SelectedIndex = 0;
            }
            ImportHistory.SelectedIndex = 0;


        }

     

        private void cmpDel1_Click(object sender, EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.cmpDel));
            this.Thread.Start();
           
        }



        public void setSchedulesettings(String Start_time, String Start_date, String End_time, String End_date, String Start_time2, String Start_date2, String End_time2, String End_date2, String Start_time3, String Start_date3, String End_time3, String End_date3, String Start_time4, String Start_date4, String End_time4, String End_date4, String Start_time5, String Start_date5, String End_time5, String End_date5, String Start_time6, String Start_date6, String End_time6, String End_date6, String Start_time7, String Start_date7, String End_time7, String End_date7, int daily,int Enable1,int Enable2,int Enable3,int Enable4,int Enable5,int Enable6,int Enable7)
        {
            if ((this.SchedCmpStartDate.InvokeRequired) && (this.SchedCmpEndDate.InvokeRequired))
            {
                this.Invoke(new MethodInvoker(delegate() {
                  /*  string[] Char_String_Start_date = Start_date.Split('/');
                    string[] Char_String_Start_date2 = Start_date2.Split('/');
                    string[] Char_String_Start_date3 = Start_date3.Split('/');
                    string[] Char_String_Start_date4 = Start_date4.Split('/');
                    string[] Char_String_Start_date5 = Start_date5.Split('/');
                    string[] Char_String_Start_date6 = Start_date6.Split('/');
                    string[] Char_String_Start_date7 = Start_date7.Split('/');
                    string[] Char_String_End_date = End_date.Split('/');
                    string[] Char_String_End_date2 = End_date2.Split('/');
                    string[] Char_String_End_date3 = End_date3.Split('/');
                    string[] Char_String_End_date4 = End_date4.Split('/');
                    string[] Char_String_End_date5 = End_date5.Split('/');
                    string[] Char_String_End_date6 = End_date6.Split('/');
                    string[] Char_String_End_date7 = End_date7.Split('/');
                    foreach (string dates in Char_String_Start_date)
                    {
              
                        WriteToConsole(dates, 1);
                    }


                    char[] delimiters = new char[] { ':', ' ' };
                    string[] Char_String_Start_time = Start_time.Split(delimiters);
                    string[] Char_String_Start_time2 = Start_time2.Split(delimiters);
                    string[] Char_String_Start_time3 = Start_time3.Split(delimiters);
                    string[] Char_String_Start_time4 = Start_time4.Split(delimiters);
                    string[] Char_String_Start_time5 = Start_time5.Split(delimiters);
                    string[] Char_String_Start_time6 = Start_time6.Split(delimiters);
                    string[] Char_String_Start_time7 = Start_time7.Split(delimiters);
                    string[] Char_String_End_time = End_time.Split(delimiters);
                    string[] Char_String_End_time2 = End_time2.Split(delimiters);
                    string[] Char_String_End_time3 = End_time3.Split(delimiters);
                    string[] Char_String_End_time4 = End_time4.Split(delimiters);
                    string[] Char_String_End_time5 = End_time5.Split(delimiters);
                    string[] Char_String_End_time6 = End_time6.Split(delimiters);
                    string[] Char_String_End_time7 = End_time7.Split(delimiters);



                    //Start Dates
                    DateTime SchedCmpStartDateTime = new DateTime(Int32.Parse(Char_String_Start_date[2]), Int32.Parse(Char_String_Start_date[0]), Int32.Parse(Char_String_Start_date[1]), Int32.Parse(Char_String_Start_time[0]), Int32.Parse(Char_String_Start_time[1]), Int32.Parse(Char_String_Start_time[2]));
                    SchedCmpStartDate.Text = SchedCmpStartDateTime.ToString();


                    DateTime SchedCmpStartDateTime2 = new DateTime(Int32.Parse(Char_String_Start_date2[2]), Int32.Parse(Char_String_Start_date2[0]), Int32.Parse(Char_String_Start_date2[1]), Int32.Parse(Char_String_Start_time2[0]), Int32.Parse(Char_String_Start_time2[1]), Int32.Parse(Char_String_Start_time2[2]));
                    SchedCmpStartDate2.Text = SchedCmpStartDateTime2.ToString();

                    DateTime SchedCmpStartDateTime3 = new DateTime(Int32.Parse(Char_String_Start_date3[2]), Int32.Parse(Char_String_Start_date3[0]), Int32.Parse(Char_String_Start_date3[1]), Int32.Parse(Char_String_Start_time3[0]), Int32.Parse(Char_String_Start_time2[1]), Int32.Parse(Char_String_Start_time3[2]));
                    SchedCmpStartDate3.Text = SchedCmpStartDateTime3.ToString();

                    DateTime SchedCmpStartDateTime4 = new DateTime(Int32.Parse(Char_String_Start_date4[2]), Int32.Parse(Char_String_Start_date4[0]), Int32.Parse(Char_String_Start_date4[1]), Int32.Parse(Char_String_Start_time4[0]), Int32.Parse(Char_String_Start_time4[1]), Int32.Parse(Char_String_Start_time4[2]));
                    SchedCmpStartDate4.Text = SchedCmpStartDateTime4.ToString();

                    DateTime SchedCmpStartDateTime5 = new DateTime(Int32.Parse(Char_String_Start_date5[2]), Int32.Parse(Char_String_Start_date5[0]), Int32.Parse(Char_String_Start_date5[1]), Int32.Parse(Char_String_Start_time5[0]), Int32.Parse(Char_String_Start_time5[1]), Int32.Parse(Char_String_Start_time5[2]));
                    SchedCmpStartDate5.Text = SchedCmpStartDateTime5.ToString();

                    DateTime SchedCmpStartDateTime6 = new DateTime(Int32.Parse(Char_String_Start_date6[2]), Int32.Parse(Char_String_Start_date6[0]), Int32.Parse(Char_String_Start_date6[1]), Int32.Parse(Char_String_Start_time6[0]), Int32.Parse(Char_String_Start_time6[1]), Int32.Parse(Char_String_Start_time6[2]));
                    SchedCmpStartDate6.Text = SchedCmpStartDateTime6.ToString();

                    DateTime SchedCmpStartDateTime7 = new DateTime(Int32.Parse(Char_String_Start_date7[2]), Int32.Parse(Char_String_Start_date7[0]), Int32.Parse(Char_String_Start_date7[1]), Int32.Parse(Char_String_Start_time7[0]), Int32.Parse(Char_String_Start_time7[1]), Int32.Parse(Char_String_Start_time7[2]));
                    SchedCmpStartDate7.Text = SchedCmpStartDateTime7.ToString();
                    
                    //End Dates
                    
                    DateTime SchedCmpEndDateTime = new DateTime(Int32.Parse(Char_String_End_date[2]), Int32.Parse(Char_String_End_date[0]), Int32.Parse(Char_String_End_date[1]), Int32.Parse(Char_String_End_time[0]), Int32.Parse(Char_String_End_time[1]), Int32.Parse(Char_String_End_time[2]));
                   SchedCmpEndDate.Text = SchedCmpEndDateTime.ToString();

                   DateTime SchedCmpEndDateTime2 = new DateTime(Int32.Parse(Char_String_End_date2[2]), Int32.Parse(Char_String_End_date2[0]), Int32.Parse(Char_String_End_date2[1]), Int32.Parse(Char_String_End_time2[0]), Int32.Parse(Char_String_End_time2[1]), Int32.Parse(Char_String_End_time2[2]));
                   SchedCmpEndDate2.Text = SchedCmpEndDateTime2.ToString();

                   DateTime SchedCmpEndDateTime3 = new DateTime(Int32.Parse(Char_String_End_date3[2]), Int32.Parse(Char_String_End_date3[0]), Int32.Parse(Char_String_End_date3[1]), Int32.Parse(Char_String_End_time3[0]), Int32.Parse(Char_String_End_time3[1]), Int32.Parse(Char_String_End_time3[2]));
                   SchedCmpEndDate3.Text = SchedCmpEndDateTime3.ToString();

                   DateTime SchedCmpEndDateTime4 = new DateTime(Int32.Parse(Char_String_End_date4[2]), Int32.Parse(Char_String_End_date4[0]), Int32.Parse(Char_String_End_date4[1]), Int32.Parse(Char_String_End_time4[0]), Int32.Parse(Char_String_End_time4[1]), Int32.Parse(Char_String_End_time4[2]));
                   SchedCmpEndDate4.Text = SchedCmpEndDateTime4.ToString();

                   DateTime SchedCmpEndDateTime5 = new DateTime(Int32.Parse(Char_String_End_date5[2]), Int32.Parse(Char_String_End_date5[0]), Int32.Parse(Char_String_End_date5[1]), Int32.Parse(Char_String_End_time5[0]), Int32.Parse(Char_String_End_time5[1]), Int32.Parse(Char_String_End_time5[2]));
                   SchedCmpEndDate5.Text = SchedCmpEndDateTime5.ToString();

                   DateTime SchedCmpEndDateTime6 = new DateTime(Int32.Parse(Char_String_End_date6[2]), Int32.Parse(Char_String_End_date6[0]), Int32.Parse(Char_String_End_date5[1]), Int32.Parse(Char_String_End_time6[0]), Int32.Parse(Char_String_End_time6[1]), Int32.Parse(Char_String_End_time6[2]));
                   SchedCmpEndDate6.Text = SchedCmpEndDateTime6.ToString();

                   DateTime SchedCmpEndDateTime7 = new DateTime(Int32.Parse(Char_String_End_date7[2]), Int32.Parse(Char_String_End_date7[0]), Int32.Parse(Char_String_End_date7[1]), Int32.Parse(Char_String_End_time7[0]), Int32.Parse(Char_String_End_time7[1]), Int32.Parse(Char_String_End_time7[2]));
                   SchedCmpEndDate7.Text = SchedCmpEndDateTime7.ToString();*/

                    DateTime SchedCmpStartDateTimea = new DateTime();
                    SchedCmpStartDateTimea=Convert.ToDateTime(Start_date+" "+ Start_time);
                    SchedCmpStartDatea.Text = SchedCmpStartDateTimea.ToString();


                    DateTime SchedCmpStartDateTime2a = Convert.ToDateTime(Start_date2 + " " + Start_time2);
                    SchedCmpStartDate2a.Text = SchedCmpStartDateTime2a.ToString();

                    DateTime SchedCmpStartDateTime3a = Convert.ToDateTime(Start_date3 + " " + Start_time3);
                    SchedCmpStartDate3a.Text = SchedCmpStartDateTime3a.ToString();

                    DateTime SchedCmpStartDateTime4a = Convert.ToDateTime(Start_date4 + " " + Start_time4);
                    SchedCmpStartDate4a.Text = SchedCmpStartDateTime4a.ToString();

                    DateTime SchedCmpStartDateTime5a = Convert.ToDateTime(Start_date5 + " " + Start_time5);
                    SchedCmpStartDate5a.Text = SchedCmpStartDateTime5a.ToString();

                    DateTime SchedCmpStartDateTime6a = Convert.ToDateTime(Start_date6 + " " + Start_time6);
                    SchedCmpStartDate6a.Text = SchedCmpStartDateTime6a.ToString();

                    DateTime SchedCmpStartDateTime7a = Convert.ToDateTime(Start_date7 + " " + Start_time7);
                    SchedCmpStartDate7a.Text = SchedCmpStartDateTime7a.ToString();

                    //End Dates

                    DateTime SchedCmpEndDateTimea = Convert.ToDateTime(End_date + " " + End_time);
                    SchedCmpEndDatea.Text = SchedCmpEndDateTimea.ToString();

                    DateTime SchedCmpEndDateTime2a = Convert.ToDateTime(End_date2 + " " + End_time2);
                    SchedCmpEndDate2a.Text = SchedCmpEndDateTime2a.ToString();

                    DateTime SchedCmpEndDateTime3a = Convert.ToDateTime(End_date3 + " " + End_time3);
                    SchedCmpEndDate3a.Text = SchedCmpEndDateTime3a.ToString();

                    DateTime SchedCmpEndDateTime4a = Convert.ToDateTime(End_date4 + " " + End_time4);
                    SchedCmpEndDate4a.Text = SchedCmpEndDateTime4a.ToString();

                    DateTime SchedCmpEndDateTime5a = Convert.ToDateTime(End_date5 + " " + End_time5);
                    SchedCmpEndDate5a.Text = SchedCmpEndDateTime5a.ToString();

                    DateTime SchedCmpEndDateTime6a = Convert.ToDateTime(End_date6 + " " + End_time6);
                    SchedCmpEndDate6a.Text = SchedCmpEndDateTime6a.ToString();

                    DateTime SchedCmpEndDateTime7a = Convert.ToDateTime(End_date7 + " " + End_time7);
                    SchedCmpEndDate7a.Text = SchedCmpEndDateTime7a.ToString();

                  repeat1a.Checked = daily == 1 ? true : false;
                  checkBox1a.Checked = Enable1 == 1 ? true : false;
                  checkBox2a.Checked = Enable2 == 1 ? true : false;
                  checkBox3a.Checked = Enable3 == 1 ? true : false;
                  checkBox4a.Checked = Enable4 == 1 ? true : false;
                  checkBox5a.Checked = Enable5 == 1 ? true : false;
                  checkBox6a.Checked = Enable6 == 1 ? true : false;
                  checkBox7a.Checked = Enable7 == 1 ? true : false;
                }));
            }
            else
            {
            }

        }

     

        private void cmpSched1_Click(object sender, EventArgs e)
        {
           
            
        }

        private void SaveSched1_Click(object sender, EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.cmpSchedcreate));
            this.Thread.Start();
            this.myUpdateCmpTabs();
       
        }

       
        private void impWB2_Click(object sender, System.EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.IPimport));
            this.Thread.Start();
        }

        private void SchedCmpBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }



        private void checkBox1_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBox1.Checked)
            {
                SchedCmpStartDate.Enabled = false;
                SchedCmpEndDate.Enabled = false;
            }
            else
            {
                SchedCmpStartDate.Enabled = true;
                SchedCmpEndDate.Enabled = true;
            }
        }

       private void checkBox2_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBox2.Checked)
            {
                SchedCmpStartDate2.Enabled = false;
                SchedCmpEndDate2.Enabled = false;
            }
            else
            {
                SchedCmpStartDate2.Enabled = true;
                SchedCmpEndDate2.Enabled = true;
            }
        }

       private void checkBox3_CheckedChanged(object sender, System.EventArgs e)
       {
           if (checkBox3.Checked)
           {
               SchedCmpStartDate3.Enabled = false;
               SchedCmpEndDate3.Enabled = false;
           }
           else
           {
               SchedCmpStartDate3.Enabled = true;
               SchedCmpEndDate3.Enabled = true;
           }
       }

       private void checkBox4_CheckedChanged(object sender, System.EventArgs e)
       {
           if (checkBox4.Checked)
           {
               SchedCmpStartDate4.Enabled = false;
               SchedCmpEndDate4.Enabled = false;
           }
           else
           {
               SchedCmpStartDate4.Enabled = true;
               SchedCmpEndDate4.Enabled = true;
           }
       }

       private void checkBox5_CheckedChanged(object sender, System.EventArgs e)
       {
           if (checkBox5.Checked)
           {
               SchedCmpStartDate5.Enabled = false;
               SchedCmpEndDate5.Enabled = false;
           }
           else
           {
               SchedCmpStartDate5.Enabled = true;
               SchedCmpEndDate5.Enabled = true;
           }
       }

       private void checkBox6_CheckedChanged(object sender, System.EventArgs e)
       {
           if (checkBox6.Checked)
           {
               SchedCmpStartDate6.Enabled = false;
               SchedCmpEndDate6.Enabled = false;
           }
           else
           {
               SchedCmpStartDate6.Enabled = true;
               SchedCmpEndDate6.Enabled = true;
           }
       }

       private void checkBox7_CheckedChanged(object sender, System.EventArgs e)
       {
           if (checkBox7.Checked)
           {
               SchedCmpStartDate7.Enabled = false;
               SchedCmpEndDate7.Enabled = false;
           }
           else
           {
               SchedCmpStartDate7.Enabled = true;
               SchedCmpEndDate7.Enabled = true;
           }
       }


       public string getimpOrigTotal()
       {
           return impOrigTotal.Text;
       }

       public string pGetimpOrigTotal()
       {
           if (this.impOrigTotal.InvokeRequired)
           {
               stringDelegate d = new stringDelegate(getimpOrigTotal);
               return (string)this.Invoke(d);


           }
           else
           {
               return this.getimpOrigTotal();
           }
       }


       public void setimpOrigTotal()
       {
           impOrigTotal.Text = (Convert.ToInt32(impOrigTotal.Text)+1).ToString();
       }

       public void pSetimpOrigTotal()
       {
           if (this.impOrigTotal.InvokeRequired)
           {
               this.Invoke(new MethodInvoker(delegate() { setimpOrigTotal(); }));


           }
           else
           {
               this.setimpOrigTotal();
           }
       }


       public string getimpMalformed()
       {
           return impMalformed.Text;
       }

       public string pGetimpMalformed()
       {
           if (this.impMalformed.InvokeRequired)
           {

               stringDelegate d = new stringDelegate(getimpMalformed);
               return (string)this.Invoke(d);

           }
           else
           {
               return this.getimpMalformed();
           }
       }



       public void setimpMalformed()
       {
           impMalformed.Text = (Convert.ToInt32(impMalformed.Text)+1).ToString();
       }

       public void pSetimpMalformed()
       {
           if (this.impMalformed.InvokeRequired)
           {
               this.Invoke(new MethodInvoker(delegate() { setimpMalformed(); }));


           }
           else
           {
               this.setimpMalformed();
           }
       }

       public string getimpInvalid()
       {
           return impInvalid.Text;
       }

       public string pGetimpInvalid()
       {
           if (this.impInvalid.InvokeRequired)
           {
               stringDelegate d = new stringDelegate(getimpInvalid);
               return (string)this.Invoke(d);


           }
           else
           {
               return this.getimpInvalid();
           }
       }


       public void setimpInvalid()
       {
           impInvalid.Text = ((Convert.ToInt32(impInvalid.Text)+1)).ToString();
       }

       public void pSetimpInvalid()
       {
           if (this.impInvalid.InvokeRequired)
           {
               this.Invoke(new MethodInvoker(delegate() { setimpInvalid(); }));


           }
           else
           {
               this.setimpInvalid();
           }
       }

       public string getimpAddTotal()
       {
           return  impAddTotal.Text;
       }

       public string pGetimpAddTotal()
       {
           if (this.impAddTotal.InvokeRequired)
           {
               stringDelegate d = new stringDelegate(getimpAddTotal);
               return (string)this.Invoke(d);


           }
           else
           {
               return this.getimpAddTotal();
           }
       }


       public void setimpAddTotal()
       {
           impAddTotal.Text = ((Convert.ToInt32(impAddTotal.Text) + 1)).ToString();
       }

       public void pSetimpAddTotal()
       {
           if (this.impAddTotal.InvokeRequired)
           {
               this.Invoke(new MethodInvoker(delegate() { setimpAddTotal(); }));


           }
           else
           {
               this.setimpAddTotal();
           }
       }

       public string getimpDNT()
       {
           return impDNT.Text;
       }

       public string pGetimpDNT()
       {
           if (this.impDNT.InvokeRequired)
           {
               stringDelegate d = new stringDelegate(getimpDNT);
               return (string)this.Invoke(d);


           }
           else
           {
               return this.getimpDNT();
           }
       }


       public void setimpDNT()
       {
           impDNT.Text = ((Convert.ToInt32(impDNT.Text)+1)).ToString();
       }

       public void pSetimpDNT()
       {
           if (this.impDNT.InvokeRequired)
           {
               this.Invoke(new MethodInvoker(delegate() { setimpDNT(); }));


           }
           else
           {
               this.setimpDNT();
           }
       }


       public string getimpDeactive()
       {
           return impDeactive.Text;
       }

       public string pGetimpDeactive()
       {
           if (this.impDeactive.InvokeRequired)
           {
               stringDelegate d = new stringDelegate(getimpDeactive);
               return (string)this.Invoke(d);


           }
           else
           {
               return this.getimpDeactive();
           }
       }


       public void setimpDeactive()
       {
           impDeactive.Text = ((Convert.ToInt32(impDeactive.Text) + 1)).ToString();
       }

       public void pSetimpDeactive()
       {
           if (this.impDeactive.InvokeRequired)
           {
               this.Invoke(new MethodInvoker(delegate() { setimpDeactive(); }));


           }
           else
           {
               this.setimpDeactive();
           }
       }

       public string getimpPrevious()
       {
           return impPrevious.Text;
       }

       public string pGetimpPrevious()
       {
           if (this.impPrevious.InvokeRequired)
           {
               stringDelegate d = new stringDelegate(getimpDeactive);
               return (string)this.Invoke(d);


           }
           else
           {
               return this.getimpPrevious();
           }
       }



       public void setimpPrevious()
       {
           impPrevious.Text = ((Convert.ToInt32(impPrevious.Text)+1)).ToString();
       }

       public void pSetimpPrevious()
       {
           if (this.impPrevious.InvokeRequired)
           {
               this.Invoke(new MethodInvoker(delegate() { setimpPrevious(); }));


           }
           else
           {
               this.setimpPrevious();
           }
       }

       public string getimpBCarr()
       {
           return impBCarr.Text;
       }

       public string pGetimpBCarr()
       {
           if (this.impBCarr.InvokeRequired)
           {
               stringDelegate d = new stringDelegate(getimpBCarr);
               return (string)this.Invoke(d);


           }
           else
           {
               return this.getimpBCarr();
           }
       }


       public void setimpBCarr()
       {
           impBCarr.Text = ((Convert.ToInt32(impBCarr.Text)+1)).ToString();
       }

       public void pSetimpBCarr()
       {
           if (this.impBCarr.InvokeRequired)
           {
               this.Invoke(new MethodInvoker(delegate() { setimpBCarr(); }));


           }
           else
           {
               this.setimpBCarr();
           }
       }


       public string getimpNCarr()
       {
           return impNCarr.Text;
       }


       public string pGetimpNCarr()
       {
           if (this.impNCarr.InvokeRequired)
           {
               stringDelegate d = new stringDelegate(getimpNCarr);
               return (string)this.Invoke(d);


           }
           else
           {
               return this.getimpNCarr();
           }
       }


       public void setimpNCarr()
       {
           impNCarr.Text = ((Convert.ToInt32(impNCarr.Text)+1)).ToString();
       }


       public void pSetimpNCarr()
       {
           if (this.impNCarr.InvokeRequired)
           {
               this.Invoke(new MethodInvoker(delegate() { setimpNCarr(); }));


           }
           else
           {
               this.setimpNCarr();
           }
       }

       private void trackBar1_Scroll(object sender, System.EventArgs e)
       {
           LDBSQL SQL = new LDBSQL();
           RateBox2.Text = trackBar1.Value.ToString();
           string sql = "UPDATE Campaigns SET Rate="+ trackBar1.Value + " where ID=" + this.pGetcmpSelDD1ID();
           SQL.edit(sql);
          
           
       }

       private void button3_Click(object sender, System.EventArgs e)
       {
           this.Thread = new Thread(new ThreadStart(this.Force_cmp_abort));
           this.Thread.Start();
       }

       private void Force_campaign_start_Click(object sender, System.EventArgs e)
       {
           this.Thread = new Thread(new ThreadStart(this.Force_cmp_start));
           this.Thread.Start();
       }

        private void Force_campaign_abort_Click(object sender, System.EventArgs e)
       {
           this.Thread = new Thread(new ThreadStart(this.Force_cmp_abort));
           this.Thread.Start();
       }

        private void nxt_DNT_row_Click(object sender, System.EventArgs e)
        {
            string rows = ROWS.Text;
            long MAX_ROWS = 0;
            char[] delimiters = { '-' };
            string[] parts = rows.Split(delimiters);
            int MIN = Convert.ToInt32(parts[0]);
            int MAX = Convert.ToInt32(parts[1]);
            SMSSQL sql = new SMSSQL();
            DataTable table = new DataTable();
            string query1 = "Select COUNT(*) AS MAXROWS FROM DoNotText";
            table = sql.query(query1);
            foreach (DataRow r in table.Rows)
            {
                MAX_ROWS = Convert.ToInt64(r["MAXROWS"].ToString());
            }
            if(MIN+20 < MAX_ROWS){
               MIN = MIN + 20;
               MAX = MAX + 20;
               string query = "Select TelephoneNumber FROM DoNotText where ID BETWEEN " + MIN + " AND " + MAX + " order by ID asc";
               table = sql.query(query);
               DNTTextBox1.Text = "PHONE Number" + "\r\n";
               foreach (DataRow r in table.Rows)
               {
                   DNTTextBox1.Text = DNTTextBox1.Text + r["TelephoneNumber"].ToString() + "\r\n";
               }
               ROWS.Text = MIN + "-" + MAX;
            }
        }

        private void dec_DNT_row_Click(object sender, System.EventArgs e)
        {
            string rows = ROWS.Text;
            char[] delimiters = { '-' };
            string[] parts = rows.Split(delimiters);
            int MIN = Convert.ToInt32(parts[0]);
            int MAX = Convert.ToInt32(parts[1]);
            SMSSQL sql = new SMSSQL();
            DataTable table = new DataTable();
            if (MIN -20 >= 0)
            {
                MIN = MIN - 20;
                MAX = MAX - 20;
                string query = "Select TelephoneNumber FROM DoNotText where ID BETWEEN " + MIN + " AND " + MAX + " order by ID asc";
                table = sql.query(query);
                DNTTextBox1.Text = "Phone Number" + "\r\n";
                foreach (DataRow r in table.Rows)
                {
                    DNTTextBox1.Text = DNTTextBox1.Text + r["TelephoneNumber"].ToString() + "\r\n";
                }
                ROWS.Text = MIN + "-" + MAX;
            }
        }

        private void FRDNC_Click(object sender, System.EventArgs e)
        {
            SMSSQL sql = new SMSSQL();
                DataTable table = new DataTable();
                string query = "Select TelephoneNumber FROM DoNotText where ID BETWEEN 0 AND 20 order by ID asc";
                table = sql.query(query);
                DNTTextBox1.Text = "Phone Number" + "\r\n";
                foreach (DataRow r in table.Rows)
                {
                    DNTTextBox1.Text = DNTTextBox1.Text + r["TelephoneNumber"].ToString() + "\r\n";
                }
                ROWS.Text = "0-20";
        }

        private void FFDNC_Click(object sender, System.EventArgs e)
        {
            string rows = ROWS.Text;
            long MAX_ROWS = 0;

            int MIN = 0;
            long MAX = 100;
            SMSSQL sql = new SMSSQL();
            DataTable table = new DataTable();
            string query1 = "Select COUNT(*) AS MAXROWS FROM DoNotText";
            table = sql.query(query1);
            foreach (DataRow r in table.Rows)
            {
                MAX_ROWS = Convert.ToInt64(r["MAXROWS"].ToString());
            }
            MAX = MAX_ROWS;
            MIN = (int)MAX_ROWS - 20;
            string query = "Select TelephoneNumber FROM DoNotText where ID BETWEEN " + MIN + " AND " + MAX + "  order by ID asc";
            table = sql.query(query);
            DNTTextBox1.Text = "Phone Number" + "\r\n";
            foreach (DataRow r in table.Rows)
            {
                DNTTextBox1.Text = DNTTextBox1.Text + r["TelephoneNumber"].ToString() + "\r\n";
            }
            ROWS.Text = MIN + "-" + MAX;
            
        }

        private void msgAddB1_Click(object sender, System.EventArgs e)
        {
            LDBSQL ldb = new LDBSQL();
            string name = addMessageTxt.Text;
            string msg = msgAddText.Text;
            msg = Regex.Replace(msg, @"\'", @"''");
            string query="INSERT INTO Messages (Name,Type,Message,Inactive) VALUES ('"+name+"',"+1+",'"+msg+"',"+  0+ ")";
            ldb.edit(query);
            msgModDD.Items.Clear();
            msgDelDD.Items.Clear();
            cmpAddMsg.Items.Clear();
            modMessages.Items.Clear();
            cmpAddMsg.Items.Clear();
            DataTable table = ldb.query("SELECT ID,Name from Messages where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {

                foreach (DataRow r in table.Rows)
                {
                    msgModDD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    msgDelDD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    cmpAddMsg.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    modMessages.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    cmpAddMsg.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }

            }
            msgModDD.SelectedIndex = 1;
            msgDelDD.SelectedIndex = 1;
            cmpAddMsg.SelectedIndex = 1;
            modMessages.SelectedIndex = 1;
            cmpAddMsg.SelectedIndex = 1;
        }

        private void SchedCmpStartDate_ValueChanged(object sender, System.EventArgs e)
        {

        }

        private void msgDelDD_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void msgModB1_Click(object sender, System.EventArgs e)
        {
            LDBSQL SQL = new LDBSQL();
            
            string msg = msgModText.Text;
            msg = Regex.Replace(msg, @"\'", @"''");
            string query = "UPDATE Messages SET Message='"+ msg + "' where ID=" + this.getmsgModDD().ID;
            SQL.edit(query);
        }

        private void msgDelB1_Click(object sender, System.EventArgs e)
        {
            LDBSQL SQL = new LDBSQL();
            string query = "UPDATE Messages SET Inactive= 1 where ID=" + this.getmsgDelDD().ID;
            SQL.edit(query);
            msgModDD.Items.Clear();
            msgDelDD.Items.Clear();
            cmpAddMsg.Items.Clear();
            modMessages.Items.Clear();
            cmpAddMsg.Items.Clear();
            DataTable table = SQL.query("SELECT ID,Name from Messages where Inactive=0 ORDER BY ID");
            if (table.TableName != "Error")
            {

                foreach (DataRow r in table.Rows)
                {
                    msgModDD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    msgDelDD.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    cmpAddMsg.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    modMessages.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                    cmpAddMsg.Items.Add(new ListItem(r[0].ToString(), r[1].ToString()));
                }

            }
            msgModDD.SelectedIndex = 1;
            msgDelDD.SelectedIndex = 1;
            cmpAddMsg.SelectedIndex = 1;
            modMessages.SelectedIndex = 1;
            cmpAddMsg.SelectedIndex = 1;
        }


        private void dateTimePicker1a_ValueChanged(object sender, System.EventArgs e)
        {

        }

        private void cmpSched1_Click_1(object sender, System.EventArgs e)
        {
          
              this.Thread = new Thread(new ThreadStart(this.cmpSchedSelect));
              this.Thread.Start();
    
        }

        private void SaveSched1_Click_1(object sender, System.EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.cmpSchedSave));
            this.Thread.Start();
        }

        private void SaveModcmp_Click(object sender, System.EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.cmpMod));
            this.Thread.Start();
        }

        private void LoadcmpMod_Click(object sender, System.EventArgs e)
        {
            
              LDBSQL SQL= new LDBSQL();
              string Message="";
              string Schedule="";
              string source="0";
              bool has32 = false; 

              string str = "SELECT StateDeletes,Sources,Message,Schedule FROM Campaigns WHERE ID=" + ((ListItem)this.modCmpbox1.SelectedItem).ID;
              DataTable table=SQL.query(str);
            foreach (DataRow r in table.Rows)
            {
                str=r["StateDeletes"].ToString();
                source = r["Sources"].ToString();
                Message=r["Message"].ToString();
                Schedule=r["Schedule"].ToString();
                
            }
            char[] Delimiters ={','};
            string[] states = str.Split(Delimiters);
            ListItem item;
            if (states[0] != "")
            {
                foreach (string s in states)
                {
                    for (int i = 0; i < this.modCmpSD.Items.Count; i++)
                    {
                        item = (ListItem)this.modCmpSD.Items[i];
                        if (item.ID == s)
                        {
                            this.modCmpSD.SetItemChecked(i, true);
                        }
                    }

                }
            }
            else
            {
                foreach (int i in this.modCmpSD.CheckedIndices)
                {
                    this.modCmpSD.SetItemChecked(i, true);
                }
            }

            
            string[] Leads = source.Split(Delimiters);
        
            if (Leads[0] != "")
            {
                foreach (string s in Leads)
                {
                    //this.modCmpSource.SetItemChecked(Convert.ToInt32(s), true);
                    for (int i=0; i < this.modCmpSource.Items.Count; i++ )
                    {
                        item = (ListItem)this.modCmpSource.Items[i];
                        if (item.ID == s)
                        {
                            this.modCmpSource.SetItemChecked(i, true);
                        }
                    }
                }
            }
            else
            {
                foreach (string s in Leads)
                {

                    //this.modCmpSD.SetItemChecked(i, false);
                    for (int i = 0; i < this.modCmpSource.Items.Count; i++)
                    {
                        item = (ListItem)this.modCmpSource.Items[i];
                        if (item.ID == s)
                        {
                            this.modCmpSource.SetItemChecked(i, false);
                        }
                    }
                }
            }
            
            Int32 ID = Convert.ToInt32(Message);
            for (int i = 0; i < this.modMessages.Items.Count; i++)
            {
                  
                   item=(ListItem)modMessages.Items[i];
                   
                if(item.ID == Message){
                    modMessages.SelectedIndex = i;
                }
            }
               
            for (int i = 0; i < this.modSchedule.Items.Count; i++)
            {
            
                item = (ListItem)modSchedule.Items[i];

                if (item.ID == Schedule)
                {
                    modSchedule.SelectedIndex = i;
                }
            }
            //modMessages.SelectedIndex = Convert.ToInt32(Message);
           //modSchedule.SelectedIndex = Convert.ToInt32(Schedule);
            char[] Delimiters2 = { ',' };
            string[] Leads_arr = source.Split(Delimiters);
            if (Leads_arr.Contains("32"))
            {
                source = "";
                for (int i = 0; i < Leads_arr.Length; i++)
                {
                    if (Leads_arr[i] != "32")
                    {
                        source = source + Leads_arr[i] + ",";
                    }
                    else
                    {
                        has32 = true;
                    }
                }
                if ((source.Length > 0) && source[source.Length - 1] == ',')
                {
                    source = source.Substring(0, source.Length - 1);
                }

            }
            str = "SELECT * FROM Leads inner join Imported on Leads.FileID=Imported.ID where CampId = 0 and LSID IN (" + source + ")";
             table = SQL.query(str);
             int total = 0;
            total = table.Rows.Count;
            if(has32){
                str="select * from Leads_Bad_Carriers where CampId=0";
                table = SQL.query(str);
                total=total+table.Rows.Count;
            }
            modLeads.Text = total.ToString();

           
        }

        private void label56_Click(object sender, System.EventArgs e)
        {

        }

        private void modCmpSource_SelectedIndexChanged(object sender, System.EventArgs e)
        {




            string IDs = "";
            ListItem item;
            foreach (int i in this.modCmpSource.CheckedIndices)
                {
                    item = (ListItem)this.modCmpSource.Items[i];
                    IDs += '\'';
                    IDs += item.ID;
                    IDs += '\'';
                    IDs += ',';
                }
                if ((IDs.Length > 0) && IDs[IDs.Length-1]!='\'')
                {
                    IDs = IDs.Substring(0, IDs.Length - 1);
                }
            //item = (ListItem)this.modCmpSource.Items[this.modCmpSource.SelectedIndex];
            //IDs = item.ID;
            LDBSQL SQL = new LDBSQL();
            string str = "SELECT * FROM LEADS where FileID IN (" + IDs + ")";
            DataTable table = SQL.query("SELECT * FROM LEADS where FileID IN (" + IDs + ")");
            modLeads.Text = (table.Rows.Count).ToString();
        }

        private void impIWstartB_Click(object sender, System.EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.IPimport));
            this.Thread.Start();
        }

        private void checkBox7a_CheckedChanged(object sender, System.EventArgs e)
        {


        }

        public void updateGrid()
        {
            LDBSQL SQL = new LDBSQL();
            DataTable table = new DataTable();
            table = SQL.query("  SELECT Campaigns.Name, Message_Sent.Numsent, Rate, Run FROM Campaigns INNER JOIN Message_Sent ON Campaigns.ID=Message_Sent.ID where Message_Sent.Numsent>0");
            this.bindingSource1.DataSource = table;
            this.campaignView1.DataSource = bindingSource1;
            this.campaignView1.Refresh();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
               /*
            LDBSQL SQL = new LDBSQL();
            string str = "Select Numsent FROM Message_Sent where ID='" + this.pGetcmpSelDD1ID() + "'";
            DataTable table = SQL.query("Select Numsent FROM Message_Sent where ID=" + Convert.ToInt64(this.pGetcmpSelDD1ID()));
            if (table.TableName != "Error")
            {
                foreach (DataRow r in table.Rows)
                {
                    num_Msg_sent.Text = r["Numsent"].ToString();
                }
            }*/
            updateGrid();
        }

        private void cmpAddSource_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            /*Thread.Sleep(1000);
            string IDs = "";
            ListItem item;
            foreach (int i in this.cmpAddSource.CheckedIndices)
            {
                item = (ListItem)this.cmpAddSource.Items[i];
                IDs += item.ID;
            }
            if (IDs.Length > 0)
            {
                IDs = IDs.Substring(0, IDs.Length - 1);
            }
            item = (ListItem)this.cmpAddSource.Items[this.cmpAddSource.SelectedIndex];
            IDs = item.ID;
            LDBSQL SQL = new LDBSQL();
            string str = "SELECT * FROM LEADS where FileID='" + IDs + "'";
            DataTable table=SQL.query("SELECT * FROM LEADS where FileID='"+IDs+"'");
            numleads.Text = (table.Rows.Count).ToString();*/
        }

        private void cmpAddSch1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void label53_Click(object sender, System.EventArgs e)
        {

        }

        private void impReset_Click(object sender, System.EventArgs e)
        {
            impFilePath.Clear();
            impFilePath.ReadOnly = false;
            impBrowse.Enabled = true;
            deFileMapping();
            impSuccess.Text = "0";
            impFail.Text = "0";
            impOrigTotal.Text = "0";
            impMalformed.Text = "0";
            impInvalid.Text = "0";
            impPrevious.Text = "0";
            impBCarr.Text = "0";
            impNCarr.Text = "0";
            impDeactive.Text = "0";
            impDNT.Text = "0";
            impAddTotal.Text = "0";
            impImportConsole.Clear();

        }

        private void modMessages_SelectedIndexChanged(object sender, System.EventArgs e)
        {

        }

        private void MsgTxt_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {

            bool has32 = false;
            string IDs = "";
            ListItem item;
            foreach (int i in this.cmpAddSource.CheckedIndices)
            {
                item = (ListItem)this.cmpAddSource.Items[i];
                IDs += '\'';
                if (item.ID != "32")
                {
                    IDs += item.ID;
                }
                else
                {
                    has32 = true;
                }
                
                IDs += '\'';
                IDs += ',';
            }
            if ((IDs.Length > 0) && IDs[IDs.Length - 1] != '\'')
            {
                IDs = IDs.Substring(0, IDs.Length - 1);
            }
            //item = (ListItem)this.modCmpSource.Items[this.modCmpSource.SelectedIndex];
            //IDs = item.ID;
            int total = 0;
            LDBSQL SQL = new LDBSQL();
            string str = "SELECT * FROM Leads inner join Imported on Leads.FileID=Imported.ID where CampId = 0 and LSID  IN (" + IDs + ")";
            DataTable table = SQL.query("SELECT * FROM Leads inner join Imported on Leads.FileID=Imported.ID where CampId = 0 and LSID  IN (" + IDs + ")");
            total = (table.Rows.Count);
            if (has32)
            {
                str = "SELECT * FROM Leads_Bad_Carriers where CampId=0";
                table = SQL.query(str);
                total = total + table.Rows.Count;
            }
            numleads.Text = (total).ToString();
        }

        private void cmpModT_Click(object sender, System.EventArgs e)
        {

        }

        private void Save_DNT_Click(object sender, System.EventArgs e)
        {
            DateTime Today = new DateTime();
            Today = DateTime.Now;
            string phone=DNT_TXT_BOX.Text;
            phone="+1"+phone;
            SMSSQL smsSQL = new SMSSQL();
            string query = "INSERT INTO DoNotText (TelephoneNumber,Date,Time,Message) VALUES ('" + phone + "','" + Today.ToString("yyyy-MM-dd") + "','" + Today.ToString("HH:mm:ss") + "','STOP' )";
            if (smsSQL.edit(query)==1)
            {
                WriteToConsole("DNT Added: " + phone, 1);
            }
            else
            {
                WriteToConsole("DNT Declined: " + phone, 1);
            }


            string rows = ROWS.Text;
            long ID = 0;

            long MIN = 0;
            long MAX = 100;
            SMSSQL sql = new SMSSQL();
            DataTable table = new DataTable();
            string query1 = "Select ID FROM DoNotText where TelephoneNumber='"+phone+"'";
            table = sql.query(query1);
            foreach (DataRow r in table.Rows)
            {
                 ID= Convert.ToInt64(r["ID"].ToString());
            }
            MAX = ID + 10;
            MIN = ID;
            string query2 = "Select TelephoneNumber FROM DoNotText where ID BETWEEN " + MIN + " AND " + MAX + "  order by ID asc";
            table = sql.query(query2);
            DNTTextBox1.Text = "Phone Number" + "\r\n";
            foreach (DataRow r in table.Rows)
            {
                DNTTextBox1.Text = DNTTextBox1.Text + r["TelephoneNumber"].ToString() + "\r\n";
            }
            ROWS.Text = MIN + "-" + MAX;

        }

        private void impPrevious_Click(object sender, EventArgs e)
        {

        }

        private void msgModDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = ((ListItem)this.msgModDD.SelectedItem).ID;
            LDBSQL LDB = new LDBSQL();
            string query = "SELECT Message FROM Messages where ID=" + Convert.ToInt64(ID);
            DataTable table=LDB.query(query);
            foreach (DataRow r in table.Rows)
            {
                msgModText.Text = r["Message"].ToString();
            }
        }

        private void settingsTab_Click(object sender, EventArgs e)
        {

        }

        private void label58_Click(object sender, EventArgs e)
        {

        }

        private void DeleteSch_Click(object sender, EventArgs e)
        {
            this.Thread = new Thread(new ThreadStart(this.SchDel));
            this.Thread.Start();
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmpSelDD1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                dataGridView1.Rows.Clear();
            }
            LDBSQL SQL = new LDBSQL();
            bool has32 = false;
            string Sources = "";
            string statedeletes = "";
            string StateDeletes = "";
            string sql = "SELECT Sources,StateDeletes FROM Campaigns where ID='" + this.pGetcmpSelDD1ID() + "'";
            DataTable record = SQL.query(sql);
            foreach (DataRow r in record.Rows)
            {
                Sources = r["Sources"].ToString();
                statedeletes = r["StateDeletes"].ToString();
            }
            char[] Delimiters = { ',' };
            string[] Leads = Sources.Split(Delimiters);
            if (Leads.Contains("32"))
            {
                Sources = "";
                for (int i = 0; i < Leads.Length; i++)
                {
                    if (Leads[i] != "32")
                    {
                        Sources = Sources + Leads[i] + ",";
                    }
                    else
                    {
                        has32 = true;
                    }
                }
                if ((Sources.Length > 0) && Sources[Sources.Length - 1] == ',')
                {
                    Sources = Sources.Substring(0, Sources.Length - 1);
                }

            }
            DataTable StateDeletes_DataTable = SQL.query("SELECT INAreaCodes FROM StateAreaCodes WHERE ID  IN (" + statedeletes + ")");
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



         /*   sql = "SELECT Message FROM Messages where ID='" + str + "'";
            record = SQL.query(sql);
            foreach (DataRow r in record.Rows)
            {
                MsgTxt.Text = r["Message"].ToString();
            }*/
            
             /*   DataGridViewColumn Source = new DataGridViewColumn();
                DataGridViewCell cell = new DataGridViewTextBoxCell();
                Source.CellTemplate = cell;

                Source.HeaderText = "Source";
                Source.Name = "Source";
                Source.Visible = true;
                Source.Width = 100;
                dataGridView1.Columns.Add(Source);

                DataGridViewColumn RemainingLeads = new DataGridViewColumn();
                DataGridViewCell cell2 = new DataGridViewTextBoxCell();
                RemainingLeads.CellTemplate = cell2;

                RemainingLeads.HeaderText = "Remaining Leads";
                RemainingLeads.Name = "Remaining Leads";
                RemainingLeads.Visible = true;
                RemainingLeads.Width = 100;
                dataGridView1.Columns.Add(RemainingLeads);*/
            
            if (Sources != "")
            {
                LDBSQL ldb = new LDBSQL();
                LDBSQL ldb2 = new LDBSQL();

                
                   int i=0;
                   string sql3 = "Select FileName, COUNT(*) as RemainingLeads From Imported left outer join Leads on Leads.FileID = Imported.ID where FileID in (SELECT ID FROM Imported WHERE LSID IN (" + Sources + ")) and Campid='0' group by FileName ";
                    DataTable record3 = SQL.query(sql3);
                    Int64 remaining = 0;
                    if (record3.TableName != "Error")
                    {
                        while (i < record3.Rows.Count)
                        {
                            if (record3.Rows.Count != 0)
                            {
                                
                                    this.dataGridView1.Rows.Add();
                                

                                this.dataGridView1.Rows[i].Cells[0].Value = record3.Rows[i]["FileName"].ToString();
                      
                                this.dataGridView1.Rows[i].Cells[1].Value = record3.Rows[i]["RemainingLeads"].ToString();


                            }
                            else
                            {
                                
                                    this.dataGridView1.Rows.Add();
                                
                                this.dataGridView1.Rows[i].Cells[0].Value = "0";
                                this.dataGridView1.Rows[i].Cells[1].Value = "0";

                            }
                            i++;

                        }

                    }
                    if (has32)
                    {
                        i = 0;
                        int n = 0;
                        sql3 = "select count(*) as 'remaining leads' from Leads_Bad_Carriers where FileID='32' and CampId=0";
                        record3 = SQL.query(sql3);
                        if (record3.TableName != "Error")
                        {
                            while (i < record3.Rows.Count)
                            {
                                if (record3.Rows.Count != 0)
                                {

                                    n=this.dataGridView1.Rows.Add();


                                    this.dataGridView1.Rows[n].Cells[0].Value = "Bad Carriers";

                                    this.dataGridView1.Rows[n].Cells[1].Value = record3.Rows[i]["remaining leads"].ToString();


                                }
                                else
                                {

                                  n=  this.dataGridView1.Rows.Add();

                                    this.dataGridView1.Rows[i].Cells[0].Value = "0";
                                    this.dataGridView1.Rows[i].Cells[1].Value = "0";

                                }
                                i++;

                            }

                        }
                    }
            }
        }

        private void viewLeads_SelectedIndexChanged(object sender, EventArgs e)
        {

            LDBSQL SQL = new LDBSQL();
            if (this.pGetviewLeads() != "-1")
            {
                string sql = "SELECT FileName FROM Imported where ID='" + this.pGetviewLeads() + "'";
                DataTable record = SQL.query(sql);
                string sql2 = "SELECT COUNT(*) as \"NumberofLeads\" FROM Leads where FileID='" + this.pGetviewLeads() + "'";
                DataTable record2 = SQL.query(sql2);
                string sql3 = "SELECT COUNT(*) as \"RemainingLeads\" FROM Leads where FileID='" + this.pGetviewLeads() + "' and Campid='" + 0 + "'";
                DataTable record3 = SQL.query(sql3);
                Int64 leadssent = Convert.ToInt64(record2.Rows[0]["NumberofLeads"].ToString()) - Convert.ToInt64(record3.Rows[0]["RemainingLeads"].ToString());
                this.dataGridView2.Rows[0].Cells[0].Value = record.Rows[0]["FileName"].ToString();
                this.dataGridView2.Rows[0].Cells[1].Value = record3.Rows[0]["RemainingLeads"].ToString();
                this.dataGridView2.Rows[0].Cells[2].Value = leadssent.ToString();
              
            }
            else
            {
                this.dataGridView2.Rows[0].Cells[0].Value = "";
                this.dataGridView2.Rows[0].Cells[1].Value = "";
                this.dataGridView2.Rows[0].Cells[2].Value = "";
               
            }
            
        }

        private void RateBox2_TextChanged(object sender, EventArgs e)
        {
            LDBSQL SQL = new LDBSQL();
            string str = RateBox2.Text;
            if (str != "")
            {
                trackBar1.Value = Convert.ToInt16(str);
                string sql = "UPDATE Campaigns SET Rate=" + trackBar1.Value + " where ID=" + this.pGetcmpSelDD1ID();
                SQL.edit(sql);
            }
        }

        private void settingSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ServerIP = serverIP.Text;
            Properties.Settings.Default.ServerPort = Int32.Parse(serverPort.Text);
            Properties.Settings.Default.Verbose = verbose.SelectedIndex;

        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            DataGridViewColumn Campaigns = new DataGridViewColumn();
            DataGridViewCell Sourcecell1 = new DataGridViewTextBoxCell();
            Campaigns.CellTemplate = Sourcecell1;

            Campaigns.HeaderText = "Campaign";
            Campaigns.Name = "Campaign";
            Campaigns.Visible = true;
            Campaigns.Width = 200;
            ReportView.Columns.Add(Campaigns);

            DataGridViewColumn Leads = new DataGridViewColumn();
            DataGridViewCell Sourcecell = new DataGridViewTextBoxCell();
            Leads.CellTemplate = Sourcecell;

            Leads.HeaderText = "Leads";
            Leads.Name = "Leads";
            Leads.Visible = true;
            Leads.Width = 187;

            ReportView.Columns.Add(Leads);

            Queue<report_structure> q = new Queue<report_structure>();
            string str2 = "";
            string output = "";
            LDBSQL SQL = new LDBSQL();
            string str = "select b.Name,c.Name, count(*) as \"Leads Sent\" from Leads a INNER JOIN Campaigns b ON a.CampId = b.ID Join refLS c on a.FileID = c.ID where QueueTime between '" + Date1.Text + "%' and '" + Date2.Text + "%' group by b.Name,c.Name,c.ID";
            DataTable report = SQL.query(str);
            if (report.TableName != "Error")
            {
                foreach (DataRow r in report.Rows)
                {


                    if (str2 != r[0].ToString())
                    {
                        str2 = r[0].ToString();
                        report_structure rpt_strct = new report_structure();
                        rpt_strct.Campaign_name = r[0].ToString();

                        rpt_strct.leads = new Queue<reportleads>();
                        reportleads rptlds = new reportleads();
                        rptlds.lead_name = r[1].ToString();
                        rptlds.leads_sent = Convert.ToInt32(r[2].ToString());

                        rpt_strct.total_leads_Sent = rpt_strct.total_leads_Sent + Convert.ToInt32(r[2].ToString());
                        rpt_strct.leads.Enqueue(rptlds);
                        q.Enqueue(rpt_strct);
                    }
                    else
                    {
                        report_structure rpt_strct_temp;
                        reportleads rptlds = new reportleads();
                        rptlds.lead_name = r[1].ToString();
                        rptlds.leads_sent = Convert.ToInt32(r[2].ToString());
                        rpt_strct_temp = q.Dequeue();
                        rpt_strct_temp.total_leads_Sent = rpt_strct_temp.total_leads_Sent + Convert.ToInt32(r[2].ToString());
                        rpt_strct_temp.leads.Enqueue(rptlds);
                        q.Enqueue(rpt_strct_temp);
                    }
                }
                int size = 0;

                int i = 0;
                int x = 0;
                int ALL = 0;
                Queue<report_structure> temp = new Queue<report_structure>();
                while (q.Count > 0)
                {
                    this.ReportView.Rows.Add();
                    report_structure rpt_strct_temp = q.Dequeue();
                    this.ReportView.Rows[i].Cells[0].Value = rpt_strct_temp.Campaign_name;
                    this.ReportView.Rows[i].Cells[1].Value = rpt_strct_temp.total_leads_Sent;
                    ALL = ALL + rpt_strct_temp.total_leads_Sent;
                    while (rpt_strct_temp.leads.Count > 0)
                    {
                        i++;
                        reportleads rptlds = rpt_strct_temp.leads.Dequeue();
                        this.ReportView.Rows.Add();
                        this.ReportView.Rows[i].Cells[0].Value = "  " + rptlds.lead_name;
                        this.ReportView.Rows[i].Cells[1].Value = "  " + rptlds.leads_sent;
                    }
                    i++;
                    temp.Enqueue(rpt_strct_temp);
                }
                this.ReportView.Rows.Add();

                this.ReportView.Rows[i].Cells[0].Value = "ALL";
                this.ReportView.Rows[i].Cells[1].Value = ALL;
                while (temp.Count > 0)
                {
                    i++;
                    report_structure rpt_strct_temp = temp.Dequeue();
                    this.ReportView.Rows.Add();
                    this.ReportView.Rows[i].Cells[0].Value = "  " + rpt_strct_temp.Campaign_name;
                    this.ReportView.Rows[i].Cells[1].Value = "  " + rpt_strct_temp.total_leads_Sent;

                }

                ReportView.Height = 0;
                for (int z = 0; z <= i + 2; z++)
                {
                    DataGridViewRow row = ReportView.Rows[0];
                    ReportView.Height = ReportView.Height + row.Height;

                }
                ReportView.Height = ReportView.Height + 5;
            }


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }


        private void transferATT()
        {
            LDBSQL SQL = new LDBSQL();
            LDBSQL SQL2 = new LDBSQL();
            string Leadinsert = "";
            string ATTtableQuery = "SELECT ID,CampId,MsgID,FileID,Full_Name,First_Name,Last_Name,Address_One,Address_Two,City,State,Zip_Code,area_code,Telephone_Number,IP_Address,URL,Opt_in_Date,QueueTime,DateLoaded,TimeZone FROM TempATTLeads where CampId=58";
            string DuplicateQuery = "Select Telephone_Number from Leads where CampId=58 and Telephone_Number='";
            DataTable table = SQL.query(ATTtableQuery);
            DataTable table2 = new DataTable();
            foreach(DataRow r in table.Rows){
                table2=SQL.query(DuplicateQuery+r["Telephone_Number"].ToString()+"'");
                if (table2.Rows.Count == 0)
                {
                    Leadinsert = "INSERT INTO Leads (CampId,MsgID,FileID,Full_Name,First_Name,Last_Name,Address_One,Address_Two,City,State,Zip_Code,Telephone_Number,IP_Address,URL,Opt_in_Date,QueueTime,DateLoaded,TimeZone)  VALUES  ('" + r["CampId"].ToString() + "','" + r["MsgID"].ToString() + "','" + r["FileID"].ToString() + "','" + r["Full_Name"].ToString() + "','" + r["First_Name"].ToString() + "','" + r["Last_Name"].ToString() + "','" + r["Address_One"].ToString() + "','" + r["Address_Two"].ToString() + "','" + r["City"].ToString() + "','" + r["State"].ToString() + "','" + r["Zip_Code"].ToString() + "','" + r["Telephone_Number"].ToString() + "','" + r["IP_Address"].ToString() + "','" + r["URL"].ToString() + "','" + r["Opt_in_Date"].ToString() + "','" + r["QueueTime"].ToString() + "','" + r["DateLoaded"].ToString() + "','" + r["TimeZone"].ToString() + "')";
                    SQL2.edit(Leadinsert);
                }
                else
                {
                    Console.Write("seen it");
                }
            
            }
        }

        private void searchbutton_Click_1(object sender, EventArgs e)
        {
            // transferATT();
            LDBSQL del = new LDBSQL();
            del.edit("DELETE FROM basic_phone_message");
            del.edit("DELETE FROM distinct_phone_message");
            del.edit("DELETE FROM Basic_report");
            this.ReportView.Columns.Clear();
            this.ReportView.Rows.Clear();
            string SF800GMonth = "";
            string SF800GDay="";
            string SF800GYR = "";
            string SF800GDate1 = "";
            string SF800GDate2 = "";
            char[] delimiters={'/'};
            string[] Date1_string_arr = Date1.Text.Split(delimiters);
            string[] Date2_string_arr = Date2.Text.Split(delimiters);

            if (Date1_string_arr[0].Length < 2)
            {
                SF800GMonth =  "0" + Date1_string_arr[0];
            }
            else
            {
                SF800GMonth = Date1_string_arr[0];
            }

            if (Date1_string_arr[1].Length < 2)
            {
                SF800GDay = "0" + Date1_string_arr[1];
            }
            else
            {
                SF800GDay = Date1_string_arr[1];
            }

            if (Date1_string_arr[2].Length == 4)
            {
                SF800GYR = Date1_string_arr[2].Substring(2, 2);
            }

            SF800GDate1 = SF800GMonth + "/" + SF800GDay + "/" + SF800GYR;

            if (Date2_string_arr[0].Length < 2)
            {
                SF800GMonth = "0" + Date2_string_arr[0];
            }
            else
            {
                SF800GMonth = Date2_string_arr[0];
            }

            if (Date2_string_arr[1].Length < 2)
            {
                SF800GDay = "0" + Date2_string_arr[1];
            }
            else
            {
                SF800GDay = Date2_string_arr[1];
            }

            if (Date2_string_arr[2].Length == 4)
            {
                SF800GYR = Date2_string_arr[2].Substring(2, 2);
            }

            SF800GDate2 = SF800GMonth + "/" + SF800GDay + "/" + SF800GYR;

            DataGridViewColumn Campaigns = new DataGridViewColumn();

            DataGridViewCell Sourcecell1 = new DataGridViewTextBoxCell();
            Campaigns.CellTemplate = Sourcecell1;

            Campaigns.HeaderText = "Campaign";
            Campaigns.Name = "Campaign";
            Campaigns.Visible = true;
            Campaigns.Width = 200;
            ReportView.Columns.Add(Campaigns);

            DataGridViewColumn Leads = new DataGridViewColumn();
            DataGridViewCell Sourcecell = new DataGridViewTextBoxCell();
            Leads.CellTemplate = Sourcecell;

            Leads.HeaderText = "Leads";
            Leads.Name = "Leads";
            Leads.Visible = true;
            Leads.Width = 187;

            ReportView.Columns.Add(Leads);

            DataGridViewColumn Leads2 = new DataGridViewColumn();
            DataGridViewCell Sourcecell2 = new DataGridViewTextBoxCell();
            Leads2.CellTemplate = Sourcecell2;

            Leads2.HeaderText = "Unique Leads";
            Leads2.Name = "Unique Leads";
            Leads2.Visible = true;
            Leads2.Width = 187;

            ReportView.Columns.Add(Leads2);


            DataGridViewCellStyle style = new DataGridViewCellStyle();

            style.Font = new Font(ReportView2.Font, FontStyle.Bold);

            Queue<report_structure> q = new Queue<report_structure>();
            string str2 = "";
            string output = "";
            int i = 0;
            LDBSQL SQL = new LDBSQL();
            
            SMSSQL smsSQL = new SMSSQL();
            string str = "";
            if (Date1.Text == Date2.Text)
            {
                str = "select   Destination,Message from TextHistory  where convert(char(10),Date,101) = '" + "0" + Date1.Text + "' and Message is not null";
            }
            else
            {
                str = "select  Destination,Message from TextHistory  where convert(char(10),Date,101) between '" + "0" + Date1.Text + "' and '" + "0" + Date2.Text + "' and Message is not null order by Destination,Message asc";
            }

            DataTable report = smsSQL.query(str);
            if (report.TableName != "Error")
            {
                if (report.Rows.Count != 0)
                {
                    string Message = "";
                    foreach(DataRow r in report.Rows){
                       
                      
                        if ((r["Destination"].ToString() != "75612") && (r["Destination"].ToString() != ""))
                        {
                            str = r["Destination"].ToString().Substring(2, r["Destination"].ToString().Length - 2);
                            Message = r["Message"].ToString();
                            Message = Regex.Replace(Message, @"\'", @"''");
                            str2 = "INSERT INTO basic_phone_message (phone,Message) VALUES ('" + str + "','" + Message + "')";
                            SQL.edit(str2);
                        }
                    }


                }

                if (SF800GDate1 == SF800GDate2)
                {
                    str = "select   DestinationNumber,Message from SF800Gpreview  where Date like '%" + SF800GDate1 + "%' ";
                }
                else
                {
                    str = "select  DestinationNumber,Message from SF800Gpreview  where Date between '" + SF800GDate1 + "' and '" + SF800GDate2 + "' ";
                }

                report = SQL.query(str);
                if (report.TableName != "Error")
                {
                    if (report.Rows.Count != 0)
                    {
                        string Message = "";
                        foreach (DataRow r in report.Rows)
                        {
                            str = r["DestinationNumber"].ToString();
                            if ((r["DestinationNumber"].ToString() != "7205480254") && (r["DestinationNumber"].ToString() != ""))
                            {
                                str = r["DestinationNumber"].ToString().Substring(2, r["DestinationNumber"].ToString().Length - 2);
                                Message = r["Message"].ToString();
                                Message = Regex.Replace(Message, @"\'", @"''");
                                str2 = "INSERT INTO basic_phone_message (phone,Message) VALUES ('" + str + "','" + Message + "')";
                                SQL.edit(str2);
                            }

                        }
                    }
                }


                if (Date1.Text == Date2.Text)
                {
                    str = "select  Destination,Message from TextHistory  where convert(char(10),Date,101) = '" + "0" + Date1.Text + "' and Message is not null";
                }
                else
                {
                    str = "select  Destination,Message from TextHistory  where convert(char(10),Date,101) between '" + "0" + Date1.Text + "' and '" + "0" + Date2.Text + "' and Message is not null order by Destination,Message asc";
                }

                report = smsSQL.query(str);
                if (report.TableName != "Error")
                {
                    if (report.Rows.Count != 0)
                    {
                        string Message = "";
                        foreach (DataRow r in report.Rows)
                        {
                            if ((r["Destination"].ToString() != "75612") && (r["Destination"].ToString() != ""))
                            {
                                str = r["Destination"].ToString().Substring(2, r["Destination"].ToString().Length - 2);
                                Message = r["Message"].ToString();
                                Message = Regex.Replace(Message, @"\'", @"''");
                                str2 = "INSERT INTO distinct_phone_message (phone,Message) VALUES ('" + str + "','" + Message + "')";
                                SQL.edit(str2);
                            }
                        }
                    }
                }

                DataTable table3 = new DataTable();

                string ldbstring = "select c.Name,d.Name, count(*) as \"Leads Sent\" from basic_phone_message a inner join Messages b on a.Message = b.Message inner join Campaigns c on b.ID=c.Message inner join Leads d on a. in c.Sources group by c.Name,d.Name";
                string phone;


                DataTable table = new DataTable();
                DataTable ldbtable = SQL.query(ldbstring);
                if (ldbtable.TableName != "Error")
                {
                    if (ldbtable.Rows.Count != 0)
                    {

                        foreach (DataRow r in ldbtable.Rows)
                        {
             
                                 SQL.edit("INSERT INTO Basic_report(Campaign_Name,Source_Name,Num_leads,Unique_Leads) VALUES('" + r[0].ToString() + "','" + r[1].ToString() + "','" + r[2].ToString()+"',0)");


                        }
                    }
                }


                ldbstring = "select c.Name,d.Name, count(*) as \"unique Leads Sent\" from distinct_phone_message a inner join Messages b on a.Message = b.Message inner join Campaigns c on b.ID=c.Message inner join refLS d on cast(d.ID AS varchar(50))=c.Sources group by c.Name,d.Name";
                table = SQL.query(ldbstring);
                if (table.TableName != "Error")
                {
                    if (table.Rows.Count != 0)
                    {
                        foreach (DataRow r in table.Rows)
                        {
                                 SQL.edit("UPDATE Basic_report SET Unique_Leads='" + r[2].ToString() + "' where Campaign_Name='" + r[0].ToString() + "' and Source_Name='" + r[1].ToString() + "'");
                        }
                    }
                }

                int index = 0;
                report = SQL.query("select * from Basic_report");
                if (report.TableName != "Error")
                {
                    while (report.Rows.Count > index)
                    {
                        report_structure rpt_strct = new report_structure();
                        rpt_strct.Campaign_name = report.Rows[index][0].ToString();
                        rpt_strct.leads = new Queue<reportleads>();
                        while (rpt_strct.Campaign_name == report.Rows[index][0].ToString())
                        {
                            reportleads rptlds = new reportleads();
                            rptlds.lead_name = report.Rows[index][1].ToString();
                            rptlds.leads_sent = Convert.ToInt32(report.Rows[index][2].ToString());
                            rptlds.unique_leads = Convert.ToInt32(report.Rows[index][3].ToString());
                            rpt_strct.total_leads_Sent = rpt_strct.total_leads_Sent + Convert.ToInt32(report.Rows[index][2].ToString()); 
                            rpt_strct.total_unique_leads_Sent = rpt_strct.total_unique_leads_Sent + Convert.ToInt32(report.Rows[index][3].ToString());
                            rpt_strct.leads.Enqueue(rptlds);
                            index++;
                            if (index == report.Rows.Count)
                            {
                                break;
                            }
                        }
                        q.Enqueue(rpt_strct);
                        if (index == report.Rows.Count)
                        {
                            break;
                        }

                    }

                }
                int size = 0;

                i = 0;
                int x = 0;
                int ALL = 0;
                int Unique_ALL = 0;
                Queue<report_structure> temp = new Queue<report_structure>();
                while (q.Count > 0)
                {
                    this.ReportView.Rows.Add();
                    report_structure rpt_strct_temp = q.Dequeue();
                    this.ReportView.Rows[i].Cells[0].Value = rpt_strct_temp.Campaign_name;
                    ReportView.Rows[i].DefaultCellStyle = style;
                    this.ReportView.Rows[i].Cells[1].Value = rpt_strct_temp.total_leads_Sent;
                    this.ReportView.Rows[i].Cells[2].Value = rpt_strct_temp.total_unique_leads_Sent;
                    ALL = ALL + rpt_strct_temp.total_leads_Sent;
                    Unique_ALL = ALL + rpt_strct_temp.total_unique_leads_Sent;
                    while (rpt_strct_temp.leads.Count > 0)
                    {
                        i++;
                        reportleads rptlds = rpt_strct_temp.leads.Dequeue();
                        this.ReportView.Rows.Add();
                        this.ReportView.Rows[i].Cells[0].Value = "  " + rptlds.lead_name;
                        this.ReportView.Rows[i].Cells[1].Value = "  " + rptlds.leads_sent;
                        this.ReportView.Rows[i].Cells[2].Value = "  " + rptlds.unique_leads;
                    }
                    i++;
                    temp.Enqueue(rpt_strct_temp);
                }
                this.ReportView.Rows.Add();

                this.ReportView.Rows[i].Cells[0].Value = "ALL";
                this.ReportView.Rows[i].Cells[1].Value = ALL;
                ReportView.Rows[i].DefaultCellStyle = style;
                while (temp.Count > 0)
                {
                    i++;
                    report_structure rpt_strct_temp = temp.Dequeue();
                    this.ReportView.Rows.Add();
                    this.ReportView.Rows[i].Cells[0].Value = "  " + rpt_strct_temp.Campaign_name;
                    this.ReportView.Rows[i].Cells[1].Value = "  " + rpt_strct_temp.total_leads_Sent;
                    this.ReportView.Rows[i].Cells[2].Value = "  " + rpt_strct_temp.total_unique_leads_Sent;

                }
                /*
                    ReportView.Height = 0;
                    for (int z = 0; z <= i + 2; z++)
                    {
                        DataGridViewRow row = ReportView.Rows[0];
                        ReportView.Height = ReportView.Height + row.Height;

                    }
                    ReportView.Height = ReportView.Height + 5;
          
                  */

            }

        }

        private void ReportSearch2_Click(object sender, EventArgs e)
        {

            this.ReportView2.Columns.Clear();
            this.ReportView2.Rows.Clear();
            int sourcetotal = 0;
            int totalsentforCampaign = 0;
            int totalsentforSource = 0;
            int totalstops = 0;
            string Campaign = "";
            string Source = "";
            string FileName = "";
            DataGridViewColumn Campaigns = new DataGridViewColumn();

            DataGridViewCell Sourcecell1 = new DataGridViewTextBoxCell();
            Campaigns.CellTemplate = Sourcecell1;

            Campaigns.HeaderText = "Campaign";
            Campaigns.Name = "Campaign";
            Campaigns.Visible = true;
            Campaigns.Width = 200;
            ReportView2.Columns.Add(Campaigns);

            DataGridViewColumn Leads = new DataGridViewColumn();
            DataGridViewCell Sourcecell = new DataGridViewTextBoxCell();
            Leads.CellTemplate = Sourcecell;


            DataGridViewCellStyle

             style = new DataGridViewCellStyle();

            style.Font =

            new Font(ReportView2.Font, FontStyle.Bold);
           
            Leads.HeaderText = "Opt-Out";
            Leads.Name = "Opt-OUT";
            Leads.Visible = true;
            Leads.Width = 187;

            ReportView2.Columns.Add(Leads);
            LDBSQL SQL =new LDBSQL();
            LDBSQL SQL1 = new LDBSQL();
            SMSSQL smsSQL = new SMSSQL();
            Queue<report_structure> q = new Queue<report_structure>();
            /*string str = "select CampId,FileID,Telephone_Number from Leads where QueueTime between '" + ReportDate1.Text + "%' and  '" + ReportDate2.Text + "%' or QueueTime Like '%" + ReportDate1.Text+"%'";
            DataTable report=SQL.query(str);
            OptoutBar.Maximum = report.Rows.Count;
            if(report.TableName != "Error"){
                foreach (DataRow r in report.Rows)
              {
                  OptoutBar.Increment(1);
                DataTable table=SQL.query("select Name from Campaigns where ID="+r["CampId"].ToString());
                Campaign = table.Rows[0]["Name"].ToString();
                Source = r["FileID"].ToString();
                table = SQL.query("SELECT CampId from Leads where CampId=" + r["CampId"].ToString());
                DataTable report2 = SQL.query("Select * from Opt_Out_Reports where Campaign_Name='" + Campaign + "'");
                if (report2.TableName != "Error")
                {
                 

                    if (report2.Rows.Count == 0)
                    {
                         str = "select CampId,FileID,Telephone_Number from Leads where QueueTime between '" + ReportDate1.Text + "%' and  '" + ReportDate2.Text + "%' or QueueTime Like '%" + ReportDate1.Text + "%' and CampId="+r["CampId"].ToString();
                         table=SQL.query(str);

                         SQL.edit("INSERT INTO Opt_Out_Reports (Campaign_Name,Source,opt_out,Source_total,Campaign_total) VALUES('" + Campaign + "','" + Campaign + "',0,0," + table.Rows.Count + ")");
                    }
                    else
                    {
                       
                        //totalsentforSource
                        table = SQL.query("Select ID,FileName from Imported where ID=" + Source);
                       
                        FileName = table.Rows[0]["FileName"].ToString();
                       
                        if (SQL.query("Select * from Opt_Out_Reports where Campaign_Name='" + Campaign + "' and Source='" + FileName + "'").Rows.Count == 0)
                        {
                             str = "select CampId,FileID,Telephone_Number from Leads where QueueTime between '" + ReportDate1.Text + "%' and  '" + ReportDate2.Text + "%' or QueueTime Like '%" + ReportDate1.Text + "%' and CampId=" + r["CampId"].ToString()+" and FileID="+table.Rows[0]["ID"].ToString();
                             table = SQL.query(str);
                            SQL.edit("INSERT INTO Opt_Out_Reports (Campaign_Name,Source,opt_out,Source_total,Campaign_total) VALUES('" + Campaign + "','" + FileName + "',0," + table.Rows.Count + ",0)");
                        }
                        

                            DataTable tables = smsSQL.query("Select Message from DoNotText where TelephoneNumber='" + "+1" + r["Telephone_Number"].ToString()+"'");
                            if (tables.TableName != "Error")
                            {
                                if (tables.Rows.Count == 0)
                                {
                                }
                                else
                                {
                                    DataTable stopstable = SQL.query("Select opt_out from Opt_Out_Reports where Source='" + FileName + "' and Campaign_Name='" + Campaign + "'");
                                    if (stopstable.TableName != "Error")
                                    {
                                        totalstops = Convert.ToInt32(stopstable.Rows[0]["opt_out"].ToString());
                                        totalstops++;
                                        SQL.edit("UPDATE Opt_Out_Reports SET opt_out=" + totalstops.ToString() + "where Source='" + FileName + "' and Campaign_Name='" + Campaign + "'");

                                    }
                                }
                            }
                        

                    }
                    
                }


              }
            }

               DataTable report_table = SQL.query("Select * from Opt_Out_Reports");
               int index = 0;
               while (report_table.Rows.Count > index) {
                   int cmp_top=0;
                   int cmp_bottom=0;
                   report_structure rpt_stct = new report_structure();
                   rpt_stct.Campaign_name=report_table.Rows[index]["Campaign_Name"].ToString();
                   
                   rpt_stct.leads = new Queue<reportleads>();
                   while (rpt_stct.Campaign_name == report_table.Rows[index]["Campaign_Name"].ToString())
                   {
                       reportleads rptlds = new reportleads();
                       index++;
                       if ((index == report_table.Rows.Count) || (rpt_stct.Campaign_name != report_table.Rows[index]["Campaign_Name"].ToString()))
                       {
                           break;
                       }

                       rptlds.lead_name = report_table.Rows[index]["Source"].ToString();
                       int top=Convert.ToInt32(report_table.Rows[index]["opt_out"].ToString());
                       int bottom=Convert.ToInt32(report_table.Rows[index]["Source_total"].ToString());
                       cmp_top =cmp_top+Convert.ToInt32(report_table.Rows[index]["opt_out"].ToString());
                       cmp_bottom=cmp_bottom+Convert.ToInt32(report_table.Rows[index]["Source_total"].ToString());
                       if (bottom != 0)
                       {
                           rptlds.opted_out =(double)top / bottom;
                       }
                       rpt_stct.cmp_top = cmp_top;
                       rpt_stct.cmp_bottom = cmp_bottom;
                       rpt_stct.leads.Enqueue(rptlds);
                       
                      
                   }
                   if (cmp_bottom != 0)
                   {
                       rpt_stct.total_leads_opt_out = (double)cmp_top / cmp_bottom;
                   }
                   q.Enqueue(rpt_stct);
                   if (index == report_table.Rows.Count)
                   {
                       break;
                   }
               }

               int size = 0;
               int cmp_top2 = 0;
               int cmp_bottom2 = 0;
               int i = 0;
               int x = 0;
               double ALL = 0;
               Queue<report_structure> temp = new Queue<report_structure>();
               while (q.Count > 0)
               {
                   this.ReportView2.Rows.Add();
                   report_structure rpt_strct_temp = q.Dequeue();
                   this.ReportView2.Rows[i].Cells[0].Value = rpt_strct_temp.Campaign_name;
                   ReportView2.Rows[i].DefaultCellStyle = style;
                   this.ReportView2.Rows[i].Cells[1].Value = rpt_strct_temp.total_leads_opt_out;
                   cmp_top2 = cmp_top2 + rpt_strct_temp.cmp_top;
                   cmp_bottom2 = cmp_bottom2 + rpt_strct_temp.cmp_bottom;
               
                   while (rpt_strct_temp.leads.Count > 0)
                   {
                       i++;
                       reportleads rptlds = rpt_strct_temp.leads.Dequeue();
                       this.ReportView2.Rows.Add();
                       this.ReportView2.Rows[i].Cells[0].Value = "  " + rptlds.lead_name;
                       this.ReportView2.Rows[i].Cells[1].Value = "  " + rptlds.opted_out;
                   }
                   i++;
                   temp.Enqueue(rpt_strct_temp);
               }
               this.ReportView2.Rows.Add();
               ALL =(double) cmp_top2 / cmp_bottom2;
               this.ReportView2.Rows[i].Cells[0].Value = "ALL";
               this.ReportView2.Rows[i].Cells[1].Value = ALL;
               while (temp.Count > 0)
               {
                   i++;
                   report_structure rpt_strct_temp = temp.Dequeue();
                   this.ReportView2.Rows.Add();
                   this.ReportView2.Rows[i].Cells[0].Value = "  " + rpt_strct_temp.Campaign_name;

                   this.ReportView2.Rows[i].Cells[1].Value = "  " + rpt_strct_temp.total_leads_opt_out;

               }

               ReportView2.Height = 0;
               for (int z = 0; z <= i + 2; z++)
               {
                   DataGridViewRow row = ReportView2.Rows[0];
                   ReportView2.Height = ReportView2.Height + row.Height;

               }
               ReportView2.Height = ReportView2.Height + 5;*/


            string opt_out_str = "select Campaigns.Name,c.FileName,count(*) as \"Opt out\" from Leads JOIN TextingLog.dbo.DoNotText ON '+1'+Leads.Telephone_Number=TextingLog.dbo.DoNotText.TelephoneNumber INNER JOIN Campaigns  ON Leads.CampId = Campaigns.ID  Join Imported c on Leads.FileID = c.ID where QueueTime between '" + ReportDate1.Text + "%' and  '" + ReportDate2.Text + "%' or QueueTime Like '%" + ReportDate1.Text + "%' group by Campaigns.Name,c.FileName order by Campaigns.Name";

            string query1 = "select b.Name,c.FileName, count(*) as \"Leads Sent\" from Leads a INNER JOIN Campaigns b ON a.CampId = b.ID Join Imported c on a.FileID = c.ID where QueueTime between '" + ReportDate1.Text + "%' and '" + ReportDate2.Text + "%' or QueueTime like '%" + ReportDate1.Text + "%' group by b.Name,c.FileName order by b.Name";

           
           
            string Insert_from_query1="";
            int leads = 0;
            int leads_total = 0;
            
            

            DataTable new_report=SQL.query(query1);
            if (new_report.TableName != "Error")
            {
               int index3 = 0;
                 while (new_report.Rows.Count > index3) {
                      Campaign=new_report.Rows[index3]["Name"].ToString();
                      leads = Convert.ToInt32(new_report.Rows[index3]["Leads Sent"].ToString());
                      Insert_from_query1="INSERT INTO Opt_Out_Reports (Campaign_Name,Source,opt_out,Source_total,Campaign_total) VALUES ('"+Campaign+"','"+ Campaign+ "',0,0,"+leads+")";
                      SQL.edit(Insert_from_query1);
                      while (Campaign == new_report.Rows[index3]["Name"].ToString())
                      {
                          
                          //string temp = new_report.Rows[index]["Name"].ToString();
                          
                          leads_total = leads_total + leads;
                          FileName = new_report.Rows[index3]["FileName"].ToString();
                          Insert_from_query1 = "INSERT INTO Opt_Out_Reports (Campaign_Name,Source,opt_out,Source_total,Campaign_total) VALUES ('" + Campaign + "','" + FileName + "',0,"+leads+",0)";
                          SQL.edit(Insert_from_query1);
                          index3++;
                          if ((index3 == new_report.Rows.Count) || (Campaign != new_report.Rows[index3]["Name"].ToString()))
                          {
                              break;
                          }

                      }
                      SQL.edit("UPDATE Opt_Out_Reports SET Campaign_total="+leads_total.ToString()+" where Campaign_Name='"+Campaign+"' and Source='"+ Campaign +"'");
                      leads_total = 0;
                     
                      if (index3 == new_report.Rows.Count)
                      {
                          break;
                      }
                }
            }

            int optout = 0;
            DataTable table = SQL.query(opt_out_str);
            if (table.TableName != "Error")
            {
                int index2 = 0;
                while (table.Rows.Count > index2)
                {
                    Campaign = table.Rows[index2]["Name"].ToString();
                    optout = Convert.ToInt32(table.Rows[index2]["Opt out"].ToString());
                    while (Campaign == table.Rows[index2]["Name"].ToString())
                    {

                        //string tempstr = table.Rows[index2]["Name"].ToString();

                        leads_total = leads_total + optout;
                        FileName = table.Rows[index2]["FileName"].ToString();
                        DataTable duplicates = SQL.query("SELECT * FROM Opt_Out_Reports where Campaign_Name='" + Campaign + "' and Source='" + FileName + "'");
                        if (duplicates.TableName != "Error")
                        {
                            if (duplicates.Rows.Count == 1)
                            {
                                Insert_from_query1 = "UPDATE Opt_Out_Reports set opt_out=" + table.Rows[index2]["Opt out"].ToString() + " where Campaign_Name='" + Campaign + "' and Source='" + FileName + "'";
                            }
                            else
                            {
                                //SQL.edit("DELETE FROM Opt_Out_Reports WHERE ID NOT IN ( SELECT MAX(ID) FROM Opt_Out_Reports GROUP BY Campaign_Name, Source, Source_total)");
                                Insert_from_query1 = "UPDATE Opt_Out_Reports set opt_out=" + table.Rows[index2]["Opt out"].ToString() + " where Campaign_Name='" + Campaign + "' and Source='" + FileName + "'";
                                
                            }
                        }
                        SQL.edit(Insert_from_query1);
                        index2++;
                        if ((index2 == table.Rows.Count) || (Campaign != table.Rows[index2]["Name"].ToString()))
                        {
                            break;
                        }

                    }
                    SQL.edit("UPDATE Opt_Out_Reports SET opt_out=" + leads_total.ToString() + " where Campaign_Name='" + Campaign + "' and Source='" + Campaign + "'");
                    leads_total = 0;

                    if (index2 == table.Rows.Count)
                    {
                        break;
                    }
 
                    
                }

            }


            DataTable report_table = SQL.query("Select * from Opt_Out_Reports");
                int index = 0;
               while (report_table.Rows.Count > index) {
                   int cmp_top=0;
                   int cmp_bottom=0;
                   report_structure rpt_stct = new report_structure();
                   rpt_stct.Campaign_name=report_table.Rows[index]["Campaign_Name"].ToString();
                   
                   rpt_stct.leads = new Queue<reportleads>();
                   while (rpt_stct.Campaign_name == report_table.Rows[index]["Campaign_Name"].ToString())
                   {
                       reportleads rptlds = new reportleads();
                       index++;
                       if ((index == report_table.Rows.Count) || (rpt_stct.Campaign_name != report_table.Rows[index]["Campaign_Name"].ToString()))
                       {
                           break;
                       }

                       rptlds.lead_name = report_table.Rows[index]["Source"].ToString();
                       int top=Convert.ToInt32(report_table.Rows[index]["opt_out"].ToString());
                       int bottom=Convert.ToInt32(report_table.Rows[index]["Source_total"].ToString());
                       cmp_top =cmp_top+Convert.ToInt32(report_table.Rows[index]["opt_out"].ToString());
                       cmp_bottom=cmp_bottom+Convert.ToInt32(report_table.Rows[index]["Source_total"].ToString());
                       if (bottom != 0)
                       {
                           rptlds.opted_out =Math.Round((double)top / bottom,3);
                       }
                       rpt_stct.cmp_top = cmp_top;
                       rpt_stct.cmp_bottom = cmp_bottom;
                       rpt_stct.leads.Enqueue(rptlds);
                       
                      
                   }
                   if (cmp_bottom != 0)
                   {
                       rpt_stct.total_leads_opt_out = Math.Round((double)cmp_top / cmp_bottom,3);
                   }
                   q.Enqueue(rpt_stct);
                   if (index == report_table.Rows.Count)
                   {
                       break;
                   }
               }

               int size = 0;
               int cmp_top2 = 0;
               int cmp_bottom2 = 0;
               int i = 0;
               int x = 0;
               double ALL = 0;
               Queue<report_structure> temp = new Queue<report_structure>();
               while (q.Count > 0)
               {
                   this.ReportView2.Rows.Add();
                   report_structure rpt_strct_temp = q.Dequeue();
                   this.ReportView2.Rows[i].Cells[0].Value = rpt_strct_temp.Campaign_name;
                   ReportView2.Rows[i].DefaultCellStyle = style;
                   this.ReportView2.Rows[i].Cells[1].Value = rpt_strct_temp.total_leads_opt_out;
                   cmp_top2 = cmp_top2 + rpt_strct_temp.cmp_top;
                   cmp_bottom2 = cmp_bottom2 + rpt_strct_temp.cmp_bottom;
               
                   while (rpt_strct_temp.leads.Count > 0)
                   {
                       i++;
                       reportleads rptlds = rpt_strct_temp.leads.Dequeue();
                       this.ReportView2.Rows.Add();
                       this.ReportView2.Rows[i].Cells[0].Value = "  " + rptlds.lead_name;
                       this.ReportView2.Rows[i].Cells[1].Value = "  " + rptlds.opted_out;
                   }
                   i++;
                   temp.Enqueue(rpt_strct_temp);
               }
               this.ReportView2.Rows.Add();
               ALL =Math.Round((double) cmp_top2 / cmp_bottom2,3);
               this.ReportView2.Rows[i].Cells[0].Value = "ALL";
               this.ReportView2.Rows[i].Cells[1].Value = ALL;
               ReportView2.Rows[i].DefaultCellStyle = style;
               while (temp.Count > 0)
               {
                   i++;
                   report_structure rpt_strct_temp = temp.Dequeue();
                   this.ReportView2.Rows.Add();
                   this.ReportView2.Rows[i].Cells[0].Value = "  " + rpt_strct_temp.Campaign_name;

                   this.ReportView2.Rows[i].Cells[1].Value = "  " + rpt_strct_temp.total_leads_opt_out;

               }
/*
               ReportView2.Height = 0;
               for (int z = 0; z <= i + 2; z++)
               {
                   DataGridViewRow row = ReportView2.Rows[0];
                   ReportView2.Height = ReportView2.Height + row.Height;

               }
               ReportView2.Height = ReportView2.Height + 5;*/

               SQL.edit("DELETE FROM Opt_Out_Reports");
       }

        private void ImportHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ImportStatview1.Columns.Clear();
            this.ImportStatview1.Rows.Clear();

              int sourcetotal = 0;
            int totalsentforCampaign = 0;
            int totalsentforSource = 0;
            int totalstops = 0;
            string Campaign = "";
            string Source = "";
            string FileName = "";
            DataGridViewColumn Campaigns = new DataGridViewColumn();

            DataGridViewCell Sourcecell1 = new DataGridViewTextBoxCell();
            Campaigns.CellTemplate = Sourcecell1;

            Campaigns.HeaderText = "Date";
            Campaigns.Name = "Date";
            Campaigns.Visible = true;
            Campaigns.Width = 60;
            ImportStatview1.Columns.Add(Campaigns);

            DataGridViewColumn Leads = new DataGridViewColumn();
            DataGridViewCell Sourcecell = new DataGridViewTextBoxCell();
            Leads.CellTemplate = Sourcecell;



            DataGridViewCellStyle

             style = new DataGridViewCellStyle();

            style.Font =

            new Font(ImportStatview1.Font, FontStyle.Bold);


            Leads.HeaderText = "Total Original";
            Leads.Name = "Total Original";
            Leads.Visible = true;
            Leads.Width = 60;
            ImportStatview1.Columns.Add(Leads);

            DataGridViewColumn Malformed = new DataGridViewColumn();
            DataGridViewCell Malformed2 = new DataGridViewTextBoxCell();
            Malformed.CellTemplate = Malformed2;
            Malformed.HeaderText = "Malformed";
            Malformed.Name = "Malformed";
            Malformed.Visible = true;
            Malformed.Width = 60;
            ImportStatview1.Columns.Add(Malformed);

            DataGridViewColumn Previous = new DataGridViewColumn();
            DataGridViewCell Previous2 = new DataGridViewTextBoxCell();
            Previous.CellTemplate = Previous2;
            Previous.HeaderText = "Previous";
            Previous.Name = "Previous";
            Previous.Visible = true;
            Previous.Width = 60;
            ImportStatview1.Columns.Add(Previous);

            DataGridViewColumn DoNotText = new DataGridViewColumn();
            DataGridViewCell DoNotText2 = new DataGridViewTextBoxCell();
            DoNotText.CellTemplate = DoNotText2;
            DoNotText.HeaderText = "Do Not Text";
            DoNotText.Name = "Do Not Text";
            DoNotText.Visible = true;
            DoNotText.Width = 60;
            ImportStatview1.Columns.Add(DoNotText);

            DataGridViewColumn BadCarrier = new DataGridViewColumn();
            DataGridViewCell BadCarrier2 = new DataGridViewTextBoxCell();
            BadCarrier.CellTemplate = BadCarrier2;
            BadCarrier.HeaderText = "Bad Carrier";
            BadCarrier.Name = "Bad Carrier";
            BadCarrier.Visible = true;
            BadCarrier.Width = 60;
            ImportStatview1.Columns.Add(BadCarrier);

            DataGridViewColumn NoCarrier = new DataGridViewColumn();
            DataGridViewCell NoCarrier2 = new DataGridViewTextBoxCell();
            NoCarrier.CellTemplate = NoCarrier2;
            NoCarrier.HeaderText = "No Carrier";
            NoCarrier.Name = "No Carrier";
            NoCarrier.Visible = true;
            NoCarrier.Width = 60;
            ImportStatview1.Columns.Add(NoCarrier);

            DataGridViewColumn Deactive = new DataGridViewColumn();
            DataGridViewCell Deactive2 = new DataGridViewTextBoxCell();
            Deactive.CellTemplate = Deactive2;
            Deactive.HeaderText = "Deactive";
            Deactive.Name = "Deactive";
            Deactive.Visible = true;
            Deactive.Width = 60;
            ImportStatview1.Columns.Add(Deactive);

            DataGridViewColumn InvalidPhone = new DataGridViewColumn();
            DataGridViewCell InvalidPhone2 = new DataGridViewTextBoxCell();
            InvalidPhone.CellTemplate = InvalidPhone2;
            InvalidPhone.HeaderText = "Invalid Phone";
            InvalidPhone.Name = "Invalid Phone";
            InvalidPhone.Visible = true;
            InvalidPhone.Width = 60;
            ImportStatview1.Columns.Add(InvalidPhone);

            DataGridViewColumn NotImported = new DataGridViewColumn();
            DataGridViewCell NotImported2 = new DataGridViewTextBoxCell();
            NotImported.CellTemplate = NotImported2;
            NotImported.HeaderText = "Not Imported";
            NotImported.Name = "Not Imported";
            NotImported.Visible = true;
            NotImported.Width = 60;
            ImportStatview1.Columns.Add(NotImported);

            DataGridViewColumn Imported = new DataGridViewColumn();
            DataGridViewCell Imported2 = new DataGridViewTextBoxCell();
            Imported.CellTemplate = Imported2;
            Imported.HeaderText = "Imported Count";
            Imported.Name = "Imported Count";
            Imported.Visible = true;
            Imported.Width = 60;
            ImportStatview1.Columns.Add(Imported);

            DataGridViewColumn state = new DataGridViewColumn();
            DataGridViewCell state2 = new DataGridViewTextBoxCell();
            state.CellTemplate = state2;
            state.HeaderText = "State Deletes";
            state.Name = "State Deletes";
            state.Visible = true;
            state.Width = 60;
            ImportStatview1.Columns.Add(state);

         
               
            if (this.pGetImportHistory() != "-1")
            {
                LDBSQL SQL = new LDBSQL();
                string sql4 = "SELECT  OriginalCount,MalformedCount,PreviousCount,DNTCount,BadCarrierCount,NoCarrierCount,DeactiveCount,InvalidPhoneCount,NotImportedCount,ImportedCount,DateImported FROM Imported where FileName='" + this.pGetImportHistory() + "' order by DateImported DESC";
                DataTable record3 = SQL.query(sql4);
                if (record3.TableName != "Error")
                {
                    int index = 0;
                    int charAt = 0;
                    foreach (DataRow r in record3.Rows)
                    {
                        this.ImportStatview1.Rows.Add();
                        charAt = r["DateImported"].ToString().IndexOf(" ");
                        this.ImportStatview1.Rows[index].Cells[0].Value = r["DateImported"].ToString().Substring(0,charAt);
                        this.ImportStatview1.Rows[index].Cells[1].Value = r["OriginalCount"].ToString();
                        this.ImportStatview1.Rows[index].Cells[2].Value = r["MalformedCount"].ToString();
                        this.ImportStatview1.Rows[index].Cells[3].Value = r["PreviousCount"].ToString();
                        this.ImportStatview1.Rows[index].Cells[4].Value = r["DNTCount"].ToString();
                        this.ImportStatview1.Rows[index].Cells[5].Value = r["BadCarrierCount"].ToString();
                        this.ImportStatview1.Rows[index].Cells[6].Value = r["NoCarrierCount"].ToString();
                        this.ImportStatview1.Rows[index].Cells[7].Value = r["DeactiveCount"].ToString();
                        this.ImportStatview1.Rows[index].Cells[8].Value = r["InvalidPhoneCount"].ToString();
                        this.ImportStatview1.Rows[index].Cells[9].Value = r["NotImportedCount"].ToString();
                        this.ImportStatview1.Rows[index].Cells[10].Value = r["ImportedCount"].ToString();
                        index++;
                    }
                }
            }
            else
            {
               
             
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string str = now.ToString("hh:mm tt");
            if("11:59 PM" == now.ToString("hh:mm tt")){
            LDBSQL SQL = new LDBSQL();
            SQL.edit("UPDATE Message_Sent SET Numsent=0");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ImportStatview1.Columns.Clear();
            this.ImportStatview1.Rows.Clear();

            SMSSQL smsSQL = new SMSSQL();
            LDBSQL SQL = new LDBSQL();
            DataTable report;
            if (URLDate1.Text != URLDate2.Text)
            {
                report = SQL.query("select URL,Count(*) as \"text sent\" from Leads where URL IS NOT NULL and QueueTime between '" + URLDate1.Text + "%' and  '" + URLDate2.Text + "%'  group by URL order by URL");
            }
            else
            {
                report = SQL.query("select URL,Count(*) as \"text sent\" from Leads where URL IS NOT NULL and QueueTime Like '%" + URLDate1.Text + "%' group by URL order by URL");
            }
            if (report.TableName != "Error")
            {
                foreach (DataRow r in report.Rows)
                {
                    if (SQL.edit("Insert into URL_Text_Sent (URL,Text_Sent) Values ('" + r["URL"].ToString() + "'," + r["text sent"].ToString() + ")") == 1)
                    {
                    }
                    else
                    {
                        string URL = r["URL"].ToString();
                        int sent = Convert.ToInt32(r["text sent"].ToString());
                    }

                }

            }

            if (URLDate1.Text != URLDate2.Text)
            {
                report = SQL.query("select URL,Telephone_Number from Leads where URL IS NOT NULL and QueueTime between '" + URLDate1.Text + "%' and  '" + URLDate2.Text + "%'  group by URL,Telephone_Number order by URL");
            }
            else
            {
                report = SQL.query("select URL,Telephone_Number from Leads where URL IS NOT NULL and QueueTime Like '%" + URLDate1.Text + "%' group by URLv order by URL");
            }
            if (report.TableName != "Error")
            {
                foreach (DataRow r in report.Rows)
                {
                    string phonenumber = "+1"+r["Telephone_Number"].ToString();
                    string URL = r["URL"].ToString();
                    DataTable smstable= smsSQL.query("SELECT Source From TextHistory where Source='"+phonenumber+"'");
                    if (smstable.TableName != "Error")
                    {
                        DataTable responcestable=SQL.query("select responces from URL_Responces where URL='" + URL + "'");
                        if (responcestable.TableName != "Error")
                        {
                            if (responcestable.Rows.Count == 0)
                            {

                                if (SQL.edit("Insert into URL_Responces (URL,responces) Values ('" + URL + "',1)") == 1)
                                {
                                }
                                else
                                {
                                    URL = r["URL"].ToString();
                             
                                }

                            }
                            else
                            {
                                int responce = Convert.ToInt32(responcestable.Rows[0]["responces"].ToString());
                                responce++;
                                if (SQL.edit("Update URL_Responces set responces="+responce+" where URL='" + URL +"'") == 1)
                                {
                                }
                                else
                                {
                                    URL = r["URL"].ToString();
                                    //int sent = Convert.ToInt32(r["text sent"].ToString());
                                }
                            }
                        }
                    }
                }
            }

          
           /* DataTable report2;
            report = SQL.query("Select URL FROM URL_Text_Sent group by URL order by URL");
            if (report.TableName != "Error")
            {
                foreach (DataRow r in report.Rows)
                {
                    report2 = SQL.query("Select URL from URL_Responces where URL='" + r["URL"].ToString() + "'");
                    if (report2.TableName != "Error")
                    {
                        if (report2.Rows.Count == 0)
                        {

                            if (SQL.edit("Insert into URL_Responces (URL,responces) Values ('" + r["URL"].ToString() + "',0)") == 1)
                            {
                            }
                            else
                            {
                                string URL = r["URL"].ToString();
                                int sent = Convert.ToInt32(r["ALL Responces"].ToString());
                            }

                        }
                    }
                }
            }*/
            string join = "select * from URL_Text_Sent a inner Join URL_Responces b ON a.URL=b.URL";
            
            this.URLReportgrid.Columns.Clear();
            this.URLReportgrid.Rows.Clear();

            DataGridViewColumn URLaddr = new DataGridViewColumn();

            DataGridViewCell Sourcecell1 = new DataGridViewTextBoxCell();
            URLaddr.CellTemplate = Sourcecell1;

            URLaddr.HeaderText = "URL";
            URLaddr.Name = "URL";
            URLaddr.Visible = true;
            URLaddr.Width = 350;
            URLReportgrid.Columns.Add(URLaddr);

            DataGridViewColumn Malformed = new DataGridViewColumn();
            DataGridViewCell Malformed2 = new DataGridViewTextBoxCell();
            Malformed.CellTemplate = Malformed2;
            Malformed.HeaderText = "All Responces";
            Malformed.Name = "All Responces";
            Malformed.Visible = true;
            Malformed.Width = 60;
            URLReportgrid.Columns.Add(Malformed);

            DataGridViewColumn Malformed1 = new DataGridViewColumn();
            DataGridViewCell Malformed3 = new DataGridViewTextBoxCell();
            Malformed1.CellTemplate = Malformed3;
            Malformed1.HeaderText = "Sent";
            Malformed1.Name = "Sent";
            Malformed1.Visible = true;
            Malformed1.Width = 60;
            URLReportgrid.Columns.Add(Malformed1);

            DataGridViewColumn Malformed5 = new DataGridViewColumn();
            DataGridViewCell Malformed4 = new DataGridViewTextBoxCell();
            Malformed5.CellTemplate = Malformed4;
            Malformed5.HeaderText = "%";
            Malformed5.Name = "%";
            Malformed5.Visible = true;
            Malformed5.Width = 60;
            URLReportgrid.Columns.Add(Malformed5);
            double top = 0;
            double bottom = 0;

            DataTable table = SQL.query("select * from URL_Text_Sent a inner Join URL_Responces b ON a.URL=b.URL");
            if (table.TableName != "Error")
            {
                int index = 0;
                foreach (DataRow r in table.Rows)
                {
                  //  this.URLReportgrid.Rows.Add();
                 //   this.URLReportgrid.Rows[index].Cells[0].Value = r["URL"].ToString();
                  // this.URLReportgrid.Rows[index].Cells[1].Value = r["responces"].ToString();
                  //  this.URLReportgrid.Rows[index].Cells[2].Value = r["Text_Sent"].ToString();
                    top = Convert.ToDouble(r["responces"].ToString());
                    bottom = Convert.ToDouble(r["Text_Sent"].ToString());

                    SQL.edit("Insert into URL_Percentage(URL,STOPS,Text_Sent,Percentage) VALUES ('" + r["URL"].ToString() + "'," + r["responces"].ToString() + "," + r["Text_Sent"].ToString() + "," + Math.Round(top / bottom, 3) + ")");
                    index++;
                }

            }


             table = SQL.query("select * from URL_Percentage order by Percentage DESC");
              if (table.TableName != "Error")
              {
                  int index = 0;
                  foreach (DataRow r in table.Rows)
                  {
                      this.URLReportgrid.Rows.Add();
                      this.URLReportgrid.Rows[index].Cells[0].Value = r["URL"].ToString();
                      this.URLReportgrid.Rows[index].Cells[1].Value = r["STOPS"].ToString();
                      this.URLReportgrid.Rows[index].Cells[2].Value = r["Text_Sent"].ToString();
                      this.URLReportgrid.Rows[index].Cells[3].Value = r["Percentage"].ToString();
                      index++;
                  }
              }

            SQL.edit("DELETE FROM URL_Text_Sent");

            SQL.edit("DELETE FROM URL_Responces");
            SQL.edit("DELETE FROM URL_Percentage");
     





        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            LDBSQL SQL = new LDBSQL();
            string area_code;

            DataTable Lead_phone_Nums = SQL.query("Select area_code from Leads where area_code > 347");
            if (Lead_phone_Nums.TableName != "Error")
            {
                foreach (DataRow r in Lead_phone_Nums.Rows)
                {
                    area_code = r["area_code"].ToString();
                    DataTable TZ = SQL.query("Select TimeZones from AreaCodeTZ where AreaCode='" + area_code + "'");
                    if (TZ.TableName != "Error")
                    {
                        if (TZ.Rows.Count != 0)
                        {
                            string TimeZone = TZ.Rows[0]["TimeZones"].ToString();
                            SQL.edit("UPDATE Leads SET TimeZone='" + TimeZone + "' where area_code='" + area_code + "'");
                        }
                    }
                }
            }
        }

        private void ImportStatview1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AvgTimeButton_Click(object sender, EventArgs e)
        {
            this.AvgTimeGridView3.Columns.Clear();
            this.AvgTimeGridView3.Rows.Clear();

            LDBSQL sql = new LDBSQL();
            SMSSQL smssql = new SMSSQL();
            string campquery;
            if (AvgTimeBox1.Text != AvgTimeBox2.Text)
            {
                campquery = "SELECT c.Name,l.FileID,l.Telephone_Number,l.Queuetime FROM leads l INNER JOIN Campaigns c ON l.CampId=c.ID  where l.CampId <> 0 and convert(char(10),QueueTime,101) between '" + AvgTimeBox2.Text + "%'  and '" + AvgTimeBox1.Text + "' order by FileID,c.Name ";

                
            }
            else
            {
                campquery = "SELECT Source,Destination,Time FROM TextHistory where ";
            }

            DataTable table = sql.query(campquery);
            if (table.TableName != "Error")
            {
                if(table.Rows.Count !=0){
                    string insertsql = "";
                    foreach (DataRow r in table.Rows)
                    {
                        
                        insertsql = "INSERT INTO Join_Leads_Camp (Campaign_name,Fileid,PhoneNumber,Time,Time2,span_in_secs) VALUES ('" + r["Name"].ToString() + "','" + r["FileID"].ToString() + "','" + r["Telephone_Number"].ToString() + "','" +  r["Queuetime"].ToString().Substring(r["Queuetime"].ToString().IndexOf(' ')) + "',0,0)";
                        sql.edit(insertsql);
                    }
                }
            }

            campquery = "select Campaign_name,PhoneNumber,Time from Join_Leads_Camp";
            table = sql.query(campquery);
           
            string time = "";
            string time2 = "";
            string texthistory = "";
            if (table.TableName != "Error")
            {
                string updatesql = "";
                if (table.Rows.Count != 0)
                {
                    foreach(DataRow r in table.Rows){
                        texthistory = "select Time from TextHistory WHERE Source='+1"+r["PhoneNumber"].ToString()+"'";
                        DataTable table2 = smssql.query(texthistory);
                        if (table2.TableName != "Error")
                        {
                            if (table2.Rows.Count != 0)
                            {
                                time2 = table2.Rows[0]["Time"].ToString();
                                TimeSpan interval= new TimeSpan(Convert.ToDateTime(time2).Ticks);
                                TimeSpan interval2= new TimeSpan(Convert.ToDateTime(r["Time"].ToString()).Ticks);
                                interval=interval-interval2;
                                updatesql = "UPDATE Join_Leads_Camp SET Time2='" + time2 + "', span_in_secs='"+interval.ToString()+"' where PhoneNumber='" + r["PhoneNumber"].ToString() + "'";
                            }
                        }
                    }
                }
            }
        }

        private void numleads_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            LiveSQL SQL = new LiveSQL();
            this.liveleadgrid.Columns.Clear();
            this.liveleadgrid.Rows.Clear();
         
            DataGridViewColumn URLaddr = new DataGridViewColumn();

            DataGridViewCellStyle style = new DataGridViewCellStyle();

            style.Font = new Font(liveleadgrid.Font, FontStyle.Bold);

            DataGridViewCell Sourcecell1 = new DataGridViewTextBoxCell();
            URLaddr.CellTemplate = Sourcecell1;
            URLaddr.HeaderText = "sourcepc";
            URLaddr.Name = "URL";
            URLaddr.Visible = true;
            URLaddr.Width = 100;
            liveleadgrid.Columns.Add(URLaddr);

 

            DataGridViewColumn Malformed = new DataGridViewColumn();
            DataGridViewCell Malformed2 = new DataGridViewTextBoxCell();
            Malformed.CellTemplate = Malformed2;
            Malformed.HeaderText = "All Responces";
            Malformed.Name = "All Responces";
            Malformed.Visible = true;
            Malformed.Width = 150;
            liveleadgrid.Columns.Add(Malformed);

           
       
            double top = 0;
            double bottom = 0;

            DateTime startdate = Convert.ToDateTime(LiveDate1.Text);
            DateTime Enddate = Convert.ToDateTime(LiveDate2.Text);
            Enddate = Enddate.AddDays(1.0);
            string Total_Recieved = "select distinct sourcepc, count(*) as \"Total Received\" from importlive where received between '" + startdate.ToString("MM/dd/yyyy") + "' and '" + Enddate.ToString("MM/dd/yyyy") + "' group by  sourcepc";
            string Total_Accepted= "select distinct sourcepc, count(*) as \"Total Accepted\" from importlive where received between '" + startdate.ToString("MM/dd/yyyy") + "' and '" + Enddate.ToString("MM/dd/yyyy") + "' and valid = 1 group by  sourcepc";
            
            
            DataTable table = SQL.query(Total_Recieved);


            

            if (table.TableName != "Error")
            {
                if (table.Rows.Count != 0)
                {
                    int Index = this.liveleadgrid.Rows.Add();
                    this.liveleadgrid.Rows[Index].Cells[1].Style.Font = style.Font;
                    this.liveleadgrid.Rows[Index].Cells[1].Value = "Total Received";
                    foreach (DataRow r in table.Rows)
                    {
                        Index=this.liveleadgrid.Rows.Add();
                        this.liveleadgrid.Rows[Index].Cells[0].Style.Font = style.Font;
                        this.liveleadgrid.Rows[Index].Cells[0].Value = r["sourcepc"].ToString();
                        this.liveleadgrid.Rows[Index].Cells[1].Value = r["Total Received"].ToString();
           
                    }
                   
                   
                }
            }

           
            table = SQL.query(Total_Accepted);
            if (table.TableName != "Error")
            {
                if (table.Rows.Count != 0)
                {
                    int Index = this.liveleadgrid.Rows.Add();
                    this.liveleadgrid.Rows[Index].Cells[1].Style.Font = style.Font;
                    this.liveleadgrid.Rows[Index].Cells[1].Value = "Total_Accepted";
                    foreach (DataRow r in table.Rows)
                    {
                        Index = this.liveleadgrid.Rows.Add();
                        this.liveleadgrid.Rows[Index].Cells[0].Style.Font = style.Font;
                        this.liveleadgrid.Rows[Index].Cells[0].Value = r["sourcepc"].ToString();
                        this.liveleadgrid.Rows[Index].Cells[1].Value = r["Total Accepted"].ToString();

                    }
                
                    
                }
            }
           


             
        }

        private void BCRadioButton_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void BCRadioButton_CheckedChanged_1(object sender, EventArgs e)
        {
              LDBSQL SQL = new LDBSQL();
            if (BCRadioButton.Checked)
            {
                SQL.edit("UPDATE Campaigns SET hasBadCarriers= 1 where ID= " + Convert.ToInt32(CampID.Text));
            }
            else
            {
                
                SQL.edit("UPDATE Campaigns SET hasBadCarriers= 0 where ID= " + Convert.ToInt32(CampID.Text));
            }
        }
    
        


       
     
        
    }
    public class ListItem
    {
        public string ID;
        public string Name;

        public ListItem() { }

        public ListItem(string ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
    }

    public struct reportleads
    {
        public string lead_name;
        public int leads_sent;
        public int unique_leads;
        public double opted_out;
    };

    public struct report_structure
    {
        public string Campaign_name;
        public int total_leads_Sent;
        public int total_unique_leads_Sent;
        public int cmp_top;
        public int cmp_bottom;
        public int unique_leads;
        public double total_leads_opt_out;
        public Queue<reportleads> leads;
    };


}
