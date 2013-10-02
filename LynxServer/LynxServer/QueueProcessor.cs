using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;
using LynxLib;


namespace LynxServer
{
   public class QueueProcessor
    {
        SMS newMessage = new SMS();
        Queue tQueue = new Queue();
        public bool QueueSMS(int compID, string number, string msg, string carrier, string sourcenum)
        {
            try
            {
                newMessage.sourceNum = sourcenum;
                newMessage.ticketid = compID.ToString();
                newMessage.PhoneNumber = number;
                newMessage.xmessage = msg;
                newMessage.carrier = carrier;
                tQueue.Enqueue(newMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public SMS getSMS()
        {
            try
            {
                return (SMS)tQueue.Dequeue();
            }
            catch
            {
                SMS ErrorMsg = new SMS();
                return ErrorMsg;
            }
        }

        public int Size()
        {
           
                return tQueue.Count;
               
           
        }

        public bool ClearQueue()
        {
            try
            {
                tQueue.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
