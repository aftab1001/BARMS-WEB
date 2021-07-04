using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public  class tblOrderInvoices:LC.Model.BMS.DAL._tblOrderInvoices
    {
        public void GetInvoiceByOrder(int orderid)
        {
            this.Where.FkOrderID.Value = orderid;
            this.Query.Load();
        }
    }
}
