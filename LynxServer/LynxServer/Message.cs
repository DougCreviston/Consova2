using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using LynxLib;

namespace LynxServer
{
    class Message
    {

        SMS SMS = new SMS();
        public void Send(Object oSMS){
            SMS SMS = (SMS)oSMS;
            SMS.send();
            
        }
        

    }
}
