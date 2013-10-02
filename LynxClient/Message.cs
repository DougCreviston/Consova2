using System;
using System.Linq;
using System.Text;
using SMSLib;



public class Message
{
	public Message()
	{
	}

    SendSMS SMS = new SendSMS();
    public void Send(Object oSMS)
    {
        SendSMS SMS = (SendSMS)oSMS;
        SMS.send();
    }
}
