using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace LC.Model.BMS.BLL
{
    public class tblRegisterHistory : LC.Model.BMS.DAL._tblRegisterHistory
    {
        public void GetRegisterValue_ByRegisterID(int regid, int day, int month, int year)
        {
            string query = string.Empty;
            query += " select rh.* from dbo.tblRegisters r left join ";
            query += " dbo.tblRegisterHistory rh on rh.fkregisterid = r.pkregisterid ";
            query += " where r.isActive = 1 and r.pkregisterid = " + regid + " and (rh.rDay = " + day + " and rh.rMonth = " + month + " and rh.rYear = " + year + ")";
            this.LoadFromRawSql(query);
        }
        public void GetSubtotal(int day, int WeekNo, int month, int year)
        {
            string query = string.Empty;
            query += " select distinct rh.rValue,rh.* from dbo.tblRegisters r inner join  ";
            query += " dbo.tblRegisterHistory rh on rh.fkregisterid = r.pkregisterid ";
            query += "  where r.isActive = 1 and (rh.idaynumber = " + day + "and rh.iweeknumber = " + WeekNo + " and rh.rMonth = " + month + " and rh.rYear = " + year + ")";
            this.LoadFromRawSql(query);
        }
        public void GetRegisterValForStats(DateTime weekstart, DateTime weekend, int weeknumber)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@weekStart", weekstart);
            Params.Add("@weekend", weekend);
            Params.Add("@iweeknumber", weeknumber);

            this.LoadFromSql("spGetRegisterForStats", Params);
        }
    }
}
