using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LC.Model.BMS.BLL;

public partial class Managers_Test_ManagerDayOff : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    int WeekNumber;
    int Day;
    int Year;
    static string selectedDate = "";
    DataTable dtOffDaysRecord = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        dtOffDaysRecord = getAllOffDays();
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];

            if (user.AccessLevel != 2)
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }

            UserID = user.UserID;
            DepartmentID = user.DepartmentID;
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }
        if ((Request.QueryString["week"] != null) && (Request.QueryString["week"] != ""))
        {

            WeekNumber = Convert.ToInt32(Request.QueryString["week"]);
        }
        else
        {
            WeekNumber = commonMethods.GetWeeknumber(DateTime.Now);
        }
        if ((Request.QueryString["year"] != null) && (Request.QueryString["year"] != ""))
        {

            Year = Convert.ToInt32(Request.QueryString["year"]);
        }
        else
        {
            Year = DateTime.Now.Year;
        }
        if ((Request.QueryString["day"] != null) && (Request.QueryString["day"] != ""))
        {

            Day = Convert.ToInt32(Request.QueryString["day"]);
        }
        else
        {
            Day = DateTime.Now.Day;
        }
        lblWeekDates.Text = "Week " + WeekNumber.ToString() + " / " + Year.ToString() + " ( Sunday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1).ToString("dd/MM/yyyy") + " till Saturday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5).ToString("dd/MM/yyyy") + " )";

        if (!IsPostBack)
        {
            GetManagerName();
            getDaysOff();
        }
    }
    protected void btnGO_Click(object sender, EventArgs e)
    {
        selectedDate = Convert.ToDateTime(datepicker.Text).ToShortDateString();
        WeekNumber = commonMethods.GetWeekNumber_New(Convert.ToDateTime(datepicker.Text));
        Year = Convert.ToDateTime(datepicker.Text).Year;
        DateTime mydate = Convert.ToDateTime(datepicker.Text);

        txtDayOff.Text = mydate.Day + "/" + mydate.Month + "/" + mydate.Year;

        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;
        Day = days;
        txtCheck.Text = "2";
        txtMyDay.Text = days.ToString();
        Response.Redirect("../Managers/ManagerDayOff.aspx?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year + "&day=" + days);
        hidParam.Value = "?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year;
        getDaysOff();
    }

    #region GetName
    private void GetManagerName()
    {
        tblUsers u = new tblUsers();
        u.LoadByPrimaryKey(UserID);
        if (u.RowCount > 0)
        {
            lblManagerName.Text = u.SFirstName + " " + u.SLastName;
        }
    }

    #endregion

    #region Get Day Off

    private void getDaysOff()
    {
        //string d = DateTime.Now.Month + "/1/" + DateTime.Now.Year;
        //string d = DateTime.Now.Month + "/"+DateTime.Now.Month
        DateTime myDate;
        if (datepicker.Text != "")
            myDate = Convert.ToDateTime(datepicker.Text);
        else if (selectedDate != "")
            myDate = Convert.ToDateTime(selectedDate);
        else
            myDate = DateTime.Now;
        tblManagerDayOff dayOff = new tblManagerDayOff();
        dayOff.getSingleDayOff(UserID, myDate.Date);
        if (dayOff.RowCount > 0)
        {
            imgBtnMessage.Visible = false;
            imgTrash.Visible = true;

            ViewState["mdoid"] = dayOff.PkManagerDayOffID;
            txtDayOff.Text = dayOff.MSingleDate.Day + "/" + dayOff.MSingleDate.Month + "/" + dayOff.MSingleDate.Year;
            txtReason.Text = dayOff.MReason;
        }
        else if (dayOff.RowCount == 0)
        {
            dayOff.FlushData();
            dayOff.getRangeDayOff(UserID, myDate.Date);
            if (dayOff.RowCount > 0)
            {
                imgBtnMessage.Visible = false;
                imgTrash.Visible = true;

                ViewState["mdoid"] = dayOff.PkManagerDayOffID;
                txtStartDate.Text = dayOff.MStartDate.Day + "/" + dayOff.MStartDate.Month + "/" + dayOff.MStartDate.Year;
                txtTillDate.Text = dayOff.MEndDate.Day + "/" + dayOff.MEndDate.Month + "/" + dayOff.MEndDate.Year;
                txtReasonLong.Text = dayOff.MLongReason;
            }
            else
            {
                imgBtnMessage.Visible = true;
                imgTrash.Visible = false;
            }
        }
        else
        {

            imgBtnMessage.Visible = true;
            imgTrash.Visible = false;
        }

    }


    #endregion

    #region Send Mesage for Day Off

    protected void imgBtnMessage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string[] start_D = txtDayOff.Text.Split('/');
            string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
            tblManagerDayOff dayoff = new tblManagerDayOff();
            dayoff.AddNew();
            dayoff.Fkuserid = UserID;
            dayoff.MSingleDate = Convert.ToDateTime(start);
            dayoff.MReason = txtReason.Text;
            dayoff.DModifiedDate = DateTime.Now.Date;
            dayoff.DCreatedDate = DateTime.Now.Date;
            dayoff.Save();


            int departmentAdminid = 0;
            tblUsers u = new tblUsers();
            u.getDepartmentAdmin(DepartmentID);
            if (u.RowCount > 0)
            {
                departmentAdminid = u.PkUserID;
            }

            string name = string.Empty;
            u.FlushData();
            u.LoadByPrimaryKey(UserID);
            if (u.RowCount > 0)
            {
                name = u.SFirstName + " " + u.SLastName;
            }
            #region Email to department admin
            /*
        int departmentAdminid = 0;
        tblUsers u = new tblUsers();
        u.getDepartmentAdmin(DepartmentID);
        if (u.RowCount > 0)
        {
            departmentAdminid = u.PkUserID;
        }


        Emailing email = new Emailing();
        tblUserEmails ue = new tblUserEmails();
        ue.LoadUserEmails(UserID);
        if (ue.RowCount > 0)
        {
            email.P_FromAddress = ue.SEmail;
        }
        ue.FlushData();
        ue.LoadUserEmails(departmentAdminid);
        if (ue.RowCount > 0)
        {
            email.P_ToAddress = ue.SEmail;
        }


        email.P_Email_Subject = "";
        email.P_Message_Body = "";
        email.Send_Email();
        */
            #endregion



            #region Internal Message

            tblUserInBox userIn = new tblUserInBox();
            // For Department Admin
            userIn.AddNew();
            userIn.FkFromUserID = UserID;
            userIn.FkToUserID = departmentAdminid;
            userIn.SSubject = "Manager Day Off";
            userIn.SMessage = "This is a system notification to let you know that the manager “ " + name + " ” is having a programmed day off for date " + start;
            userIn.DReceivedDate = DateTime.Now;
            userIn.BIsread = false;
            userIn.Save();
            userIn.FlushData();


            tblUserSentBox userOut = new tblUserSentBox();

            //For Current Manager
            userOut.AddNew();
            userOut.FkFromUserID = UserID;
            userOut.FkToUserID = departmentAdminid;
            userOut.SSubject = "Manager Day Off";
            userOut.SMessage = "This is a system notification to let you know that the manager “ " + name + " ” is having a programmed day off for date " + start;
            userOut.DSentDate = DateTime.Now;
            userOut.Save();
            userOut.FlushData();

            #endregion


            lblRecordMessage.Text = "Day Off Message is sent to department admin Successfully!";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "del", "$(function(){RecordSaved();});", true);

            ViewState["mdoid"] = dayoff.PkManagerDayOffID;

            imgBtnMessage.Visible = true;
            imgTrash.Visible = true;
        }
        catch (Exception ex)
        { }
    }
    #endregion

    #region Deleting Message for Day
    protected void imgTrash_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["mdoid"] != null)
        {
            tblManagerDayOff dayoff = new tblManagerDayOff();
            dayoff.LoadByPrimaryKey(Convert.ToInt32(ViewState["mdoid"]));
            if (dayoff.RowCount > 0)
            {
                dayoff.MarkAsDeleted();
                dayoff.Save();
                txtDayOff.Text = "";
                txtReason.Text = "";
                txtStartDate.Text = "";
                txtTillDate.Text = "";
                txtReasonLong.Text = "";
                lblRecordMessage.Text = "Program Day Off is removed Successfully!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "del", "$(function(){RecordSaved();});", true);
            }
            ViewState["mdoid"] = null;

            imgBtnMessage.Visible = true;
            imgTrash.Visible = false;
        }
    }
    #endregion

    #region Saving a programmed day off

    protected void imgBtnProgram_Click(object sender, ImageClickEventArgs e)
    {
        string[] start_D = txtStartDate.Text.Split('/');
        string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];


        start_D = txtTillDate.Text.Split('/');
        string end = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
        DateTime dStart = Convert.ToDateTime(start);
        while (true)
        {
            tblManagerDayOff dayOff = new tblManagerDayOff();
            dayOff.getSingleDayOff(UserID, dStart.Date);
            if (dayOff.RowCount > 0)
            {
                dayOff.MarkAsDeleted();
                dayOff.Save();
            }
            dStart = dStart.AddDays(1);
            if (dStart <= Convert.ToDateTime(end))
            {

            }
            else
            {
                break;
            }
        }
        tblManagerDayOff dayoff = new tblManagerDayOff();
        dayoff.AddNew();
        dayoff.Fkuserid = UserID;
        dayoff.MStartDate = Convert.ToDateTime(start);
        dayoff.MEndDate = Convert.ToDateTime(end);
        dayoff.MLongReason = txtReasonLong.Text;
        dayoff.DModifiedDate = DateTime.Now.Date;
        dayoff.DCreatedDate = DateTime.Now.Date;
        dayoff.Save();


        int departmentAdminid = 0;
        tblUsers u = new tblUsers();
        u.getDepartmentAdmin(DepartmentID);
        if (u.RowCount > 0)
        {
            departmentAdminid = u.PkUserID;
        }

        string name = string.Empty;
        u.FlushData();
        u.LoadByPrimaryKey(UserID);
        if (u.RowCount > 0)
        {
            name = u.SFirstName + " " + u.SLastName;
        }

        #region Internal Message

        tblUserInBox userIn = new tblUserInBox();
        // For Department Admin
        userIn.AddNew();
        userIn.FkFromUserID = UserID;
        userIn.FkToUserID = departmentAdminid;
        userIn.SSubject = "Manager Day Off";
        userIn.SMessage = "This is a system notification to let you know that the manager “ " + name + " ” is having a programmed day off from date (" + start + ") to  (" + end + ")";
        userIn.DReceivedDate = DateTime.Now;
        userIn.BIsread = false;
        userIn.Save();
        userIn.FlushData();

        tblUserSentBox userOut = new tblUserSentBox();

        //For Current Manager
        userOut.AddNew();
        userOut.FkFromUserID = UserID;
        userOut.FkToUserID = departmentAdminid;
        userOut.SSubject = "Manager Day Off";
        userOut.SMessage = "This is a system notification to let you know that the manager “ " + name + " ” is having a programmed day off from date (" + start + ") to  (" + end + ")";
        userOut.DSentDate = DateTime.Now;
        userOut.Save();
        userOut.FlushData();

        #endregion


        lblRecordMessage.Text = "Day Off Message is sent to department admin Successfully!";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "del", "$(function(){RecordSaved();});", true);
    }
    #endregion

    protected void ManagerCalendar_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            CalendarDay day = (CalendarDay)e.Day;
            TableCell cell = (TableCell)e.Cell;
            if (!day.IsOtherMonth)
            {
                foreach (DataRow dr in dtOffDaysRecord.Rows)
                {
                    DateTime dCalendarDate = day.Date;
                    DateTime tempDate = Convert.ToDateTime(dr["dOffDate"]);
                    if (dCalendarDate == tempDate)
                    {
                        int iManagerID = Convert.ToInt32(dr["iUserID"]);                       
                        cell.BackColor = System.Drawing.Color.Green;
                        cell.Attributes.Add("onclick", "datePic('" + day.Date + "')");                        
                    }
                    
                }
                cell.Style.Add("cursor", "pointer");
            }
        //    FlagShowVideo = false;
        //    CalendarDay day = (CalendarDay)e.Day;
        //    TableCell cell = (TableCell)e.Cell;
        //    cell.ID = "cell" + e.Day.DayNumberText + e.Day.Date.Month.ToString();

        //    HtmlTable tblMainCell = new HtmlTable();
        //    ImageButton imgVideoPath = new ImageButton();
        //    imgVideoPath.ID = "Vid" + e.Day.DayNumberText + e.Day.Date.Month.ToString();
        //    Image imgMisedVideo = new Image();
        //    imgMisedVideo.ID = "btnMised" + e.Day.DayNumberText + e.Day.Date.Month.ToString();
        //    imgMisedVideo.ImageUrl = "../images/MisedDate.png";

        //    Label lblEventdescription = new Label();
        //    Label lblBirthDay = new Label();
        //    HtmlAnchor LnkMeal1 = new HtmlAnchor();
        //    HtmlAnchor LnkMeal2 = new HtmlAnchor();
        //    HtmlAnchor LnkMeal3 = new HtmlAnchor();

        //    lblEventdescription.ID = "lblEvent" + e.Day.DayNumberText + e.Day.Date.Month.ToString();
        //    lblBirthDay.ID = "lblBD" + e.Day.DayNumberText + e.Day.Date.Month.ToString();
        //    LnkMeal1.ID = "Lnk1" + e.Day.DayNumberText + e.Day.Date.Month.ToString();
        //    LnkMeal2.ID = "Lnk2" + e.Day.DayNumberText + e.Day.Date.Month.ToString();
        //    LnkMeal3.ID = "Lnk3" + e.Day.DayNumberText + e.Day.Date.Month.ToString();

        //    LnkMeal1.InnerHtml = "Meal 1";
        //    LnkMeal2.InnerHtml = "Meal 2";
        //    LnkMeal3.InnerHtml = "Meal 3";

        //    //cell.Width = 140;
        //    cell.Height = 140;
        //    HtmlTableRow tr0 = new HtmlTableRow();
        //    HtmlTableCell td01 = new HtmlTableCell();
        //    td01.Attributes.Add("align", "left");
        //    td01.Attributes.Add("colspan", "2");
        //    td01.Attributes.Add("width", "20");
        //    td01.Controls.Add(lblEventdescription);
        //    tr0.Cells.Add(td01);
        //    tblMainCell.Rows.Add(tr0);

        //    HtmlTableRow tr01 = new HtmlTableRow();
        //    HtmlTableCell td001 = new HtmlTableCell();
        //    td001.Attributes.Add("align", "left");
        //    td001.Attributes.Add("colspan", "2");
        //    td001.Controls.Add(lblBirthDay);
        //    tr01.Cells.Add(td001);
        //    tblMainCell.Rows.Add(tr01);

        //    HtmlTableRow tr1 = new HtmlTableRow();
        //    HtmlTableCell td11 = new HtmlTableCell();

        //    td11.Controls.Add(LnkMeal1);
        //    td11.Attributes.Add("align", "left");
        //    td11.Attributes.Add("width", "42px");

        //    HtmlTableCell td12 = new HtmlTableCell();
        //    td12.Controls.Add(imgVideoPath);
        //    td12.Attributes.Add("rowspan", "3");
        //    tr1.Cells.Add(td11);
        //    tr1.Cells.Add(td12);
        //    tblMainCell.Rows.Add(tr1);

        //    HtmlTableRow tr2 = new HtmlTableRow();
        //    HtmlTableCell td21 = new HtmlTableCell();
        //    td21.Controls.Add(LnkMeal2);
        //    td21.Attributes.Add("align", "left");
        //    tr2.Cells.Add(td21);
        //    tblMainCell.Rows.Add(tr2);

        //    HtmlTableRow tr3 = new HtmlTableRow();
        //    HtmlTableCell td31 = new HtmlTableCell();
        //    td31.Controls.Add(LnkMeal3);
        //    td31.Attributes.Add("align", "left");
        //    tr3.Cells.Add(td31);
        //    tblMainCell.Rows.Add(tr3);

        //    HtmlTableRow tr4 = new HtmlTableRow();
        //    HtmlTableCell td41 = new HtmlTableCell();
        //    td41.Controls.Add(imgMisedVideo);
        //    td41.Attributes.Add("colspan", "2");
        //    td41.Attributes.Add("align", "right");
        //    tr4.Cells.Add(td41);
        //    tblMainCell.Rows.Add(tr4);
        //    cell.Controls.Add(tblMainCell);

        //    imgVideoPath.Style.Add("display", "none");
        //    imgMisedVideo.Style.Add("display", "none");
        //    LnkMeal1.Style.Add("color", "#245edc");
        //    LnkMeal2.Style.Add("color", "#245edc");
        //    LnkMeal3.Style.Add("color", "#245edc");
        //    LnkMeal1.Style.Add("align", "left");
        //    LnkMeal2.Style.Add("align", "left");
        //    LnkMeal3.Style.Add("align", "left");
        //    LnkMeal1.Style.Add("display", "none");
        //    LnkMeal2.Style.Add("display", "none");
        //    LnkMeal3.Style.Add("display", "none");

        //    /////////// This Portion is For User's selected schedule for videos ============================ new =========
            //if (!day.IsOtherMonth)
            //{
        //        //if (day.Date >= CalendarFillStartDate.Date && day.Date <= CalendarfillEndDate.Date)
        //        {
        //            if ((lstUserWorkOutVideosDetail_All != null) && (lstUserWorkOutVideosDetail_All.Count > 0))
        //            {

        //                List<UserWorkoutVideosDetail> lstTempDetail = lstUserWorkOutVideosDetail_All.Where(p => p.VideoDate != null && p.VideoDate.Value.Date == day.Date).ToList();
        //                if ((lstTempDetail != null) && (lstTempDetail.Count > 0))
        //                {

        //                    Video objVideoTemp = VideoStaticRepository.GetEntity(lstTempDetail[0].VideoID);
        //                    string URLOfVideo = "MyVideos.aspx?Date=" + day.Date.ToShortDateString();
        //                    string ImageName = objVideoTemp.VideoPath.Substring(objVideoTemp.VideoPath.LastIndexOf("\\") + 1).ToString().Split('.').First();
        //                    string Imagepath = Server.MapPath("~/Temp/" + ImageName + ".GIF");
        //                    imgVideoPath.ImageUrl = objVideoTemp.VideoImage;
        //                    imgVideoPath.Attributes.Add("onclick", "PlayVideo('" + URLOfVideo + "');");
        //                    imgVideoPath.Width = 80;
        //                    imgVideoPath.Height = 80;
        //                    imgVideoPath.Style.Add("display", "block");
        //                    imgMisedVideo.Style.Add("display", "block");
        //                    cell.Style.Add("cursor", "pointer");
        //                    imgMisedVideo.Attributes.Add("onclick", "javascript:return ShowHideConfirmDiv();");

        //                    foreach (UserWorkoutVideosDetail ud in lstTempDetail)
        //                    {
        //                        FlagShowVideo = true;
        //                        if (ud.IsDefaultVideo == true)
        //                        {
        //                            cell.BackColor = System.Drawing.Color.FromArgb(24, 182, 240);
        //                            cell.ToolTip = "Your Workout Day.";
        //                            imgVideoPath.ToolTip = objVideoTemp.VideoTitle;
        //                        }
        //                        else
        //                        {
        //                            cell.ToolTip = "Your Workout Day.";
        //                            imgVideoPath.ToolTip = objVideoTemp.VideoTitle;
        //                            cell.BackColor = System.Drawing.Color.White;
        //                            break;
        //                        }
        //                    }
        //                    if (day.Date < CalendarFillStartDate.Date)
        //                    {
        //                        imgMisedVideo.Style.Add("display", "none");
        //                    }
        //                }
        //            }
        //            cell.Attributes.Add("onclick", "ShowCalendarDescription('" + day.Date + "')");
        //            cell.Style.Add("cursor", "pointer");
        //        }

        //        /////////// End Portion is For User's selected schedule
        //        //======================== Portion For User's Meal Plans

        //        //if (day.Date >= CalendarFillStartDate.Date && day.Date <= CalendarFillStartDate.AddDays(365).Date)
        //        {

        //            if (lstUserMealPlan != null && lstUserMealPlan.Count > 0)
        //            {
        //                if (lstUserMealPlan.Where(us => us.MealDate.Value.Date == day.Date).Count() > 0)
        //                {
        //                    UserMealPlan objUserMealPlan = lstUserMealPlan.Where(us => us.MealDate.Value.Date == day.Date).ToList().First();
        //                    if (objUserMealPlan.Meal1ID != null)
        //                    {
        //                        LnkMeal1.HRef = "RecipeContentPage.aspx?recpId=" + objUserMealPlan.Meal1ID;
        //                        Recipe objRecipe = RecipeStaticRepository.GetEntity(Convert.ToInt32(objUserMealPlan.Meal1ID));
        //                        LnkMeal1.Title = objRecipe.RecipeName;
        //                    }
        //                    if (objUserMealPlan.Meal2ID != null)
        //                    {
        //                        LnkMeal2.HRef = "RecipeContentPage.aspx?recpId=" + objUserMealPlan.Meal2ID;
        //                        Recipe objRecipe = RecipeStaticRepository.GetEntity(Convert.ToInt32(objUserMealPlan.Meal2ID));
        //                        LnkMeal2.Title = objRecipe.RecipeName;
        //                    }
        //                    if (objUserMealPlan.Meal3ID != null)
        //                    {
        //                        LnkMeal3.HRef = "RecipeContentPage.aspx?recpId=" + objUserMealPlan.Meal3ID;
        //                        Recipe objRecipe = RecipeStaticRepository.GetEntity(Convert.ToInt32(objUserMealPlan.Meal3ID));
        //                        LnkMeal3.Title = objRecipe.RecipeName;
        //                    }
        //                    LnkMeal1.Style.Add("display", "block");
        //                    LnkMeal2.Style.Add("display", "block");
        //                    LnkMeal3.Style.Add("display", "block");
        //                }
        //            }
        //        }

        //        //======================== End Portion For User's Meal Plans
        //        //========== To Set Off Days And Rest week =====
        //        if (liUserOffDates != null && liUserOffDates.Count > 0)
        //        {
        //            int OffDateCount = liUserOffDates.Count();
        //            for (int i = 0; i < OffDateCount; i++)
        //            {
        //                if (liUserOffDates.Count > 0)
        //                {
        //                    if (day.Date >= liUserOffDates[i].OffDateStart.Date && day.Date <= liUserOffDates[i].OffDateEnd.Value.Date)
        //                    {
        //                        cell.BackColor = System.Drawing.Color.FromArgb(0, 176, 80);
        //                        imgVideoPath.Style.Add("display", "none"); // these line were added for test...
        //                        imgMisedVideo.Style.Add("display", "none");
        //                        cell.ToolTip = "Rest Period";
        //                        FlagShowVideo = false;
        //                    }
        //                }
        //            }
        //        }
        //        //========== End Of Set Off Days And Rest week 
        //        //========== To Set News Dates
        //        if (lstUserNewsEvent != null && lstUserNewsEvent.Count > 0)
        //        {
        //            foreach (UserNewsEvent UNE in lstUserNewsEvent)
        //            {
        //                if (UNE.EventStartDate != null && UNE.EventEndDate != null)
        //                {
        //                    if (day.Date >= UNE.EventStartDate.Value.Date && day.Date <= UNE.EventEndDate.Value.Date)
        //                    {
        //                        if (day.Date >= CalendarFillStartDate.Date && day.Date <= CalendarfillEndDate.Date)
        //                        {
        //                            if (FlagShowVideo == true)
        //                            {
        //                                cell.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
        //                                cell.ToolTip = "Special Event";
        //                                lblEventdescription.Text = UNE.EventDescription + "<br/>";
        //                                lblEventdescription.ForeColor = System.Drawing.Color.Blue;
        //                                imgVideoPath.Style.Add("display", "block");
        //                                imgMisedVideo.Style.Add("display", "block");
        //                            }
        //                            else
        //                            {
        //                                cell.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
        //                                cell.ToolTip = "Special Event";
        //                                lblEventdescription.Text = UNE.EventDescription + "<br/>";
        //                                lblEventdescription.ForeColor = System.Drawing.Color.Blue;
        //                                imgVideoPath.Style.Add("display", "none");
        //                                imgMisedVideo.Style.Add("display", "none");
        //                                FlagShowVideo = false;
        //                            }

        //                        }
        //                        else
        //                        {
        //                            //HtmlTableRow tr0 = new HtmlTableRow();
        //                            //HtmlTableCell td01 = new HtmlTableCell();
        //                            td01.Attributes.Add("align", "left");
        //                            td01.Controls.Add(lblEventdescription);
        //                            tr0.Cells.Add(td01);
        //                            tblMainCell.Rows.Add(tr0);
        //                            cell.Controls.Add(tblMainCell);

        //                            cell.BackColor = System.Drawing.Color.FromArgb(255, 255, 0);
        //                            cell.ToolTip = "Special Event";
        //                            lblEventdescription.Text = UNE.EventDescription + "<br/>";
        //                            lblEventdescription.ForeColor = System.Drawing.Color.Blue;
        //                            imgVideoPath.Style.Add("display", "none"); // these line were added for test...
        //                            imgMisedVideo.Style.Add("display", "none");
        //                            FlagShowVideo = false;
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //        //========== End News Dates

        //        if (string.IsNullOrEmpty(cell.ToolTip))
        //        {
        //            cell.ToolTip = "Your off Day";

        //        }

        //        if ((lstUserWorkOutVideosdetail_Missed != null) && (lstUserWorkOutVideosdetail_Missed.Count > 0))
        //        {
        //            int MisseddaysCount = lstUserWorkOutVideosdetail_Missed.Count();
        //            for (int i = 0; i < MisseddaysCount; i++)
        //            {
        //                if (day.Date == lstUserWorkOutVideosdetail_Missed[i].VideoDate)
        //                {
        //                    imgVideoPath.Style.Add("display", "block");
        //                    imgMisedVideo.Style.Add("display", "none");
        //                    cell.BackColor = System.Drawing.Color.FromArgb(184, 183, 250);
        //                    cell.ToolTip = "You Missed This Day's Activity";
        //                    FlagShowVideo = false;
        //                }
        //            }
        //        }
        //        imgMisedVideo.Attributes.Add("onclick", "javascript:return ShowHideConfirmDiv();");
        //        if (objUser_tbl != null)
        //        {
        //            if (day.Date.Month == objUser_tbl.BirthDate.Date.Month && day.Date.Day == objUser_tbl.BirthDate.Date.Day)
        //            {
        //                cell.BackColor = System.Drawing.Color.Pink;
        //                cell.ToolTip = "your Birthday";
        //                lblBirthDay.Text = "Birthday: " + day.Date.ToString("MMM") + " " + day.Date.Day;
        //                lblBirthDay.ForeColor = System.Drawing.Color.Green;

        //                imgVideoPath.Width = 70;
        //                imgVideoPath.Height = 70;
        //            }
        //        }

        //    }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable getAllOffDays()
    {
        try
        {
            DateTime dFirstDate = FirstDayOfMonth(DateTime.Now);
            DateTime dLastDate = LastDayOfMonth(DateTime.Now);

            DataTable temptblOffDays = new DataTable();
            temptblOffDays.Columns.Add("iUserID");
            temptblOffDays.Columns.Add("dOffDate");

            tblManagerDayOff objManagerDayOff_Range = new tblManagerDayOff();
            objManagerDayOff_Range.getAllRangeDayOff(dFirstDate, dLastDate);

            foreach (DataRow dr in objManagerDayOff_Range.DefaultView.Table.Rows)
            {
                DateTime dStartDate = Convert.ToDateTime(dr["mstartdate"]);
                DateTime dEndDate = Convert.ToDateTime(dr["menddate"]);
                TimeSpan ts = dEndDate - dStartDate;
                int iTimeDifference = ts.Days;
                for (int i = 0; i < iTimeDifference; i++)
                {
                    DataRow tDR = temptblOffDays.NewRow();
                    tDR[0] = dr["fkuserid"];
                    tDR[1] = dStartDate.AddDays(i);
                    temptblOffDays.Rows.Add(tDR);

                }
            }

            tblManagerDayOff objManagerDayOff_Single = new tblManagerDayOff();
            objManagerDayOff_Single.getAllSingleDayOff(dFirstDate, dLastDate);
            foreach (DataRow drs in objManagerDayOff_Single.DefaultView.Table.Rows)
            {
                DataRow tDR = temptblOffDays.NewRow();
                tDR[0] = drs["fkuserid"];
                tDR[1] = drs["mSingleDate"];
                temptblOffDays.Rows.Add(tDR);
            }
            return temptblOffDays;
               
           
        }
        catch (Exception ex)
        {
            return null;
        }

    }

    public DateTime FirstDayOfMonth(DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1);
    }

    public DateTime LastDayOfMonth(DateTime dateTime)
    {
        DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
        return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
    }

}
