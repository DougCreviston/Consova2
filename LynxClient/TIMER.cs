﻿using System;
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
using LynxLib;

public class myTIMER
{
    private DateTime datetime;

	public myTIMER()
	{ 
        datetime = DateTime.Now;
        
	}

   
    public String toString(){
        return datetime.toString("HH:mm");
    }

}
