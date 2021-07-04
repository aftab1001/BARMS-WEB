using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class DepartmentAdmin_ManagerDayOff : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    int WeekNumber;
    int Day;
    int Year;
    static int iManagerID;
    static DateTime dDayOff = DateTime.Now;
    static string selectedDate = "";
    DataTable dtOffDaysRecord = new DataTable();
    bool isFirstTime = true;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];

            if (user.AccessLevel != 5)
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
        dtOffDaysRecord = getAllOffDays();
        if (!IsPostBack)
        {
            LoadColorCodes();
            getDaysOff();
            dtOffDaysRecord = getAllOffDays();
            if (dDayOff != null)
            {
                txtDayOff.Text = dDayOff.Date.ToString("MM/dd/yyyy");
            }


        }
        if (isFirstTime == true)
        {
            trManagerName.Style.Add("display", "none");
            trManagerName_line.Style.Add("display", "none");
            isFirstTime = false;
        }

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "img", "$(function(){ShowDataPicker();});", true);
    }
    protected void btnGO_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdnUserID.Value))
        {
            iManagerID = Convert.ToInt32(hdnUserID.Value);
        }
        if (!string.IsNullOrEmpty(txtDayOff.Text))
        {
            dDayOff = Convert.ToDateTime(txtDayOff.Text);
        }
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
        //Response.Redirect("../Managers/ManagerDayOff.aspx?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year + "&day=" + days);
        hidParam.Value = "?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year;
        getDaysOff();
    }

    #region GetName
    private void GetManagerName(int iUserID)
    {
        tblUsers u = new tblUsers();
        u.LoadByPrimaryKey(iUserID);
        lblManagerName.Text = string.Empty;
        if (u.RowCount > 0)
        {
            lblManagerName.Text = u.SFirstName + " " + u.SLastName;
        }
    }

    #endregion

    #region Get Day Off

    private void getDaysOff()
    {
        DateTime myDate;
        if (datepicker.Text != "")
            myDate = Convert.ToDateTime(datepicker.Text);
        else if (selectedDate != "")
            myDate = Convert.ToDateTime(selectedDate);
        else
            myDate = DateTime.Now;
        tblManagerDayOff dayOff = new tblManagerDayOff();

        trManagerName.Style.Add("display", "table-row");
        trManagerName_line.Style.Add("display", "table-row");
        // trManagerName_line.Style.Add("display", "none");
        if (iManagerID != 0)
        {
            GetManagerName(iManagerID);
            dayOff.getSingleDayOff(iManagerID, myDate.Date);
            //trManagerName.Style.Add("display", "table-row");
            //trManagerName_line.Style.Add("display", "table-row");
        }
        else
        {
            GetManagerName(0);
        }

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
            if (iManagerID != 0)
            {
                dayOff.getRangeDayOff(iManagerID, myDate.Date);
            }
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
                imgTrash.Visible = true;
            }
        }
        else
        {

            imgBtnMessage.Visible = true;
            imgTrash.Visible = true;
        }
        if (iManagerID != 0)
        {
            imgBtnProgram.Visible = false;
        }
        if (iManagerID == UserID)
        {
            imgBtnMessage.Visible = false;
            imgTrash.Visible = true;
            imgBtnProgram.Visible = true;
        }
        else
        {
            imgBtnMessage.Visible = true;
            imgTrash.Visible = true;
            //imgBtnProgram.Visible = false;
        }
        iManagerID = 0;

    }


    #endregion

    #region Send Mesage for Day Off

    protected void imgBtnMessage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iManagerID = 0;
            if (ViewState["ManagerID"] != null)
            {
                iManagerID = Convert.ToInt32(ViewState["ManagerID"]);

                string[] start_D = txtDayOff.Text.Split('/');
                string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

                //tblManagerDayOff dayoff = new tblManagerDayOff();
                //if (iManagerID == 0)
                //{

                //}

                //int departmentAdminid = 0;

                //u.getDepartmentAdmin(DepartmentID);
                //if (u.RowCount > 0)
                //{
                //    departmentAdminid = u.PkUserID;
                //}
                tblUsers u = new tblUsers();
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
                if (iManagerID != 0)
                {
                    userIn.FkToUserID = iManagerID;
                    userIn.SSubject = "Manager Day Off";
                    userIn.SMessage = "This is a system notification to let you know that the manager “ " + name + " ” is having a programmed day off for date " + start;
                    userIn.DReceivedDate = DateTime.Now;
                    userIn.BIsread = false;
                    userIn.Save();
                    userIn.FlushData();
                }

                tblUserSentBox userOut = new tblUserSentBox();

                //For Current Manager
                userOut.AddNew();
                userOut.FkFromUserID = UserID;
                if (iManagerID != 0)
                {
                    userOut.FkToUserID = iManagerID;
                    userOut.SSubject = "Manager Day Off";
                    userOut.SMessage = "This is a system notification to let you know that the manager “ " + name + " ” is having a programmed day off for date " + start;
                    userOut.DSentDate = DateTime.Now;
                    userOut.Save();
                    userOut.FlushData();
                }
                #endregion


                lblRecordMessage.Text = "Day Off Message is sent to manager Successfully!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "del", "$(function(){RecordSaved();});", true);

                // ViewState["mdoid"] = dayoff.PkManagerDayOffID;

                imgBtnMessage.Visible = true;
                imgTrash.Visible = true;
                //Response.Redirect("managerdayoff.aspx");
                //Response.Redirect(Request.FilePath);
                dtOffDaysRecord = getAllOffDays();
            }
        }
        catch (Exception ex)
        { }
    }
    #endregion

    protected void imgTrash_Click(object sender, ImageClickEventArgs e)
    {
        //if (ViewState["mdoid"] != null)
        //{

        string[] start_D = txtDayOff.Text.Split('/');
        string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
        tblManagerDayOff dayoff = new tblManagerDayOff();
        dayoff.getDateAvailable(Convert.ToDateTime(start));
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
            userIn.SMessage = "This is a system notification to let you know that the manager “ " + name + " ” has canceled his scheduled day of for the date (" + start + ")";
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
            userOut.SMessage = "This is a system notification to let you know that the manager “ " + name + " ” has canceled his scheduled day of for the date (" + start + ")";
            userOut.DSentDate = DateTime.Now;
            userOut.Save();
            userOut.FlushData();

            #endregion
        }
        //ViewState["mdoid"] = null;

        imgBtnMessage.Visible = true;
        imgTrash.Visible = true;
        dtOffDaysRecord = getAllOffDays();
        //}
    }

    protected void imgBtnYes_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtStartDate.Text))
        {
            string[] start_D1;
            string start1;
            start_D1 = txtStartDate.Text.Split('/');
            start1 = start_D1[1] + "/" + start_D1[0] + "/" + start_D1[2];
            DateTime dTempDate = Convert.ToDateTime(start1);
            if (dTempDate.Date < DateTime.Now)
            {
                lblRecordMessage.Text = "Start date should be greator or equal to current date";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "del", "$(function(){RecordSaved();});", true);
                return;
            }
        }
        ModalPopupExtender2.Hide();

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
        if (iManagerID != 0)
        {
            userIn.FkToUserID = iManagerID;
        }
        else
        {
            userIn.FkToUserID = departmentAdminid;
        }
        userIn.SSubject = "Manager Day Off";
        userIn.SMessage = "Warning! Manager " + name + " has requested to get a day off for " + (ViewState["dateIssue"].ToString().Split(','))[0].ToString() + " on the same day as manager " + (ViewState["dateIssue"].ToString().Split(','))[1].ToString() + "!";
        userIn.DReceivedDate = DateTime.Now;
        userIn.BIsread = false;
        userIn.Save();
        userIn.FlushData();


        tblUserSentBox userOut = new tblUserSentBox();

        //For Current Manager
        userOut.AddNew();
        userOut.FkFromUserID = UserID;
        if (iManagerID != 0)
        {
            userOut.FkToUserID = iManagerID;
        }
        else
        {
            userOut.FkToUserID = departmentAdminid;
        }
        userOut.SSubject = "Manager Day Off";
        userOut.SMessage = "Warning! Manager " + name + " has requested to get a day off for " + (ViewState["dateIssue"].ToString().Split(','))[0].ToString() + " on the same day as manager " + (ViewState["dateIssue"].ToString().Split(','))[1].ToString() + "!";
        userOut.DSentDate = DateTime.Now;
        userOut.Save();
        userOut.FlushData();

        #endregion

        string[] start_D;
        string start;
        string end;

        start_D = txtStartDate.Text.Split('/');
        start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

        start_D = txtTillDate.Text.Split('/');
        end = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

        TimeSpan ts = Convert.ToDateTime(end) - Convert.ToDateTime(start);
        tblManagerDayOff dayoff = new tblManagerDayOff();

        if (ts.Days >= 0)
        {
            for (int i = 0; i < ts.Days; i++)
            {
                dayoff.FlushData();
                dayoff.getDateAvailable(Convert.ToDateTime(start).AddDays(i));
                if (dayoff.RowCount == 0)
                {
                    dayoff.FlushData();
                    dayoff.AddNew();
                    dayoff.Fkuserid = UserID;
                    dayoff.MSingleDate = Convert.ToDateTime(start).AddDays(i);
                    dayoff.MLongReason = txtReasonLong.Text;
                    dayoff.DModifiedDate = DateTime.Now.Date;
                    dayoff.DCreatedDate = DateTime.Now.Date;
                    dayoff.Save();
                }
            }



            #region Internal Message

            //tblUserInBox userIn = new tblUserInBox();
            userIn.FlushData();
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

            //tblUserSentBox userOut = new tblUserSentBox();

            userOut.FlushData();
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


        }
    }

    protected void imgBtnProgram_Click(object sender, ImageClickEventArgs e)
    {

        string[] start_D;
        string start = string.Empty;
        string end = string.Empty;

        if (txtTillDate.Text == "")
        {
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                string[] start_D1;
                string start1;
                start_D1 = txtStartDate.Text.Split('/');
                start1 = start_D1[1] + "/" + start_D1[0] + "/" + start_D1[2];
                DateTime dTempDate = Convert.ToDateTime(start1);
                if (dTempDate.Date < DateTime.Now)
                {
                    lblRecordMessage.Text = "Date should be greater or equal to Today's date";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "del", "$(function(){RecordSaved();});", true);
                    return;
                }
            }
            start_D = txtStartDate.Text.Split('/');
            start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

            start_D = txtStartDate.Text.Split('/');
            end = start_D[1] + "/" + start_D[0] + "/" + start_D[2];




        }
        else if (txtTillDate.Text != "")
        {
            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                string[] start_D1;
                string start1;
                start_D1 = txtStartDate.Text.Split('/');
                start1 = start_D1[1] + "/" + start_D1[0] + "/" + start_D1[2];

                start_D1 = txtTillDate.Text.Split('/');
                end = start_D1[1] + "/" + start_D1[0] + "/" + start_D1[2];
                DateTime dTempDate = Convert.ToDateTime(start1);
                DateTime dTempDateEnd = Convert.ToDateTime(end);
                if (dTempDate.Date < DateTime.Now.Date)
                {
                    lblRecordMessage.Text = "Start date should be greater or equal to current date";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "del", "$(function(){RecordSaved();});", true);
                    return;
                }
                else if (dTempDateEnd.Date < dTempDate.Date)
                {
                    lblRecordMessage.Text = "End date should be greater than start date";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "del", "$(function(){RecordSaved();});", true);
                    return;
                }
            }

            start_D = txtStartDate.Text.Split('/');
            start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

            start_D = txtTillDate.Text.Split('/');
            end = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

        }
        TimeSpan ts = Convert.ToDateTime(end) - Convert.ToDateTime(start);
        tblManagerDayOff dayoff = new tblManagerDayOff();
        if (ts.Days >= 0)
        {
            for (int i = 0; i < ts.Days; i++)
            {
                dayoff.FlushData();
                dayoff.getDateAvailable(Convert.ToDateTime(start).AddDays(i));
                if (dayoff.RowCount > 0)
                {
                    tblUsers u = new tblUsers();
                    u.LoadByPrimaryKey(dayoff.Fkuserid);
                    lblOffDayMessage.Text = "On " + Convert.ToDateTime(start).AddDays(i).ToString("dddd") + " " + Convert.ToDateTime(start).AddDays(i).ToString("dd/MM") + " the manager " + u.SFirstName + " " + u.SLastName + " has scheduled already a day off! <br/> Are you sure that you want to have a day off on the same day?";
                    ViewState["dateIssue"] = Convert.ToDateTime(start).AddDays(i).ToString("dddd") + " " + Convert.ToDateTime(start).AddDays(i).ToString("dd/MM") + "," + u.SFirstName + " " + u.SLastName;
                    ModalPopupExtender2.Show();
                    break;
                }
            }
        }

        //TimeSpan ts = Convert.ToDateTime(end) - Convert.ToDateTime(start);
        //tblManagerDayOff dayoff = new tblManagerDayOff();

        if (ts.Days >= 0)
        {
            for (int i = 0; i <= ts.Days; i++)
            {
                dayoff.FlushData();
                dayoff.getDateAvailable(Convert.ToDateTime(start).AddDays(i));
                if (dayoff.RowCount == 0)
                {
                    dayoff.FlushData();
                    dayoff.AddNew();
                    dayoff.Fkuserid = UserID;
                    dayoff.MSingleDate = Convert.ToDateTime(start).AddDays(i);
                    dayoff.MLongReason = txtReasonLong.Text;
                    dayoff.DModifiedDate = DateTime.Now.Date;
                    dayoff.DCreatedDate = DateTime.Now.Date;
                    dayoff.Save();
                }
            }

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
            userIn.FlushData();
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

            userOut.FlushData();
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


        }







        //while (true)
        //{
        //    tblManagerDayOff dayOff = new tblManagerDayOff();
        //    dayOff.getSingleDayOff(UserID, dStart.Date);
        //    if (dayOff.RowCount > 0)
        //    {
        //        dayOff.MarkAsDeleted();
        //        dayOff.Save();
        //    }
        //    dStart = dStart.AddDays(1);
        //    if (dStart <= Convert.ToDateTime(end))
        //    {

        //    }
        //    else
        //    {
        //        break;
        //    }
        //}
        //tblManagerDayOff dayoff = new tblManagerDayOff();
        //dayoff.AddNew();
        //dayoff.Fkuserid = UserID;
        //dayoff.MStartDate = Convert.ToDateTime(start);
        //dayoff.MEndDate = Convert.ToDateTime(end);
        //dayoff.MLongReason = txtReasonLong.Text;
        //dayoff.DModifiedDate = DateTime.Now.Date;
        //dayoff.DCreatedDate = DateTime.Now.Date;
        //dayoff.Save();


        //int departmentAdminid = 0;
        //tblUsers u = new tblUsers();
        //u.getDepartmentAdmin(DepartmentID);
        //if (u.RowCount > 0)
        //{
        //    departmentAdminid = u.PkUserID;
        //}

        //string name = string.Empty;
        //u.FlushData();
        //u.LoadByPrimaryKey(UserID);
        //if (u.RowCount > 0)
        //{
        //    name = u.SFirstName + " " + u.SLastName;
        //}

        //#region Internal Message

        //tblUserInBox userIn = new tblUserInBox();
        //// For Department Admin
        //userIn.AddNew();
        //userIn.FkFromUserID = UserID;

        //userIn.FkToUserID = departmentAdminid;
        //userIn.SSubject = "Manager Day Off";
        //userIn.SMessage = "This is a system notification to let you know that the manager “ " + name + " ” is having a programmed day off from date (" + start + ") to  (" + end + ")";
        //userIn.DReceivedDate = DateTime.Now;
        //userIn.BIsread = false;
        //userIn.Save();
        //userIn.FlushData();

        //tblUserSentBox userOut = new tblUserSentBox();

        ////For Current Manager
        //userOut.AddNew();
        //userOut.FkFromUserID = UserID;
        //userOut.FkToUserID = departmentAdminid;
        //userOut.SSubject = "Manager Day Off";
        //userOut.SMessage = "This is a system notification to let you know that the manager “ " + name + " ” is having a programmed day off from date (" + start + ") to  (" + end + ")";
        //userOut.DSentDate = DateTime.Now;
        //userOut.Save();
        //userOut.FlushData();

        //#endregion


        lblRecordMessage.Text = "Day Off Message is sent to department admin Successfully!";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "del", "$(function(){RecordSaved();});", true);
        dtOffDaysRecord = getAllOffDays();
    }

    #region Calendar

    protected void ManagerCalendar_DayRender(object sender, DayRenderEventArgs e)
    {
        try
        {
            CalendarDay day = (CalendarDay)e.Day;
            TableCell cell = (TableCell)e.Cell;
            string strJavascript = string.Empty; // "javascript:datePic('" + day.Date + "','" + 0 + "');";
            //if (day.Date >= DateTime.Today)
            //{
            //    cell.Attributes.Add("onclick", strJavascript);
            //}
            foreach (DataRow dr in dtOffDaysRecord.Rows)
            {
                DateTime dCalendarDate = day.Date;
                DateTime tempDate = Convert.ToDateTime(dr["dOffDate"]);
                if (dCalendarDate == tempDate)
                {
                    int iManagerID = Convert.ToInt32(dr["iUserID"]);
                    ViewState["ManagerID"] = iManagerID;
                    tblColorCodes objColorCode = new tblColorCodes();
                    objColorCode.GetColor(iManagerID);
                    string colorCode = objColorCode.ColorCode;
                    cell.BackColor = System.Drawing.Color.FromName(colorCode);
                    strJavascript = "javascript:datePic('" + day.Date + "','" + iManagerID + "');";
                    if (day.Date >= DateTime.Today)
                    {
                        cell.Attributes.Add("onclick", strJavascript);
                    }
                }

            }
            if (day.Date < DateTime.Now.Date)
            {
                cell.BackColor = System.Drawing.Color.FromArgb(150, 150, 150);
                cell.ToolTip = "You can’t select a date in the past!";
            }
            cell.Style.Add("cursor", "pointer");


        }
        catch (Exception ex)
        {

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
            objManagerDayOff_Range.getALLoffDays();

            //foreach (DataRow dr in objManagerDayOff_Range.DefaultView.Table.Rows)
            //{
            //    DateTime dStartDate = Convert.ToDateTime(dr["mstartdate"]);
            //    DateTime dEndDate = Convert.ToDateTime(dr["menddate"]);
            //    TimeSpan ts = dEndDate - dStartDate;
            //    int iTimeDifference = ts.Days;
            //    for (int i = 0; i <= iTimeDifference; i++)
            //    {
            //        DataRow tDR = temptblOffDays.NewRow();
            //        tDR[0] = dr["fkuserid"];
            //        tDR[1] = dStartDate.AddDays(i);
            //        temptblOffDays.Rows.Add(tDR);

            //    }
            //}

            //tblManagerDayOff objManagerDayOff_Single = new tblManagerDayOff();
            //objManagerDayOff_Single.getAllSingleDayOff(dFirstDate, dLastDate);
            foreach (DataRow drs in objManagerDayOff_Range.DefaultView.Table.Rows)
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

    private void LoadColorCodes()
    {
        tblColorCodes objColorCodes = new tblColorCodes();
        objColorCodes.LoadAll();
        dlColorCodes.DataSource = objColorCodes.DefaultView;
        dlColorCodes.DataBind();
    }

    protected void dlColorCodes_RowDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)e.Item.DataItem;
            Label lblManagername = (Label)e.Item.FindControl("lblManagername");
            HtmlContainerControl divColorCode = (HtmlContainerControl)e.Item.FindControl("divColorCode");
            tblUsers objusers = new tblUsers();
            objusers.LoadByPrimaryKey(Convert.ToInt32(drv["fkuserid"]));
            lblManagername.Text = objusers.s_SFirstName + " " + objusers.s_SLastName;
            divColorCode.Style.Add("background-color", drv["colorcode"].ToString());
        }
    }


    #endregion

}
