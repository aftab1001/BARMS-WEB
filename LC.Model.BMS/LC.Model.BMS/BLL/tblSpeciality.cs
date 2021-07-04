using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSpeciality : LC.Model.BMS.DAL._tblSpeciality
    {
        public void CheckSpecialtySeparator(int orderid, int departmentid)
        {
            string query = string.Empty;
            query += " select t.* from dbo.tblSpeciality t where t.orderid > " + orderid + " and t.fkdepartmentid = " + departmentid + " and t.sspeciality = 'Separator' order by t.orderid ";
            this.LoadFromRawSql(query);
        }
        public void GetPositionSeperatorOptional(int orderid, int departmentid)
        {
            string query = string.Empty;
            query += " select top(1) s.* from dbo.tblSpeciality s where s.orderid > " + orderid + " and s.fkdepartmentid = " + departmentid + " order by s.OrderID ";
            this.LoadFromRawSql(query);
        }
        public void GetPositionSeperatorPermanent(int orderid, int departmentid)
        {
            string query = string.Empty;
            query += " select top (1) s.* from dbo.tblSpeciality s ";
            query += " where s.orderid > " + orderid + " and s.fkdepartmentid = " + departmentid + " and s.sspeciality = 'Separator'  order by s.orderid ";
            this.LoadFromRawSql(query);
        }
        public void LoadWorkShiftSpecialities(int DepartmentID)
        {
            string strQuery = string.Empty;
            strQuery += " select sp.sSpeciality ,sp.pkSpecialityID , sp.fkSpecialityTypeID , sp.Abbrv , sp.bIsActive , sp.OrderID ";
            strQuery += " from tblSpeciality sp inner join tblSpecialityType sptype on sp.fkSpecialityTypeID = sptype.pkSpecialityTypeID ";
            strQuery += " where sp.bIsActive = 1 and  sp.fkdepartmentID = " + DepartmentID;
            //strQuery += " order by  sptype.pkSpecialityTypeID ";
            strQuery += " order by sp.OrderID, sp.sSpeciality ";

            this.LoadFromRawSql(strQuery);
        }
        public void LoadAllSpecialities(int DepartmentID)
        {
            string strQuery = string.Empty;
            strQuery += " select sp.sSpeciality ,sp.pkSpecialityID , sp.fkSpecialityTypeID , sp.Abbrv , sp.bIsActive ,sp.bIsIncomeSpecific, sp.OrderID, sp.dModifiedDate ";
            strQuery += " from tblSpeciality sp inner join tblSpecialityType sptype on sp.fkSpecialityTypeID = sptype.pkSpecialityTypeID ";
            strQuery += " where sp.fkdepartmentID = " + DepartmentID;
            strQuery += " order by sp.OrderID ";
            //strQuery += " order by sptype.pkSpecialityTypeID ";

            this.LoadFromRawSql(strQuery);
        }
        public void GetAllActiveSpecialties(int DepartmentID)
        {
            string strQuery = string.Empty;
            strQuery = "Select sp.* from  tblSpeciality sp where sp.fkdepartmentID = " + DepartmentID + " order by sp.sSpeciality";
            this.LoadFromRawSql(strQuery);
        }
        public void CheckSpecialityName(string SpecialityName)
        {
            this.FlushData();
            this.Where.WhereClauseReset();
            this.Where.SSpeciality.Value = SpecialityName;
            this.Query.Load();

        }
        public void GetSpecialtyMaxOrderID()
        {
            string query = string.Empty;
            query = "select max(orderid) as orderid from tblspeciality";
            this.LoadFromRawSql(query);
        }
        public void GetSpecialtiesForSortLesser(int newSortNo, int oldSortNo)
        {
            string query = string.Empty;
            query = "select s.* from dbo.tblSpeciality s where s.orderid >= " + newSortNo + " and s.orderid<= " + oldSortNo + "";
            this.LoadFromRawSql(query);
        }
        public void GetSpecialtiesForSortGreater(int oldSortNo, int newSortNo)
        {
            string query = string.Empty;
            query = "select s.* from dbo.tblSpeciality s where s.orderid >= " + oldSortNo + " and s.orderid<= " + newSortNo + "";
            this.LoadFromRawSql(query);
        }
        public void LoadSpecialtiesWithSeperator(int Departmentid)
        {
            string query = string.Empty;
            query = "select s.* from dbo.tblSpeciality s where s.sSpeciality != 'Seperator' and s.bIsActive = 1 and s.fkDepartmentID = " + Departmentid;
            this.LoadFromRawSql(query);
        }
        public void GetPositionsForECuser(int departmentid)
        {
            string query = string.Empty;
            query += " select sp.* from  dbo.tblSpeciality sp  where bisactive = 1 and sSpeciality !='Separator' and sp.bisincomeSpecific = 1 and sp.fkdepartmentid = " + departmentid + " order by orderid ";
            this.LoadFromRawSql(query);
        }
        public void GetRegistersForECuser()
        {
            string query = string.Empty;
            query += " select r.* from dbo.tblRegisters r where r.isactive = 1 ";
            this.LoadFromRawSql(query);
        }
        public void getPositionForStats(int departmentid)
        {
            string query = string.Empty;
            query += " select sp.* from  dbo.tblSpeciality sp where bisincomespecific = 1 and sSpeciality !='Separator' and bisactive = 1 and fkdepartmentid = " + departmentid;
            this.LoadFromRawSql(query);
        }
        public void getSpecialityForStatistics(int year, int departmentid)
        {
            string query = string.Empty;
            query += " select sptypes.pkspecialityTypeid, sptypes.sspecialityName from dbo.tblSpecialityType sptypes ";
            query += " where sptypes.pkSpecialityTypeID in ";
            query += " (select distinct s.fkSpecialityTypeID from dbo.tblSpeciality s ";
            query += " where s.sSpeciality != 'Separator' and s.fkdepartmentid = " + departmentid + " and  s.pkSpecialityID in ( ";
            query += " select distinct uws.fkSpecialityID from dbo.tblUserWorkshifts uws ";
            query += " where uws.iyear = " + year + ")) ";
            this.LoadFromRawSql(query);
        }
        public void getECUserPosition(int ecuserid, DateTime d)
        {
            string query = string.Empty;
            query += " select s.pkSpecialityID,sSpeciality from dbo.tblECUserAssignments ec inner join ";
            query += " dbo.tblSpeciality s on s.pkSpecialityID = ec.fkSpecialtyID ";
            query += " where ecuserid = " + ecuserid + "  and ec.dmodifieddate = '" + d + "'";
            this.LoadFromRawSql(query);
        }
        public void getECUserSpeciality(int userid)
        {
            string query = string.Empty;
            query += " select top(1) us.fkspecialitytypeid from dbo.tblUserSpeciality us ";
            query += " where us.fkuserid = " + userid + " order by us.dcreatedate desc";
            this.LoadFromRawSql(query);
        }
        public void CheckSeperator(int FirstOrder,int SecondOrder)
        {
            string query = string.Empty;
            query += " select * from dbo.tblspeciality s ";   
            query += "where s.orderid between " +  FirstOrder+ " and "+SecondOrder+"";
            this.LoadFromRawSql(query);
        }
    }
}
