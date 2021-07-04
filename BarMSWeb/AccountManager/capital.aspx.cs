using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class AccountManager_capital : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    static double startupCapital = 0;
    static double weeklyTurnover = 0;
    static double transactions = 0;
    static double userAdvances = 0;
    static double operatingCapital = 0;
    static double smallchangeVal = 0;
    static double totaladvancePopup = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
            }
            else
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }
            if (!IsPostBack)
            {
                resetTotals();
                LoadDefaultDates();
                GetStartupCapital();
                GetTotalTurnover();
                getDepartmentAdmin();
                loadTransactionTypes();
                GetAllTransactions();
                GetAllAdvances();
                GetAllSmallChange();
                GetOperatingCapital();
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "tra", "$(function(){ ShowDataPicker();filterAmount();});", true);
        }
        catch (Exception ex)
        { }
    }

    private void LoadDefaultDates()
    {
        try
        {
            txtFromDate.Text = "01/01/" + DateTime.Now.Year;
            txtTillDate.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
            lblDatePeriod.Text = "From " + txtFromDate.Text + " till " + txtTillDate.Text;
            lblPopupDate.Text = "From " + txtFromDate.Text + " till " + txtTillDate.Text;
        }
        catch (Exception ex)
        { }
    }

    private void GetOperatingCapital()
    {
        try
        {
            operatingCapital = (startupCapital + weeklyTurnover) - transactions;
            txtTotalOperatingCapital.Text = commonMethods.ChangetToUK(operatingCapital.ToString("N")) + " €";
            txtOperatingCapical.Text = commonMethods.ChangetToUK(operatingCapital.ToString("N")) + " €";
            txtRealOperatingCapital.Text = commonMethods.ChangetToUK((operatingCapital - (smallchangeVal + userAdvances)).ToString("N")) + " €";
        }
        catch (Exception ex)
        { }
    }

    #region Top View (Data Selectors & Totals)

    protected void imgBtnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            resetTotals();
            GetStartupCapital();
            GetTotalTurnover();
            getDepartmentAdmin();
            loadTransactionTypes();
            GetAllTransactions();
            GetAllAdvances();
            GetAllSmallChange();
            GetOperatingCapital();
        }
        catch (Exception ex)
        { }
    }
    private void resetTotals()
    {
        try
        {
            startupCapital = 0;
            weeklyTurnover = 0;
            transactions = 0;
            userAdvances = 0;
            operatingCapital = 0;
            smallchangeVal = 0;
            totaladvancePopup = 0;
        }
        catch (Exception ex)
        { }
    }

    #endregion

    #region Startup Total

    private void GetStartupCapital()
    {
        try
        {
            tblStartupCapital stCapital = new tblStartupCapital();

            string[] start_D = txtFromDate.Text.Split('/'); // start Date
            string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

            string[] end_D = txtTillDate.Text.Split('/'); // end date
            string end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];

            lblDatePeriod.Text = "From " + txtFromDate.Text + " till " + txtTillDate.Text;
            lblPopupDate.Text = "From " + txtFromDate.Text + " till " + txtTillDate.Text;

            stCapital.getAllStartupCapital_ByDatePeriod(Convert.ToDateTime(start), Convert.ToDateTime(end));
            grdStartupCapital.DataSource = stCapital.DefaultView;
            grdStartupCapital.DataBind();

            txtTotalStartupCapital.Text = commonMethods.ChangetToUK(startupCapital.ToString()) + " €";
            txtTotalStartup.Text = commonMethods.ChangetToUK(startupCapital.ToString()) + " €";

            operatingCapital += startupCapital;
        }
        catch (Exception ex)
        { }
    }
    protected void grdStartupCapital_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            try
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                Label lblNotes = e.Row.FindControl("lblNotes") as Label;
                Label lblAmount = e.Row.FindControl("lblAmount") as Label;

                if (dr["note"].ToString() != "")
                {
                    if (dr["note"].ToString().Length > 30)
                    {
                        string note = dr["note"].ToString().Substring(0, 30);
                        lblNotes.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + note + "');");
                        lblNotes.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow();");
                    }
                }

                DateTime dt = Convert.ToDateTime(dr["dmodifieddate"].ToString());
                lblDate.Text = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
                lblAmount.Text = lblAmount.Text + " €";

                startupCapital += Convert.ToDouble(dr["Amount"].ToString());
            }
            catch (Exception ex)
            { }
        }
    }

    #endregion

    #region Weekly Turnover

    private void GetTotalTurnover()
    {
        try
        {
            string[] start_D = txtFromDate.Text.Split('/'); // start Date
            string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

            string[] end_D = txtTillDate.Text.Split('/'); // end date
            string end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];

            int startWeek = commonMethods.GetWeekNumber_New(Convert.ToDateTime(start));
            int endWeek = commonMethods.GetWeekNumber_New(Convert.ToDateTime(end));
            int startYear = Convert.ToDateTime(start).Year;
            int endYear = Convert.ToDateTime(end).Year;
            weeklyTurnover = 0;

            tblIncome income = new tblIncome();
            income.GetWeekly_Income_Salaries_for_turnover(Convert.ToDateTime(start), Convert.ToDateTime(end));
            grdTotalTurnover.DataSource = income.DefaultView;
            grdTotalTurnover.DataBind();


            txtWeeklyTurnover.Text = commonMethods.ChangetToUK(weeklyTurnover.ToString("N")) + " €";
            txtTotalTurnover.Text = commonMethods.ChangetToUK(weeklyTurnover.ToString("N")) + " €";
        }
        catch (Exception ex)
        { }

    }

    protected void grdTotalTurnover_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                Label lblTotalIncome = e.Row.FindControl("lblTotalIncome") as Label;
                Label lblTotalSalaries = e.Row.FindControl("lblTotalSalaries") as Label;
                Label lblTotalExpenses = e.Row.FindControl("lblTotalExpenses") as Label;
                Label lblWeeklyTurnover = e.Row.FindControl("lblWeeklyTurnover") as Label;

                string weekStart = string.Empty;
                string weekEnd = string.Empty;

                DataRowView drv = (DataRowView)e.Row.DataItem;
                weekStart = Convert.ToDateTime(drv["dWeekStartDate"]).ToString("dd") + "/" + Convert.ToDateTime(drv["dWeekStartDate"]).ToString("MM");
                weekEnd = Convert.ToDateTime(drv["dWeekEndDate"]).ToString("dd") + "/" + Convert.ToDateTime(drv["dWeekEndDate"]).ToString("MM");
                #region other income
                /*
                tblIncome otherIncome = new tblIncome();
                otherIncome.GetOtherIncome_for_turnover(Convert.ToInt32(lblDate.Text), Convert.ToDateTime(drv["dWeekStartDate"]).Year, Convert.ToDateTime(drv["dWeekEndDate"]).Year);
                if (otherIncome.RowCount > 0)
                {
                    lblTotalIncome.Text = (Convert.ToDouble(lblTotalIncome.Text) + Convert.ToDouble(otherIncome.GetColumn("OtherIncome").ToString())).ToString("N");
                }
                 */
                try
                {
                    lblTotalIncome.Text = (Convert.ToDouble(lblTotalIncome.Text) + Convert.ToDouble(drv["OtherIncome"].ToString())).ToString("N");
                }
                catch (FormatException ex)
                {
                    lblTotalIncome.Text = "0";
                    lblTotalIncome.Text = (Convert.ToDouble(lblTotalIncome.Text) + Convert.ToDouble(drv["OtherIncome"].ToString())).ToString("N");
                }
                #endregion


                #region Weekly Turnover
                double salaries = 0.0;
                double expense = 0.0;
                double income = 0.0;
                double wTurnover = 0.0;



                #endregion



                #region expenses
                tblExpanses expenses = new tblExpanses();
                expenses.GetAllExpensesByWeek(Convert.ToInt32(lblDate.Text));
                if (expenses.RowCount > 0)
                {
                    if (expenses.GetColumn("expense").ToString() != "")
                    {
                        lblTotalExpenses.Text = commonMethods.ChangetToUK(Convert.ToDouble(expenses.GetColumn("expense").ToString()).ToString("N")) + " €";

                        expense = Convert.ToDouble(expenses.GetColumn("expense").ToString());

                    }
                    else
                        lblTotalExpenses.Text = "00,00" + " €";
                }
                else
                {
                    lblTotalExpenses.Text = "00,00" + " €";
                }
                #endregion



                if (lblTotalIncome.Text == "" || lblTotalIncome.Text == "0")
                    lblTotalIncome.Text = "00,00 €";
                else
                {
                    income = Convert.ToDouble(lblTotalIncome.Text);
                    lblTotalIncome.Text = commonMethods.ChangetToUK(lblTotalIncome.Text) + " €";
                }

                if (lblTotalSalaries.Text == "" || lblTotalSalaries.Text == "0")
                    lblTotalSalaries.Text = "00,00 €";
                else
                {
                    salaries = Convert.ToDouble(lblTotalSalaries.Text);
                    lblTotalSalaries.Text = commonMethods.ChangetToUK(lblTotalSalaries.Text) + " €";
                }

                wTurnover = income - (salaries + expense);
                weeklyTurnover += wTurnover;
                lblWeeklyTurnover.Text = commonMethods.ChangetToUK(wTurnover.ToString("N")) + " €";

                lblDate.Text = "Week " + lblDate.Text + "( " + weekStart + " - " + weekEnd + ")";
            }
            catch (Exception ex)
            { }
        }
    }
    #endregion

    #region Transactions

    private void GetAllTransactions()
    {
        try
        {
            string[] start_D = txtFromDate.Text.Split('/'); // start Date
            string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

            string[] end_D = txtTillDate.Text.Split('/'); // end date
            string end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];

            transactions = 0;

            tblTransactions tra = new tblTransactions();
            tra.getAllTransactions(Convert.ToDateTime(start), Convert.ToDateTime(end));


            grdTransactions.DataSource = tra.DefaultView;
            grdTransactions.DataBind();

            txtTotalTrasactions.Text = commonMethods.ChangetToUK(transactions.ToString("N")) + " €";
            txtTotalTransactions.Text = commonMethods.ChangetToUK(transactions.ToString("N")) + " €";
        }
        catch (Exception ex)
        { }
    }

    private void getDepartmentAdmin()
    {
        try
        {
            ddlDepartmentAdmin.Items.Clear();
            tblUsers u = new tblUsers();
            u.getDepartmentAdmin(DepartmentID);
            if (u.RowCount > 0)
            {
                commonMethods.FillDropDownList(ddlDepartmentAdmin, u.DefaultView, "FullName", "pkuserid");
                if (u.RowCount > 1)
                    ddlDepartmentAdmin.Items.Insert(0, new ListItem("Pick Department Admin", "0"));
            }
        }
        catch (Exception ex)
        { }
    }
    private void loadTransactionTypes()
    {
        try
        {
            ddlTransactions.Items.Clear();
            ddlTransactions.Items.Insert(0, new ListItem("Transaction Type", "0"));
            ddlTransactions.Items.Insert(1, new ListItem("Bank Transaction", "1"));
            ddlTransactions.Items.Insert(2, new ListItem("Personal Delivery", "2"));
            ddlTransactions.Items.Insert(3, new ListItem("Western Union", "3"));
        }
        catch (Exception ex)
        { }
    }

    protected void imgBtnAddTransaction_Click(object sender, EventArgs e)
    {
        Add();


    }
    protected void imgBtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Save();
            tblTransactions tra = new tblTransactions();
            tra.AddNew();
            tra.FkAccountManagerID = UserID;
            tra.FkDepartmentAdminID = Convert.ToInt32(ddlDepartmentAdmin.SelectedValue);
            tra.TransactionType = ddlTransactions.SelectedItem.Text;
            tra.Amount = commonMethods.ChangeToUS(txtTransactionAmount.Text);
            tra.Received = false;
            tra.Notes = txtComment.Text;
            string[] date = txtDateTransaction.Text.Split('/');
            tra.DModifiedDate = Convert.ToDateTime(date[1].ToString() + "/" + date[0].ToString() + "/" + date[2].ToString());
            tra.DCreatedDate = DateTime.Now;
            tra.Save();

            #region email
            Emailing email = new Emailing();

            tblUserEmails ue = new tblUserEmails();
            ue.LoadUserEmails(UserID);
            if (ue.RowCount > 0)
            {
                email.P_FromAddress = ue.SEmail;
            }
            ue.FlushData();
            ue.LoadUserEmails(Convert.ToInt32(ddlDepartmentAdmin.SelectedValue));
            if (ue.RowCount > 0)
            {
                email.P_ToAddress = ue.SEmail;
            }
            email.P_Email_Subject = "New Transaction";
            email.P_Message_Body = txtComment.Text;
            //email.P_Message_Body = "<a href='www.google.com' style='font-size:15px;'>Click me</a>";
            email.Send_Email();




            #endregion

            #region Internal Message

            tblUserInBox userIn = new tblUserInBox();
            // For Current Manger
            userIn.AddNew();
            userIn.FkFromUserID = UserID;
            userIn.FkToUserID = Convert.ToInt32(ddlDepartmentAdmin.SelectedValue);
            userIn.SSubject = "New Transaction";
            userIn.SMessage = txtComment.Text;
            userIn.DReceivedDate = DateTime.Now;
            userIn.BIsread = false;
            userIn.Save();
            userIn.FlushData();


            tblUserSentBox userOut = new tblUserSentBox();

            //For Current Manager
            userOut.AddNew();
            userOut.FkFromUserID = UserID;
            userOut.FkToUserID = Convert.ToInt32(ddlDepartmentAdmin.SelectedValue);
            userOut.SSubject = "New Transaction";
            userOut.SMessage = txtComment.Text;
            userOut.DSentDate = DateTime.Now;
            userOut.Save();
            userOut.FlushData();

            #endregion

            txtTransactionAmount.Text = "";
            txtComment.Text = "";
            GetAllTransactions();
            getDepartmentAdmin();
            loadTransactionTypes();
            upnlTransaction.Update();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saveTran", "$(function(){ TransactionSaved();});", true);
        }
        catch (Exception ex)
        { }

    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        Cancel();
    }

    protected void grdTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                HtmlImage img = e.Row.FindControl("img") as HtmlImage;
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                Label lblAmount = e.Row.FindControl("lblAmount") as Label;

                DataRowView drv = (DataRowView)e.Row.DataItem;
                if (Convert.ToBoolean(drv["Received"].ToString()))
                    img.Src = "../Images/activate_icon.gif";
                lblDate.Text = Convert.ToDateTime(lblDate.Text).ToString("dd") + "/" + Convert.ToDateTime(lblDate.Text).ToString("MM") + "/" + Convert.ToDateTime(lblDate.Text).Year;
                transactions += Convert.ToDouble(lblAmount.Text);
                lblAmount.Text = commonMethods.ChangetToUK(Convert.ToDouble(lblAmount.Text).ToString("N")) + " €";
            }
            catch (Exception ex)
            { }

        }
    }

    protected void grdTransactions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandArgument != null)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                switch (e.CommandName.ToLower())
                {
                    case "edt":

                        break;
                }
            }
        }
        catch (Exception ex)
        { }
    }

    private void Add()
    {
        imgBtnAddTransaction.Visible = false;
        imgBtnSave.Visible = true;
        imgBtnCancel.Visible = true;
        trContent.Visible = true;
        trLineBefore.Visible = false;
        trLineAfter.Visible = true;
        txtDateTransaction.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
    }
    private void Save()
    {
        //imgBtnAddTransaction.Visible = false;

        //imgBtnSave.Visible = true;
        //imgBtnCancel.Visible = true;
        //trContent.Visible = true;
        //trLineBefore.Visible = false;
        //trLineAfter.Visible = true;
    }
    private void Edit()
    {
        imgBtnAddTransaction.Visible = false;

        imgBtnSave.Visible = false;
        imgBtnCancel.Visible = true;
        trContent.Visible = true;
        trLineBefore.Visible = true;
        trLineAfter.Visible = false;
    }
    private void Cancel()
    {
        imgBtnAddTransaction.Visible = true;

        imgBtnSave.Visible = false;
        imgBtnCancel.Visible = false;
        trContent.Visible = false;
        trLineBefore.Visible = true;
        trLineAfter.Visible = false;
    }
    #endregion

    #region Operating Capital

    #region Advances
    private void GetAllAdvances()
    {
        try
        {
            string[] start_D = txtFromDate.Text.Split('/'); // start Date
            string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

            string[] end_D = txtTillDate.Text.Split('/'); // end date
            string end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];

            int startWeek = commonMethods.GetWeekNumber_New(Convert.ToDateTime(start));
            int endWeek = commonMethods.GetWeekNumber_New(Convert.ToDateTime(end));
            int startYear = Convert.ToDateTime(start).Year;
            int endYear = Convert.ToDateTime(end).Year;
            userAdvances = 0;

            tblUserAdvances advances = new tblUserAdvances();
            advances.getUserAdvances_for_OperatingCapital(Convert.ToDateTime(start), Convert.ToDateTime(end));
            grdAdvance.DataSource = advances.DefaultView;
            grdAdvance.DataBind();

            txtTotalAdvances.Text = commonMethods.ChangetToUK(userAdvances.ToString("N")) + " €";
            txtAdvances.Text = commonMethods.ChangetToUK(userAdvances.ToString("N")) + " €";
        }
        catch (Exception ex)
        { }
    }
    protected void grdAdvance_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                LinkButton lnkAmount = e.Row.FindControl("lnkAmount") as LinkButton;

                userAdvances += Convert.ToDouble(drv["advance"].ToString());
                lnkAmount.Text = commonMethods.ChangetToUK(Convert.ToDouble(drv["advance"].ToString()).ToString("N")) + " €";
            }
            catch (FormatException ex)
            { }
            catch (Exception ex)
            { }
        }

    }
    protected void grdAdvance_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "edt":
                    tblUsers u = new tblUsers();
                    u.LoadByPrimaryKey(id);
                    if (u.RowCount > 0)
                    {
                        lblPopAdvanceTitle.Text = u.SFirstName + " " + u.SLastName + " Advances";
                    }

                    string[] start_D = txtFromDate.Text.Split('/'); // start Date
                    string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

                    string[] end_D = txtTillDate.Text.Split('/'); // end date
                    string end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];

                    int startWeek = commonMethods.GetWeekNumber_New(Convert.ToDateTime(start));
                    int endWeek = commonMethods.GetWeekNumber_New(Convert.ToDateTime(end));
                    int startYear = Convert.ToDateTime(start).Year;
                    int endYear = Convert.ToDateTime(end).Year;

                    tblUserAdvances ad = new tblUserAdvances();
                    ad.getUserAdvances_for_Popup(Convert.ToDateTime(start), Convert.ToDateTime(end), id);
                    if (ad.RowCount > 0)
                    {
                        grdAdvancepopup.DataSource = ad.DefaultView;
                        grdAdvancepopup.DataBind();
                        ModalPopupExtender1.Show();
                        txtTotalAdvancesPopup.Text = commonMethods.ChangetToUK(totaladvancePopup.ToString("N")) + " €";
                    }

                    break;
            }


        }
    }
    #endregion

    #region Small Change
    private void GetAllSmallChange()
    {
        try
        {
            string[] start_D = txtFromDate.Text.Split('/'); // start Date
            string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

            string[] end_D = txtTillDate.Text.Split('/'); // end date
            string end = end_D[1] + "/" + end_D[0] + "/" + end_D[2];

            int startWeek = commonMethods.GetWeekNumber_New(Convert.ToDateTime(start));
            int endWeek = commonMethods.GetWeekNumber_New(Convert.ToDateTime(end));
            int startYear = Convert.ToDateTime(start).Year;
            int endYear = Convert.ToDateTime(end).Year;
            smallchangeVal = 0;

            tblSmallChange sml = new tblSmallChange();
            sml.GetSmallChanges(Convert.ToDateTime(start), Convert.ToDateTime(end));
            grdSmallChange.DataSource = sml.DefaultView;
            grdSmallChange.DataBind();

            txtSmallChangeTab.Text = commonMethods.ChangetToUK(smallchangeVal.ToString()) + " €";
            txtSmallChange.Text = commonMethods.ChangetToUK(smallchangeVal.ToString()) + " €";
        }
        catch (Exception ex)
        { }

    }

    protected void imgBtnAddSmallChange_Click(object sender, EventArgs e)
    {
        AddChange();

    }
    protected void imgBtnSaveChange_Click(object sender, EventArgs e)
    {
        try
        {
            tblSmallChange smallChange = new tblSmallChange();
            smallChange.AddNew();
            smallChange.Amount = commonMethods.ChangeToUS(txtSmallChangeAmount.Text);
            smallChange.Notes = txtNoteSmallChange.Text;
            smallChange.DModifiedDate = Convert.ToDateTime(Convert.ToDateTime(txtDateSmallChange.Text).Month + "/" + Convert.ToDateTime(txtDateSmallChange.Text).Day + "/" + Convert.ToDateTime(txtDateSmallChange.Text).Year);
            smallChange.DCreatedDate = DateTime.Now;
            smallChange.FkAccountManagerID = UserID;
            smallChange.Save();
            GetAllSmallChange();
            GetOperatingCapital();
            upnlOperatingCapital.Update();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "saveSmall", "$(function(){ SmallChangeSaved();});", true);
        }
        catch (Exception ex)
        { }

    }
    protected void imgBtnEditChange_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["smallChangeid"] != null)
            {
                tblSmallChange sml = new tblSmallChange();
                sml.LoadByPrimaryKey(Convert.ToInt32(ViewState["smallChangeid"]));
                if (sml.RowCount > 0)
                {
                    sml.Amount = commonMethods.ChangeToUS(txtSmallChangeAmount.Text);
                    sml.Notes = txtNoteSmallChange.Text;
                    sml.DModifiedDate = Convert.ToDateTime(Convert.ToDateTime(txtDateSmallChange.Text).Month + "/" + Convert.ToDateTime(txtDateSmallChange.Text).Day + "/" + Convert.ToDateTime(txtDateSmallChange.Text).Year);
                    sml.Save();
                    GetAllSmallChange();
                    GetOperatingCapital();
                    CancelChange();
                    upnlOperatingCapital.Update();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "savesmall", "$(function(){ SmallChangeSaved();});", true);
                }
            }
        }
        catch (Exception ex)
        { }
    }
    protected void imgBtnCancelChange_Click(object sender, EventArgs e)
    {
        CancelChange();
    }

    protected void grdSmallChange_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblAmount = e.Row.FindControl("lblAmount") as Label;
                Label lblDate = e.Row.FindControl("lblDate") as Label;

                if (lblAmount != null)
                {
                    if (lblAmount.Text != "" && lblAmount.Text != "0")
                    {
                        smallchangeVal += commonMethods.ChangeToUS(lblAmount.Text);
                        lblAmount.Text = lblAmount.Text + " €";
                    }
                }

                lblDate.Text = Convert.ToDateTime(lblDate.Text).ToString("dd") + "/" + Convert.ToDateTime(lblDate.Text).ToString("MM") + "/" + Convert.ToDateTime(lblDate.Text).Year;
            }
            catch (Exception ex)
            { }
        }
    }
    protected void grdSmallChange_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument != null)
            {
                int id = Convert.ToInt32(e.CommandArgument);
                switch (e.CommandName.ToLower())
                {
                    case "edt":
                        tblSmallChange smallChange = new tblSmallChange();
                        smallChange.LoadByPrimaryKey(id);
                        if (smallChange.RowCount > 0)
                        {
                            txtDateSmallChange.Text = "";
                            txtSmallChangeAmount.Text = "";
                            txtNoteSmallChange.Text = "";
                            ViewState["smallChangeid"] = smallChange.PkSmallChangeID;
                            txtDateSmallChange.Text = smallChange.DModifiedDate.Day + "/" + smallChange.DModifiedDate.Month + "/" + smallChange.DModifiedDate.Year;
                            txtSmallChangeAmount.Text = commonMethods.ChangetToUK(smallChange.Amount.ToString("N"));
                            txtNoteSmallChange.Text = smallChange.Notes;
                            EditChange();
                        }

                        break;
                }
            }
        }
        catch (Exception ex)
        { }
    }

    private void AddChange()
    {
        imgBtnAddSmallChange.Visible = false;
        imgBtnSaveChange.Visible = true;
        imgBtnEditChange.Visible = false;
        imgBtnCancelChange.Visible = true;
        trSmallChange.Visible = true;
        trbefore.Visible = false;
        trAfter.Visible = true;
        txtDateSmallChange.Text = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
    }
    private void SaveChange()
    {
        //imgBtnAddTransaction.Visible = false;

        //imgBtnSave.Visible = true;
        //imgBtnCancel.Visible = true;
        //trContent.Visible = true;
        //trLineBefore.Visible = false;
        //trLineAfter.Visible = true;
    }
    private void EditChange()
    {
        imgBtnAddSmallChange.Visible = false;
        imgBtnEditChange.Visible = true;
        imgBtnSaveChange.Visible = false;
        imgBtnCancelChange.Visible = true;
        trSmallChange.Visible = true;
        trbefore.Visible = true;
        trAfter.Visible = false;
    }
    private void CancelChange()
    {
        imgBtnAddSmallChange.Visible = true;
        imgBtnEditChange.Visible = false;
        imgBtnSaveChange.Visible = false;
        imgBtnCancelChange.Visible = false;
        trSmallChange.Visible = false;
        trbefore.Visible = true;
        trAfter.Visible = false;
        ViewState["smallChangeid"] = null;
    }
    #endregion

    #endregion

    #region Advance popup

    private void GetAdvancePopup()
    {

    }

    protected void grdAdvancepopup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDatePopup = e.Row.FindControl("lblDatePopup") as Label;
            Label lblAmount = e.Row.FindControl("lblAmount") as Label;
            lblDatePopup.Text = Convert.ToDateTime(lblDatePopup.Text).ToString("dd") + "/" + Convert.ToDateTime(lblDatePopup.Text).ToString("MM") + "/" + Convert.ToDateTime(lblDatePopup.Text).Year;
            totaladvancePopup += Convert.ToDouble(lblAmount.Text);
            lblAmount.Text = commonMethods.ChangetToUK(Convert.ToDouble(lblAmount.Text).ToString("N")) + " €";

        }
    }

    #endregion
}
