
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using LC.Model.BMS.BLL;


public partial class Admin_AdminDailyIncome : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    int year;
    int selectedWeek;
    int day;

    static int incomeRowCount = 0;
    static int O_IncomeID = 0;
    static int O_ExpenseID = 0;

    static double incomeRowDataBound = 0.0;
    static double netIncomeRowDataBound = 0.0;
    static double tip = 0.0;
    static double penality = 0.0;
    static double bonus = 0.0;
    static double salary = 0.0;
    static double advance = 0.0;
    static double netIncome = 0.0;
    static double grandTotal = 0.0;
    static int position = 0;
    static string[] arrSpecialtyTypes;
    static string[] OrderIDs;
    int loopVariable = 0;
    static string colorArray = "#ffc9c9,#caffc9,#f8ffc9,#e6c9ff,#c9c9ff,#e8e7d0,#c3caff,#e9c3ff,#c3fff0,#ff9898";
    static string[] editableColorArray = colorArray.Split(',');

    List<string> list = new List<string>(editableColorArray);
    List<string> idsList = null;

    static double registerSubtotal = 0.0;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];

            if (user.AccessLevel != 6)
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }

            UserID = user.UserID;
            DepartmentID = user.DepartmentID;

            if (Request.QueryString.Count > 0)
            {
                workWithQueryString(Request.QueryString["dcid"].ToString());
            }
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }
        ScriptManager.GetCurrent(this.Page).RegisterAsyncPostBackControl(this.btnGO);
        if (!IsPostBack)
        {
            LoadSubtotals();
            LoadGridUsers();
            BindIncomeTypes();
            BindOtherIncomes();

            BindExpensesTypes();
            BindExpenses();
            GetRegisters();

            editableColorArray = colorArray.Split(',');
            list = new List<string>(editableColorArray);
            loadDayComment();
        }

    }
    private void workWithQueryString(string date)
    {
        loadDayComment();
        DateTime mydate = Convert.ToDateTime(date);
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;

        txtCheck.Text = "2";
        txtDay.Text = days.ToString();
        lblWeek.Text = mydate.DayOfWeek.ToString() + " " + mydate.Day.ToString() + "/" + mydate.Month.ToString();
        year = mydate.Year;
        selectedWeek = commonMethods.GetWeekNumber_New(mydate);
        day = commonMethods.getDay(mydate);

        LoadSubtotals();
        LoadGridUsers();
        BindIncomeTypes();
        BindOtherIncomes();
        BindExpensesTypes();
        BindExpenses();
        GetRegisters();

        editableColorArray = colorArray.Split(',');
        list = new List<string>(editableColorArray);
    }
    #region Income Types with ohter income

    private void BindIncomeTypes()
    {
        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }
        ddlIncomeTypes.Items.Clear();
        tblIncomTypes incomeTypes = new tblIncomTypes();
        incomeTypes.GetActiveIncomeTypes(mydate.Year, mydate.Month, mydate.Day);
        if (incomeTypes.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlIncomeTypes, incomeTypes.DefaultView, "SIncomType", "PkIncomeTypeID");
        }
        ddlIncomeTypes.Items.Insert(0, new ListItem("Select Income Type", "0"));

        //tblIncome income = new tblIncome();
        //income.CheckIncomTypeInUse(Convert.ToInt32(ddlIncomeTypes.SelectedValue), mydate.Year, mydate.Month, mydate.Day);
        //if (income.RowCount > 0)
        //{
        //    pnlOtherIncome.DefaultButton = "ImgBtnEdit";
        //    O_IncomeID = Convert.ToInt32(income.GetColumn("pkIncomID").ToString());
        //    txtIncomeAmount.Text = commonMethods.ChangetToUK(income.FIncome.ToString());
        //    imgBtnSaveOtherIncome.Visible = false;
        //    ImgBtnEdit.Visible = true;

        //    //imgBtnDelete.Visible = true;
        //}
        //else
        //{
        //    pnlOtherIncome.DefaultButton = "imgBtnSaveOtherIncome";
        //    txtIncomeAmount.Text = "";
        //    txtNoteOtherIncome.Text = "";
        //    imgBtnSaveOtherIncome.Visible = true;
        //    //imgBtnDelete.Visible = false;
        //    ImgBtnEdit.Visible = false;


        //}
        divIncomeDropDownList.Visible = true;

        trOtherIncome.Visible = false;

    }
    private void BindOtherIncomes()
    {
        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }
        tblIncome income = new tblIncome();
        income.GetOtherIncomes(mydate.Year, mydate.Month, mydate.Day);
        grdOtherIncomes.DataSource = income.DefaultView;
        grdOtherIncomes.DataBind();


        if (income.RowCount > 0)
        {
            tdOtherIncomeTotal.Visible = true;
            double otherIcomeAmount = 0.0;

            for (int i = 0; i < income.RowCount; i++)
            {
                otherIcomeAmount += Convert.ToDouble(income.GetColumn("fIncome").ToString());
                income.MoveNext();
            }
            lblOtherIncomeSubtotal.Text = commonMethods.ChangetToUK(otherIcomeAmount.ToString("N")) + " €";
        }
        else
        {

            tdOtherIncomeTotal.Visible = false;
        }
        upnlOtherIncome.Update();
    }
    protected void grdOtherIncomes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkIncome = e.Row.FindControl("lnkIncome") as LinkButton;
            lnkIncome.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('Click to change amount.')");
            lnkIncome.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            lnkIncome.Text = commonMethods.ChangetToUK(lnkIncome.Text) + " €";


            LinkButton lnkIncomeType = e.Row.FindControl("lnkIncomeType") as LinkButton;
            lnkIncomeType.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('Click to change amount.')");
            lnkIncomeType.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
        }
    }
    protected void grdOtherIncomes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int incomeid = Convert.ToInt32(e.CommandArgument);
            tblIncome income = new tblIncome();
            switch (e.CommandName.ToLower())
            {
                case "income":
                    trMessageOtherIncome.Visible = false;
                    income.FlushData();
                    income.GetOtherIncomeByID(incomeid);
                    if (income.RowCount > 0)
                    {
                        O_IncomeID = Convert.ToInt32(income.GetColumn("pkIncomID").ToString());
                        trOtherIncome.Visible = true;
                        divIncomeDropDownList.Visible = false;
                        ImgBtnEdit.Visible = true;
                        imgBtnCancel.Visible = true;
                        //imgBtnDelete.Visible = false;
                        imgBtnSaveOtherIncome.Visible = false;
                        txtIncomeAmount.Text = commonMethods.ChangetToUK(income.GetColumn("FIncome").ToString());
                        txtNoteOtherIncome.Text = income.GetColumn("IComment").ToString();
                        lblOtherIncomeTypeName.Text = income.GetColumn("sIncomType").ToString();
                        trMessageOtherIncome.Visible = false;
                        pnlOtherIncome.DefaultButton = "ImgBtnEdit";
                        trLineAfter.Visible = false;
                        trLineBefore.Visible = true;


                    }
                    // upnlOtherIncome.Update();
                    break;

                case "incometype":
                    trddlIncome.Visible = false;
                    trMessageOtherIncome.Visible = false;
                    income.FlushData();
                    income.GetOtherIncomeByID(incomeid);
                    if (income.RowCount > 0)
                    {
                        O_IncomeID = Convert.ToInt32(income.GetColumn("pkIncomID").ToString());
                        trOtherIncome.Visible = true;
                        divIncomeDropDownList.Visible = false;
                        ImgBtnEdit.Visible = true;
                        imgBtnCancel.Visible = true;
                        //imgBtnDelete.Visible = false;
                        imgBtnSaveOtherIncome.Visible = false;
                        txtIncomeAmount.Text = commonMethods.ChangetToUK(income.GetColumn("FIncome").ToString());
                        txtNoteOtherIncome.Text = income.GetColumn("IComment").ToString();
                        lblOtherIncomeTypeName.Text = income.GetColumn("sIncomType").ToString();
                        trMessageOtherIncome.Visible = false;
                        pnlOtherIncome.DefaultButton = "ImgBtnEdit";
                        trLineAfter.Visible = false;
                        trLineBefore.Visible = true;


                    }
                    // upnlOtherIncome.Update();
                    break;
                case "delincome":
                    trMessageOtherIncome.Visible = false;
                    income.FlushData();
                    income.GetOtherIncomeByID(incomeid);
                    if (income.RowCount > 0)
                    {
                        income.MarkAsDeleted();
                        income.Save();

                        BindIncomeTypes();
                        BindOtherIncomes();
                        LoadGridUsers();
                        LoadSubtotals();
                        upnlOtherIncome.Update();
                        upnlDailyIncome.Update();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
                    }
                    break;
            }
        }
    }
    protected void ddlIncomeTypes_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlIncomeTypes.SelectedValue != "0")
        {
            DateTime mydate;
            if (datepicker.Text != "")
            {
                mydate = Convert.ToDateTime(datepicker.Text);
                mydate = mydate.AddDays(-1);
            }
            else
            {
                mydate = DateTime.Now.AddDays(-1);
            }

            trOtherIncome.Visible = true;
            lblOtherIncomeTypeName.Text = ddlIncomeTypes.SelectedItem.Text;
            //tblIncome income = new tblIncome();
            //income.CheckIncomTypeInUse(Convert.ToInt32(ddlIncomeTypes.SelectedValue), mydate.Year, mydate.Month, mydate.Day);
            //if (income.RowCount > 0)
            //{
            //    O_IncomeID = Convert.ToInt32(income.GetColumn("pkIncomID").ToString());
            //    txtIncomeAmount.Text = commonMethods.ChangetToUK(income.FIncome.ToString());
            //    imgBtnSaveOtherIncome.Visible = false;
            //    ImgBtnEdit.Visible = true;
            //}
            //else
            //{
            //    O_IncomeID = 0;
            //    txtIncomeAmount.Text = "";
            //    imgBtnSaveOtherIncome.Visible = true;
            //    ImgBtnEdit.Visible = false;
            //}

            trOtherIncome.Style.Add("display", "block");

        }
        else
        {
            trOtherIncome.Visible = false;
        }
        ImgBtnEdit.Visible = false;
        upnlOtherIncome.Update();

    }

    #region Adding, Updating and Canceling Income Amount
    protected void imgAddSpecialIncome_Click(object sender, ImageClickEventArgs e)
    {
        //trLineBefore.Visible = false;
        trddlIncome.Visible = true;
        trMessageOtherIncome.Visible = false;
        trLineBefore.Visible = false;
        trLineAfter.Visible = true;
        imgBtnSaveOtherIncome.Visible = true;
        BindIncomeTypes();
        BindOtherIncomes();
        //trOtherIncome.Visible = true;
        trMessageOtherIncome.Visible = false;
        txtIncomeAmount.Text = "";
        txtNoteOtherIncome.Text = "";
    }
    protected void imgBtnSaveOtherIncome_Click(object sender, ImageClickEventArgs e)
    {
        if (txtIncomeAmount.Text != "")
        {
            DateTime mydate;
            if (datepicker.Text != "")
            {
                mydate = Convert.ToDateTime(datepicker.Text);
                mydate = mydate.AddDays(-1);
            }
            else
            {
                mydate = DateTime.Now.AddDays(-1);
            }

            tblIncome income = new tblIncome();
            income.AddNew();
            income.FIncome = commonMethods.ChangeToUS(txtIncomeAmount.Text);
            income.FkIncomeTypeID = Convert.ToInt32(ddlIncomeTypes.SelectedValue);
            income.IComment = txtNoteOtherIncome.Text;
            income.DIncomeDate = mydate;
            income.Save();

            trddlIncome.Visible = false;
            txtIncomeAmount.Text = "";
            txtNoteOtherIncome.Text = "";



            BindIncomeTypes();
            BindOtherIncomes();
            trMessageOtherIncome.Visible = true;
            lblMessageOtherIncome.Text = "Successfully Added Income Amount!";
            lblMessageOtherIncome.ForeColor = Color.Green;
            O_IncomeID = 0;
            LoadSubtotals();
            LoadGridUsers();

        }
        else
        {
            trMessageOtherIncome.Visible = true;
            lblMessageOtherIncome.Text = "Plese enter Income Amount!";
            lblMessageOtherIncome.ForeColor = Color.Red;
            pnlOtherIncome.DefaultButton = "imgBtnSaveOtherIncome";

        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
    }
    protected void ImgBtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (txtIncomeAmount.Text != "")
        {
            DateTime mydate;
            if (datepicker.Text != "")
            {
                mydate = Convert.ToDateTime(datepicker.Text);
                mydate = mydate.AddDays(-1);
            }
            else
            {
                mydate = DateTime.Now.AddDays(-1);
            }
            tblIncome income = new tblIncome();
            income.LoadByPrimaryKey(O_IncomeID);
            if (income.RowCount > 0)
            {
                income.FIncome = commonMethods.ChangeToUS(txtIncomeAmount.Text);
                income.IComment = txtNoteOtherIncome.Text;
                income.Save();
                trMessageOtherIncome.Visible = true;
                txtIncomeAmount.Text = "";
                txtNoteOtherIncome.Text = "";
                lblMessageOtherIncome.Text = "Successfully Updated Income Amount!";
                lblMessageOtherIncome.ForeColor = Color.Green;
                BindIncomeTypes();
                BindOtherIncomes();
                LoadGridUsers();
                LoadSubtotals();
                O_IncomeID = 0;
            }
        }
        else
        {
            trMessageOtherIncome.Visible = true;
            lblMessageOtherIncome.Text = "Plese enter Income Amount!";
            lblMessageOtherIncome.ForeColor = Color.Red;
            pnlOtherIncome.DefaultButton = "ImgBtnEdit";
        }


        upnlOtherIncome.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
    }
    protected void imgBtnCancel_Click(object sender, ImageClickEventArgs e)
    {
        ImgBtnEdit.Visible = false;
        imgBtnSaveOtherIncome.Visible = true;
        divIncomeDropDownList.Visible = true;
        trddlIncome.Visible = false;

        trMessageOtherIncome.Visible = false;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
        BindIncomeTypes();
        BindOtherIncomes();
        upnlOtherIncome.Update();
        upnlDailyIncome.Update();
        upnlSaveChanges.Update();
    }


    #endregion

    #region Add New Income Type
    protected void imgBtnAddTempIncome_Click(object sender, ImageClickEventArgs e)
    {
        trMessageOtherIncome.Visible = false;
        tblUsers users = new tblUsers();
        users.LoadByPrimaryKey(UserID);
        if (users.RowCount > 0)
        {
            lblFromAddress.Text = users.SFirstName + " " + users.SLastName;
        }

        users.FlushData();
        users.GetDepartmentAdminID(DepartmentID);
        if (users.RowCount > 0)
        {
            tblUsers u = new tblUsers();
            u.LoadByPrimaryKey(Convert.ToInt32(users.GetColumn("fkuserid").ToString()));
            if (u.RowCount > 0)
                lblToAddress.Text = u.SFirstName + " " + u.SLastName;
        }
        txtMessage.Text = "Please add the following New Income Type: >>>>>Add here the name<<<<<.  This is necessary for: >>>>>Add here the description<<<<<";

        trMessageOtherIncome.Visible = false;
        ModalPopupExtender1.Show();
    }
    #endregion

    #endregion

    #region Expense Types

    private void BindExpensesTypes()
    {
        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }

        ddlExpenses.Items.Clear();
        tblExpanseCategory expenseCatagory = new tblExpanseCategory();
        expenseCatagory.GetActiveExpenseCategories(mydate.Year, mydate.Month, mydate.Day);
        if (expenseCatagory.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlExpenses, expenseCatagory.DefaultView, "sExpanseCategory", "pkExpanseCategoryID");
        }
        ddlExpenses.Items.Insert(0, new ListItem("Select Expense Type", "0"));


        //tblExpanses ex = new tblExpanses();
        //ex.CheckExpenseTypeInUse(Convert.ToInt32(ddlExpenses.SelectedValue), mydate.Year, mydate.Month, mydate.Day);
        //if (ex.RowCount > 0)
        //{
        //    pnlExpenses.DefaultButton = "imgBtnEditExpense";
        //    O_ExpenseID = Convert.ToInt32(ex.GetColumn("pkExpanseID").ToString());
        //    txtEnpenseAmount.Text = commonMethods.ChangetToUK(ex.ExpanseAmount.ToString());
        //    imgBtnSaveExpense.Visible = false;
        //    imgBtnEditExpense.Visible = true;
        //}
        //else
        //{
        //    pnlExpenses.DefaultButton = "imgBtnSaveExpense";
        //    txtEnpenseAmount.Text = "";
        //    txtNoteExpense.Text = "";
        //    imgBtnSaveExpense.Visible = true;
        //    imgBtnEditExpense.Visible = false;
        //}
        divExpenseDropDownList.Visible = true;

        trExpense.Visible = false;
        trExpenseButton.Visible = false;
    }
    private void BindExpenses()
    {
        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }
        tblExpanses ex = new tblExpanses();
        ex.GetExpenses(mydate.Year, mydate.Month, mydate.Day);
        grdExpenses.DataSource = ex.DefaultView;
        grdExpenses.DataBind();

        if (ex.RowCount > 0)
        {
            tdExpenseTotal.Visible = true;
            double ExpenseAmount = 0.0;

            for (int i = 0; i < ex.RowCount; i++)
            {
                ExpenseAmount += Convert.ToDouble(ex.GetColumn("ExpanseAmount").ToString());
                ex.MoveNext();
            }
            lblExpenseSubtotal.Text = commonMethods.ChangetToUK(ExpenseAmount.ToString("N")) + " €";
        }
        else
        {
            tdExpenseTotal.Visible = false;
        }
        upnlExpenses.Update();
    }
    protected void grdExpenses_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int expenseid = Convert.ToInt32(e.CommandArgument);
            tblExpanses ex = new tblExpanses();
            switch (e.CommandName.ToLower())
            {
                case "expense":
                    trMessageExpense.Visible = false;
                    ex.FlushData();
                    ex.GetExpenseByID(expenseid);
                    if (ex.RowCount > 0)
                    {
                        O_ExpenseID = Convert.ToInt32(ex.GetColumn("PkExpanseID").ToString());
                        trExpense.Visible = true;
                        trExpenseButton.Visible = true;
                        divExpenseDropDownList.Visible = false;

                        imgBtnEditExpense.Visible = true;
                        imgBtnSaveExpense.Visible = false;
                        if (ex.GetColumn("InvoicedAmount").ToString() != null && ex.GetColumn("InvoicedAmount").ToString() != "")
                        {
                            txtInvoicedAmount.Text = commonMethods.ChangetToUK(ex.GetColumn("InvoicedAmount").ToString());
                        }
                        else
                        {
                            txtInvoicedAmount.Text = "";
                        }
                        if (ex.GetColumn("NonInvoicedAmount").ToString() != null && ex.GetColumn("NonInvoicedAmount").ToString() != "")
                        {
                            txtNonInvoicedAmount.Text = commonMethods.ChangetToUK(ex.GetColumn("NonInvoicedAmount").ToString());
                        }
                        else
                        {
                            txtNonInvoicedAmount.Text = "";
                        }
                        txtEnpenseAmount.Text = commonMethods.ChangetToUK(ex.GetColumn("ExpanseAmount").ToString());
                        txtNoteExpense.Text = ex.GetColumn("IComment").ToString();
                        lblExpenseTypeName.Text = ex.GetColumn("sExpanseCategory").ToString();
                        trMessageExpense.Visible = false;
                        pnlExpenses.DefaultButton = "imgBtnEditExpense";

                        trLineAfterExpense.Visible = false;
                        trLineBeforeExpense.Visible = true;
                    }
                    // upnlOtherIncome.Update();
                    break;
                case "expensetype":
                    trMessageExpense.Visible = false;
                    ex.FlushData();
                    ex.GetExpenseByID(expenseid);
                    if (ex.RowCount > 0)
                    {
                        O_ExpenseID = Convert.ToInt32(ex.GetColumn("PkExpanseID").ToString());
                        trExpense.Visible = true;
                        trExpenseButton.Visible = true;
                        divExpenseDropDownList.Visible = false;

                        imgBtnEditExpense.Visible = true;
                        imgBtnSaveExpense.Visible = false;
                        if (ex.GetColumn("InvoicedAmount").ToString() != null && ex.GetColumn("InvoicedAmount").ToString() != "")
                        {
                            txtInvoicedAmount.Text = commonMethods.ChangetToUK(ex.GetColumn("InvoicedAmount").ToString());
                        }
                        else
                        {
                            txtInvoicedAmount.Text = "";
                        }
                        if (ex.GetColumn("NonInvoicedAmount").ToString() != null && ex.GetColumn("NonInvoicedAmount").ToString() != "")
                        {
                            txtNonInvoicedAmount.Text = commonMethods.ChangetToUK(ex.GetColumn("NonInvoicedAmount").ToString());
                        }
                        else
                        {
                            txtNonInvoicedAmount.Text = "";
                        }
                        
                        
                        txtEnpenseAmount.Text = commonMethods.ChangetToUK(ex.GetColumn("ExpanseAmount").ToString());
                        txtNoteExpense.Text = ex.GetColumn("IComment").ToString();
                        lblExpenseTypeName.Text = ex.GetColumn("sExpanseCategory").ToString();
                        trMessageExpense.Visible = false;
                        pnlExpenses.DefaultButton = "imgBtnEditExpense";

                        trLineAfterExpense.Visible = false;
                        trLineBeforeExpense.Visible = true;
                    }
                    // upnlOtherIncome.Update();
                    break;
                case "delexpense":
                    trMessageExpense.Visible = false;
                    trddlExpense.Visible = false;
                    ex.FlushData();
                    ex.GetExpenseByID(expenseid);
                    if (ex.RowCount > 0)
                    {
                        ex.MarkAsDeleted();
                        ex.Save();

                        BindExpensesTypes();
                        BindExpenses();
                        LoadGridUsers();
                        LoadSubtotals();
                        upnlExpenses.Update();
                        upnlDailyIncome.Update();
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
                    }
                    break;
            }
        }
    }
    protected void grdExpenses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkExpense = e.Row.FindControl("lnkExpense") as LinkButton;
            lnkExpense.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('Click to change amount.')");
            lnkExpense.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            lnkExpense.Text = commonMethods.ChangetToUK(lnkExpense.Text) + " €";


            LinkButton lnkExpenseType = e.Row.FindControl("lnkExpense") as LinkButton;
            lnkExpenseType.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('Click to change amount.')");
            lnkExpenseType.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
        }
    }
    protected void ddlExpenses_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlExpenses.SelectedValue != "0")
        {
            DateTime mydate;
            if (datepicker.Text != "")
            {
                mydate = Convert.ToDateTime(datepicker.Text);
                mydate = mydate.AddDays(-1);
            }
            else
            {
                mydate = DateTime.Now.AddDays(-1);
            }
            lblExpenseTypeName.Text = ddlExpenses.SelectedItem.Text;
            //tblExpanses ex = new tblExpanses();
            //ex.CheckExpenseTypeInUse(Convert.ToInt32(ddlExpenses.SelectedValue), mydate.Year, mydate.Month, mydate.Day);

            //if (ex.RowCount > 0)
            //{
            //    O_ExpenseID = Convert.ToInt32(ex.GetColumn("PkExpanseID").ToString());
            //    txtEnpenseAmount.Text = commonMethods.ChangetToUK(ex.ExpanseAmount.ToString());
            //    imgBtnSaveExpense.Visible = false;
            //    imgBtnEditExpense.Visible = true;
            //    pnlExpenses.DefaultButton = "imgBtnEditExpense";
            //}
            //else
            //{
            //    O_ExpenseID = 0;
            //    txtEnpenseAmount.Text = "";
            //    imgBtnSaveExpense.Visible = true;
            //    imgBtnEditExpense.Visible = false;
            //    pnlExpenses.DefaultButton = "imgBtnSaveExpense";
            //}

            trExpense.Visible = true;
            trExpenseButton.Visible = true;
            txtEnpenseAmount.Text = "";
            txtNoteExpense.Text = "";
            txtNonInvoicedAmount.Text = "";
            txtInvoicedAmount.Text = "";
            trExpense.Style.Add("display", "block");
            pnlExpenses.DefaultButton = "imgBtnSaveExpense";
        }
        else
        {
            trExpense.Visible = false;
        }
        imgBtnEditExpense.Visible = false;
        upnlExpenses.Update();
    }

    #region Adding,Editing and Canceling Expenses
    protected void imgBtnAddExpense_Click(object sender, ImageClickEventArgs e)
    {
        trddlExpense.Visible = true;
        trLineBeforeExpense.Visible = false;
        trLineAfterExpense.Visible = true;
        imgBtnSaveExpense.Visible = true;
        trMessageExpense.Visible = false;
        BindExpensesTypes();
        BindExpenses();
        trExpense.Visible = false;
        trMessageExpense.Visible = false;
        txtEnpenseAmount.Text = "";
        txtNoteExpense.Text = "";
        pnlExpenses.DefaultButton = "imgBtnSaveExpense";
    }
    protected void imgBtnSaveExpense_Click(object sender, ImageClickEventArgs e)
    {
        if (txtEnpenseAmount.Text != "" && txtInvoicedAmount.Text != "" && txtNonInvoicedAmount.Text != "")
        {
            DateTime mydate;
            if (datepicker.Text != "")
            {
                mydate = Convert.ToDateTime(datepicker.Text);
                mydate = mydate.AddDays(-1);
            }
            else
            {
                mydate = DateTime.Now.AddDays(-1);
            }

            tblExpanses ex = new tblExpanses();
            ex.AddNew();
            ex.Invoicedamount = commonMethods.ChangeToUS(txtInvoicedAmount.Text);
            ex.Noninvoicedamount = commonMethods.ChangeToUS(txtNonInvoicedAmount.Text);
            ex.ExpanseAmount = commonMethods.ChangeToUS(txtEnpenseAmount.Text);
            ex.FkExpanseCategoryID = Convert.ToInt32(ddlExpenses.SelectedValue);
            ex.IComment = txtNoteExpense.Text;
            ex.DCreateDate = mydate;
            ex.BHasInvoice = false;
            ex.BIsPaid = false;
            ex.Save();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " alert('saved')", true);

            BindExpensesTypes();
            BindExpenses();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jque", " alert('binding complete')", true);
            trMessageExpense.Visible = true;
            trddlExpense.Visible = false;
            lblMessageExpense.Text = "Successfully Added Expense Amount!";
            lblMessageExpense.ForeColor = Color.Green;
            txtEnpenseAmount.Text = "";
            txtNoteExpense.Text = "";
            txtNonInvoicedAmount.Text = "";
            txtInvoicedAmount.Text = "";
            O_ExpenseID = 0;
            LoadSubtotals();
            LoadGridUsers();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jqu", " alert('loading totals')", true);

        }
        else
        {
            trMessageExpense.Visible = true;
            lblMessageExpense.Text = "Plese enter Expense Amount!";
            lblMessageExpense.ForeColor = Color.Red;
            pnlExpenses.DefaultButton = "imgBtnSaveExpense";

        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
    }
    protected void imgBtnEditExpense_Click(object sender, ImageClickEventArgs e)
    {
        trddlExpense.Visible = false;
        if (txtEnpenseAmount.Text != "")
        {
            DateTime mydate;
            if (datepicker.Text != "")
            {
                mydate = Convert.ToDateTime(datepicker.Text);
                mydate = mydate.AddDays(-1);
            }
            else
            {
                mydate = DateTime.Now.AddDays(-1);
            }
            tblExpanses ex = new tblExpanses();
            ex.LoadByPrimaryKey(O_ExpenseID);
            if (ex.RowCount > 0)
            {
                ex.Invoicedamount = commonMethods.ChangeToUS(txtInvoicedAmount.Text);
                ex.Noninvoicedamount = commonMethods.ChangeToUS(txtNonInvoicedAmount.Text);
                ex.ExpanseAmount = commonMethods.ChangeToUS(txtEnpenseAmount.Text);
                ex.IComment = txtNoteExpense.Text;
                ex.Save();
                trMessageExpense.Visible = true;
                lblMessageExpense.Text = "Successfully Updated Expense Amount!";
                lblMessageExpense.ForeColor = Color.Green;
                BindExpensesTypes();
                BindExpenses();
                LoadGridUsers();
                LoadSubtotals();
            }
            O_ExpenseID = 0;
        }
        else
        {
            trMessageExpense.Visible = true;
            lblMessageExpense.Text = "Plese enter Expense Amount!";
            lblMessageExpense.ForeColor = Color.Red;
            pnlExpenses.DefaultButton = "imgBtnEditExpense";
        }
        upnlExpenses.Update();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
    }
    protected void imgBtnCancelExpense_Click(object sender, ImageClickEventArgs e)
    {
        imgBtnEditExpense.Visible = false;
        imgBtnSaveExpense.Visible = true;
        divExpenseDropDownList.Visible = true;
        trddlExpense.Visible = false;
        trMessageExpense.Visible = false;
        trMessageExpense.Visible = false;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
        BindExpensesTypes();
        BindExpenses();
        upnlExpenses.Update();
        upnlDailyIncome.Update();
        upnlSaveChanges.Update();
    }

    #endregion

    #region Adding New Expense Type

    protected void imgBtnAddNewExpenseType_Click(object sender, ImageClickEventArgs e)
    {
        trMessageExpense.Visible = false;
        tblUsers users = new tblUsers();
        users.LoadByPrimaryKey(UserID);
        if (users.RowCount > 0)
        {
            lblFromAddress.Text = users.SFirstName + " " + users.SLastName;
        }

        users.FlushData();
        users.GetDepartmentAdminID(DepartmentID);
        if (users.RowCount > 0)
        {
            tblUsers u = new tblUsers();
            u.LoadByPrimaryKey(Convert.ToInt32(users.GetColumn("fkuserid").ToString()));
            if (u.RowCount > 0)
                lblToAddress.Text = u.SFirstName + " " + u.SLastName;
        }
        txtMessage.Text = "Please add the following New Expense Type: >>>>>Add here the name<<<<<.  This is necessary for: >>>>>Add here the description<<<<<";

        trMessageExpense.Visible = false;
        ModalPopupExtender1.Show();
    }
    #endregion

    #endregion

    protected void btnGO_Click(object sender, EventArgs e)
    {
        loadDayComment();
        DateTime mydate = Convert.ToDateTime(datepicker.Text);
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;

        txtCheck.Text = "2";
        txtDay.Text = days.ToString();
        lblWeek.Text = mydate.DayOfWeek.ToString() + " " + mydate.Day.ToString() + "/" + mydate.Month.ToString();
        year = mydate.Year;
        selectedWeek = commonMethods.GetWeekNumber_New(mydate);
        day = commonMethods.getDay(mydate);

        LoadSubtotals();
        LoadGridUsers();
        BindIncomeTypes();
        BindOtherIncomes();
        BindExpensesTypes();
        BindExpenses();
        GetRegisters();

        editableColorArray = colorArray.Split(',');
        list = new List<string>(editableColorArray);

    }

    #region SubTotal for Tab 1

    private void LoadSubtotals()
    {
        double amount = 0;
        double advanceofDay = 0;
        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }

        year = mydate.Year;
        selectedWeek = commonMethods.GetWeekNumber_New(mydate);
        day = commonMethods.getDay(mydate);



        //income.getSubtotals(DepartmentID, year, selectedWeek, day);
        tblUserWorkshifts workshiftIncome = new tblUserWorkshifts();
        workshiftIncome.LoadCurrentIncomeTotalForAccountManager(day + 1, selectedWeek, DepartmentID, year);

        if (workshiftIncome.RowCount > 0)
        {
            incomeRowCount = workshiftIncome.RowCount;
            string O_IDs = string.Empty;
            for (int i = 0; i < workshiftIncome.RowCount; i++)
            {
                O_IDs += workshiftIncome.GetColumn("Orderid").ToString() + ",";
                if (workshiftIncome.GetColumn("fIncome").ToString() != "")
                    amount += Convert.ToDouble(workshiftIncome.GetColumn("fIncome").ToString());
                if (workshiftIncome.GetColumn("uAdvance").ToString() != "")
                    advanceofDay += Convert.ToDouble(workshiftIncome.GetColumn("uAdvance").ToString());

                workshiftIncome.MoveNext();
            }
            if (O_IDs.LastIndexOf(',') != -1)
            {
                O_IDs = O_IDs.Substring(0, O_IDs.LastIndexOf(','));
            }
            OrderIDs = O_IDs.Split(',');
            idsList = new List<string>(OrderIDs);
            tdTotal.Visible = true;
        }
        else
        {
            tdTotal.Visible = false;
        }
        grdIncome.DataSource = workshiftIncome.DefaultView;
        grdIncome.DataBind();

        tblIncome income = new tblIncome();
        income.GetOtherIncomes(mydate.Year, mydate.Month, mydate.Day);
        double otherincome = 0.00;
        if (income.RowCount > 0)
        {
            for (int i = 0; i < income.RowCount; i++)
            {
                amount += Convert.ToDouble(income.GetColumn("fIncome").ToString());
                otherincome += Convert.ToDouble(income.GetColumn("fIncome").ToString());
                income.MoveNext();
            }
            tdTotal.Visible = true;
            divOtherIncomeTotalTab1.Visible = true;
            lblOtherIncomeMessageTab1.Visible = false;
        }
        else
        {
            divOtherIncomeTotalTab1.Visible = false;
            lblOtherIncomeMessageTab1.Visible = true;
        }
        lblIncomeOther.Text = commonMethods.ChangetToUK(otherincome.ToString("N"));
        double expenseValue = 0.0;

        tblExpanses ex = new tblExpanses();
        ex.GetExpenses(mydate.Year, mydate.Month, mydate.Day);
        if (ex.RowCount > 0)
        {
            for (int i = 0; i < ex.RowCount; i++)
            {
                expenseValue += Convert.ToDouble(ex.GetColumn("ExpanseAmount").ToString());
                ex.MoveNext();
            }
            tdTotal.Visible = true;
            divExpenseTotalTab1.Visible = true;
            //lblExpenseMessageTab1.Visible = false;
            lblExpenseMessageTab1.Visible = false;
        }
        else
        {
            divExpenseTotalTab1.Visible = false;
            lblExpenseMessageTab1.Visible = true;
        }

        if (advanceofDay == 0 || advanceofDay == 0.0 || advanceofDay == 0.00)
        {
            divAdvanceTotalTab1.Visible = false;
            lblAdvanceMessageTab1.Visible = true;
        }
        else
        {
            divAdvanceTotalTab1.Visible = true;
            lblAdvanceMessageTab1.Visible = false;
            lblAdvance.Text = commonMethods.ChangetToUK(advanceofDay.ToString("N"));
        }
        double regSub = 0.0;
        tblRegisterHistory reh = new tblRegisterHistory();
        //reh.GetSubtotal(mydate.Day, mydate.Month, mydate.Year);
        reh.GetSubtotal(Convert.ToInt32(mydate.DayOfWeek) + 2, selectedWeek, mydate.Month, mydate.Year);
        if (reh.RowCount > 0)
        {
            for (int i = 0; i < reh.RowCount; i++)
            {
                double rvalue = Convert.ToDouble(reh.GetColumn("rValue").ToString());
                //double vat = Convert.ToDouble(reh.GetColumn("vat").ToString().Replace("%", ""));
                //regSub += rvalue - (vat * rvalue) / 100;
                regSub += rvalue;
                reh.MoveNext();
            }
            lblRegister.Text = commonMethods.ChangetToUK(regSub.ToString());
            lblRegisterMessageTab1.Visible = false;
        }
        else
        {
            divRegisterTotalTab1.Visible = false;
            lblRegisterMessageTab1.Visible = true;
            lblRegister.Text = "0,00";
        }
        //if (expenseValue != 0)
        //{
        //    lblIExpense.Text = commonMethods.ChangetToUK(expenseValue.ToString("N"));
        //}

        // if (amount != 0)
        {
            amount = amount - (advanceofDay + expenseValue);
            //amount = amount - advanceofDay;
            lblTotal.Text = "Total:  " + commonMethods.ChangetToUK(amount.ToString("N")) + " €";
            lblIExpense.Text = commonMethods.ChangetToUK(expenseValue.ToString("N"));
        }
        //else
        //{
        //    lblTotal.Text = "Total: 00,00 €";
        //   // lblIExpense.Text = "00,00";
        //}
        //divProgress.Style.Add("margin-bottom", "0px");
        upnlTotal.Update();

    }
    protected void grdIncome_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                //Label lblIncome = e.Row.FindControl("lblIncomeSubtotalDailtyReference") as Label;
                Label lblpkuserid = (Label)e.Row.FindControl("lblpkUserID");
                Label lblpkUserWorkshiftID = (Label)e.Row.FindControl("lblpkUserWorkshiftID");


                TextBox txtIncome = e.Row.FindControl("txtIncomeDailyReference") as TextBox;



                txtIncome.Attributes.Add("onchange", "javascript:Calculate(this);");
                txtIncome.Attributes.Add("onfocus", "javascript:getLastValue(this);");
                txtIncome.Attributes.Add("onclick", "javascript:getLastValue(this);");


                double netIn = 0.0;
                if (txtIncome.Text != "")
                {
                    netIn = Convert.ToDouble(txtIncome.Text);
                    incomeRowDataBound += netIn;
                    txtIncome.Text = commonMethods.ChangetToUK(Convert.ToDouble(txtIncome.Text).ToString("N"));
                }

                //if (drv["fIncome"].ToString() != "")
                //    incomeRowDataBound += Convert.ToDouble(drv["fIncome"].ToString());

                tblSpeciality specialtySeperator = new tblSpeciality();
                specialtySeperator.GetPositionSeperatorOptional(Convert.ToInt32(drv["orderid"].ToString()), DepartmentID);
                if (specialtySeperator.RowCount > 0)
                {
                    if (specialtySeperator.SSpeciality == "Separator")
                    {
                        CreatingGridIncomeControls_For_Subtotal(e);
                    }
                    else
                    {
                        specialtySeperator.FlushData();
                        specialtySeperator.GetPositionSeperatorPermanent(Convert.ToInt32(drv["orderid"].ToString()), DepartmentID);
                        if (specialtySeperator.RowCount > 0)
                        {
                            bool checkSmallOrderid = false;
                            for (int i = 0; i < idsList.Count - 1; i++)
                            {
                                try
                                {
                                    if (idsList[i + 1] != null)
                                    {
                                        if (Convert.ToInt32(idsList[i + 1]) < specialtySeperator.OrderID)
                                        {
                                            checkSmallOrderid = true;
                                            break;
                                        }
                                    }
                                }
                                catch (ArgumentOutOfRangeException ex)
                                {


                                }
                            }
                            if (!checkSmallOrderid)
                            {
                                CreatingGridIncomeControls_For_Subtotal(e);
                            }
                        }
                    }
                }
                idsList.RemoveAt(0);
                incomeRowCount--;
                if (incomeRowCount == 0)
                {
                    specialtySeperator.FlushData();
                    specialtySeperator.CheckSpecialtySeparator(Convert.ToInt32(drv["orderid"].ToString()), DepartmentID);
                    if (specialtySeperator.RowCount == 0)
                    {
                        CreatingGridIncomeControls_For_Subtotal(e);
                    }
                    // CreatingGridUserControls_For_Subtotal(e);
                }


                //if (lblIncome.Text != "")
                //    lblIncome.Text = Convert.ToDouble(lblIncome.Text).ToString("N") + " €";
            }
            catch (Exception ex)
            { }
        }
    }
    private void CreatingGridIncomeControls_For_Subtotal(GridViewRowEventArgs e)
    {
        HtmlContainerControl trSeprator = e.Row.FindControl("trSepratorDailyReference") as HtmlContainerControl;
        //HtmlContainerControl tdSeparator = e.Row.FindControl("tdSeparator") as HtmlContainerControl;
        HtmlContainerControl trSubtotal = e.Row.FindControl("trSubtotalDailtyReference") as HtmlContainerControl;

        Label lblIncomeSubtotal = e.Row.FindControl("lblIncomeSubtotalDailtyReference") as Label;

        HtmlContainerControl divIncomeSubtotal = e.Row.FindControl("divIncomeSubtotalDailyReference") as HtmlContainerControl;

        string colorVal = list[0].ToString();
        list.RemoveAt(0);

        //tdSeparator.Style.Add("background-color", colorVal);

        divIncomeSubtotal.Style.Add("background-color", colorVal);



        lblIncomeSubtotal.Text = commonMethods.ChangetToUK(incomeRowDataBound.ToString("N"));

        grandTotal += incomeRowDataBound;

        incomeRowDataBound = 0.0;

        trSeprator.Visible = true;
        trSubtotal.Visible = true;
    }

    #endregion

    #region Daily Income Users for Tab 2

    private void LoadGridUsers()
    {
        grandTotal = 0.0;
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = DateTime.Now;

        year = mydate.Year;
        selectedWeek = commonMethods.GetWeekNumber_New(mydate);
        day = commonMethods.getDay(mydate);
        tblUserWorkshifts uws = new tblUserWorkshifts();
        uws.LoadCurrentIncomeDetailForAccountManager(day, selectedWeek, DepartmentID, year);
        if (uws.RowCount > 0)
        {
            incomeRowCount = uws.RowCount;
            string array = string.Empty;
            string O_IDs = string.Empty; // pkSpecialityID
            if (!string.IsNullOrEmpty(uws.GetColumn("pkSpecialityTypeID").ToString()))
            {
                position = Convert.ToInt32(uws.GetColumn("pkSpecialityTypeID").ToString());
            }
            divUsers.Style.Add("overflow", "scroll");

            divUsers.Style.Add("width", "854px");
            divUsers.Style.Add("min-height", "300px");

            for (int i = 0; i < uws.RowCount; i++)
            {
                array += uws.GetColumn("pkSpecialityTypeID").ToString() + ",";
                O_IDs += uws.GetColumn("Orderid").ToString() + ",";
                uws.MoveNext();
            }
            if (array.LastIndexOf(',') != -1)
            {
                array = array.Substring(0, array.LastIndexOf(','));
            }
            if (O_IDs.LastIndexOf(',') != -1)
            {
                O_IDs = O_IDs.Substring(0, O_IDs.LastIndexOf(','));
            }
            arrSpecialtyTypes = array.Split(',');
            OrderIDs = O_IDs.Split(',');
            idsList = new List<string>(OrderIDs);
            imgBtnSaveTop.Visible = true;
            imgBtnSaveBottom.Visible = true;
        }
        else
        {
            divUsers.Style.Add("overflow", "hidden");
            divUsers.Style.Add("min-height", "0px");
            imgBtnSaveTop.Visible = false;
            imgBtnSaveBottom.Visible = false;
        }
        incomeRowDataBound = 0.0;
        netIncomeRowDataBound = 0.0;
        tip = 0.0;
        penality = 0.0;
        bonus = 0.0;
        salary = 0.0;
        advance = 0.0;
        GrdUsers.DataSource = uws.DefaultView;
        GrdUsers.DataBind();
        if (uws.RowCount > 0)
        {
            tdTotalofDay.Visible = true;
            hdnUserCount.Value = uws.RowCount.ToString();
        }
        else
        {
            tdTotalofDay.Visible = false;
        }
        //tblIncome objIncome = new tblIncome();
        //objIncome.GetOtherIncomes(mydate.Year, mydate.Month, mydate.Day);
        //double amount = 0.0;
        //if (objIncome.RowCount > 0)
        //{
        //    for (int i = 0; i < objIncome.RowCount; i++)
        //    {
        //        amount += Convert.ToDouble(objIncome.GetColumn("fIncome").ToString());
        //        objIncome.MoveNext();
        //    }
        //    tdTotal.Visible = true;
        //}
        //grandTotal += amount;
        lblTotalofDay.Text = commonMethods.ChangetToUK(grandTotal.ToString("N")) + " €";
        upnlTotalOfDay.Update();
        //upnlDailyIncome.Update();
    }
    protected void GrdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
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

                Label lblAgreedSalary = (Label)e.Row.FindControl("lblAgreedSalary");

                double WeeklySalary = 0.0;
                DateTime dDatePickerDate;
                if (datepicker.Text != "")
                    dDatePickerDate = Convert.ToDateTime(datepicker.Text);
                else
                    dDatePickerDate = DateTime.Now;
                //string SalarySymbol = "€";
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
                        HiddenField hidPercentage = e.Row.FindControl("hidPercentage") as HiddenField;
                        HiddenField hidPercentageOver = e.Row.FindControl("hidPercentageOver") as HiddenField;
                        HiddenField hidMinimumPerDay = e.Row.FindControl("hidMinimumPerDay") as HiddenField;
                        HiddenField hidPercentageValue = e.Row.FindControl("hidPercentageValue") as HiddenField;

                        hidPercentage.Value = drv["fSalaryPercentage"].ToString();
                        hidPercentageOver.Value = drv["PercentageOver"].ToString();
                        hidMinimumPerDay.Value = drv["MinimumPerday"].ToString();

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
                            //double incomeSum = income.UserTip + income.FIncome;
                            double incomeSum = income.FIncome;
                            if (incomeSum > Convert.ToDouble(drv["PercentageOver"].ToString()))
                                lblAgreedSalary.Text = ((Convert.ToDouble(drv["fSalaryPercentage"].ToString()) * incomeSum) / 100).ToString("N") + " €";
                            else
                                lblAgreedSalary.Text = Convert.ToDouble(drv["MinimumPerday"].ToString()).ToString("N") + " €";
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
                                    //double incomeSum = income.UserTip + income.FIncome;
                                    double incomeSum = income.FIncome;
                                    if (incomeSum > Convert.ToDouble(checkUserLastDayWorking.GetColumn("PercentageOver").ToString()))
                                        lblAgreedSalary.Text = ((Convert.ToDouble(checkUserLastDayWorking.GetColumn("fSalaryPercentage").ToString()) * incomeSum) / 100).ToString("N") + " €";
                                    else
                                        lblAgreedSalary.Text = Convert.ToDouble(drv["MinimumPerday"].ToString()).ToString("N") + " €";
                                }
                                else
                                    lblAgreedSalary.Text = Convert.ToDouble(drv["MinimumPerday"].ToString()).ToString("N") + " €";
                            }
                            else
                                lblAgreedSalary.Text = Convert.ToDouble(drv["MinimumPerday"].ToString()).ToString("N") + " €";
                        }
                        hidPercentageValue.Value = lblAgreedSalary.Text;
                    }
                    if (WeeklySalary != 0.0)
                    {
                        //lblAgreedSalary.Text = (Convert.ToDouble(WeeklySalary / Convert.ToDouble(7))).ToString("N") + " €";
                        lblAgreedSalary.Text = (Convert.ToDouble(WeeklySalary)).ToString("N") + " €";
                    }
                }
                else
                {
                    lblAgreedSalary.Text = WeeklySalary.ToString("N") + " €";
                }
                if (lblAgreedSalary.Text != "")
                {
                    lblAgreedSalary.Text = Math.Round(Convert.ToDouble(lblAgreedSalary.Text.Replace(" €", " "))).ToString("N");
                    lblAgreedSalary.Text = commonMethods.ChangetToUK(lblAgreedSalary.Text) + " €";


                }

                TextBox txtNotes = (TextBox)e.Row.FindControl("txtNotes");
                TextBox txtPenalty = (TextBox)e.Row.FindControl("txtPenalty");
                TextBox txtAdvances = (TextBox)e.Row.FindControl("txtAdvances");
                if (txtAdvances.Text != "")
                {
                    if (txtAdvances.Text == "0")
                        txtAdvances.Text = "";
                    else if (txtAdvances.Text == "0.00")
                        txtAdvances.Text = "";
                    else
                    {
                        txtAdvances.Text = Convert.ToDouble(txtAdvances.Text).ToString("N");
                        txtAdvances.Text = commonMethods.ChangetToUK(txtAdvances.Text);
                    }
                }
                //txtPenalty.Attributes.Add("onchange", "javascript:fixedDecimalPlace(this);");

                TextBox txtBonus = (TextBox)e.Row.FindControl("txtBonus");
                //txtBonus.Attributes.Add("onchange", "javascript:fixedDecimalPlace(this);");
                lblpkuserid.Text = drv["pkUserID"].ToString();
                lblpkUserWorkshiftID.Text = drv["pkUserWorkshiftID"].ToString();
                lblSpeciality.Text = drv["Abbrv"].ToString();
                lblSpeciality.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + drv["sSpeciality"].ToString() + "')");
                lblSpeciality.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                lblFullName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (name.Length > 10)
                {
                    lblFullName.Text = lblFullName.Text.Substring(0, 10) + "...";
                    lblFullName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblFullName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                txtNotes.Text = drv["sNotes"].ToString();

                string notes = drv["sNotes"].ToString();
                if (notes.Length > 56)
                {
                    txtNotes.Text = txtNotes.Text.Substring(0, 56) + "...";
                    txtNotes.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + notes + "')");
                    txtNotes.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                txtPenalty.Text = Convert.ToDouble(drv["Penalty"]).ToString("N");
                txtBonus.Text = Convert.ToDouble(drv["Bonus"]).ToString("N");

                if (Convert.ToInt32(drv["Penalty"]) > 0)
                {
                    txtPenalty.Text = Convert.ToDouble(drv["Penalty"]).ToString("N");
                    txtPenalty.Text = "-" + commonMethods.ChangetToUK(txtPenalty.Text);
                }
                else if (Convert.ToInt32(drv["Penalty"]) < 0)
                {
                    txtPenalty.Text = Convert.ToDouble(drv["Penalty"]).ToString("N");
                    txtPenalty.Text = commonMethods.ChangetToUK(txtPenalty.Text);
                }
                else if (Convert.ToInt32(drv["Penalty"]) == 0)
                {
                    txtPenalty.Text = "";
                }
                if (Convert.ToInt32(drv["Bonus"]) > 0)
                {
                    txtBonus.Text = Convert.ToDouble(drv["Bonus"]).ToString("N");
                    txtBonus.Text = "+" + commonMethods.ChangetToUK(txtBonus.Text);
                }
                else if (Convert.ToInt32(drv["Bonus"]) == 0)
                {
                    txtBonus.Text = "";
                }

                HtmlContainerControl divIncome = e.Row.FindControl("divIncome") as HtmlContainerControl;
                HtmlContainerControl divNetIncome = e.Row.FindControl("divNetIncome") as HtmlContainerControl;
                HtmlContainerControl divTip = e.Row.FindControl("divTip") as HtmlContainerControl;

                TextBox txtIncome = e.Row.FindControl("txtIncome") as TextBox;


                TextBox txtTip = e.Row.FindControl("txtTip") as TextBox;
                txtTip.Attributes.Add("onchange", "javascript:Calculate(this);");
                txtTip.Attributes.Add("onfocus", "javascript:getLastValue(this);");
                txtTip.Attributes.Add("onclick", "javascript:getLastValue(this);");

                //Label lblTip = e.Row.FindControl("lblTip") as Label;
                HiddenField hidTipValue = e.Row.FindControl("hidTipValue") as HiddenField;
                TextBox txtNetIncome = e.Row.FindControl("txtNetIncome") as TextBox;


                if (Convert.ToBoolean(drv["bIsIncomeSpecific"]))
                {
                    txtIncome.Attributes.Add("onchange", "javascript:Calculate(this);");
                    txtIncome.Attributes.Add("onfocus", "javascript:getLastValue(this);");
                    txtIncome.Attributes.Add("onclick", "javascript:getLastValue(this);");

                    txtNetIncome.Attributes.Add("onchange", "javascript:Calculate(this);");
                    txtNetIncome.Attributes.Add("onfocus", "javascript:getLastValue(this);");
                    txtNetIncome.Attributes.Add("onclick", "javascript:getLastValue(this);");

                    double netIn = 0.0;
                    double In = 0.0;
                    if (txtIncome.Text != "")
                    {
                        if (txtIncome.Text == "0.00")
                        {
                            txtIncome.Text = "";
                        }
                        else if (txtIncome.Text == "0")
                        {
                            txtIncome.Text = "";
                        }
                        else
                        {
                            In = Convert.ToDouble(txtIncome.Text);
                            txtIncome.Text = Convert.ToDouble(txtIncome.Text).ToString("N");
                            txtIncome.Text = commonMethods.ChangetToUK(txtIncome.Text);
                        }

                    }
                    if (txtNetIncome.Text != "")
                    {
                        if (txtNetIncome.Text == "0.00")
                        {
                            txtNetIncome.Text = "";
                        }
                        else if (txtNetIncome.Text == "0")
                        {
                            txtNetIncome.Text = "";
                        }
                        else
                        {
                            netIn = Convert.ToDouble(txtNetIncome.Text);
                            txtNetIncome.Text = Convert.ToDouble(txtNetIncome.Text).ToString("N");
                            txtNetIncome.Text = commonMethods.ChangetToUK(txtNetIncome.Text);
                        }
                    }
                    if (txtTip.Text != "")
                    {
                        if (txtTip.Text == "0.00")
                        {
                            txtTip.Text = commonMethods.ChangetToUK((In - netIn).ToString("N"));
                            //txtTip.Text = "00,00";
                        }
                        else if (txtTip.Text == "0")
                        {
                            txtTip.Text = commonMethods.ChangetToUK((In - netIn).ToString("N"));
                            //txtTip.Text = "00,00";
                        }
                        else
                        {
                            txtTip.Text = Convert.ToDouble(txtTip.Text).ToString("N");
                            txtTip.Text = commonMethods.ChangetToUK(txtTip.Text);
                        }
                    }
                    else if (txtTip.Text == "")
                    {
                        txtTip.Text = commonMethods.ChangetToUK((In - netIn).ToString("N"));
                    }

                    //lblNetIncome.Text = netIn.ToString("N");

                    if (drv["fIncome"].ToString() != "")
                        incomeRowDataBound += Convert.ToDouble(drv["fIncome"].ToString());
                    if (drv["netIncome"].ToString() != "")
                        netIncomeRowDataBound += Convert.ToDouble(drv["netIncome"].ToString());
                    if (drv["userTip"].ToString() != "")
                        tip += Convert.ToDouble(drv["userTip"].ToString());
                }
                else
                {
                    divIncome.Visible = false;
                    divNetIncome.Visible = false;
                    divTip.Visible = false;
                }


                if (drv["Penalty"].ToString() != "")
                    penality += Convert.ToDouble(drv["Penalty"].ToString());
                if (drv["Bonus"].ToString() != "")
                    bonus += Convert.ToDouble(drv["Bonus"].ToString());
                if (lblAgreedSalary.Text != "")
                    salary += commonMethods.ChangeToUS(lblAgreedSalary.Text.Replace("€", " "));
                if (drv["uAdvance"].ToString() != "")
                    advance += Convert.ToDouble(drv["uAdvance"].ToString());

                tblSpeciality specialtySeperator = new tblSpeciality();
                specialtySeperator.GetPositionSeperatorOptional(Convert.ToInt32(drv["orderid"].ToString()), DepartmentID);
                if (specialtySeperator.RowCount > 0)
                {
                    if (specialtySeperator.SSpeciality == "Separator")
                    {
                        CreatingGridUserControls_For_Subtotal(e);
                    }
                    else
                    {
                        specialtySeperator.FlushData();
                        specialtySeperator.GetPositionSeperatorPermanent(Convert.ToInt32(drv["orderid"].ToString()), DepartmentID);
                        if (specialtySeperator.RowCount > 0)
                        {
                            bool checkSmallOrderid = false;
                            for (int i = 0; i < idsList.Count - 1; i++)
                            {
                                try
                                {
                                    if (idsList[i + 1] != null)
                                    {
                                        if (Convert.ToInt32(idsList[i + 1]) < specialtySeperator.OrderID)
                                        {
                                            checkSmallOrderid = true;
                                            break;
                                        }
                                    }
                                }
                                catch (ArgumentOutOfRangeException ex)
                                {


                                }
                            }
                            if (!checkSmallOrderid)
                            {
                                CreatingGridUserControls_For_Subtotal(e);
                            }
                        }
                    }
                }
                idsList.RemoveAt(0);
                incomeRowCount--;
                if (incomeRowCount == 0)
                {
                    specialtySeperator.FlushData();
                    specialtySeperator.CheckSpecialtySeparator(Convert.ToInt32(drv["orderid"].ToString()), DepartmentID);
                    if (specialtySeperator.RowCount == 0)
                    {
                        CreatingGridUserControls_For_Subtotal(e);
                    }
                    // CreatingGridUserControls_For_Subtotal(e);
                }

                #region Commented
                /*
                if (drv["sSpeciality"].ToString() == "Separator")
                {
                    //position = Convert.ToInt32(drv["pkSpecialityTypeID"].ToString());
                    //HtmlContainerControl trSeprator = e.Row.FindControl("trSeprator") as HtmlContainerControl;
                   // HtmlContainerControl trSubtotal = e.Row.FindControl("trSubtotal") as HtmlContainerControl;
                   // trSeprator.Visible = true;
                    //trSubtotal.Visible = true;
                }
                else
                {
                    if (drv["fIncome"].ToString() != "")
                        income += Convert.ToDouble(drv["fIncome"].ToString());
                    if (drv["userTip"].ToString() != "")
                        tip += Convert.ToDouble(drv["userTip"].ToString());
                    if (drv["Penalty"].ToString() != "")
                        penality += Convert.ToDouble(drv["Penalty"].ToString());
                    if (drv["Bonus"].ToString() != "")
                        bonus += Convert.ToDouble(drv["Bonus"].ToString());
                    if (lblAgreedSalary.Text != "")
                        salary += Convert.ToDouble(lblAgreedSalary.Text.Replace("€", " "));
                    if (drv["uAdvance"].ToString() != "")
                        advance += Convert.ToDouble(drv["uAdvance"].ToString());
                    if (arrSpecialtyTypes.Length >= loopVariable + 1)
                    {
                        if (arrSpecialtyTypes.Length != loopVariable + 1)
                        {
                            if (Convert.ToInt32(arrSpecialtyTypes[loopVariable + 1].ToString()) != Convert.ToInt32(drv["pkSpecialityTypeID"].ToString()))
                            {
                                position = Convert.ToInt32(arrSpecialtyTypes[loopVariable + 1].ToString());
                                HtmlContainerControl trSeprator = e.Row.FindControl("trSeprator") as HtmlContainerControl;
                                HtmlContainerControl trSubtotal = e.Row.FindControl("trSubtotal") as HtmlContainerControl;

                                Label lblIncomeSubtotal = e.Row.FindControl("lblIncomeSubtotal") as Label;
                                Label lblNetIncomeSubtotal = e.Row.FindControl("lblNetIncomeSubtotal") as Label;
                                Label lblSalarySubtotal = e.Row.FindControl("lblSalarySubtotal") as Label;
                                Label lblTipSubtotal = e.Row.FindControl("lblTipSubtotal") as Label;
                                Label lblAdvanceSubtotal = e.Row.FindControl("lblAdvanceSubtotal") as Label;
                                Label lblBonus = e.Row.FindControl("lblBonus") as Label;
                                Label lblPenalty = e.Row.FindControl("lblPenalty") as Label;

                                HtmlContainerControl divIncomeSubtotal = e.Row.FindControl("divIncomeSubtotal") as HtmlContainerControl;
                                HtmlContainerControl divNetIncomeSubtotal = e.Row.FindControl("divNetIncomeSubtotal") as HtmlContainerControl;
                                HtmlContainerControl divSalarySubtotal = e.Row.FindControl("divSalarySubtotal") as HtmlContainerControl;
                                HtmlContainerControl divTipSubtotal = e.Row.FindControl("divTipSubtotal") as HtmlContainerControl;
                                HtmlContainerControl divAdvanceSubtotal = e.Row.FindControl("divAdvanceSubtotal") as HtmlContainerControl;
                                HtmlContainerControl divBonusSubtotal = e.Row.FindControl("divBonusSubtotal") as HtmlContainerControl;
                                HtmlContainerControl divPenaltySubtotal = e.Row.FindControl("divPenaltySubtotal") as HtmlContainerControl;

                                string colorVal = list[0].ToString();
                                list.RemoveAt(0);

                                divIncomeSubtotal.Style.Add("background-color", colorVal);
                                divNetIncomeSubtotal.Style.Add("background-color", colorVal);
                                divSalarySubtotal.Style.Add("background-color", colorVal);
                                divTipSubtotal.Style.Add("background-color", colorVal);
                                divAdvanceSubtotal.Style.Add("background-color", colorVal);
                                divBonusSubtotal.Style.Add("background-color", colorVal);
                                divPenaltySubtotal.Style.Add("background-color", colorVal);




                                lblIncomeSubtotal.Text = income.ToString("N") + " €";
                                lblNetIncomeSubtotal.Text = (income + tip).ToString("N") + " €";
                                lblSalarySubtotal.Text = salary.ToString("N") + " €";
                                lblTipSubtotal.Text = tip.ToString("N") + " €";
                                lblAdvanceSubtotal.Text = advance.ToString("N") + " €";
                                lblBonus.Text = bonus.ToString("N") + " €";
                                lblPenalty.Text = penality.ToString("N") + " €";

                                grandTotal += income - advance;

                                income = 0.0;
                                tip = 0.0;
                                penality = 0.0;
                                bonus = 0.0;
                                salary = 0.0;
                                advance = 0.0;
                                trSeprator.Visible = true;
                                trSubtotal.Visible = true;
                            }
                        }
                        else
                        {
                            HtmlContainerControl trSeprator = e.Row.FindControl("trSeprator") as HtmlContainerControl;
                            HtmlContainerControl trSubtotal = e.Row.FindControl("trSubtotal") as HtmlContainerControl;

                            Label lblIncomeSubtotal = e.Row.FindControl("lblIncomeSubtotal") as Label;
                            Label lblNetIncomeSubtotal = e.Row.FindControl("lblNetIncomeSubtotal") as Label;
                            Label lblSalarySubtotal = e.Row.FindControl("lblSalarySubtotal") as Label;
                            Label lblTipSubtotal = e.Row.FindControl("lblTipSubtotal") as Label;
                            Label lblAdvanceSubtotal = e.Row.FindControl("lblAdvanceSubtotal") as Label;
                            Label lblBonus = e.Row.FindControl("lblBonus") as Label;
                            Label lblPenalty = e.Row.FindControl("lblPenalty") as Label;

                            HtmlContainerControl divIncomeSubtotal = e.Row.FindControl("divIncomeSubtotal") as HtmlContainerControl;
                            HtmlContainerControl divNetIncomeSubtotal = e.Row.FindControl("divNetIncomeSubtotal") as HtmlContainerControl;
                            HtmlContainerControl divSalarySubtotal = e.Row.FindControl("divSalarySubtotal") as HtmlContainerControl;
                            HtmlContainerControl divTipSubtotal = e.Row.FindControl("divTipSubtotal") as HtmlContainerControl;
                            HtmlContainerControl divAdvanceSubtotal = e.Row.FindControl("divAdvanceSubtotal") as HtmlContainerControl;
                            HtmlContainerControl divBonusSubtotal = e.Row.FindControl("divBonusSubtotal") as HtmlContainerControl;
                            HtmlContainerControl divPenaltySubtotal = e.Row.FindControl("divPenaltySubtotal") as HtmlContainerControl;

                            string colorVal = list[0].ToString();
                            list.RemoveAt(0);

                            divIncomeSubtotal.Style.Add("background-color", colorVal);
                            divNetIncomeSubtotal.Style.Add("background-color", colorVal);
                            divSalarySubtotal.Style.Add("background-color", colorVal);
                            divTipSubtotal.Style.Add("background-color", colorVal);
                            divAdvanceSubtotal.Style.Add("background-color", colorVal);
                            divBonusSubtotal.Style.Add("background-color", colorVal);
                            divPenaltySubtotal.Style.Add("background-color", colorVal);


                            lblIncomeSubtotal.Text = income.ToString("N") + " €";
                            lblNetIncomeSubtotal.Text = (income + tip).ToString("N") + " €";
                            lblSalarySubtotal.Text = salary.ToString("N") + " €";
                            lblTipSubtotal.Text = tip.ToString("N") + " €";
                            lblAdvanceSubtotal.Text = advance.ToString("N") + " €";
                            lblBonus.Text = bonus.ToString("N") + " €";
                            lblPenalty.Text = penality.ToString("N") + " €";

                            grandTotal += income - advance;
                            
                            trSeprator.Visible = true;
                            trSubtotal.Visible = true;
                        }
                    }
                    else
                    {
                        HtmlContainerControl trSeprator = e.Row.FindControl("trSeprator") as HtmlContainerControl;
                        HtmlContainerControl trSubtotal = e.Row.FindControl("trSubtotal") as HtmlContainerControl;

                        Label lblIncomeSubtotal = e.Row.FindControl("lblIncomeSubtotal") as Label;
                        Label lblNetIncomeSubtotal = e.Row.FindControl("lblNetIncomeSubtotal") as Label;
                        Label lblSalarySubtotal = e.Row.FindControl("lblSalarySubtotal") as Label;
                        Label lblTipSubtotal = e.Row.FindControl("lblTipSubtotal") as Label;
                        Label lblAdvanceSubtotal = e.Row.FindControl("lblAdvanceSubtotal") as Label;
                        Label lblBonus = e.Row.FindControl("lblBonus") as Label;
                        Label lblPenalty = e.Row.FindControl("lblPenalty") as Label;

                        HtmlContainerControl divIncomeSubtotal = e.Row.FindControl("divIncomeSubtotal") as HtmlContainerControl;
                        HtmlContainerControl divNetIncomeSubtotal = e.Row.FindControl("divNetIncomeSubtotal") as HtmlContainerControl;
                        HtmlContainerControl divSalarySubtotal = e.Row.FindControl("divSalarySubtotal") as HtmlContainerControl;
                        HtmlContainerControl divTipSubtotal = e.Row.FindControl("divTipSubtotal") as HtmlContainerControl;
                        HtmlContainerControl divAdvanceSubtotal = e.Row.FindControl("divAdvanceSubtotal") as HtmlContainerControl;
                        HtmlContainerControl divBonusSubtotal = e.Row.FindControl("divBonusSubtotal") as HtmlContainerControl;
                        HtmlContainerControl divPenaltySubtotal = e.Row.FindControl("divPenaltySubtotal") as HtmlContainerControl;

                        string colorVal = list[0].ToString();
                        list.RemoveAt(0);

                        divIncomeSubtotal.Style.Add("background-color", colorVal);
                        divNetIncomeSubtotal.Style.Add("background-color", colorVal);
                        divSalarySubtotal.Style.Add("background-color", colorVal);
                        divTipSubtotal.Style.Add("background-color", colorVal);
                        divAdvanceSubtotal.Style.Add("background-color", colorVal);
                        divBonusSubtotal.Style.Add("background-color", colorVal);
                        divPenaltySubtotal.Style.Add("background-color", colorVal);

                        lblIncomeSubtotal.Text = income.ToString("N") + " €";
                        lblNetIncomeSubtotal.Text = (income + tip).ToString("N") + " €";
                        lblSalarySubtotal.Text = salary.ToString("N") + " €";
                        lblTipSubtotal.Text = tip.ToString("N") + " €";
                        lblAdvanceSubtotal.Text = advance.ToString("N") + " €";
                        lblBonus.Text = bonus.ToString("N") + " €";
                        lblPenalty.Text = penality.ToString("N") + " €";

                        grandTotal += income - advance;
                        
                        trSeprator.Visible = true;
                        trSubtotal.Visible = true;
                    }
                }
                loopVariable += 1;
                */
                #endregion
            }
            catch (Exception ex)
            {

            }
        }
    }
    private void CreatingGridUserControls_For_Subtotal(GridViewRowEventArgs e)
    {
        HtmlContainerControl trSeprator = e.Row.FindControl("trSeprator") as HtmlContainerControl;
        //HtmlContainerControl tdSeparator = e.Row.FindControl("tdSeparator") as HtmlContainerControl;
        HtmlContainerControl trSubtotal = e.Row.FindControl("trSubtotal") as HtmlContainerControl;

        Label lblIncomeSubtotal = e.Row.FindControl("lblIncomeSubtotal") as Label;
        Label lblNetIncomeSubtotal = e.Row.FindControl("lblNetIncomeSubtotal") as Label;
        Label lblSalarySubtotal = e.Row.FindControl("lblSalarySubtotal") as Label;
        Label lblTipSubtotal = e.Row.FindControl("lblTipSubtotal") as Label;
        Label lblAdvanceSubtotal = e.Row.FindControl("lblAdvanceSubtotal") as Label;
        Label lblBonus = e.Row.FindControl("lblBonus") as Label;
        Label lblPenalty = e.Row.FindControl("lblPenalty") as Label;

        HtmlContainerControl divIncomeSubtotal = e.Row.FindControl("divIncomeSubtotal") as HtmlContainerControl;
        HtmlContainerControl divNetIncomeSubtotal = e.Row.FindControl("divNetIncomeSubtotal") as HtmlContainerControl;
        HtmlContainerControl divSalarySubtotal = e.Row.FindControl("divSalarySubtotal") as HtmlContainerControl;
        HtmlContainerControl divTipSubtotal = e.Row.FindControl("divTipSubtotal") as HtmlContainerControl;
        HtmlContainerControl divAdvanceSubtotal = e.Row.FindControl("divAdvanceSubtotal") as HtmlContainerControl;
        HtmlContainerControl divBonusSubtotal = e.Row.FindControl("divBonusSubtotal") as HtmlContainerControl;
        HtmlContainerControl divPenaltySubtotal = e.Row.FindControl("divPenaltySubtotal") as HtmlContainerControl;

        string colorVal = list[0].ToString();
        list.RemoveAt(0);

        //tdSeparator.Style.Add("background-color", colorVal);

        divIncomeSubtotal.Style.Add("background-color", colorVal);
        divNetIncomeSubtotal.Style.Add("background-color", colorVal);
        divSalarySubtotal.Style.Add("background-color", colorVal);
        divTipSubtotal.Style.Add("background-color", colorVal);
        divAdvanceSubtotal.Style.Add("background-color", colorVal);
        divBonusSubtotal.Style.Add("background-color", colorVal);
        divPenaltySubtotal.Style.Add("background-color", colorVal);

        if (incomeRowDataBound == 0 || incomeRowDataBound == 0.00 || incomeRowDataBound == 0.0)
            lblIncomeSubtotal.Text = "00,00";
        else
            lblIncomeSubtotal.Text = commonMethods.ChangetToUK(incomeRowDataBound.ToString("N"));

        if (netIncomeRowDataBound == 0 || netIncomeRowDataBound == 0.00 || netIncomeRowDataBound == 0.0)
            lblNetIncomeSubtotal.Text = "00,00";
        else
            lblNetIncomeSubtotal.Text = commonMethods.ChangetToUK((netIncomeRowDataBound).ToString("N"));

        if (salary == 0 || salary == 0.00 || salary == 0.0)
            lblSalarySubtotal.Text = "00,00";
        else
            lblSalarySubtotal.Text = commonMethods.ChangetToUK(salary.ToString("N"));

        if (tip == 0 || tip == 0.00 || tip == 0.0)
            lblTipSubtotal.Text = "00,00";
        else
            lblTipSubtotal.Text = commonMethods.ChangetToUK(tip.ToString("N"));

        if (advance == 0 || advance == 0.00 || advance == 0.0)
            lblAdvanceSubtotal.Text = "00,00";
        else
            lblAdvanceSubtotal.Text = commonMethods.ChangetToUK(advance.ToString("N"));

        if (bonus == 0 || bonus == 0.00 || bonus == 0.0)
            lblBonus.Text = "00,00";
        else
            lblBonus.Text = commonMethods.ChangetToUK(bonus.ToString("N"));

        if (penality == 0 || penality == 0.00 || penality == 0.0)
            lblPenalty.Text = "00,00";
        else
            lblPenalty.Text = commonMethods.ChangetToUK(penality.ToString("N"));

        //        grandTotal += incomeRowDataBound - advance;
        grandTotal += incomeRowDataBound;

        incomeRowDataBound = 0.0;
        netIncomeRowDataBound = 0.0;
        tip = 0.0;
        penality = 0.0;
        bonus = 0.0;
        salary = 0.0;
        advance = 0.0;
        trSeprator.Visible = true;
        trSubtotal.Visible = true;
    }

    #region Saving Daily Income
    protected void imgBtnSaveTop_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //divProgress.Style.Add("display", "block");
            //divProgress.Style.Add("margin-bottom", "8px");
            if (GrdUsers.Rows.Count > 0)
            {
                for (int i = 0; i < GrdUsers.Rows.Count; i++)
                {
                    HiddenField hid = GrdUsers.Rows[i].FindControl("hidWorkshipid") as HiddenField;
                    HiddenField hidAdvanceid = GrdUsers.Rows[i].FindControl("hidAdvanceid") as HiddenField;
                    HiddenField hidIncomeid = GrdUsers.Rows[i].FindControl("hidIncomeid") as HiddenField;
                    HiddenField hidTipValue = GrdUsers.Rows[i].FindControl("hidTipValue") as HiddenField;

                    Label lblpkuserid = (Label)GrdUsers.Rows[i].FindControl("lblpkUserID");
                    tblUserWorkshifts workshift = new tblUserWorkshifts();
                    workshift.LoadByPrimaryKey(Convert.ToInt32(hid.Value));
                    if (workshift.RowCount > 0)
                    {
                        TextBox txtNotes = GrdUsers.Rows[i].FindControl("txtNotes") as TextBox;
                        TextBox txtPenalty = GrdUsers.Rows[i].FindControl("txtPenalty") as TextBox;
                        TextBox txtBonus = GrdUsers.Rows[i].FindControl("txtBonus") as TextBox;


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
                                txtPenalty.Text = "-" + txtPenalty.Text.TrimStart('-');
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
                                txtBonus.Text = "+" + txtBonus.Text.TrimStart('+');
                                workshift.Bonus = bonus;
                            }
                        }
                        else
                            txtBonus.Text = "";

                        workshift.SNotes = txtNotes.Text;
                        workshift.DModifiedDate = DateTime.Now;
                        workshift.Save();


                    }

                    tblUserAdvances advances = new tblUserAdvances();
                    if (hidAdvanceid.Value != "")
                    {
                        advances.LoadByPrimaryKey(Convert.ToInt32(hidAdvanceid.Value));
                        if (advances.RowCount > 0)
                        {
                            TextBox txtAdvances = GrdUsers.Rows[i].FindControl("txtAdvances") as TextBox;
                            if (txtAdvances.Text != "")
                            {
                                advances.UAdvance = commonMethods.ChangeToUS(txtAdvances.Text);
                                advances.DModifiedDate = DateTime.Now;
                                advances.Save();
                            }
                            else
                            {
                                advances.UAdvance = 00.00;
                                advances.DModifiedDate = DateTime.Now;
                                advances.Save();
                            }
                        }
                    }
                    else
                    {
                        TextBox txtAdvances = GrdUsers.Rows[i].FindControl("txtAdvances") as TextBox;
                        if (txtAdvances.Text != "")
                        {
                            advances.AddNew();
                            advances.UAdvance = commonMethods.ChangeToUS(txtAdvances.Text);
                            advances.IsBack = false;
                            advances.FkUserWorkshiftID = Convert.ToInt32(hid.Value);
                            advances.FkUserID = Convert.ToInt32(lblpkuserid.Text);
                            advances.AdvanceDate = DateTime.Now;
                            advances.DModifiedDate = DateTime.Now;
                            advances.DCreatedDate = DateTime.Now;
                            advances.Save();
                        }

                    }

                    tblIncome income = new tblIncome();
                    if (hidIncomeid.Value != "")
                    {
                        income.LoadByPrimaryKey(Convert.ToInt32(hidIncomeid.Value));
                        if (income.RowCount > 0)
                        {
                            TextBox txtIncome = GrdUsers.Rows[i].FindControl("txtIncome") as TextBox;
                            TextBox txtNetIncome = GrdUsers.Rows[i].FindControl("txtNetIncome") as TextBox;

                            TextBox txtTip = GrdUsers.Rows[i].FindControl("txtTip") as TextBox;
                            //Label lblTip = GrdUsers.Rows[i].FindControl("lblTip") as Label;

                            if (txtIncome.Text != "")
                                income.FIncome = commonMethods.ChangeToUS(txtIncome.Text);
                            else
                                income.FIncome = 00.00;

                            if (txtNetIncome.Text != "")
                                income.NetIncome = commonMethods.ChangeToUS(txtNetIncome.Text);
                            else
                                income.NetIncome = 00.00;

                            if (hidTipValue.Value != "")
                                income.UserTip = Convert.ToDouble(hidTipValue.Value);
                            else
                                income.UserTip = 00.00;
                            if (txtTip.Text != "")
                                income.UserTip = commonMethods.ChangeToUS(txtTip.Text);
                            else
                                income.UserTip = 00.00;
                            income.DIncomeDate = DateTime.Now;
                            income.Save();
                        }
                    }
                    else
                    {
                        TextBox txtIncome = GrdUsers.Rows[i].FindControl("txtIncome") as TextBox;
                        TextBox txtNetIncome = GrdUsers.Rows[i].FindControl("txtNetIncome") as TextBox;
                        //TextBox txtTip = GrdUsers.Rows[i].FindControl("txtTip") as TextBox;
                        //Label lblTip = GrdUsers.Rows[i].FindControl("lblTip") as Label;
                        bool checkIncome = false;
                        bool checkTip = false;
                        if (txtIncome.Text != "")
                            checkIncome = true;
                        if (hidTipValue.Value != "")
                            checkTip = true;
                        if (checkIncome || checkTip)
                        {
                            income.AddNew();
                            if (txtIncome.Text != "")
                                income.FIncome = commonMethods.ChangeToUS(txtIncome.Text);
                            if (txtNetIncome.Text != "")
                                income.NetIncome = commonMethods.ChangeToUS(txtNetIncome.Text);
                            if (hidTipValue.Value != "")
                                income.UserTip = Convert.ToDouble(hidTipValue.Value);
                            income.FkUserID = Convert.ToInt32(lblpkuserid.Text);
                            income.FkUserWorkshiftID = Convert.ToInt32(hid.Value);
                            income.DIncomeDate = DateTime.Now;
                            income.Save();
                        }
                    }
                }
                //upnlDailyIncome.Update();

                LoadSubtotals();
                LoadGridUsers();
            }
            trDailyIncomeMessage.Visible = true;
            lblDailyIncomeMessage.Text = "Successfully Updated!";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
        }
        catch (Exception ex)
        {
            trDailyIncomeMessage.Visible = true;
            lblDailyIncomeMessage.Text = "Updating Error! Please Try Again";
        }
        upnlDailyIncome.Update();
    }
    #endregion

    #region Cancel Save Changing on Daily Income
    protected void imgCancelChanges_Click(object sender, ImageClickEventArgs e)
    {
        hidChange.Value = "0";
        LoadGridUsers();
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "jquery", " $(function() { ApplyJquery(); });", true);
        upnlSaveChanges.Update();
        upnlDailyIncome.Update();
    }
    #endregion

    #endregion

    #region Sending New Income and Expense Messages,Email to Department Admim

    protected void imgBtnMessage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tblUsers users = new tblUsers();
            users.GetDepartmentAdminID(DepartmentID);
            if (users.RowCount > 0)
            {

                tblUserInBox userIn = new tblUserInBox();
                userIn.AddNew();
                userIn.FkFromUserID = UserID;
                userIn.FkToUserID = Convert.ToInt32(users.GetColumn("fkuserid").ToString());
                userIn.SSubject = txtSubject.Text;
                userIn.SMessage = txtMessage.Text;
                userIn.DReceivedDate = DateTime.Now;
                userIn.BIsread = false;
                userIn.Save();

                tblUserSentBox userOut = new tblUserSentBox();
                userOut.AddNew();
                userOut.FkFromUserID = UserID;
                userOut.FkToUserID = Convert.ToInt32(users.GetColumn("fkuserid").ToString());
                userOut.SSubject = txtSubject.Text;
                userOut.SMessage = txtMessage.Text;
                userOut.DSentDate = DateTime.Now;
                userOut.Save();
                ModalPopupExtender1.Hide();

                Emailing email = new Emailing();
                email.P_Email_Subject = txtSubject.Text;
                tblUserEmails useremail = new tblUserEmails();
                useremail.LoadUserEmailsActive(UserID);
                if (useremail.RowCount > 0)
                    email.P_FromAddress = useremail.SEmail;
                useremail.FlushData();
                useremail.LoadUserEmailsActive(Convert.ToInt32(users.GetColumn("fkuserid").ToString()));
                if (useremail.RowCount > 0)
                    email.P_ToAddress = useremail.SEmail;
                email.P_Message_Body = txtMessage.Text;
                email.Send_Email();

            }
        }
        catch (Exception ex)
        { }
    }

    #endregion

    #region Day Comment

    private void loadDayComment()
    {
        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }
        tblDayComments comments = new tblDayComments();
        comments.getDayComment(mydate.Year, mydate.Month, mydate.Day);
        if (comments.RowCount > 0)
            txtDayComment.Text = comments.CComment;
        else
            txtDayComment.Text = "";
    }
    protected void imgBtnSaveDayComment_Click(object sender, ImageClickEventArgs e)
    {
        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }
        tblDayComments comments = new tblDayComments();
        comments.getDayComment(mydate.Year, mydate.Month, mydate.Day);
        if (comments.RowCount > 0)
        {
            comments.CComment = txtDayComment.Text;
            comments.Save();
        }
        else
        {
            comments.FlushData();
            comments.AddNew();
            comments.FkAccountManagerID = UserID;
            comments.CComment = txtDayComment.Text;
            comments.CommentDate = mydate;
            comments.Save();
        }
    }

    #endregion

    #region Extra Code

    protected void upnlGo_Load(object sender, EventArgs e)
    {
        //divProgress.Style.Add("display", "block");
        //divProgress.Style.Add("margin-bottom", "8px");

        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }

        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;

        txtCheck.Text = "2";
        txtDay.Text = days.ToString();
        lblWeek.Text = mydate.DayOfWeek.ToString() + " " + mydate.Day.ToString() + "/" + mydate.Month.ToString();
        year = mydate.Year;
        selectedWeek = commonMethods.GetWeekNumber_New(mydate);
        day = commonMethods.getDay(mydate);
        //upnlDate.Update();
        LoadSubtotals();
        LoadGridUsers();
    }
    [WebMethod(EnableSession = true)]
    public static string HelpSaved(string msg)
    {
       // AccountManager_DailyIncome d = new AccountManager_DailyIncome();
        Admin_AdminDailyIncome d = new Admin_AdminDailyIncome();
        HtmlContainerControl divProgress = d.FindControl("divProgress") as HtmlContainerControl;
        TextBox datepicker = d.FindControl("datepicker") as TextBox;
        TextBox txtCheck = d.FindControl("txtCheck") as TextBox;
        TextBox txtDay = d.FindControl("txtDay") as TextBox;
        Label lblWeek = d.FindControl("lblWeek") as Label;
        UpdatePanel upnlDate = d.FindControl("upnlDate") as UpdatePanel;

        //divProgress.Style.Add("display", "block");
        //divProgress.Style.Add("margin-bottom", "8px");

        DateTime mydate;
        if (datepicker.Text != "")
        {
            mydate = Convert.ToDateTime(datepicker.Text);
            mydate = mydate.AddDays(-1);
        }
        else
        {
            mydate = DateTime.Now.AddDays(-1);
        }
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;

        txtCheck.Text = "2";
        txtDay.Text = days.ToString();
        lblWeek.Text = mydate.DayOfWeek.ToString() + " " + mydate.Day.ToString() + "/" + mydate.Month.ToString();
        d.year = mydate.Year;
        d.selectedWeek = commonMethods.GetWeekNumber_New(mydate);
        d.day = commonMethods.getDay(mydate);
        upnlDate.Update();

        d.LoadGridUsers();
        d.LoadSubtotals();
        return "name";
    }

    protected void pnlUsers_DataBinding(object sender, EventArgs e)
    {

    }
    #endregion

    #region Registers

    private void GetRegisters()
    {
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = DateTime.Now;

        tblRegisters regs = new tblRegisters();
        regs.GetActiveRegisterForDailyInput(mydate.Day, mydate.Month, mydate.Year);
        grdRegisters.DataSource = regs.DefaultView;
        grdRegisters.DataBind();
        lblRegSubTotal.Text = "0.00 €";
        if (registerSubtotal != 0.0 || registerSubtotal != 0.00 || registerSubtotal != 0)
        {
            lblRegSubTotal.Text = commonMethods.ChangetToUK(registerSubtotal.ToString("N")) + " €";
            registerSubtotal = 0.0;
        }

        if (regs.RowCount > 0)
        {
            imgBtnSaveRegisterValueTop.Visible = true;
            imgBtnSaveRegisterValueBottom.Visible = true;
        }
        else
        {
            imgBtnSaveRegisterValueTop.Visible = false;
            imgBtnSaveRegisterValueBottom.Visible = false;
        }
        upnlRegister.Update();


    }
    protected void imgBtnSaveRegisterValueTop_Click(object sender, ImageClickEventArgs e)
    {
        DateTime mydate;
        if (datepicker.Text != "")
            mydate = Convert.ToDateTime(datepicker.Text);
        else
            mydate = DateTime.Now;
        if (grdRegisters.Rows.Count > 0)
        {
            for (int i = 0; i < grdRegisters.Rows.Count; i++)
            {
                HiddenField hidRegVat = grdRegisters.Rows[i].FindControl("hidRegVat") as HiddenField;
                HiddenField hidRegID = grdRegisters.Rows[i].FindControl("hidRegID") as HiddenField;
                TextBox txtRegisterValue = grdRegisters.Rows[i].FindControl("txtRegisterValue") as TextBox;
                TextBox txtRegisterNote = grdRegisters.Rows[i].FindControl("txtRegisterNote") as TextBox;

                if (txtRegisterValue.Text != "")
                {
                    //tblVAT vat = new tblVAT();
                    //vat.LoadByPrimaryKey(Convert.ToInt32(hidRegVat.Value));
                    //string vatValue = string.Empty;
                    //if (vat.RowCount > 0)
                    //{
                    //    vatValue = vat.Vat.Replace("%", "");
                    //}

                    tblRegisterHistory reghis = new tblRegisterHistory();
                    reghis.GetRegisterValue_ByRegisterID(Convert.ToInt32(hidRegID.Value), mydate.Day - 1, mydate.Month, mydate.Year);
                    if (reghis.RowCount > 0)
                    {
                        reghis.RValue = commonMethods.ChangeToUS(txtRegisterValue.Text);
                        reghis.RNote = txtRegisterNote.Text;
                        reghis.DModifiedDate = DateTime.Now;
                        reghis.Save();
                        lblMessageRegister.Visible = true;
                        lblMessageRegister.Text = "Successfully Updated!";
                    }
                    else
                    {
                        reghis.FlushData();
                        reghis.AddNew();
                        reghis.FkRegisterID = Convert.ToInt32(hidRegID.Value);
                        reghis.RValue = commonMethods.ChangeToUS(txtRegisterValue.Text);
                        reghis.RNote = txtRegisterNote.Text;
                        //reghis.VatPer = Convert.ToDouble(vatValue);
                        reghis.RDay = mydate.Day - 1;
                        reghis.RMonth = mydate.Month;
                        reghis.RYear = mydate.Year;
                        reghis.Iweeknumber = commonMethods.GetWeeknumber(mydate);

                        if (mydate.Date.DayOfWeek == DayOfWeek.Sunday)
                            reghis.Idaynumber = 1;
                        else if (mydate.Date.DayOfWeek == DayOfWeek.Monday)
                            reghis.Idaynumber = 2;
                        else if (mydate.Date.DayOfWeek == DayOfWeek.Tuesday)
                            reghis.Idaynumber = 3;
                        else if (mydate.Date.DayOfWeek == DayOfWeek.Wednesday)
                            reghis.Idaynumber = 4;
                        else if (mydate.Date.DayOfWeek == DayOfWeek.Thursday)
                            reghis.Idaynumber = 5;
                        else if (mydate.Date.DayOfWeek == DayOfWeek.Friday)
                            reghis.Idaynumber = 6;
                        else if (mydate.Date.DayOfWeek == DayOfWeek.Saturday)
                            reghis.Idaynumber = 7;
                        reghis.Dweekstart = commonMethods.GetWeekStartDate(mydate.Year, commonMethods.GetWeeknumber(mydate.Date)).AddDays(-1);
                        reghis.Dweekend = commonMethods.GetWeekStartDate(mydate.Year, commonMethods.GetWeeknumber(mydate.Date)).AddDays(5);
                        reghis.DModifiedDate = DateTime.Now;
                        reghis.DCreatedDate = DateTime.Now;
                        reghis.Save();
                        lblMessageRegister.Visible = true;
                        lblMessageRegister.Text = "Successfully Updated!";
                    }
                }
            }
            LoadSubtotals();
        }
        GetRegisters();
        upnlRegister.Update();
    }
    protected void grdRegisters_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdRegisters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
            Label lblDescription = e.Row.FindControl("lblDescription") as Label;
            TextBox txtRegisterValue = e.Row.FindControl("txtRegisterValue") as TextBox;
            TextBox txtRegisterNote = e.Row.FindControl("txtRegisterNote") as TextBox;

            HiddenField hidRegID = e.Row.FindControl("hidRegID") as HiddenField;
            HiddenField hidRegVat = e.Row.FindControl("hidRegVat") as HiddenField;

            //tblVAT vat = new tblVAT();
            //vat.LoadByPrimaryKey(Convert.ToInt32(hidRegVat.Value));
            //string vatValue = string.Empty;
            //if (vat.RowCount > 0)
            //{
            //    vatValue = vat.Vat.Replace("%", "");
            //}

            DateTime mydate;
            if (datepicker.Text != "")
                mydate = Convert.ToDateTime(datepicker.Text);
            else
                mydate = DateTime.Now;

            if (lblDescription.Text != "")
            {
                string des = lblDescription.Text;
                if (lblDescription.Text.Length > 40)
                {
                    lblDescription.Text = lblDescription.Text.Substring(0, 40) + "...";
                    lblDescription.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + des + "')");
                    lblDescription.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
            }

            tblRegisterHistory reghis = new tblRegisterHistory();
            reghis.GetRegisterValue_ByRegisterID(Convert.ToInt32(hidRegID.Value), mydate.Day - 1, mydate.Month, mydate.Year);
            if (reghis.RowCount > 0)
            {
                //registerSubtotal += reghis.RValue - (Convert.ToDouble(vatValue) * reghis.RValue) / 100;
                registerSubtotal += reghis.RValue;

                txtRegisterValue.Text = commonMethods.ChangetToUK(reghis.RValue.ToString());
                txtRegisterNote.Text = reghis.RNote;
            }
            else
            {
                txtRegisterValue.Text = "";
                txtRegisterNote.Text = "";
            }
        }
    }
    #endregion
}

