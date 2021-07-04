using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
   public class tblSupplierFaxes:LC.Model.BMS.DAL._tblSupplierFaxes
    {
       public void GetSupplierFaxes(int supplierid)
       {
           string query = string.Empty;
           query += "select sf.* from dbo.tblSupplierFaxes sf where sf.fkSupplierID = " + supplierid;
           this.LoadFromRawSql(query);
       }
       public bool GetFirst(int supplierid)
       {
           string query = string.Empty;
           query += "select Top(1) sf.* from dbo.tblSupplierFaxes sf where sf.fkSupplierID = " + supplierid;
           this.LoadFromRawSql(query);
           if (this.RowCount > 0)
               return true;
           else
               return false;
       }
       public void GetAllExceptOne(int supplierFaxID)
       {
           string query = string.Empty;
           query += "select sf.* from dbo.tblSupplierFaxes sf where sf.pkSupplierFaxID != " + supplierFaxID;
           this.LoadFromRawSql(query);
       }
       public void CheckSupplierFax(string fax)
       {
           string query = string.Empty;
           query += " select sf.* from dbo.tblSupplierFaxes sf where sf.sFax = '" + fax + "'";
           this.LoadFromRawSql(query);
       }
    }
}
