using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
   public class tblContactPeople:LC.Model.BMS.DAL._tblContactPeople
    {
       public bool GetFirst(int supplierid)
       {
           string query = string.Empty;
           query += "select Top(1) c.* from dbo.tblContactPeople c where c.fkSuplierID = " + supplierid;
           this.LoadFromRawSql(query);
           if (this.RowCount > 0)
               return true;
           else
               return false;
       }
       public void GetAllExceptOne(int ContactPeopleID)
       {
           string query = string.Empty;
           query += "select c.* from dbo.tblContactPeople c where c.pkContactPeopleID != " + ContactPeopleID;
           this.LoadFromRawSql(query);
       }
       public void GetContactsBySupplier(int supplierid)
       {
           string query = string.Empty;
           query += "select c.* from dbo.tblContactPeople c where c.fkSuplierID = " + supplierid;
           this.LoadFromRawSql(query);
       }
           
    }
}
