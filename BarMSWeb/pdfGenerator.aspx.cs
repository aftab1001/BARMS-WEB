using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using LC.Model.BMS.BLL;
using ExpertPdf.HtmlToPdf;
using System.Drawing;

public partial class pdfGenerator : System.Web.UI.Page
{
    static double invoiceSum = 0.0;
    static double NonInvoiceSum = 0.0;
    int supplierid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["r"] == "s")
                {
                    GenerateHTMLForOrder(pdfhtml._SupplierName, pdfhtml._OrderStatus, pdfhtml._ProductNames.Substring(0, pdfhtml._ProductNames.Length - 1).Split(','), pdfhtml._Size.Substring(0, pdfhtml._Size.Length - 1).Split(':'), pdfhtml._Qty.Substring(0, pdfhtml._Qty.Length - 1).Split(','), pdfhtml._comment, pdfhtml._deliveryTime);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "post", "$(function(){postback();});", true);
                }
                else if (Request.QueryString["r"] == "m")
                {
                    GenerateHTMLForOrderAll(pdfhtml._SupplierNameAll.Substring(0, pdfhtml._SupplierNameAll.Length - 1).Split(','), pdfhtml._OrderStatusAll, pdfhtml._ProductNamesAll.Substring(0, pdfhtml._ProductNamesAll.Length - 1).Split(','), pdfhtml._SizeAll.Substring(0, pdfhtml._SizeAll.Length - 1).Split(':'), pdfhtml._QtyAll.Substring(0, pdfhtml._QtyAll.Length - 1).Split(','), pdfhtml._commentAll.Substring(0, pdfhtml._commentAll.Length - 1).Split(','), pdfhtml._deliveryTimeAll.Substring(0, pdfhtml._deliveryTimeAll.Length - 1).Split(','), pdfhtml._spSeperator.Substring(0, pdfhtml._spSeperator.Length - 1).Split(','));
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "post", "$(function(){postback();});", true);
                }
                else if (Request.QueryString["r"] == "ps")
                {
                    GenerateHTMLForOrder(pdfhtml._SupplierName, pdfhtml._OrderStatus, pdfhtml._ProductNames.Substring(0, pdfhtml._ProductNames.Length - 1).Split(','), pdfhtml._Size.Substring(0, pdfhtml._Size.Length - 1).Split(':'), pdfhtml._Qty.Substring(0, pdfhtml._Qty.Length - 1).Split(','), pdfhtml._comment, pdfhtml._deliveryTime);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "printWorkshift", "window.print();", true);
                }
                else if (Request.QueryString["r"] == "pm")
                {
                    GenerateHTMLForOrderAll(pdfhtml._SupplierNameAll.Substring(0, pdfhtml._SupplierNameAll.Length - 1).Split(','), pdfhtml._OrderStatusAll, pdfhtml._ProductNamesAll.Substring(0, pdfhtml._ProductNamesAll.Length - 1).Split(','), pdfhtml._SizeAll.Substring(0, pdfhtml._SizeAll.Length - 1).Split(':'), pdfhtml._QtyAll.Substring(0, pdfhtml._QtyAll.Length - 1).Split(','), pdfhtml._commentAll.Substring(0, pdfhtml._commentAll.Length - 1).Split(','), pdfhtml._deliveryTimeAll.Substring(0, pdfhtml._deliveryTimeAll.Length - 1).Split(','), pdfhtml._spSeperator.Substring(0, pdfhtml._spSeperator.Length - 1).Split(','));
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "printWorkshift", "window.print();", true);
                }
                else if (Request.QueryString["r"] == "pdfo")
                {
                    GenerateHTMLForSupplierOrder(pdfhtml._SupplierName,pdfhtml._InvoiceSum, pdfhtml._NonInvoiceSum, pdfhtml._TotalOrder, pdfhtml._Paid, pdfhtml._Due,
                         pdfhtml._OrderNo.Substring(0, pdfhtml._OrderNo.Length - 1).Split(','),
                         pdfhtml._Date.Substring(0, pdfhtml._Date.Length - 1).Split(','),
                         pdfhtml._InvoiceNo.Substring(0, pdfhtml._InvoiceNo.Length - 1).Split(','),
                         pdfhtml._InvSum.Substring(0, pdfhtml._InvSum.Length - 1).Split(','),
                         pdfhtml._NonInvSum.Substring(0, pdfhtml._NonInvSum.Length - 1).Split(','),
                         pdfhtml._Subtotal.Substring(0, pdfhtml._Subtotal.Length - 1).Split(':'),
                         pdfhtml._PaidAmount.Substring(0, pdfhtml._PaidAmount.Length - 1).Split(':'),
                         pdfhtml._DueAmount.Substring(0, pdfhtml._DueAmount.Length - 1).Split(':'),
                         pdfhtml._sProduct.Substring(0, pdfhtml._sProduct.Length - 1).Split(','),
                         pdfhtml._sPacking.Substring(0, pdfhtml._sPacking.Length - 1).Split(','),
                         pdfhtml._sQuantity.Substring(0, pdfhtml._sQuantity.Length - 1).Split(','),
                         pdfhtml._sPrice.Substring(0, pdfhtml._sPrice.Length - 1).Split(':'),
                         pdfhtml._sVat.Substring(0, pdfhtml._sVat.Length - 1).Split(':'),
                         pdfhtml._sAmount.Substring(0, pdfhtml._sAmount.Length - 1).Split(','),
                         pdfhtml._sSubtotal.Substring(0, pdfhtml._sSubtotal.Length - 1).Split(':'),
                         pdfhtml._OSep.Substring(0, pdfhtml._OSep.Length - 1).Split(',')
                         );


                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "post", "$(function(){postback();});", true);
                }
                else if (Request.QueryString["r"] == "printo")
                {
                    GenerateHTMLForSupplierOrder(pdfhtml._SupplierName,pdfhtml._InvoiceSum, pdfhtml._NonInvoiceSum, pdfhtml._TotalOrder, pdfhtml._Paid, pdfhtml._Due,
                        pdfhtml._OrderNo.Substring(0, pdfhtml._OrderNo.Length - 1).Split(','),
                        pdfhtml._Date.Substring(0, pdfhtml._Date.Length - 1).Split(','),
                        pdfhtml._InvoiceNo.Substring(0, pdfhtml._InvoiceNo.Length - 1).Split(','),
                        pdfhtml._InvSum.Substring(0, pdfhtml._InvSum.Length - 1).Split(','),
                        pdfhtml._NonInvSum.Substring(0, pdfhtml._NonInvSum.Length - 1).Split(','),
                        pdfhtml._Subtotal.Substring(0, pdfhtml._Subtotal.Length - 1).Split(':'),
                        pdfhtml._PaidAmount.Substring(0, pdfhtml._PaidAmount.Length - 1).Split(':'),
                        pdfhtml._DueAmount.Substring(0, pdfhtml._DueAmount.Length - 1).Split(':'),
                        pdfhtml._sProduct.Substring(0, pdfhtml._sProduct.Length - 1).Split(','),
                        pdfhtml._sPacking.Substring(0, pdfhtml._sPacking.Length - 1).Split(','),
                        pdfhtml._sQuantity.Substring(0, pdfhtml._sQuantity.Length - 1).Split(','),
                        pdfhtml._sPrice.Substring(0, pdfhtml._sPrice.Length - 1).Split(':'),
                        pdfhtml._sVat.Substring(0, pdfhtml._sVat.Length - 1).Split(':'),
                        pdfhtml._sAmount.Substring(0, pdfhtml._sAmount.Length - 1).Split(','),
                        pdfhtml._sSubtotal.Substring(0, pdfhtml._sSubtotal.Length - 1).Split(':'),
                        pdfhtml._OSep.Substring(0, pdfhtml._OSep.Length - 1).Split(',')
                        );
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "printWorkshift", "window.print();", true);
                }
            }
        }
        else
        {

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
    private void GenerateHTMLForOrderAll(string[] supplierName, string orderDate, string[] productNames, string[] size, string[] qty, string[] note, string[] deliveryTime, string[] seperator)
    {
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
        cell.InnerHtml = orderDate;
        row.Cells.Add(cell);

        tblSampleText.Rows.Add(row);

        for (int j = 0; j < supplierName.Length; j++)
        {
            HtmlTableRow rowInner = new HtmlTableRow();
            HtmlTableCell cellInner = new HtmlTableCell();
            cellInner.InnerHtml = "<div style='font-weight:bold;color:White;background-color:Black;width:auto;padding:3px;'>" + supplierName[j].ToString() + "</div>";
            rowInner.Cells.Add(cellInner);

            tblSampleText.Rows.Add(rowInner);
            /*
            HtmlTableRow rowBreak = new HtmlTableRow();
            HtmlTableCell cellBreak = new HtmlTableCell();
            cellBreak.InnerHtml = "<br/>";
            rowBreak.Cells.Add(cellBreak);
            tblSampleText.Rows.Add(rowBreak);
            */
            for (int i = 0; i < productNames.Length; i++)
            {
                if (seperator[i] == supplierName[j])
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

            HtmlTableRow rowBreakAfter = new HtmlTableRow();
            HtmlTableCell cellBreakAfter = new HtmlTableCell();
            cellBreakAfter.InnerHtml = "<br/><br/>";
            rowBreakAfter.Cells.Add(cellBreakAfter);
            tblSampleText.Rows.Add(rowBreakAfter);
        }
    }
    private void GenerateHTMLForSupplierOrder(string SupplierName, string InvoiceSum, string nonInvoiceSum, string TotalOrder, string Paid, string Due,
                                                string[] OrderNo, string[] Date, string[] InvoceNo, string[] Invsum, string[] NonInvSum, string[] subtoal,
                                                string[] paidAmount, string[] DueAmount, string[] sProduct, string[] sPacking, string[] sQuantity, string[] SPrice,
                                                string[] sVat, string[] SAmount, string[] sSubtotal, string[] sep)
    {


        for (int i = 0; i < 6; i++)
        {
            if (i == 0)
                MakeRows("<b>Supplier Name</b>", "<b>"+SupplierName+"</b>");
            if (i == 1)
                MakeRows("Invoice Sum", InvoiceSum + " €");
            if (i == 2)
                MakeRows("NonInvoice Sum", nonInvoiceSum + " €");
            if (i == 3)
                MakeRows("Total Order", TotalOrder + " €");
            if (i == 4)
                MakeRows("Total Paid", Paid);
            if (i == 5)
                MakeRows("Total Due", Due);
        }


        for (int j = 0; j < OrderNo.Length; j++)
        {
            HtmlTableRow rowInner = new HtmlTableRow();
            HtmlTableCell cellInner = new HtmlTableCell();
            cellInner.InnerHtml = "<div style='font-weight:bold;color:White;background-color:Black;width:auto;padding:3px;'>" + OrderNo[j].ToString() + "</div>";
            rowInner.Cells.Add(cellInner);

            HtmlTableCell tdSpace = new HtmlTableCell();
            tdSpace.InnerHtml = "";
            rowInner.Cells.Add(tdSpace);

            HtmlTableCell cellInner2 = new HtmlTableCell();
            cellInner2.InnerHtml = "<div style='font-weight:bold; text-decoration: underline;'>" + Date[j].ToString() + "</div>";
            rowInner.Cells.Add(cellInner2);

            tblSampleText.Rows.Add(rowInner);

            for (int i = 0; i < sProduct.Length; i++)
            {
                if (sep[i] == OrderNo[j])
                {
                    HtmlTableRow tr = new HtmlTableRow();
                    HtmlTableCell td = new HtmlTableCell();
                    td.InnerHtml = sProduct[i].ToString();
                    HtmlTableCell td2 = new HtmlTableCell();
                    td2.InnerHtml = "---------";
                    HtmlTableCell td3 = new HtmlTableCell();
                    td3.InnerHtml = "<div style='width: 100px;'>" + SAmount[i] + " x " + sPacking[i] + " " + sQuantity[i] + " " + "</div>";
                    HtmlTableCell td4 = new HtmlTableCell();
                    td4.InnerHtml = "<div style='width: 75px;'> Vat:" + sVat[i] + " % </div>";
                    HtmlTableCell td5 = new HtmlTableCell();
                    td5.InnerHtml = " Subtotal:" + sSubtotal[i] + " €";

                    tr.Cells.Add(td);
                    tr.Cells.Add(td2);
                    tr.Cells.Add(td3);
                    tr.Cells.Add(td4);
                    tr.Cells.Add(td5);

                    tblSampleText.Rows.Add(tr);
                }
            }

            HtmlTableRow rowBreakAfter = new HtmlTableRow();
            HtmlTableCell cellBreakAfter = new HtmlTableCell();
            cellBreakAfter.InnerHtml = "<br/><br/>";
            rowBreakAfter.Cells.Add(cellBreakAfter);
            tblSampleText.Rows.Add(rowBreakAfter);
        }
    }

    private void MakeRows(string title, string value)
    {
        HtmlTableRow row = new HtmlTableRow();
        HtmlTableCell cell = new HtmlTableCell();
        cell.InnerHtml = title + ":";
        row.Cells.Add(cell);

        HtmlTableCell cellInv = new HtmlTableCell();
        cellInv.InnerHtml = value;
        row.Cells.Add(cellInv);
        tblSampleText.Rows.Add(row);
    }
    private void GenerateSupplierOrder()
    {
        Serialization srl = new Serialization();
        if (Session["html"] != null)
        {
            HtmlTableRow tr = new HtmlTableRow();
            HtmlTableCell tc = new HtmlTableCell();
            tc.InnerHtml = srl.DeSerializeHtml((MemoryStream)Session["html"]); ;
            tr.Controls.Add(tc);
            tblSampleText.Controls.Add(tr);
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btn_Click(object sender, EventArgs e)
    {
        PdfConverter pdfConverter = new PdfConverter();
        // string urlToConvert = tbURL.Text.Trim();
        // Label1.Text = urlToConvert;
        string thisPageURL = HttpContext.Current.Request.Url.AbsoluteUri;

        pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
        pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
        pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;

        pdfConverter.PdfDocumentOptions.ShowHeader = true;
        pdfConverter.PdfHeaderOptions.HeaderText = "Order Detail";
        pdfConverter.PdfHeaderOptions.HeaderTextColor = Color.BurlyWood;
        //pdfConverter.PdfHeaderOptions.HeaderDescriptionText = string.Empty;
        pdfConverter.PdfHeaderOptions.DrawHeaderLine = false;


        pdfConverter.PdfDocumentOptions.ShowFooter = true;
        pdfConverter.PdfFooterOptions.FooterText = "West";
        pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue;
        pdfConverter.PdfFooterOptions.DrawFooterLine = false;
        pdfConverter.PdfFooterOptions.PageNumberText = "Page";
        pdfConverter.PdfFooterOptions.ShowPageNumber = true;


        pdfConverter.PdfDocumentOptions.LeftMargin = 5;
        pdfConverter.PdfDocumentOptions.RightMargin = 5;
        pdfConverter.PdfDocumentOptions.TopMargin = 5;
        pdfConverter.PdfDocumentOptions.BottomMargin = 5;
        pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = true;

        pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;
        pdfConverter.PdfDocumentInfo.AuthorName = "West";

        //byte[] pdfBytes = pdfConverter.GetPdfBytesFromUrl(urlToConvert);
        byte[] pdfBytes = pdfConverter.GetPdfBytesFromUrl(thisPageURL);


        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Type", "binary/octet-stream");
        response.AddHeader("Content-Disposition",
            "attachment; filename=ConversionResult.pdf; size=" + pdfBytes.Length.ToString());
        response.Flush();
        response.BinaryWrite(pdfBytes);
        response.Flush();
        response.End();
    }

}
