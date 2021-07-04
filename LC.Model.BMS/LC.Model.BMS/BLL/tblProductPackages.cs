using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
   public class tblProductPackages: LC.Model.BMS.DAL._tblProductPackages
    {
       public void GetAllProductPackages(int productid)
       {
           string query = string.Empty;
           query += "select pk.* from dbo.tblProductPackages pk where pk.fkproductid = " + productid;
           this.LoadFromRawSql(query);
       }
       public bool GetFirst(int ProductPackageID)
       {
           string query = string.Empty;
           query += "select Top(1) pk.* from dbo.tblProductPackages pk where pk.pkProductPackageID = " + ProductPackageID;
           this.LoadFromRawSql(query);
           if (this.RowCount > 0)
               return true;
           else
               return false;
       }
       public void GetAllExceptOne(int ProductPackageID)
       {
           string query = string.Empty;
           query += "select pk.* from dbo.tblProductPackages pk where pk.pkProductPackageID != " + ProductPackageID;
           this.LoadFromRawSql(query);
       }
       public void CheckProductPackage(string package)
       {
           string query = string.Empty;
           query += " Select pk.* from dbo.tblProductPackages pk where pk.Packages = '" + package + "'";
           this.LoadFromRawSql(query);
       }
       public void GetActivePackage(int productid)
       {
           string query = string.Empty;
           query += "select pk.* from dbo.tblProductPackages pk where pk.fkproductid = " + productid + " and pk.bIsPrimary = 1";
           this.LoadFromRawSql(query);
       }
       public void GetPackingMaxOrderNum()
       {
           string query = string.Empty;
           query = "select  isnull(max(pOrder),'0') as orderNum from dbo.tblProductPackages";
           this.LoadFromRawSql(query);
       }
       public void GetPackingForSortLesser(int newSortNo, int oldSortNo)
       {
           string query = string.Empty;
           query = "select pk.* from  dbo.tblProductPackages pk where pk.pOrder >= " + newSortNo + " and pk.pOrder<= " + oldSortNo + "";
           this.LoadFromRawSql(query);
       }
       public void GetPackingForSortGreater(int oldSortNo, int newSortNo)
       {
           string query = string.Empty;
           query = "select pk.* from dbo.tblProductPackages pk where pk.pOrder >= " + oldSortNo + " and pk.pOrder<= " + newSortNo + "";
           this.LoadFromRawSql(query);
       }
       public void GetOrderedPacking()
       {
           string query = string.Empty;
           query += " select pk.* from dbo.tblProductPackages pk order by pk.pOrder";
           this.LoadFromRawSql(query);
       }

       public void GetPackingOptions_Active()
       {
           string query = string.Empty;
           query += " select pk.* from dbo.tblProductPackages pk where pk.isActive = 1 order by pk.pName";
           this.LoadFromRawSql(query);
       }
    }
}
