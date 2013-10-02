using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading;
using System.Text.RegularExpressions;
using LynxLib;
using SMSLib;


namespace LynxClient
{

    struct myFields
    {
        public string field_name;
        public bool mapped;
        public bool seen;
        public string value;
        public int ID;
    };

    class ImportProcessor
    {
        string command = "";
        Main Parent;
        Queue<string> sql_statements;

        public ImportProcessor(string cmd, Main p)
        {
            this.command = cmd;
            this.Parent = p;
            sql_statements = new Queue<string>();
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
                    case "Import":
                        return Import();
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

        private void DetermineTZ()
        {
            LDBSQL SQL = new LDBSQL();
            string area_code;

            DataTable Lead_phone_Nums = SQL.query("Select area_code from Leads  where CampId=0 order by area_code");
            if(Lead_phone_Nums.TableName != "Error"){
                foreach(DataRow r in Lead_phone_Nums.Rows){
                    area_code = r["area_code"].ToString();
                    DataTable TZ = SQL.query("Select TimeZones from AreaCodeTZ where AreaCode='" + area_code + "'");
                    if (TZ.TableName != "Error")
                    {
                        string TimeZone = TZ.Rows[0]["TimeZones"].ToString();
                        SQL.edit("UPDATE Leads SET TimeZone='" + TimeZone + "' where area_code='" + area_code + "'");
                    }
                }
            }
    

        }


      

        
        private bool Import()
        {
            
            string filepath = Parent.pGetImpFilePath();
            string telephone_pattern=@"^(\(?[0-9]{3}\)?)?\-?[0-9]{3}\-?[0-9]{4}$";
            string zip_code = @"^\d{5}(-\d{4})?$";
            string IP_addr = @"^([0-2]?[0-5]?[0-5]\.){3}[0-2]?[0-5]?[0-5]$";
            CsvFileReader Reader = new CsvFileReader(filepath);
            int impOrigTotal = 0;
            int total_num_of_columns = 0;
            int malformed = 0;
            int bad_phone_nums = 0;
            int Previous = 0;
            int impfailure=0;
            int bad_carriers = 0;
            int no_carriers = 0;
            int impAddTotal = 0;
            int DoNotText = 0;
            int Deactivates = 0;
            int columns_read = 0;
            string CampId = "0";
            int MsgID = 0;
            int FileID = 0;
            int mapped_columns = 1;
            int coloumn = 2;
            bool first_row = true;
            bool flag = true;
            DataTable csv = new DataTable();

            myFields[] fields = { new myFields {  field_name="Full_Name",mapped=false,seen=false,value="", ID=-1 },
                                  new myFields{ field_name="First_Name",mapped=false,seen=false,value="", ID=-1},
                                  new myFields{ field_name="Last_Name",mapped=false,seen=false,value="", ID=-1},
                                  new myFields{ field_name="Address_One",mapped=false,seen=false,value="", ID=-1},
                                  new myFields{ field_name="Address_Two",mapped=false,seen=false,value="", ID=-1},
                                  new myFields{ field_name="City",mapped=false,seen=false,value="", ID=-1},
                                  new myFields{ field_name="State",mapped=false,seen=false,value="", ID=-1},
                                  new myFields{ field_name="Zip_Code",mapped=false,seen=false,value="", ID=-1},
                                  new myFields{ field_name="Telephone_Number",mapped=false,seen=false,value="", ID=-1},
                                  new myFields{ field_name="IP_Address",mapped=false,seen=false,value="", ID=-1},
                                  new myFields{ field_name="URL",mapped=false,seen=false,value="", ID=-1},
                                  new myFields{ field_name="Opt_in_Date",mapped=false,seen=false,value="", ID=-1}
                                };


            if ((Parent.pGetImpMapFullName().Name.CompareTo("N/A") != 0))
            {
                fields[0].ID= Convert.ToInt32(Parent.pGetImpMapFullName().ID);
                fields[0].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapFirstName().Name.CompareTo("N/A") != 0))
            {
                fields[1].ID = Convert.ToInt32(Parent.pGetImpMapFirstName().ID);
                fields[1].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapLastName().Name.CompareTo("N/A") != 0))
            {
                fields[2].ID = Convert.ToInt32(Parent.pGetImpMapLastName().ID);
                fields[2].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapAddrOne().Name.CompareTo("N/A") != 0))
            {
                fields[3].ID = Convert.ToInt32(Parent.pGetImpMapAddrOne().ID);
                fields[3].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapAddrTwo().Name.CompareTo("N/A") != 0))
            {
                fields[4].ID = Convert.ToInt32(Parent.pGetImpMapAddrTwo().ID);
                fields[4].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapCity().Name.CompareTo("N/A") != 0))
            {
                fields[5].ID = Convert.ToInt32(Parent.pGetImpMapCity().ID);
                fields[5].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapState().Name.CompareTo("N/A") != 0))
            {
                fields[6].ID = Convert.ToInt32(Parent.pGetImpMapState().ID);
                fields[6].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapZipCode().Name.CompareTo("N/A") != 0))
            {
                fields[7].ID = Convert.ToInt32(Parent.pGetImpMapZipCode().ID);
                fields[7].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapPhone().Name.CompareTo("N/A") != 0))
            {
                fields[8].ID = Convert.ToInt32(Parent.pGetImpMapPhone().ID);
                fields[8].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapIPAddr().Name.CompareTo("N/A") != 0))
            {
                fields[9].ID = Convert.ToInt32(Parent.pGetImpMapIPAddr().ID);
                fields[9].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapURL().Name.CompareTo("N/A") != 0))
            {
                fields[10].ID = Convert.ToInt32(Parent.pGetImpMapURL().ID);
                fields[10].mapped = true;
                mapped_columns++;
            }
            if ((Parent.pGetImpMapOptIN().Name.CompareTo("N/A") != 0))
            {
                fields[11].ID = Convert.ToInt32(Parent.pGetImpMapOptIN().ID);
                fields[11].mapped = true;
                mapped_columns++;
            }


          
            string TimeZone = "";
            int statedelete = 0;
            CsvRow Row = new CsvRow();
            SMSSQL smsSQL2 = new SMSSQL();
            LDBSQL SQL = new LDBSQL();
            string returned="";
            mapped_columns=0;
            string query1 = "";
            string notImportedquery = "";
            while (Reader.ReadRow(Row))
            {
                
                for (int index = 0; index < fields.Length; index++)
                {
                    if (fields[index].mapped && !fields[index].seen)
                    {
                        fields[index].value = Row[fields[index].ID].ToString();
                        fields[index].seen = true;
                      //  mapped_columns++;
                      
                    }
                }

                
               
                impOrigTotal++;
                //Parent.setimpOrigTotal(impOrigTotal.ToString());
                Parent.pSetimpOrigTotal();

                CampId = "0";

                if (coloumn == 3)
                {
                    MsgID = 0;
                }
                else if (coloumn == 4)
                {
                    FileID = Convert.ToInt32(Parent.pGetimpMapSourceID());
                }
                int z = 0;
              

                 try
                {
                   
                    columns_read = 0;
                    string values = "";
                    string query = "INSERT INTO Leads (CampId,MsgID,FileID,";
                    for (int index = 0; index < fields.Length; index++)
                    {
                        if (fields[index].mapped && fields[index].seen)
                        {
                           
                             //   query = query + fields[index].field_name + ") values ( " + Int32.Parse(CampId) + "," + 0 + "," + Convert.ToInt32(Parent.pGetimpMapSourceID()) + ",'";
                           
                            
                                query = query + fields[index].field_name + ",";
                         
                        }

                    }
                    if ((query.Length > 0))
                    {
                        query = query.Substring(0, query.Length - 1);
                    }
                    query = query + ",DateLoaded,TimeZone) values ( " + Int32.Parse(CampId) + "," + 0 + "," + Convert.ToInt32(Parent.pGetimpMapSourceID()) + ",'";
                    for (int index = 0; index < fields.Length; index++)
                    { 
                        if (fields[index].mapped)
                            {
                           //   if (index == mapped_columns)
                             // {
                              //  values = values + ",'" + fields[index].value + "')";
                             // }
                              //else
                                // {
                           
                                if (fields[index].field_name.CompareTo("Telephone_Number") != 0)
                                {
                                    values = values + fields[index].value + "','";
                                }
                                else
                                {
                                    SMSSQL smsSQL = new SMSSQL();


                                    if (fields[index].value.Length != 10)
                                    {
                                        bad_phone_nums++;
                                        //Parent.setimpInvalid(bad_phone_nums.ToString());
                                        Parent.WriteToimpImportConsole("Invalid Phone Number" + fields[index].value.ToString() + "\r\n", 1);
                                        Parent.pSetimpInvalid();
                                        returned="Bad Phone Number";
                                        notImportedquery = "INSERT INTO NotImported (FileID,FirstName,LastName,PhoneNumber,AddressOne,AddressTwo,City,State,ZipCode,IPAddress,URL,DateReceived,Returned) VALUES (" + Convert.ToInt32(Parent.pGetimpMapSourceID()) + ",'" + fields[1].value + "','" + fields[2].value + "','" + fields[8].value + "','" + fields[3].value + "','" + fields[4].value + "','" + fields[5].value + "','" + fields[6].value + "','" + fields[7].value + "','" + fields[9].value + "','" + fields[10].value + "', getDate() ,'" + returned + "')";
                                        SQL.edit(notImportedquery);
                                        flag = false;
                                    }
                                    else
                                    {

                                        DataTable Record = smsSQL.query("SELECT ID From DoNotText where TelephoneNumber='" + "+1" + fields[index].value + "'");
                                        int x = Record.Rows.Count;
                                        if (x == 0)
                                        {
                                            SendSMS SMS = new SendSMS();


                                            DataTable record1 = smsSQL2.query("SELECT * From Preview where TelephoneNumber='" + "+1" + fields[index].value + "'");

                                            if (record1.Rows.Count == 0)
                                            {
                                                SMS.sourceNum = "75612";
                                                SMS.PhoneNumber = "+1" + fields[index].value;
                                                if (SMS.PhoneNumber.Length == 12)
                                                {
                                                    
                                                    if (SMS.preview() == 1)
                                                    {
                                                        record1 = smsSQL2.query("SELECT * From Preview where TelephoneNumber='" + SMS.PhoneNumber + "'");
                                                        int carrier = 0;
                                                        foreach (DataRow r in record1.Rows)
                                                        {
                                                            if(r["CarrierID"].ToString() != ""){

                                                                carrier = Convert.ToInt32(r["CarrierID"].ToString());
                                                             }else{
                                                                  no_carriers++;
                                                                  Parent.pSetimpNCarr();
                                                                  returned="No Carrier";
                                                                  notImportedquery = "INSERT INTO NotImported (FileID,FirstName,LastName,PhoneNumber,AddressOne,AddressTwo,City,State,ZipCode,IPAddress,URL,DateReceived,Returned) VALUES (" + Convert.ToInt32(Parent.pGetimpMapSourceID()) + ",'" + fields[1].value + "','" + fields[2].value + "','" + fields[8].value + "','" + fields[3].value + "','" + fields[4].value + "','" + fields[5].value + "','" + fields[6].value + "','" + fields[7].value + "','" + fields[9].value + "','" + fields[10].value + "', getDate() ,'" + returned + "')";
                                                                  SQL.edit(notImportedquery);
                                                                flag = false;
                                                             }
                                                        }
                                                        if (carrier != 0)
                                                        {
                                                            if ((carrier != 516) && (carrier != 535) && (carrier != 622))
                                                            {
                                                                record1 = smsSQL2.query("SELECT * From Deactivates where Phone=" + fields[index].value + " and CarrierID=" + carrier);

                                                                if (record1.Rows.Count == 0)
                                                                {
                                                                    DataTable TZ = SQL.query("Select TimeZones from AreaCodeTZ where AreaCode='" + fields[index].value.Substring(0, 3) + "'");
                                                                    if (TZ.TableName != "Error")
                                                                    {
                                                                        if (TZ.Rows.Count == 0)
                                                                        {
                                                                            TimeZone = TZ.Rows[0]["TimeZones"].ToString();
                                                                        }
                                                                    }
                                                                    values = values + fields[index].value + "','";
                                                                }
                                                                else
                                                                {
                                                                    Deactivates++;
                                                                    Parent.WriteToimpImportConsole("Lead Add Failed: " + "Deactive", 1);
                                                                    Parent.pSetimpDeactive();
                                                                    returned = "Deactive";
                                                                    notImportedquery = "INSERT INTO NotImported (FileID,FirstName,LastName,PhoneNumber,AddressOne,AddressTwo,City,State,ZipCode,IPAddress,URL,DateReceived,Returned) VALUES (" + Convert.ToInt32(Parent.pGetimpMapSourceID()) + ",'" + fields[1].value + "','" + fields[2].value + "','" + fields[8].value + "','" + fields[3].value + "','" + fields[4].value + "','" + fields[5].value + "','" + fields[6].value + "','" + fields[7].value + "','" + fields[9].value + "','" + fields[10].value + "', getDate() ,'" + returned + "')";
                                                                    SQL.edit(notImportedquery);
                                                                    flag = false;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                

                                                                      bad_carriers++;
                                                                //Parent.setimpBCarr(bad_carriers.ToString());
                                                                Parent.pSetimpBCarr();
                                                                flag = false;
                                                                returned="Bad Carriers";
                                                                notImportedquery = "INSERT INTO Leads_Bad_Carriers(CampId,MsgID,FileID,Full_Name,First_Name,Last_Name,Address_One,Address_Two,City,State,Zip_Code,Telephone_Number,IP_Address,URL,Opt_in_Date,QueueTime,DateLoaded,TimeZone) VALUES(0,0,32 ,'" + fields[0].value + "','" + fields[1].value + "','" + fields[2].value + "','" + fields[3].value + "','" + fields[4].value + "','" + fields[5].value + "','" + fields[6].value + "','" + fields[7].value + "','" + fields[8].value + "','" + fields[9].value + "','" + fields[10].value + "','"+fields[11].value+"',getDate(),getDate(),NULL)";
                                                                SQL.edit(notImportedquery);

                                                            }


                                                        }
                                                        else
                                                        {
                                                            
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Previous++;
                                                Parent.WriteToimpImportConsole("Lead Add Failed: ", 1);
                                              /*  string delete = "DELETE FROM preview where TelephoneNumber='" + "+1" + fields[index].value + "'";
                                                if (smsSQL2.edit(delete) == 1)
                                                {
                                                    Parent.WriteToimpImportConsole("Lead Deleted " + delete, 1);
                                                }*/
                                                Parent.pSetimpPrevious();
                                                returned="Previous";
                                                notImportedquery = "INSERT INTO NotImported (FileID,FirstName,LastName,PhoneNumber,AddressOne,AddressTwo,City,State,ZipCode,IPAddress,URL,DateReceived,Returned) VALUES (" + Convert.ToInt32(Parent.pGetimpMapSourceID()) + ",'" + fields[1].value + "','" + fields[2].value + "','" + fields[8].value + "','" + fields[3].value + "','" + fields[4].value + "','" + fields[5].value + "','" + fields[6].value + "','" + fields[7].value + "','" + fields[9].value + "','" + fields[10].value + "', getDate() ,'" + returned + "')";
                                                SQL.edit(notImportedquery);
                                                flag = false;
                                            }


                                        }
                                        else
                                        {
                                            DoNotText++;
                                            //Parent.setimpDNT(DoNotText.ToString());
                                            Parent.pSetimpDNT();
                                            returned="Do Not Text";
                                            notImportedquery = "INSERT INTO NotImported (FileID,FirstName,LastName,PhoneNumber,AddressOne,AddressTwo,City,State,ZipCode,IPAddress,URL,DateReceived,Returned) VALUES (" + Convert.ToInt32(Parent.pGetimpMapSourceID()) + ",'" + fields[1].value + "','" + fields[2].value + "','" + fields[8].value + "','" + fields[3].value + "','" + fields[4].value + "','" + fields[5].value + "','" + fields[6].value + "','" + fields[7].value + "','" + fields[9].value + "','" + fields[10].value + "', getDate() ,'" + returned + "')";
                                            SQL.edit(notImportedquery);
                                            flag = false;
                                        }
                                    }

                                
                            }



                        }
                    }
                    if ((values.Length > 0))
                    {
                        values = values.Substring(0, values.Length - 1);
                        values = values.Substring(0, values.Length - 1);
                    }
                    values = values + ",getDate(),'"+TimeZone+"')";
                    query = query + values;
                    if (query.CompareTo("") != 0)
                    {

                        if (Convert.ToInt32(Parent.pGetimpMapSourceID()) != 32)
                        {
                            if (SQL.edit(query) == 1)
                            {
                                impAddTotal++;
                                Parent.WriteToimpImportConsole("Lead Added: " + query, 1);
                                Parent.pSetimpSuccess();
                                for (int index = 0; index < fields.Length; index++)
                                {


                                    fields[index].seen = false;


                                }
                                query = "";
                                values = "";
                            }
                            else
                            {
                                for (int index = 0; index < fields.Length; index++)
                                {


                                    fields[index].seen = false;

                                }
                                impfailure++;
                                Parent.pSetimpFail();
                                //Parent.WriteToimpImportConsole("Lead Add Failed: " + query, 1);
                                query = "";

                            }

                        }
                    }else if(!flag){

                        notImportedquery = "INSERT INTO NotImported (FileID,FirstName,LastName,PhoneNumber,AddressOne,AddressTwo,City,State,ZipCode,IPAddress,URL,DateReceived,Returned) VALUES (" + Convert.ToInt32(Parent.pGetimpMapSourceID()) + ",'" + fields[1].value + "','" + fields[2].value + "','" + fields[8].value + "','" + fields[3].value + "','" + fields[4].value + "','" + fields[5].value + "','" + fields[6].value + "','" + fields[7].value + "','" + fields[9].value + "','" + fields[10].value + "', getDate() ,'" + returned + "')";
                        SQL.edit(notImportedquery);

                    }
                    coloumn = 0;
                    for (int index = 0; (index < fields.Length); index++)
                    {
                        if ((fields[index].mapped) && (fields[index].value.CompareTo("") != 0))
                        {
                            fields[index].value = "";
                        }

                    }
                    query1 = "";
                    flag = true;
                    mapped_columns = 1;
                }
                catch
                {
                    malformed++;
                    flag = false;
                    Parent.pSetimpMalformed();
                    if (!flag)
                    {
                        notImportedquery = "INSERT INTO NotImported (FileID,FirstName,LastName,PhoneNumber,AddressOne,AddressTwo,City,State,ZipCode,IPAddress,URL,DateReceived,Returned) VALUES (" + Convert.ToInt32(Parent.pGetimpMapSourceID()) + ",'" + fields[1].value + "','" + fields[2].value + "','" + fields[8].value + "','" + fields[3].value + "','" + fields[4].value + "','" + fields[5].value + "','" + fields[6].value + "','" + fields[7].value + "','" + fields[9].value + "','" + fields[10].value + "', getDate() ,'" + returned + "')";
                        SQL.edit(notImportedquery);
                    }
                    string str = "";
                    for (int index = 0; (index < fields.Length); index++)
                    {
                        if ((fields[index].mapped) && (fields[index].value.CompareTo("") != 0))
                        {
                            fields[index].seen = false;
                            str = str + "," + fields[index].value;
                        }

                    }
                    //Parent.WriteToimpImportConsole(str, 1);
                    for (int index = 0; (index < fields.Length); index++)
                    {
                        if ((fields[index].mapped) && (fields[index].value.CompareTo("") != 0))
                        {
                            fields[index].seen = false;
                            fields[index].value = "";
                        }

                    }
                }

                mapped_columns = 1;
                columns_read = 0;
            
               
            }
            LDBSQL importFile = new LDBSQL();
            int notImported= Convert.ToInt32(Parent.pGetimpOrigTotal())-Convert.ToInt32(Parent.pGetimpAddTotal());

            importFile.edit("INSERT INTO Imported(FileName,LSID,OriginalCount,MalformedCount,PreviousCount,DNTCount,BadCarrierCount,NoCarrierCount,DeactiveCount,InvalidPhoneCount,NotImportedCount,ImportedCount,DateImported) VALUES ('"+Parent.pGetimpMapSourceName()+"',"+Convert.ToInt32(Parent.pGetimpMapSourceID())+","+Convert.ToInt32(Parent.pGetimpOrigTotal())+","+Convert.ToInt32(Parent.pGetimpMalformed())+","+Convert.ToInt32(Parent.pGetimpPrevious())+","+Convert.ToInt32(Parent.pGetimpDNT())+","+Convert.ToInt32(Parent.pGetimpBCarr())+","+Convert.ToInt32(Parent.pGetimpNCarr())+","+Convert.ToInt32(Parent.pGetimpDeactive())+","+Convert.ToInt32(Parent.pGetimpInvalid())+","+notImported+","+Convert.ToInt32(Parent.pGetimpAddTotal())+",getDate())");
            DataTable table = importFile.query("Select Top 1 ID,DateImported FROM Imported where LSID =" + Convert.ToInt32(Parent.pGetimpMapSourceID()) + " order by DateImported DESC");
            if (table.TableName != "Error")
            {
                string ID = table.Rows[0]["ID"].ToString();
                importFile.edit("UPDATE Leads_Bad_Carriers SET FileID=" + ID + " where FileID=" + Convert.ToInt32(Parent.pGetimpMapSourceID()));
                importFile.edit("UPDATE Leads SET FileID=" + ID + " where FileID=" + Convert.ToInt32(Parent.pGetimpMapSourceID()));
            }
          
           
            return true;
        }

    
    }


    public class CsvRow : List<string>
    {
        public string LineText { get; set; }
    }

    public class CsvFileWriter : StreamWriter
    {
        public CsvFileWriter(Stream stream)
            : base(stream)
        {
        }

        public CsvFileWriter(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        public void WriteRow(CsvRow row)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in row)
            {
                // Add separator if this isn't the first value
                if (builder.Length > 0)
                    builder.Append(',');

                if (value.IndexOfAny(new char[] { '"', ',' }) != -1)
                {
                    // Special handling for values that contain comma or quote
                    // Enclose in quotes and double up any double quotes
                    builder.AppendFormat("\"{0}\"", value.Replace("\"", "\"\""));
                }
                else builder.Append(value);
            }
            row.LineText = builder.ToString();
            WriteLine(row.LineText);
        }
    }


    public class CsvFileReader : StreamReader
    {
        public CsvFileReader(Stream stream)
            : base(stream)
        {
        }

        public CsvFileReader(string filename)
            : base(filename)
        {
        }

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool ReadRow(CsvRow row)
        {
            row.LineText = ReadLine();
            if (String.IsNullOrEmpty(row.LineText))
                return false;

            int pos = 0;
            int rows = 0;

            while (pos < row.LineText.Length)
            {
                string value;

                // Special handling for quoted field
                if (row.LineText[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    int start = pos;
                    while (pos < row.LineText.Length)
                    {
                        // Test for quote character
                        if (row.LineText[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }
                        pos++;
                    }
                    value = row.LineText.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    int start = pos;
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    value = row.LineText.Substring(start, pos - start);
                }

                // Add field to list
                if (rows < row.Count)
                    row[rows] = value;
                else
                    row.Add(value);
                rows++;

                // Eat up to and including next comma
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                if (pos < row.LineText.Length)
                    pos++;
            }
            // Delete any unused items
            while (row.Count > rows)
                row.RemoveAt(rows);

            // Return true if any columns read
            return (row.Count > 0);
        }
    }
}
