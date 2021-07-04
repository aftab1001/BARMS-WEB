using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Summary description for LineChart
/// </summary>
public class LineChart
{
    public LineChart()
    {

        //
        // TODO: Add constructor logic here
        //
        Graphicsg.Dispose();

        Bitmapb.Dispose();
    }
    public Bitmap Bitmapb;
    public string strTitle = "Default Title";
    public ArrayList ArrayListchartValues = new ArrayList();
    public float ftXorigin = 0, ftYorigin = 0;
    public float ftScaleX, ftScaleY;
    public float ftXdivs = 2, ftYdivs = 2;
    private int intWidth, intHeight;
    private Graphics Graphicsg;
    private Page Pagep;

    public string xaxis_label_first;
    public string xaxis_label_last;
    struct datapoint
    {
        public float ftx;
        public float fty;
        public bool bolvalid;
    }
    //initialize

    public LineChart(int intmyWidth, int intmyHeight, Page myPage)
    {
        intWidth = intmyWidth; intHeight = intmyHeight;
        ftScaleX = intmyWidth; ftScaleY = intmyHeight;
        Bitmapb = new Bitmap(intmyWidth, intmyHeight);
        Graphicsg = Graphics.FromImage(Bitmapb);
        Pagep = myPage;
    }
    public void SetXAxisLabel(string xFirst, string xLast)
    {
        xaxis_label_first = xFirst;
        xaxis_label_last = xLast;
    }
    public void AddValue(int intx, int inty)
    {
        datapoint myPoint;
        myPoint.ftx = intx;
        myPoint.fty = inty;
        myPoint.bolvalid = true;
        ArrayListchartValues.Add(myPoint);

    }
    public void Draw()
    {

        int inti;
        float ftx, fty, ftx0, fty0;
        string strmyLabel;
        Pen penblackPen = new Pen(Color.LightGray, 5);
        Brush BrushblackBrush = new SolidBrush(Color.Black);
        Font FontaxesFont = new Font("arial", 10);

        // like padding from top and left
        int intChartInset = 70;

        // Drawing Y-Axis BaseLine
        Point px1 = new Point(intChartInset, intChartInset);
        Point px2 = new Point(intChartInset, intHeight - intChartInset + 2);

        // Drawing X-Axis BaseLine
        Point py1 = new Point(intChartInset - 2, intHeight - intChartInset);
        Point py2 = new Point(intWidth - intChartInset, intHeight - intChartInset);

        //first establish working area

        // Pagep.Response.ContentType = "image/jpeg";


        Graphicsg.FillRectangle(new
        SolidBrush(Color.White), 0, 0, intWidth, intHeight);

        int intChartWidth = intWidth - (2 * intChartInset);
        int intChartHeight = intHeight - (2 * intChartInset);
        //Graphicsg.DrawRectangle(new Pen(Color.Black, 1), intChartInset, intChartInset, intChartWidth, intChartHeight);
        Graphicsg.DrawLine(new Pen(Color.Black, 5), px1, px2);
        Graphicsg.DrawLine(new Pen(Color.Black, 5), py1, py2);
        //must draw all text items before doing the rotate below
        Graphicsg.DrawString(strTitle, new Font("arial", 14), BrushblackBrush, intWidth / 3, 10);
        //draw X axis labels
        for (inti = 0; inti <= ftXdivs; inti++)
        {

            ftx = intChartInset + (inti * intChartWidth) / ftXdivs;
            fty = intChartHeight + intChartInset;
            strmyLabel = (ftXorigin + (ftScaleX * inti / ftXdivs)).ToString();
            if (inti == 0)
                Graphicsg.DrawString(xaxis_label_first, FontaxesFont, BrushblackBrush, ftx - 4, fty + 10);
            if (inti == ftXdivs)
                Graphicsg.DrawString(xaxis_label_last, FontaxesFont, BrushblackBrush, ftx - 4, fty + 10);
            //Graphicsg.DrawLine(penblackPen, ftx, fty + 2, ftx, fty - 2);
        }

        //draw Y axis labels
        for (inti = 0; inti <= ftYdivs; inti++)
        {

            ftx = intChartInset;
            fty = intChartHeight + intChartInset - (inti * intChartHeight / ftYdivs);
            strmyLabel = (ftYorigin + (ftScaleY * inti / ftYdivs)).ToString();
            if (inti != 0)
                Graphicsg.DrawString(commonMethods.ChangetToUK(Convert.ToDouble(strmyLabel).ToString("N")) + " €", FontaxesFont, BrushblackBrush, 5, fty - 6);
            //Graphicsg.DrawLine(penblackPen, ftx + 2, fty, ftx - 2, fty);

        }


        #region CommentedCode
        //transform drawing coords to lower-left (0,0)

        //Graphicsg.RotateTransform(180);
        //Graphicsg.TranslateTransform(0, -intHeight);

        //Graphicsg.TranslateTransform(-intChartInset, intChartInset);

        //Graphicsg.ScaleTransform(-1, 1);
        #endregion



        //draw chart data

        datapoint datapointprevPoint = new datapoint();
        datapointprevPoint.bolvalid = false;
        int loop = 1;
        Font FontPoints = new Font("arial", 7);
        foreach (datapoint myPoint in ArrayListchartValues)
        {

            if (datapointprevPoint.bolvalid == true)
            {
                Graphicsg.SmoothingMode = SmoothingMode.AntiAlias;
                /*
                ftx0 = intChartWidth * (datapointprevPoint.ftx - ftXorigin) / ftScaleX;
                fty0 = intChartHeight * (datapointprevPoint.fty - ftYorigin) / ftScaleY;
                */

                ftx0 = (70) + (datapointprevPoint.ftx) * intChartWidth / ftScaleX;
                fty0 = (intHeight - 70) - (datapointprevPoint.fty) * intChartHeight / ftScaleY;

                Graphicsg.DrawString(commonMethods.ChangetToUK(Convert.ToDouble(datapointprevPoint.fty).ToString("N")) + " €", FontPoints, BrushblackBrush, ftx0 - 20, fty0 - 40);

                /*
                ftx = intChartWidth * (myPoint.ftx - ftXorigin) / ftScaleX;
                fty = intChartHeight * (myPoint.fty - ftYorigin) / ftScaleY;
                 */

                ftx = (70) + (myPoint.ftx) * intChartWidth / ftScaleX;
                fty = (intHeight - 70) - (myPoint.fty) * intChartHeight / ftScaleY;

                if (loop == ArrayListchartValues.Count)
                    Graphicsg.DrawString(commonMethods.ChangetToUK(Convert.ToDouble(myPoint.fty).ToString("N")) + " €", FontPoints, BrushblackBrush, ftx - 20, fty - 40);
                //ftx = myPoint.ftx;
                //fty = myPoint.fty;
                Graphicsg.DrawLine(penblackPen, ftx0, fty0, ftx, fty);
                Graphicsg.FillEllipse(BrushblackBrush, ftx0 - 2, fty0 - 2, 5, 5);
                Graphicsg.FillEllipse(BrushblackBrush, ftx - 2, fty - 2, 5, 5);

            }

            datapointprevPoint = myPoint;
            loop++;
        }


        //finally send graphics to browser
        //Bitmapb.Save(Pagep.Response.OutputStream, ImageFormat.Jpeg);

        Serialization srl = new Serialization();
        Serialization.gStream = srl.SerializeObject(Bitmapb);

        //HtmlImage img = new HtmlImage();
        //Bitmapb.Save(Serialization.gStream, ImageFormat.Jpeg);
        //img = Bitmapb.Save(;


    }
    //LineChart()
    // {

    //     Graphicsg.Dispose();

    //     Bitmapb.Dispose();

    // }
}
