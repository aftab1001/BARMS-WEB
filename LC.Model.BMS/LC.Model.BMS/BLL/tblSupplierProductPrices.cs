using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Model.BMS.BLL
{
    public class tblSupplierProductPrices : LC.Model.BMS.DAL._tblSupplierProductPrices
    {
        public void getSupplierProductPrices(int supplierid)
        {
            string query = string.Empty;
            query += " select distinct s.pksupplierid,rel.pkProductPackingQuantityRelID,p.pkproductid, p.sProductName,pp.pkProductPackageID, pp.pName,pp.qName, spp.fkSupplierID ";
            query += " from  dbo.tblSupplier s  inner join ";
            query += " dbo.tblSupplierProducts sp on sp.fksupplierid = s.pksupplierid inner join ";
            query += " dbo.tblProducts p on p.pkproductid = sp.fkproductid inner join ";
            query += " dbo.tblProductPackingQuantityRel rel on rel.fkproductid= p.pkproductid inner join ";
            query += " dbo.tblProductPackages pp on pp.pkProductPackageID = rel.fkProductPackageID inner join ";
            query += " dbo.tblSupplierProductPrices spp on spp.fkProductPackingQuantityRelID = rel.pkProductPackingQuantityRelID and s.pksupplierid = spp.fksupplierid ";
            query += " where s.pksupplierid =" + supplierid + " and pp.isActive = 1";
            this.LoadFromRawSql(query);
        }

        public void getSupplierProductPrices_Filter(int supplierid, string pname)
        {
            string query = string.Empty;
            query += " select distinct s.pksupplierid,rel.pkProductPackingQuantityRelID,p.sProductName,pp.pName,pp.qName, spp.fkSupplierID ";
            query += " from  dbo.tblSupplier s  inner join ";
            query += " dbo.tblSupplierProducts sp on sp.fksupplierid = s.pksupplierid inner join ";
            query += " dbo.tblProducts p on p.pkproductid = sp.fkproductid inner join ";
            query += " dbo.tblProductPackingQuantityRel rel on rel.fkproductid= p.pkproductid inner join ";
            query += " dbo.tblProductPackages pp on pp.pkProductPackageID = rel.fkProductPackageID inner join ";
            query += " dbo.tblSupplierProductPrices spp on spp.fkProductPackingQuantityRelID = rel.pkProductPackingQuantityRelID and s.pksupplierid = spp.fksupplierid ";
            query += " where s.pksupplierid =" + supplierid + " and p.sProductName = '" + pname + "' and pp.isActive = 1";
            this.LoadFromRawSql(query);
        }

        public void GETRecord(int supplierid, int relid)
        {
            this.Where.FkSupplierID.Value = supplierid;
            this.Where.FkProductPackingQuantityRelID.Value = relid;
            this.Query.Load();
        }

        public void GetOldNewPrice(int supplierid, int relid)
        {
            string query = string.Empty;
            query += " select top(2) ssp.* from dbo.tblSupplierProductPrices ssp ";
            query += " where ssp.fkSupplierID = " + supplierid + " and ssp.fkProductPackingQuantityRelID = " + relid;
            query += " order by ssp.dCreatedDate desc";
            this.LoadFromRawSql(query);
        }

        public void GetOldNewPriceAll(int supplierid, int relid)
        {
            string query = string.Empty;
            query += " select ssp.* from dbo.tblSupplierProductPrices ssp ";
            query += " where ssp.fkSupplierID = " + supplierid + " and ssp.fkProductPackingQuantityRelID = " + relid;
            query += " order by ssp.dCreatedDate";
            this.LoadFromRawSql(query);
        }

        public void GetOldNewPriceAll_Filer(string start, string end, int supplierid, int relid)
        {
            string query = string.Empty;
            query += " select ssp.* from dbo.tblSupplierProductPrices ssp ";
            query += " where ssp.fkSupplierID = " + supplierid + " and ssp.fkProductPackingQuantityRelID = " + relid;
            query += " and (ssp.dModifiedDate > '" + start + "' and ssp.dModifiedDate < '" + end + "')";
            query += " order by ssp.dCreatedDate";
            this.LoadFromRawSql(query);
        }

    }
}
