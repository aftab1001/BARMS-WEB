using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSupplierSubCats : LC.Model.BMS.DAL._tblSupplierSubCats
    {
        public void GetSupplierNamesForSub(int subid)
        {
            string query = string.Empty;
            query += " select s.pksupplierid,s.sbrandname,s.isActive, ";
            query += " dbo.GetSupplierSubLinking(s.pksupplierid," + subid + ") as SupplierSubID ";
            query += " from dbo.tblSupplier s order by s.sbrandname";
            this.LoadFromRawSql(query);
        }

        public void GetSupplierSubRecord(int supplierid, int subCatid)
        {
            this.Where.FkSupplierID.Value = supplierid;
            this.Where.FkSubCategoryID.Value = subCatid;
            this.Query.Load();
        }
    }
}
