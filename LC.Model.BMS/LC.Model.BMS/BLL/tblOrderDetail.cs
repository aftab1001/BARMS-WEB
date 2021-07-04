using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblOrderDetail : LC.Model.BMS.DAL._tblOrderDetail
    {
        public void GetOrderDetail(int orderid)
        {
            this.Where.FkOrderID.Value = orderid;
            this.Query.Load();
        }
        public void GetOrderDetailForUpdate(string sessionOrder, int orderid, int productid)
        {
            string query = string.Empty;
            query += " select od.* from dbo.tblOrderDetail od inner join ";
            query += " tblorders o on o.pkorderid = od.fkorderid inner join ";
            query += " tblbaseorder b on b.pkbaseorderid = o.fkbaseorderid ";
            query += " where od.fkOrderID = " + orderid + " and od.fkProductID = " + productid + " and b.SessionOrderID = '" + sessionOrder + "'";
            this.LoadFromRawSql(query);
        }
    }
}
