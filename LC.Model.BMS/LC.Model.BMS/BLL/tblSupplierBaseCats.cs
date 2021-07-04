using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSupplierBaseCats : LC.Model.BMS.DAL._tblSupplierBaseCats
    {
        public void GetSupplierNamesForBase(int basecatid)
        {
            string query = string.Empty;
            query += "select s.pksupplierid,s.sbrandname,s.isActive, ";
            query += "dbo.GetSupplierBaseLinking(s.pksupplierid," + basecatid + ") as SupplierBaseID ";
            query += "from dbo.tblSupplier s order by s.sbrandname";
            this.LoadFromRawSql(query);
        }
    }
}
