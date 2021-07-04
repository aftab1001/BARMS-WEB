using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSubCategories : LC.Model.BMS.DAL._tblSubCategories
    {
        public void GetSubCategories(int baseCatID)
        {
            string query = string.Empty;
            query += "select s.*  from dbo.tblSubCategories s where s.fkBaseCategoryID = " + baseCatID;
            this.LoadFromRawSql(query);
        }
        public void GetActiveSubCat(int baseCat)
        {
            this.Where.FkBaseCategoryID.Value = baseCat;
            this.Where.IsActive.Value = true;
            this.Query.Load();
        }
        public void GetAllSubCats(int departmentid)
        {
            string query = string.Empty;
            query += " select b.pkbasecategoryid,b.CatagoryName, sub.*,v.vat from dbo.tblBaseCategories b inner join ";
            query += " dbo.tblSubCategories sub on sub.fkbasecategoryid = b.pkbasecategoryid left join ";
            query += " dbo.tblVAT v on  v.pkvatid = b.fkvatid where b.isactive = 1  and b.fkdepartmentid = " + departmentid + " order by b.pkbasecategoryid ";
            this.LoadFromRawSql(query);
        }
        public void GetActiveSubCate(int subcat)
        {
            string query = string.Empty;
            query += " select sub.* from dbo.tblSubCategories sub where sub.pkSubCategoryID != " + subcat + " and sub.isactive = 1";
            this.LoadFromRawSql(query);
        }
    }
}
