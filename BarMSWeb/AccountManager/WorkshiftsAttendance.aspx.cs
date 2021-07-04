using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using LC.Model.BMS.BLL;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class AccountManager_WorkshiftsAttendance : System.Web.UI.Page
{
    int UserID;
    tblUsers Users = new tblUsers();
    int CurrentWeek;
    static string WeekDate;
    int DepartmentID;
    int Year;
    //static int dayNum = 0;
    static DateTime DayNumDate;
    protected DateTime mySelectedDate;

    static DateTime SalaryDate;

    string WeekSTime;
    string WeekETime;
    int userIdWork;
    string WeekendSTime;
    string WeekendETime;

    protected void Page_Load(object sender, EventArgs e)
    {
        // string st = datepicker.Text;
        //string controlName = Request.Params.Get("__EVENTTARGET");
        //string arg = Request.Params.Get("__EVENTARGUMENT");
        //if(arg != null && arg !="")
        // mySelectedDate = Convert.ToDateTime(arg.ToString());
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];

            if (user.AccessLevel != 4)
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");

            }

            UserID = user.UserID;
            DepartmentID = user.DepartmentID;
            LoadDefaultTime();
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }
        if (!IsPostBack)
        {
            LoadGrid();
            CheckManagerSalaryPaid();
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "javaLoad", "javascript:abcd();", true);
       
        //LoadGrid(arg);
    }
    private void LoadDefaultTime()
    {
        tblDepartments departments = new tblDepartments();
        departments.LoadByPrimaryKey(DepartmentID);
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
    }
    private void LoadGrid()
    {
        try
        {
            DateTime mydate;

            //DateTime mydate = DateTime.Now;
            //DayNumDate = DateTime.Now;
            mydate = DateTime.Now;
            DayNumDate = DateTime.Now;
            SalaryDate = mydate;
            int selectedweek = commonMethods.GetWeeknumber(mydate);
            if (mydate.DayOfWeek == DayOfWeek.Sunday)
                selectedweek += 1;
            DateTime d_start = commonMethods.GetWeekStartDate(mydate.Year, selectedweek).AddDays(-1);
            tblUserWorkshifts workshift_start = new tblUserWorkshifts();
            workshift_start.LoadWorkShiftByStartDate(d_start);
            if (workshift_start.RowCount > 0)
            {
                chkSalaryPaid.Visible = true;
            }
            else
            {
                chkSalaryPaid.Visible = false;
            }




            lnkPrevious.Visible = true;
            lnkNext.Visible = true;
            DateTime startDate = commonMethods.GetWeekStartDate(mydate.Year, selectedweek).AddDays(-1);
            //startDate = startDate.AddDays(-1);
            int day = 0;
            for (int i = 0; i <= 6; i++)
            {
                if (i == 0)
                {
                    if (startDate.Date == mydate.Date)
                    {
                        day = i + 1;
                        break;
                    }
                }
                else
                {
                    if (startDate.Date == mydate.Date)
                    {
                        day = i + 1;
                        break;
                    }
                }
                startDate = startDate.AddDays(1);
            }
            hdnDay.Value = day.ToString();
            tblUserWorkshifts ecUserWorkshift = new tblUserWorkshifts();
            ecUserWorkshift.getECUserWorkshift(day, selectedweek, mydate.Year, DepartmentID);
            grdEcusers.DataSource = ecUserWorkshift.DefaultView;
            grdEcusers.DataBind();

            tblUserWorkshifts managerWorkshift = new tblUserWorkshifts();
            managerWorkshift.getManagerWorkshift(commonMethods.GetWeekStartDate(mydate.Year, selectedweek).AddDays(-1), mydate, day, mydate.Year, DepartmentID);
            grdManagers.DataSource = managerWorkshift.DefaultView;
            grdManagers.DataBind();

            tblUserWorkshifts specialUserWorkshift = new tblUserWorkshifts();
            specialUserWorkshift.getSpecialUserWorkshiftForAttendence(day, selectedweek, mydate.Year, DepartmentID);
            grdSpecialUsers.DataSource = specialUserWorkshift.DefaultView;
            grdSpecialUsers.DataBind();

            LoadWorkShiftForm(mydate, mydate.Year, selectedweek);
            Year = mydate.Year;
            LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
        }
        catch (Exception ex)
        {
            //lblErrorMsg.Text = ex.ToString();
        }
    }
    private void CheckManagerSalaryPaid()
    {
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = DateTime.Now;
        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;
        DateTime d_start = commonMethods.GetWeekStartDate(mydate.Year, selectedweek).AddDays(-1);
        tblUsers u = new tblUsers();
        u.GetSalariedUsersIDs(DepartmentID);
        for (int i = 0; i < u.RowCount; i++)
        {
            tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
            weeklyPayment.GetWeeklySalariedUser(u.PkUserID, d_start, d_start.AddDays(6));
            if (weeklyPayment.RowCount > 0)
            {
                if (weeklyPayment.PaidByAccountManager)
                    chkSalaryPaid.Checked = true;
                else
                    chkSalaryPaid.Checked = false;
            }
            u.MoveNext();
        }
    }

    protected void grdEcusers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSalary = e.Row.FindControl("lblSalary") as Label;
            Label lblEcuserName = e.Row.FindControl("lblEcuserName") as Label;

            DataRow drv = ((DataRowView)e.Row.DataItem).Row;

            tblUsers u = new tblUsers();
            u.LoadByPrimaryKey(Convert.ToInt32(drv["ecuserid"]));
            lblEcuserName.Text = u.SFirstName + " " + u.SLastName;


            tblUserContract objContract = new tblUserContract();
            objContract.GetAgreedContract(u.PkUserID);
            DateTime mydate;
            if (datepicker.Text != "")
                mydate = Convert.ToDateTime(datepicker.Text);
            else
                mydate = DateTime.Now;
            int selectedweek = commonMethods.GetWeeknumber(mydate);
            switch (objContract.FkSalaryTypeID)
            {
                case 1:
                    objContract.FlushData();

                    lblSalary.Text = commonMethods.ChangetToUK(objContract.GetUserSeasonSalary(commonMethods.GetWeekStartDate(mydate.Year, selectedweek), u.PkUserID, DepartmentID).ToString("N"));
                    break;
                case 2:

                    lblSalary.Text = commonMethods.ChangetToUK(objContract.StandardSalary.ToString("N"));
                    break;
                case 3:
                    lblSalary.Text = commonMethods.ChangetToUK(objContract.MinimumPerday.ToString("N"));
                    break;
            }


            // lblSalary.Text = commonMethods.ChangetToUK(Convert.ToDouble(lblSalary.Text).ToString("N")) + " €";
        }
    }
    protected void grdEcusers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName.ToLower())
            {
                case "del":
                    tblECUserAssignments ecassign = new tblECUserAssignments();
                    DateTime mydate;
                    if (datepicker.Text != "")
                        mydate = Convert.ToDateTime(datepicker.Text);
                    else
                        mydate = DateTime.Now;
                    ecassign.CheckECUsersSalery(mydate.Date, id);


                    if (ecassign.RowCount > 0)
                    {
                        for (int i = 0; i < ecassign.RowCount; i++)
                        {
                            tblECUserAssignments assign = new tblECUserAssignments();
                            assign.LoadByPrimaryKey(ecassign.PkECAssignedUserID);
                            assign.MarkAsDeleted();
                            assign.Save();
                            
                            
                            ecassign.MoveNext();
                        }
                        LoadAttendanceSheet();
                    }
                    break;
            }
        }
    }

    protected void grdManagers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSalary = e.Row.FindControl("lblSalary") as Label;
            lblSalary.Text = commonMethods.ChangetToUK(Convert.ToDouble(lblSalary.Text).ToString("N")) + " €";
        }
    }
    protected void grdManagers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName.ToLower())
            {
                case "del":
                    tblManagerDayOff dayoff = new tblManagerDayOff();
                    dayoff.AddNew();
                    dayoff.Fkuserid = id;
                    DateTime mydate;
                    if (datepicker.Text != "")
                        mydate = Convert.ToDateTime(datepicker.Text);
                    else
                        mydate = DateTime.Now;
                    dayoff.MSingleDate = mydate.Date;
                    dayoff.MLongReason = "Account manger set day off for you";
                    dayoff.DModifiedDate = DateTime.Now;
                    dayoff.DCreatedDate = DateTime.Now;
                    dayoff.Save();
                    LoadAttendanceSheet();
                    break;
            }
        }
    }

    protected void grdSpecialUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            switch (e.CommandName.ToLower())
            {
                case "del":
                    tblUserWorkshifts workshifts = new tblUserWorkshifts();
                    workshifts.LoadByPrimaryKey(id);
                    if (workshifts.RowCount > 0)
                    {
                        tblIncome income = new tblIncome();
                        income.GetIncomeByWorkshiftID(id);
                        if (income.RowCount > 0)
                        {
                            income.MarkAsDeleted();
                            income.Save();
                        }

                        workshifts.MarkAsDeleted();
                        workshifts.Save();

                        LoadAttendanceSheet();

                    }
                    break;
            }
        }
    }
    protected void grdSpecialUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSalary = e.Row.FindControl("lblSalary") as Label;
            lblSalary.Text = commonMethods.ChangetToUK(Convert.ToDouble(lblSalary.Text).ToString("N")) + " €";
        }
    }

    protected void GrdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                Label lblpkuserid = (Label)e.Row.FindControl("lblpkUserID");
                Label lblpkUserWorkshiftID = (Label)e.Row.FindControl("lblpkUserWorkshiftID");
                Label lblSpeciality = (Label)e.Row.FindControl("lblSpeciality");
                Label lblFullName = (Label)e.Row.FindControl("lblFullName");

                Label lblShift = (Label)e.Row.FindControl("lblShift");

                Label lblAgreedSalary = (Label)e.Row.FindControl("lblAgreedSalary");
                CheckBox chk = (CheckBox)e.Row.FindControl("OnTime");

                ImageButton imgBtnCheck = e.Row.FindControl("imgBtnCheck") as ImageButton;


                double WeeklySalary = 0.0;
                //string SalarySymbol = "€";
                DateTime dDatePickerDate;
                if (datepicker.Text != "")
                    dDatePickerDate = Convert.ToDateTime(datepicker.Text);
                else
                    dDatePickerDate = DateTime.Now;
                if (drv["fkSalaryTypeID"].ToString() != "")
                {
                    tblUserContract contract = new tblUserContract();
                    if (Convert.ToInt32(drv["fkSalaryTypeID"]) == 1)
                    {
                        contract.FlushData();
                        contract = new tblUserContract();
                        WeeklySalary = contract.GetUserSeasonSalary(dDatePickerDate, Convert.ToInt32(drv["pkUserID"]), DepartmentID);
                    }
                    else if (Convert.ToInt32(drv["fkSalaryTypeID"]) == 2)
                    {
                        WeeklySalary = Convert.ToDouble(drv["StandardSalary"]);
                    }
                    else if (Convert.ToInt32(drv["fkSalaryTypeID"]) == 3)
                    {
                        int day = 1;
                        int week = 1;
                        int year = 1990;
                        if (Convert.ToInt32(drv["idaynumber"].ToString()) == 1)
                        {
                            day = 7;
                            if (Convert.ToInt32(drv["iweeknumber"].ToString()) == 1)
                            {
                                year = Convert.ToInt32(drv["iYear"].ToString()) - 1;
                                week = commonMethods.GetWeekNumber_New(Convert.ToDateTime("12/31/" + year));
                            }
                            else
                            {
                                week = Convert.ToInt32(drv["iweeknumber"].ToString()) - 1;
                                year = Convert.ToInt32(drv["iYear"].ToString());
                            }

                        }
                        else
                        {
                            day = Convert.ToInt32(drv["idaynumber"].ToString());
                            if (Convert.ToInt32(drv["iweeknumber"].ToString()) == 1)
                            {
                                year = Convert.ToInt32(drv["iYear"].ToString()) - 1;
                                week = commonMethods.GetWeekNumber_New(Convert.ToDateTime("12/31/" + year));
                            }
                            else
                            {
                                week = Convert.ToInt32(drv["iweeknumber"].ToString());
                                year = Convert.ToInt32(drv["iYear"].ToString());
                            }
                        }


                        tblIncome income = new tblIncome();
                        income.getMinumumPerDaySalary(Convert.ToInt32(drv["FkUserID"].ToString()), Convert.ToInt32(drv["PkUserWorkshiftID"].ToString()));
                        if (income.RowCount > 0)
                        {

                            double incomeSum = income.UserTip + income.FIncome;
                            if (incomeSum > Convert.ToDouble(drv["PercentageOver"].ToString()))
                                lblAgreedSalary.Text = ((Convert.ToDouble(drv["fSalaryPercentage"].ToString()) * incomeSum) / 100).ToString("N") + "€";
                            else
                                lblAgreedSalary.Text = Convert.ToDouble(drv["MinimumPerday"].ToString()).ToString("N") + "€";
                        }
                        else
                        {

                            tblUserWorkshifts checkUserLastDayWorking = new tblUserWorkshifts();
                            checkUserLastDayWorking.getLastDayWorking(day, week, year, Convert.ToInt32(drv["fkuserid"].ToString()));
                            if (checkUserLastDayWorking.RowCount > 0)
                            {
                                income.FlushData();
                                income.getMinumumPerDaySalary(checkUserLastDayWorking.FkUserID, checkUserLastDayWorking.PkUserWorkshiftID);
                                if (income.RowCount > 0)
                                {
                                    double incomeSum = income.UserTip + income.FIncome;
                                    if (incomeSum > Convert.ToDouble(checkUserLastDayWorking.GetColumn("PercentageOver").ToString()))
                                        lblAgreedSalary.Text = ((Convert.ToDouble(checkUserLastDayWorking.GetColumn("fSalaryPercentage").ToString()) * incomeSum) / 100).ToString("N") + "€";
                                    else
                                        lblAgreedSalary.Text = Convert.ToDouble(drv["MinimumPerday"].ToString()).ToString("N") + "€";
                                }
                                else
                                    lblAgreedSalary.Text = Convert.ToDouble(drv["MinimumPerday"].ToString()).ToString("N") + "€";
                            }
                            else
                                lblAgreedSalary.Text = Convert.ToDouble(drv["MinimumPerday"].ToString()).ToString("N") + "€";
                        }
                    }
                    if (WeeklySalary != 0.0)
                    {
                        //lblAgreedSalary.Text = (Convert.ToDouble(WeeklySalary / Convert.ToDouble(7))).ToString("N") + "€";
                        lblAgreedSalary.Text = (Convert.ToDouble(WeeklySalary)).ToString("N") + "€";
                    }
                }
                else
                {
                    lblAgreedSalary.Text = WeeklySalary.ToString("N") + "€";
                }
                if (lblAgreedSalary.Text != "")
                    lblAgreedSalary.Text = commonMethods.ChangetToUK(lblAgreedSalary.Text.Replace("€", "")) + " €";

                TextBox txtNotes = (TextBox)e.Row.FindControl("txtNotes");

                TextBox txtPenalty = (TextBox)e.Row.FindControl("txtPenalty");
                TextBox txtBonus = (TextBox)e.Row.FindControl("txtBonus");
                tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();

                LoadUserOffday.PreviousWeekOffDaysByUSer(Convert.ToInt32(drv["iWeekNumber"]) - 1, DepartmentID, Convert.ToInt32(drv["iYear"]), Convert.ToInt32(drv["fkUserID"]));
                int WorkingDays = 0;
                if (LoadUserOffday.RowCount > 0)
                {
                    WorkingDays = Convert.ToInt32(LoadUserOffday.GetColumn("offdays"));
                }
                string Styleoffday = string.Empty;
                if ((7 - WorkingDays == 0) || (7 - WorkingDays > 2))
                {
                    Styleoffday = "style='color:RED'";
                }
                else
                {
                    Styleoffday = "style='color:Black'";
                }
                string dayOffBalloon = "Days off on previous week";
                string offDays = "<span " + Styleoffday + " onmouseover=\"javascript:OpenFeedbackWindow('" + dayOffBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'>(" + (7 - WorkingDays).ToString() + ")</span>";

                lblpkuserid.Text = drv["pkUserID"].ToString();
                lblpkUserWorkshiftID.Text = drv["pkUserWorkshiftID"].ToString();
                lblSpeciality.Text = drv["sSpeciality"].ToString();
                lblFullName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (name.Length > 10)
                {
                    lblFullName.Text = lblFullName.Text.Substring(0, 10) + "...";
                    lblFullName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblFullName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                HtmlGenericControl divOffDay = e.Row.FindControl("divOffDay") as HtmlGenericControl;
                divOffDay.InnerHtml = offDays;
                lblShift.Text = "[" + drv["sStartTime"].ToString() + " " + drv["sEndTime"].ToString() + "]";
                string weekPart = string.Empty;
                if (drv["iDayNumber"].ToString() == "6" || drv["iDayNumber"].ToString() == "7")
                {
                    weekPart = "1";
                    if (drv["sStartTime"].ToString() == WeekendSTime || drv["sEndTime"].ToString() == WeekendETime)
                        lblShift.ForeColor = Color.Black;
                    else
                        lblShift.ForeColor = Color.Blue;
                }
                else
                {
                    weekPart = "0";
                    if (drv["sStartTime"].ToString() == WeekSTime || drv["sEndTime"].ToString() == WeekETime)
                        lblShift.ForeColor = Color.Black;
                    else
                        lblShift.ForeColor = Color.Blue;
                }
                lblShift.Attributes.Add("onclick", "javascript:EditRecord(" + drv["pkUserWorkshiftID"] + ",'" + weekPart + "')");
                string attendance = string.Empty;
                attendance = "Click to edit attendance";
                lblShift.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + attendance + "')");
                lblShift.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                txtNotes.Text = drv["sNotes"].ToString();


                string notes = drv["sNotes"].ToString();
                if (notes.Length > 56)
                {
                    txtNotes.Text = txtNotes.Text.Substring(0, 56) + "...";
                    txtNotes.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + notes + "')");
                    txtNotes.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                txtPenalty.Text = drv["Penalty"].ToString();
                txtBonus.Text = drv["Bonus"].ToString();


                if (Convert.ToBoolean(drv["bOnTime"]))
                {
                    imgBtnCheck.ImageUrl = "~/Images/check_box_select.png";
                }


                if (Convert.ToInt32(drv["Penalty"]) > 0)
                {
                    txtPenalty.Text = "-" + commonMethods.ChangetToUK(drv["Penalty"].ToString());
                }
                else if (Convert.ToInt32(drv["Penalty"]) < 0)
                {
                    txtPenalty.Text = commonMethods.ChangetToUK(drv["Penalty"].ToString());
                }
                else if (Convert.ToInt32(drv["Penalty"]) == 0)
                {
                    txtPenalty.Text = "";
                }
                if (Convert.ToInt32(drv["Bonus"]) > 0)
                {
                    txtBonus.Text = "+" + commonMethods.ChangetToUK(drv["Bonus"].ToString());
                }
                else if (Convert.ToInt32(drv["Bonus"]) > 0)
                {
                    txtBonus.Text = commonMethods.ChangetToUK(drv["Bonus"].ToString());
                }
                else if (Convert.ToInt32(drv["Bonus"]) == 0)
                {
                    txtBonus.Text = "";
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void GrdUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GrdUsers.EditIndex = e.NewEditIndex;
            DateTime mydate = Convert.ToDateTime(datepicker.Text);
            int selectedweek = commonMethods.GetWeeknumber(mydate);
            GridViewRow row = GrdUsers.Rows[GrdUsers.EditIndex];
            CheckBox OnTime = ((CheckBox)row.FindControl("OnTime"));

            if (mydate.DayOfWeek == DayOfWeek.Sunday)
                selectedweek += 1;
            LoadWorkShiftForm(mydate, mydate.Year, selectedweek);

        }
        catch (Exception ex)
        {
            //lblErrorMsg.Text = ex.ToString();
        }
    }
    protected void GrdUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        tblUserWorkshifts UWShift = new tblUserWorkshifts();
        try
        {
            int UserShiftID = 0;
            string Notes;
            string Penalty;
            string Bonus;
            int index = GrdUsers.EditIndex;
            GridViewRow row = GrdUsers.Rows[index];
            UserShiftID = Convert.ToInt32(((Label)row.FindControl("lblpkUserWorkshiftID")).Text);
            Notes = ((TextBox)row.FindControl("txtNotes")).Text;
            Penalty = ((TextBox)row.FindControl("txtPenalty")).Text;
            Bonus = ((TextBox)row.FindControl("txtBonus")).Text;
            CheckBox OnTime = ((CheckBox)row.FindControl("OnTime"));

            UWShift.LoadByPrimaryKey(UserShiftID);
            UWShift.SNotes = Notes;
            if (Convert.ToBoolean(Bonus == ""))
            {
                UWShift.Bonus = 0;
            }
            else if (Convert.ToBoolean(Bonus == "+"))
            {
                UWShift.Bonus = 0;
            }
            else if (Bonus != "")
            {
                UWShift.Bonus = Convert.ToInt32(Bonus);
            }

            if (Convert.ToBoolean(Penalty == ""))
            {
                UWShift.Penalty = 0;
            }
            else if (Convert.ToBoolean(Penalty == "-"))
            {
                UWShift.Penalty = 0;
            }
            else if (Penalty != "")
            {
                UWShift.Penalty = Convert.ToInt32(Penalty);
            }

            //UWShift.s_Penalty = Penalty;
            //UWShift.s_Bonus = Bonus;

            if (OnTime.Checked)
            {
                UWShift.BOnTime = true;

            }
            else
            {
                UWShift.BOnTime = false;
            }
            UWShift.Save();
            GrdUsers.EditIndex = -1;
            DateTime mydate = Convert.ToDateTime(datepicker.Text);
            int selectedweek = commonMethods.GetWeeknumber(mydate);

            if (mydate.DayOfWeek == DayOfWeek.Sunday)
                selectedweek += 1;

            LoadWorkShiftForm(mydate, mydate.Year, selectedweek);
        }
        catch (Exception ex)
        {
            //lblErrorMsg.Text = ex.ToString();
        }
    }
    protected void GrdUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GrdUsers.EditIndex = -1;
            DateTime mydate = Convert.ToDateTime(datepicker.Text);
            int selectedweek = commonMethods.GetWeeknumber(mydate);


            LoadWorkShiftForm(mydate, mydate.Year, selectedweek);
        }
        catch (Exception ex)
        {
            // lblErrorMsg.Text = ex.ToString();
        }
    }
    //protected void btnGO_Click(object sender, ImageClickEventArgs e)
    //{
    //    DateTime mydate = Convert.ToDateTime(datepicker.Text);
    //    DayNumDate =  Convert.ToDateTime(datepicker.Text);
    //    int selectedweek = commonMethods.GetWeeknumber(mydate);
    //    LoadWorkShiftForm(mydate, mydate.Year, selectedweek);
    //    Year = mydate.Year;
    //    LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);


    //}

    private void LoadWorkShiftForm(DateTime mydate, int year, int SelectedWeek)
    {
        int day = 0;
        ShowWorkshift.Style.Add("display", "block");
        mydate = Convert.ToDateTime(mydate.ToShortDateString());
        DateTime startDate = commonMethods.GetWeekStartDate(year, SelectedWeek).AddDays(-1);
        //startDate = startDate.AddDays(-1);
        for (int i = 0; i <= 6; i++)
        {
            if (i == 0)
            {
                if (startDate == mydate)
                {
                    day = i + 1;
                    break;
                }
            }
            else
            {
                if (startDate == mydate)
                {
                    day = i + 1;
                    break;
                }
            }
            startDate = startDate.AddDays(1);
        }
        // lblSundayDate.Text = startDate.ToShortDateString();

        //if (mydate.DayOfWeek == DayOfWeek.Sunday)
        //    SelectedWeek += 1;
        lblSundayDate2.Value = mydate.Month + "/" + mydate.Day + "/" + mydate.Year;
        lblSundayDate1.Text = mydate.DayOfWeek.ToString() + " " + mydate.Day + "/" + mydate.Month + "/" + mydate.Year;
        WeekDate = "Week " + SelectedWeek.ToString() + " / " + mydate.Year + " ( " + startDate.AddDays(-1).DayOfWeek.ToString() + " " + startDate.AddDays(-1).Day + "/" + startDate.AddDays(-1).Month + "/" + startDate.Year + " till " + (startDate.AddDays(-1)).AddDays(6).DayOfWeek.ToString() + " " + (startDate.AddDays(-1)).AddDays(6).Day + "/" + (startDate.AddDays(-1)).AddDays(6).Month + "/" + (startDate.AddDays(-1)).AddDays(6).Year + " )";
        lblOffDayPeople.Text = "People with DAY OFF on " + mydate.DayOfWeek.ToString() + " " + mydate.Day.ToString() + "/" + mydate.Month.ToString();
        lblWeek.Text = mydate.DayOfWeek.ToString() + " " + mydate.Day.ToString() + "/" + mydate.Month.ToString();

        tblSpeciality speciality = new tblSpeciality();
        speciality.LoadWorkShiftSpecialities(DepartmentID);

        tblUserWorkshifts uws = new tblUserWorkshifts();
        tblUserWorkshifts LoadUserOffday = new tblUserWorkshifts();
        DataTable dt = uws.LoadCurrentWorkshiftForAccountManager(day, SelectedWeek, DepartmentID, year);
        //DataTable dt = uws.LoadCurrentWorkshiftForAccountManager(day, SelectedWeek, DepartmentID, year);

        GrdUsers.DataSource = uws.DefaultView;
        GrdUsers.DataBind();

        if (uws.RowCount > 0)
        {
            imgBtnSaveBottom.Visible = true;
            imgBtnSaveTop.Visible = true;
            chkSalaryPaid.Visible = true;
        }
        else
        {
            imgBtnSaveBottom.Visible = false;
            imgBtnSaveTop.Visible = false;
            chkSalaryPaid.Visible = false;

        }
        LoadOffDayGrids(day);

    }
    private void LoadOffDayGrids(int day)
    {
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = DayNumDate;
        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        tblUserWorkshifts uws = new tblUserWorkshifts();
        uws.LoadOffdayUsers(DepartmentID, selectedweek, mydate.Year, day);
        grdSunday.DataSource = uws.DefaultView;
        grdSunday.DataBind();
    }

    private void LoadPreviousOffDays(int weeknumber, int year, int departmentid)
    {

        tblUserWorkshifts uws = new tblUserWorkshifts();
        uws.PreviousWeekOffDays(weeknumber, departmentid, year);
        int count = uws.RowCount;
        //DLPreviousOffdays.DataSource = uws.DefaultView;
        //DLPreviousOffdays.DataBind();
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
                    mydate = DayNumDate;
                int selectedweek = commonMethods.GetWeeknumber(mydate);
                if (mydate.DayOfWeek == DayOfWeek.Sunday)
                    selectedweek += 1;
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                Label lnkUser = (Label)e.Row.FindControl("lnkUser");
                //lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkuserid"].ToString();
                lnkUser.Text = drv["FullName"].ToString();

                string name = drv["FullName"].ToString();
                if (name.Length > 20)
                {
                    lnkUser.Text = lnkUser.Text.Substring(0, 20) + "...";
                    lnkUser.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lnkUser.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                //lblemail.Text = drv["sEmail"].ToString();
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
                string dayOffBalloon = "Days off on previous week";
                string strOffday = "<span " + Styleoffday + " onmouseover=\"javascript:OpenFeedbackWindow('" + dayOffBalloon + "')\" onmouseout='javascript:CloseFeedBackWindow()'>(" + (7 - WorkingDays).ToString() + ")</span>";
                //string strOffday = "<span " + Styleoffday + ">(" + (7 - WorkingDays).ToString() + ")</span>";
                HtmlGenericControl mydiv = (HtmlGenericControl)e.Row.FindControl("mydiv");
                mydiv.InnerHtml = strOffday;

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {

        DayNumDate = DayNumDate.AddDays(1);
        int selectedweek = commonMethods.GetWeeknumber(DayNumDate);

        lnkPrevious.Visible = true;
        lnkNext.Visible = true;
        LoadWorkShiftForm(DayNumDate, DayNumDate.Year, selectedweek);
        Year = DayNumDate.Year;
        LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
        //LoadOffDayGrids();
    }

    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        DayNumDate = DayNumDate.AddDays(-1);
        int selectedweek = commonMethods.GetWeeknumber(DayNumDate);

        lnkPrevious.Visible = true;
        lnkNext.Visible = true;
        LoadWorkShiftForm(DayNumDate, DayNumDate.Year, selectedweek);
        Year = DayNumDate.Year;
        LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
        //LoadOffDayGrids();
    }

    protected void btnGO_Click(object sender, EventArgs e)
    {
        LoadAttendanceSheet();
        upMain.Update();
    }
    private void LoadAttendanceSheet()
    {
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = DateTime.Now;
        //DateTime mydate = Convert.ToDateTime(datepicker.Text);
        SalaryDate = mydate;
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;

        txtCheck.Text = "2";
        txtDay.Text = days.ToString();

        DayNumDate = mydate;

        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;

        DateTime d_start = commonMethods.GetWeekStartDate(mydate.Year, selectedweek).AddDays(-1);
        tblUserWorkshifts workshift_start = new tblUserWorkshifts();
        workshift_start.LoadWorkShiftByStartDate(d_start);
        if (workshift_start.RowCount > 0)
            chkSalaryPaid.Visible = true;
        else
            chkSalaryPaid.Visible = false;
        CheckManagerSalaryPaid();


        //if (mydate.DayOfWeek == DayOfWeek.Sunday)
        //    selectedweek += 1;

        DateTime startDate = commonMethods.GetWeekStartDate(mydate.Year, selectedweek).AddDays(-1);
        //startDate = startDate.AddDays(-1);
        int day = 0;
        for (int i = 0; i <= 6; i++)
        {
            if (i == 0)
            {
                if (startDate == mydate)
                {
                    day = i + 1;
                    break;
                }
            }
            else
            {
                if (startDate == mydate)
                {
                    day = i + 1;
                    break;
                }
            }
            startDate = startDate.AddDays(1);
        }
        hdnDay.Value = day.ToString();
        tblUserWorkshifts ecUserWorkshift = new tblUserWorkshifts();
        ecUserWorkshift.getECUserWorkshift(day - 1, selectedweek, mydate.Year, DepartmentID);

        tblECUserAssignments ecuserassign = new tblECUserAssignments();
        ecuserassign.CheckECUsersSalery(mydate);

        grdEcusers.DataSource = ecuserassign.DefaultView;
        grdEcusers.DataBind();

        tblUserWorkshifts managerWorkshift = new tblUserWorkshifts();
        managerWorkshift.getManagerWorkshift(commonMethods.GetWeekStartDate(mydate.Year, selectedweek).AddDays(-1), mydate, day, mydate.Year, DepartmentID);
        grdManagers.DataSource = managerWorkshift.DefaultView;
        grdManagers.DataBind();


        tblUserWorkshifts specialUserWorkshift = new tblUserWorkshifts();
        specialUserWorkshift.getSpecialUserWorkshiftForAttendence(day, selectedweek, mydate.Year, DepartmentID);
        grdSpecialUsers.DataSource = specialUserWorkshift.DefaultView;
        grdSpecialUsers.DataBind();

        LoadWorkShiftForm(mydate, mydate.Year, selectedweek);
        Year = mydate.Year;
        LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
    }
    protected void GrdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            ImageButton btn = (ImageButton)e.CommandSource;
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {

                //case "save":
                //    try
                //    {
                //        TextBox txtNotes = btn.FindControl("txtNotes") as TextBox;
                //        TextBox txtPenalty = btn.FindControl("txtPenalty") as TextBox;
                //        TextBox txtBonus = btn.FindControl("txtBonus") as TextBox;
                //        tblUserWorkshifts workshift = new tblUserWorkshifts();
                //        workshift.LoadByPrimaryKey(id);

                //        if (workshift.RowCount > 0)
                //        {
                //            int penalty = 0;
                //            int bonus = 0;

                //            if (txtPenalty.Text != "")
                //            {
                //                txtPenalty.Text = txtPenalty.Text.TrimStart('-');
                //                penalty = Convert.ToInt32("-" + txtPenalty.Text);
                //                txtPenalty.Text = "-" + txtPenalty.Text.TrimStart('-');
                //            }
                //            else
                //                txtPenalty.Text = "";
                //            if (txtBonus.Text != "")
                //            {
                //                bonus = Convert.ToInt32(txtBonus.Text);
                //                txtBonus.Text = "+" + txtBonus.Text.TrimStart('+');
                //            }
                //            else
                //                txtBonus.Text = "";
                //            workshift.Penalty = penalty;
                //            workshift.Bonus = bonus;
                //            workshift.SNotes = txtNotes.Text;
                //            workshift.DModifiedDate = DateTime.Now;
                //            workshift.Save();
                //        }
                //    }
                //    catch (Exception ex)
                //    {

                //    }

                //    break;
                case "chk":
                    try
                    {
                        ImageButton imgBtnCheck = btn.FindControl("imgBtnCheck") as ImageButton;
                        tblUserWorkshifts workshiftCheckbox = new tblUserWorkshifts();
                        workshiftCheckbox.LoadByPrimaryKey(id);
                        if (workshiftCheckbox.RowCount > 0)
                        {
                            if (workshiftCheckbox.BOnTime)
                            {
                                imgBtnCheck.ImageUrl = "~/Images/check_box.png";
                                workshiftCheckbox.BOnTime = false;

                            }
                            else if (!workshiftCheckbox.BOnTime)
                            {
                                imgBtnCheck.ImageUrl = "~/Images/check_box_select.png";
                                workshiftCheckbox.BOnTime = true;
                            }
                            workshiftCheckbox.DModifiedDate = DateTime.Now;
                            workshiftCheckbox.Save();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    break;
                case "del":
                    try
                    {
                        tblUserWorkshifts workshifts = new tblUserWorkshifts();
                        workshifts.LoadByPrimaryKey(id);
                        if (workshifts.RowCount > 0)
                        {
                            tblIncome income = new tblIncome();
                            income.GetIncomeByWorkshiftID(id);
                            if (income.RowCount > 0)
                            {
                                income.MarkAsDeleted();
                                income.Save();
                            }

                            workshifts.MarkAsDeleted();
                            workshifts.Save();

                            //int selectedweek = commonMethods.GetWeeknumber(DayNumDate);

                            //lnkPrevious.Visible = true;
                            //lnkNext.Visible = true;
                            //LoadWorkShiftForm(DayNumDate, DayNumDate.Year, selectedweek);
                            //Year = DayNumDate.Year;
                            //LoadPreviousOffDays(selectedweek - 1, Year, DepartmentID);
                            LoadAttendanceSheet();

                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    break;
            }
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        tblUserWorkshifts workshift = new tblUserWorkshifts();
        workshift.LoadByPrimaryKey(Convert.ToInt32(txtddl.Text));
        if (workshift.RowCount > 0)
        {

            string[] start = workshift.SStartTime.Split(':');
            if (Convert.ToInt32(txtsh.Text) > Convert.ToInt32(start[0]))
            {
                int late = 0;
                late = Convert.ToInt32(txtsh.Text) - Convert.ToInt32(start[0]);
                if (Convert.ToInt32(txtsm.Text) >= Convert.ToInt32(start[1]))
                {
                    workshift.BLateHours = late;
                    workshift.BLate = true;
                    workshift.BOnTime = false;
                }
                else
                {
                    late = late - 1;
                    if (late != 0 && late > 0)
                    {
                        workshift.BLateHours = late;
                        workshift.BLate = true;
                        workshift.BOnTime = false;
                    }
                }
            }

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
            LoadWorkShiftForm(mydate, mydate.Year, selectedweek);
        }
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        tblUserWorkshifts uws = new tblUserWorkshifts();
        uws.LoadByPrimaryKey(Convert.ToInt32(txtdel.Text));
        if (uws.RowCount > 0)
        {
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
        LoadWorkShiftForm(mydate, mydate.Year, selectedweek);
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
            LoadWorkShiftForm(mydate, mydate.Year, selectedweek);
        }
    }
    protected void imgBtnSaveTop_Click(object sender, ImageClickEventArgs e)
    {

        if (GrdUsers.Rows.Count > 0)
        {
            for (int i = 0; i < GrdUsers.Rows.Count; i++)
            {
                HiddenField hid = GrdUsers.Rows[i].FindControl("hidWorkshipid") as HiddenField;
                tblUserWorkshifts workshift = new tblUserWorkshifts();
                workshift.LoadByPrimaryKey(Convert.ToInt32(hid.Value));
                if (workshift.RowCount > 0)
                {
                    TextBox txtNotes = GrdUsers.Rows[i].FindControl("txtNotes") as TextBox;
                    TextBox txtPenalty = GrdUsers.Rows[i].FindControl("txtPenalty") as TextBox;
                    TextBox txtBonus = GrdUsers.Rows[i].FindControl("txtBonus") as TextBox;
                    CheckBox chkOnTime = GrdUsers.Rows[i].FindControl("chkOnTime") as CheckBox;

                    double penalty = 0;
                    double bonus = 0;

                    if (txtPenalty.Text != "")
                    {
                        if (txtPenalty.Text.StartsWith("-") || txtPenalty.Text.StartsWith("€"))
                        { }
                        else
                        {
                            txtPenalty.Text = txtPenalty.Text.Replace("€", "").Replace(" ", "");
                            txtPenalty.Text = txtPenalty.Text.TrimStart('-');
                            penalty = Convert.ToDouble("-" + commonMethods.ChangeToUS(txtPenalty.Text).ToString("N"));
                            txtPenalty.Text = "-" + commonMethods.ChangetToUK(txtPenalty.Text.TrimStart('-'));
                            workshift.Penalty = penalty;
                        }
                    }
                    else
                        txtPenalty.Text = "";
                    if (txtBonus.Text != "")
                    {
                        if (txtBonus.Text.StartsWith("+") || txtBonus.Text.StartsWith("€"))
                        { }
                        else
                        {
                            txtBonus.Text = txtBonus.Text.Replace("€", "").Replace(" ", "");
                            bonus = commonMethods.ChangeToUS(txtBonus.Text);
                            txtBonus.Text = "+" + commonMethods.ChangetToUK(txtBonus.Text.TrimStart('+'));
                            workshift.Bonus = bonus;
                        }
                    }
                    else
                        txtBonus.Text = "";

                    workshift.SNotes = txtNotes.Text;
                    //if (chkOnTime.Checked)
                    //    workshift.BLate = false;
                    //else if (!chkOnTime.Checked)
                    //    workshift.BLate = true;
                    workshift.BOnTime = chkOnTime.Checked;
                    workshift.DModifiedDate = DateTime.Now;
                    workshift.Save();
                }
            }
            LoadAttendanceSheet();

        }
    }
    protected void chkSalaryPaid_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSalaryPaid.Checked)
        {
            double GrandTotal = 0.0;
            tblUsers u = new tblUsers();
            u.GetSalariedUsersIDs(DepartmentID);
            if (u.RowCount > 0)
            {


                int selectedweek = commonMethods.GetWeekNumber_New(SalaryDate);
                DateTime d_start = commonMethods.GetWeekStartDate(SalaryDate.Year, selectedweek).AddDays(-1);

                tblUserWorkshifts uws = new tblUserWorkshifts();


                for (int i = 0; i < u.RowCount; i++)
                {
                    uws.FlushData();
                    DataTable dt = uws.LoadCurrentUserSalary(selectedweek, d_start, DepartmentID, SalaryDate.Year, u.PkUserID);
                    tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
                    weeklyPayment.GetWeeklySalariedUser(u.PkUserID, d_start, d_start.AddDays(6));
                    if (weeklyPayment.RowCount > 0)
                    {
                        weeklyPayment.EmailDate = DateTime.Now;
                        weeklyPayment.DModifiedDate = DateTime.Now;
                        weeklyPayment.PaidByAccountManager = true;
                    }
                    else
                    {
                        if (uws.RowCount > 0)
                        {
                            weeklyPayment.AddNew();
                            weeklyPayment.FkUserID = u.PkUserID;
                            weeklyPayment.IWeekNumber = selectedweek;
                            weeklyPayment.DWeekStartDate = d_start;
                            weeklyPayment.DWeekEndDate = d_start.AddDays(6);
                            weeklyPayment.DCreatedDate = DateTime.Now;
                            weeklyPayment.DModifiedDate = DateTime.Now;
                            weeklyPayment.EmailDate = DateTime.Now;
                            weeklyPayment.PaidByAccountManager = true;
                            weeklyPayment.ReceivedByUser = false;
                            weeklyPayment.BIsActive = true;
                        }
                    }







                    DataRow[] dr;
                    double WeeklySalary = 0;
                    double PerDaySalary = 0;

                    double netSalary = 0;
                    double netTips = 0;
                    double netBonus = 0;
                    double netAdvances = 0;
                    double netPanelty = 0;

                    for (int Counter = 1; Counter <= 6; Counter++)
                    {
                        if (Counter == 1)
                        {
                            double totalSalary = 0;

                            for (int salary = 1; salary <= 8; salary++)
                            {
                                if (salary == 8)
                                {
                                    netSalary = totalSalary;
                                }
                                else
                                {
                                    dr = dt.Select("iDayNumber = " + salary + " AND bLate = 0");
                                    if (dr.Length > 0)
                                    {
                                        if (dr[0]["fkSalaryTypeID"].ToString() != "")
                                        {
                                            tblUserContract contract = new tblUserContract();
                                            if (Convert.ToInt32(dr[0]["fkSalaryTypeID"]) == 1)
                                            {
                                                contract.FlushData();
                                                contract = new tblUserContract();
                                                if (datepicker.Text != "")
                                                    WeeklySalary = contract.GetUserSeasonSalary(Convert.ToDateTime(datepicker.Text), UserID, DepartmentID);
                                                else if (Request.QueryString.Count > 0)
                                                {
                                                    WeeklySalary = contract.GetUserSeasonSalary(Convert.ToDateTime(commonMethods.GetWeekStartDate(Year, selectedweek).ToShortDateString()), UserID, DepartmentID);
                                                }
                                                else
                                                    WeeklySalary = contract.GetUserSeasonSalary(Convert.ToDateTime(DateTime.Now.ToShortDateString()), UserID, DepartmentID);
                                            }
                                            else if (Convert.ToInt32(dr[0]["fkSalaryTypeID"]) == 2)
                                            {
                                                WeeklySalary = Convert.ToDouble(dr[0]["StandardSalary"]);
                                            }
                                            else if (Convert.ToInt32(dr[0]["fkSalaryTypeID"]) == 3)
                                            {
                                                int day = 1;
                                                int week = 1;
                                                int year = 1990;
                                                if (Convert.ToInt32(dr[0]["idaynumber"].ToString()) == 1)
                                                {
                                                    day = 7;
                                                    if (Convert.ToInt32(dr[0]["iweeknumber"].ToString()) == 1)
                                                    {
                                                        year = Convert.ToInt32(dr[0]["iYear"].ToString()) - 1;
                                                        week = commonMethods.GetWeekNumber_New(Convert.ToDateTime("12/31/" + year));
                                                    }
                                                    else
                                                    {
                                                        week = Convert.ToInt32(dr[0]["iweeknumber"].ToString()) - 1;
                                                        year = Convert.ToInt32(dr[0]["iYear"].ToString());
                                                    }

                                                }
                                                else
                                                {
                                                    day = Convert.ToInt32(dr[0]["idaynumber"].ToString());
                                                    if (Convert.ToInt32(dr[0]["iweeknumber"].ToString()) == 1)
                                                    {
                                                        year = Convert.ToInt32(dr[0]["iYear"].ToString()) - 1;
                                                        week = commonMethods.GetWeekNumber_New(Convert.ToDateTime("12/31/" + year));
                                                    }
                                                    else
                                                    {
                                                        week = Convert.ToInt32(dr[0]["iweeknumber"].ToString());
                                                        year = Convert.ToInt32(dr[0]["iYear"].ToString());
                                                    }
                                                }


                                                tblIncome income = new tblIncome();
                                                income.getMinumumPerDaySalary(Convert.ToInt32(dr[0]["FkUserID"].ToString()), Convert.ToInt32(dr[0]["PkUserWorkshiftID"].ToString()));
                                                if (income.RowCount > 0)
                                                {

                                                    double incomeSum = income.UserTip + income.FIncome;
                                                    if (incomeSum > Convert.ToDouble(dr[0]["PercentageOver"].ToString()))
                                                        PerDaySalary = (Convert.ToDouble(dr[0]["fSalaryPercentage"].ToString()) * incomeSum) / 100;
                                                    else
                                                        PerDaySalary = Convert.ToDouble(dr[0]["MinimumPerday"].ToString());
                                                }
                                                else
                                                {

                                                    tblUserWorkshifts checkUserLastDayWorking = new tblUserWorkshifts();
                                                    checkUserLastDayWorking.getLastDayWorking(day, week, year, Convert.ToInt32(dr[0]["fkuserid"].ToString()));
                                                    if (checkUserLastDayWorking.RowCount > 0)
                                                    {
                                                        income.FlushData();
                                                        income.getMinumumPerDaySalary(checkUserLastDayWorking.FkUserID, checkUserLastDayWorking.PkUserWorkshiftID);
                                                        if (income.RowCount > 0)
                                                        {
                                                            double incomeSum = income.UserTip + income.FIncome;
                                                            if (incomeSum > Convert.ToDouble(checkUserLastDayWorking.GetColumn("PercentageOver").ToString()))
                                                                PerDaySalary = (Convert.ToDouble(checkUserLastDayWorking.GetColumn("fSalaryPercentage").ToString()) * incomeSum) / 100;
                                                            else
                                                                PerDaySalary = Convert.ToDouble(dr[0]["MinimumPerday"].ToString());
                                                        }
                                                        else
                                                            PerDaySalary = Convert.ToDouble(dr[0]["MinimumPerday"].ToString());
                                                    }
                                                    else
                                                        PerDaySalary = Convert.ToDouble(dr[0]["MinimumPerday"].ToString());
                                                }
                                            }
                                            if (WeeklySalary != 0.0)
                                            {
                                                //PerDaySalary = Convert.ToDouble(Convert.ToDouble(WeeklySalary / Convert.ToDouble(7)).ToString("N"));
                                                PerDaySalary = Convert.ToDouble(Convert.ToDouble(WeeklySalary).ToString("N"));
                                            }
                                        }
                                        else
                                        {
                                            PerDaySalary = Convert.ToDouble(WeeklySalary.ToString("N"));
                                        }
                                        totalSalary += PerDaySalary;
                                    }
                                }
                            }
                        }
                        else if (Counter == 2)
                        {
                            double totalTips = 0;

                            for (int tips = 1; tips <= 8; tips++)
                            {
                                if (tips == 8)
                                {
                                    netTips = totalTips;
                                }
                                else
                                {
                                    dr = dt.Select("iDayNumber = " + tips + "AND bOnTime = 1 AND bLate = 0");
                                    if (dr.Length > 0)
                                    {
                                        if (dr[0]["userTip"].ToString() != "")
                                        {
                                            totalTips += Convert.ToDouble(dr[0]["userTip"]);
                                        }
                                    }
                                }
                            }
                        }
                        else if (Counter == 3)
                        {

                            double totalBonus = 0;
                            for (int bonus = 1; bonus <= 8; bonus++)
                            {

                                if (bonus == 8)
                                {
                                    netBonus = totalBonus;

                                }
                                else
                                {
                                    dr = dt.Select("iDayNumber = " + bonus + "AND bOnTime = 1 AND bLate = 0");
                                    if (dr.Length > 0)
                                    {


                                        if (dr[0]["Bonus"].ToString() != "")
                                        {
                                            totalBonus += Convert.ToDouble(dr[0]["Bonus"]);
                                        }
                                    }
                                }
                            }
                        }
                        else if (Counter == 4)
                        {
                            double totalAdvance = 0;
                            for (int advance = 1; advance <= 8; advance++)
                            {
                                if (advance == 8)
                                {
                                    netAdvances = totalAdvance;
                                }
                                else
                                {
                                    dr = dt.Select("iDayNumber = " + advance);
                                    if (dr.Length > 0)
                                    {
                                        if (dr[0]["uAdvance"].ToString() != "")
                                        {
                                            totalAdvance += Convert.ToDouble(dr[0]["uAdvance"]);
                                        }
                                    }
                                }
                            }
                        }
                        else if (Counter == 5)
                        {

                            double totalPenalty = 0;
                            string strSpan = string.Empty;
                            for (int penalty = 1; penalty <= 8; penalty++)
                            {
                                if (penalty == 8)
                                {
                                    if (totalPenalty != 0.0)
                                        netPanelty = Convert.ToDouble(totalPenalty.ToString().Substring(1));
                                }
                                else
                                {
                                    dr = dt.Select("iDayNumber = " + penalty);
                                    if (dr.Length > 0)
                                    {
                                        if (dr[0]["Penalty"].ToString() != "")
                                        {
                                            totalPenalty += Convert.ToDouble(dr[0]["Penalty"]);
                                        }
                                    }
                                }
                            }
                        }
                        else if (Counter == 6)
                        {
                            GrandTotal = netSalary + netTips + netBonus - (netAdvances + netPanelty);
                        }
                    }
                    if (uws.RowCount > 0)
                    {
                        weeklyPayment.Salary = Convert.ToDouble(GrandTotal.ToString("#0.00"));
                        weeklyPayment.Save();
                    }

                    try
                    {
                        string fromAddress = string.Empty;
                        tblUsers uProfile = new tblUsers();
                        uProfile.GetUserProfile(UserID);
                        fromAddress = uProfile.GetColumn("sEmail").ToString();

                        uProfile.FlushData();
                        uProfile.GetUserProfile(u.PkUserID);

                        Emailing email = new Emailing();
                        email.P_ToAddress = uProfile.GetColumn("sEmail").ToString();
                        email.P_FromAddress = fromAddress;
                        email.P_Email_Subject = "Payment";
                        //email.P_Message_Body = " Dear " + uProfile.GetColumn("FullName").ToString() + ".<br/>";
                        //email.P_Message_Body += " The Department’s Account Manager has marked you as “paid off” for the " + WeekDate + ".<br/>";
                        //email.P_Message_Body += " Please check your “My Payments” page and sign up for your payment!<br/>";

                        email.P_Message_Body = "<div><table style='border:1px solid #e8e8e8' align='center'><tr><td align='left' style='font-weight:bold;height:30px;background-color:Red;vertical-align:middle; padding-left:15px;'>" + WeekDate + "</td></tr> ";
                        email.P_Message_Body += "<tr><td align='left' style='background-color:White;font-family:Verdana, Geneva, sans-serif;height:200px;' valign='top'>";
                        email.P_Message_Body += "<div style='padding-top: 10px;'><span style='font-weight: bold; margin-top: 1px;'> Dear " + u.GetColumn("SUsername").ToString() + " " + uProfile.GetColumn("FullName").ToString() + "</span><br /><br />";
                        email.P_Message_Body += "<span>The Department’s Account Manager has marked you as “paid off” for the given Week.<br/>Please check your “My Payments” page and sign up for your payment!</span>";
                        email.P_Message_Body += "</div></td></tr></table></div>";
                        email.Send_Email();
                    }
                    catch (Exception ex)
                    { }
                    u.MoveNext();
                }
            }
        }
        else if (!chkSalaryPaid.Checked)
        {
            tblUsers u = new tblUsers();
            u.GetSalariedUsersIDs(DepartmentID);
            if (u.RowCount > 0)
            {
                int selectedweek = commonMethods.GetWeeknumber(SalaryDate);
                DateTime d_start = commonMethods.GetWeekStartDate(SalaryDate.Year, selectedweek).AddDays(-1);
                for (int i = 0; i < u.RowCount; i++)
                {
                    tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
                    weeklyPayment.GetWeeklySalariedUser(u.PkUserID, d_start, d_start.AddDays(6));
                    if (weeklyPayment.RowCount > 0)
                    {
                        weeklyPayment.PaidByAccountManager = false;
                        weeklyPayment.DModifiedDate = DateTime.Now;
                        weeklyPayment.Save();
                    }
                    u.MoveNext();
                }
            }
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string mystr = txtsh.Text;
        lblDayDate.Visible = false; 
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        int selectedweek = commonMethods.GetWeeknumber(mydate);
        if (mydate.DayOfWeek == DayOfWeek.Sunday)
            selectedweek += 1;
        DateTime startDate = commonMethods.GetWeekStartDate(mydate.Year, selectedweek).AddDays(-1);
         tblUserWorkshifts workshift1 = new tblUserWorkshifts();
         workshift1.LoadUserAlreadyExsit(Convert.ToInt16(txtddl.Text), selectedweek, Convert.ToInt32(mydate.Year), Convert.ToInt32(hdnDay.Value));
         if (workshift1.RowCount > 0)
        {
            lblDayDate.Visible = true;
            lblDayDate.Text = "User already exsit";
        }
        else
        {
       tblUserWorkshifts workshift = new tblUserWorkshifts();
        workshift.AddNew();
        workshift.FkUserID = Convert.ToInt16(IdUser.Text);// txtddl.Text); //Convert.ToInt32(ddlusers.SelectedValue);
        workshift.s_FkSpecialityID = txtddl.Text;
        workshift.IWeekNumber = selectedweek;
        workshift.IYear = mydate.Year;
        workshift.DWeekStartDate = startDate;
        workshift.DWeekEndDate = startDate.AddDays(6);
        workshift.IDayNumber = Convert.ToInt32(hdnDay.Value);
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
        LoadAttendanceSheet();
        upMain.Update();
        ModalPopupExtender1.Hide();
        }
        // LoadWorkShiftForm(mydate.Year, selectedweek);
    }
    
    protected void grdSunday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "Edit")
        {
            
            LinkButton btn = (LinkButton)e.CommandSource;
            Label UserName = btn.FindControl("lnkUser") as Label;
            txtUserName.Text = UserName.Text;
            tblUserSpeciality speciality=new tblUserSpeciality();
            WorkassinUserID.Value = e.CommandArgument.ToString();
            speciality.LoadUserSpecialityActive_New(Convert.ToInt16(e.CommandArgument));
            if (speciality.RowCount > 0)
            {

                commonMethods.FillDropDownList(dlUserSpitilaty, speciality.DefaultView, "sSpeciality", "pkSpecialityID");
                dlUserSpitilaty.Items.Insert(0, new ListItem("--Select Speciality--", "0"));

            }
            else
            {
                
                dlUserSpitilaty.Items.Insert(0, new ListItem("--Select Speciality--", "0"));
            }
            LoadAttendanceSheet();
            upMain.Update();
            upnlCalender.Update();
            lblDayDate.Visible = false; 
           // TextBox3.Text;
           // string strScript1 = " hs.addEventListener(window, 'load', function() {document.getElementById('autoloadNew').onclick();});";
           // ScriptManager.RegisterStartupScript(this, this.GetType(), "select1", strScript1, true);
            //LoadGrid();
           // CheckManagerSalaryPaid();
            ModalPopupExtender1.Show();
        }
    }
    protected void grdSunday_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}
