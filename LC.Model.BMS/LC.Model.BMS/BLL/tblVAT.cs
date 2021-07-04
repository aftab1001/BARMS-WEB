using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblVAT : LC.Model.BMS.DAL._tblVAT
    {
        public void GetSortedVatValues()
        {
            string query = string.Empty;
            query += "select v.* from dbo.tblVAT v order by convert(int, REPLACE(vat,'%',''))";
            this.LoadFromRawSql(query);
        }

        public void CheckVatForBase(int vatid)
        {
            string query = string.Empty;
            query += " select v.* from dbo.tblVAT v inner join ";
            query += " dbo.tblBaseCategories b on b.fkvatid = v.pkvatid  ";
            query += " where v.pkvatid = " + vatid;
            this.LoadFromRawSql(query);
        }
        public void CheckVatForSub(int vatid)
        {
            string query = string.Empty;
            query += " select v.* from dbo.tblVAT v inner join ";
            query += " dbo.tblSubCategories s on s.fkvatid = v.pkvatid  ";
            query += " where v.pkvatid = " + vatid;
            this.LoadFromRawSql(query);
        }
        public void CheckVatForPro(int vatid)
        {
            string query = string.Empty;
            query += " select v.* from dbo.tblVAT v inner join ";
            query += " dbo.tblProductPackingQuantityRel rel on rel.fkvatid = v.pkvatid  ";
            query += " where v.pkvatid = " + vatid;
            this.LoadFromRawSql(query);
        }
        


    }
}
