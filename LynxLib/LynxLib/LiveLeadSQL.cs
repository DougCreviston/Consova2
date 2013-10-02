using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LynxLib
{
    class LiveLeadSQL
    {
          private string _table = "";
        private string _clause = "";

        public string table
        {
            get { return _table; }
            set { _table = value; }
        }
        public string clause
        {
            get { return _clause; }
            set { _clause = value; }
        }
        public string queryField(string field)
        {
            return _queryField(field);
        }
        private string _queryField(string field)
        {
            if (_table == "" || _clause == "" || field == "")
            {
                return "{empty}";
            }
            else
            {
                using (SqlConnection con = new SqlConnection(LynxLib.Properties.Resources.liveleadConnStr))
                {
                    con.Open();
                    SqlCommand cmdSQL = new SqlCommand("SELECT Top 1 " + field + " FROM " + _table + " WHERE " + _clause, con);
                    try
                    {
                        SqlDataReader reader = cmdSQL.ExecuteReader();
                        while (reader.Read())
                        {
                            string temp = reader.GetValue(0).ToString();
                            cmdSQL.Dispose();
                            con.Close();
                            return temp;
                        }
                    }
                    catch
                    {
                        cmdSQL.Dispose();
                        con.Close();
                        return "{error}";
                    }
                    cmdSQL.Dispose();
                    con.Close();

                }
            }
            return "{end}";
        }
        public int editField(string field, string value)
        {
            return _editField(field, value);
        }
        private int _editField(string field, string value)
        {
            if (_table == "" || _clause == "" || value == "" || field == "")
            {
                return 0;
            }
            else
            {
                int affected = 0;
                using (SqlConnection con = new SqlConnection(LynxLib.Properties.Resources.liveleadConnStr))
                {
                    con.Open();

                    SqlCommand cmdSQL = new SqlCommand("UPDATE " + _table + " SET " + field + "='" + value + "' WHERE " + _clause, con);
                    try
                    {
                        affected = cmdSQL.ExecuteNonQuery();
                        cmdSQL.Dispose();
                        con.Close();
                        return affected;
                    }
                    catch
                    {
                        cmdSQL.Dispose();
                        con.Close();
                        return -1;
                    }

                }
            }
        }
        public DataTable query(string SQL)
        { 
            

                DataSet myData = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlConnection con = new SqlConnection(LynxLib.Properties.Resources.liveleadConnStr);
                try
                {
                    string str = LynxLib.Properties.Resources.ldbConnStr;
                    SqlCommand cmdSQL = new SqlCommand(SQL, con);
                    adapter.SelectCommand = cmdSQL;
                    adapter.Fill(myData, "myTable");
                }
                catch (Exception e)
                {
                    DataTable myTable = myData.Tables.Add("Error");
                    myTable.Columns.Add("Source", typeof(string));
                    myTable.Columns.Add("Message", typeof(string));
                    myTable.Columns.Add("StackTrace", typeof(string));

                    myTable.Rows.Add(e.Source, e.Message, e.StackTrace);

                }
                return myData.Tables[0];
            
        
         
        }
        public int edit(string SQL)
        {
            
                int affected = 0;
                using (SqlConnection con = new SqlConnection(LynxLib.Properties.Resources.liveleadConnStr))
                {  try
                    {
                    con.Open();
                    SqlCommand cmdSQL = new SqlCommand(SQL, con);
                  
                        affected = cmdSQL.ExecuteNonQuery();
                    cmdSQL.Dispose();
                    con.Close();
                    }
                    catch
                    {
                        affected = -1;
                    }
                    
                    return affected;
                }
            
           
          
        }


    }
}
