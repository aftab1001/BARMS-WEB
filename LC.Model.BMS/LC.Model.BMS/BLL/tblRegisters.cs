using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblRegisters : LC.Model.BMS.DAL._tblRegisters
    {
        public void CheckRegisterNameInSaveMode(string registerName)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.RName.Value = registerName;
            this.Query.Load();
        }
        public void CheckRegisterNameInEditMode(int regid, string registerName)
        {
            string query = string.Empty;
            query += "select r.pkregisterid from dbo.tblRegisters r where r.pkregisterid != " + regid + " and rName = '" + registerName + "'";
            this.LoadFromRawSql(query);
        }
        public void GetAllRegisters()
        {
            string query = string.Empty;
            query += "select r.*,v.* from dbo.tblRegisters r left join dbo.tblVAT v on v.pkvatid = r.fkvatid";
            this.LoadFromRawSql(query);
        }
        public void GetActiveRegisterForDailyInput(int day, int month, int year)
        {
            string query = string.Empty;
            query += " select r.* from dbo.tblRegisters r ";
            query += " where r.isActive = 1 ";
            this.LoadFromRawSql(query);
        }
        public void getRegisterForECUser(DateTime assignedDate, int ECUserID)
        {
            string query = string.Empty;
            query += " select distinct(r.rname),r.* from dbo.tblECUserAssignments ec inner join ";
            query += " dbo.tblRegisters r on r.pkRegisterID = ec.fkRegisterID ";
            query += " where r.isactive = 1 and ec.dRegisterDate = '" + assignedDate + "' and ECUserID = '" + ECUserID + "'";
            this.LoadFromRawSql(query);

        }

    }
}
