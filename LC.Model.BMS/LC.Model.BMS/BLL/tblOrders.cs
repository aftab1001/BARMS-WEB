using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblOrders : LC.Model.BMS.DAL._tblOrders
    {
        public void GetOrderHistory()
        {
            string query = string.Empty;
            query += " select distinct b.pkBaseOrderID,b.SessionOrderID,b.dCreatedDate,b.CashAmount,b.OrderReceivedByUser, s.sBrandName, o.fkOrderStatusID, ";
            query += " [dbo].[GetSubtotalBy_BaseOrder_&_Supplierid](s.pksupplierid,b.pkBaseOrderID) as subtotal, ";
            query += " [dbo].[GetPaidAmountByBaseOrder](s.pksupplierid,b.pkBaseOrderID) as paid, ";
            query += " [dbo].[GetDueAmountByBaseOrder](s.pksupplierid,b.pkBaseOrderID) as due ";
            query += " from dbo.tblOrders o ";
            query += " inner join dbo.tblBaseOrder b on b.pkbaseorderid = o.fkbaseorderid ";
            query += " inner join dbo.tblOrderDetail od on o.pkOrderID = od.fkOrderID ";
            query += " inner join dbo.tblSupplier s on o.fkSupplierID = s.pksupplierid ";
            query += " inner join  dbo.tblProducts p on p.pkproductid = od.fkProductID ";
            query += " inner join  dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID  ";
            query += " left join dbo.tblOrderInvoices i on i.fkorderid = o.pkorderid ";
            query += " order by b.dCreatedDate desc";
            this.LoadFromRawSql(query);
        }
        public void GetAllOrdersOnly()
        {
            string query = string.Empty;
            query += " select distinct b.pkBaseOrderID,b.SessionOrderID,b.dCreatedDate,b.CashAmount,b.OrderReceivedByUser,b.GrandSubtotal";
            query += " from dbo.tblOrders o ";
            query += " inner join dbo.tblBaseOrder b on b.pkbaseorderid = o.fkbaseorderid ";
            query += " inner join dbo.tblOrderDetail od on o.pkOrderID = od.fkOrderID ";
            query += " inner join dbo.tblSupplier s on o.fkSupplierID = s.pksupplierid ";
            query += " inner join  dbo.tblProducts p on p.pkproductid = od.fkProductID ";
            query += " inner join  dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID  ";
            query += " left join dbo.tblOrderInvoices i on i.fkorderid = o.pkorderid ";
            query += " order by b.dCreatedDate desc";
            this.LoadFromRawSql(query);
        }
        public void GetOrderHistoryFilter(string filter, int supplierid)
        {
            string query = string.Empty;
            query += " select distinct b.pkBaseOrderID,b.SessionOrderID,b.dCreatedDate, s.sBrandName, o.fkOrderStatusID, ";
            query += " [dbo].[GetSubtotalBy_BaseOrder_&_Supplierid](" + supplierid + ",b.pkBaseOrderID) as subtotal, ";
            query += " [dbo].[GetPaidAmountByBaseOrder](" + supplierid + ",b.pkBaseOrderID) as paid, ";
            query += " [dbo].[GetDueAmountByBaseOrder](" + supplierid + ",b.pkBaseOrderID) as due ";
            query += " from dbo.tblOrders o ";
            query += " inner join dbo.tblBaseOrder b on b.pkbaseorderid = o.fkbaseorderid ";
            query += " inner join dbo.tblOrderDetail od on o.pkOrderID = od.fkOrderID ";
            query += " inner join dbo.tblSupplier s on o.fkSupplierID = s.pksupplierid ";
            query += " inner join  dbo.tblProducts p on p.pkproductid = od.fkProductID ";
            query += " inner join  dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID  ";
            query += " left join dbo.tblOrderInvoices i on i.fkorderid = o.pkorderid ";
            if (filter != "")
                query += filter;
            query += " order by  b.dCreatedDate desc";
            this.LoadFromRawSql(query);
        }
        public void getOrdersByBaseOrder(int baseorderid)
        {
            this.Where.FkBaseOrderID.Value = baseorderid;
            this.Query.Load();
        }
        public void getOrdersBySessionOrderID(string orderid)
        {
            string query = string.Empty;
            query += "select o.fksupplierid from tblorders o inner join tblbaseorder b on b.pkBaseOrderID = o.fkBaseOrderID where b.SessionOrderID = '" + orderid + "'";
            this.LoadFromRawSql(query);
        }
        public void getOrderCash(string orderid)
        {
            string query = string.Empty;
            query += "select b.cashamount from tblorders o inner join tblbaseorder b on b.pkBaseOrderID = o.fkBaseOrderID where b.SessionOrderID = '" + orderid + "'";
            this.LoadFromRawSql(query);
        }
        public void GetOrder(int supplierid, string orderid)
        {
            string query = string.Empty;
            query += "select o.* from tblorders o inner join tblbaseorder b on b.pkBaseOrderID = o.fkBaseOrderID where b.SessionOrderID = '" + orderid + "' and o.fksupplierid = " + supplierid;
            this.LoadFromRawSql(query);
        }

    }
}
