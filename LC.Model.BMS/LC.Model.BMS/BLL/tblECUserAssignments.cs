using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblECUserAssignments : LC.Model.BMS.DAL._tblECUserAssignments
    {
        public void CheckAssignedUsers(int spid, int assignedID, DateTime assignedDate)
        {


            string query = string.Empty;
            query += " select ec.* from dbo.tblECUserAssignments ec ";
            query += " where ecuserid = " + assignedID + " and fkspecialtyid = " + spid + " and dmodifieddate = '" + assignedDate + "' ";
            this.LoadFromRawSql(query);

        }
        public void CheckAssignedRegisters(int rid, int assignedID, DateTime assignedDate)
        {

            string query = string.Empty;
            query += " select ec.* from dbo.tblECUserAssignments ec ";
            query += " where ec.ecuserid = " + assignedID + " and ec.fkRegisterID = " + rid + " and ec.dRegisterDate = '" + assignedDate + "' ";
            this.LoadFromRawSql(query);

        }
        public void GetRegistersForECuserforDay(DateTime dRegisterDate)
        {
            string query = string.Empty;
            query += "select UA.*,r.* from dbo.tblECUserAssignments UA inner join dbo.tblRegisters r on r.pkRegisterID= UA.fkRegisterID where UA.dRegisterDate ='" + dRegisterDate + "'";
            this.LoadFromRawSql(query);
        }
        public void CheckAssignedToECUsers( DateTime assignedDate)
        {
            string query = string.Empty;
            query += " select ec.* from dbo.tblECUserAssignments ec ";
            query += " where dmodifieddate = '" + assignedDate + "' ";
            this.LoadFromRawSql(query);

        }
        public void CheckAssignedRegistersToECUsers(int rid, DateTime dRegisterDate)
        {
            string query = string.Empty;
            query += " select ec.* from dbo.tblECUserAssignments ec ";
            query += " where  ec.fkRegisterID = " + rid + " and dRegisterdate = '" + dRegisterDate + "' ";
            this.LoadFromRawSql(query);

        }
        public void CheckAssignedToECUsersByDateandID(DateTime assignedDate,int iECUserID)
        {
            string query = string.Empty;
            query += " select ec.* from dbo.tblECUserAssignments ec ";
            query += " where ec.dmodifieddate = '" + assignedDate + "' and  ec.ECUserID ='" + iECUserID + "'";
            this.LoadFromRawSql(query);

        }
        public void CheckAssignedRegisterToECUsersByDateandID(DateTime assignedDate, int iECUserID)
        {
            string query = string.Empty;
            query += " select ec.* from dbo.tblECUserAssignments ec ";
            query += " where ec.dRegisterDate = '" + assignedDate + "' and  ec.ECUserID ='" + iECUserID + "'";
            this.LoadFromRawSql(query);

        }
        public void CheckECUsersSalery(DateTime assignedDate,int iECUserid)
        {
            string query = string.Empty;
            query += " select ec.* from dbo.tblECUserAssignments ec ";
            query += " where dmodifieddate = '" + assignedDate + "' or dRegisterDate = '" + assignedDate + "' and ec.ECUserID = '" + iECUserid + "' ";
            this.LoadFromRawSql(query);

        }
        public void CheckECUsersSalery(DateTime assignedDate)
        {
            string query = string.Empty;
            query += " select distinct ec.ecuserid from dbo.tblECUserAssignments ec  ";            
            query += " where dmodifieddate = '" + assignedDate + "' or dRegisterDate = '" + assignedDate + "'";
            this.LoadFromRawSql(query);

        }
        public void CheckECUsersSaleryForWeek(int iECUserid,DateTime dStartDate, DateTime dEndDate)
        {
            string query = string.Empty;
            query += " select ec.*, dmodifieddate as WorkDate from dbo.tblECUserAssignments ec  where ec.ECUserID = '" + iECUserid + "' and dcreateddate >= '" + dStartDate + "' and dcreateddate <= '" + dEndDate + "' union  ";
            query += " select ec.* , dRegisterDate as WorkDate from dbo.tblECUserAssignments ec  where ec.ECUserID = '" + iECUserid + "' and dRegisterDate >= '" + dStartDate + "' and dRegisterDate >= '" + dEndDate + "' ";
            this.LoadFromRawSql(query);

        }
    }
}
