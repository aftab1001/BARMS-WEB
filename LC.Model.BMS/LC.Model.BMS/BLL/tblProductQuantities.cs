using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
   public class tblProductQuantities:LC.Model.BMS.DAL._tblProductQuantities
    {
       public void GetQuantityMaxOrderNum()
       {
           string query = string.Empty;
           query = "select  isnull(max(qOrder),'0') as orderNum from dbo.tblProductQuantities";
           this.LoadFromRawSql(query);
       }
       public void GetQuantityForSortLesser(int newSortNo, int oldSortNo)
       {
           string query = string.Empty;
           query = "select q.* from  dbo.tblProductQuantities q where q.qOrder >= " + newSortNo + " and q.qOrder<= " + oldSortNo + "";
           this.LoadFromRawSql(query);
       }
       public void GetQuantityForSortGreater(int oldSortNo, int newSortNo)
       {
           string query = string.Empty;
           query = "select q.* from dbo.tblProductQuantities q where q.qOrder >= " + oldSortNo + " and q.qOrder<= " + newSortNo + "";
           this.LoadFromRawSql(query);
       }
    }
}
