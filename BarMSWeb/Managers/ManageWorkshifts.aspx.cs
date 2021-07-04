using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LC.Model.BMS.BLL;
using MyGeneration.dOOdads;
using ICSharpCode.SharpZipLib;
using System.Drawing;
using iTextSharp;
using Pdfizer;
using System.IO;
using ExpertPdf.HtmlToPdf;
public partial class Managers_ManageWorkshifts : System.Web.UI.Page
{
    int CurrentWeek;
    int UserID;
    int DepartmentID;
    int Year;
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentWeek = commonMethods.GetWeeknumber(DateTime.Now);
        lblError.Visible = false;
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            UserID = user.UserID;
            DepartmentID = user.DepartmentID;

            if (user.AccessLevel != 2)
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }
            //UserID = user.UserID;
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }
        if (!Page.IsPostBack)
        {
            //LoadOffDayGrids();
            LoadDefaultWorkshift();
            LoadImageFormats();
            LoadPDFFormats();
            LoadPageOrientations();
            LoadColors();
            LoadCompressionLevels();
            LoadPdfSubsets();
            BindDefaultDropDownsTime();
        }
    }

    private void LoadDefaultWorkshift()
    {
        int selectedweek = commonMethods.GetWeeknumber(DateTime.Now);
        if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;
        Year = DateTime.Now.Year;
        LoadWorkShiftForm(DateTime.Now.Year, selectedweek);

        LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
        txtParams.Text = "?date=" + DateTime.Now.ToShortDateString();

    }

    private void LoadOffDayGrids()
    {
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

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
        DLPreviousOffdays.DataSource = uws.DefaultView;
        DLPreviousOffdays.DataBind();
    }

    protected void btnGO_Click(object sender, ImageClickEventArgs e)
    {
        DateTime mydate = Convert.ToDateTime(datepicker.Text);
        int selectedweek = commonMethods.GetWeeknumber(mydate);
        //if (selectedweek <= CurrentWeek)
        //{
        //    lblError.Visible = true;
        //    lblError.Text = " You can set workshifts for current or past weeks";
        //    ShowWorkshift.Style.Add("display", "none");
        //    return;
        //}
        //else
        //{
        LoadWorkShiftForm(mydate.Year, selectedweek);
        Year = Convert.ToDateTime(datepicker.Text).Year;
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;
        LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
        //LoadOffDayGrids();
        txtParams.Text = "?date=" + mydate;
        //}
        // ShowWorkshift.Style.Add("display", "block");
    }

    private void BindDefaultDropDownsTime()
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

        //string WeeksHour = string.Empty;
        //string WeeksMinute = string.Empty;
        //string WeekeHour = string.Empty;
        //string WeekeMinute = string.Empty;

        //WeeksHour = WeekSTime.Substring(0, 2);
        //WeeksMinute = WeekSTime.Substring(3, 2);
        //WeekeHour = WeekETime.Substring(0, 2);
        //WeekeMinute = WeekETime.Substring(3, 2);

        #region Editing Default Time
        ddlStartHourEdit.SelectedValue = WeekSTime.Substring(0, 2);
        ddlStartMinEdit.SelectedValue = WeekSTime.Substring(3, 2);
        ddlEndHourEdit.SelectedValue = WeekETime.Substring(0, 2);
        ddlEndMinEdit.SelectedValue = WeekETime.Substring(3, 2);
        #endregion
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

        //string sHour = string.Empty;
        //string sMinute = string.Empty;
        //string eHour = string.Empty;
        //string eMinute = string.Empty;

        //sHour = STime.Substring(0, 2);
        //sMinute = STime.Substring(3, 2);
        //eHour = ETime.Substring(0, 2);
        //eMinute = ETime.Substring(3, 2);

        #region Changing Default Time

        ddlStartHoursChangeWeek.SelectedValue = WeekSTime.Substring(0, 2);
        ddlStartMinutesChangeWeek.SelectedValue = WeekSTime.Substring(3, 2);
        ddlEndHoursChangeWeek.SelectedValue = WeekETime.Substring(0, 2);
        ddlEndMinutesChangeWeek.SelectedValue = WeekETime.Substring(3, 2);

        ddlStartHoursChangeWeekend.SelectedValue = WeekendSTime.Substring(0, 2);
        ddlStartMinutesChangeWeekend.SelectedValue = WeekendSTime.Substring(3, 2);
        ddlEndHoursChangeWeekend.SelectedValue = WeekendETime.Substring(0, 2);
        ddlEndMinutesChangeWeekend.SelectedValue = WeekendETime.Substring(3, 2);

        #endregion

        #region Saving Default Time
        if (hidWeekPart.Value == "0")
        {
            ddlStartHour.SelectedValue = WeekSTime.Substring(0, 2);
            ddlStartMin.SelectedValue = WeekSTime.Substring(3, 2);
            ddlEndHour.SelectedValue = WeekETime.Substring(0, 2);
            ddlEndMin.SelectedValue = WeekETime.Substring(3, 2);
        }
        else
        {
            ddlStartHour.SelectedValue = WeekendSTime.Substring(0, 2);
            ddlStartMin.SelectedValue = WeekendSTime.Substring(3, 2);
            ddlEndHour.SelectedValue = WeekendETime.Substring(0, 2);
            ddlEndMin.SelectedValue = WeekendETime.Substring(3, 2);
        }
        #endregion

        ShowWorkshift.Style.Add("display", "block");

        DateTime startDate = commonMethods.GetWeekStartDate(year, SelectedWeek);
        startDate = startDate.AddDays(-1);


        lblSundayDate.Text = startDate.ToString("dd/MM/yyyy");
        lblMondayDate.Text = startDate.AddDays(1).ToString("dd/MM/yyyy");
        lblTuesdayDate.Text = startDate.AddDays(2).ToString("dd/MM/yyyy");
        lblWednesdayDate.Text = startDate.AddDays(3).ToString("dd/MM/yyyy");
        lblThursdayDate.Text = startDate.AddDays(4).ToString("dd/MM/yyyy");
        lblFridayDate.Text = startDate.AddDays(5).ToString("dd/MM/yyyy");
        lblSaturdayDate.Text = startDate.AddDays(6).ToString("dd/MM/yyyy");


        lblSundayDate1.Text = startDate.ToString("dd/MM/yyyy");
        lblMondayDate1.Text = startDate.AddDays(1).ToString("dd/MM/yyyy");
        lblTuesdayDate1.Text = startDate.AddDays(2).ToString("dd/MM/yyyy");
        lblWednesdayDate1.Text = startDate.AddDays(3).ToString("dd/MM/yyyy");
        lblThursdayDate1.Text = startDate.AddDays(4).ToString("dd/MM/yyyy");
        lblFridayDate1.Text = startDate.AddDays(5).ToString("dd/MM/yyyy");
        lblSaturdayDate1.Text = startDate.AddDays(6).ToString("dd/MM/yyyy");


        lblWeek.Text = " Week " + SelectedWeek;
        lblWeek.Text = "Week " + SelectedWeek.ToString() + " / " + year.ToString() + " ( Sunday " + commonMethods.GetWeekStartDate(year, SelectedWeek).AddDays(-1).ToString("dd/MM/yyyy") + " till Saturday " + commonMethods.GetWeekStartDate(year, SelectedWeek).AddDays(5).ToString("dd/MM/yyyy") + " )";

        tblSpeciality speciality = new tblSpeciality();
        speciality.LoadWorkShiftSpecialities(DepartmentID);
        tblSpeciality specialtyBlank = new tblSpeciality();
        specialtyBlank.LoadWorkShiftSpecialities(DepartmentID);
        tblUserWorkshifts uws = new tblUserWorkshifts();
        tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();
        DataTable dt = uws.LoadCurrentWorkshift(SelectedWeek, DepartmentID, year);

        string hoursBalloon = "Click to edit hours";
        string dayOffBalloon = "Days off on previous week";
        int specialtyTypeid = specialtyBlank.FkSpecialityTypeID;
        bool blankRow = false;
        specialtyBlank.MoveNext();
        for (int Counter = 1; Counter <= speciality.RowCount; Counter++)
        {
            if (specialtyTypeid != specialtyBlank.FkSpecialityTypeID)
            {
                //blankRow = true;
                //specialtyTypeid = specialtyBlank.FkSpecialityTypeID;
            }
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
                css = "rowstyle_new";
            }
            else
            {
                css = "alternate_row_new";
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
                gcSunday.Style.Add("float", "left");
                HtmlGenericControl gcSunday1 = new HtmlGenericControl("span");
                //gcSunday1.Style.Add("display", "block");
                //gcSunday1.Style.Add("width", "92px");
                //gcSunday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 1 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    string nameBalloon = string.Empty;


                    imgSundayPlus.Src = "../images/minus_img.png";
                    imgSundayPlus.Style.Add("cursor", "pointer");
                    imgSundayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    imgSundayPlus.Attributes.Add("align", "right"); //New LIne code for right side minus UKD
                    //tdSunday.InnerHtml = "<a href='#'>" + dr[0][0].ToString() + "</a>&nbsp;";
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekSTime && dr[0]["sEndTime"].ToString() == WeekETime)
                    {
                        strStyle = "style='cursor:pointer;color:Black'";
                    }
                    else
                    {
                        strStyle = "style='cursor:pointer;color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + " onclick='javascript:EditRecord(" + dr[0]["pkUserWorkshiftID"] + ")'  onmouseover=\"javascript:OpenFeedbackWindow('" + hoursBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'  > [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
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


                    strOffday = "<span " + Styleoffday + " onmouseover=\"javascript:OpenFeedbackWindow('" + dayOffBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    if (name.Length > 14)
                    {
                        name = name.Substring(0, 14).Trim() + "..";
                        nameBalloon += dr[0]["FullName"].ToString() + "<br/>Click to edit details";
                    }
                    else
                    {
                        nameBalloon = "Click to edit details";
                    }
                    gcSunday1.InnerHtml = "<a href='../Managers/EditUser.aspx?id=" + dr[0]["fkUserID"] + "' onmouseover=\"javascript:OpenFeedbackWindow('" + nameBalloon + "')\"  onmouseout='javascript:CloseFeedBackWindow()'  >" + name + "</a>" + strSpan + strOffday;//[" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]";

                }
                else
                {
                    imgSundayPlus.Src = "../images/plus_img.png";
                    imgSundayPlus.Style.Add("cursor", "pointer");
                    // imgSundayPlus.Attributes.Add("align", "right"); // New LIne code for right side plus UKD
                    imgSundayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.GetColumn("FkSpecialityTypeID") + "','" + speciality.GetColumn("pkSpecialityID") + "','1');");

                }
                gcSunday.Controls.Add(imgSundayPlus);
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
                //gcMonday.Style.Add("float", "left");
                HtmlGenericControl gcMonday1 = new HtmlGenericControl("span");
                //gcMonday1.Style.Add("display", "block");
                //gcMonday1.Style.Add("width", "92px");
                //gcMonday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 2 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    string nameBalloon = string.Empty;
                    imgMondayPlus.Src = "../images/minus_img.png";
                    imgMondayPlus.Style.Add("cursor", "pointer");
                    imgMondayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekSTime && dr[0]["sEndTime"].ToString() == WeekETime)
                    {
                        strStyle = "style='cursor:pointer;color:Black'";
                    }
                    else
                    {
                        strStyle = "style='cursor:pointer;color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + " onclick='javascript:EditRecord(" + dr[0]["pkUserWorkshiftID"] + ")'  onmouseover=\"javascript:OpenFeedbackWindow('" + hoursBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()' > [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
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
                    strOffday = "<span " + Styleoffday + " onmouseover=\"javascript:OpenFeedbackWindow('" + dayOffBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'>(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    if (name.Length > 14)
                    {
                        name = name.Substring(0, 14).Trim() + "..";
                        nameBalloon += dr[0]["FullName"].ToString() + "<br/>Click to edit details";
                    }
                    else
                    {
                        nameBalloon = "Click to edit details";
                    }
                    gcMonday1.InnerHtml = "<a href='../Managers/EditUser.aspx?id=" + dr[0]["fkUserID"] + "' onmouseover=\"javascript:OpenFeedbackWindow('" + nameBalloon + "')\"  onmouseout='javascript:CloseFeedBackWindow()'>" + name + "</a>" + strSpan + strOffday;
                }
                else
                {
                    imgMondayPlus.Src = "../images/plus_img.png";
                    imgMondayPlus.Style.Add("cursor", "pointer");
                    imgMondayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','2');");
                }
                gcMonday.Controls.Add(imgMondayPlus);
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
                //gcTuesday.Style.Add("float", "left");
                HtmlGenericControl gcTuesday1 = new HtmlGenericControl("span");
                //gcTuesday1.Style.Add("display", "block");
                //gcTuesday1.Style.Add("width", "92px");
                //gcTuesday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 3 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    string nameBalloon = string.Empty;
                    imgTuesdayPlus.Src = "../images/minus_img.png";
                    imgTuesdayPlus.Style.Add("cursor", "pointer");
                    imgTuesdayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekSTime && dr[0]["sEndTime"].ToString() == WeekETime)
                    {
                        strStyle = "style='cursor:pointer;color:Black'";
                    }
                    else
                    {
                        strStyle = "style='cursor:pointer;color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + " onclick='javascript:EditRecord(" + dr[0]["pkUserWorkshiftID"] + ")'   onmouseover=\"javascript:OpenFeedbackWindow('" + hoursBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
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
                    strOffday = "<span " + Styleoffday + " onmouseover=\"javascript:OpenFeedbackWindow('" + dayOffBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'>(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    if (name.Length > 14)
                    {
                        name = name.Substring(0, 14).Trim() + "..";
                        nameBalloon += dr[0]["FullName"].ToString() + "<br/>Click to edit details";
                    }
                    else
                    {
                        nameBalloon = "Click to edit details";
                    }
                    gcTuesday1.InnerHtml = "<a href='../Managers/EditUser.aspx?id=" + dr[0]["fkUserID"] + "' onmouseover=\"javascript:OpenFeedbackWindow('" + nameBalloon + "')\"  onmouseout='javascript:CloseFeedBackWindow()'>" + name + "</a>" + strSpan + strOffday;
                }
                else
                {
                    imgTuesdayPlus.Src = "../images/plus_img.png";
                    imgTuesdayPlus.Style.Add("cursor", "pointer");
                    imgTuesdayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','3');");
                }
                gcTuesday.Controls.Add(imgTuesdayPlus);
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
                //gcWednesday.Style.Add("float", "left");
                HtmlGenericControl gcWednesday1 = new HtmlGenericControl("span");
                //gcWednesday1.Style.Add("display", "block");
                //gcWednesday1.Style.Add("width", "92px");
                //gcWednesday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 4 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    string nameBalloon = string.Empty;
                    imgWednesdayPlus.Src = "../images/minus_img.png";
                    imgWednesdayPlus.Style.Add("cursor", "pointer");
                    imgWednesdayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekSTime && dr[0]["sEndTime"].ToString() == WeekETime)
                    {
                        strStyle = "style='cursor:pointer;color:Black'";
                    }
                    else
                    {
                        strStyle = "style='cursor:pointer;color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + " onclick='javascript:EditRecord(" + dr[0]["pkUserWorkshiftID"] + ")'  onmouseover=\"javascript:OpenFeedbackWindow('" + hoursBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
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
                    strOffday = "<span " + Styleoffday + " onmouseover=\"javascript:OpenFeedbackWindow('" + dayOffBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'>(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    if (name.Length > 14)
                    {
                        name = name.Substring(0, 14).Trim() + "..";
                        nameBalloon += dr[0]["FullName"].ToString() + "<br/>Click to edit details";
                    }
                    else
                    {
                        nameBalloon = "Click to edit details";
                    }
                    gcWednesday1.InnerHtml = "<a href='../Managers/EditUser.aspx?id=" + dr[0]["fkUserID"] + "' onmouseover=\"javascript:OpenFeedbackWindow('" + nameBalloon + "')\"  onmouseout='javascript:CloseFeedBackWindow()'>" + name + "</a>" + strSpan + strOffday;
                }
                else
                {
                    imgWednesdayPlus.Src = "../images/plus_img.png";
                    imgWednesdayPlus.Style.Add("cursor", "pointer");
                    imgWednesdayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','4');");
                }
                gcWednesday.Controls.Add(imgWednesdayPlus);
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
                //gcThursday.Style.Add("float", "left");
                HtmlGenericControl gcThursday1 = new HtmlGenericControl("span");
                //gcThursday1.Style.Add("display", "block");
                //gcThursday1.Style.Add("width", "92px");
                //gcThursday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 5 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    string nameBalloon = string.Empty;
                    imgThursdayPlus.Src = "../images/minus_img.png";
                    imgThursdayPlus.Style.Add("cursor", "pointer");
                    imgThursdayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekSTime && dr[0]["sEndTime"].ToString() == WeekETime)
                    {
                        strStyle = "style='cursor:pointer;color:Black'";
                    }
                    else
                    {
                        strStyle = "style='cursor:pointer;color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + " onclick='javascript:EditRecord(" + dr[0]["pkUserWorkshiftID"] + ")'  onmouseover=\"javascript:OpenFeedbackWindow('" + hoursBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
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
                    strOffday = "<span " + Styleoffday + " onmouseover=\"javascript:OpenFeedbackWindow('" + dayOffBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'>(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    if (name.Length > 14)
                    {
                        name = name.Substring(0, 14).Trim() + "..";
                        nameBalloon += dr[0]["FullName"].ToString() + "<br/>Click to edit details";
                    }
                    else
                    {
                        nameBalloon = "Click to edit details";
                    }
                    gcThursday1.InnerHtml = "<a href='../Managers/EditUser.aspx?id=" + dr[0]["fkUserID"] + "' onmouseover=\"javascript:OpenFeedbackWindow('" + nameBalloon + "')\"  onmouseout='javascript:CloseFeedBackWindow()'>" + name + "</a>" + strSpan + strOffday;
                }
                else
                {
                    imgThursdayPlus.Src = "../images/plus_img.png";
                    imgThursdayPlus.Style.Add("cursor", "pointer");
                    imgThursdayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','5');");
                }
                gcThursday.Controls.Add(imgThursdayPlus);
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
                //gcFriday.Style.Add("float", "left");
                HtmlGenericControl gcFriday1 = new HtmlGenericControl("span");
                //gcFriday1.Style.Add("display", "block");
                //gcFriday1.Style.Add("width", "92px");
                //gcFriday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 6 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    string nameBalloon = string.Empty;
                    imgFridayPlus.Src = "../images/minus_img.png";
                    imgFridayPlus.Style.Add("cursor", "pointer");
                    imgFridayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekendSTime && dr[0]["sEndTime"].ToString() == WeekendETime)
                    {
                        strStyle = "style='cursor:pointer;color:Black'";
                    }
                    else
                    {
                        strStyle = "style='cursor:pointer;color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + " onclick=\"javascript:EditRecord(" + dr[0]["pkUserWorkshiftID"] + ",'1')\"  onmouseover=\"javascript:OpenFeedbackWindow('" + hoursBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
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
                    strOffday = "<span " + Styleoffday + " onmouseover=\"javascript:OpenFeedbackWindow('" + dayOffBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'>(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    if (name.Length > 14)
                    {
                        name = name.Substring(0, 14).Trim() + "..";
                        nameBalloon += dr[0]["FullName"].ToString() + "<br/>Click to edit details";
                    }
                    else
                    {
                        nameBalloon = "Click to edit details";
                    }
                    gcFriday1.InnerHtml = "<a href='../Managers/EditUser.aspx?id=" + dr[0]["fkUserID"] + "' onmouseover=\"javascript:OpenFeedbackWindow('" + nameBalloon + "')\"  onmouseout='javascript:CloseFeedBackWindow()'>" + name + "</a>" + strSpan + strOffday;
                }
                else
                {
                    imgFridayPlus.Src = "../images/plus_img.png";
                    imgFridayPlus.Style.Add("cursor", "pointer");
                    imgFridayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','6','1');");
                }
                gcFriday.Controls.Add(imgFridayPlus);
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
                //gcSaturday.Style.Add("float", "left");
                HtmlGenericControl gcSaturday1 = new HtmlGenericControl("span");
                //gcSaturday1.Style.Add("display", "block");
                //gcSaturday1.Style.Add("width", "92px");
                //gcSaturday1.Style.Add("float", "left");
                dr = dt.Select("iDayNumber = " + 7 + "AND fkSpecialityID = " + speciality.GetColumn("pkSpecialityID").ToString());
                if (dr.Length > 0)
                {
                    string nameBalloon = string.Empty;
                    imgSaturdayPlus.Src = "../images/minus_img.png";
                    imgSaturdayPlus.Style.Add("cursor", "pointer");
                    imgSaturdayPlus.Attributes.Add("onclick", "javascript:DeleteRecord('" + dr[0]["pkUserWorkshiftID"] + "');");
                    string strStyle = string.Empty;
                    if (dr[0]["sStartTime"].ToString() == WeekendSTime && dr[0]["sEndTime"].ToString() == WeekendETime)
                    {
                        strStyle = "style='cursor:pointer;color:Black'";
                    }
                    else
                    {
                        strStyle = "style='cursor:pointer;color:Blue'";
                    }
                    string strSpan = "<span " + strStyle + " onclick=\"javascript:EditRecord(" + dr[0]["pkUserWorkshiftID"] + ",'1')\"     onmouseover=\"javascript:OpenFeedbackWindow('" + hoursBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'> [" + dr[0]["sStartTime"] + "-" + dr[0]["sEndTime"] + "]</span>";
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
                    strOffday = "<span " + Styleoffday + " onmouseover=\"javascript:OpenFeedbackWindow('" + dayOffBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'>(" + (7 - WorkingDays).ToString() + ")</span>";
                    string name = dr[0]["FullName"].ToString();
                    if (name.Length > 14)
                    {
                        name = name.Substring(0, 14).Trim() + "..";
                        nameBalloon += dr[0]["FullName"].ToString() + "<br/>Click to edit details";
                    }
                    else
                    {
                        nameBalloon = "Click to edit details";
                    }
                    gcSaturday1.InnerHtml = "<a href='../Managers/EditUser.aspx?id=" + dr[0]["fkUserID"] + "' onmouseover=\"javascript:OpenFeedbackWindow('" + nameBalloon + "')\"  onmouseout='javascript:CloseFeedBackWindow()'>" + name + "</a>" + strSpan + strOffday;
                }
                else
                {
                    imgSaturdayPlus.Src = "../images/plus_img.png";
                    imgSaturdayPlus.Style.Add("cursor", "pointer");
                    imgSaturdayPlus.Attributes.Add("onclick", "javascript:CopyValues('" + speciality.FkSpecialityTypeID + "','" + speciality.GetColumn("pkSpecialityID") + "','7','1');");
                }
                gcSaturday.Controls.Add(imgSaturdayPlus);
                tdSaturday.Controls.Add(gcSaturday1);
                tdSaturday.Controls.Add(gcSaturday);
                //tdSaturday.Controls.Add(imgSaturdayPlus);
                tr.Cells.Add(tdSaturday);

                tblWork.Rows.Add(tr);

            }

            speciality.MoveNext();
            specialtyBlank.MoveNext();
        }

        upnlWorkshift.Update();
        LoadOffDayGrids();

    }
    //btndel
    protected void btndel_Click(object sender, EventArgs e)
    {
        try
        {
            tblUserWorkshifts uws = new tblUserWorkshifts();
            uws.LoadByPrimaryKey(Convert.ToInt32(txtdel.Text));
            if (uws.RowCount > 0)
            {
                tblIncome income = new tblIncome();
                income.GetIncomeByWorkshiftID(uws.PkUserWorkshiftID);
                if (income.RowCount > 0)
                {
                    income.MarkAsDeleted();
                    income.Save();
                }

                uws.MarkAsDeleted();
                uws.Save();

            }
            DateTime mydate;
            if (datepicker.Text != "")
                mydate = Convert.ToDateTime(datepicker.Text);
            else
                mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            int selectedweek = commonMethods.GetWeeknumber(mydate);
            if (mydate.DayOfWeek == DayOfWeek.Sunday)
                selectedweek += 1;
            LoadWorkShiftForm(mydate.Year, selectedweek);
        }
        catch (Exception ex)
        {
            ModalPopupExtender2.Show();
            DateTime mydate;
            if (datepicker.Text != "")
                mydate = Convert.ToDateTime(datepicker.Text);
            else
                mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            int selectedweek = commonMethods.GetWeeknumber(mydate);
            if (mydate.DayOfWeek == DayOfWeek.Sunday)
                selectedweek += 1;
            LoadWorkShiftForm(mydate.Year, selectedweek);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        if (hidWeekPart.Value == "1")
        {
            lblWeekPartAdd.Text = "Weekend";
        }
        else if (hidWeekPart.Value == "0")
        {
            lblWeekPartAdd.Text = "Week Day";
        }

       string day = txtDay.Text;
        tblUsers user = new tblUsers();
        user.LoadUsersForWorkShift(DepartmentID, Convert.ToInt32(txtSpecialityTypeID.Text), selectedweek, mydate.Year, Convert.ToInt32(txtDay.Text));
        commonMethods.FillDropDownList(ddlusers, user.DefaultView, "FullName", "pkUserID");

        DateTime startDate = commonMethods.GetWeekStartDate(mydate.Year, selectedweek);
        startDate = startDate.AddDays(-1);
        string dateDays= "" ;
        if (txtDay.Text == "1")
        {
            dateDays = startDate.ToString("dd/MM/yyyy");
        }
        else if (txtDay.Text == "2")
        {
            dateDays = startDate.AddDays(1).ToString("dd/MM/yyyy");
        }
        else if(txtDay.Text == "3")
        {
         dateDays = startDate.AddDays(2).ToString("dd/MM/yyyy");
        }
        else if (txtDay.Text == "4")
        {
            dateDays = startDate.AddDays(3).ToString("dd/MM/yyyy");
        }
        else if (txtDay.Text == "5")
        {
            dateDays = startDate.AddDays(4).ToString("dd/MM/yyyy");
        }
        else if (txtDay.Text == "6")
        {
            dateDays = startDate.AddDays(5).ToString("dd/MM/yyyy");
        }
        else if (txtDay.Text == "7")
        {
        dateDays = startDate.AddDays(6).ToString("dd/MM/yyyy");
        }

        string strScript1 = " hs.addEventListener(window, 'load', function() {document.getElementById('autoload').onclick();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "select1", strScript1, true);
        
        LoadWorkShiftForm(mydate.Year, selectedweek);
        tblDays days = new tblDays();
       
        DateTime mydate3;
       // string[] strary = "";
        string[] strary = dateDays.Split('/');
        string dateDayFirst = strary[1] + "/" + strary[0] + "/" + strary[2];
        mydate3 = Convert.ToDateTime(dateDayFirst.ToString());
        days.CheckDoublecatio(mydate3, UserID, DepartmentID);
        if (days.RowCount > 0)
        {
        //tblDepartments departments = new tblDepartments();
        //departments.LoadDepartmentByMenagerID(UserID);
            string WeekSTime = string.Empty;
            string WeekETime = string.Empty;

            string WeekendSTime = string.Empty;
            string WeekendETime = string.Empty;


            WeekSTime = days.GetColumn("DayStartTime").ToString();// departments.WeekStartTime;
            WeekETime = days.GetColumn("DayEndTime").ToString();// departments.WeekEndTime;

            WeekendSTime = days.GetColumn("DayStartTime").ToString();// departments.WeekendStartTime;
            WeekendETime = days.GetColumn("DayEndTime").ToString();//  departments.WeekendEndTime;
            
            ddlStartHour.SelectedValue = WeekSTime.Substring(0, 2);
            ddlStartMin.SelectedValue = WeekSTime.Substring(3, 2);
            ddlEndHour.SelectedValue = WeekETime.Substring(0, 2);
            ddlEndMin.SelectedValue = WeekETime.Substring(3, 2);
        
        }




    }

    protected void btnEditRecord_Click(object sender, EventArgs e)
    {
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        if (hidWeekPart.Value == "1")
        {
            lblWeekPartEdit.Text = "Weekend";
        }
        else if (hidWeekPart.Value == "0")
        {
            lblWeekPartEdit.Text = "Week Day";
        }
        tblUserWorkshifts uws = new tblUserWorkshifts();
        uws.LoadByPrimaryKey(Convert.ToInt32(txtdel.Text));
        if (uws.RowCount > 0)
        {
            tblUsers user = new tblUsers();
            user.LoadByPrimaryKey(uws.FkUserID);
            lblEditUser.Text = user.SFirstName + " " + user.SLastName;
            lblID.Text = uws.PkUserWorkshiftID.ToString();
            string[] start = uws.SStartTime.Split(':');
            string[] end = uws.SEndTime.Split(':');
            ddlStartHourEdit.SelectedValue = start[0].ToString();
            ddlStartMinEdit.SelectedValue = start[1].ToString();
            ddlEndHourEdit.SelectedValue = end[0].ToString();
            ddlEndMinEdit.SelectedValue = end[1].ToString();
            string strScript1 = " hs.addEventListener(window, 'load', function() {document.getElementById('Editautoload').onclick();});";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "select1", strScript1, true);
            LoadWorkShiftForm(mydate.Year, selectedweek);

        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        tblUserWorkshifts workshift = new tblUserWorkshifts();
        workshift.LoadByPrimaryKey(Convert.ToInt32(txtddl.Text));
        if (workshift.RowCount > 0)
        {
            workshift.SStartTime = txtsh.Text + ":" + txtsm.Text;
            workshift.SEndTime = txteh.Text + ":" + txtem.Text;
            workshift.DModifiedDate = DateTime.Now;
            workshift.Save();

            DateTime mydate;
            if (datepicker.Text != "")
                mydate = Convert.ToDateTime(datepicker.Text);
            else
                mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            int selectedweek = commonMethods.GetWeeknumber(mydate);
            if (mydate.DayOfWeek == DayOfWeek.Sunday)
                selectedweek += 1;
            LoadWorkShiftForm(mydate.Year, selectedweek);
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string mystr = txtsh.Text;
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;
        DateTime startDate = commonMethods.GetWeekStartDate(mydate.Year, selectedweek).AddDays(-1);
        tblUserWorkshifts workshift = new tblUserWorkshifts();
        workshift.AddNew();
        workshift.FkUserID = Convert.ToInt32(txtddl.Text); //Convert.ToInt32(ddlusers.SelectedValue);
        workshift.s_FkSpecialityID = txtSp.Text;
        workshift.IWeekNumber = selectedweek;
        workshift.IYear = mydate.Year;
        workshift.DWeekStartDate = startDate;
        workshift.DWeekEndDate = startDate.AddDays(6);
        workshift.IDayNumber = Convert.ToInt32(txtDay.Text);
        //workshift.SStartTime = txtsh.Text + ":" + txtsm.Text;
        //workshift.SEndTime = txteh.Text + ":" + txtem.Text;
        workshift.SStartTime = txtsh.Text + ":" + txtsm.Text;
        workshift.SEndTime = txteh.Text + ":" + txtem.Text;
        workshift.BOnTime = false;
        workshift.BLate = false;
        workshift.Penalty = 0;
        workshift.Bonus = 0;
        workshift.DCreateDate = DateTime.Now;
        workshift.DModifiedDate = DateTime.Now;
        workshift.Save();

        LoadWorkShiftForm(mydate.Year, selectedweek);
    }
    protected void grdSunday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                DateTime mydate;
                if (datepicker.Text != "")
                    mydate = Convert.ToDateTime(datepicker.Text);
                else
                    mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                LinkButton lnkUser = (LinkButton)e.Row.FindControl("lnkUser");
                lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (name.Length > 10)
                {
                    lnkUser.Text = lnkUser.Text.Substring(0, 10) + "..";
                    lnkUser.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lnkUser.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
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
                string strOffday = "<span onmouseover=\"javascript:OpenFeedbackWindow('Total hours that worked the previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + hours + ") </span>" + "<span " + Styleoffday + "onmouseover=\"javascript:OpenFeedbackWindow('Staff DAYS OFF for previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + (7 - WorkingDays).ToString() + ")</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                mydiv.InnerHtml = strOffday;

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
                DateTime mydate;
                if (datepicker.Text != "")
                    mydate = Convert.ToDateTime(datepicker.Text);
                else
                    mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                LinkButton lnkUser = (LinkButton)e.Row.FindControl("lnkUser");
                lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (name.Length > 10)
                {
                    lnkUser.Text = lnkUser.Text.Substring(0, 10) + "..";
                    lnkUser.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lnkUser.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
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
                string strOffday = "<span onmouseover=\"javascript:OpenFeedbackWindow('Total hours that worked the previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + hours + ") </span>" + "<span " + Styleoffday + "onmouseover=\"javascript:OpenFeedbackWindow('Staff DAYS OFF for previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + (7 - WorkingDays).ToString() + ")</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                mydiv.InnerHtml = strOffday;

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
                DateTime mydate;
                if (datepicker.Text != "")
                    mydate = Convert.ToDateTime(datepicker.Text);
                else
                    mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                LinkButton lnkUser = (LinkButton)e.Row.FindControl("lnkUser");
                lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (name.Length > 10)
                {
                    lnkUser.Text = lnkUser.Text.Substring(0, 10) + "..";
                    lnkUser.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lnkUser.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
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
                string strOffday = "<span onmouseover=\"javascript:OpenFeedbackWindow('Total hours that worked the previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + hours + ") </span>" + "<span " + Styleoffday + "onmouseover=\"javascript:OpenFeedbackWindow('Staff DAYS OFF for previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + (7 - WorkingDays).ToString() + ")</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                mydiv.InnerHtml = strOffday;

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
                DateTime mydate;
                if (datepicker.Text != "")
                    mydate = Convert.ToDateTime(datepicker.Text);
                else
                    mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                LinkButton lnkUser = (LinkButton)e.Row.FindControl("lnkUser");
                lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (name.Length > 10)
                {
                    lnkUser.Text = lnkUser.Text.Substring(0, 10) + "..";
                    lnkUser.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lnkUser.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
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
                string strOffday = "<span onmouseover=\"javascript:OpenFeedbackWindow('Total hours that worked the previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + hours + ") </span>" + "<span " + Styleoffday + "onmouseover=\"javascript:OpenFeedbackWindow('Staff DAYS OFF for previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + (7 - WorkingDays).ToString() + ")</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                mydiv.InnerHtml = strOffday;
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
                DateTime mydate;
                if (datepicker.Text != "")
                    mydate = Convert.ToDateTime(datepicker.Text);
                else
                    mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                LinkButton lnkUser = (LinkButton)e.Row.FindControl("lnkUser");
                lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (name.Length > 10)
                {
                    lnkUser.Text = lnkUser.Text.Substring(0, 10) + "..";
                    lnkUser.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lnkUser.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
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
                string strOffday = "<span onmouseover=\"javascript:OpenFeedbackWindow('Total hours that worked the previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + hours + ") </span>" + "<span " + Styleoffday + "onmouseover=\"javascript:OpenFeedbackWindow('Staff DAYS OFF for previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + (7 - WorkingDays).ToString() + ")</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                mydiv.InnerHtml = strOffday;
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
                DateTime mydate;
                if (datepicker.Text != "")
                    mydate = Convert.ToDateTime(datepicker.Text);
                else
                    mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                LinkButton lnkUser = (LinkButton)e.Row.FindControl("lnkUser");
                lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (name.Length > 10)
                {
                    lnkUser.Text = lnkUser.Text.Substring(0, 10) + "..";
                    lnkUser.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lnkUser.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
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
                string strOffday = "<span onmouseover=\"javascript:OpenFeedbackWindow('Total hours that worked the previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + hours + ") </span>" + "<span " + Styleoffday + "onmouseover=\"javascript:OpenFeedbackWindow('Staff DAYS OFF for previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + (7 - WorkingDays).ToString() + ")</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                mydiv.InnerHtml = strOffday;
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
                DateTime mydate;
                if (datepicker.Text != "")
                    mydate = Convert.ToDateTime(datepicker.Text);
                else
                    mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                LinkButton lnkUser = (LinkButton)e.Row.FindControl("lnkUser");
                lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                lnkUser.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (name.Length > 10)
                {
                    lnkUser.Text = lnkUser.Text.Substring(0, 10) + "..";
                    lnkUser.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lnkUser.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
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
                string strOffday = "<span onmouseover=\"javascript:OpenFeedbackWindow('Total hours that worked the previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + hours + ") </span>" + "<span " + Styleoffday + "onmouseover=\"javascript:OpenFeedbackWindow('Staff DAYS OFF for previous week')\" onmouseout='javascript:CloseFeedBackWindow()' >(" + (7 - WorkingDays).ToString() + ")</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                mydiv.InnerHtml = strOffday;
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
            if (name.Length > 10)
            {
                lblName.Text = lblName.Text.Substring(0, 10) + "...";
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            Label lblCount = (Label)e.Item.FindControl("lblCount");
            int WorkingDays = Convert.ToInt32(dr["offdays"].ToString());

            string dayOffBalloon = "Staff DAYS OFF for previous week";
            string Styleoffday = string.Empty;
            if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
            {
                Styleoffday = "style='color:RED'";
            }
            else
            {
                Styleoffday = "style='color:Black'";
            }
            string strOffday = "<span " + Styleoffday + "onmouseover=\"javascript:OpenFeedbackWindow('" + dayOffBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'>(" + (7 - WorkingDays).ToString() + ")</span>";
            //lblCount.Text =  (7 - WorkingDays).ToString();
            lblCount.Text = strOffday;
        }
        catch (Exception err)
        {

        }
    }
    protected void btnGO_Click(object sender, EventArgs e)
    {
        DateTime mydate = Convert.ToDateTime(datepicker.Text);
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;

        txtCheck.Text = "2";
        txtMyDay.Text = days.ToString();

        int selectedweek = commonMethods.GetWeekNumber_New(mydate);
        //if (mydate.DayOfWeek == DayOfWeek.Sunday)
        //    selectedweek += 1;
        //if (selectedweek <= CurrentWeek)
        //{
        //    lblError.Visible = true;
        //    lblError.Text = " You can set workshifts for current or past weeks";
        //    ShowWorkshift.Style.Add("display", "none");
        //    return;
        //}
        //else
        //{
        LoadWorkShiftForm(mydate.Year, selectedweek);
        Year = mydate.Year;
        LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
        //LoadOffDayGrids();
        txtParams.Text = "?date=" + mydate;
        //}
        // ShowWorkshift.Style.Add("display", "block");
    }
    protected void lnkChangeDefaultTime_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;

        txtCheck.Text = "2";
        txtMyDay.Text = days.ToString();

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        LoadWorkShiftForm(mydate.Year, selectedweek);
        Year = mydate.Year;
        LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
        txtParams.Text = "?date=" + mydate;
    }
    protected void imgBtnSaveDefautTime_Click(object sender, ImageClickEventArgs e)
    {
        tblDepartments departments = new tblDepartments();
        departments.LoadDepartmentByMenagerID(UserID);
        if (departments.RowCount > 0)
        {
            departments.WeekStartTime = ddlStartHoursChangeWeek.SelectedItem.Text + ":" + ddlStartMinutesChangeWeek.SelectedItem.Text;
            departments.WeekEndTime = ddlEndHoursChangeWeek.SelectedItem.Text + ":" + ddlEndMinutesChangeWeek.SelectedItem.Text;

            departments.WeekendStartTime = ddlStartHoursChangeWeekend.SelectedItem.Text + ":" + ddlStartMinutesChangeWeekend.SelectedItem.Text;
            departments.WeekendEndTime = ddlEndHoursChangeWeekend.SelectedItem.Text + ":" + ddlEndMinutesChangeWeekend.SelectedItem.Text;

            departments.DModifiedDate = DateTime.Now;
            departments.Save();
        }

        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;

        txtCheck.Text = "2";
        txtMyDay.Text = days.ToString();

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        LoadWorkShiftForm(mydate.Year, selectedweek);
        ModalPopupExtender1.Hide();
    }


    protected void btnPDF_Click(object sender, EventArgs e)
    {


        if (!Page.IsValid)
            return;

        string urlToConvert = "http://localhost:1172/BarMSWeb/Managers/PrintWorkshift.aspx"; //textBoxWebPageURL.Text.Trim();

        string downloadName = "Report";
        byte[] downloadBytes = null;

        //if (radioConvertToPDF.Checked || radioConvertToSelectableText.Checked)
        //{
        downloadName += ".pdf";
        PdfConverter pdfConverter = GetPdfConverter();
        downloadBytes = pdfConverter.GetPdfBytesFromUrl(urlToConvert);
        pdfConverter.AuthenticationOptions.Username = "newuser";
        pdfConverter.AuthenticationOptions.Password = "abcd";
        //}
        //else
        //{
        //downloadName += "." + ddlImageFormat.SelectedValue;
        //ImgConverter imgConverter = GetImgConverter();
        //downloadBytes = imgConverter.GetImageFromUrlBytes(urlToConvert, GetImageFormat((RenderImageFormat)Enum.Parse(typeof(RenderImageFormat), ddlImageFormat.SelectedValue)));
        //}

        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Type", "binary/octet-stream");
        response.AddHeader("Content-Disposition",
            "attachment; filename=" + downloadName + "; size=" + downloadBytes.Length.ToString());
        response.Flush();
        response.BinaryWrite(downloadBytes);
        response.Flush();
        response.End();
    }

    private void LoadImageFormats()
    {
        string[] imageFormats = Enum.GetNames(typeof(RenderImageFormat));
        ddlImageFormat.DataSource = imageFormats;
        ddlImageFormat.DataBind();
        ddlImageFormat.SelectedValue = RenderImageFormat.Jpeg.ToString();
    }

    private void LoadCompressionLevels()
    {
        string[] pdfCompressionLevels = Enum.GetNames(typeof(PdfCompressionLevel));
        ddlCompressionLevel.DataSource = pdfCompressionLevels;
        ddlCompressionLevel.DataBind();
        ddlCompressionLevel.SelectedValue = PdfCompressionLevel.Normal.ToString();
    }

    private void LoadPageOrientations()
    {
        string[] pdfPageOrientations = Enum.GetNames(typeof(PDFPageOrientation));
        ddlPageOrientation.DataSource = pdfPageOrientations;
        ddlPageOrientation.DataBind();
        ddlPageOrientation.SelectedValue = PDFPageOrientation.Portrait.ToString();
    }

    private void LoadPDFFormats()
    {
        string[] pdfFormats = Enum.GetNames(typeof(PdfPageSize));
        ddlPDFPageFormat.DataSource = pdfFormats;
        ddlPDFPageFormat.DataBind();
        ddlPDFPageFormat.SelectedValue = PdfPageSize.A4.ToString();
    }

    private void LoadColors()
    {
        string[] colors = Enum.GetNames(typeof(KnownColor));

        ddlHeaderColor.DataSource = colors;
        ddlHeaderColor.DataBind();
        ddlHeaderColor.SelectedValue = KnownColor.Black.ToString();

        ddlFooterTextColor.DataSource = colors;
        ddlFooterTextColor.DataBind();
        ddlFooterTextColor.SelectedValue = KnownColor.Black.ToString();
    }

    private string[] pdfStandards = { "PDF", "PDF/A", "PDF/X", "PDF/SiqQA", "PDF/SiqQB" };
    private PdfStandardSubset GetPdfStandard(string standardName)
    {
        switch (standardName)
        {
            case "PDF":
                return PdfStandardSubset.Full;
            case "PDF/A":
                return PdfStandardSubset.Pdf_A_1b;
            case "PDF/X":
                return PdfStandardSubset.Pdf_X_1a;
            case "PDF/SiqQA":
                return PdfStandardSubset.Pdf_SiqQ_a;
            case "PDF/SiqQB":
                return PdfStandardSubset.Pdf_SiqQ_b;
            default:
                return PdfStandardSubset.Full;

        }
    }

    private void LoadPdfSubsets()
    {
        ddlPdfSubset.DataSource = pdfStandards;
        ddlPdfSubset.DataBind();
        ddlPdfSubset.SelectedValue = "PDF";
    }

    /// <summary>
    /// Create a PdfConverter object
    /// </summary>
    /// <returns></returns>
    private PdfConverter GetPdfConverter()
    {
        PdfConverter pdfConverter = new PdfConverter();

        //pdfConverter.LicenseKey = "put your license key here";

        // set the HTML page width in pixels
        // the default value is 1024 pixels
        if (radioCustomWebPageSize.Checked)
        {
            pdfConverter.PageWidth = int.Parse(textBoxCustomWebPageWidth.Text.Trim());
        }
        else
        {
            pdfConverter.PageWidth = 0; // autodetect the HTML page width
        }

        // set if the generated PDF contains selectable text or an embedded image - default value is true
        pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = cbGenerateSelectablePdf.Checked;

        //set the PDF page size 
        pdfConverter.PdfDocumentOptions.PdfPageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), ddlPDFPageFormat.SelectedValue);
        // set the PDF compression level
        pdfConverter.PdfDocumentOptions.PdfCompressionLevel = (PdfCompressionLevel)Enum.Parse(typeof(PdfCompressionLevel), ddlCompressionLevel.SelectedValue);
        // set the PDF page orientation (portrait or landscape)
        pdfConverter.PdfDocumentOptions.PdfPageOrientation = (PDFPageOrientation)Enum.Parse(typeof(PDFPageOrientation), ddlPageOrientation.SelectedValue.ToString());
        //set the PDF standard used to generate the PDF document
        pdfConverter.PdfStandardSubset = GetPdfStandard(ddlPdfSubset.SelectedItem.ToString());
        // show or hide header and footer
        pdfConverter.PdfDocumentOptions.ShowHeader = cbShowheader.Checked;
        pdfConverter.PdfDocumentOptions.ShowFooter = cbShowFooter.Checked;
        //set the PDF document margins
        pdfConverter.PdfDocumentOptions.LeftMargin = int.Parse(textBoxLeftMargin.Text.Trim());
        pdfConverter.PdfDocumentOptions.RightMargin = int.Parse(textBoxRightMargin.Text.Trim());
        pdfConverter.PdfDocumentOptions.TopMargin = int.Parse(textBoxTopMargin.Text.Trim());
        pdfConverter.PdfDocumentOptions.BottomMargin = int.Parse(textBoxBottomMargin.Text.Trim());
        // set if the HTTP links are enabled in the generated PDF
        pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = cbLiveLinksEnabled.Checked;
        // set if the HTML content is resized if necessary to fit the PDF page width - default is true
        pdfConverter.PdfDocumentOptions.FitWidth = cbFitWidth.Checked;
        // set if the PDF page should be automatically resized to the size of the HTML content when FitWidth is false
        pdfConverter.PdfDocumentOptions.AutoSizePdfPage = true;
        // embed the true type fonts in the generated PDF document
        pdfConverter.PdfDocumentOptions.EmbedFonts = cbEmbedFonts.Checked;
        // compress the images in PDF with JPEG to reduce the PDF document size - default is true
        pdfConverter.PdfDocumentOptions.JpegCompressionEnabled = cbJpegCompression.Checked;
        // set if the JavaScript is enabled during conversion 
        pdfConverter.ScriptsEnabled = pdfConverter.ScriptsEnabledInImage = cbScriptsEnabled.Checked;

        // set if the converter should try to avoid breaking the images between PDF pages
        pdfConverter.AvoidImageBreak = cbAvoidImageBreak.Checked;

        pdfConverter.PdfHeaderOptions.HeaderText = textBoxHeaderText.Text;
        pdfConverter.PdfHeaderOptions.HeaderTextColor = Color.FromKnownColor((KnownColor)Enum.Parse(typeof(KnownColor), ddlHeaderColor.SelectedValue));
        pdfConverter.PdfHeaderOptions.HeaderSubtitleText = textBoxHeaderSubtitle.Text;
        pdfConverter.PdfHeaderOptions.DrawHeaderLine = cbDrawHeaderLine.Checked;
        pdfConverter.PdfHeaderOptions.HeaderHeight = 50;

        pdfConverter.PdfFooterOptions.FooterText = textBoxFooterText.Text;
        pdfConverter.PdfFooterOptions.FooterTextColor = Color.FromKnownColor((KnownColor)Enum.Parse(typeof(KnownColor), ddlFooterTextColor.SelectedValue));
        pdfConverter.PdfFooterOptions.DrawFooterLine = cbDrawFooterLine.Checked;
        pdfConverter.PdfFooterOptions.PageNumberText = textBoxPageNmberText.Text;
        pdfConverter.PdfFooterOptions.ShowPageNumber = cbShowPageNumber.Checked;
        pdfConverter.PdfFooterOptions.FooterHeight = 50;

        pdfConverter.PdfBookmarkOptions.TagNames = cbBookmarks.Checked ? new string[] { "h1", "h2" } : null;

        return pdfConverter;
    }

    private ImgConverter GetImgConverter()
    {
        ImgConverter imgConverter = new ImgConverter();

        //imgConverter.LicenseKey = "put your license key here";

        // set common properties
        if (radioCustomWebPageSize.Checked)
        {
            imgConverter.PageWidth = int.Parse(textBoxCustomWebPageWidth.Text.Trim());
        }
        imgConverter.ScriptsEnabled = cbScriptsEnabled.Checked;

        return imgConverter;
    }

    private System.Drawing.Imaging.ImageFormat GetImageFormat(RenderImageFormat format)
    {
        switch (format)
        {
            case RenderImageFormat.Bmp:
                return System.Drawing.Imaging.ImageFormat.Bmp;
            case RenderImageFormat.Gif:
                return System.Drawing.Imaging.ImageFormat.Gif;
            case RenderImageFormat.Jpeg:
                return System.Drawing.Imaging.ImageFormat.Jpeg;
            case RenderImageFormat.Png:
                return System.Drawing.Imaging.ImageFormat.Png;
            case RenderImageFormat.Tiff:
                return System.Drawing.Imaging.ImageFormat.Tiff;
            default:
                return System.Drawing.Imaging.ImageFormat.Bmp;
        }
    }
    protected void lnkBtnSettings_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;

        if (pnlRenderMoreOptions.Visible)
        {
            pnlRenderMoreOptions.Visible = false;
            lnkBtnSettings.Text = "More Converter Settings >>";
        }
        else
        {
            pnlRenderMoreOptions.Visible = true;
            lnkBtnSettings.Text = "<< Hide Settings";
        }
    }
    protected void radioAutodetectWebPageSize_CheckedChanged(object sender, EventArgs e)
    {
        pnlCustomPageSize.Visible = !((RadioButton)sender).Checked;
    }
    protected void radioCustomWebPageSize_CheckedChanged(object sender, EventArgs e)
    {
        pnlCustomPageSize.Visible = ((RadioButton)sender).Checked;
    }

    protected void cbShowheader_CheckedChanged(object sender, EventArgs e)
    {
        pnlPDFHeaderOptions.Visible = ((CheckBox)sender).Checked;
    }
    protected void cbShowFooter_CheckedChanged(object sender, EventArgs e)
    {
        pnlPDFFooterOptions.Visible = ((CheckBox)sender).Checked;
    }
    protected void cvCustomPageWidth_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        int width = 0;
        try
        {
            width = int.Parse(textBoxCustomWebPageWidth.Text.Trim());
        }
        catch
        {
            args.IsValid = false;
            return;
        }
        args.IsValid = width >= 0;
    }
    protected void cvLeftMargin_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        int width = 0;
        try
        {
            width = int.Parse(textBoxLeftMargin.Text.Trim());
        }
        catch
        {
            args.IsValid = false;
            return;
        }
        args.IsValid = width >= 0;
    }
    protected void cvRightMargin_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        int width = 0;
        try
        {
            width = int.Parse(textBoxRightMargin.Text.Trim());
        }
        catch
        {
            args.IsValid = false;
            return;
        }
        args.IsValid = width >= 0;
    }
    protected void cvTopMargin_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        int width = 0;
        try
        {
            width = int.Parse(textBoxTopMargin.Text.Trim());
        }
        catch
        {
            args.IsValid = false;
            return;
        }
        args.IsValid = width >= 0;
    }
    protected void cvBottomMargin_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = true;
        int width = 0;
        try
        {
            width = int.Parse(textBoxBottomMargin.Text.Trim());
        }
        catch
        {
            args.IsValid = false;
            return;
        }
        args.IsValid = width >= 0;
    }


    protected void btnSaveDefault_Click(object sender, EventArgs e)
    {
        /*
        tblDepartments departments = new tblDepartments();
        departments.LoadDepartmentByMenagerID(UserID);
        if (departments.RowCount > 0)
        {
            departments.StartTime = ddlStartHoursChange.SelectedItem.Text + ":" + ddlStartMinutesChange.SelectedItem.Text;
            departments.EndTime = ddlEndHoursChange.SelectedItem.Text + ":" + ddlEndMinutesChange.SelectedItem.Text;
            departments.DModifiedDate = DateTime.Now;
            departments.Save();
        }

        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;

        txtCheck.Text = "2";
        txtMyDay.Text = days.ToString();

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        LoadWorkShiftForm(mydate.Year, selectedweek);
        ModalPopupExtender1.Hide();
         */
    }
    protected void lblMondayDate_Click(object sender, EventArgs e)
    {
        string txt = txtDay.Text;
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        if (hidWeekPart.Value == "1")
        {
            lblWeekPartAdd.Text = "Weekend";
        }
        else if (hidWeekPart.Value == "0")
        {
            lblWeekPartAdd.Text = "Week Day";
        }
        DateTime mydate2;
        string[] strary = (lblMondayDate.Text).Split('/');
        Label31.Text = strary[1] + "/" + strary[0] + "/" + strary[2];
        hdnf.Value = Label31.Text;
        mydate2 = Convert.ToDateTime(Label31.Text);
        Label31.Text = "Weekend Monday Default Time:";
        string strScript1 = " hs.addEventListener(window, 'load', function() {document.getElementById('autoloadNew').onclick();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "select1", strScript1, true);
        LoadWorkShiftForm(mydate.Year, selectedweek);

    }
    protected void lblTuesdayDate_Click(object sender, EventArgs e)
    {
        string txt = txtDay.Text;
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        if (hidWeekPart.Value == "1")
        {
            lblWeekPartAdd.Text = "Weekend";
        }
        else if (hidWeekPart.Value == "0")
        {
            lblWeekPartAdd.Text = "Week Day";
        }
        DateTime mydate2;
        string[] strary = (lblTuesdayDate.Text).Split('/');
        Label31.Text = strary[1] + "/" + strary[0] + "/" + strary[2];
        hdnf.Value = Label31.Text;
        mydate2 = Convert.ToDateTime(Label31.Text);
        Label31.Text = "Weekend Tuesday Default Time:";
        string strScript1 = " hs.addEventListener(window, 'load', function() {document.getElementById('autoloadNew').onclick();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "select1", strScript1, true);
        LoadWorkShiftForm(mydate.Year, selectedweek);

    }
    protected void lblWednesdayDate_Click(object sender, EventArgs e)
    {
        string txt = txtDay.Text;
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        if (hidWeekPart.Value == "1")
        {
            lblWeekPartAdd.Text = "Weekend";
        }
        else if (hidWeekPart.Value == "0")
        {
            lblWeekPartAdd.Text = "Week Day";
        }
        DateTime mydate2;
        string[] strary = (lblWednesdayDate.Text).Split('/');
        Label31.Text = strary[1] + "/" + strary[0] + "/" + strary[2];
        hdnf.Value = Label31.Text;
        mydate2 = Convert.ToDateTime(Label31.Text);
        Label31.Text = "Weekend Wednesday Default Time:";
        string strScript1 = " hs.addEventListener(window, 'load', function() {document.getElementById('autoloadNew').onclick();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "select1", strScript1, true);
        LoadWorkShiftForm(mydate.Year, selectedweek);

    }
    protected void lblThursdayDate_Click(object sender, EventArgs e)
    {
        string txt = txtDay.Text;
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        if (hidWeekPart.Value == "1")
        {
            lblWeekPartAdd.Text = "Weekend";
        }
        else if (hidWeekPart.Value == "0")
        {
            lblWeekPartAdd.Text = "Week Day";
        }
        DateTime mydate2;
        string[] strary = (lblThursdayDate.Text).Split('/');
        Label31.Text = strary[1] + "/" + strary[0] + "/" + strary[2];
        hdnf.Value = Label31.Text;
        mydate2 = Convert.ToDateTime(Label31.Text);
        Label31.Text = "Weekend Thursday Default Time:";
        string strScript1 = " hs.addEventListener(window, 'load', function() {document.getElementById('autoloadNew').onclick();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "select1", strScript1, true);
        LoadWorkShiftForm(mydate.Year, selectedweek);

    }
    protected void lblFridayDate_Click(object sender, EventArgs e)
    {
        string txt = txtDay.Text;
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        if (hidWeekPart.Value == "1")
        {
            lblWeekPartAdd.Text = "Weekend";
        }
        else if (hidWeekPart.Value == "0")
        {
            lblWeekPartAdd.Text = "Week Day";
        }
        DateTime mydate2;
        string[] strary = (lblFridayDate.Text).Split('/');
        Label31.Text = strary[1] + "/" + strary[0] + "/" + strary[2];
        hdnf.Value = Label31.Text;
        mydate2 = Convert.ToDateTime(Label31.Text);
        Label31.Text ="Weekend Friday Default Time:";
        string strScript1 = " hs.addEventListener(window, 'load', function() {document.getElementById('autoloadNew').onclick();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "select1", strScript1, true);
        LoadWorkShiftForm(mydate.Year, selectedweek);

    }
    protected void lblSaturdayDate_Click(object sender, EventArgs e)
    {
        string txt = txtDay.Text;
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        if (hidWeekPart.Value == "1")
        {
            lblWeekPartAdd.Text = "Weekend";
        }
        else if (hidWeekPart.Value == "0")
        {
            lblWeekPartAdd.Text = "Week Day";
        }
        DateTime mydate2;
        string[] strary = (lblSundayDate.Text).Split('/');
        Label31.Text = strary[1] + "/" + strary[0] + "/" + strary[2];
        hdnf.Value = Label31.Text;
        mydate2 = Convert.ToDateTime(Label31.Text);
        Label31.Text = "Weekend Saturday Default Time:";
        string strScript1 = " hs.addEventListener(window, 'load', function() {document.getElementById('autoloadNew').onclick();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "select1", strScript1, true);
        LoadWorkShiftForm(mydate.Year, selectedweek);

    }
    protected void lblSundayDate_Click(object sender, EventArgs e)
    {
        string txt = txtDay.Text;
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        if (hidWeekPart.Value == "1")
        {
            lblWeekPartAdd.Text = "Weekend";
        }
        else if (hidWeekPart.Value == "0")
        {
            lblWeekPartAdd.Text = "Week Day";
        }
        DateTime mydate2;
        string[] strary = (lblSundayDate.Text).Split('/');
        Label31.Text = strary[1] + "/" + strary[0] + "/" + strary[2];
        hdnf.Value = Label31.Text;
        mydate2 = Convert.ToDateTime(Label31.Text);
        Label31.Text = "Weekend Sunday Default Time:";
        string strScript1 = " hs.addEventListener(window, 'load', function() {document.getElementById('autoloadNew').onclick();});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "select1", strScript1, true);
        LoadWorkShiftForm(mydate.Year, selectedweek);

    }
    protected void btnAddbtnEdit_Click(object sender, EventArgs e)
    {
        DateTime mydate2;
        if (datepicker.Text != "")
            mydate2 = Convert.ToDateTime(datepicker.Text);
        else
            mydate2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        int selectedweek2 = commonMethods.GetWeeknumber(mydate2);
        if (mydate2.DayOfWeek == DayOfWeek.Sunday)
            selectedweek2 += 1;

        if (hidWeekPart.Value == "1")
        {
            lblWeekPartAdd.Text = "Weekend";
        }
        else if (hidWeekPart.Value == "0")
        {
            lblWeekPartAdd.Text = "Week Day";
        }
       
       
       
        string txt= txtDay.Text;
        DateTime mydate;
        string startH = txtsh.Text;// DropDownList1.SelectedItem.Text;
        string startM = txtsm.Text;//DropDownList2.SelectedItem.Text;
        string EndtH = txteh.Text; //DropDownList3.SelectedItem.Text;
        string EndtM = txtem.Text;//DropDownList4.SelectedItem.Text;
        string ddl = txtddl.Text;
       string usid= UserID.ToString();
       string Depid = DepartmentID.ToString();
       
        mydate = Convert.ToDateTime(ddl.ToString());
        int selectedweek = commonMethods.GetWeeknumber(mydate);
        tblDays days = new tblDays();
        days.CheckDoublecatio(mydate,UserID,DepartmentID);
        
        if (days.RowCount > 0)
        {
            int primaryky = Convert.ToInt16(days.GetColumn("DepartmentDaysId").ToString());
            days.LoadByPrimaryKey(primaryky);
            days.DayStartTime = txtsh.Text + ":" + txtsm.Text;
            days.DayEndTime = txteh.Text + ":" + txtem.Text;
            days.ModifyDate = DateTime.Now.Date;
            days.Save();
        
        }
        else
        {
        days.AddNew();
        days.DateDay = mydate.Date;
        days.DaysName = mydate.DayOfWeek.ToString();
        days.YearDay = mydate.Year.ToString();
        days.WeekNumber = selectedweek2.ToString();
        days.FkManagerUserID = UserID;
        days.PkDepartmentID = DepartmentID;
        days.DayStartTime = txtsh.Text + ":" + txtsm.Text;
        days.DayEndTime = txteh.Text + ":" + txtem.Text;
        days.CreatedDate = DateTime.Now.Date;
        days.ModifyDate = DateTime.Now.Date;
        days.Save();
        }
        LoadWorkShiftForm(mydate2.Year, selectedweek2);

    }
}
