using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

using System.Drawing;
using LC.Model.BMS.BLL;
using ExpertPdf.HtmlToPdf;

public partial class Admin_PrintStats : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["html"] != "" && Session["html"] != null)
            {
                content.InnerHtml = Session["html"].ToString();
            }
            else
            {
                content.InnerHtml = GetHtml.getDivContent();
            }
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString["r"] == "ps")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "post", "$(function(){postback();});", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "printWorkshift", "window.print();", true);
            }
        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            PdfConverter pdfConverter = new PdfConverter();
            // string urlToConvert = tbURL.Text.Trim();
            // Label1.Text = urlToConvert;
            string thisPageURL = HttpContext.Current.Request.Url.AbsoluteUri;

            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Best;
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;

            pdfConverter.PdfDocumentOptions.ShowHeader = true;
            pdfConverter.PdfHeaderOptions.HeaderText = "Statistics";
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
        catch (Exception ex)
        { }
    }
}
