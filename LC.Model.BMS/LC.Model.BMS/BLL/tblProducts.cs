using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace LC.Model.BMS.BLL
{
    public class tblProducts : LC.Model.BMS.DAL._tblProducts
    {
        public void GetProducts(int subcatid)
        {
            string query = string.Empty;
            query += "select p.* from dbo.tblProducts p where p.fkSubCategoryID =  " + subcatid;
            this.LoadFromRawSql(query);
        }
        public void GetProductsForOrders(string pids)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@ProductID", pids);
            this.LoadFromSql("GetProductForOrders", Params);
        }
        public void getActiveProduct(int subcat)
        {
            this.Where.FkSubCategoryID.Value = subcat;
            this.Where.IsActive.Value = true;
            this.Query.Load();
        }
        public void GetProductsForGrid(int departmentid)
        {
            string query = string.Empty;

            query += " select distinct bas.pkbasecategoryid,sub.pksubcategoryid,p.pkproductid, bas.CatagoryName as BaseCat, ";
            query += " sub.csubcategoryname as SubCat,p.sproductname as Product, pqr.isActive,v.vat,pqr.pkProductPackingQuantityRelID ,pp.pName, pp.qName  ";
            query += " from tblproducts p ";
            query += " left join dbo.tblSubCategories sub on sub.pkSubCategoryID = p.fksubcategoryid ";
            query += " left join dbo.tblBaseCategories bas on bas.pkBaseCategoryID = sub.fkBaseCategoryID ";
            query += " left join dbo.tblVAT v on v.pkvatid = bas.fkvatid ";
            query += " left join dbo.tblProductPackingQuantityRel pqr on pqr.fkproductid = p.pkproductid ";
            query += " left join dbo.tblProductPackages pp on pp.pkProductPackageID = pqr.fkProductPackageID ";
            query += " where bas.fkdepartmentid  = " + departmentid + " and pp.isActive = 1 ";
            query += " order by bas.CatagoryName, sub.csubcategoryname, p.sproductname";

            this.LoadFromRawSql(query);
        }

        public void checkProductExist(string productName, int subcatid)
        {
            string query = string.Empty;
            query += " select p.pkproductid from tblproducts p where p.sproductname = '" + productName + "' and p.fkSubCategoryID = " + subcatid;
            this.LoadFromRawSql(query);
        }
        public void checkProductExist_for_Updating(int pid, string productName)
        {
            string query = string.Empty;
            query += "select p.pkproductid from tblproducts p where p.pkproductid != " + pid + " and p.sproductname = '" + productName + "'";
            this.LoadFromRawSql(query);
        }
        public void checkProduct_Packing_Quantity(int productid, int packingid)
        {
            string query = string.Empty;
            query += " select pqr.* from  dbo.tblProductPackingQuantityRel pqr ";
            query += " where pqr.fkproductid = " + productid + " ";
            query += " and pqr.fkProductPackageID = " + packingid;
            this.LoadFromRawSql(query);
        }
        public void checkProduct_Packing_Quantity_for_updating(int pqrid, int productid, int packingid)
        {
            string query = string.Empty;
            query += " select pqr.* from  dbo.tblProductPackingQuantityRel pqr  ";
            query += " where pqr.fkproductid  = " + productid + " and pqr.pkProductPackingQuantityRelID != " + pqrid + " ";
            query += " and pqr.fkProductPackageID = " + packingid;
            this.LoadFromRawSql(query);
        }
        public void GetProductsForGrid_filter(int departmentid, string filter)
        {
            string query = string.Empty;
            query += " select distinct bas.pkbasecategoryid,sub.pksubcategoryid,p.pkproductid,rel.pkProductPackingQuantityRelID,";
            query += " bas.CatagoryName as BaseCat,sub.csubcategoryname as SubCat,p.sproductname as Product, p.isActive,v.vat ";
            query += " ,pp.pName, pp.qName ";
            query += " from tblproducts p INNER JOIN ";
            query += " dbo.tblProductPackingQuantityRel rel on rel.fkProductID = p.pkproductid and rel.isactive = 1 inner join ";
            query += " dbo.tblSubCategories sub on sub.pkSubCategoryID = p.fksubcategoryid inner join ";
            query += " dbo.tblBaseCategories bas on bas.pkBaseCategoryID = sub.fkBaseCategoryID inner join ";
            query += " dbo.tblSupplierProducts sp on sp.fkproductid = p.pkproductid inner join  ";
            query += " dbo.tblVAT v on v.pkvatid = bas.fkvatid left join";
            query += " dbo.tblProductPackages pp on pp.pkProductPackageID = rel.fkProductPackageID";
            query += " where bas.fkdepartmentid  = " + departmentid + " and " + " pp.isActive = 1 ";
            if (filter != "")
                query += " " + filter + " ";
            query += " order by bas.pkbasecategoryid, sub.pksubcategoryid, p.pkproductid ";
            this.LoadFromRawSql(query);
        }
        public void GetProducts_Autocomplete(int departmentid, string autocomlete)
        {
            string query = string.Empty;
            query += " select distinct p.sproductname from tblproducts p INNER JOIN ";
            query += " dbo.tblProductPackingQuantityRel rel on rel.fkProductID = p.pkproductid and rel.isactive = 1 inner join ";
            query += " dbo.tblSubCategories sub on sub.pkSubCategoryID = p.fksubcategoryid inner join ";
            query += " dbo.tblBaseCategories bas on bas.pkBaseCategoryID = sub.fkBaseCategoryID inner join ";
            query += " dbo.tblSupplierProducts sp on sp.fkproductid = p.pkproductid inner join ";
            query += " dbo.tblVAT v on v.pkvatid = bas.fkvatid left join ";
            query += " dbo.tblProductPackages pp on pp.pkProductPackageID = rel.fkProductPackageID left join ";
            query += " dbo.tblProductQuantities pq on pq.pkProductQuantityID = rel.fkProductQuantityID ";
            query += " where bas.fkdepartmentid  = 1 ";
            if (autocomlete != "")
                query += autocomlete;
            query += " order by p.sproductname ";
            this.LoadFromRawSql(query);
        }

        public void getOrderProductsIDs(string orderid)
        {
            string query = string.Empty;
            query += " select distinct od.fkproductid as pkProductid from dbo.tblBaseOrder b inner join ";
            query += " dbo.tblOrders o on b.pkBaseOrderID = o.fkBaseOrderID inner join ";
            query += " dbo.tblOrderDetail od on od.fkOrderID = o.pkorderid ";
            query += " where b.SessionOrderID = '" + orderid + "' ";
            this.LoadFromRawSql(query);
        }

        public void getOrderPackingSupplier(string orderid, int productid)
        {
            string query = string.Empty;
            query += " select distinct o.fksupplierid, o.ordersubtotal, od.fkproductpackageid, od.quantity, od.subtotal,o.deliverytime ";
            query += " from dbo.tblBaseOrder b inner join ";
            query += " dbo.tblOrders o on b.pkBaseOrderID = o.fkBaseOrderID inner join ";
            query += " dbo.tblOrderDetail od on od.fkOrderID = o.pkorderid ";
            query += " where b.SessionOrderID = '" + orderid + "' and od.fkproductid = " + productid;
            this.LoadFromRawSql(query);
        }
        public void getDeliveryTime(string orderid, int supplierid)
        {
            string query = string.Empty;
            query += " select distinct o.deliverytime, o.ordernote,o.OrderNoteByReceiver,oi.InvoiceNumber,oi.InvoiceAmount,oi.NonInvoiceAmount from dbo.tblBaseOrder b inner join ";
            query += " dbo.tblOrders o on b.pkBaseOrderID = o.fkBaseOrderID inner join ";
            query += " dbo.tblOrderDetail od on od.fkOrderID = o.pkorderid inner join ";
            query += " dbo.tblOrderInvoices oi on oi.fkOrderID = o.pkorderid ";
            query += " where b.SessionOrderID = '" + orderid + "' and o.fksupplierid = " + supplierid;
            this.LoadFromRawSql(query);
        }
        public void GetProductsWeekStats(DateTime weekstart, DateTime weekend, int weeknumber, int productid, int baseid, int subid)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@weekStart", weekstart);
            Params.Add("@weekEnd", weekend);
            Params.Add("@weeknumber", weeknumber);
            Params.Add("@Productid", productid);
            Params.Add("@baseID", baseid);
            Params.Add("@subid", subid);

            this.LoadFromSql("spGetProductForStats", Params);
        }
    }
}
