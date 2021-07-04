using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSupplierEmails:LC.Model.BMS.DAL._tblSupplierEmails
    {
        public void GetSupplierEmails(int supplierid)
        {
            string query = string.Empty;
            query += "select se.* from dbo.tblSupplierEmails se where se.fkSupplierID = " + supplierid;
            this.LoadFromRawSql(query);
        }
        public bool GetFirst(int supplierid)
        {
            string query = string.Empty;
            query += "select Top(1) se.* from dbo.tblSupplierEmails se where se.fkSupplierID = " + supplierid;
            this.LoadFromRawSql(query);
            if (this.RowCount > 0)
                return true;
            else
                return false;
        }
        public void GetAllExceptOne(int supplierEmailID)
        {
            string query = string.Empty;
            query += "select se.* from dbo.tblSupplierEmails se where se.pksupplieremails != " + supplierEmailID;
            this.LoadFromRawSql(query);
        }
        public void CheckSupplierEmail(string email)
        {
            string query = string.Empty;
            query += " Select se.* from dbo.tblSupplierEmails se where se.sEmail = '" + email + "'";
            this.LoadFromRawSql(query);
        }
        public void GetActiveEmail(int supplierid)
        {
            string query = string.Empty;
            query += "select se.* from dbo.tblSupplierEmails se where se.fksupplierid = " + supplierid + " and se.bisprimary =1";
            this.LoadFromRawSql(query);
        }
    }
}
