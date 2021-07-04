using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblBaseOrder : LC.Model.BMS.DAL._tblBaseOrder
    {
        public void getBaseOrder(string sessionorderid)
        {
            this.Where.SessionOrderID.Value = sessionorderid;
            this.Query.Load();
        }
        public void GetUniqueOrder(int supplierid)
        {
            string query = string.Empty;
            //query += " select distinct b.*,o.* ,inv.InvoiceNumber, inv.InvoiceAmount from dbo.tblBaseOrder b inner join ";
            //query += " dbo.tblOrders o on o.fkBaseOrderID = b.pkbaseorderid left join ";
            //query += " dbo.tblOrderInvoices inv on inv.fkOrderID = o.pkorderid ";
            //query += " where o.fkSupplierID = " + supplierid;

            query += " select distinct ";
            query += " b.pkBaseOrderID,b.SessionOrderID,b.dCreatedDate, ";
            query += " [dbo].[GetSubtotalBy_BaseOrder_&_Supplierid](" + supplierid + ",b.pkBaseOrderID) as subtotal, ";
            query += " [dbo].[GetPaidAmountByBaseOrder](" + supplierid + ",b.pkBaseOrderID) as paid, ";
            query += " [dbo].[GetDueAmountByBaseOrder](" + supplierid + ",b.pkBaseOrderID) as due ";

            query += " from dbo.tblBaseOrder b  ";
            query += " inner join  dbo.tblOrders o on o.fkBaseOrderID = b.pkbaseorderid  ";
            query += " inner join  dbo.tblOrderDetail od  on od.fkOrderID = o.pkOrderID ";
            query += " inner join  dbo.tblProducts p on p.pkproductid = od.fkProductID ";
            query += " inner join  dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID  ";
            query += " left join  dbo.tblOrderInvoices inv on inv.fkOrderID = o.pkorderid  where o.fkSupplierID = " + supplierid + " order by b.dCreatedDate desc";
            this.LoadFromRawSql(query);
        }

        public void GetUniqueOrderForInvoices(int supplierid, int baseorderid)
        {
            string query = string.Empty;
            //query += " select distinct b.*,o.* ,inv.InvoiceNumber, inv.InvoiceAmount from dbo.tblBaseOrder b inner join ";
            //query += " dbo.tblOrders o on o.fkBaseOrderID = b.pkbaseorderid left join ";
            //query += " dbo.tblOrderInvoices inv on inv.fkOrderID = o.pkorderid ";
            //query += " where o.fkSupplierID = " + supplierid;

            query += " select distinct ";
            query += " inv.InvoiceNumber, inv.InvoiceAmount, inv.NonInvoiceAmount ";
            query += " from dbo.tblOrders o  ";
            query += " inner join  dbo.tblOrderDetail od  on od.fkOrderID = o.pkOrderID ";
            query += " inner join  dbo.tblProducts p on p.pkproductid = od.fkProductID ";
            query += " inner join  dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID  ";
            query += " inner join  dbo.tblOrderInvoices inv on inv.fkOrderID = o.pkorderid  where o.fkSupplierID = " + supplierid + " and o.fkbaseorderid = " + baseorderid + " and o.fkOrderStatusID = 2 order by inv.InvoiceNumber";
            this.LoadFromRawSql(query);
        }

        public void GetOrderByFiler(string statDate, string endDate, int supplierid)
        {
            string query = string.Empty;
            query += " select distinct ";
            query += " b.pkBaseOrderID,b.SessionOrderID,b.dCreatedDate, ";
            query += " [dbo].[GetSubtotalBy_BaseOrder_&_Supplierid](" + supplierid + ",b.pkBaseOrderID) as subtotal, ";
            query += " [dbo].[GetPaidAmountByBaseOrder](" + supplierid + ",b.pkBaseOrderID) as paid, ";
            query += " [dbo].[GetDueAmountByBaseOrder](" + supplierid + ",b.pkBaseOrderID) as due, ";
            query += " inv.InvoiceNumber, inv.InvoiceAmount  ";
            query += " from dbo.tblBaseOrder b  ";
            query += " inner join  dbo.tblOrders o on o.fkBaseOrderID = b.pkbaseorderid  ";
            query += " inner join  dbo.tblOrderDetail od  on od.fkOrderID = o.pkOrderID ";
            query += " inner join  dbo.tblProducts p on p.pkproductid = od.fkProductID ";
            query += " inner join  dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID ";
            query += " left join  dbo.tblOrderInvoices inv on inv.fkOrderID = o.pkorderid ";
            query += " where (b.dCreatedDate > '" + statDate + "' and  b.dCreatedDate < '" + endDate + "')";
            if (supplierid != 0)
                query += " and o.fkSupplierID = " + supplierid + " order by b.dCreatedDate desc";
            else
                query += " order by b.dCreatedDate desc ";
            this.LoadFromRawSql(query);
        }

        public void GetOrderDetail(string orderid, int supplierid)
        {
            string query = string.Empty;
            query += " select distinct p.sProductName,pp.pName,pp.qName, od.ProudctPrice,od.vat,  ";
            query += " od.afterVat, od.Quantity,od.subtotal, b.dCreatedDate    ";
            query += " from tblBaseOrder b inner join ";
            query += " dbo.tblOrders o on o.fkBaseOrderID = b.pkBaseOrderID inner join ";
            query += " dbo.tblOrderDetail od  on od.fkOrderID = o.pkOrderID inner join ";
            query += " dbo.tblProducts p on p.pkproductid = od.fkProductID inner join ";
            query += " dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID ";
            query += " where b.SessionOrderID = '" + orderid + "' and o.fkSupplierID = " + supplierid;

            this.LoadFromRawSql(query);
        }

        public void getOrdersForStatistics(int day, int month, int year)
        {
            string query = string.Empty;
            query += " select distinct b.* from tblBaseOrder b ";
            query += " inner join  dbo.tblOrders o on o.fkBaseOrderID = b.pkBaseOrderID  ";
            query += " inner join  dbo.tblOrderDetail od  on od.fkOrderID = o.pkOrderID ";
            query += " inner join  dbo.tblProducts p on p.pkproductid = od.fkProductID ";
            query += " inner join  dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID ";
            query += " where month(b.dmodifieddate) = " + month + " and day(b.dmodifieddate) = " + day + " and year(b.dmodifieddate) = " + year;
            this.LoadFromRawSql(query);
        }
        public void getorderbysessionfor_Stats(int baseorderid)
        {
            string query = string.Empty;
            query += " select distinct p.sProductName,pp.pName,pp.qName, od.ProudctPrice,od.vat,  ";
            query += " od.afterVat, od.Quantity,od.subtotal, b.dCreatedDate     ";
            query += " from tblBaseOrder b ";
            query += " inner join  dbo.tblOrders o on o.fkBaseOrderID = b.pkBaseOrderID ";
            query += " inner join  dbo.tblOrderDetail od  on od.fkOrderID = o.pkOrderID ";
            query += " inner join  dbo.tblProducts p on p.pkproductid = od.fkProductID ";
            query += " inner join  dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID  ";
            query += " where b.pkBaseOrderID = " + baseorderid;
            this.LoadFromRawSql(query);
        }

        public void GetUniqueOrderByDate(int day, int month, int year)
        {
            string query = string.Empty;
            //query += " select distinct b.*,o.* ,inv.InvoiceNumber, inv.InvoiceAmount from dbo.tblBaseOrder b inner join ";
            //query += " dbo.tblOrders o on o.fkBaseOrderID = b.pkbaseorderid left join ";
            //query += " dbo.tblOrderInvoices inv on inv.fkOrderID = o.pkorderid ";
            //query += " where o.fkSupplierID = " + supplierid;

            //query += " select distinct ";
            //query += " b.pkBaseOrderID,b.SessionOrderID,b.dCreatedDate, ";
            //query += " [dbo].[GetSubtotalBy_BaseOrder_&_Supplierid](" + supplierid + ",b.pkBaseOrderID) as subtotal, ";
            //query += " [dbo].[GetPaidAmountByBaseOrder](" + supplierid + ",b.pkBaseOrderID) as paid, ";
            //query += " [dbo].[GetDueAmountByBaseOrder](" + supplierid + ",b.pkBaseOrderID) as due ";

            //query += " from dbo.tblBaseOrder b  ";
            //query += " inner join  dbo.tblOrders o on o.fkBaseOrderID = b.pkbaseorderid  ";
            //query += " inner join  dbo.tblOrderDetail od  on od.fkOrderID = o.pkOrderID ";
            //query += " inner join  dbo.tblProducts p on p.pkproductid = od.fkProductID ";
            //query += " inner join  dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID  ";
            //query += " left join  dbo.tblOrderInvoices inv on inv.fkOrderID = o.pkorderid  where month(b.dmodifieddate) = " + day + " and day(b.dmodifieddate) = " + month + " and year(b.dmodifieddate) = " + year + " order by b.dCreatedDate desc";
            //this.LoadFromRawSql(query);
        }
        public void checkorderstatus_for_permission(int baseorder)
        {
            string query = string.Empty;
            query += " select o.* from  dbo.tblOrders o  where o.fkBaseOrderID = " + baseorder + " and o.fkorderstatusid = 1 ";
            this.LoadFromRawSql(query);

        }

    }
}
