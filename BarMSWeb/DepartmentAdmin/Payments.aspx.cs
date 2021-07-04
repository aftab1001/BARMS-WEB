using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LC.Model.BMS.BLL;

public partial class DepartmentAdmin_Payments : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    int WeekNumber;
    int Day;
    int Year;
    static int selectedWeek = 0;
    static bool checkSalaryPaid = false;
    static bool checkStatus = false;
    static double total = 0.0;
    static double totalPayment = 0.0;
    static DateTime SelectedWeekStart;
    static DateTime SelectedWeekEnd;



    #region Popup Parameters

    static int p_userid = 0;
    static int p_weeknumber = 0;
    static DateTime p_weelStart;
    static DateTime p_weekEnd;
    static string p_name = "";

    #endregion

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
            Day = DateTime.Now.Year;
        }
        if (!IsPostBack)
        {
            LoadDefaultDates();
            GetReference();
            BindPaperSize();
        }
        lblWeek.Text = "Week " + WeekNumber.ToString() + " / " + Year.ToString() + " ( Sunday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1).ToString("dd/MM/yyyy") + " till Saturday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5).ToString("dd/MM/yyyy") + " )";
        //GetTotalUserForSelectedWeek(WeekNumber, commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1), commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5));
        if (Request.QueryString.Count > 0)
        {
            GetSalaries(WeekNumber, commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1), commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5));
        }


    }

    private void BindPaperSize()
    {
        ddlPaperSize.Items.Clear();
        ddlPaperSize.Items.Insert(0, new ListItem("Select Paper Size", "0"));
        ddlPaperSize.Items.Insert(1, new ListItem("A4", "1"));



    }

    private void GetTotalUserForSelectedWeek(int weeknumber, DateTime weekStart, DateTime weekEnd)
    {
        tblUsers u = new tblUsers();
        u.GetAllSalaries(weeknumber, DepartmentID, weekStart, weekEnd);

        DataTable dt = new DataTable();
        dt = u.DefaultView.ToTable();

        u.FlushData();
        u.LoadManager_and_AccountManager(DepartmentID);

        foreach (DataRow drow in u.DefaultView.ToTable().Rows)
        {
            DataRow dr = dt.NewRow();
            dr["FullName"] = drow["FullName"];// u.s_SFirstName + " " + u.s_SLastName;
            dr["pkuserid"] = drow["pkuserid"];
            dr["uAdvance"] = 0;
            dr["dWeekStartDate"] = weekStart.ToString();
            dr["dWeekEndDate"] = weekEnd.ToString();
            tblUserContract objContract = new tblUserContract();
            objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));

            switch (objContract.FkSalaryTypeID)
            {
                case 1:
                    objContract.FlushData();
                    dr["Sunday"] = objContract.GetUserSeasonSalary(weekStart, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                    break;
                case 2:

                    dr["Sunday"] = objContract.s_StandardSalary;
                    break;
                case 3:
                    dr["Sunday"] = objContract.s_MinimumPerday;
                    break;
            }


            switch (objContract.FkSalaryTypeID)
            {
                case 1:
                    objContract.FlushData();
                    dr["Monday"] = objContract.GetUserSeasonSalary(weekStart, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                    break;
                case 2:
                    dr["Monday"] = objContract.s_StandardSalary;
                    break;
                case 3:
                    dr["Monday"] = objContract.s_MinimumPerday;
                    break;
            }

            switch (objContract.FkSalaryTypeID)
            {
                case 1:
                    objContract.FlushData();
                    dr["Tuesday"] = objContract.GetUserSeasonSalary(weekStart, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                    break;
                case 2:
                    dr["Tuesday"] = objContract.s_StandardSalary;
                    break;
                case 3:
                    dr["Tuesday"] = objContract.s_MinimumPerday;
                    break;
            }


            switch (objContract.FkSalaryTypeID)
            {
                case 1:
                    objContract.FlushData();
                    dr["Wednesday"] = objContract.GetUserSeasonSalary(weekStart, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                    break;
                case 2:
                    dr["Wednesday"] = objContract.s_StandardSalary;
                    break;
                case 3:
                    dr["Wednesday"] = objContract.s_MinimumPerday;
                    break;
            }


            switch (objContract.FkSalaryTypeID)
            {
                case 1:
                    objContract.FlushData();
                    dr["Thursday"] = objContract.GetUserSeasonSalary(weekStart, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                    break;
                case 2:
                    dr["Thursday"] = objContract.s_StandardSalary;
                    break;
                case 3:
                    dr["Thursday"] = objContract.s_MinimumPerday;
                    break;
            }


            switch (objContract.FkSalaryTypeID)
            {
                case 1:
                    objContract.FlushData();
                    dr["Friday"] = objContract.GetUserSeasonSalary(weekStart, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                    break;
                case 2:
                    dr["Friday"] = objContract.s_StandardSalary;
                    break;
                case 3:
                    dr["Friday"] = objContract.s_MinimumPerday;
                    break;
            }

            switch (objContract.FkSalaryTypeID)
            {
                case 1:
                    objContract.FlushData();
                    dr["Saturday"] = objContract.GetUserSeasonSalary(weekStart, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                    break;
                case 2:
                    dr["Saturday"] = objContract.s_StandardSalary;
                    break;
                case 3:
                    dr["Saturday"] = objContract.s_MinimumPerday;
                    break;
            }


            dr["SalaryStatus"] = "True";
            dr["permission"] = "True";

            tblManagerDayOff objManagerDayoff = new tblManagerDayOff();
            objManagerDayoff.getSingleDayOffforSalery(Convert.ToInt32(drow["pkuserid"]), weekStart, weekEnd);
            if (objManagerDayoff.RowCount > 0)
            {
                for (int i = 0; i < objManagerDayoff.RowCount; i++)
                {
                    string WeekDay = Convert.ToDateTime(objManagerDayoff.s_MSingleDate).DayOfWeek.ToString();
                    switch (WeekDay)
                    {
                        case "Sunday":
                            dr["Sunday"] = 0;
                            break;
                        case "Monday":
                            dr["Monday"] = 0;
                            break;
                        case "Tuesday":
                            dr["Tuesday"] = 0;
                            break;
                        case "Wednesday":
                            dr["Wednesday"] = 0;
                            break;
                        case "Thursday":
                            dr["Thursday"] = 0;
                            break;
                        case "Friday":
                            dr["Friday"] = 0;
                            break;
                        case "Saturday":
                            dr["Saturday"] = 0;
                            break;
                    }
                    objManagerDayoff.MoveNext();
                }

            }
            dt.Rows.Add(dr);

        }

        //*******************************


        u.FlushData();
        u.LoadECUser(DepartmentID);

        foreach (DataRow drow in u.DefaultView.ToTable().Rows)
        {
            tblECUserAssignments ua = new tblECUserAssignments();
            ua.CheckECUsersSaleryForWeek(Convert.ToInt32(drow["pkuserid"]), weekStart, weekEnd);
            if (ua.RowCount > 0)
            {

                DataRow dr = dt.NewRow();
                dr["FullName"] = drow["FullName"];
                dr["pkuserid"] = drow["pkuserid"];
                dr["uAdvance"] = 0;
                dr["dWeekStartDate"] = weekStart.ToString();
                dr["dWeekEndDate"] = weekEnd.ToString();
                tblUserContract objContract = new tblUserContract();


                dr["Sunday"] = 0;


                dr["Monday"] = 0;
                dr["Tuesday"] = 0;
                dr["Wednesday"] = 0;
                dr["Thursday"] = 0;
                dr["Friday"] = 0;
                dr["Saturday"] = 0;

                dr["SalaryStatus"] = "True";
                dr["permission"] = "True";


                for (int j = 0; j < 7; j++)
                {
                    ua.FlushData();
                    ua.CheckECUsersSalery(weekStart.AddDays(j), Convert.ToInt32(drow["pkuserid"]));
                    if (ua.RowCount > 0)
                    {
                        string WeekDay = weekStart.AddDays(j).DayOfWeek.ToString();
                        switch (WeekDay)
                        {
                            case "Sunday":
                                objContract.FlushData();
                                objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                switch (objContract.FkSalaryTypeID)
                                {
                                    case 1:
                                        objContract.FlushData();
                                        dr["Sunday"] = objContract.GetUserSeasonSalary(weekStart, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                        break;
                                    case 2:
                                        dr["Sunday"] = objContract.s_StandardSalary;
                                        break;
                                    case 3:
                                        dr["Sunday"] = objContract.s_MinimumPerday;
                                        break;
                                }

                                break;
                            case "Monday":
                                objContract.FlushData();
                                objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));

                                switch (objContract.FkSalaryTypeID)
                                {
                                    case 1:
                                        objContract.FlushData();
                                        dr["Monday"] = objContract.GetUserSeasonSalary(weekStart.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                        break;
                                    case 2:
                                        dr["Monday"] = objContract.s_StandardSalary;
                                        break;
                                    case 3:
                                        dr["Monday"] = objContract.s_MinimumPerday;
                                        break;
                                }
                                break;
                            case "Tuesday":
                                objContract.FlushData();
                                objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                switch (objContract.FkSalaryTypeID)
                                {
                                    case 1:
                                        objContract.FlushData();
                                        dr["Tuesday"] = objContract.GetUserSeasonSalary(weekStart.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                        break;
                                    case 2:
                                        dr["Tuesday"] = objContract.s_StandardSalary;
                                        break;
                                    case 3:
                                        dr["Tuesday"] = objContract.s_MinimumPerday;
                                        break;
                                }
                                break;
                            case "Wednesday":
                                objContract.FlushData();
                                objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                switch (objContract.FkSalaryTypeID)
                                {
                                    case 1:
                                        objContract.FlushData();
                                        dr["Wednesday"] = objContract.GetUserSeasonSalary(weekStart.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                        break;
                                    case 2:
                                        dr["Wednesday"] = objContract.s_StandardSalary;
                                        break;
                                    case 3:
                                        dr["Wednesday"] = objContract.s_MinimumPerday;
                                        break;
                                }
                                break;
                            case "Thursday":
                                objContract.FlushData();
                                objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                switch (objContract.FkSalaryTypeID)
                                {
                                    case 1:
                                        objContract.FlushData();
                                        dr["Thursday"] = objContract.GetUserSeasonSalary(weekStart.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                        break;
                                    case 2:
                                        dr["Thursday"] = objContract.s_StandardSalary;
                                        break;
                                    case 3:
                                        dr["Thursday"] = objContract.s_MinimumPerday;
                                        break;
                                }
                                break;
                            case "Friday":
                                objContract.FlushData();
                                objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                switch (objContract.FkSalaryTypeID)
                                {
                                    case 1:
                                        objContract.FlushData();
                                        dr["Friday"] = objContract.GetUserSeasonSalary(weekStart.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                        break;
                                    case 2:
                                        dr["Friday"] = objContract.s_StandardSalary;
                                        break;
                                    case 3:
                                        dr["Friday"] = objContract.s_MinimumPerday;
                                        break;
                                }
                                break;
                            case "Saturday":
                                objContract.FlushData();
                                objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                switch (objContract.FkSalaryTypeID)
                                {
                                    case 1:
                                        objContract.FlushData();
                                        dr["Saturday"] = objContract.GetUserSeasonSalary(weekStart.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                        break;
                                    case 2:
                                        dr["Saturday"] = objContract.s_StandardSalary;
                                        break;
                                    case 3:
                                        dr["Saturday"] = objContract.s_MinimumPerday;
                                        break;
                                }
                                break;
                        }
                    }





                }
                dt.Rows.Add(dr);
            }
        }
        ////*********************************










        if (dt.Rows.Count > 0)
        {


            ddlSelectReceipt.Items.Clear();
            commonMethods.FillDropDownList(ddlSelectReceipt, dt.DefaultView, "FullName", "pkuserid");
            ddlSelectReceipt.Items.Insert(0, new ListItem("Select Receipt", "0"));
            int pages = 0;
            int receipts = 0;
            if (dt.Rows.Count > 4)
            {
                if (u.RowCount % 4 == 0)
                    pages = dt.Rows.Count / 4;
                else
                    pages = (dt.Rows.Count / 4) + 1;
                receipts = 4;
            }
            else
            {
                pages = 1;
                receipts = dt.Rows.Count;
            }
            if (pages == 1)
                lblSelected.Text = "A4 Size is selected: " + pages + " page in total, " + receipts + " receipts per page";
            else if (pages > 0)
                lblSelected.Text = "A4 Size is selected: " + pages + " pages in total, " + receipts + " receipts per page";
            trPrint.Visible = true;
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "wmm", "$(function(){alert('Sorry No Salaries!');});", true);
            tblSalary.Visible = false;
            trPrint.Visible = false;
            lblSelected.Text = "No any Receipt found.";
        }
        upnlPrint.Update();

    }

    //private void GetTotalUserForSelectedWeek(int weeknumber, DateTime weekStart, DateTime weekEnd)
    //{
    //    tblUsers u = new tblUsers();
    //    u.GetAllSalaries(weeknumber, DepartmentID, weekStart, weekEnd);
    //    if (u.RowCount > 0)
    //    {


    //        ddlSelectReceipt.Items.Clear();
    //        commonMethods.FillDropDownList(ddlSelectReceipt, u.DefaultView, "FullName", "pkuserid");
    //        ddlSelectReceipt.Items.Insert(0, new ListItem("Select Receipt", "0"));
    //        int pages = 0;
    //        int receipts = 0;
    //        if (u.RowCount > 4)
    //        {
    //            if (u.RowCount % 4 == 0)
    //                pages = u.RowCount / 4;
    //            else
    //                pages = (u.RowCount / 4) + 1;
    //            receipts = 4;
    //        }
    //        else
    //        {
    //            pages = 1;
    //            receipts = u.RowCount;
    //        }
    //        if (pages == 1)
    //            lblSelected.Text = "A4 Size is selected: " + pages + " page in total, " + receipts + " receipts per page";
    //        else if (pages > 0)
    //            lblSelected.Text = "A4 Size is selected: " + pages + " pages in total, " + receipts + " receipts per page";
    //        trPrint.Visible = true;
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "wmm", "$(function(){alert('Sorry No Salaries!');});", true);
    //        tblSalary.Visible = false;
    //        trPrint.Visible = false;
    //        lblSelected.Text = "No any Receipt found.";
    //    }
    //    upnlPrint.Update();

    //}

    private void LoadDefaultDates()
    {
        try
        {
            txtFromDate.Text = "01/01/" + DateTime.Now.Year;
            txtTillDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            lblDatePeriod.Text = "From " + txtFromDate.Text + " till " + txtTillDate.Text;
        }
        catch (Exception ex)
        { }
    }
    protected void btnGO_Click(object sender, EventArgs e)
    {
        WeekNumber = commonMethods.GetWeekNumber_New(Convert.ToDateTime(datepicker.Text));
        Year = Convert.ToDateTime(datepicker.Text).Year;
        DateTime mydate = Convert.ToDateTime(datepicker.Text);
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;
        Day = days;
        txtCheck.Text = "2";
        txtMyDay.Text = days.ToString();
        Response.Redirect("../DepartmentAdmin/Payments.aspx?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year + "&day=" + days);
        hidParam.Value = "?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year;

    }

    #region Reference

    private void GetReference()
    {
        string[] start_D = txtFromDate.Text.Split('/'); // start Date
        string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

        string[] end_D = txtTillDate.Text.Split('/'); // end date
        string end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];

        lblDatePeriod.Text = "From " + txtFromDate.Text + " till " + txtTillDate.Text;

        tblIncome income = new tblIncome();
        income.GetWeekly_Income_Salaries_with_Status(Convert.ToDateTime(start), Convert.ToDateTime(end));
        grdWeeklyReference.DataSource = income.DefaultView;
        grdWeeklyReference.DataBind();
        upnlReference.Update();
    }

    protected void imgBtnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            string[] start_D = txtFromDate.Text.Split('/'); // start Date
            string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

            string[] end_D = txtTillDate.Text.Split('/'); // end date
            string end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];

            lblDatePeriod.Text = "From " + txtFromDate.Text + " till " + txtTillDate.Text;

            tblIncome income = new tblIncome();
            income.GetWeekly_Income_Salaries_with_Status(Convert.ToDateTime(start), Convert.ToDateTime(end));
            grdWeeklyReference.DataSource = income.DefaultView;
            grdWeeklyReference.DataBind();


        }
        catch (Exception ex)
        { }

        upnlReference.Update();
    }

    protected void grdWeeklyReference_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                Label lblTotalSalaries = e.Row.FindControl("lblTotalSalaries") as Label;
                LinkButton lnkDate = e.Row.FindControl("lnkDate") as LinkButton;
                CheckBox chkPaid = e.Row.FindControl("chkPaid") as CheckBox;
                CheckBox chkAllow = e.Row.FindControl("chkAllow") as CheckBox;

                string weekStart = string.Empty;
                string weekEnd = string.Empty;

                DataRowView drv = (DataRowView)e.Row.DataItem;
                weekStart = Convert.ToDateTime(drv["dWeekStartDate"]).ToString("dd") + "/" + Convert.ToDateTime(drv["dWeekStartDate"]).ToString("MM");
                weekEnd = Convert.ToDateTime(drv["dWeekEndDate"]).ToString("dd") + "/" + Convert.ToDateTime(drv["dWeekEndDate"]).ToString("MM");

                double salaries = 0.0;



               

                if (lblTotalSalaries.Text == "" || lblTotalSalaries.Text == "0")
                    lblTotalSalaries.Text = "00,00 €";
                else
                {
                    salaries = Convert.ToDouble(lblTotalSalaries.Text);
                    lblTotalSalaries.Text = commonMethods.ChangetToUK(lblTotalSalaries.Text) + " €";
                }


                tblUsers u = new tblUsers();
                u.GetAllSalaries(Convert.ToInt32(lnkDate.CommandArgument), DepartmentID, Convert.ToDateTime(drv["dWeekStartDate"]), Convert.ToDateTime(drv["dWeekEndDate"]));
                if (u.RowCount > 0)
                {
                    for (int i = 0; i < u.RowCount; i++)
                    {
                        tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
                        weeklyPayment.GetWeeklySalariedUser(u.PkUserID, Convert.ToDateTime(u.GetColumn("dWeekStartDate")), Convert.ToDateTime(u.GetColumn("dWeekEndDate")));
                        if (weeklyPayment.RowCount > 0)
                        {
                            chkPaid.Checked = true;
                            chkPaid.Enabled = true;
                        }
                        else
                        {
                            chkPaid.Checked = false;
                            chkPaid.Enabled = true;
                            break;
                        }
                        u.MoveNext();
                    }
                }

                lnkDate.Text = "Week " + lblDate.Text + "( " + weekStart + " - " + weekEnd + ")";

                if (Convert.ToBoolean(drv["permission"]))
                {
                    chkPaid.Enabled = true;
                    chkAllow.Checked = true;
                }
                else if (!Convert.ToBoolean(drv["permission"]))
                {
                    chkPaid.Enabled = true;
                    chkAllow.Checked = false;
                }

                if (Convert.ToBoolean(drv["SalaryStatus"]))
                    chkPaid.Checked = true;
                else if (!Convert.ToBoolean(drv["SalaryStatus"]))
                {
                    chkPaid.Checked = false;
                    chkPaid.Enabled = true;
                }
            }
            catch (Exception ex)
            { }
        }
    }
    protected void grdWeeklyReference_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument != null)
            {
                int weeknumber = Convert.ToInt32(e.CommandArgument);
                selectedWeek = weeknumber;
                LinkButton lnk = e.CommandSource as LinkButton;
                HiddenField hidWeekStart = lnk.FindControl("hidWeekStart") as HiddenField;
                HiddenField hidWeekEnd = lnk.FindControl("hidWeekEnd") as HiddenField;
                HiddenField hidStatus = lnk.FindControl("hidStatus") as HiddenField;
                CheckBox chkPaid = lnk.FindControl("chkPaid") as CheckBox;
                if (!chkPaid.Checked)
                    checkSalaryPaid = false;
                else
                    checkSalaryPaid = true;
                SelectedWeekStart = Convert.ToDateTime(hidWeekStart.Value);
                SelectedWeekEnd = Convert.ToDateTime(hidWeekEnd.Value);

                switch (e.CommandName.ToLower())
                {
                    case "week":
                        if (hidStatus.Value != "" && hidStatus.Value != "0")
                        {
                            checkStatus = Convert.ToBoolean(hidStatus.Value);
                        }
                        total = 0.0;
                        GetTotalUserForSelectedWeek(weeknumber, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        GetSalaries(weeknumber, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "week", "$(function(){SalaryTab();});", true);
                        tblSalary.Visible = true;

                        upnlSalaries.Update();
                        upnlReference.Update();
                        break;
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void chkPaid_Clicked(object sender, EventArgs e)
    {
        CheckBox chkPaid = ((CheckBox)sender).FindControl("chkPaid") as CheckBox;
        LinkButton lnkDate = ((CheckBox)sender).FindControl("lnkDate") as LinkButton;
        HiddenField hidWeekStart = ((CheckBox)sender).FindControl("hidWeekStart") as HiddenField;
        HiddenField hidWeekEnd = ((CheckBox)sender).FindControl("hidWeekEnd") as HiddenField;
        HiddenField hidStatus = ((CheckBox)sender).FindControl("hidStatus") as HiddenField;

        if (chkPaid.Checked)
        {

            tblUserWorkshifts uws = new tblUserWorkshifts();
            uws.getOneWeekUsers(Convert.ToInt32(lnkDate.CommandArgument), DepartmentID);

            if (uws.RowCount > 0)
            {
                for (int i = 0; i < uws.RowCount; i++)
                {
                    tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
                    weeklyPayment.GetWeeklySalariedUser(uws.FkUserID, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                    if (weeklyPayment.RowCount > 0)
                    {
                        weeklyPayment.EmailDate = DateTime.Now;
                        weeklyPayment.DModifiedDate = DateTime.Now;
                        weeklyPayment.PaidByAccountManager = true;
                        weeklyPayment.AllowByDepartmentAdmin = false;
                        weeklyPayment.Save();
                    }
                    else
                    {
                        if (uws.RowCount > 0)
                        {
                            weeklyPayment.AddNew();
                            weeklyPayment.FkUserID = uws.FkUserID;
                            weeklyPayment.IWeekNumber = Convert.ToInt32(lnkDate.CommandArgument);
                            weeklyPayment.DWeekStartDate = Convert.ToDateTime(hidWeekStart.Value);
                            weeklyPayment.DWeekEndDate = Convert.ToDateTime(hidWeekEnd.Value);
                            weeklyPayment.DCreatedDate = DateTime.Now;
                            weeklyPayment.DModifiedDate = DateTime.Now;
                            weeklyPayment.EmailDate = DateTime.Now;
                            weeklyPayment.PaidByAccountManager = true;
                            weeklyPayment.ReceivedByUser = false;
                            weeklyPayment.BIsActive = true;
                            weeklyPayment.AllowByDepartmentAdmin = false;
                            weeklyPayment.Save();
                            
                            try
                            {
                                string fromAddress = string.Empty;
                                tblUsers uProfile = new tblUsers();
                                uProfile.GetUserProfile(UserID);
                                fromAddress = uProfile.GetColumn("sEmail").ToString();

                                uProfile.FlushData();
                                uProfile.GetUserProfile(uws.FkUserID);

                                tblUsers u = new tblUsers();
                                u.LoadByPrimaryKey(uws.FkUserID);

                                Emailing email = new Emailing();
                                email.P_ToAddress = uProfile.GetColumn("sEmail").ToString();
                                email.P_FromAddress = fromAddress;
                                email.P_Email_Subject = "Payment";
                                email.P_Message_Body = "<div><table style='border:1px solid #e8e8e8' align='center'><tr><td align='left' style='font-weight:bold;height:30px;background-color:Red;vertical-align:middle; padding-left:15px;'>" + lblWeek.Text + "</td></tr> ";
                                email.P_Message_Body += "<tr><td align='left' style='background-color:White;font-family:Verdana, Geneva, sans-serif;height:200px;' valign='top'>";
                                email.P_Message_Body += "<div style='padding-top: 10px;'><span style='font-weight: bold; margin-top: 1px;'> Dear " + u.GetColumn("SUsername").ToString() + " " + uProfile.GetColumn("FullName").ToString() + "</span><br /><br />";
                                email.P_Message_Body += "<span>The Department’s Account Manager has marked you as “paid off” for the given Week.<br/>Please check your “My Payments” page and sign up for your payment!</span>";
                                email.P_Message_Body += "</div></td></tr></table></div>";
                                email.Send_Email();
                            }
                            catch (Exception ex)
                            { }
                        }
                    }

                   
                    uws.MoveNext();
                }
            }
        }
        else if (!chkPaid.Checked)
        {
            tblUserWeeklyPayments uws = new tblUserWeeklyPayments();
            uws.getOneWeekUsers(Convert.ToInt32(lnkDate.CommandArgument), Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
            if (uws.RowCount > 0)
            {
                for (int i = 0; i < uws.RowCount; i++)
                {
                    uws.AllowByDepartmentAdmin = true;
                    uws.PaidByAccountManager = false;
                    uws.DModifiedDate = DateTime.Now;
                    uws.Save();

                    uws.MoveNext();
                }
            }
        }
        GetReference();
    }


    protected void chkAllow_Clicked(object sender, EventArgs e)
    {
        CheckBox chkAllow = ((CheckBox)sender).FindControl("chkAllow") as CheckBox;
        CheckBox chkPaid = ((CheckBox)sender).FindControl("chkPaid") as CheckBox;
        LinkButton lnkDate = ((CheckBox)sender).FindControl("lnkDate") as LinkButton;
        HiddenField hidWeekStart = ((CheckBox)sender).FindControl("hidWeekStart") as HiddenField;
        HiddenField hidWeekEnd = ((CheckBox)sender).FindControl("hidWeekEnd") as HiddenField;
        HiddenField hidStatus = ((CheckBox)sender).FindControl("hidStatus") as HiddenField;

        if (chkAllow.Checked)
        {

            tblUserWorkshifts uws = new tblUserWorkshifts();
            uws.getOneWeekUsers(Convert.ToInt32(lnkDate.CommandArgument), DepartmentID);

            if (uws.RowCount > 0)
            {
                for (int i = 0; i < uws.RowCount; i++)
                {
                    tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
                    weeklyPayment.GetWeeklySalariedUser(uws.FkUserID, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                    if (weeklyPayment.RowCount > 0)
                    {
                        weeklyPayment.EmailDate = DateTime.Now;
                        weeklyPayment.DModifiedDate = DateTime.Now;
                        weeklyPayment.PaidByAccountManager = false;
                        weeklyPayment.AllowByDepartmentAdmin = true;
                        weeklyPayment.Save();

                        try
                        {
                            string fromAddress = string.Empty;
                            tblUsers uProfile = new tblUsers();
                            uProfile.GetUserProfile(UserID);
                            fromAddress = uProfile.GetColumn("sEmail").ToString();

                            uProfile.FlushData();
                            uProfile.GetUserProfile(uws.FkUserID);

                            tblUsers u = new tblUsers();
                            u.LoadByPrimaryKey(uws.FkUserID);

                            Emailing email = new Emailing();
                            email.P_ToAddress = uProfile.GetColumn("sEmail").ToString();
                            email.P_FromAddress = fromAddress;
                            email.P_Email_Subject = "Payment";
                            email.P_Message_Body = "<div><table style='border:1px solid #e8e8e8' align='center'><tr><td align='left' style='font-weight:bold;height:30px;background-color:Red;vertical-align:middle; padding-left:15px;'>" + lblWeek.Text + "</td></tr> ";
                            email.P_Message_Body += "<tr><td align='left' style='background-color:White;font-family:Verdana, Geneva, sans-serif;height:200px;' valign='top'>";
                            email.P_Message_Body += "<div style='padding-top: 10px;'><br /><br />";
                            email.P_Message_Body += "<span>The Department’s Account Manager has provided the permission marked again the salary as “paid off” for the given Week.</span>";
                            email.P_Message_Body += "</div></td></tr></table></div>";
                            email.Send_Email();
                        }
                        catch (Exception ex)
                        { }

                    }
                    else
                    {
                        if (uws.RowCount > 0)
                        {
                            weeklyPayment.AddNew();
                            weeklyPayment.FkUserID = uws.FkUserID;
                            weeklyPayment.IWeekNumber = Convert.ToInt32(lnkDate.CommandArgument);
                            weeklyPayment.DWeekStartDate = Convert.ToDateTime(hidWeekStart.Value);
                            weeklyPayment.DWeekEndDate = Convert.ToDateTime(hidWeekEnd.Value);
                            weeklyPayment.DCreatedDate = DateTime.Now;
                            weeklyPayment.DModifiedDate = DateTime.Now;
                            weeklyPayment.EmailDate = DateTime.Now;
                            weeklyPayment.PaidByAccountManager = false;
                            weeklyPayment.ReceivedByUser = false;
                            weeklyPayment.BIsActive = true;
                            weeklyPayment.AllowByDepartmentAdmin = true;
                            weeklyPayment.Save();
                        }
                    }
                    uws.MoveNext();
                }
            }
        }
        else if (!chkAllow.Checked)
        {
            tblUserWeeklyPayments uws = new tblUserWeeklyPayments();
            uws.getOneWeekUsers(Convert.ToInt32(lnkDate.CommandArgument), Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
            if (uws.RowCount > 0)
            {
                for (int i = 0; i < uws.RowCount; i++)
                {
                    uws.AllowByDepartmentAdmin = true;
                    uws.PaidByAccountManager = false;
                    uws.DModifiedDate = DateTime.Now;
                    uws.Save();

                    uws.MoveNext();
                }
            }
        }
        GetReference();

    }
    #endregion

    #region Salaries

    private void GetSalaries(int weeknumber, DateTime weekStart, DateTime weekEnd)
    {

        try
        {
            tblUsers u = new tblUsers();
            u.GetAllSalaries(weeknumber, DepartmentID, weekStart, weekEnd);

            ViewState["iWeekNo"] = weeknumber;
            DataTable dt = new DataTable();
            dt = u.DefaultView.ToTable();

            u.FlushData();
            u.LoadManager_and_AccountManager(DepartmentID);

            foreach (DataRow drow in u.DefaultView.ToTable().Rows)
            {
                DataRow dr = dt.NewRow();
                dr["FullName"] = drow["FullName"];// u.s_SFirstName + " " + u.s_SLastName;
                dr["pkuserid"] = u.PkUserID;
                dr["uAdvance"] = 0;
                dr["dWeekStartDate"] = weekStart.ToString();
                dr["dWeekEndDate"] = weekEnd.ToString();
                tblUserContract objContract = new tblUserContract();

                objContract.GetUserSeasonSalary(weekStart, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                switch (objContract.FkSalaryTypeID)
                {
                    case 1:
                        dr["Sunday"] = objContract.s_HighSeasonSalary;
                        break;
                    case 2:
                        dr["Sunday"] = objContract.s_StandardSalary;
                        break;
                    case 3:
                        dr["Sunday"] = objContract.s_MinimumPerday;
                        break;
                }

                objContract.FlushData();
                objContract.GetUserSeasonSalary(weekStart.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                switch (objContract.FkSalaryTypeID)
                {
                    case 1:
                        dr["Monday"] = objContract.s_HighSeasonSalary;
                        break;
                    case 2:
                        dr["Monday"] = objContract.s_StandardSalary;
                        break;
                    case 3:
                        dr["Monday"] = objContract.s_MinimumPerday;
                        break;
                }

                objContract.FlushData();
                objContract.GetUserSeasonSalary(weekStart.AddDays(2), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                switch (objContract.FkSalaryTypeID)
                {
                    case 1:
                        dr["Tuesday"] = objContract.s_HighSeasonSalary;
                        break;
                    case 2:
                        dr["Tuesday"] = objContract.s_StandardSalary;
                        break;
                    case 3:
                        dr["Tuesday"] = objContract.s_MinimumPerday;
                        break;
                }

                objContract.FlushData();
                objContract.GetUserSeasonSalary(weekStart.AddDays(3), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                switch (objContract.FkSalaryTypeID)
                {
                    case 1:
                        dr["Wednesday"] = objContract.s_HighSeasonSalary;
                        break;
                    case 2:
                        dr["Wednesday"] = objContract.s_StandardSalary;
                        break;
                    case 3:
                        dr["Wednesday"] = objContract.s_MinimumPerday;
                        break;
                }

                objContract.FlushData();
                objContract.GetUserSeasonSalary(weekStart.AddDays(4), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                switch (objContract.FkSalaryTypeID)
                {
                    case 1:
                        dr["Thursday"] = objContract.s_HighSeasonSalary;
                        break;
                    case 2:
                        dr["Thursday"] = objContract.s_StandardSalary;
                        break;
                    case 3:
                        dr["Thursday"] = objContract.s_MinimumPerday;
                        break;
                }

                objContract.FlushData();
                objContract.GetUserSeasonSalary(weekStart.AddDays(5), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                switch (objContract.FkSalaryTypeID)
                {
                    case 1:
                        dr["Friday"] = objContract.s_HighSeasonSalary;
                        break;
                    case 2:
                        dr["Friday"] = objContract.s_StandardSalary;
                        break;
                    case 3:
                        dr["Friday"] = objContract.s_MinimumPerday;
                        break;
                }

                objContract.FlushData();
                objContract.GetUserSeasonSalary(weekStart.AddDays(6), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                switch (objContract.FkSalaryTypeID)
                {
                    case 1:
                        dr["Saturday"] = objContract.s_HighSeasonSalary;
                        break;
                    case 2:
                        dr["Saturday"] = objContract.s_StandardSalary;
                        break;
                    case 3:
                        dr["Saturday"] = objContract.s_MinimumPerday;
                        break;
                }


                dr["SalaryStatus"] = "True";
                dr["permission"] = "True";

                tblManagerDayOff objManagerDayoff = new tblManagerDayOff();
                objManagerDayoff.getSingleDayOffforSalery(Convert.ToInt32(drow["pkuserid"]), weekStart, weekEnd);
                if (objManagerDayoff.RowCount > 0)
                {
                    for (int i = 0; i < objManagerDayoff.RowCount; i++)
                    {
                        string WeekDay = Convert.ToDateTime(objManagerDayoff.s_MSingleDate).DayOfWeek.ToString();
                        switch (WeekDay)
                        {
                            case "Sunday":
                                dr["Sunday"] = 0;
                                break;
                            case "Monday":
                                dr["Monday"] = 0;
                                break;
                            case "Tuesday":
                                dr["Tuesday"] = 0;
                                break;
                            case "Wednesday":
                                dr["Wednesday"] = 0;
                                break;
                            case "Thursday":
                                dr["Thursday"] = 0;
                                break;
                            case "Friday":
                                dr["Friday"] = 0;
                                break;
                            case "Saturday":
                                dr["Saturday"] = 0;
                                break;
                        }
                        objManagerDayoff.MoveNext();
                    }

                }
                dt.Rows.Add(dr);

            }

            //*******************************


            u.FlushData();
            u.LoadECUser(DepartmentID);

            foreach (DataRow drow in u.DefaultView.ToTable().Rows)
            {
                tblECUserAssignments ua = new tblECUserAssignments();
                ua.CheckECUsersSaleryForWeek(Convert.ToInt32(drow["pkuserid"]), weekStart, weekEnd);
                if (ua.RowCount > 0)
                {

                    DataRow dr = dt.NewRow();
                    dr["FullName"] = drow["FullName"];
                    dr["pkuserid"] = u.PkUserID;
                    dr["uAdvance"] = 0;
                    dr["dWeekStartDate"] = weekStart.ToString();
                    dr["dWeekEndDate"] = weekEnd.ToString();
                    tblUserContract objContract = new tblUserContract();


                    dr["Sunday"] = 0;


                    dr["Monday"] = 0;
                    dr["Tuesday"] = 0;
                    dr["Wednesday"] = 0;
                    dr["Thursday"] = 0;
                    dr["Friday"] = 0;
                    dr["Saturday"] = 0;

                    dr["SalaryStatus"] = "True";
                    dr["permission"] = "True";


                    for (int j = 0; j < 7; j++)
                    {
                        ua.FlushData();
                        ua.CheckECUsersSalery(weekStart.AddDays(j), Convert.ToInt32(drow["pkuserid"]));
                        if (ua.RowCount > 0)
                        {


                            string WeekDay = weekStart.AddDays(j).DayOfWeek.ToString();
                            switch (WeekDay)
                            {
                                case "Sunday":
                                    objContract.FlushData();
                                    objContract.GetUserSeasonSalary(weekStart, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                    switch (objContract.FkSalaryTypeID)
                                    {
                                        case 1:
                                            dr["Sunday"] = objContract.s_HighSeasonSalary;
                                            break;
                                        case 2:
                                            dr["Sunday"] = objContract.s_StandardSalary;
                                            break;
                                        case 3:
                                            dr["Sunday"] = objContract.s_MinimumPerday;
                                            break;
                                    }

                                    break;
                                case "Monday":
                                    objContract.FlushData();
                                    objContract.GetUserSeasonSalary(weekStart.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                    switch (objContract.FkSalaryTypeID)
                                    {
                                        case 1:
                                            dr["Monday"] = objContract.s_HighSeasonSalary;
                                            break;
                                        case 2:
                                            dr["Monday"] = objContract.s_StandardSalary;
                                            break;
                                        case 3:
                                            dr["Monday"] = objContract.s_MinimumPerday;
                                            break;
                                    }
                                    break;
                                case "Tuesday":
                                    objContract.FlushData();
                                    objContract.GetUserSeasonSalary(weekStart.AddDays(2), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                    switch (objContract.FkSalaryTypeID)
                                    {
                                        case 1:
                                            dr["Tuesday"] = objContract.s_HighSeasonSalary;
                                            break;
                                        case 2:
                                            dr["Tuesday"] = objContract.s_StandardSalary;
                                            break;
                                        case 3:
                                            dr["Tuesday"] = objContract.s_MinimumPerday;
                                            break;
                                    }
                                    break;
                                case "Wednesday":
                                    objContract.FlushData();
                                    objContract.GetUserSeasonSalary(weekStart.AddDays(3), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                    switch (objContract.FkSalaryTypeID)
                                    {
                                        case 1:
                                            dr["Wednesday"] = objContract.s_HighSeasonSalary;
                                            break;
                                        case 2:
                                            dr["Wednesday"] = objContract.s_StandardSalary;
                                            break;
                                        case 3:
                                            dr["Wednesday"] = objContract.s_MinimumPerday;
                                            break;
                                    }
                                    break;
                                case "Thursday":
                                    objContract.FlushData();
                                    objContract.GetUserSeasonSalary(weekStart.AddDays(4), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                    switch (objContract.FkSalaryTypeID)
                                    {
                                        case 1:
                                            dr["Thursday"] = objContract.s_HighSeasonSalary;
                                            break;
                                        case 2:
                                            dr["Thursday"] = objContract.s_StandardSalary;
                                            break;
                                        case 3:
                                            dr["Thursday"] = objContract.s_MinimumPerday;
                                            break;
                                    }
                                    break;
                                case "Friday":
                                    objContract.FlushData();
                                    objContract.GetUserSeasonSalary(weekStart.AddDays(5), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                    switch (objContract.FkSalaryTypeID)
                                    {
                                        case 1:
                                            dr["Friday"] = objContract.s_HighSeasonSalary;
                                            break;
                                        case 2:
                                            dr["Friday"] = objContract.s_StandardSalary;
                                            break;
                                        case 3:
                                            dr["Friday"] = objContract.s_MinimumPerday;
                                            break;
                                    }
                                    break;
                                case "Saturday":
                                    objContract.FlushData();
                                    objContract.GetUserSeasonSalary(weekStart.AddDays(6), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                    switch (objContract.FkSalaryTypeID)
                                    {
                                        case 1:
                                            dr["Saturday"] = objContract.s_HighSeasonSalary;
                                            break;
                                        case 2:
                                            dr["Saturday"] = objContract.s_StandardSalary;
                                            break;
                                        case 3:
                                            dr["Saturday"] = objContract.s_MinimumPerday;
                                            break;
                                    }
                                    break;
                            }
                        }





                    }
                    dt.Rows.Add(dr);
                }
            }
            //*********************************










            grdSalaries.DataSource = dt.DefaultView;

            grdSalaries.DataBind();

            if (checkSalaryPaid)
            {
                trChecked.Visible = true;
                trAsk.Visible = true;
            }
            else if (!checkSalaryPaid)
            {
                trMark.Visible = true;
                trChecked.Visible = false;
                trAsk.Visible = false;
            }
            if (u.RowCount > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "week", "$(function(){SalaryTab();});", true);
                tblSalary.Visible = true;
            }
            txtTotal.Text = total.ToString("N") + " €";
            upnlSalaries.Update();
        }
        catch (Exception ex)
        {

        }
    }

    //private void GetSalaries(int weeknumber, DateTime weekStart, DateTime weekEnd)
    //{
    //    tblUsers u = new tblUsers();
    //    u.GetAllSalaries(weeknumber, DepartmentID, weekStart, weekEnd);
    //    grdSalaries.DataSource = u.DefaultView;
    //    grdSalaries.DataBind();

    //    if (checkSalaryPaid)
    //    {
    //        trChecked.Visible = true;
    //        trAsk.Visible = true;
    //    }
    //    else if (!checkSalaryPaid)
    //    {
    //        trMark.Visible = true;
    //        trChecked.Visible = false;
    //        trAsk.Visible = false;
    //    }
    //    if (u.RowCount > 0)
    //    {
    //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "week", "$(function(){SalaryTab();});", true);
    //        tblSalary.Visible = true;
    //    }
    //    txtTotal.Text = total.ToString("N") + " €";
    //    upnlSalaries.Update();
    //}



    protected void imgBtnMarkAsPaid_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void lnkMarkAsPaid_Click(Object sender, EventArgs e)
    {
        DateTime start = DateTime.Now;
        DateTime end = DateTime.Now;
        bool yes = false;
        for (int i = 0; i < grdSalaries.Rows.Count; i++)
        {
            CheckBox chkPaid = grdSalaries.Rows[i].FindControl("chkPaid") as CheckBox;
            HiddenField hidWeekStart = grdSalaries.Rows[i].FindControl("hidWeekStart") as HiddenField;
            HiddenField hidWeekEnd = grdSalaries.Rows[i].FindControl("hidWeekEnd") as HiddenField;
            HiddenField hidUserIDCheck = grdSalaries.Rows[i].FindControl("hidUserIDCheck") as HiddenField;

            start = Convert.ToDateTime(hidWeekStart.Value);
            end = Convert.ToDateTime(hidWeekEnd.Value);

            if (chkPaid.Checked)
            {
                tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
                weeklyPayment.GetWeeklySalariedUser(Convert.ToInt32(hidUserIDCheck.Value), start, end);
                if (weeklyPayment.RowCount > 0)
                {
                    weeklyPayment.EmailDate = DateTime.Now;
                    weeklyPayment.DModifiedDate = DateTime.Now;
                    weeklyPayment.PaidByAccountManager = true;
                    weeklyPayment.ReceivedByUser = false;
                    weeklyPayment.AllowByDepartmentAdmin = false;
                    weeklyPayment.Save();
                }
                else
                {
                    weeklyPayment.AddNew();
                    weeklyPayment.FkUserID = Convert.ToInt32(hidUserIDCheck.Value);
                    weeklyPayment.IWeekNumber = selectedWeek;
                    weeklyPayment.DWeekStartDate = start;
                    weeklyPayment.DWeekEndDate = end;
                    weeklyPayment.DCreatedDate = DateTime.Now;
                    weeklyPayment.DModifiedDate = DateTime.Now;
                    weeklyPayment.EmailDate = DateTime.Now;
                    weeklyPayment.PaidByAccountManager = true;
                    weeklyPayment.ReceivedByUser = false;
                    weeklyPayment.BIsActive = true;
                    weeklyPayment.AllowByDepartmentAdmin = true;
                    weeklyPayment.Save();
                }
                //yes = true;
                //break;
            }
            else if (!chkPaid.Checked)
            {
                tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
                weeklyPayment.GetWeeklySalariedUser(Convert.ToInt32(hidUserIDCheck.Value), start, end);
                if (weeklyPayment.RowCount > 0)
                {
                    weeklyPayment.EmailDate = DateTime.Now;
                    weeklyPayment.DModifiedDate = DateTime.Now;
                    weeklyPayment.PaidByAccountManager = false;
                    weeklyPayment.ReceivedByUser = false;
                    weeklyPayment.AllowByDepartmentAdmin = false;
                    weeklyPayment.Save();
                }
            }
            try
            {
                string fromAddress = string.Empty;
                tblUsers uProfile = new tblUsers();
                uProfile.GetUserProfile(UserID);
                fromAddress = uProfile.GetColumn("sEmail").ToString();

                uProfile.FlushData();
                uProfile.GetUserProfile(Convert.ToInt32(hidUserIDCheck.Value));

                tblUsers u = new tblUsers();
                u.LoadByPrimaryKey(Convert.ToInt32(hidUserIDCheck.Value));

                Emailing email = new Emailing();
                email.P_ToAddress = uProfile.GetColumn("sEmail").ToString();
                email.P_FromAddress = fromAddress;
                email.P_Email_Subject = "Payment";
                email.P_Message_Body = "<div><table style='border:1px solid #e8e8e8' align='center'><tr><td align='left' style='font-weight:bold;height:30px;background-color:Red;vertical-align:middle; padding-left:15px;'>" + lblWeek.Text + "</td></tr> ";
                email.P_Message_Body += "<tr><td align='left' style='background-color:White;font-family:Verdana, Geneva, sans-serif;height:200px;' valign='top'>";
                email.P_Message_Body += "<div style='padding-top: 10px;'><span style='font-weight: bold; margin-top: 1px;'> Dear " + u.GetColumn("SUsername").ToString() + " " + uProfile.GetColumn("FullName").ToString() + "</span><br /><br />";
                email.P_Message_Body += "<span>The Department’s Account Manager has marked you as “paid off” for the given Week.<br/>Please check your “My Payments” page and sign up for your payment!</span>";
                email.P_Message_Body += "</div></td></tr></table></div>";
                email.Send_Email();
            }
            catch (Exception ex)
            { }
        }
        GetReference();
        upnlSalaries.Update();

        #region commented
        /*
        if (yes)
        {
            tblUsers u = new tblUsers();
            u.GetSalariedUsersIDs(DepartmentID);
            if (u.RowCount > 0)
            {
                tblUserWorkshifts uws = new tblUserWorkshifts();
                for (int i = 0; i < u.RowCount; i++)
                {
                    uws.FlushData();
                    DataTable dt = uws.LoadCurrentUserSalary(selectedWeek, start, DepartmentID, end.Year, u.PkUserID);
                    tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
                    weeklyPayment.GetWeeklySalariedUser(u.PkUserID, start, end);
                    if (weeklyPayment.RowCount > 0)
                    {
                        weeklyPayment.EmailDate = DateTime.Now;
                        weeklyPayment.DModifiedDate = DateTime.Now;
                        weeklyPayment.PaidByAccountManager = true;
                        weeklyPayment.AllowByDepartmentAdmin = false;
                        weeklyPayment.Save();

                    }
                    else
                    {
                        if (uws.RowCount > 0)
                        {
                            weeklyPayment.AddNew();
                            weeklyPayment.FkUserID = u.PkUserID;
                            weeklyPayment.IWeekNumber = selectedWeek;
                            weeklyPayment.DWeekStartDate = start;
                            weeklyPayment.DWeekEndDate = end;
                            weeklyPayment.DCreatedDate = DateTime.Now;
                            weeklyPayment.DModifiedDate = DateTime.Now;
                            weeklyPayment.EmailDate = DateTime.Now;
                            weeklyPayment.PaidByAccountManager = true;
                            weeklyPayment.ReceivedByUser = false;
                            weeklyPayment.BIsActive = true;
                            weeklyPayment.AllowByDepartmentAdmin = false;
                            weeklyPayment.Save();
                        }
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
                        email.P_Message_Body = "<div><table style='border:1px solid #e8e8e8' align='center'><tr><td align='left' style='font-weight:bold;height:30px;background-color:Red;vertical-align:middle; padding-left:15px;'>" + lblWeek.Text + "</td></tr> ";
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
        else if (!yes)
        {
            tblUsers u = new tblUsers();
            u.GetSalariedUsersIDs(DepartmentID);
            if (u.RowCount > 0)
            {
                for (int i = 0; i < u.RowCount; i++)
                {
                    tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
                    weeklyPayment.GetWeeklySalariedUser(u.PkUserID, start, end);
                    if (weeklyPayment.RowCount > 0)
                    {
                        weeklyPayment.AllowByDepartmentAdmin = false;
                        weeklyPayment.PaidByAccountManager = false;
                        weeklyPayment.DModifiedDate = DateTime.Now;
                        weeklyPayment.Save();
                    }
                    u.MoveNext();
                }
            }
        }
        */
        #endregion
    }

    protected void lnkAskForEdit_Click(Object sender, EventArgs e)
    {
        try
        {
            #region email
            Emailing email = new Emailing();

            tblUsers u = new tblUsers();
            u.GetDepartmentAdminID(DepartmentID);
            tblUserEmails ue = new tblUserEmails();
            ue.LoadUserEmails(UserID);
            if (ue.RowCount > 0)
            {
                email.P_FromAddress = ue.SEmail;
            }
            ue.FlushData();
            ue.LoadUserEmails(Convert.ToInt32(u.GetColumn("fkuserid").ToString()));
            if (ue.RowCount > 0)
            {
                email.P_ToAddress = ue.SEmail;
            }
            email.P_Email_Subject = "Further Salary edits on week#" + selectedWeek;
            email.P_Message_Body = "Please allow further edits on salaries for week#" + selectedWeek + ". Reason: ………………";
            email.Send_Email();
            #endregion

            int departmentAdminID = Convert.ToInt32(u.GetColumn("fkuserid").ToString());
            u.FlushData();
            u.LoadByPrimaryKey(UserID);

            lblFromAddress.Text = u.SFirstName + " " + u.SLastName;

            u.FlushData();
            u.LoadByPrimaryKey(departmentAdminID);

            lblToAddress.Text = u.SFirstName + " " + u.SLastName;
            txtSubject.Text = "Further Salary edits on week#" + selectedWeek;
            txtMessage.Text = "Please allow further edits on salaries for week#" + selectedWeek + ". Reason: ………………";
            ModalPopupExtender1.Show();

        }
        catch (Exception ex)
        { }

    }

    protected void imgBtnMessage_Click(object sender, ImageClickEventArgs e)
    {
        #region Internal Message

        tblUsers u = new tblUsers();
        u.GetDepartmentAdminID(DepartmentID);
        tblUserInBox userIn = new tblUserInBox();
        // For Current Manger
        userIn.AddNew();
        userIn.FkFromUserID = UserID;
        userIn.FkToUserID = Convert.ToInt32(u.GetColumn("fkuserid").ToString());
        userIn.SSubject = txtSubject.Text;
        userIn.SMessage = txtMessage.Text;
        userIn.DReceivedDate = DateTime.Now;
        userIn.BIsread = false;
        userIn.Save();
        userIn.FlushData();


        tblUserSentBox userOut = new tblUserSentBox();

        //For Current Manager
        userOut.AddNew();
        userOut.FkFromUserID = UserID;
        userOut.FkToUserID = Convert.ToInt32(u.GetColumn("fkuserid").ToString());
        userOut.SSubject = txtSubject.Text;
        userOut.SMessage = txtMessage.Text;
        userOut.DSentDate = DateTime.Now;
        userOut.Save();
        userOut.FlushData();
        #endregion
        ModalPopupExtender1.Hide();
        upnlSalaries.Update();
    }
    protected void grdSalaries_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

            Label lblSundayHeader = e.Row.FindControl("lblSundayHeader") as Label;
            Label lblMondayHeader = e.Row.FindControl("lblMondayHeader") as Label;
            Label lblTuesdayHeader = e.Row.FindControl("lblTuesdayHeader") as Label;
            Label lblWednesdayHeader = e.Row.FindControl("lblWednesdayHeader") as Label;
            Label lblThursdayHeader = e.Row.FindControl("lblThursdayHeader") as Label;
            Label lblFridayHeader = e.Row.FindControl("lblFridayHeader") as Label;
            Label lblSaturdayHeader = e.Row.FindControl("lblSaturdayHeader") as Label;


            lblSundayHeader.Text = "Sunday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1).ToString("dd/MM");
            lblMondayHeader.Text = "Monday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(0).ToString("dd/MM");
            lblTuesdayHeader.Text = "Tuesday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(1).ToString("dd/MM");
            lblWednesdayHeader.Text = "Wednesday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(2).ToString("dd/MM");
            lblThursdayHeader.Text = "Thursday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(3).ToString("dd/MM");
            lblFridayHeader.Text = "Friday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(4).ToString("dd/MM");
            lblSaturdayHeader.Text = "Saturday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5).ToString("dd/MM");
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblSundaySalary = e.Row.FindControl("lblSundaySalary") as Label;
                Label lblMondaySalary = e.Row.FindControl("lblMondaySalary") as Label;
                Label lblTuesdaySalary = e.Row.FindControl("lblTuesdaySalary") as Label;
                Label lblWednesdaySalary = e.Row.FindControl("lblWednesdaySalary") as Label;
                Label lblThursdaySalary = e.Row.FindControl("lblThursdaySalary") as Label;
                Label lblFridaySalary = e.Row.FindControl("lblFridaySalary") as Label;
                Label lblSaturdaySalary = e.Row.FindControl("lblSaturdaySalary") as Label;

                HiddenField hidWeekStart = e.Row.FindControl("hidWeekStart") as HiddenField;
                HiddenField hidWeekEnd = e.Row.FindControl("hidWeekEnd") as HiddenField;
                HiddenField hidUserIDCheck = e.Row.FindControl("hidUserIDCheck") as HiddenField;



                CheckBox chkPaid = e.Row.FindControl("chkPaid") as CheckBox;

                Label lblWeekSubtotal = e.Row.FindControl("lblWeekSubtotal") as Label;

                lblWeekSubtotal.Text = (Convert.ToDouble(lblSundaySalary.Text) +
                                        Convert.ToDouble(lblMondaySalary.Text) +
                                        Convert.ToDouble(lblTuesdaySalary.Text) +
                                        Convert.ToDouble(lblWednesdaySalary.Text) +
                                        Convert.ToDouble(lblThursdaySalary.Text) +
                                        Convert.ToDouble(lblFridaySalary.Text) +
                                        Convert.ToDouble(lblSaturdaySalary.Text)).ToString("N");
                total += Convert.ToDouble(lblWeekSubtotal.Text);
                lblWeekSubtotal.Text = lblWeekSubtotal.Text + " €";
                lblSundaySalary.Text = lblSundaySalary.Text + " €";
                lblMondaySalary.Text = lblMondaySalary.Text + " €";
                lblTuesdaySalary.Text = lblTuesdaySalary.Text + " €";
                lblWednesdaySalary.Text = lblWednesdaySalary.Text + " €";
                lblThursdaySalary.Text = lblThursdaySalary.Text + " €";
                lblFridaySalary.Text = lblFridaySalary.Text + " €";
                lblSaturdaySalary.Text = lblSaturdaySalary.Text + " €";

                tblUserWeeklyPayments uwsp = new tblUserWeeklyPayments();
                uwsp.getUniqueWeekUserStatus(Convert.ToInt32(hidUserIDCheck.Value), Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                if (uwsp.RowCount > 0)
                {
                    if (uwsp.PaidByAccountManager)
                    {
                        chkPaid.Checked = true;
                        chkPaid.Enabled = false;
                    }
                    else if (!uwsp.PaidByAccountManager)
                        chkPaid.Checked = false;

                    if (Convert.ToBoolean(DataBinder.GetPropertyValue(e.Row.DataItem, "permission")))
                        chkPaid.Enabled = true;
                    else
                        chkPaid.Enabled = false;
                }
                else
                    chkPaid.Enabled = true;



                //if (Convert.ToBoolean(DataBinder.GetPropertyValue(e.Row.DataItem, "SalaryStatus")))
                //{
                //    chkPaid.Checked = true;
                //}
                //else if (!Convert.ToBoolean(DataBinder.GetPropertyValue(e.Row.DataItem, "SalaryStatus")))
                //{
                //    chkPaid.Checked = false;
                //}
            }
            catch (Exception ex)
            { }
        }
    }
    protected void grdSalaries_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument != null)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                LinkButton lnk = e.CommandSource as LinkButton;
                HiddenField hidWeekStart = lnk.FindControl("hidWeekStart") as HiddenField;
                HiddenField hidWeekEnd = lnk.FindControl("hidWeekEnd") as HiddenField;
                LinkButton lnkName = lnk.FindControl("lnkName") as LinkButton;
                switch (e.CommandName.ToLower())
                {
                    case "user":
                        p_userid = id;
                        p_weeknumber = selectedWeek;
                        p_weelStart = Convert.ToDateTime(hidWeekStart.Value);
                        p_weekEnd = Convert.ToDateTime(hidWeekEnd.Value);
                        p_name = lnkName.Text;
                        GetPaymentForSingleUser(id, selectedWeek, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value), lnkName.Text);

                        break;

                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void lnkSelectAll_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < grdSalaries.Rows.Count; i++)
        {
            CheckBox chk = grdSalaries.Rows[i].FindControl("chkPaid") as CheckBox;
            chk.Checked = true;
        }
        upnlSalaries.Update();

    }

    #endregion

    #region single User Payment on POPUp


    private void GetPaymentForSingleUser(int userid, int weeknum, DateTime weekStart, DateTime weekEnd, string name)
    {
        tblUsers u = new tblUsers();
        u.GetPaymentForSingle(userid, weeknum, DepartmentID, weekStart, weekEnd);
        grdPaymentForSingleUser.DataSource = u.DefaultView;
        total = 0.0;
        grdPaymentForSingleUser.DataBind();
        lblPopupTitle.Text = "Salary for " + name + " for week " + weeknum + "/" + weekStart.Year;
        chkAlreadyPaid.Checked = checkStatus;
        ModalPopupExtender2.Show();
        UpdatePanel1.Update();
    }
    protected void grdPaymentForSingleUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            LinkButton lnk = e.CommandSource as LinkButton;
            HiddenField hidWeekStart = lnk.FindControl("hidWeekStart") as HiddenField;
            HiddenField hidWeekEnd = lnk.FindControl("hidWeekEnd") as HiddenField;
            LinkButton lnkName = lnk.FindControl("lnkName") as LinkButton;
            Label lblType = lnk.FindControl("lblType") as Label;

            switch (e.CommandName.ToLower())
            {
                case "sunday":
                    if (lblType.Text != "Salary" && lblType.Text != "Advance" && lblType.Text != "Tips")
                    {
                        tblUserWorkshifts uws = new tblUserWorkshifts();
                        uws.GetUserWorkshit(id, selectedWeek, 1, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (uws.RowCount > 0)
                        {
                            ViewState["adid"] = null;
                            ViewState["inid"] = null;
                            ViewState["uwsid"] = uws.PkUserWorkshiftID;
                            if (lblType.Text == "Bonus")
                            {
                                txtEditItem.Text = uws.Bonus.ToString();
                                lblEditItem.Text = "Bonus";
                                ViewState["editType"] = "Bonus";
                            }
                            else if (lblType.Text == "Penalty")
                            {
                                txtEditItem.Text = uws.Penalty.ToString();
                                lblEditItem.Text = "Penalty";
                                ViewState["editType"] = "Penalty";
                            }
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Advance")
                    {
                        lblEditItem.Text = "Advance";
                        tblUserAdvances userAdvances = new tblUserAdvances();
                        userAdvances.GetUserAdvance(id, selectedWeek, 1, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (userAdvances.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["inid"] = null;
                            ViewState["adid"] = userAdvances.PkUserAdvanceID;
                            txtEditItem.Text = userAdvances.UAdvance.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Tips")
                    {
                        lblEditItem.Text = "Tips";
                        tblIncome income = new tblIncome();
                        income.GetUserIncome(id, selectedWeek, 1, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (income.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["adid"] = null;
                            ViewState["inid"] = income.PkIncomID;
                            txtEditItem.Text = income.UserTip.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }


                    break;
                case "monday":
                    if (lblType.Text != "Salary" && lblType.Text != "Advance" && lblType.Text != "Tips")
                    {
                        tblUserWorkshifts uws = new tblUserWorkshifts();
                        uws.GetUserWorkshit(id, selectedWeek, 2, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (uws.RowCount > 0)
                        {
                            ViewState["uwsid"] = uws.PkUserWorkshiftID;
                            if (lblType.Text == "Bonus")
                            {
                                txtEditItem.Text = uws.Bonus.ToString();
                                lblEditItem.Text = "Bonus";
                                ViewState["editType"] = "Bonus";
                            }
                            else if (lblType.Text == "Penalty")
                            {
                                txtEditItem.Text = uws.Penalty.ToString();
                                lblEditItem.Text = "Penalty";
                                ViewState["editType"] = "Penalty";
                            }
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Advance")
                    {
                        lblEditItem.Text = "Advance";
                        tblUserAdvances userAdvances = new tblUserAdvances();
                        userAdvances.GetUserAdvance(id, selectedWeek, 2, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (userAdvances.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["inid"] = null;
                            ViewState["adid"] = userAdvances.PkUserAdvanceID;
                            txtEditItem.Text = userAdvances.UAdvance.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Tips")
                    {
                        lblEditItem.Text = "Tips";
                        tblIncome income = new tblIncome();
                        income.GetUserIncome(id, selectedWeek, 2, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (income.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["adid"] = null;
                            ViewState["inid"] = income.PkIncomID;
                            txtEditItem.Text = income.UserTip.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    break;
                case "tuesday":
                    if (lblType.Text != "Salary" && lblType.Text != "Advance" && lblType.Text != "Tips")
                    {
                        tblUserWorkshifts uws = new tblUserWorkshifts();
                        uws.GetUserWorkshit(id, selectedWeek, 3, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (uws.RowCount > 0)
                        {
                            ViewState["uwsid"] = uws.PkUserWorkshiftID;
                            if (lblType.Text == "Bonus")
                            {
                                txtEditItem.Text = uws.Bonus.ToString();
                                lblEditItem.Text = "Bonus";
                                ViewState["editType"] = "Bonus";
                            }
                            else if (lblType.Text == "Penalty")
                            {
                                txtEditItem.Text = uws.Penalty.ToString();
                                lblEditItem.Text = "Penalty";
                                ViewState["editType"] = "Penalty";
                            }
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Advance")
                    {
                        lblEditItem.Text = "Advance";
                        tblUserAdvances userAdvances = new tblUserAdvances();
                        userAdvances.GetUserAdvance(id, selectedWeek, 3, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (userAdvances.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["inid"] = null;
                            ViewState["adid"] = userAdvances.PkUserAdvanceID;
                            txtEditItem.Text = userAdvances.UAdvance.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Tips")
                    {
                        lblEditItem.Text = "Tips";
                        tblIncome income = new tblIncome();
                        income.GetUserIncome(id, selectedWeek, 3, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (income.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["adid"] = null;
                            ViewState["inid"] = income.PkIncomID;
                            txtEditItem.Text = income.UserTip.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    break;
                case "wednesday":
                    if (lblType.Text != "Salary" && lblType.Text != "Advance" && lblType.Text != "Tips")
                    {
                        tblUserWorkshifts uws = new tblUserWorkshifts();
                        uws.GetUserWorkshit(id, selectedWeek, 4, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (uws.RowCount > 0)
                        {
                            ViewState["uwsid"] = uws.PkUserWorkshiftID;
                            if (lblType.Text == "Bonus")
                            {
                                txtEditItem.Text = uws.Bonus.ToString();
                                lblEditItem.Text = "Bonus";
                                ViewState["editType"] = "Bonus";
                            }
                            else if (lblType.Text == "Penalty")
                            {
                                txtEditItem.Text = uws.Penalty.ToString();
                                lblEditItem.Text = "Penalty";
                                ViewState["editType"] = "Penalty";
                            }
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Advance")
                    {
                        lblEditItem.Text = "Advance";
                        tblUserAdvances userAdvances = new tblUserAdvances();
                        userAdvances.GetUserAdvance(id, selectedWeek, 4, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (userAdvances.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["inid"] = null;
                            ViewState["adid"] = userAdvances.PkUserAdvanceID;
                            txtEditItem.Text = userAdvances.UAdvance.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Tips")
                    {
                        lblEditItem.Text = "Tips";
                        tblIncome income = new tblIncome();
                        income.GetUserIncome(id, selectedWeek, 4, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (income.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["adid"] = null;
                            ViewState["inid"] = income.PkIncomID;
                            txtEditItem.Text = income.UserTip.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    break;
                case "thursday":
                    if (lblType.Text != "Salary" && lblType.Text != "Advance" && lblType.Text != "Tips")
                    {
                        tblUserWorkshifts uws = new tblUserWorkshifts();
                        uws.GetUserWorkshit(id, selectedWeek, 5, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (uws.RowCount > 0)
                        {
                            ViewState["uwsid"] = uws.PkUserWorkshiftID;
                            if (lblType.Text == "Bonus")
                            {
                                txtEditItem.Text = uws.Bonus.ToString();
                                lblEditItem.Text = "Bonus";
                                ViewState["editType"] = "Bonus";
                            }
                            else if (lblType.Text == "Penalty")
                            {
                                txtEditItem.Text = uws.Penalty.ToString();
                                lblEditItem.Text = "Penalty";
                                ViewState["editType"] = "Penalty";

                            }
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Advance")
                    {
                        lblEditItem.Text = "Advance";
                        tblUserAdvances userAdvances = new tblUserAdvances();
                        userAdvances.GetUserAdvance(id, selectedWeek, 5, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (userAdvances.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["inid"] = null;
                            ViewState["adid"] = userAdvances.PkUserAdvanceID;
                            txtEditItem.Text = userAdvances.UAdvance.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Tips")
                    {
                        lblEditItem.Text = "Tips";
                        tblIncome income = new tblIncome();
                        income.GetUserIncome(id, selectedWeek, 5, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (income.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["adid"] = null;
                            ViewState["inid"] = income.PkIncomID;
                            txtEditItem.Text = income.UserTip.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    break;
                case "friday":
                    if (lblType.Text != "Salary" && lblType.Text != "Advance" && lblType.Text != "Tips")
                    {
                        tblUserWorkshifts uws = new tblUserWorkshifts();
                        uws.GetUserWorkshit(id, selectedWeek, 6, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (uws.RowCount > 0)
                        {
                            ViewState["uwsid"] = uws.PkUserWorkshiftID;
                            if (lblType.Text == "Bonus")
                            {
                                txtEditItem.Text = uws.Bonus.ToString();
                                lblEditItem.Text = "Bonus";
                                ViewState["editType"] = "Bonus";
                            }
                            else if (lblType.Text == "Penalty")
                            {
                                txtEditItem.Text = uws.Penalty.ToString();
                                lblEditItem.Text = "Penalty";
                                ViewState["editType"] = "Penalty";
                            }
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Advance")
                    {
                        lblEditItem.Text = "Advance";
                        tblUserAdvances userAdvances = new tblUserAdvances();
                        userAdvances.GetUserAdvance(id, selectedWeek, 6, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (userAdvances.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["inid"] = null;
                            ViewState["adid"] = userAdvances.PkUserAdvanceID;
                            txtEditItem.Text = userAdvances.UAdvance.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Tips")
                    {
                        lblEditItem.Text = "Tips";
                        tblIncome income = new tblIncome();
                        income.GetUserIncome(id, selectedWeek, 6, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (income.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["adid"] = null;
                            ViewState["inid"] = income.PkIncomID;
                            txtEditItem.Text = income.UserTip.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    break;
                case "saturday":
                    if (lblType.Text != "Salary" && lblType.Text != "Advance" && lblType.Text != "Tips")
                    {
                        tblUserWorkshifts uws = new tblUserWorkshifts();
                        uws.GetUserWorkshit(id, selectedWeek, 7, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (uws.RowCount > 0)
                        {
                            ViewState["uwsid"] = uws.PkUserWorkshiftID;
                            if (lblType.Text == "Bonus")
                            {
                                txtEditItem.Text = uws.Bonus.ToString();
                                lblEditItem.Text = "Bonus";
                                ViewState["editType"] = "Bonus";
                            }
                            else if (lblType.Text == "Penalty")
                            {
                                txtEditItem.Text = uws.Penalty.ToString();
                                lblEditItem.Text = "Penalty";
                                ViewState["editType"] = "Penalty";
                            }
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Advance")
                    {
                        lblEditItem.Text = "Advance";
                        tblUserAdvances userAdvances = new tblUserAdvances();
                        userAdvances.GetUserAdvance(id, selectedWeek, 7, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (userAdvances.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["inid"] = null;
                            ViewState["adid"] = userAdvances.PkUserAdvanceID;
                            txtEditItem.Text = userAdvances.UAdvance.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    else if (lblType.Text == "Tips")
                    {
                        lblEditItem.Text = "Tips";
                        tblIncome income = new tblIncome();
                        income.GetUserIncome(id, selectedWeek, 7, Convert.ToDateTime(hidWeekStart.Value), Convert.ToDateTime(hidWeekEnd.Value));
                        if (income.RowCount > 0)
                        {
                            ViewState["uwsid"] = null;
                            ViewState["adid"] = null;
                            ViewState["inid"] = income.PkIncomID;
                            txtEditItem.Text = income.UserTip.ToString();
                            trEdit.Visible = true;
                        }
                        else
                        {
                            string message = "Workshift  " + e.CommandName.ToUpper() + " " + Convert.ToDateTime(hidWeekStart.Value).ToString("dd/MM") + " is not created for " + lnkName.Text;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error", "$(function(){userAbsent('" + message + "');})", true);
                        }
                    }
                    break;
            }
            //UpdatePanel1.Update();
        }
    }

    protected void imgBtnSaveEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["uwsid"] != null)
        {
            tblUserWorkshifts uws = new tblUserWorkshifts();
            uws.LoadByPrimaryKey(Convert.ToInt32(ViewState["uwsid"]));
            if (uws.RowCount > 0)
            {
                if (ViewState["editType"].ToString() == "Bonus")
                    uws.Bonus = commonMethods.ChangeToUS(txtEditItem.Text);
                else if (ViewState["editType"].ToString() == "Penalty")
                    uws.Penalty = commonMethods.ChangeToUS(txtEditItem.Text);
                uws.DModifiedDate = DateTime.Now;
                uws.Save();
                trEdit.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "save", "$(function(){RecordSaved();})", true);
            }
        }
        else if (ViewState["adid"] != null)
        {
            tblUserAdvances uad = new tblUserAdvances();
            uad.LoadByPrimaryKey(Convert.ToInt32(ViewState["adid"]));
            if (uad.RowCount > 0)
            {
                uad.UAdvance = commonMethods.ChangeToUS(txtEditItem.Text);
                uad.DModifiedDate = DateTime.Now;
                uad.Save();
                trEdit.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "save", "$(function(){RecordSaved();})", true);
            }
        }
        else if (ViewState["inid"] != null)
        {
            tblIncome income = new tblIncome();
            income.LoadByPrimaryKey(Convert.ToInt32(ViewState["inid"]));
            if (income.RowCount > 0)
            {
                income.UserTip = commonMethods.ChangeToUS(txtEditItem.Text);
                income.Save();
                trEdit.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "save", "$(function(){RecordSaved();})", true);
            }
        }
        GetPaymentForSingleUser(p_userid, p_weeknumber, p_weelStart, p_weekEnd, p_name);
        UpdatePanel1.Update();

    }
    protected void imgBtnCancelEdit_Click(object sender, ImageClickEventArgs e)
    {
        trEdit.Visible = false;
    }


    protected void grdPaymentForSingleUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            Label lblSundayHeader = e.Row.FindControl("lblSundayHeader") as Label;
            Label lblMondayHeader = e.Row.FindControl("lblMondayHeader") as Label;
            Label lblTuesdayHeader = e.Row.FindControl("lblTuesdayHeader") as Label;
            Label lblWednesdayHeader = e.Row.FindControl("lblWednesdayHeader") as Label;
            Label lblThursdayHeader = e.Row.FindControl("lblThursdayHeader") as Label;
            Label lblFridayHeader = e.Row.FindControl("lblFridayHeader") as Label;
            Label lblSaturdayHeader = e.Row.FindControl("lblSaturdayHeader") as Label;


            lblSundayHeader.Text = "Sunday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1).ToString("dd/MM");
            lblMondayHeader.Text = "Monday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(0).ToString("dd/MM");
            lblTuesdayHeader.Text = "Tuesday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(1).ToString("dd/MM");
            lblWednesdayHeader.Text = "Wednesday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(2).ToString("dd/MM");
            lblThursdayHeader.Text = "Thursday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(3).ToString("dd/MM");
            lblFridayHeader.Text = "Friday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(4).ToString("dd/MM");
            lblSaturdayHeader.Text = "Saturday <br/>" + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5).ToString("dd/MM");
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblSundaySalary = e.Row.FindControl("lblSundaySalary") as Label;
                Label lblMondaySalary = e.Row.FindControl("lblMondaySalary") as Label;
                Label lblTuesdaySalary = e.Row.FindControl("lblTuesdaySalary") as Label;
                Label lblWednesdaySalary = e.Row.FindControl("lblWednesdaySalary") as Label;
                Label lblThursdaySalary = e.Row.FindControl("lblThursdaySalary") as Label;
                Label lblFridaySalary = e.Row.FindControl("lblFridaySalary") as Label;
                Label lblSaturdaySalary = e.Row.FindControl("lblSaturdaySalary") as Label;


                Label lblWeekSubtotal = e.Row.FindControl("lblWeekSubtotal") as Label;

                lblWeekSubtotal.Text = (Convert.ToDouble(lblSundaySalary.Text) +
                                        Convert.ToDouble(lblMondaySalary.Text) +
                                        Convert.ToDouble(lblTuesdaySalary.Text) +
                                        Convert.ToDouble(lblWednesdaySalary.Text) +
                                        Convert.ToDouble(lblThursdaySalary.Text) +
                                        Convert.ToDouble(lblFridaySalary.Text) +
                                        Convert.ToDouble(lblSaturdaySalary.Text)).ToString("N");
                total += Convert.ToDouble(lblWeekSubtotal.Text);
                lblWeekSubtotal.Text = lblWeekSubtotal.Text + " €";
                lblSundaySalary.Text = lblSundaySalary.Text + " €";
                lblMondaySalary.Text = lblMondaySalary.Text + " €";
                lblTuesdaySalary.Text = lblTuesdaySalary.Text + " €";
                lblWednesdaySalary.Text = lblWednesdaySalary.Text + " €";
                lblThursdaySalary.Text = lblThursdaySalary.Text + " €";
                lblFridaySalary.Text = lblFridaySalary.Text + " €";
                lblSaturdaySalary.Text = lblSaturdaySalary.Text + " €";

                LinkButton lnkSunday = e.Row.FindControl("lnkSunday") as LinkButton;
                LinkButton lnkMonday = e.Row.FindControl("lnkMonday") as LinkButton;
                LinkButton lnkTuesday = e.Row.FindControl("lnkTuesday") as LinkButton;
                LinkButton lnkWednesday = e.Row.FindControl("lnkWednesday") as LinkButton;
                LinkButton lnkThursday = e.Row.FindControl("lnkThursday") as LinkButton;
                LinkButton lnkFriday = e.Row.FindControl("lnkFriday") as LinkButton;
                LinkButton lnkSaturday = e.Row.FindControl("lnkSaturday") as LinkButton;

                lnkSunday.Text = lnkSunday.Text + " €";
                lnkMonday.Text = lnkMonday.Text + " €";
                lnkTuesday.Text = lnkTuesday.Text + " €";
                lnkWednesday.Text = lnkWednesday.Text + " €";
                lnkThursday.Text = lnkThursday.Text + " €";
                lnkFriday.Text = lnkFriday.Text + " €";
                lnkSaturday.Text = lnkSaturday.Text + " €";

                if (checkStatus)
                {
                    lnkSunday.Enabled = false;
                    lnkSunday.Style.Add("cursor", "pointer");
                    lnkSunday.Style.Add("text-decoration", "none");
                    lnkSunday.Attributes.Add("OnClientClick", "return false;");

                    lnkMonday.Enabled = false;
                    lnkMonday.Style.Add("cursor", "pointer");
                    lnkMonday.Style.Add("text-decoration", "none");
                    lnkMonday.Attributes.Add("OnClientClick", "return false;");

                    lnkTuesday.Enabled = false;
                    lnkTuesday.Style.Add("cursor", "pointer");
                    lnkTuesday.Style.Add("text-decoration", "none");
                    lnkTuesday.Attributes.Add("OnClientClick", "return false;");

                    lnkWednesday.Enabled = false;
                    lnkWednesday.Style.Add("cursor", "pointer");
                    lnkWednesday.Style.Add("text-decoration", "none");
                    lnkWednesday.Attributes.Add("OnClientClick", "return false;");

                    lnkThursday.Enabled = false;
                    lnkThursday.Style.Add("cursor", "pointer");
                    lnkThursday.Style.Add("text-decoration", "none");
                    lnkThursday.Attributes.Add("OnClientClick", "return false;");

                    lnkFriday.Enabled = false;
                    lnkFriday.Style.Add("cursor", "pointer");
                    lnkFriday.Style.Add("text-decoration", "none");
                    lnkFriday.Attributes.Add("OnClientClick", "return false;");

                    lnkSaturday.Enabled = false;
                    lnkSaturday.Style.Add("cursor", "pointer");
                    lnkSaturday.Style.Add("text-decoration", "none");
                    lnkSaturday.Attributes.Add("OnClientClick", "return false;");
                }

                //if (Convert.ToBoolean(DataBinder.GetPropertyValue(e.Row.DataItem, "SalaryStatus")))
                //{

                //}
                //else if (Convert.ToBoolean(DataBinder.GetPropertyValue(e.Row.DataItem, "SalaryStatus")))
                //{

                //}
            }
            catch (Exception ex)
            { }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalPayment = e.Row.FindControl("lblTotalPayment") as Label;
            lblTotalPayment.Text = commonMethods.ChangetToUK(total.ToString("N")) + " €";
        }
    }

    #endregion

    #region Receipts
    protected void lnkPrintAll_Click(object sender, EventArgs e)
    {
        string url = "PrintReceipts.aspx?week=" + selectedWeek + "&year=" + Year + "&startDate=" + SelectedWeekStart.ToString() + "&endDate=" + SelectedWeekEnd + "&all=1&usid=0";
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "print", "$(function(){printreport('" + url + "');})", true);
    }
    protected void lnkPrint_Click(object sender, EventArgs e)
    {
        string url = "PrintReceipts.aspx?week=" + selectedWeek + "&year=" + Year + "&startDate=" + SelectedWeekStart.ToString() + "&endDate=" + SelectedWeekEnd + "&all=0&usid=" + ddlSelectReceipt.SelectedValue;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "print", "$(function(){printreport('" + url + "');})", true);
    }

    #endregion

}
