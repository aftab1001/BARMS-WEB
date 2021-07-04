using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSupplierAddresses : LC.Model.BMS.DAL._tblSupplierAddresses
    {
        public void GetSupplierAddresses(int supplierid)
        {
            string query = string.Empty;
            query += " select sa.*, c.sCountry from dbo.tblSupplierAddresses sa inner join ";
            query += " dbo.tblCountries c on c.pkCountryID = sa.fkAddressCountry ";
            query += " where sa.fkSupplierID = " + supplierid;
            this.LoadFromRawSql(query);
        }
        public bool GetFirst(int supplierid)
        {
            string query = string.Empty;
            query += "select Top(1) sa.* from dbo.tblSupplierAddresses sa where sa.fkSupplierID != " + supplierid;
            this.LoadFromRawSql(query);
            if (this.RowCount > 0)
                return true;
            else
                return false;
        }
        public void GetAllExceptOne(int supplierAddressID)
        {
            string query = string.Empty;
            query += "select sa.* from dbo.tblSupplierAddresses sa where sa.pkSupplierAddressID != " + supplierAddressID;
            this.LoadFromRawSql(query);
        }
    }
}
