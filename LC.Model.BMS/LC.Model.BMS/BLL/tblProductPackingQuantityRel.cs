using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblProductPackingQuantityRel : LC.Model.BMS.DAL._tblProductPackingQuantityRel
    {
        public void GetPacking_Qunatity(int productid)
        {
            string query = string.Empty;
            query += " select rel.pkProductPackingQuantityRelID, pp.pkProductPackageID, pp.pName, pp.qName  from dbo.tblProductPackages pp inner join ";
            query += " dbo.tblProductPackingQuantityRel rel on rel.fkProductPackageID = pp.pkProductPackageID and rel.isactive = 1 inner join ";
            query += " dbo.tblProducts p on p.pkproductid = rel.fkproductid and p.pkproductid = " + productid;
            this.LoadFromRawSql(query);
        }
        public void GetProductsForVat(int departmentid)
        {
            string query = string.Empty;
            query += " select distinct pqr.*";
            query += " from tblproducts p ";
            query += " left join dbo.tblSubCategories sub on sub.pkSubCategoryID = p.fksubcategoryid ";
            query += " left join dbo.tblBaseCategories bas on bas.pkBaseCategoryID = sub.fkBaseCategoryID ";
            query += " left join dbo.tblVAT v on v.pkvatid = bas.fkvatid ";
            query += " left join dbo.tblProductPackingQuantityRel pqr on pqr.fkproductid = p.pkproductid ";
            query += " left join dbo.tblProductPackages pp on pp.pkProductPackageID = pqr.fkProductPackageID ";
            query += " left join dbo.tblProductQuantities pq on pq.pkProductQuantityID = pqr.fkProductQuantityID  ";
            query += " where bas.fkdepartmentid  = " + departmentid + " ";

            this.LoadFromRawSql(query);
        }

        public void GetID(int productid, int packingid)
        {
            this.Where.FkProductID.Value = productid;
            this.Where.FkProductPackageID.Value = packingid;
            this.Query.Load();
        }
    }
}
