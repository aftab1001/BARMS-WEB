using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSupplierProducts : LC.Model.BMS.DAL._tblSupplierProducts
    {
        public void GetSupplierProducts(int productid, int supplierid)
        {
            string query = string.Empty;
            query += " select sp.*, s.sBrandName, p.sProductName from dbo.tblProducts  p inner join ";
            query += " dbo.tblSupplierProducts sp on sp.fkProductID = p.pkProductID inner join ";
            query += " dbo.tblSupplier s on s.pkSupplierID = sp.fkSupplierID ";
            query += " where sp.fkProductID = " + productid + " and sp.fkSupplierID = " + supplierid;
            this.LoadFromRawSql(query);

        }
        public void GetSupplierProductsBySupplier(int supplierid)
        {
            string query = string.Empty;
            query += " select sp.*, s.sBrandName, p.sProductName from dbo.tblProducts  p inner join ";
            query += " dbo.tblSupplierProducts sp on sp.fkProductID = p.pkProductID inner join ";
            query += " dbo.tblSupplier s on s.pkSupplierID = sp.fkSupplierID ";
            query += " where sp.fkSupplierID = " + supplierid;
            this.LoadFromRawSql(query);

        }
        public void GetSupplierNamesForPro(int productid)
        {
            string query = string.Empty;
            query += " select s.pksupplierid,s.sbrandname,s.isActive, ";
            query += " dbo.GetSupplierProLinking(s.pksupplierid," + productid + ") as SupplierProID  ";
            query += " from dbo.tblSupplier s order by s.sbrandname";
            this.LoadFromRawSql(query);
        }
    }
}
