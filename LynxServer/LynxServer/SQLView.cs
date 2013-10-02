using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lynxServer
{
    class SQLView
    {
        private string _name = "";
        private string _stdel = "";
        private string _gdac = "";
        private string _lsid = "";

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string StateDeletes
        {
            get { return _stdel; }
            set { _stdel = value; }
        }
        public string GoodAreaCodes
        {
            get { return _gdac; }
            set { _gdac = value; }
        }
        public string LSIDs
        {
            get { return _lsid; }
            set { _lsid = value; }
        }

        public string GenerateCampaignView()
        {
            string SQL = "";
            if (_name != "" && _lsid != "" && _stdel != "" && _gdac != "")
            {
                SQL += "CREATE OR REPLACE VIEW vw" + _name + " AS";
                SQL += "SELECT ID, Telephone_Number FROM Leads";
                SQL += " WHERE FileID in (SELECT ID FROM Imported WHERE LSID IN (" + _lsid + "))";
                SQL += " and area_code not in (" + _stdel + ")";
                SQL += " and (area_code  in (" + _gdac + ") or area_code not in(select distinct areacode from areacodetz))";
                SQL += " and CampId = 0";
                SQL += " Order By ID DESC";
            }
            else
            {
                SQL += "Error";
            }
            return SQL;
        }
        public string RemoveView()
        {
            string SQL="";
            if (_name != "")
            {
                SQL += " DROP VIEW vw" + _name;
            }
            else
            {
                SQL += "Error";
            }
            return SQL;
        }
    }
}
