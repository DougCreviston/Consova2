using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
namespace LynxLib
{
   public  class ATTSMS
    {
        private string _sourceNum = "";
        private string _carrier = "";
        private string _phonenumber = "";
        private string _xmessage = "";
        private string _ticketid = "";
        private  int modem = 1;
       
        public string sourceNum
        {
            get { return _sourceNum; }
            set { _sourceNum = value; }
        }
        public string carrier
        {
            get { return _carrier; }
            set { _carrier = value; }
        }
        public string PhoneNumber
        {
            get { return _phonenumber; }
            set { _phonenumber = value; }
        }
        public string xmessage
        {
            get { return _xmessage; }
            set { _xmessage = value; }
        }
        public string ticketid
        {
            get { return _ticketid; }
            set { _ticketid = value; }
        }

        public int Modem
        {
            get { return modem; }
            set { modem = value; }
        }

        public int send()
        {
            int sent = -1;
            try
            {
                LDBSQL sql = new LDBSQL();
                _xmessage = _xmessage.Replace(" ", "%20");
             
                string url = "http://10.8.8.245:81/sendmsg?user=admin&passwd=smssystem&cat=1&modem=" + modem + "&to=" + _phonenumber + "&text=" + _xmessage;
               
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://208.93.48.10/wmp");
                myRequest.Method = "GET";
                myRequest.KeepAlive = true;

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();
                string myNow = DateTime.Now.ToString("yy/MM/dd");
                _xmessage = _xmessage.Replace("%20", " ");
                _xmessage = Regex.Replace(_xmessage, @"\'", @"''");
                sql.edit("INSERT INTO SF800Gpreview (SourceNumber,DestinationNumber,Date,Time,Message) VALUES ('7205480254','" + _phonenumber + "','" + DateTime.Now.ToString("MM/dd/yy") + "','" + DateTime.Now.ToString("hh:mm tt") + "','" + xmessage + "')");

                 myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();

                System.IO.Stream streamResponse = myHttpWebResponse.GetResponseStream();

                



                // Release the response object resources.
                StreamReader streamRead = new StreamReader(streamResponse);

                Char[] readBuffer = new Char[512];

                // Read from buffer
                int count = streamRead.Read(readBuffer, 0, 512);
                string xmlResult = "";
                while (count > 0)
                {
                    // get string
                    String resultData = new String(readBuffer, 0, count);

                    // Write the data
                    xmlResult = xmlResult + resultData;

                    // Read from buffer
                    count = streamRead.Read(readBuffer, 0, 512);
                }



                // Example #3: Write only some strings in an array to a file.
                string insert = "";

                StringBuilder output = new StringBuilder();
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlResult)))
                {
                    int i = 0;
                    string SenderNumber = "";
                    string Date = "";
                    string Time = "";
                    string message = "";

                    while (i < 2323)
                    {
                        reader.ReadToFollowing("SenderNumber");
                        //reader.MoveToFirstAttribute();
                        SenderNumber = reader.ReadElementContentAsString();
                        SenderNumber = SenderNumber.Substring(1, SenderNumber.Length - 1);
                        reader.ReadToFollowing("Date");
                        //reader.MoveToFirstAttribute();
                        Date = reader.ReadElementContentAsString();

                        reader.ReadToFollowing("Time");
                        //reader.MoveToFirstAttribute();
                        Time = reader.ReadElementContentAsString();



                        reader.ReadToFollowing("Message");
                        reader.MoveToFirstAttribute();
                        message = reader.ReadElementContentAsString();
                        message = message.Replace("%20", " ");
                        message = message.ToLower();
                        SMSSQL smsSQL = new SMSSQL();
                        insert = "INSERT INTO SF800Gpreview (DestinationNumber,Date,Time,Message) VALUES  ('" + "+" + SenderNumber + "','" + Date + "','" + Time + "','" + message + "')";
                        sql.edit(insert);
                        if (message.Contains("stop"))
                        {
                            insert = "INSERT INTO DoNotText (TelephoneNumber,Date,Time,Message) VALUES ('" + "+" + SenderNumber + "','" + Date + "','" + Time + "','" + message + "')";
                            smsSQL.edit(insert);
                        }
                        i++;
                    }

                }

                // Close response

                myHttpWebResponse.Close();





            }
            catch
            {


                // Close response

            }   
                return sent;
            
           
 
        }
        public int query()
        {
            int sent = -1;
            ASCIIEncoding encoding = new ASCIIEncoding();

            // Create the xml document containe
            XmlDocument doc = new XmlDocument();// Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);// Create the root element
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("version", "3.0");
            root.SetAttribute("protocol", "wmp");
            root.SetAttribute("type", "query");
            doc.AppendChild(root);
            XmlElement user = doc.CreateElement("user");
            user.SetAttribute("agent", "XML/SMS/1.0.0");

            XmlElement account = doc.CreateElement("account");
            // account.SetAttribute("id", "000-000-108-11011");
            //account.SetAttribute("password", "KN950NKB");
            account.SetAttribute("id", "000-000-109-30401");
            account.SetAttribute("password", "xtopoly0912");


            XmlElement ticket = doc.CreateElement("ticket");
            ticket.SetAttribute("id", _ticketid);


            root.AppendChild(user);
            root.AppendChild(account);
            root.AppendChild(ticket);

            //Response.Write(doc.OuterXml);
            //Response.End();

            string postData = doc.OuterXml;

            byte[] buffer = encoding.GetBytes(postData);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://smsc-03.openmarket.com/wmp");
            myRequest.Method = "POST";

            // Set the content type to a FORM
            myRequest.ContentType = "urlencoded";

            // Get length of content
            myRequest.ContentLength = buffer.Length;

            // Get request stream
            System.IO.Stream newStream = myRequest.GetRequestStream();

            // Send the data.
            newStream.Write(buffer, 0, buffer.Length);

            // Close stream
            newStream.Close();



            // Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();

            // Display the contents of the page to the console.
            System.IO.Stream streamResponse = myHttpWebResponse.GetResponseStream();

            // Get stream object
            StreamReader streamRead = new StreamReader(streamResponse);

            Char[] readBuffer = new Char[256];

            // Read from buffer
            int count = streamRead.Read(readBuffer, 0, 256);
            string xmlResult = "";
            while (count > 0)
            {
                // get string
                String resultData = new String(readBuffer, 0, count);

                // Write the data
                xmlResult = xmlResult + resultData;

                // Read from buffer
                count = streamRead.Read(readBuffer, 0, 256);
            }
            //Response.Write(xmlResult);
            StringBuilder output = new StringBuilder();
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlResult)))
            {

                reader.ReadToFollowing("error");
                reader.MoveToFirstAttribute();
                string code = reader.Value;
                reader.MoveToNextAttribute();
                string desc = reader.Value;
                reader.ReadToFollowing("status");
                reader.MoveToFirstAttribute();
                string stcode = reader.Value;
                reader.MoveToNextAttribute();
                string stdesc = reader.Value;
                SMSSQL SQL = new SMSSQL();
                sent = SQL.edit("UPDATE TextHistory SET ErrorCode = '" + code + "',ErrorDesc = '" + desc + "', StatusCode = '" + stcode + "', StatusDesc = '" + stdesc + "' WHERE TicketID = '" + _ticketid + "'");




            }
            // Release the response object resources.
            streamRead.Close();
            streamResponse.Close();

            // Close response
            myHttpWebResponse.Close();
            return sent;
        }
        public int preview()
        {
            int sent = -1;
            ASCIIEncoding encoding = new ASCIIEncoding();

            // Create the xml document containe
            XmlDocument doc = new XmlDocument();// Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);// Create the root element
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("version", "3.0");
            root.SetAttribute("protocol", "wmp");
            root.SetAttribute("type", "preview");
            doc.AppendChild(root);
            XmlElement user = doc.CreateElement("user");
            user.SetAttribute("agent", "XML/SMS/1.0.0");

            XmlElement account = doc.CreateElement("account");
            //account.SetAttribute("id", "000-000-108-11011");
            //account.SetAttribute("password", "KN950NKB");
            account.SetAttribute("id", "000-000-109-30401");
            account.SetAttribute("password", "xtopoly0912");

            root.AppendChild(user);
            root.AppendChild(account);



            XmlElement destination = doc.CreateElement("destination");

            destination.SetAttribute("ton", "0");
            destination.SetAttribute("address", _phonenumber);
            root.AppendChild(destination);


            //Response.Write(doc.OuterXml);
            //Response.End();

            string postData = doc.OuterXml;

            byte[] buffer = encoding.GetBytes(postData);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://smsc-03.openmarket.com/wmp");
            myRequest.Method = "POST";

            // Set the content type to a FORM
            myRequest.ContentType = "text/xml";

            // Get length of content
            myRequest.ContentLength = buffer.Length;

            // Get request stream
            System.IO.Stream newStream = myRequest.GetRequestStream();

            // Send the data.
            newStream.Write(buffer, 0, buffer.Length);

            // Close stream
            newStream.Close();



            // Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();

            // Display the contents of the page to the console.
            System.IO.Stream streamResponse = myHttpWebResponse.GetResponseStream();

            // Get stream object
            StreamReader streamRead = new StreamReader(streamResponse);

            Char[] readBuffer = new Char[256];

            // Read from buffer
            int count = streamRead.Read(readBuffer, 0, 256);
            string xmlResult = "";
            while (count > 0)
            {
                // get string
                String resultData = new String(readBuffer, 0, count);

                // Write the data
                xmlResult = xmlResult + resultData;

                // Read from buffer
                count = streamRead.Read(readBuffer, 0, 256);
            }
            //Response.Write(xmlResult);
            StringBuilder output = new StringBuilder();
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlResult)))
            {

                reader.ReadToFollowing("error");
                reader.MoveToFirstAttribute();
                string code = reader.Value;
                reader.MoveToNextAttribute();
                string desc = reader.Value;
                reader.ReadToFollowing("location");
                reader.MoveToAttribute("region");
                string region = reader.Value;
                reader.MoveToAttribute("city");
                string city = reader.Value;
                reader.MoveToAttribute("postal_code");
                string zipcode = reader.Value;
                reader.MoveToAttribute("timezone_min");
                string TZ = reader.Value;
                reader.MoveToAttribute("estimated_latitude");
                string est_lat = reader.Value;
                reader.MoveToAttribute("estimated_longitude");
                string est_long = reader.Value;
                reader.ReadToFollowing("operator");
                reader.MoveToAttribute("id");
                string carrierid = reader.Value;
                reader.MoveToAttribute("name");
                string carriername = reader.Value;
                reader.MoveToAttribute("text_length");
                string text_length = reader.Value;
                reader.MoveToAttribute("smart_messaging");
                string smart_messaging = reader.Value;
                reader.MoveToAttribute("price");
                string price = reader.Value;
                reader.MoveToAttribute("price_currency");
                string price_currency = reader.Value;

                string id = reader.Value;
                SMSSQL SQL = new SMSSQL();
                sent = SQL.edit("INSERT INTO TextHistory (ErrorCode,ErrorDesc,Destination,DTON,Carrier,Date,Time,XML) VALUES ('" + code + "','" + desc + "','" + _phonenumber + "','0','" + carrierid + "',Convert(char(10),getDate(),110),'" + DateTime.Now.TimeOfDay.ToString() + "','" + postData + "')");

                sent = SQL.edit("INSERT INTO preview (TelephoneNumber,date,time,CarrierID,CarrierName,text_length,smart_messaging,price,price_currency,City,region,postal_code,timezone,est_lat,est_long,xml) VALUES ('" + _phonenumber + "',Convert(char(10),getDate(),110),'" + DateTime.Now.TimeOfDay.ToString() + "','" + carrierid + "','" + carriername + "','" + text_length + "','" + smart_messaging + "','" + price + "','" + price_currency + "','" + city + "','" + region + "','','" + TZ + "','" + est_lat + "','" + est_long + "','" + xmlResult + "')");
                //sent = SQL.edit("INSERT INTO preview (TelephoneNumber,date,time,CarrierID,CarrierName,text_length,smart_messaging,price,price_currency,City,region,postal_code,timezone,est_lat,est_long,xml) VALUES ('" + _phonenumber + "',Convert(char(10),getDate(),110),'" + DateTime.Now.TimeOfDay.ToString() + "','" + carrierid + "','','','','','','','','','','','','" + xmlResult + "')");




            }
            // Release the response object resources.
            streamRead.Close();
            streamResponse.Close();

            // Close response
            myHttpWebResponse.Close();
            return sent;
        }
        public int LRNpreview()
        {
            int sent = -1;
            ASCIIEncoding encoding = new ASCIIEncoding();

            // Create the xml document containe
            XmlDocument doc = new XmlDocument();// Create the XML Declaration, and append it to XML document
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", null, null);
            doc.AppendChild(dec);// Create the root element
            XmlElement root = doc.CreateElement("request");
            root.SetAttribute("version", "3.0");
            root.SetAttribute("protocol", "wmp");
            root.SetAttribute("type", "preview");
            doc.AppendChild(root);
            XmlElement user = doc.CreateElement("user");
            user.SetAttribute("agent", "XML/SMS/1.0.0");

            XmlElement account = doc.CreateElement("account");
            //account.SetAttribute("id", "000-000-108-11011");
            //account.SetAttribute("password", "KN950NKB");
            account.SetAttribute("id", "000-000-109-30401");
            account.SetAttribute("password", "xtopoly0912");


            root.AppendChild(user);
            root.AppendChild(account);



            XmlElement destination = doc.CreateElement("destination");

            destination.SetAttribute("ton", "0");
            destination.SetAttribute("address", _phonenumber);
            root.AppendChild(destination);


            //Response.Write(doc.OuterXml);
            //Response.End();

            string postData = doc.OuterXml;

            byte[] buffer = encoding.GetBytes(postData);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("http://smsc-03.openmarket.com/wmp");
            myRequest.Method = "POST";

            // Set the content type to a FORM
            myRequest.ContentType = "text/xml";

            // Get length of content
            myRequest.ContentLength = buffer.Length;

            // Get request stream
            System.IO.Stream newStream = myRequest.GetRequestStream();

            // Send the data.
            newStream.Write(buffer, 0, buffer.Length);

            // Close stream
            newStream.Close();



            // Assign the response object of 'HttpWebRequest' to a 'HttpWebResponse' variable.
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();

            // Display the contents of the page to the console.
            System.IO.Stream streamResponse = myHttpWebResponse.GetResponseStream();

            // Get stream object
            StreamReader streamRead = new StreamReader(streamResponse);

            Char[] readBuffer = new Char[256];

            // Read from buffer
            int count = streamRead.Read(readBuffer, 0, 256);
            string xmlResult = "";
            while (count > 0)
            {
                // get string
                String resultData = new String(readBuffer, 0, count);

                // Write the data
                xmlResult = xmlResult + resultData;

                // Read from buffer
                count = streamRead.Read(readBuffer, 0, 256);
            }
            //Response.Write(xmlResult);
            StringBuilder output = new StringBuilder();
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlResult)))
            {

                reader.ReadToFollowing("error");
                reader.MoveToFirstAttribute();
                string code = reader.Value;
                reader.MoveToNextAttribute();
                string desc = reader.Value;
                reader.ReadToFollowing("location");
                reader.MoveToAttribute("region");
                string region = reader.Value;
                reader.MoveToAttribute("city");
                string city = reader.Value;
                reader.MoveToAttribute("postal_code");
                string zipcode = reader.Value;
                reader.MoveToAttribute("timezone_min");
                string TZ = reader.Value;
                reader.MoveToAttribute("estimated_latitude");
                string est_lat = reader.Value;
                reader.MoveToAttribute("estimated_longitude");
                string est_long = reader.Value;
                reader.ReadToFollowing("operator");
                reader.MoveToAttribute("id");
                string carrierid = reader.Value;
                reader.MoveToAttribute("name");
                string carriername = reader.Value;
                reader.MoveToAttribute("text_length");
                string text_length = reader.Value;
                reader.MoveToAttribute("smart_messaging");
                string smart_messaging = reader.Value;
                reader.MoveToAttribute("price");
                string price = reader.Value;
                reader.MoveToAttribute("price_currency");
                string price_currency = reader.Value;

                string id = reader.Value;
                SMSSQL SQL = new SMSSQL();
                sent = SQL.edit("INSERT INTO TextHistory (ErrorCode,ErrorDesc,Destination,DTON,Carrier,Date,Time,XML) VALUES ('" + code + "','" + desc + "','" + _phonenumber + "','0','" + carrierid + "',Convert(char(10),getDate(),110),'" + DateTime.Now.TimeOfDay.ToString() + "','" + postData + "')");

                sent = SQL.edit("INSERT INTO preview (TelephoneNumber,date,time,CarrierID,CarrierName,text_length,smart_messaging,price,price_currency,City,region,postal_code,timezone,est_lat,est_long,xml) VALUES ('" + _phonenumber + "',Convert(char(10),getDate(),110),'" + DateTime.Now.TimeOfDay.ToString() + "','" + carrierid + "','" + carriername + "','" + text_length + "','" + smart_messaging + "','" + price + "','" + price_currency + "','" + city + "','" + region + "','','" + TZ + "','" + est_lat + "','" + est_long + "','" + xmlResult + "')");
                //sent = SQL.edit("INSERT INTO preview (TelephoneNumber,date,time,CarrierID,CarrierName,text_length,smart_messaging,price,price_currency,City,region,postal_code,timezone,est_lat,est_long,xml) VALUES ('" + _phonenumber + "',Convert(char(10),getDate(),110),'" + DateTime.Now.TimeOfDay.ToString() + "','" + carrierid + "','','','','','','','','','','','','" + xmlResult + "')");




            }
            // Release the response object resources.
            streamRead.Close();
            streamResponse.Close();

            // Close response
            myHttpWebResponse.Close();
            return sent;
        }
        public void receive(string xml)
        {
            StringBuilder sb = new StringBuilder(xml);
            // Selectively allow  and <i>
            sb.Replace("&lt;request", "<request");
            sb.Replace("&lt;source", "<source");
            sb.Replace("&lt;destination", "<destination");
            sb.Replace("&lt;ticket", "<ticket");
            sb.Replace("&lt;message", "<message");
            sb.Replace("&lt;account", "<account");
            sb.Replace("&lt;option", "<option");
            sb.Replace("&lt;?xml", "<?xml");
            sb.Replace("&lt;/request", "</request");
            sb.Replace("&gt;", ">");
            sb.Replace("&quot;", "\"");
            sb.Replace("'", "''");
            //Response.Write(sb);
            //Response.End();

            // Create an XmlReader
            using (XmlReader xreader = XmlReader.Create(new StringReader(sb.ToString())))
            {

                xreader.ReadToFollowing("destination");
                xreader.MoveToFirstAttribute();
                string dton = xreader.Value;
                xreader.MoveToNextAttribute();
                string daddress = xreader.Value;
                xreader.ReadToFollowing("source");
                xreader.MoveToFirstAttribute();
                string carrier = xreader.Value;
                xreader.MoveToNextAttribute();
                string ston = xreader.Value;
                xreader.MoveToNextAttribute();
                string saddress = xreader.Value;
                xreader.ReadToFollowing("message");
                xreader.MoveToFirstAttribute();
                xreader.MoveToNextAttribute();
                string message = xreader.Value;
                byte[] bytes = StringToByteArray(message);
                Encoding UTF7 = Encoding.UTF7;
                message = UTF7.GetString(bytes);
                xreader.ReadToFollowing("ticket");
                xreader.MoveToFirstAttribute();
                string ticketid = xreader.Value;
                SMSSQL SQL = new SMSSQL();
                SQL.edit("INSERT INTO TextHistory (TicketID,Source,STON,Destination,DTON,Carrier,Date,Time,Message,XML) VALUES ('" + ticketid + "','" + saddress + "','" + ston + "','" + daddress + "','" + dton + "','" + carrier + "',Convert(char(10),getDate(),110),'" + DateTime.Now.TimeOfDay + "','" + message + "','" + sb.ToString() + "')");
                SMS newMSG = new SMS();
                newMSG.carrier = carrier;
                newMSG.PhoneNumber = saddress;
                if (message.ToUpper().Contains("STOP"))
                {
                    newMSG.xmessage = "You have unsubscribed from CCI alerts & will no longer receive msgs or charges. Questions? 75612@MyCCIOnline.com or 1-800-851-8934. Msg&data rates may apply";
                    SQL.edit("INSERT INTO DoNotText (TelephoneNumber,Date,Time,Message) VALUES ('" + saddress + "',Convert(char(10),getDate(),110),'" + DateTime.Now.TimeOfDay.ToString() + "','" + message.Replace("'", "''") + "')");
                }
                else if (message.ToUpper().Contains("HELP"))
                {
                    newMSG.xmessage = "CCI alerts: Need help? 75612@MyCCIOnline.com or 1-800-851-8934. Reply STOP to cancel. Msg&data rates may apply";
                    SQL.edit("INSERT INTO HelpText (TelephoneNumber,Date,Time,Message) VALUES ('" + saddress + "',Convert(char(10),getDate(),110),'" + DateTime.Now.TimeOfDay.ToString() + "','" + message.Replace("'", "''") + "')");
                }
                else if (message.ToUpper().Contains("YES"))
                {
                    newMSG.xmessage = "Thank you for subscribing to CCI alerts (8 msgs/mo). Reply HELP for help, STOP to cancel. Msg&data rates may apply";
                    SQL.edit("INSERT INTO ConfirmText (TelephoneNumber,Date,Time,Message) VALUES ('" + saddress + "',Convert(char(10),getDate(),110),'" + DateTime.Now.TimeOfDay.ToString() + "','" + message.Replace("'", "''") + "')");
                }
                newMSG.send();
            }
        }
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
