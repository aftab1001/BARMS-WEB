using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LC.Model.BMS.BLL;
using ExpertPdf.HtmlToPdf;

public partial class Managers_PrintWorkShift : System.Web.UI.Page
{
    static DateTime mydate;
    static int Year = 0;
    static int DepartmentID = 0;
    static int UserID = 0;
    static DateTime DefaultDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["date"] != null)
        {
            mydate = Convert.ToDateTime(Request.QueryString["date"]);
            DefaultDate = mydate;
        }
        //CurrentWeek = commonMethods.GetWeeknumber(DateTime.Now);
        //lblError.Visible = false;
        if (Session["UserLogin"] != null)
        {
            if (UserID == 0)
            {
                SessionUser user = new SessionUser();
                user = (SessionUser)Session["UserLogin"];
                UserID = user.UserID;
                DepartmentID = user.DepartmentID;
                Year = mydate.Year;
                if (user.AccessLevel != 2)
                {
                    Session["UserLogin"] = null;
                    Response.Redirect("../West_login.aspx");
                }
            }
            //UserID = user.UserID;
        }
        else
        {
            Session["UserLogin"] = null;
            if (Request.QueryString.Count < 2)
                Response.Redirect("../West_login.aspx");

        }
        if (!Page.IsPostBack)
        {

            int selectedweek = commonMethods.GetWeekNumber_New(mydate);
            //if(mydate.DayOfWeek == DayOfWeek.Sunday)
            //    selectedweek += 1;

            LoadWorkShiftForm(mydate.Year, selectedweek);
            LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
            if (Request.QueryString.Count > 1)
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
    private void LoadOffDayGrids()
    {
        //DateTime mydate = Convert.ToDateTime(datepicker.Text);
        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        tblUserWorkshifts uws = new tblUserWorkshifts();
        uws.LoadOffdayUsers(DepartmentID, selectedweek, mydate.Year, 1);
        grdSunday.DataSource = uws.DefaultView;
        grdSunday.DataBind();

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, selectedweek, mydate.Year, 2);
        grdMonday.DataSource = uws.DefaultView;
        grdMonday.DataBind();

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, selectedweek, mydate.Year, 3);
        grdTuesday.DataSource = uws.DefaultView;
        grdTuesday.DataBind();

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, selectedweek, mydate.Year, 4);
        grdWednesday.DataSource = uws.DefaultView;
        grdWednesday.DataBind();

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, selectedweek, mydate.Year, 5);
        grdThursday.DataSource = uws.DefaultView;
        grdThursday.DataBind();


        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, selectedweek, mydate.Year, 6);
        grdFriday.DataSource = uws.DefaultView;
        grdFriday.DataBind();

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, selectedweek, mydate.Year, 7);
        grdSaturday.DataSource = uws.DefaultView;
        grdSaturday.DataBind();
    }
    private void LoadPreviousOffDays(int weeknumber, int year, int departmentid)
    {

        tblUserWorkshifts uws = new tblUserWorkshifts();
        uws.PreviousWeekOffDays(weeknumber, departmentid, year);
        int count = uws.RowCount;
        //DLPreviousOffdays.DataSource = uws.DefaultView;
        //DLPreviousOffdays.DataBind();
    }
    protected void btnGO_Click(object sender, ImageClickEventArgs e)
    {
        //DateTime mydate = Convert.ToDateTime(datepicker.Text);
        //int selectedweek = commonMethods.GetWeeknumber(mydate);
        ////if (selectedweek <= CurrentWeek)
        ////{
        ////    lblError.Visible = true;
        ////    lblError.Text = " You can set workshifts for current or past weeks";
        ////    ShowWorkshift.Style.Add("display", "none");
        ////    return;
        ////}
        ////else
        ////{
        //LoadWorkShiftForm(mydate.Year, selectedweek);
        //Year = mydate.Year;
        //LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
        ////LoadOffDayGrids();

        ////}
        //// ShowWorkshift.Style.Add("display", "block");
    }
    private void LoadWorkShiftForm(int year, int SelectedWeek)
    {
        tblDepartments departments = new tblDepartments();
        departments.LoadDepartmentByMenagerID(UserID);
        string WeekSTime = string.Empty;
        string WeekETime = string.Empty;

        string WeekendSTime = string.Empty;
        string WeekendETime = string.Empty;

        if (departments.RowCount > 0)
        {
            WeekSTime = departments.WeekStartTime;
            WeekETime = departments.WeekEndTime;

            WeekendSTime = departments.WeekendStartTime;
            WeekendETime = departments.WeekendEndTime;
        }
        else
        {
            WeekSTime = "20:30";
            WeekETime = "04:30";

            WeekendSTime = "20:00";
            WeekendETime = "04:00";
        }




        ShowWorkshift.Style.Add("display", "block");

        DateTime startDate = commonMethods.GetWeekStartDate(year, SelectedWeek);
        startDate = startDate.AddDays(-1);
        lblSundayDate.Text = "<br/>" + startDate.ToString("dd/MM/yyyy");
        lblMondayDate.Text = "<br/>" + startDate.AddDays(1).ToString("dd/MM/yyyy");
        lblTuesdayDate.Text = "<br/>" + startDate.AddDays(2).ToString("dd/MM/yyyy");
        lblWednesdayDate.Text = "<br/>" + startDate.AddDays(3).ToString("dd/MM/yyyy");
        lblThursdayDate.Text = "<br/>" + startDate.AddDays(4).ToString("dd/MM/yyyy");
        lblFridayDate.Text = "<br/>" + startDate.AddDays(5).ToString("dd/MM/yyyy");
        lblSaturdayDate.Text = "<br/>" + startDate.AddDays(6).ToString("dd/MM/yyyy");


        lblSundayDate1.Text = "<br/>" + startDate.ToString("dd/MM/yyyy");
        lblMondayDate1.Text = "<br/>" + startDate.AddDays(1).ToString("dd/MM/yyyy");
        lblTuesdayDate1.Text = "<br/>" + startDate.AddDays(2).ToString("dd/MM/yyyy");
        lblWednesdayDate1.Text = "<br/>" + startDate.AddDays(3).ToString("dd/MM/yyyy");
        lblThursdayDate1.Text = "<br/>" + startDate.AddDays(4).ToString("dd/MM/yyyy");
        lblFridayDate1.Text = "<br/>" + startDate.AddDays(5).ToString("dd/MM/yyyy");
        lblSaturdayDate1.Text = "<br/>" + startDate.AddDays(6).ToString("dd/MM/yyyy");


        lblWeek.Text = " Week " + SelectedWeek;
        lblWeek.Text = "Week " + SelectedWeek.ToString() + " / " + Year.ToString() + " ( Sunday " + commonMethods.GetWeekStartDate(Year, SelectedWeek).AddDays(-1).ToString("dd/MM/yyyy") + " till Saturday " + commonMethods.GetWeekStartDate(Year, SelectedWeek).AddDays(5).ToString("dd/MM/yyyy") + " )";

        tblSpeciality speciality = new tblSpeciality();
        speciality.LoadWorkShiftSpecialities(DepartmentID);

        tblUserWorkshifts uws = new tblUserWorkshifts();
        tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();
        DataTable dt = uws.LoadCurrentWorkshift(SelectedWeek, DepartmentID, year);
        bool blankRow = false;
        for (int Counter = 1; Counter <= speciality.RowCount; Counter++)
        {

            string css = string.Empty;
            string Tdcss = string.Empty;
            if (Counter % 2 == 0)
            {
                Tdcss = "grey";
            }
            else
            {
                Tdcss = "white";
            }

            if (Counter % 2 == 0)
            {
                css = "rowstyle_new_print";
            }
            else
            {
                css = "alternate_row_new_print";
            }
            if (speciality.s_SSpeciality == "Separator")
            {
                HtmlTableRow trBlank = new HtmlTableRow();
                HtmlTableCell tdBlank = new HtmlTableCell();
                tdBlank.ColSpan = 8;
                tdBlank.Style.Add("height", "2px;");
                tdBlank.Style.Add("border", "2px;");
                trBlank.Style.Add("background-color", "#999999");
                trBlank.Controls.Add(tdBlank);
                tblWork.Controls.Add(trBlank);
                blankRow = false;
            }
            else
            {
                HtmlTableRow tr = new HtmlTableRow();
                tr.Attributes.Add("class", css);
                HtmlTableCell td = new HtmlTableCell();
                td.InnerHtml = speciality.s_SSpeciality;
                td.Attributes.Add("class", Tdcss);
                tr.Cells.Add(td);

                //For Sunday
                //pkUserWorkshiftID  DeleteRecord
                DataRow[] dr;


                HtmlTableCell tdSunday = new HtmlTableCell();
                tdSunday.Attributes.Add("class", css);
                HtmlImage imgSundayPlus = new HtmlImage();
                HtmlGenericControl gcSunday = new HtmlGenericControl("span");
                //gcSunday.Style.Add("display", "block");
                //gcSunday.Style.Add("vertical-align", "middle");
                //gcSunday.Style.Add("float", "right");
                HtmlGenericControl gcSunday1 = new HtmlGenericControl("span");
                //gcSunday1.Style.Add("display", "block");
                //gcSunday1.Style.Add("width", "92px");
                //gcSunday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 1 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {

                    //imgSundayPlus.Src = "../images/minus_img.png";
                    //imgSundayPlus.Style.Add("cursor", "pointer");
                    //imgSundayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    //imgSundayPlus.Attributes.Add("align", "right"); //New LIne code for right side minus UKD
                    //tdSunday.InnerHtml = "<a href='#'>" + dr[0][0].ToString() + "</a>&nbsp;";
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekSTime && dr[0]["sEndTime"].ToString() == WeekETime)
                    {
                        strStyle = "style='color:Black'";
                    }
                    else
                    {
                        strStyle = "style='color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + "> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
                    string strOffday = string.Empty;
                    LoadUserOffday.FlushData();
                    LoadUserOffday.PreviousWeekOffDaysByUSer(SelectedWeek - 1, DepartmentID, year, Convert.ToInt32(dr[0]["fkUserID"]));
                    int WorkingDays = 0;
                    if (LoadUserOffday.RowCount > 0)
                    {
                        WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                    }
                    //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                    string Styleoffday = string.Empty;
                    if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                    {
                        Styleoffday = "style='color:RED'";
                    }
                    else
                    {
                        Styleoffday = "style='color:Black'";
                    }
                    strOffday = "<span " + Styleoffday + ">(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    //if (name.Length > 10)
                    //    name = name.Substring(0, 10) + "...";
                    gcSunday1.InnerHtml = name + strSpan; //+ strOffday;//[" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]";
                    //}
                }
                else
                {
                    //imgSundayPlus.Src = "../images/plus_img.png";
                    //imgSundayPlus.Style.Add("cursor", "pointer");
                    // imgSundayPlus.Attributes.Add("align", "right"); // New LIne code for right side plus UKD
                    //imgSundayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.GetColumn("FkSpecialityTypeID") + "','" + speciality.GetColumn("pkSpecialityID") + "','1');");

                }
                //gcSunday.Controls.Add(imgSundayPlus);
                //tdSunday.Controls.Add(imgSundayPlus);
                tdSunday.Controls.Add(gcSunday1);
                tdSunday.Controls.Add(gcSunday);
                tr.Cells.Add(tdSunday);

                //For Monday

                HtmlTableCell tdMonday = new HtmlTableCell();
                tdMonday.Attributes.Add("class", css);
                HtmlImage imgMondayPlus = new HtmlImage();
                HtmlGenericControl gcMonday = new HtmlGenericControl("span");
                //gcMonday.Style.Add("display", "block");
                //gcMonday.Style.Add("vertical-align", "middle");
                //gcMonday.Style.Add("float", "right");
                HtmlGenericControl gcMonday1 = new HtmlGenericControl("span");
                //gcMonday1.Style.Add("display", "block");
                //gcMonday1.Style.Add("width", "92px");
                //gcMonday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 2 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    //imgMondayPlus.Src = "../images/minus_img.png";
                    //imgMondayPlus.Style.Add("cursor", "pointer");
                    //imgMondayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekSTime && dr[0]["sEndTime"].ToString() == WeekETime)
                    {
                        strStyle = "style='color:Black'";
                    }
                    else
                    {
                        strStyle = "style='color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + "> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
                    string strOffday = string.Empty;
                    LoadUserOffday.FlushData();
                    LoadUserOffday.PreviousWeekOffDaysByUSer(SelectedWeek - 1, DepartmentID, year, Convert.ToInt32(dr[0]["fkUserID"]));
                    int WorkingDays = 0;
                    if (LoadUserOffday.RowCount > 0)
                    {
                        WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                    }
                    //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                    string Styleoffday = string.Empty;
                    if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                    {
                        Styleoffday = "style='color:RED'";
                    }
                    else
                    {
                        Styleoffday = "style='color:Black'";
                    }
                    strOffday = "<span " + Styleoffday + ">(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    //if (name.Length > 10)
                    //    name = name.Substring(0, 10) + "...";
                    gcMonday1.InnerHtml = name + strSpan; //+ strOffday;
                    //}
                }
                else
                {
                    //imgMondayPlus.Src = "../images/plus_img.png";
                    //imgMondayPlus.Style.Add("cursor", "pointer");
                    //imgMondayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','2');");
                }
                //gcMonday.Controls.Add(imgMondayPlus);
                tdMonday.Controls.Add(gcMonday1);
                tdMonday.Controls.Add(gcMonday);

                //tdMonday.Controls.Add(imgMondayPlus);
                tr.Cells.Add(tdMonday);


                //For Tuesday

                HtmlTableCell tdTuesday = new HtmlTableCell();
                tdTuesday.Attributes.Add("class", css);
                HtmlImage imgTuesdayPlus = new HtmlImage();
                HtmlGenericControl gcTuesday = new HtmlGenericControl("span");
                //gcTuesday.Style.Add("display", "block");
                //gcTuesday.Style.Add("vertical-align", "middle");
                //gcTuesday.Style.Add("float", "right");
                HtmlGenericControl gcTuesday1 = new HtmlGenericControl("span");
                //gcTuesday1.Style.Add("display", "block");
                //gcTuesday1.Style.Add("width", "92px");
                //gcTuesday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 3 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    //imgTuesdayPlus.Src = "../images/minus_img.png";
                    //imgTuesdayPlus.Style.Add("cursor", "pointer");
                    //imgTuesdayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekSTime && dr[0]["sEndTime"].ToString() == WeekETime)
                    {
                        strStyle = "style='color:Black'";
                    }
                    else
                    {
                        strStyle = "style='color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + "> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
                    string strOffday = string.Empty;
                    LoadUserOffday.FlushData();
                    LoadUserOffday.PreviousWeekOffDaysByUSer(SelectedWeek - 1, DepartmentID, year, Convert.ToInt32(dr[0]["fkUserID"]));
                    int WorkingDays = 0;
                    if (LoadUserOffday.RowCount > 0)
                    {
                        WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                    }
                    //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                    string Styleoffday = string.Empty;
                    if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                    {
                        Styleoffday = "style='color:RED'";
                    }
                    else
                    {
                        Styleoffday = "style='color:Black'";
                    }
                    strOffday = "<span " + Styleoffday + ">(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    //if (name.Length > 10)
                    //    name = name.Substring(0, 10) + "...";
                    gcTuesday1.InnerHtml = name + strSpan;// +strOffday;
                    //}
                }
                else
                {
                    //imgTuesdayPlus.Src = "../images/plus_img.png";
                    //imgTuesdayPlus.Style.Add("cursor", "pointer");
                    //imgTuesdayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','3');");
                }
                //gcTuesday.Controls.Add(imgTuesdayPlus);
                tdTuesday.Controls.Add(gcTuesday1);
                tdTuesday.Controls.Add(gcTuesday);
                //tdTuesday.Controls.Add(imgTuesdayPlus);
                tr.Cells.Add(tdTuesday);


                //For Wednesday

                HtmlTableCell tdWednesday = new HtmlTableCell();
                tdWednesday.Attributes.Add("class", css);
                HtmlImage imgWednesdayPlus = new HtmlImage();
                HtmlGenericControl gcWednesday = new HtmlGenericControl("span");
                //gcWednesday.Style.Add("display", "block");
                //gcWednesday.Style.Add("vertical-align", "middle");
                //gcWednesday.Style.Add("float", "right");
                HtmlGenericControl gcWednesday1 = new HtmlGenericControl("span");
                //gcWednesday1.Style.Add("display", "block");
                //gcWednesday1.Style.Add("width", "92px");
                //gcWednesday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 4 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    //imgWednesdayPlus.Src = "../images/minus_img.png";
                    //imgWednesdayPlus.Style.Add("cursor", "pointer");
                    //imgWednesdayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekSTime && dr[0]["sEndTime"].ToString() == WeekETime)
                    {
                        strStyle = "style='color:Black'";
                    }
                    else
                    {
                        strStyle = "style='color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + "> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
                    string strOffday = string.Empty;
                    LoadUserOffday.FlushData();
                    LoadUserOffday.PreviousWeekOffDaysByUSer(SelectedWeek - 1, DepartmentID, year, Convert.ToInt32(dr[0]["fkUserID"]));
                    int WorkingDays = 0;
                    if (LoadUserOffday.RowCount > 0)
                    {
                        WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                    }
                    //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                    string Styleoffday = string.Empty;
                    if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                    {
                        Styleoffday = "style='color:RED'";
                    }
                    else
                    {
                        Styleoffday = "style='color:Black'";
                    }
                    strOffday = "<span " + Styleoffday + ">(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    //if (name.Length > 10)
                    //    name = name.Substring(0, 10) + "...";
                    gcWednesday1.InnerHtml = name + strSpan;// +strOffday;
                    //}
                }
                else
                {
                    //imgWednesdayPlus.Src = "../images/plus_img.png";
                    //imgWednesdayPlus.Style.Add("cursor", "pointer");
                    //imgWednesdayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','4');");
                }
                //gcWednesday.Controls.Add(imgWednesdayPlus);
                tdWednesday.Controls.Add(gcWednesday1);
                tdWednesday.Controls.Add(gcWednesday);
                //tdWednesday.Controls.Add(imgWednesdayPlus);
                tr.Cells.Add(tdWednesday);


                //For Thursday

                HtmlTableCell tdThursday = new HtmlTableCell();
                tdThursday.Attributes.Add("class", css);
                HtmlImage imgThursdayPlus = new HtmlImage();
                HtmlGenericControl gcThursday = new HtmlGenericControl("span");
                //gcThursday.Style.Add("display", "block");
                //gcThursday.Style.Add("vertical-align", "middle");
                //gcThursday.Style.Add("float", "right");
                HtmlGenericControl gcThursday1 = new HtmlGenericControl("span");
                //gcThursday1.Style.Add("display", "block");
                //gcThursday1.Style.Add("width", "92px");
                //gcThursday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 5 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    //imgThursdayPlus.Src = "../images/minus_img.png";
                    //imgThursdayPlus.Style.Add("cursor", "pointer");
                    //imgThursdayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekSTime && dr[0]["sEndTime"].ToString() == WeekETime)
                    {
                        strStyle = "style='color:Black'";
                    }
                    else
                    {
                        strStyle = "style='color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + "> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
                    string strOffday = string.Empty;
                    LoadUserOffday.FlushData();
                    LoadUserOffday.PreviousWeekOffDaysByUSer(SelectedWeek - 1, DepartmentID, year, Convert.ToInt32(dr[0]["fkUserID"]));
                    int WorkingDays = 0;
                    if (LoadUserOffday.RowCount > 0)
                    {
                        WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                    }
                    //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                    string Styleoffday = string.Empty;
                    if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                    {
                        Styleoffday = "style='color:RED'";
                    }
                    else
                    {
                        Styleoffday = "style='color:Black'";
                    }
                    strOffday = "<span " + Styleoffday + ">(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    //if (name.Length > 10)
                    //    name = name.Substring(0, 10) + "...";
                    gcThursday1.InnerHtml = name + strSpan;// +strOffday;
                    //}
                }
                else
                {
                    //imgThursdayPlus.Src = "../images/plus_img.png";
                    //imgThursdayPlus.Style.Add("cursor", "pointer");
                    //imgThursdayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','5');");
                }
                //gcThursday.Controls.Add(imgThursdayPlus);
                tdThursday.Controls.Add(gcThursday1);
                tdThursday.Controls.Add(gcThursday);
                //tdThursday.Controls.Add(imgThursdayPlus);
                tr.Cells.Add(tdThursday);


                //For Friday

                HtmlTableCell tdFriday = new HtmlTableCell();
                tdFriday.Attributes.Add("class", css);
                HtmlImage imgFridayPlus = new HtmlImage();
                HtmlGenericControl gcFriday = new HtmlGenericControl("span");
                //gcFriday.Style.Add("display", "block");
                //gcFriday.Style.Add("vertical-align", "middle");
                //gcFriday.Style.Add("float", "right");
                HtmlGenericControl gcFriday1 = new HtmlGenericControl("span");
                //gcFriday1.Style.Add("display", "block");
                //gcFriday1.Style.Add("width", "92px");
                //gcFriday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 6 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    //imgFridayPlus.Src = "../images/minus_img.png";
                    //imgFridayPlus.Style.Add("cursor", "pointer");
                    //imgFridayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekendSTime && dr[0]["sEndTime"].ToString() == WeekendETime)
                    {
                        strStyle = "style='color:Black'";
                    }
                    else
                    {
                        strStyle = "style='color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + "> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
                    string strOffday = string.Empty;
                    LoadUserOffday.FlushData();
                    LoadUserOffday.PreviousWeekOffDaysByUSer(SelectedWeek - 1, DepartmentID, year, Convert.ToInt32(dr[0]["fkUserID"]));
                    int WorkingDays = 0;
                    if (LoadUserOffday.RowCount > 0)
                    {
                        WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                    }
                    //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                    string Styleoffday = string.Empty;
                    if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                    {
                        Styleoffday = "style='color:RED'";
                    }
                    else
                    {
                        Styleoffday = "style='color:Black'";
                    }
                    strOffday = "<span " + Styleoffday + ">(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    //if (name.Length > 10)
                    //    name = name.Substring(0, 10) + "...";
                    gcFriday1.InnerHtml = name + strSpan;// +strOffday;
                    //}
                }
                else
                {
                    //imgFridayPlus.Src = "../images/plus_img.png";
                    //imgFridayPlus.Style.Add("cursor", "pointer");
                    //imgFridayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','6');");
                }
                //gcFriday.Controls.Add(imgFridayPlus);
                tdFriday.Controls.Add(gcFriday1);
                tdFriday.Controls.Add(gcFriday);
                //tdFriday.Controls.Add(imgFridayPlus);
                tr.Cells.Add(tdFriday);

                //For Saturday

                HtmlTableCell tdSaturday = new HtmlTableCell();
                tdSaturday.Attributes.Add("class", css);
                HtmlImage imgSaturdayPlus = new HtmlImage();
                HtmlGenericControl gcSaturday = new HtmlGenericControl("span");
                //gcSaturday.Style.Add("display", "block");
                //gcSaturday.Style.Add("vertical-align", "middle");
                //gcSaturday.Style.Add("float", "right");
                HtmlGenericControl gcSaturday1 = new HtmlGenericControl("span");
                //gcSaturday1.Style.Add("display", "block");
                //gcSaturday1.Style.Add("width", "92px");
                //gcSaturday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 7 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    //imgSaturdayPlus.Src = "../images/minus_img.png";
                    //imgSaturdayPlus.Style.Add("cursor", "pointer");
                    //imgSaturdayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekendSTime && dr[0]["sEndTime"].ToString() == WeekendETime)
                    {
                        strStyle = "style='color:Black'";
                    }
                    else
                    {
                        strStyle = "style='color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + "> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
                    string strOffday = string.Empty;
                    LoadUserOffday.FlushData();
                    LoadUserOffday.PreviousWeekOffDaysByUSer(SelectedWeek - 1, DepartmentID, year, Convert.ToInt32(dr[0]["fkUserID"]));
                    int WorkingDays = 0;
                    if (LoadUserOffday.RowCount > 0)
                    {
                        WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                    }
                    //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                    string Styleoffday = string.Empty;
                    if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                    {
                        Styleoffday = "style='color:RED'";
                    }
                    else
                    {
                        Styleoffday = "style='color:Black'";
                    }
                    strOffday = "<span " + Styleoffday + ">(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    //if (name.Length > 10)
                    //    name = name.Substring(0, 10) + "...";
                    gcSaturday1.InnerHtml = name + strSpan;// +strOffday;
                    //}
                }
                else
                {
                    //imgSaturdayPlus.Src = "../images/plus_img.png";
                    //imgSaturdayPlus.Style.Add("cursor", "pointer");
                    //imgSaturdayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','7');");
                }
                //gcSaturday.Controls.Add(imgSaturdayPlus);
                tdSaturday.Controls.Add(gcSaturday1);
                tdSaturday.Controls.Add(gcSaturday);
                //tdSaturday.Controls.Add(imgSaturdayPlus);
                tr.Cells.Add(tdSaturday);

                tblWork.Rows.Add(tr);
            }

            speciality.MoveNext();
        }



        LoadOffDayGrids();




    }
    protected void grdSunday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                mydate = Convert.ToDateTime(DefaultDate);
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                Label lnkUser = (Label)e.Row.FindControl("lnkUser");
                string name = drv["FullName"].ToString();
                lnkUser.Text = drv["FullName"].ToString();
                //if (name.Length > 10)
                //{
                //    lnkUser.Text = name.Substring(0, 10) + "...";
                //}
                //lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                //lnkUser.Text = drv["FullName"].ToString();
                //lblemail.Text = drv["sEmail"].ToString();

                tblUserWorkshifts LoadUserOnHours = new tblUserWorkshifts();
                LoadUserOnHours.PreviousWeekOnHoursByUSer(selectedweek - 1, mydate.Year, Convert.ToInt32(drv["pkuserid"]));

                int hours = 0;
                if (LoadUserOnHours.RowCount > 0)
                {
                    for (int i = 0; i < LoadUserOnHours.RowCount; i++)
                    {
                        //if (LoadUserOnHours.IDayNumber == 1 || LoadUserOnHours.IDayNumber == 7)
                        //{
                        DateTime S_StartDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SStartTime + ":00");
                        int startHour = Convert.ToInt32(LoadUserOnHours.SStartTime.Substring(0, LoadUserOnHours.SStartTime.Length - 3));
                        int endHour = Convert.ToInt32(LoadUserOnHours.SEndTime.Substring(0, LoadUserOnHours.SEndTime.Length - 3));
                        DateTime S_EndDate;
                        if (startHour > 12 && endHour < 12)
                        {
                            mydate = mydate.AddDays(1);
                            S_EndDate = Convert.ToDateTime(mydate.Month.ToString() + "/" + (mydate.Day).ToString() + "/" + mydate.Year.ToString() + " " + LoadUserOnHours.SEndTime + ":00");
                        }
                        else
                            S_EndDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SEndTime + ":00");
                        TimeSpan ts = S_EndDate.Subtract(S_StartDate);
                        hours += ts.Hours;
                        //}

                        LoadUserOnHours.MoveNext();
                    }
                }

                tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();
                LoadUserOffday.PreviousWeekOffDaysByUSer(selectedweek - 1, DepartmentID, mydate.Year, Convert.ToInt32(drv["pkuserid"]));
                int WorkingDays = 0;
                if (LoadUserOffday.RowCount > 0)
                {
                    WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                }

                //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                string Styleoffday = string.Empty;
                if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                {
                    Styleoffday = "style='color:RED'";
                }
                else
                {
                    Styleoffday = "style='color:Black'";
                }
                string strOffday = " (" + hours + ") " + "<span " + Styleoffday + ">" + (7 - WorkingDays).ToString() + "</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                // mydiv.InnerHtml = strOffday;


            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdMonday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                mydate = Convert.ToDateTime(DefaultDate);
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                Label lnkUser = (Label)e.Row.FindControl("lnkUser");
                string name = drv["FullName"].ToString();
                lnkUser.Text = drv["FullName"].ToString();
                //if (name.Length > 10)
                //{
                //    lnkUser.Text = name.Substring(0, 10) + "...";
                //}
                //lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                //lnkUser.Text = drv["FullName"].ToString();
                //lblemail.Text = drv["sEmail"].ToString();

                tblUserWorkshifts LoadUserOnHours = new tblUserWorkshifts();
                LoadUserOnHours.PreviousWeekOnHoursByUSer(selectedweek - 1, mydate.Year, Convert.ToInt32(drv["pkuserid"]));

                int hours = 0;
                if (LoadUserOnHours.RowCount > 0)
                {
                    for (int i = 0; i < LoadUserOnHours.RowCount; i++)
                    {
                        DateTime S_StartDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SStartTime + ":00");
                        int startHour = Convert.ToInt32(LoadUserOnHours.SStartTime.Substring(0, LoadUserOnHours.SStartTime.Length - 3));
                        int endHour = Convert.ToInt32(LoadUserOnHours.SEndTime.Substring(0, LoadUserOnHours.SEndTime.Length - 3));
                        DateTime S_EndDate;
                        if (startHour > 12 && endHour < 12)
                        {
                            mydate = mydate.AddDays(1);
                            S_EndDate = Convert.ToDateTime(mydate.Month.ToString() + "/" + (mydate.Day).ToString() + "/" + mydate.Year.ToString() + " " + LoadUserOnHours.SEndTime + ":00");
                        }
                        else
                            S_EndDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SEndTime + ":00");
                        TimeSpan ts = S_EndDate.Subtract(S_StartDate);
                        hours += ts.Hours;
                        LoadUserOnHours.MoveNext();
                    }
                }
                tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();
                LoadUserOffday.PreviousWeekOffDaysByUSer(selectedweek - 1, DepartmentID, mydate.Year, Convert.ToInt32(drv["pkuserid"]));
                int WorkingDays = 0;
                if (LoadUserOffday.RowCount > 0)
                {
                    WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                }
                //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                string Styleoffday = string.Empty;
                if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                {
                    Styleoffday = "style='color:RED'";
                }
                else
                {
                    Styleoffday = "style='color:Black'";
                }
                string strOffday = " (" + hours + ") " + "<span " + Styleoffday + ">" + (7 - WorkingDays).ToString() + "</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                // mydiv.InnerHtml = strOffday;


            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdTuesday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                mydate = Convert.ToDateTime(DefaultDate);
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                Label lnkUser = (Label)e.Row.FindControl("lnkUser");
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                //if (name.Length > 10)
                //{
                //    lnkUser.Text = name.Substring(0, 10) + "...";
                //}
                //lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                //lnkUser.Text = drv["FullName"].ToString();
                //lblemail.Text = drv["sEmail"].ToString();

                tblUserWorkshifts LoadUserOnHours = new tblUserWorkshifts();
                LoadUserOnHours.PreviousWeekOnHoursByUSer(selectedweek - 1, mydate.Year, Convert.ToInt32(drv["pkuserid"]));

                int hours = 0;
                if (LoadUserOnHours.RowCount > 0)
                {
                    for (int i = 0; i < LoadUserOnHours.RowCount; i++)
                    {

                        DateTime S_StartDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SStartTime + ":00");
                        int startHour = Convert.ToInt32(LoadUserOnHours.SStartTime.Substring(0, LoadUserOnHours.SStartTime.Length - 3));
                        int endHour = Convert.ToInt32(LoadUserOnHours.SEndTime.Substring(0, LoadUserOnHours.SEndTime.Length - 3));
                        DateTime S_EndDate;
                        if (startHour > 12 && endHour < 12)
                        {
                            mydate = mydate.AddDays(1);
                            S_EndDate = Convert.ToDateTime(mydate.Month.ToString() + "/" + (mydate.Day).ToString() + "/" + mydate.Year.ToString() + " " + LoadUserOnHours.SEndTime + ":00");
                        }
                        else
                            S_EndDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SEndTime + ":00");
                        TimeSpan ts = S_EndDate.Subtract(S_StartDate);
                        hours += ts.Hours;

                        LoadUserOnHours.MoveNext();
                    }
                }

                tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();

                LoadUserOffday.PreviousWeekOffDaysByUSer(selectedweek - 1, DepartmentID, mydate.Year, Convert.ToInt32(drv["pkuserid"]));
                int WorkingDays = 0;
                if (LoadUserOffday.RowCount > 0)
                {
                    WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                }
                //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                string Styleoffday = string.Empty;
                if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                {
                    Styleoffday = "style='color:RED'";
                }
                else
                {
                    Styleoffday = "style='color:Black'";
                }
                string strOffday = " (" + hours + ") " + "<span " + Styleoffday + ">" + (7 - WorkingDays).ToString() + "</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                // mydiv.InnerHtml = strOffday;


            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdWednesday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                mydate = Convert.ToDateTime(DefaultDate);
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                Label lnkUser = (Label)e.Row.FindControl("lnkUser");
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                //if (name.Length > 10)
                //{
                //    lnkUser.Text = name.Substring(0, 10) + "...";
                //}
                //lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                //lnkUser.Text = drv["FullName"].ToString();
                //lblemail.Text = drv["sEmail"].ToString();

                tblUserWorkshifts LoadUserOnHours = new tblUserWorkshifts();
                LoadUserOnHours.PreviousWeekOnHoursByUSer(selectedweek - 1, mydate.Year, Convert.ToInt32(drv["pkuserid"]));

                int hours = 0;
                if (LoadUserOnHours.RowCount > 0)
                {
                    for (int i = 0; i < LoadUserOnHours.RowCount; i++)
                    {
                        DateTime S_StartDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SStartTime + ":00");
                        int startHour = Convert.ToInt32(LoadUserOnHours.SStartTime.Substring(0, LoadUserOnHours.SStartTime.Length - 3));
                        int endHour = Convert.ToInt32(LoadUserOnHours.SEndTime.Substring(0, LoadUserOnHours.SEndTime.Length - 3));
                        DateTime S_EndDate;
                        if (startHour > 12 && endHour < 12)
                        {
                            mydate = mydate.AddDays(1);
                            S_EndDate = Convert.ToDateTime(mydate.Month.ToString() + "/" + (mydate.Day).ToString() + "/" + mydate.Year.ToString() + " " + LoadUserOnHours.SEndTime + ":00");
                        }
                        else
                            S_EndDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SEndTime + ":00");
                        TimeSpan ts = S_EndDate.Subtract(S_StartDate);
                        hours += ts.Hours;
                        LoadUserOnHours.MoveNext();
                    }
                }


                tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();
                LoadUserOffday.PreviousWeekOffDaysByUSer(selectedweek - 1, DepartmentID, mydate.Year, Convert.ToInt32(drv["pkuserid"]));
                int WorkingDays = 0;
                if (LoadUserOffday.RowCount > 0)
                {
                    WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                }
                //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                string Styleoffday = string.Empty;
                if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                {
                    Styleoffday = "style='color:RED'";
                }
                else
                {
                    Styleoffday = "style='color:Black'";
                }
                string strOffday = " (" + hours + ") " + "<span " + Styleoffday + ">" + (7 - WorkingDays).ToString() + "</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                // mydiv.InnerHtml = strOffday;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdThursday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                mydate = Convert.ToDateTime(DefaultDate);
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                Label lnkUser = (Label)e.Row.FindControl("lnkUser");
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                //if (name.Length > 10)
                //{
                //    lnkUser.Text = name.Substring(0, 10) + "...";
                //}
                //lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                //lnkUser.Text = drv["FullName"].ToString();
                //lblemail.Text = drv["sEmail"].ToString();

                tblUserWorkshifts LoadUserOnHours = new tblUserWorkshifts();
                LoadUserOnHours.PreviousWeekOnHoursByUSer(selectedweek - 1, mydate.Year, Convert.ToInt32(drv["pkuserid"]));

                int hours = 0;
                if (LoadUserOnHours.RowCount > 0)
                {
                    for (int i = 0; i < LoadUserOnHours.RowCount; i++)
                    {
                        DateTime S_StartDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SStartTime + ":00");
                        int startHour = Convert.ToInt32(LoadUserOnHours.SStartTime.Substring(0, LoadUserOnHours.SStartTime.Length - 3));
                        int endHour = Convert.ToInt32(LoadUserOnHours.SEndTime.Substring(0, LoadUserOnHours.SEndTime.Length - 3));
                        DateTime S_EndDate;
                        if (startHour > 12 && endHour < 12)
                        {
                            mydate = mydate.AddDays(1);
                            S_EndDate = Convert.ToDateTime(mydate.Month.ToString() + "/" + (mydate.Day).ToString() + "/" + mydate.Year.ToString() + " " + LoadUserOnHours.SEndTime + ":00");
                        }
                        else
                            S_EndDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SEndTime + ":00");
                        TimeSpan ts = S_EndDate.Subtract(S_StartDate);
                        hours += ts.Hours;
                        LoadUserOnHours.MoveNext();
                    }
                }

                tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();
                LoadUserOffday.PreviousWeekOffDaysByUSer(selectedweek - 1, DepartmentID, mydate.Year, Convert.ToInt32(drv["pkuserid"]));
                int WorkingDays = 0;
                if (LoadUserOffday.RowCount > 0)
                {
                    WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                }
                //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                string Styleoffday = string.Empty;
                if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                {
                    Styleoffday = "style='color:RED'";
                }
                else
                {
                    Styleoffday = "style='color:Black'";
                }
                string strOffday = " (" + hours + ") " + "<span " + Styleoffday + ">" + (7 - WorkingDays).ToString() + "</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                // mydiv.InnerHtml = strOffday;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdFriday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                mydate = Convert.ToDateTime(DefaultDate);
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                Label lnkUser = (Label)e.Row.FindControl("lnkUser");
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                //if (name.Length > 10)
                //{
                //    lnkUser.Text = name.Substring(0, 10) + "...";
                //}
                //lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                //lnkUser.Text = drv["FullName"].ToString();
                //lblemail.Text = drv["sEmail"].ToString();

                tblUserWorkshifts LoadUserOnHours = new tblUserWorkshifts();
                LoadUserOnHours.PreviousWeekOnHoursByUSer(selectedweek - 1, mydate.Year, Convert.ToInt32(drv["pkuserid"]));

                int hours = 0;
                if (LoadUserOnHours.RowCount > 0)
                {
                    for (int i = 0; i < LoadUserOnHours.RowCount; i++)
                    {
                        DateTime S_StartDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SStartTime + ":00");
                        int startHour = Convert.ToInt32(LoadUserOnHours.SStartTime.Substring(0, LoadUserOnHours.SStartTime.Length - 3));
                        int endHour = Convert.ToInt32(LoadUserOnHours.SEndTime.Substring(0, LoadUserOnHours.SEndTime.Length - 3));
                        DateTime S_EndDate;
                        if (startHour > 12 && endHour < 12)
                        {
                            mydate = mydate.AddDays(1);
                            S_EndDate = Convert.ToDateTime(mydate.Month.ToString() + "/" + (mydate.Day).ToString() + "/" + mydate.Year.ToString() + " " + LoadUserOnHours.SEndTime + ":00");
                        }
                        else
                            S_EndDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SEndTime + ":00");
                        TimeSpan ts = S_EndDate.Subtract(S_StartDate);
                        hours += ts.Hours;
                        LoadUserOnHours.MoveNext();
                    }
                }
                tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();
                LoadUserOffday.PreviousWeekOffDaysByUSer(selectedweek - 1, DepartmentID, mydate.Year, Convert.ToInt32(drv["pkuserid"]));
                int WorkingDays = 0;
                if (LoadUserOffday.RowCount > 0)
                {
                    WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                }
                //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                string Styleoffday = string.Empty;
                if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                {
                    Styleoffday = "style='color:RED'";
                }
                else
                {
                    Styleoffday = "style='color:Black'";
                }
                string strOffday = " (" + hours + ") " + "<span " + Styleoffday + ">" + (7 - WorkingDays).ToString() + "</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                // mydiv.InnerHtml = strOffday;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdSaturday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                mydate = Convert.ToDateTime(DefaultDate);
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                Label lnkUser = (Label)e.Row.FindControl("lnkUser");
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                //if (name.Length > 10)
                //{
                //    lnkUser.Text = name.Substring(0, 10) + "...";
                //}
                // lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                //lnkUser.Text = drv["FullName"].ToString();
                //lblemail.Text = drv["sEmail"].ToString();



                tblUserWorkshifts LoadUserOnHours = new tblUserWorkshifts();
                LoadUserOnHours.PreviousWeekOnHoursByUSer(selectedweek - 1, mydate.Year, Convert.ToInt32(drv["pkuserid"]));

                int hours = 0;
                if (LoadUserOnHours.RowCount > 0)
                {
                    for (int i = 0; i < LoadUserOnHours.RowCount; i++)
                    {
                        //if (LoadUserOnHours.IDayNumber == 1 || LoadUserOnHours.IDayNumber == 7)
                        //{
                        DateTime S_StartDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SStartTime + ":00");
                        int startHour = Convert.ToInt32(LoadUserOnHours.SStartTime.Substring(0, LoadUserOnHours.SStartTime.Length - 3));
                        int endHour = Convert.ToInt32(LoadUserOnHours.SEndTime.Substring(0, LoadUserOnHours.SEndTime.Length - 3));
                        DateTime S_EndDate;
                        if (startHour > 12 && endHour < 12)
                        {
                            mydate = mydate.AddDays(1);
                            S_EndDate = Convert.ToDateTime(mydate.Month.ToString() + "/" + (mydate.Day).ToString() + "/" + mydate.Year.ToString() + " " + LoadUserOnHours.SEndTime + ":00");
                        }
                        else
                            S_EndDate = Convert.ToDateTime(mydate.ToShortDateString() + " " + LoadUserOnHours.SEndTime + ":00");
                        TimeSpan ts = S_EndDate.Subtract(S_StartDate);
                        hours += ts.Hours;
                        //}
                        LoadUserOnHours.MoveNext();
                    }
                }
                tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();
                LoadUserOffday.PreviousWeekOffDaysByUSer(selectedweek - 1, DepartmentID, mydate.Year, Convert.ToInt32(drv["pkuserid"]));
                int WorkingDays = 0;
                if (LoadUserOffday.RowCount > 0)
                {
                    WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                }
                //strOffday = "(" + (7 - WorkingDays).ToString() + ")";
                string Styleoffday = string.Empty;
                if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                {
                    Styleoffday = "style='color:RED'";
                }
                else
                {
                    Styleoffday = "style='color:Black'";
                }
                string strOffday = " (" + hours + ") " + "<span " + Styleoffday + ">" + (7 - WorkingDays).ToString() + "</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                // mydiv.InnerHtml = strOffday;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DLPreviousOffdays_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        DataRow dr = ((DataRowView)e.Item.DataItem).Row;
        try
        {


            Label lblName = (Label)e.Item.FindControl("lblName");

            lblName.Text = dr["FullName"].ToString(); //dr["Week"].ToString();
            string name = dr["FullName"].ToString();
            //if (name.Length > 10)
            //{
            //    lblName.Text = name.Substring(0, 10) + "...";
            //}
            Label lblCount = (Label)e.Item.FindControl("lblCount");
            int WorkingDays = Convert.ToInt32(dr["offdays"].ToString());


            //lblCount.Text = (7 - WorkingDays).ToString();
            string Styleoffday = string.Empty;
            if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
            {
                Styleoffday = "style='color:RED'";
            }
            else
            {
                Styleoffday = "style='color:Black'";
            }
            string strOffday = "<span " + Styleoffday + ">" + (7 - WorkingDays).ToString() + "</span>";
            lblCount.Text = strOffday;


        }
        catch (Exception err)
        {

        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        PdfConverter pdfConverter = new PdfConverter();
        // string urlToConvert = tbURL.Text.Trim();
        // Label1.Text = urlToConvert;
        string thisPageURL = HttpContext.Current.Request.Url.AbsoluteUri;

        pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4;
        pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Best;
        pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait;

        pdfConverter.PdfDocumentOptions.ShowHeader = true;
        pdfConverter.PdfHeaderOptions.HeaderText = "WorkShift";
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
