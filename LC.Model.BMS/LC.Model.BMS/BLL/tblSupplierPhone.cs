using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSupplierPhone:LC.Model.BMS.DAL._tblSupplierPhone
    {
        public void GetSupplierPhones(int supplierid)
        {
            string query = string.Empty;
            query += "select sp.* from dbo.tblSupplierPhone sp where sp.fkSupplierID = " + supplierid;
            this.LoadFromRawSql(query);
        }
        public bool GetFirst(int supplierid)
        {
            string query = string.Empty;
            query += "select Top(1) sp.* from dbo.tblSupplierPhone sp where sp.fkSupplierID = " + supplierid;
            this.LoadFromRawSql(query);
            if (this.RowCount > 0)
                return true;
            else
                return false;
        }
        public void GetAllExceptOne(int supplierPhoneID)
        {
            string query = string.Empty;
            query += "select sp.* from dbo.tblSupplierPhone sp where sp.pkSupplierPhoneID != " + supplierPhoneID;
            this.LoadFromRawSql(query);
        }

        public void CheckSupplierPhone(string phone)
        {
            string query = string.Empty;
            query += " select sp.* from dbo.tblSupplierPhone sp where sp.phone = '" + phone + "'";
            this.LoadFromRawSql(query);
        }
    }
}
