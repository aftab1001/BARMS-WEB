using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using LC.Model.BMS.BLL;
public partial class AccountManager_pdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GenerateHTMLForOrder(pdfhtml._SupplierName, pdfhtml._OrderStatus, pdfhtml._ProductNames.Substring(0, pdfhtml._ProductNames.Length - 1).Split(','), pdfhtml._Size.Substring(0, pdfhtml._Size.Length - 1).Split(','), pdfhtml._Qty.Substring(0, pdfhtml._Qty.Length - 1).Split(','), pdfhtml._comment, pdfhtml._deliveryTime);
        }
    }
    private void GenerateHTMLForOrder(string supplierName, string orderDate, string[] productNames, string[] size, string[] qty, string note, string deliveryTime)
    {
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
        cell.InnerHtml = supplierName + " " + orderDate;
        row.Cells.Add(cell);
        tblSampleText.Rows.Add(row);
        for (int i = 0; i < productNames.Length; i++)
        {
            HtmlTableRow tr = new HtmlTableRow();
            HtmlTableCell td = new HtmlTableCell();
            td.InnerHtml = productNames[i].ToString();
            HtmlTableCell td2 = new HtmlTableCell();
            td2.InnerHtml = "---------";
            HtmlTableCell td3 = new HtmlTableCell();
            td3.InnerHtml = qty[i] + " x " + size[i];
            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            tr.Cells.Add(td3);
            tblSampleText.Rows.Add(tr);
        }
    }
}
