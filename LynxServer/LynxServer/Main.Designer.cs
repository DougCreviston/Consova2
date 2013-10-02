namespace LynxServer
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.console = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.consoleTab = new System.Windows.Forms.TabPage();
            this.taskTab = new System.Windows.Forms.TabPage();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.setSave = new System.Windows.Forms.Button();
            this.verbose = new System.Windows.Forms.ComboBox();
            this.setVerbose = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.setPort = new System.Windows.Forms.Label();
            this.IPADDR = new System.Windows.Forms.TextBox();
            this.setIP = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.counter = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.consoleTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(0, 0);
            this.console.Name = "console";
            this.console.ReadOnly = true;
            this.console.Size = new System.Drawing.Size(458, 256);
            this.console.TabIndex = 0;
            this.console.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(462, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.Start);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.Stop);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.counter});
            this.statusStrip1.Location = new System.Drawing.Point(0, 308);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(462, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.consoleTab);
            this.tabControl.Controls.Add(this.taskTab);
            this.tabControl.Controls.Add(this.settingsTab);
            this.tabControl.Location = new System.Drawing.Point(0, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(462, 278);
            this.tabControl.TabIndex = 3;
            // 
            // consoleTab
            // 
            this.consoleTab.Controls.Add(this.console);
            this.consoleTab.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.consoleTab.Location = new System.Drawing.Point(4, 22);
            this.consoleTab.Name = "consoleTab";
            this.consoleTab.Padding = new System.Windows.Forms.Padding(3);
            this.consoleTab.Size = new System.Drawing.Size(454, 252);
            this.consoleTab.TabIndex = 0;
            this.consoleTab.Text = "Console";
            this.consoleTab.UseVisualStyleBackColor = true;
            // 
            // taskTab
            // 
            this.taskTab.Location = new System.Drawing.Point(4, 22);
            this.taskTab.Name = "taskTab";
            this.taskTab.Padding = new System.Windows.Forms.Padding(3);
            this.taskTab.Size = new System.Drawing.Size(454, 252);
            this.taskTab.TabIndex = 1;
            this.taskTab.Text = "Tasks";
            this.taskTab.UseVisualStyleBackColor = true;
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.setSave);
            this.settingsTab.Controls.Add(this.verbose);
            this.settingsTab.Controls.Add(this.setVerbose);
            this.settingsTab.Controls.Add(this.port);
            this.settingsTab.Controls.Add(this.setPort);
            this.settingsTab.Controls.Add(this.IPADDR);
            this.settingsTab.Controls.Add(this.setIP);
            this.settingsTab.Location = new System.Drawing.Point(4, 22);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Size = new System.Drawing.Size(454, 252);
            this.settingsTab.TabIndex = 2;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            // 
            // setSave
            // 
            this.setSave.Location = new System.Drawing.Point(371, 218);
            this.setSave.Name = "setSave";
            this.setSave.Size = new System.Drawing.Size(75, 23);
            this.setSave.TabIndex = 6;
            this.setSave.Text = "Save";
            this.setSave.UseVisualStyleBackColor = true;
            this.setSave.Click += new System.EventHandler(this.saveSettings);
            // 
            // verbose
            // 
            this.verbose.FormattingEnabled = true;
            this.verbose.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4"});
            this.verbose.Location = new System.Drawing.Point(0, 64);
            this.verbose.Name = "verbose";
            this.verbose.Size = new System.Drawing.Size(121, 21);
            this.verbose.TabIndex = 5;
            // 
            // setVerbose
            // 
            this.setVerbose.AutoSize = true;
            this.setVerbose.Location = new System.Drawing.Point(0, 48);
            this.setVerbose.Name = "setVerbose";
            this.setVerbose.Size = new System.Drawing.Size(75, 13);
            this.setVerbose.TabIndex = 4;
            this.setVerbose.Text = "Verbose Level";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(117, 20);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(39, 20);
            this.port.TabIndex = 3;
            // 
            // setPort
            // 
            this.setPort.AutoSize = true;
            this.setPort.Location = new System.Drawing.Point(114, 4);
            this.setPort.Name = "setPort";
            this.setPort.Size = new System.Drawing.Size(26, 13);
            this.setPort.TabIndex = 2;
            this.setPort.Text = "Port";
            // 
            // IPADDR
            // 
            this.IPADDR.Location = new System.Drawing.Point(0, 21);
            this.IPADDR.Name = "IPADDR";
            this.IPADDR.Size = new System.Drawing.Size(100, 20);
            this.IPADDR.TabIndex = 1;
            // 
            // setIP
            // 
            this.setIP.AutoSize = true;
            this.setIP.Location = new System.Drawing.Point(4, 4);
            this.setIP.Name = "setIP";
            this.setIP.Size = new System.Drawing.Size(58, 13);
            this.setIP.TabIndex = 0;
            this.setIP.Text = "IP Address";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick_1);
            // 
            // counter
            // 
            this.counter.Name = "counter";
            this.counter.Size = new System.Drawing.Size(0, 17);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(462, 330);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Lynx Server";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.consoleTab.ResumeLayout(false);
            this.settingsTab.ResumeLayout(false);
            this.settingsTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox console;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage consoleTab;
        private System.Windows.Forms.TabPage taskTab;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.ComboBox verbose;
        private System.Windows.Forms.Label setVerbose;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Label setPort;
        private System.Windows.Forms.TextBox IPADDR;
        private System.Windows.Forms.Label setIP;
        private System.Windows.Forms.Button setSave;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripStatusLabel counter;
    }
}

