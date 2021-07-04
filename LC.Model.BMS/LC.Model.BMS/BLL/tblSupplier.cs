using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace LC.Model.BMS.BLL
{
    public class tblSupplier : LC.Model.BMS.DAL._tblSupplier
    {
        public void GetDepartmentSuppliers(int departmentid)
        {
            string query = string.Empty;
            //query += " select s.* from dbo.tblSupplier s ";
            //query += " inner join dbo.tblDepartmentSuppliers ds on ds.fkSupplierID = s.pkSupplierID ";
            //query += " where ds.fkDepartmentID = " + departmentid;
            query += " select s.*,se.sEmail,sf.sFax,sp.phone,[dbo].[GetOweAmount](s.pksupplierid) as amount from dbo.tblSupplier s left join ";
            query += " dbo.tblSupplierEmails se on se.fkSupplierID = s.pkSupplierID and se.bisprimary = 1 left join ";
            query += " dbo.tblSupplierFaxes sf on sf.fkSupplierID = s.pkSupplierID  and sf.bisprimary = 1 left join ";
            query += " dbo.tblSupplierPhone sp on sp.fkSupplierID  = s.pkSupplierID  and sp.bisprimary = 1 inner join ";
            query += " dbo.tblDepartmentSuppliers ds on ds.fkSupplierID = s.pkSupplierID and ds.fkDepartmentID = " + departmentid + " order by s.sBrandName ";
            this.LoadFromRawSql(query);
        }

        public void GetDepartmentSuppliersByOrder(int departmentid, string order)
        {
            string query = string.Empty;
            //query += " select s.* from dbo.tblSupplier s ";
            //query += " inner join dbo.tblDepartmentSuppliers ds on ds.fkSupplierID = s.pkSupplierID ";
            //query += " where ds.fkDepartmentID = " + departmentid;
            query += " select s.*,se.sEmail,sf.sFax,sp.phone,[dbo].[GetOweAmount](s.pksupplierid) as amount from dbo.tblSupplier s left join ";
            query += " dbo.tblSupplierEmails se on se.fkSupplierID = s.pkSupplierID and se.bisprimary = 1 left join ";
            query += " dbo.tblSupplierFaxes sf on sf.fkSupplierID = s.pkSupplierID  and sf.bisprimary = 1 left join ";
            query += " dbo.tblSupplierPhone sp on sp.fkSupplierID  = s.pkSupplierID  and sp.bisprimary = 1 inner join ";
            query += " dbo.tblDepartmentSuppliers ds on ds.fkSupplierID = s.pkSupplierID and ds.fkDepartmentID = " + departmentid + " order by s.sBrandName " + order;
            this.LoadFromRawSql(query);
        }
        public void GetSupplierInfoForProductHistory(int supplierid)
        {
            string query = string.Empty;
            query += " select s.sLogo,s.sBrandName,sa.sAddressStreet,sa.sAddressTown, ";
            query += " sa.sAddressRegion,sa.sAddressPostCode,c.sCountry, sp.phone, ";
            query += " se.sEmail, sf.sFax, cp.pName,isnull(cp.Phone1,cp.Phone2) as cphone,  ";
            query += " isnull(cp.pEmail1,cp.pEmail2) as cemail,cp.fax ";
            query += "  from dbo.tblSupplier s ";
            query += " left join dbo.tblSupplierAddresses sa on sa.fksupplierid = s.pksupplierid and sa.bIsPrimary = 1 ";
            query += " left join dbo.tblCountries c on sa.fkAddressCountry = c.pkCountryID";
            query += " left join dbo.tblSupplierPhone sp on sp.fksupplierid = s.pksupplierid and sp.bIsPrimary = 1";
            query += " left join dbo.tblSupplierEmails se on se.fksupplierid = s.pksupplierid and se.bIsPrimary = 1";
            query += " left join dbo.tblSupplierFaxes sf on sf.fksupplierid = s.pksupplierid and sf.bIsPrimary = 1";
            query += " left join dbo.tblContactPeople cp on cp.fkSuplierID= s.pksupplierid and cp.bIsPrimary = 1";
            query += " where s.pksupplierid = " + supplierid;
            this.LoadFromRawSql(query);
        }
        public void GetOweAmount(int supplierid, int departmentid)
        {
            string query = string.Empty;
            //query += " select s.* from dbo.tblSupplier s ";
            //query += " inner join dbo.tblDepartmentSuppliers ds on ds.fkSupplierID = s.pkSupplierID ";
            //query += " where ds.fkDepartmentID = " + departmentid;
            query += " select s.pksupplierid,[dbo].[GetOweAmount](s.pksupplierid) as amount from dbo.tblSupplier s left join ";
            query += " dbo.tblSupplierEmails se on se.fkSupplierID = s.pkSupplierID and se.bisprimary = 1 left join ";
            query += " dbo.tblSupplierFaxes sf on sf.fkSupplierID = s.pkSupplierID  and sf.bisprimary = 1 left join ";
            query += " dbo.tblSupplierPhone sp on sp.fkSupplierID  = s.pkSupplierID  and sp.bisprimary = 1 inner join ";
            query += " dbo.tblDepartmentSuppliers ds on ds.fkSupplierID = s.pkSupplierID and ds.fkDepartmentID = " + departmentid + " where s.pksupplierid = " + supplierid + " order by s.sBrandName ";
            this.LoadFromRawSql(query);
        }
        public void GetSupplyWeekStats(DateTime weekstart, DateTime weekend, int supplierid)
        {
            ListDictionary Params = new ListDictionary();
            Params.Add("@weekStart", weekstart);
            Params.Add("@weekEnd", weekend);
            Params.Add("@supplierid", supplierid);
            this.LoadFromSql("spSupplyForStats", Params);
        }

        public void getSupplierOrdersForStats(DateTime date, int supplierid)
        {
            string query = string.Empty;
            query += " select distinct p.sProductName,pp.pName,pp.qName, od.ProudctPrice,od.vat,   od.afterVat, od.Quantity,od.subtotal, b.dCreatedDate  ";
            query += " from tblBaseOrder b ";
            query += " inner join  dbo.tblOrders o on o.fkBaseOrderID = b.pkBaseOrderID ";
            query += " inner join  dbo.tblOrderDetail od  on od.fkOrderID = o.pkOrderID ";
            query += " inner join  dbo.tblProducts p on p.pkproductid = od.fkProductID ";
            query += " inner join  dbo.tblProductPackages pp on pp.pkProductPackageID = od.fkProductPackageID ";
            query += " where b.dCreatedDate = '" + date + "' ";
            if (supplierid != 0)
                query += " and o.fkSupplierID = 2 ";

            this.LoadFromRawSql(query);
        }




    }

}
