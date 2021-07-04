using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.Sql;
using System.Drawing;

using ExpertPdf.HtmlToPdf;
using System.Drawing.Imaging;

using LC.Model.BMS.BLL;
public partial class Statistics : System.Web.UI.Page
{
    static int UserID;
    static int DepartmentID;

    static double weektotal = 0.0;
    static double weekDifference = 0.0;
    static double weekPercentage = 0.0;

    static double grandDiff = 0.0;
    static double grandPer = 0.0;

    static string showStatisticsTitle = string.Empty;

    ArrayList arrLeftValue = new ArrayList();
    ArrayList arrRightValue = new ArrayList();
    ArrayList positions = new ArrayList();

    static double weekIncome = 0.0;
    static double weekSalary = 0.0;
    static double weekTip = 0.0;
    static double weekBonus = 0.0;
    static double weekPenalty = 0.0;
    static double weekAdvance = 0.0;
    static int weekLateHours = 0;

    static double TotalIncome = 0.0;
    static double TotalSalary = 0.0;
    static double TotalTip = 0.0;
    static double TotalBonus = 0.0;
    static double TotalPenalty = 0.0;
    static double TotalAdvance = 0.0;
    static int TotalLateHours = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

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


        if (!IsPostBack)
        {
            showStatisticsTitle = "income";
            BindDropDowns();
            GetPositionsList();
            GetWeekStats();
        }
    }
    private void GetPositionsList()
    {
        tblSpeciality sp = new tblSpeciality();
        sp.getPositionForStats(DepartmentID);
        dtlPositions.DataSource = sp.DefaultView;
        dtlPositions.DataBind();
    }
    private void GetWeekStats()
    {
        grandDiff = 0.0;
        grandPer = 0.0;

        if (showStatisticsTitle == "income")
        {
            lblStatisticsTitle.Text = (showStatisticsTitle + " Statistics").ToUpper();
            setStatwithYear(showStatisticsTitle);
        }
        else if (showStatisticsTitle == "register")
        {
            lblStatisticsTitle.Text = (showStatisticsTitle + " Statistics").ToUpper();
            setStatwithYear(showStatisticsTitle);
        }
        else if (showStatisticsTitle == "salaries")
        {
            lblStatisticsTitle.Text = (showStatisticsTitle + " Statistics").ToUpper();
            setStatwithYear(showStatisticsTitle);
        }
        else if (showStatisticsTitle == "expense")
        {
            lblStatisticsTitle.Text = (showStatisticsTitle + " Statistics").ToUpper();
            setStatwithYear(showStatisticsTitle);
        }
        else if (showStatisticsTitle == "supply")
        {
            lblStatisticsTitle.Text = (showStatisticsTitle + " Statistics").ToUpper();
            setStatwithYear(showStatisticsTitle);
        }
        else if (showStatisticsTitle == "product")
        {
            lblStatisticsTitle.Text = (showStatisticsTitle + " Statistics").ToUpper();
            setStatwithYear(showStatisticsTitle);
        }
        else if (showStatisticsTitle == "position")
        {
            lblStatisticsTitle.Text = (showStatisticsTitle + " Statistics").ToUpper();
            setStatwithYear(showStatisticsTitle);
        }
        else if (showStatisticsTitle == "other")
        {
            lblStatisticsTitle.Text = (showStatisticsTitle + " Statistics").ToUpper();
            setStatwithYear(showStatisticsTitle);
        }
        else if (showStatisticsTitle == "staff")
        {
            lblStatisticsTitle.Text = (showStatisticsTitle + " Statistics").ToUpper();
            setStatwithYear(showStatisticsTitle);

        }
        if (showStatisticsTitle != "staff")
        {
            grdWeeks.DataSource = GetWeek();
            grdWeeks.DataBind();
        }
        else
        {
            grdWeeks.DataSource = GetWeekForStaff();
            grdWeeks.DataBind();
        }

        if (grandDiff > 0)
        {
            divDiffValue.Style.Add("background", "url(../Images/textbox_light_green.png);");
            divDiffPer.Style.Add("background", "url(../Images/textbox_light_green.png);");
        }
        else if (grandDiff < 0)
        {
            divDiffValue.Style.Add("background", "url(../Images/textbox_pink.png);");
            divDiffPer.Style.Add("background", "url(../Images/textbox_pink.png);");
        }
        else
        {
            divDiffValue.Style.Add("background", " url(../Images/textbox_115.png);");
            divDiffPer.Style.Add("background", " url(../Images/textbox_115.png);");
        }


    }
    private void setStatwithYear(string stat_name)
    {
        if (ddlStartYear.SelectedValue == "0")
            lblStartYear.Text = stat_name + DateTime.Now.Year;
        else if (ddlStartYear.SelectedValue != "0")
            lblStartYear.Text = stat_name + ddlStartYear.SelectedItem.Text;

        if (ddlEndYear.SelectedValue == "0")
            lblEndYear.Text = stat_name + DateTime.Now.Year;
        else
            lblEndYear.Text = stat_name + ddlEndYear.SelectedItem.Text;
    }
    private void BindDropDowns()
    {
        #region Display Options
        ddlOptions.Items.Clear();
        ddlOptions.Items.Add(new ListItem("Display Options", "0"));
        ddlOptions.Items.Add(new ListItem("Week only", "1"));
        ddlOptions.Items.Add(new ListItem("Week +,- 1", "2"));
        ddlOptions.Items.Add(new ListItem("Week +,- 2", "3"));
        ddlOptions.Items.Add(new ListItem("Week +,- 4", "4"));
        ddlOptions.Items.Add(new ListItem("All Weeks", "5"));


        ddlStaffDisplayOptions.Items.Clear();
        ddlStaffDisplayOptions.Items.Add(new ListItem("Display Options", "0"));
        ddlStaffDisplayOptions.Items.Add(new ListItem("Week only", "1"));
        ddlStaffDisplayOptions.Items.Add(new ListItem("Week +,- 1", "2"));
        ddlStaffDisplayOptions.Items.Add(new ListItem("Week +,- 2", "3"));
        ddlStaffDisplayOptions.Items.Add(new ListItem("Week +,- 4", "4"));
        ddlStaffDisplayOptions.Items.Add(new ListItem("All Weeks", "5"));

        #endregion

        #region Select Week
        ddlWeek.Items.Clear();
        ddlWeek.Items.Add(new ListItem("Select Week", "0"));
        ddlWeek.Items.Add(new ListItem("Current Week", "53"));

        ddlStaffWeek.Items.Clear();
        ddlStaffWeek.Items.Add(new ListItem("Select Week", "0"));
        ddlStaffWeek.Items.Add(new ListItem("Current Week", "53"));


        for (int i = 52; i >= 1; i--)
        {
            ddlWeek.Items.Add(new ListItem("Week " + i + " ", i.ToString()));
            ddlStaffWeek.Items.Add(new ListItem("Week " + i + " ", i.ToString()));
        }

        #endregion

        #region Years
        ddlStartYear.Items.Clear();
        ddlEndYear.Items.Clear();
        ddlStaffYear.Items.Clear();

        tblUserWorkshifts uws = new tblUserWorkshifts();
        uws.getWorshiftYears();
        for (int i = 0; i < uws.RowCount; i++)
        {
            ddlStartYear.Items.Add(new ListItem(uws.IYear.ToString(), (i + 1).ToString()));
            ddlEndYear.Items.Add(new ListItem(uws.IYear.ToString(), (i + 1).ToString()));
            ddlStaffYear.Items.Add(new ListItem(uws.IYear.ToString(), (i + 1).ToString()));
            uws.MoveNext();
        }

        ddlStartYear.Items.Insert(0, new ListItem("Change Year", "0"));
        ddlEndYear.Items.Insert(0, new ListItem("Change Year", "0"));
        ddlStaffYear.Items.Insert(0, new ListItem("Select Year", "0"));
        #endregion

        #region Expense
        ddlExpense.Items.Clear();
        tblExpanseCategory expCat = new tblExpanseCategory();
        expCat.LoadAll();
        if (expCat.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlExpense, expCat.DefaultView, "sExpanseCategory", "pkExpanseCategoryID");
        }
        ddlExpense.Items.Insert(0, new ListItem("All Expenses", "0"));

        #endregion

        #region Suppliers

        tblSupplier sup = new tblSupplier();
        sup.GetDepartmentSuppliers(DepartmentID);
        if (sup.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlSuppliers, sup.DefaultView, "SBrandName", "PkSupplierID");
            ddlSuppliers.Items.Insert(0, new ListItem("Select suppliers", "0"));
        }

        #endregion

        #region Other Income
        ddlOtherIncome.Items.Clear();
        tblIncomTypes itypes = new tblIncomTypes();
        itypes.LoadAll();
        if (itypes.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlOtherIncome, itypes.DefaultView, "SIncomType", "PkIncomeTypeID");
        }
        ddlOtherIncome.Items.Insert(0, new ListItem("Select Other Income", "0"));

        #endregion

        #region BaseCat
        tblBaseCategories b = new tblBaseCategories();
        b.GetAllBase(DepartmentID);
        ddlBaseCat.Items.Clear();
        if (b.RowCount > 0)
        {
            commonMethods.FillDropDownList(ddlBaseCat, b.DefaultView, "CatagoryName", "PkBaseCategoryID");
        }
        ddlBaseCat.Items.Insert(0, new ListItem("Base Category", "0"));

        ddlSubCat.Items.Clear();
        ddlSubCat.Items.Insert(0, new ListItem("Sub Category", "0"));
        #endregion

        #region SubCat


        #endregion

        #region Product
        ddlProduct.Items.Insert(0, new ListItem("Select Product", "0"));
        #endregion

        ddlStaffSpecialty.Items.Clear();
        ddlStaffSpecialty.Items.Insert(0, new ListItem("Select Specialty", "0"));

        ddlStaffName.Items.Clear();
        ddlStaffName.Items.Insert(0, new ListItem("Select Name", "0"));






    }

    protected void lnkIncome_Click(object sender, EventArgs e)
    {
        divStaffInfo.Visible = false;
        divExpense.Visible = false;
        divOtherIncome.Visible = false;
        divSuppliers.Visible = false;
        divPositionsTop.Visible = false;
        divProducts.Visible = false;
        trStaff.Visible = false;
        trReset.Visible = true;
        imgBtnReset.Style.Add("float", "right");
        grdWeeks.Visible = true;
        showStatisticsTitle = "income";
        GetWeekStats();
    }
    protected void lnkRegister_Click(object sender, EventArgs e)
    {
        divStaffInfo.Visible = false;
        divExpense.Visible = false;
        divSuppliers.Visible = false;
        divOtherIncome.Visible = false;
        divPositionsTop.Visible = false;
        divProducts.Visible = false;
        trStaff.Visible = false;
        trReset.Visible = true;
        grdWeeks.Visible = true;
        imgBtnReset.Style.Add("float", "right");

        showStatisticsTitle = "register";
        GetWeekStats();
    }
    protected void lnkSalaries_Click(object sender, EventArgs e)
    {
        divStaffInfo.Visible = false;
        divExpense.Visible = false;
        divOtherIncome.Visible = false;
        divSuppliers.Visible = false;
        divPositionsTop.Visible = false;
        divProducts.Visible = false;
        trStaff.Visible = false;
        trReset.Visible = true;
        grdWeeks.Visible = true;
        imgBtnReset.Style.Add("float", "right");

        showStatisticsTitle = "salaries";
        GetWeekStats();
    }
    protected void lnkExpense_Click(object sender, EventArgs e)
    {
        divStaffInfo.Visible = false;
        divExpense.Visible = true;
        divOtherIncome.Visible = false;
        divSuppliers.Visible = false;
        imgBtnReset.Style.Add("float", "left");
        divPositionsTop.Visible = false;
        divProducts.Visible = false;
        trStaff.Visible = false;
        trReset.Visible = true;
        grdWeeks.Visible = true;

        showStatisticsTitle = "expense";
        GetWeekStats();
    }
    protected void lnkSupply_Click(object sender, EventArgs e)
    {
        divStaffInfo.Visible = false;
        divSuppliers.Visible = true;
        divExpense.Visible = false;
        divOtherIncome.Visible = false;
        divPositionsTop.Visible = false;
        divProducts.Visible = false;
        trStaff.Visible = false;
        trReset.Visible = true;
        imgBtnReset.Style.Add("float", "left");
        showStatisticsTitle = "supply";
        grdWeeks.Visible = true;
        GetWeekStats();

    }
    protected void lnkProduct_Click(object sender, EventArgs e)
    {
        divStaffInfo.Visible = false;
        divExpense.Visible = false;
        divOtherIncome.Visible = false;
        divSuppliers.Visible = false;
        divPositionsTop.Visible = false;
        divProducts.Visible = true;
        trStaff.Visible = false;
        imgBtnReset.Style.Add("float", "left");
        grdWeeks.Visible = false;
        trReset.Visible = true;
        showStatisticsTitle = "product";

    }
    protected void lnkPositionIncome_Click(object sender, EventArgs e)
    {
        divStaffInfo.Visible = false;
        divExpense.Visible = false;
        divOtherIncome.Visible = false;
        divSuppliers.Visible = false;
        divPositionsTop.Visible = true;
        divProducts.Visible = false;
        imgBtnReset.Style.Add("float", "right");
        trStaff.Visible = false;
        grdWeeks.Visible = false;
        trReset.Visible = true;

        showStatisticsTitle = "position";
        GetWeekStats();
    }
    protected void lnkOther_Click(object sender, EventArgs e)
    {
        divStaffInfo.Visible = false;
        divExpense.Visible = false;
        divSuppliers.Visible = false;
        divOtherIncome.Visible = true;
        divPositionsTop.Visible = false;
        divProducts.Visible = false;
        imgBtnReset.Style.Add("float", "left");
        trStaff.Visible = false;
        trReset.Visible = true;
        grdWeeks.Visible = true;
        showStatisticsTitle = "other";
        GetWeekStats();
    }
    protected void lnkStaff_Click(object sender, EventArgs e)
    {
        divStaffInfo.Visible = true;
        trStaff.Visible = true;
        trReset.Visible = false;
        divExpense.Visible = false;
        divOtherIncome.Visible = false;
        divPositionsTop.Visible = false;
        divSuppliers.Visible = false;
        divProducts.Visible = false;
        imgBtnReset.Style.Add("float", "right");

        grdWeeks.Visible = false;
        showStatisticsTitle = "staff";

        divYear.Visible = false;
        divContent.Visible = false;


        GetWeekStats();
    }

    protected void imgBtnApplyPosition_Click(object sender, ImageClickEventArgs e)
    {

        bool check = false;

        for (int i = 0; i < dtlPositions.Items.Count; i++)
        {
            CheckBox chk = dtlPositions.Items[i].FindControl("chkPosition") as CheckBox;
            HiddenField hidPosition = dtlPositions.Items[i].FindControl("hidPosition") as HiddenField;
            if (chk.Checked)
            {
                check = true;
                positions.Add(hidPosition.Value);
            }
        }
        if (!check)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "$(function(){alert('Please select at least 1 position');});", true);
        }
        else
        {
            grdWeeks.Visible = true;
            GetWeekStats();
        }
    }
    protected void imgBtnFilter_Click(object sender, EventArgs e)
    {
        grdWeeks.Visible = true;



        GetWeekStats();
    }
    protected void imgBtnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("Statistics.aspx");
    }

    protected void imgbtnClear_Click(object sender, EventArgs e)
    {
        BindDropDowns();
    }
    protected void imgbtnApply_Click(object sender, EventArgs e)
    {
        tblUsers u = new tblUsers();
        u.getuserInfoForStats(Convert.ToInt32(ddlStaffName.SelectedValue));
        if (u.RowCount > 0)
        {
            lblStaffName.Text = u.GetColumn("fullname").ToString();
            lblUserName.Text = u.GetColumn("fullname").ToString();
            lblEmail.Text = u.GetColumn("sEmail").ToString();
            lblmobile.Text = u.GetColumn("sMobilePhone").ToString();
            lbladdress.Text = u.GetColumn("useraddress").ToString();
            imgUser.Src = u.GetColumn("sImagePath").ToString();
            hidUserid.Value = ddlStaffName.SelectedValue;
            ankUser.HRef = "EditUser.aspx?id=" + ddlStaffName.SelectedValue;
        }


        grdWeeks.Visible = true;
        divStaffInfo.Visible = true;
        GetWeekStats();

        lblIncomeGrand.Text = commonMethods.ChangetToUK(TotalIncome.ToString("N"));
        lblSalaryGrand.Text = commonMethods.ChangetToUK(TotalSalary.ToString("N"));
        lblTipGrand.Text = commonMethods.ChangetToUK(TotalTip.ToString("N"));
        lblBonusGrand.Text = commonMethods.ChangetToUK(TotalBonus.ToString("N"));
        lblPenaltyGrand.Text = commonMethods.ChangetToUK(TotalPenalty.ToString("N"));
        lblLateGramd.Text = commonMethods.ChangetToUK(TotalLateHours.ToString("N"));


    }
    private DataTable GetWeek()
    {



        DataTable dTable = new DataTable();

        dTable.Columns.Add("weekNumber");
        dTable.Columns.Add("weeStartDate");
        dTable.Columns.Add("weekEndDate");


        if (ddlOptions.SelectedValue == "0")
        {
            int weeknum = 0;
            if (ddlWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlWeek.SelectedValue);

            weeknum = weeknum - 2;
            for (int i = 1; i <= 3; i++)
            {
                DataRow dRow = dTable.NewRow();
                dRow["weekNumber"] = weeknum;
                dTable.Rows.Add(dRow);
                weeknum += 1;
            }



        }
        else if (ddlOptions.SelectedValue == "1")
        {
            int weeknum = 0;
            if (ddlWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlWeek.SelectedValue);


            DataRow dRow = dTable.NewRow();
            dRow["weekNumber"] = weeknum;
            dTable.Rows.Add(dRow);



        }
        else if (ddlOptions.SelectedValue == "2")
        {
            int weeknum = 0;
            if (ddlWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlWeek.SelectedValue);

            weeknum = weeknum - 2;
            for (int i = 1; i <= 3; i++)
            {
                DataRow dRow = dTable.NewRow();
                weeknum += 1;
                dRow["weekNumber"] = weeknum;
                dTable.Rows.Add(dRow);
            }


        }
        else if (ddlOptions.SelectedValue == "3")
        {
            int weeknum = 0;
            if (ddlWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlWeek.SelectedValue);

            weeknum = weeknum - 3;
            for (int i = 1; i <= 5; i++)
            {
                DataRow dRow = dTable.NewRow();
                weeknum += 1;
                dRow["weekNumber"] = weeknum;
                dTable.Rows.Add(dRow);
            }


        }
        else if (ddlOptions.SelectedValue == "4")
        {
            int weeknum = 0;
            if (ddlWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlWeek.SelectedValue);

            weeknum = weeknum - 5;
            for (int i = 1; i <= 9; i++)
            {
                DataRow dRow = dTable.NewRow();
                weeknum += 1;
                //if (weeknum >= 53)
                //{
                //    dRow["weekNumber"] = weeknum - 52;
                //}
                //else 
                //{
                dRow["weekNumber"] = weeknum;
                // }

                dTable.Rows.Add(dRow);
            }


        }
        else if (ddlOptions.SelectedValue == "5")
        {
            int weeknum = 0;
            if (ddlWeek.SelectedValue == "0")
                weeknum = 53;
            else if (ddlWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlWeek.SelectedValue);

            int weekcounnts = weeknum;
            for (int i = 1; i <= weekcounnts; i++)
            {
                DataRow dRow = dTable.NewRow();
                weeknum -= 1;
                dRow["weekNumber"] = weeknum;
                dTable.Rows.Add(dRow);
            }


        }
        DataView dv = dTable.DefaultView;
        if (ddlOptions.SelectedValue != "5")
        {
            // dv.Sort = "weekNumber DESC";
        }

        return dv.ToTable();
    }
    //private DataTable GetWeek()
    //{



    //    DataTable dTable = new DataTable();

    //    dTable.Columns.Add("weekNumber");
    //    dTable.Columns.Add("weeStartDate");
    //    dTable.Columns.Add("weekEndDate");


    //    if (ddlOptions.SelectedValue == "0")
    //    {
    //        int weeknum = 0;
    //        if (ddlWeek.SelectedValue == "0")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else if (ddlWeek.SelectedValue == "53")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else
    //            weeknum = Convert.ToInt32(ddlWeek.SelectedItem);

    //        weeknum = weeknum - 2;
    //        for (int i = 1; i <= 3; i++)
    //        {
    //            DataRow dRow = dTable.NewRow();
    //            weeknum += 1;
    //            dRow["weekNumber"] = weeknum;
    //            dTable.Rows.Add(dRow);
    //        }



    //    }
    //    else if (ddlOptions.SelectedValue == "1")
    //    {
    //        int weeknum = 0;
    //        if (ddlWeek.SelectedValue == "0")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else if (ddlWeek.SelectedValue == "53")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else
    //            weeknum = Convert.ToInt32(ddlWeek.SelectedItem);


    //        DataRow dRow = dTable.NewRow();
    //        dRow["weekNumber"] = weeknum;
    //        dTable.Rows.Add(dRow);



    //    }
    //    else if (ddlOptions.SelectedValue == "2")
    //    {
    //        int weeknum = 0;
    //        if (ddlWeek.SelectedValue == "0")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else if (ddlWeek.SelectedValue == "53")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else
    //            weeknum = Convert.ToInt32(ddlWeek.SelectedItem);

    //        weeknum = weeknum - 2;
    //        for (int i = 1; i <= 3; i++)
    //        {
    //            DataRow dRow = dTable.NewRow();
    //            weeknum += 1;
    //            dRow["weekNumber"] = weeknum;
    //            dTable.Rows.Add(dRow);
    //        }


    //    }
    //    else if (ddlOptions.SelectedValue == "3")
    //    {
    //        int weeknum = 0;
    //        if (ddlWeek.SelectedValue == "0")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else if (ddlWeek.SelectedValue == "53")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else
    //            weeknum = Convert.ToInt32(ddlWeek.SelectedItem);

    //        weeknum = weeknum - 3;
    //        for (int i = 1; i <= 5; i++)
    //        {
    //            DataRow dRow = dTable.NewRow();
    //            weeknum += 1;
    //            dRow["weekNumber"] = weeknum;
    //            dTable.Rows.Add(dRow);
    //        }


    //    }
    //    else if (ddlOptions.SelectedValue == "4")
    //    {
    //        int weeknum = 0;
    //        if (ddlWeek.SelectedValue == "0")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else if (ddlWeek.SelectedValue == "53")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else
    //            weeknum = Convert.ToInt32(ddlWeek.SelectedItem);

    //        weeknum = weeknum - 5;
    //        for (int i = 1; i <= 9; i++)
    //        {
    //            DataRow dRow = dTable.NewRow();
    //            weeknum += 1;
    //            dRow["weekNumber"] = weeknum;
    //            dTable.Rows.Add(dRow);
    //        }


    //    }
    //    else if (ddlOptions.SelectedValue == "5")
    //    {
    //        int weeknum = 0;
    //        if (ddlWeek.SelectedValue == "0")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else if (ddlWeek.SelectedValue == "53")
    //            weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
    //        else
    //            weeknum = Convert.ToInt32(ddlWeek.SelectedItem);


    //        for (int i = 1; i <= 52; i++)
    //        {
    //            DataRow dRow = dTable.NewRow();
    //            dRow["weekNumber"] = i;
    //            dTable.Rows.Add(dRow);
    //        }


    //    }
    //    return dTable;
    //}
    private DataTable GetWeekForStaff()
    {

        DataTable dTable = new DataTable();

        dTable.Columns.Add("weekNumber");
        dTable.Columns.Add("weeStartDate");
        dTable.Columns.Add("weekEndDate");


        if (ddlStaffDisplayOptions.SelectedValue == "0")
        {
            int weeknum = 0;
            if (ddlStaffWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlStaffWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlStaffWeek.SelectedItem);

            weeknum = weeknum - 2;
            for (int i = 1; i <= 3; i++)
            {
                DataRow dRow = dTable.NewRow();
                weeknum += 1;
                dRow["weekNumber"] = weeknum;
                dTable.Rows.Add(dRow);
            }



        }
        else if (ddlStaffDisplayOptions.SelectedValue == "1")
        {
            int weeknum = 0;
            if (ddlStaffWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlStaffWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlStaffWeek.SelectedItem);


            DataRow dRow = dTable.NewRow();
            dRow["weekNumber"] = weeknum;
            dTable.Rows.Add(dRow);



        }
        else if (ddlStaffDisplayOptions.SelectedValue == "2")
        {
            int weeknum = 0;
            if (ddlStaffWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlStaffWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlStaffWeek.SelectedItem);

            weeknum = weeknum - 2;
            for (int i = 1; i <= 3; i++)
            {
                DataRow dRow = dTable.NewRow();
                weeknum += 1;
                dRow["weekNumber"] = weeknum;
                dTable.Rows.Add(dRow);
            }


        }
        else if (ddlStaffDisplayOptions.SelectedValue == "3")
        {
            int weeknum = 0;
            if (ddlStaffWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlStaffWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlStaffWeek.SelectedItem);

            weeknum = weeknum - 3;
            for (int i = 1; i <= 5; i++)
            {
                DataRow dRow = dTable.NewRow();
                weeknum += 1;
                dRow["weekNumber"] = weeknum;
                dTable.Rows.Add(dRow);
            }


        }
        else if (ddlStaffDisplayOptions.SelectedValue == "4")
        {
            int weeknum = 0;
            if (ddlStaffWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlStaffWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlStaffWeek.SelectedItem);

            weeknum = weeknum - 5;
            for (int i = 1; i <= 9; i++)
            {
                DataRow dRow = dTable.NewRow();
                weeknum += 1;
                dRow["weekNumber"] = weeknum;
                dTable.Rows.Add(dRow);
            }


        }
        else if (ddlStaffDisplayOptions.SelectedValue == "5")
        {
            int weeknum = 0;
            if (ddlStaffWeek.SelectedValue == "0")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else if (ddlStaffWeek.SelectedValue == "53")
                weeknum = commonMethods.GetWeeknumber(DateTime.Now.Date);
            else
                weeknum = Convert.ToInt32(ddlStaffWeek.SelectedItem);


            for (int i = 1; i <= 52; i++)
            {
                DataRow dRow = dTable.NewRow();
                dRow["weekNumber"] = i;
                dTable.Rows.Add(dRow);
            }


        }
        return dTable;
    }

    protected void ddlOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (showStatisticsTitle == "position")
        {
            bool check = false;

            for (int i = 0; i < dtlPositions.Items.Count; i++)
            {
                CheckBox chk = dtlPositions.Items[i].FindControl("chkPosition") as CheckBox;
                HiddenField hidPosition = dtlPositions.Items[i].FindControl("hidPosition") as HiddenField;
                if (chk.Checked)
                {
                    check = true;
                    positions.Add(hidPosition.Value);
                }
            }
            if (!check)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "$(function(){alert('Please select at least 1 position');});", true);
            }
            else
            {
                grdWeeks.Visible = true;
                GetWeekStats();
            }
        }
        else
        {
            GetWeekStats();
        }
    }
    protected void ddlWeek_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (showStatisticsTitle == "position")
        {
            bool check = false;

            for (int i = 0; i < dtlPositions.Items.Count; i++)
            {
                CheckBox chk = dtlPositions.Items[i].FindControl("chkPosition") as CheckBox;
                HiddenField hidPosition = dtlPositions.Items[i].FindControl("hidPosition") as HiddenField;
                if (chk.Checked)
                {
                    check = true;
                    positions.Add(hidPosition.Value);
                }
            }
            if (!check)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "$(function(){alert('Please select at least 1 position');});", true);
            }
            else
            {
                grdWeeks.Visible = true;
                GetWeekStats();
            }
        }
        else
        {
            GetWeekStats();
        }
    }
    protected void ddlStartYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (showStatisticsTitle == "position")
        {
            bool check = false;

            for (int i = 0; i < dtlPositions.Items.Count; i++)
            {
                CheckBox chk = dtlPositions.Items[i].FindControl("chkPosition") as CheckBox;
                HiddenField hidPosition = dtlPositions.Items[i].FindControl("hidPosition") as HiddenField;
                if (chk.Checked)
                {
                    check = true;
                    positions.Add(hidPosition.Value);
                }
            }
            if (!check)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "$(function(){alert('Please select at least 1 position');});", true);
            }
            else
            {
                grdWeeks.Visible = true;
                GetWeekStats();
            }
        }
        else
        {
            GetWeekStats();
        }
    }
    protected void ddlEndYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (showStatisticsTitle == "position")
        {
            bool check = false;

            for (int i = 0; i < dtlPositions.Items.Count; i++)
            {
                CheckBox chk = dtlPositions.Items[i].FindControl("chkPosition") as CheckBox;
                HiddenField hidPosition = dtlPositions.Items[i].FindControl("hidPosition") as HiddenField;
                if (chk.Checked)
                {
                    check = true;
                    positions.Add(hidPosition.Value);
                }
            }
            if (!check)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "$(function(){alert('Please select at least 1 position');});", true);
            }
            else
            {
                grdWeeks.Visible = true;
                GetWeekStats();
            }
        }
        else
        {
            GetWeekStats();
        }
    }
    protected void ddlExpense_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetWeekStats();
    }
    protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetWeekStats();
    }
    protected void ddlOtherIncome_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetWeekStats();
    }
    protected void ddlBaseCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBaseCat.SelectedValue != "0")
        {
            tblSubCategories sub = new tblSubCategories();
            sub.GetActiveSubCat(Convert.ToInt32(ddlBaseCat.SelectedValue));
            ddlSubCat.Items.Clear();
            if (sub.RowCount > 0)
            {
                commonMethods.FillDropDownList(ddlSubCat, sub.DefaultView, "cSubCategoryName", "pkSubCategoryID");
            }
            ddlSubCat.Items.Insert(0, new ListItem("Sub Category", "0"));
        }
    }
    protected void ddlSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubCat.SelectedValue != "0")
        {
            tblProducts prod = new tblProducts();
            prod.GetProducts(Convert.ToInt32(ddlSubCat.SelectedValue));
            if (prod.RowCount > 0)
            {
                commonMethods.FillDropDownList(ddlProduct, prod.DefaultView, "sProductName", "PkProductID");
                ddlProduct.Items.Insert(0, new ListItem("Select Product", "0"));
            }
            else
            {
                ddlProduct.Items.Clear();
                ddlProduct.Items.Insert(0, new ListItem("Select Product", "0"));
            }
        }
    }


    #region Staff DropDowns

    protected void ddlStaffYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblSpeciality s = new tblSpeciality();
        if (ddlStaffYear.SelectedValue != "0")
        {
            s.getSpecialityForStatistics(Convert.ToInt32(ddlStaffYear.SelectedItem.Text), DepartmentID);
            if (s.RowCount > 0)
            {
                ddlStaffSpecialty.Items.Clear();
                commonMethods.FillDropDownList(ddlStaffSpecialty, s.DefaultView, "sspecialityName", "pkspecialityTypeid");
                ddlStaffSpecialty.Items.Insert(0, new ListItem("Select Specilty", "0"));
            }
        }

    }
    protected void ddlStaffSpecialty_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblUsers u = new tblUsers();
        if (ddlStaffSpecialty.SelectedValue != "0")
        {
            u.getStaffNameForStatistics(Convert.ToInt32(ddlStaffYear.SelectedItem.Text), DepartmentID, Convert.ToInt32(ddlStaffSpecialty.SelectedValue));
            if (u.RowCount > 0)
            {
                ddlStaffName.Items.Clear();
                commonMethods.FillDropDownList(ddlStaffName, u.DefaultView, "FullName", "pkuserid");
                ddlStaffName.Items.Insert(0, new ListItem("Select Name", "0"));
            }
        }
    }


    #endregion

    protected void grdWeeks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                Label lblWeekTitle = e.Row.FindControl("lblWeekTitle") as Label;
                Label lblWeekTotal1 = e.Row.FindControl("lblWeekTotal1") as Label;
                Label lblWeekTotal2 = e.Row.FindControl("lblWeekTotal2") as Label;

                HtmlTable tblSave = e.Row.FindControl("tblSave") as HtmlTable;
                HtmlTable tblStaff = e.Row.FindControl("tblStaff") as HtmlTable;





                GridView grdWeekDetail1 = e.Row.FindControl("grdWeekDetail1") as GridView;
                GridView grdWeekDetail2 = e.Row.FindControl("grdWeekDetail2") as GridView;

                GridView grdSalaries = e.Row.FindControl("grdSalaries") as GridView;

                if (Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "weeknumber").ToString()) >= 53)
                {
                    lblWeekTitle.Text = "Week #" + (Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "weeknumber").ToString()) - 52);
                }
                else if (Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "weeknumber").ToString()) <= 0)
                {
                    lblWeekTitle.Text = "Week #" + (Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "weeknumber").ToString()) + 52);

                }
                else
                {

                    lblWeekTitle.Text = "Week #" + DataBinder.GetPropertyValue(e.Row.DataItem, "weeknumber").ToString();
                }

                //lblWeekTitle.Text = "Week #" + DataBinder.GetPropertyValue(e.Row.DataItem, "weeknumber").ToString();

                if (showStatisticsTitle == "income")
                {
                    getIncomeStatistics(ref drv, ref grdWeekDetail1, ref grdWeekDetail2, ref lblWeekTotal1, ref lblWeekTotal2);
                }
                else if (showStatisticsTitle == "register")
                {
                    getRegisterStatistics(ref drv, ref grdWeekDetail1, ref grdWeekDetail2, ref lblWeekTotal1, ref lblWeekTotal2);
                }
                else if (showStatisticsTitle == "salaries")
                {
                    getSalaryStatistics(ref drv, ref grdWeekDetail1, ref grdWeekDetail2, ref lblWeekTotal1, ref lblWeekTotal2);
                }
                else if (showStatisticsTitle == "expense")
                {
                    getExpenseStatistics(ref drv, ref grdWeekDetail1, ref grdWeekDetail2, ref lblWeekTotal1, ref lblWeekTotal2);
                }
                else if (showStatisticsTitle == "supply")
                {
                    getSupplyStatistics(ref drv, ref grdWeekDetail1, ref grdWeekDetail2, ref lblWeekTotal1, ref lblWeekTotal2);
                }
                else if (showStatisticsTitle == "product")
                {
                    getProductsStatistics(ref drv, ref grdWeekDetail1, ref grdWeekDetail2, ref lblWeekTotal1, ref lblWeekTotal2);
                }
                else if (showStatisticsTitle == "position")
                {
                    getPositionIncomeStatistics(ref drv, ref grdWeekDetail1, ref grdWeekDetail2, ref lblWeekTotal1, ref lblWeekTotal2);
                }
                else if (showStatisticsTitle == "other")
                {
                    getOtherIncomeStatistics(ref drv, ref grdWeekDetail1, ref grdWeekDetail2, ref lblWeekTotal1, ref lblWeekTotal2);
                }
                else if (showStatisticsTitle == "staff")
                {

                    Label lblIncome_WeekTotal = e.Row.FindControl("lblIncome_WeekTotal") as Label;
                    Label lblSalary_WeekTotal = e.Row.FindControl("lblSalary_WeekTotal") as Label;
                    Label lblTips_WeekTotal = e.Row.FindControl("lblTips_WeekTotal") as Label;
                    Label lblBonus_WeekTotal = e.Row.FindControl("lblBonus_WeekTotal") as Label;
                    Label lblPenalty_WeekTotal = e.Row.FindControl("lblPenalty_WeekTotal") as Label;
                    Label lblAdvance_WeekTotal = e.Row.FindControl("lblAdvance_WeekTotal") as Label;
                    Label lblLate_WeekTotal = e.Row.FindControl("lblLate_WeekTotal") as Label;

                    tblSave.Visible = false;
                    tblStaff.Visible = true;

                    getStaffStatistics(ref drv, ref grdSalaries, ref lblWeekTotal1, ref lblWeekTotal2);

                    lblIncome_WeekTotal.Text = commonMethods.ChangetToUK(weekIncome.ToString("N")) + " €";
                    lblSalary_WeekTotal.Text = commonMethods.ChangetToUK(weekSalary.ToString("N")) + " €";
                    lblTips_WeekTotal.Text = commonMethods.ChangetToUK(weekTip.ToString("N")) + " €";
                    lblBonus_WeekTotal.Text = commonMethods.ChangetToUK(weekBonus.ToString("N")) + " €";
                    lblPenalty_WeekTotal.Text = commonMethods.ChangetToUK(weekPenalty.ToString("N")) + " €";
                    lblAdvance_WeekTotal.Text = commonMethods.ChangetToUK(weekAdvance.ToString("N")) + " €";
                    lblLate_WeekTotal.Text = weekLateHours.ToString();

                    TotalIncome += weekIncome;
                    TotalSalary += weekSalary;
                    TotalTip += weekTip;
                    TotalBonus += weekBonus;
                    TotalPenalty += weekPenalty;
                    TotalAdvance += weekAdvance;
                    TotalLateHours += weekLateHours;

                    weekIncome = 0.0;
                    weekSalary = 0.0;
                    weektotal = 0.0;
                    weekBonus = 0.0;
                    weekPenalty = 0.0;
                    weekAdvance = 0.0;
                    weekLateHours = 0;


                }

                #region Difference & Percentage

                Label lblSaturdayDiff = e.Row.FindControl("lblSaturdayDiff") as Label;
                Label lblFridayDiff = e.Row.FindControl("lblFridayDiff") as Label;
                Label lblThursdayDiff = e.Row.FindControl("lblThursdayDiff") as Label;
                Label lblWednesdayDiff = e.Row.FindControl("lblWednesdayDiff") as Label;
                Label lblTuesdayDiff = e.Row.FindControl("lblTuesdayDiff") as Label;
                Label lblMondayDiff = e.Row.FindControl("lblMondayDiff") as Label;
                Label lblSundayDiff = e.Row.FindControl("lblSundayDiff") as Label;

                Label lblDifference = e.Row.FindControl("lblDifference") as Label;
                HtmlContainerControl divDifference = e.Row.FindControl("divDifference") as HtmlContainerControl;

                double diff = 0.0;

                Label lblSaturdayPer = e.Row.FindControl("lblSaturdayPer") as Label;
                Label lblFridayPer = e.Row.FindControl("lblFridayPer") as Label;
                Label lblThursdayPer = e.Row.FindControl("lblThursdayPer") as Label;
                Label lblWednesdayPer = e.Row.FindControl("lblWednesdayPer") as Label;
                Label lblTuesdayPer = e.Row.FindControl("lblTuesdayPer") as Label;
                Label lblMondayPer = e.Row.FindControl("lblMondayPer") as Label;
                Label lblSundayPer = e.Row.FindControl("lblSundayPer") as Label;

                Label lblPercentage = e.Row.FindControl("lblPercentage") as Label;
                HtmlContainerControl divPercentage = e.Row.FindControl("divPercentage") as HtmlContainerControl;

                double per = 0.0;

                for (int i = 0; i < arrLeftValue.Count; i++)
                {
                    if (i == 0)
                    {
                        #region Saturday Difference & Percentage
                        diff = Convert.ToDouble(arrRightValue[i]) - Convert.ToDouble(arrLeftValue[i]);
                        lblSaturdayDiff.Text = commonMethods.ChangetToUK(diff.ToString("N")) + " €";
                        weekDifference += diff;
                        if (Convert.ToDouble(arrLeftValue[i]) == 0 && Convert.ToDouble(arrRightValue[i]) != 0)
                            per = 100;
                        else
                            per = (diff * Convert.ToDouble(arrLeftValue[i])) / 100;
                        lblSaturdayPer.Text = commonMethods.ChangetToUK(per.ToString("N")) + " %";
                        weekPercentage += per;

                        if (diff > 0)
                        {
                            lblSaturdayDiff.Text = "+ " + lblSaturdayDiff.Text;
                            lblSaturdayPer.Text = "+ " + lblSaturdayPer.Text;

                            lblSaturdayDiff.Style.Add("color", "Green");
                            lblSaturdayPer.Style.Add("color", "Green");
                        }
                        else if (diff < 0)
                        {
                            lblSaturdayDiff.Text = "- " + lblSaturdayDiff.Text;
                            lblSaturdayPer.Text = "- " + lblSaturdayPer.Text;

                            lblSaturdayDiff.Style.Add("color", "Red");
                            lblSaturdayPer.Style.Add("color", "Red");
                        }
                        else
                        {
                            lblSaturdayDiff.Style.Add("color", "Gray");
                            lblSaturdayPer.Style.Add("color", "Gray");
                        }
                        #endregion
                    }
                    else if (i == 1)
                    {
                        #region Friday Difference & Percentage
                        diff = Convert.ToDouble(arrRightValue[i]) - Convert.ToDouble(arrLeftValue[i]);
                        lblFridayDiff.Text = commonMethods.ChangetToUK(diff.ToString("N")) + " €";
                        weekDifference += diff;
                        if (Convert.ToDouble(arrLeftValue[i]) == 0 && Convert.ToDouble(arrRightValue[i]) != 0)
                            per = 100;
                        else
                            per = (diff * Convert.ToDouble(arrLeftValue[i])) / 100;
                        lblFridayPer.Text = commonMethods.ChangetToUK(per.ToString("N")) + " %";
                        weekPercentage += per;

                        if (diff > 0)
                        {
                            lblFridayDiff.Text = "+ " + lblFridayDiff.Text;
                            lblFridayPer.Text = "+ " + lblFridayPer.Text;

                            lblFridayDiff.Style.Add("color", "Green");
                            lblFridayPer.Style.Add("color", "Green");
                        }
                        else if (diff < 0)
                        {
                            lblFridayDiff.Text = "- " + lblFridayDiff.Text;
                            lblFridayPer.Text = "- " + lblFridayPer.Text;

                            lblFridayDiff.Style.Add("color", "Red");
                            lblFridayPer.Style.Add("color", "Red");
                        }
                        else
                        {
                            lblFridayDiff.Style.Add("color", "Gray");
                            lblFridayPer.Style.Add("color", "Gray");
                        }
                        #endregion
                    }
                    else if (i == 2)
                    {
                        #region Thursday Difference & Percentage
                        diff = Convert.ToDouble(arrRightValue[i]) - Convert.ToDouble(arrLeftValue[i]);
                        lblThursdayDiff.Text = commonMethods.ChangetToUK(diff.ToString("N")) + " €";
                        weekDifference += diff;
                        if (Convert.ToDouble(arrLeftValue[i]) == 0 && Convert.ToDouble(arrRightValue[i]) != 0)
                            per = 100;
                        else
                            per = (diff * Convert.ToDouble(arrLeftValue[i])) / 100;
                        lblThursdayPer.Text = commonMethods.ChangetToUK(per.ToString("N")) + " %";
                        weekPercentage += per;

                        if (diff > 0)
                        {
                            lblThursdayDiff.Text = "+ " + lblThursdayDiff.Text;
                            lblThursdayPer.Text = "+ " + lblThursdayPer.Text;

                            lblThursdayDiff.Style.Add("color", "Green");
                            lblThursdayPer.Style.Add("color", "Green");
                        }
                        else if (diff < 0)
                        {
                            lblThursdayDiff.Text = "- " + lblThursdayDiff.Text;
                            lblThursdayPer.Text = "- " + lblThursdayPer.Text;

                            lblThursdayDiff.Style.Add("color", "Red");
                            lblThursdayPer.Style.Add("color", "Red");
                        }
                        else
                        {
                            lblThursdayDiff.Style.Add("color", "Gray");
                            lblThursdayPer.Style.Add("color", "Gray");
                        }
                        #endregion
                    }
                    else if (i == 3)
                    {
                        #region Wednesday Difference & Percentage
                        diff = Convert.ToDouble(arrRightValue[i]) - Convert.ToDouble(arrLeftValue[i]);
                        lblWednesdayDiff.Text = commonMethods.ChangetToUK(diff.ToString("N")) + " €";
                        weekDifference += diff;
                        if (Convert.ToDouble(arrLeftValue[i]) == 0 && Convert.ToDouble(arrRightValue[i]) != 0)
                            per = 100;
                        else
                            per = (diff * Convert.ToDouble(arrLeftValue[i])) / 100;
                        lblWednesdayPer.Text = commonMethods.ChangetToUK(per.ToString("N")) + " %";
                        weekPercentage += per;

                        if (diff > 0)
                        {
                            lblWednesdayDiff.Text = "+ " + lblWednesdayDiff.Text;
                            lblWednesdayPer.Text = "+ " + lblWednesdayPer.Text;

                            lblWednesdayDiff.Style.Add("color", "Green");
                            lblWednesdayPer.Style.Add("color", "Green");
                        }
                        else if (diff < 0)
                        {
                            lblWednesdayDiff.Text = "- " + lblWednesdayDiff.Text;
                            lblWednesdayPer.Text = "- " + lblWednesdayPer.Text;

                            lblWednesdayDiff.Style.Add("color", "Red");
                            lblWednesdayPer.Style.Add("color", "Red");
                        }
                        else
                        {
                            lblWednesdayDiff.Style.Add("color", "Gray");
                            lblWednesdayPer.Style.Add("color", "Gray");
                        }
                        #endregion
                    }
                    else if (i == 4)
                    {
                        #region Tuesday Difference & Percentage
                        diff = Convert.ToDouble(arrRightValue[i]) - Convert.ToDouble(arrLeftValue[i]);
                        lblTuesdayDiff.Text = commonMethods.ChangetToUK(diff.ToString("N")) + " €";
                        weekDifference += diff;
                        if (Convert.ToDouble(arrLeftValue[i]) == 0 && Convert.ToDouble(arrRightValue[i]) != 0)
                            per = 100;
                        else
                            per = (diff * Convert.ToDouble(arrLeftValue[i])) / 100;
                        lblTuesdayPer.Text = commonMethods.ChangetToUK(per.ToString("N")) + " %";
                        weekPercentage += per;

                        if (diff > 0)
                        {
                            lblTuesdayDiff.Text = "+ " + lblTuesdayDiff.Text;
                            lblTuesdayPer.Text = "+ " + lblTuesdayPer.Text;

                            lblTuesdayDiff.Style.Add("color", "Green");
                            lblTuesdayPer.Style.Add("color", "Green");
                        }
                        else if (diff < 0)
                        {
                            lblTuesdayDiff.Text = "- " + lblTuesdayDiff.Text;
                            lblTuesdayPer.Text = "- " + lblTuesdayPer.Text;

                            lblTuesdayDiff.Style.Add("color", "Red");
                            lblTuesdayPer.Style.Add("color", "Red");
                        }
                        else
                        {
                            lblTuesdayDiff.Style.Add("color", "Gray");
                            lblTuesdayPer.Style.Add("color", "Gray");
                        }
                        #endregion
                    }
                    else if (i == 5)
                    {
                        #region Monday Difference & Percentage
                        diff = Convert.ToDouble(arrRightValue[i]) - Convert.ToDouble(arrLeftValue[i]);
                        lblMondayDiff.Text = commonMethods.ChangetToUK(diff.ToString("N")) + " €";
                        weekDifference += diff;
                        if (Convert.ToDouble(arrLeftValue[i]) == 0 && Convert.ToDouble(arrRightValue[i]) != 0)
                            per = 100;
                        else
                            per = (diff * Convert.ToDouble(arrLeftValue[i])) / 100;
                        lblMondayPer.Text = commonMethods.ChangetToUK(per.ToString("N")) + " %";
                        weekPercentage += per;

                        if (diff > 0)
                        {
                            lblMondayDiff.Text = "+ " + lblMondayDiff.Text;
                            lblMondayPer.Text = "+ " + lblMondayPer.Text;

                            lblMondayDiff.Style.Add("color", "Green");
                            lblMondayPer.Style.Add("color", "Green");
                        }
                        else if (diff < 0)
                        {
                            lblMondayDiff.Text = "- " + lblMondayDiff.Text;
                            lblMondayPer.Text = "- " + lblMondayPer.Text;

                            lblMondayDiff.Style.Add("color", "Red");
                            lblMondayPer.Style.Add("color", "Red");
                        }
                        else
                        {
                            lblMondayDiff.Style.Add("color", "Gray");
                            lblMondayPer.Style.Add("color", "Gray");
                        }
                        #endregion
                    }
                    else if (i == 6)
                    {
                        #region Sunday Difference & Percentage
                        diff = Convert.ToDouble(arrRightValue[i]) - Convert.ToDouble(arrLeftValue[i]);
                        lblSundayDiff.Text = commonMethods.ChangetToUK(diff.ToString("N")) + " €";
                        weekDifference += diff;
                        if (Convert.ToDouble(arrLeftValue[i]) == 0 && Convert.ToDouble(arrRightValue[i]) != 0)
                            per = 100;
                        else
                            per = (diff * Convert.ToDouble(arrLeftValue[i])) / 100;
                        lblSundayPer.Text = commonMethods.ChangetToUK(per.ToString("N")) + " %";
                        weekPercentage += per;

                        if (diff > 0)
                        {
                            lblSundayDiff.Text = "+ " + lblSundayDiff.Text;
                            lblSundayPer.Text = "+ " + lblSundayPer.Text;


                            lblSundayDiff.Style.Add("color", "Green");
                            lblSundayPer.Style.Add("color", "Green");
                        }
                        else if (diff < 0)
                        {
                            lblSundayDiff.Text = "- " + lblSundayDiff.Text;
                            lblSundayPer.Text = "- " + lblSundayDiff.Text;

                            lblSundayDiff.Style.Add("color", "Red");
                            lblSundayPer.Style.Add("color", "Red");
                        }
                        else
                        {
                            lblSundayDiff.Style.Add("color", "Gray");
                            lblSundayPer.Style.Add("color", "Gray");
                        }
                        #endregion
                    }
                }
                lblDifference.Text = commonMethods.ChangetToUK(weekDifference.ToString("N")) + " €";
                lblPercentage.Text = commonMethods.ChangetToUK(weekPercentage.ToString("N")) + " %";

                if (weekDifference > 0)
                {
                    divDifference.Style.Add("background", "url(../Images/textbox_light_green.png);");
                    divPercentage.Style.Add("background", "url(../Images/textbox_light_green.png);");

                    //lblDifference.Style.Add("color", "White");
                    //lblPercentage.Style.Add("color", "White");
                }
                else if (weekDifference < 0)
                {
                    divDifference.Style.Add("background", "url(../Images/textbox_pink.png);");
                    divPercentage.Style.Add("background", "url(../Images/textbox_pink.png);");
                    //lblDifference.Style.Add("color", "White");
                }
                else
                {
                    divDifference.Style.Add("background", "url(../Images/textbox_115.png);");
                    divPercentage.Style.Add("background", "url(../Images/textbox_115.png);");
                    //lblDifference.Style.Add("color", "Gray");
                }

                diff = 0.0;
                per = 0.0;

                grandDiff += weekDifference;
                lblDifferenceValue.Text = commonMethods.ChangetToUK(grandDiff.ToString("N")) + " €";
                weekDifference = 0.0;

                grandPer += weekPercentage;
                lblDifferencePercentage.Text = commonMethods.ChangetToUK(grandPer.ToString("N")) + " %";
                weekPercentage = 0.0;

                arrLeftValue.Clear();
                arrRightValue.Clear();

                #endregion
            }
        }
        catch (Exception ex)
        { }
    }

    private void getIncomeStatistics(ref DataRowView drv, ref GridView grdWeekDetail1, ref GridView grdWeekDetail2, ref Label lblWeekTotal1, ref Label lblWeekTotal2)
    {
        tblUserWorkshifts uws = new tblUserWorkshifts();
        if (ddlStartYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID);
        }
        else if (ddlStartYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID);
        }
        grdWeekDetail1.DataSource = uws.DefaultView;
        grdWeekDetail1.DataBind();
        lblWeekTotal1.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";

        weektotal = 0.0;
        uws.FlushData();
        if (ddlEndYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID);
        }
        else if (ddlEndYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID);
        }
        grdWeekDetail2.DataSource = uws.DefaultView;
        grdWeekDetail2.DataBind();
        lblWeekTotal2.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;
    }
    private void getRegisterStatistics(ref DataRowView drv, ref GridView grdWeekDetail1, ref GridView grdWeekDetail2, ref Label lblWeekTotal1, ref Label lblWeekTotal2)
    {
        tblRegisterHistory regh = new tblRegisterHistory();

        if (ddlStartYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            regh.GetRegisterValForStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]));
        }
        else if (ddlStartYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            regh.GetRegisterValForStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]));
        }
        grdWeekDetail1.DataSource = regh.DefaultView;
        grdWeekDetail1.DataBind();
        lblWeekTotal1.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;

        regh.FlushData();
        if (ddlEndYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            regh.GetRegisterValForStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]));
        }
        else if (ddlEndYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            regh.GetRegisterValForStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]));
        }
        grdWeekDetail2.DataSource = regh.DefaultView;
        grdWeekDetail2.DataBind();
        lblWeekTotal2.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;
    }
    private void getSalaryStatistics(ref DataRowView drv, ref GridView grdWeekDetail1, ref GridView grdWeekDetail2, ref Label lblWeekTotal1, ref Label lblWeekTotal2)
    {
        tblUserWorkshifts uws = new tblUserWorkshifts();
        if (ddlStartYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetSalaryWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID);
        }
        else if (ddlStartYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetSalaryWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID);
        }
        grdWeekDetail1.DataSource = uws.DefaultView;
        grdWeekDetail1.DataBind();
        lblWeekTotal1.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;


        uws.FlushData();
        if (ddlEndYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetSalaryWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID);
        }
        else if (ddlEndYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetSalaryWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID);
        }
        grdWeekDetail2.DataSource = uws.DefaultView;
        grdWeekDetail2.DataBind();
        lblWeekTotal2.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;
    }
    private void getExpenseStatistics(ref DataRowView drv, ref GridView grdWeekDetail1, ref GridView grdWeekDetail2, ref Label lblWeekTotal1, ref Label lblWeekTotal2)
    {
        tblExpanses expense = new tblExpanses();
        if (ddlStartYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            expense.GetExpenseWeekStats(w_start, w_end, Convert.ToInt32(ddlExpense.SelectedValue));
        }
        else if (ddlStartYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            expense.GetExpenseWeekStats(w_start, w_end, Convert.ToInt32(ddlExpense.SelectedValue));
        }
        grdWeekDetail1.DataSource = expense.DefaultView;
        grdWeekDetail1.DataBind();
        lblWeekTotal1.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;


        expense.FlushData();
        if (ddlEndYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            expense.GetExpenseWeekStats(w_start, w_end, Convert.ToInt32(ddlExpense.SelectedValue));
        }
        else if (ddlEndYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            expense.GetExpenseWeekStats(w_start, w_end, Convert.ToInt32(ddlExpense.SelectedValue));
        }
        grdWeekDetail2.DataSource = expense.DefaultView;
        grdWeekDetail2.DataBind();
        lblWeekTotal2.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;
    }
    private void getSupplyStatistics(ref DataRowView drv, ref GridView grdWeekDetail1, ref GridView grdWeekDetail2, ref Label lblWeekTotal1, ref Label lblWeekTotal2)
    {
        tblSupplier sup = new tblSupplier();
        if (ddlStartYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            sup.GetSupplyWeekStats(w_start, w_end, Convert.ToInt32(ddlSuppliers.SelectedValue));
        }
        else if (ddlStartYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            sup.GetSupplyWeekStats(w_start, w_end, Convert.ToInt32(ddlSuppliers.SelectedValue));
        }
        grdWeekDetail1.DataSource = sup.DefaultView;
        grdWeekDetail1.DataBind();
        lblWeekTotal1.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;


        sup.FlushData();
        if (ddlEndYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            sup.GetSupplyWeekStats(w_start, w_end, Convert.ToInt32(ddlSuppliers.SelectedValue));
        }
        else if (ddlEndYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            sup.GetSupplyWeekStats(w_start, w_end, Convert.ToInt32(ddlSuppliers.SelectedValue));
        }
        grdWeekDetail2.DataSource = sup.DefaultView;
        grdWeekDetail2.DataBind();
        lblWeekTotal2.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;
    }
    private void getOtherIncomeStatistics(ref DataRowView drv, ref GridView grdWeekDetail1, ref GridView grdWeekDetail2, ref Label lblWeekTotal1, ref Label lblWeekTotal2)
    {
        tblIncome inc = new tblIncome();
        if (ddlStartYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            inc.GetOtherIncomeWeekStats(w_start, w_end, Convert.ToInt32(ddlOtherIncome.SelectedValue));
        }
        else if (ddlStartYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            inc.GetOtherIncomeWeekStats(w_start, w_end, Convert.ToInt32(ddlOtherIncome.SelectedValue));
        }
        grdWeekDetail1.DataSource = inc.DefaultView;
        grdWeekDetail1.DataBind();
        lblWeekTotal1.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";

        weektotal = 0.0;
        inc.FlushData();
        if (ddlEndYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            inc.GetOtherIncomeWeekStats(w_start, w_end, Convert.ToInt32(ddlOtherIncome.SelectedValue));
        }
        else if (ddlEndYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            inc.GetOtherIncomeWeekStats(w_start, w_end, Convert.ToInt32(ddlOtherIncome.SelectedValue));
        }
        grdWeekDetail2.DataSource = inc.DefaultView;
        grdWeekDetail2.DataBind();
        lblWeekTotal2.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;
    }
    private void getPositionIncomeStatistics(ref DataRowView drv, ref GridView grdWeekDetail1, ref GridView grdWeekDetail2, ref Label lblWeekTotal1, ref Label lblWeekTotal2)
    {
        tblIncome uws = new tblIncome();
        if (ddlStartYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            if (positions.Count == 1)
                uws.GetPositionIncomeWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID, positions[0].ToString(), 0, DateTime.Now.Year, Convert.ToInt32(positions[0].ToString()));
            else if (positions.Count > 1)
            {
                string ids = string.Empty;

                for (int i = 0; i < positions.Count; i++)
                {
                    ids = ids + positions[i].ToString() + ",";
                }
                ids = ids.Substring(0, ids.Length - 1);
                uws.GetPositionIncomeWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID, ids, 1, DateTime.Now.Year, 0);
            }
        }
        else if (ddlStartYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            if (positions.Count == 1)
                uws.GetPositionIncomeWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID, positions[0].ToString(), 0, Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(positions[0].ToString()));
            else if (positions.Count > 1)
            {
                string ids = string.Empty;

                for (int i = 0; i < positions.Count; i++)
                {
                    ids = ids + positions[i].ToString() + ",";
                }
                ids = ids.Substring(0, ids.Length - 1);
                uws.GetPositionIncomeWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID, ids, 1, Convert.ToInt32(ddlStartYear.SelectedItem.Text), 0);
            }
        }
        grdWeekDetail1.DataSource = uws.DefaultView;
        grdWeekDetail1.DataBind();
        lblWeekTotal1.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";

        weektotal = 0.0;
        uws.FlushData();
        if (ddlEndYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            if (positions.Count == 1)
                uws.GetPositionIncomeWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID, positions[0].ToString(), 0, DateTime.Now.Year, Convert.ToInt32(positions[0].ToString()));
            else if (positions.Count > 1)
            {
                string ids = string.Empty;

                for (int i = 0; i < positions.Count; i++)
                {
                    ids = ids + positions[i].ToString() + ",";
                }
                ids = ids.Substring(0, ids.Length - 1);
                uws.GetPositionIncomeWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID, ids, 1, DateTime.Now.Year, 0);
            }
        }
        else if (ddlEndYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            if (positions.Count == 1)
                uws.GetPositionIncomeWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID, positions[0].ToString(), 0, Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(positions[0].ToString()));
            else if (positions.Count > 1)
            {
                string ids = string.Empty;

                for (int i = 0; i < positions.Count; i++)
                {
                    ids = ids + positions[i].ToString() + ",";
                }
                ids = ids.Substring(0, ids.Length - 1);
                uws.GetPositionIncomeWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID, ids, 1, Convert.ToInt32(ddlEndYear.SelectedItem.Text), 0);
            }
        }
        grdWeekDetail2.DataSource = uws.DefaultView;
        grdWeekDetail2.DataBind();
        lblWeekTotal2.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;
    }
    private void getProductsStatistics(ref DataRowView drv, ref GridView grdWeekDetail1, ref GridView grdWeekDetail2, ref Label lblWeekTotal1, ref Label lblWeekTotal2)
    {
        tblProducts uws = new tblProducts();
        if (ddlStartYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetProductsWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlBaseCat.SelectedValue), Convert.ToInt32(ddlSubCat.SelectedValue));
        }
        else if (ddlStartYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStartYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetProductsWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlBaseCat.SelectedValue), Convert.ToInt32(ddlSubCat.SelectedValue));
        }
        grdWeekDetail1.DataSource = uws.DefaultView;
        grdWeekDetail1.DataBind();
        lblWeekTotal1.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";

        weektotal = 0.0;
        uws.FlushData();
        if (ddlEndYear.SelectedValue == "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(DateTime.Now.Year, Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetProductsWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlBaseCat.SelectedValue), Convert.ToInt32(ddlSubCat.SelectedValue));
        }
        else if (ddlEndYear.SelectedValue != "0")
        {
            DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
            DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlEndYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);
            uws.GetProductsWeekStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), Convert.ToInt32(ddlProduct.SelectedValue), Convert.ToInt32(ddlBaseCat.SelectedValue), Convert.ToInt32(ddlSubCat.SelectedValue));
        }
        grdWeekDetail2.DataSource = uws.DefaultView;
        grdWeekDetail2.DataBind();
        lblWeekTotal2.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";
        weektotal = 0.0;
    }
    private void getStaffStatistics(ref DataRowView drv, ref GridView grdSalaries, ref Label lblWeekTotal1, ref Label lblWeekTotal2)
    {
        tblUsers u = new tblUsers();

        DateTime w_start = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStaffYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(-1);
        DateTime w_end = commonMethods.GetWeekStartDate(Convert.ToInt32(ddlStaffYear.SelectedItem.Text), Convert.ToInt32(drv["weeknumber"])).AddDays(5);

        u.GetStaffForStats(w_start, w_end, Convert.ToInt32(drv["weeknumber"]), DepartmentID, Convert.ToInt32(ddlStaffName.SelectedValue));
        grdSalaries.DataSource = u.DefaultView;
        grdSalaries.DataBind();
        lblWeekTotal1.Text = commonMethods.ChangetToUK(weektotal.ToString("N")) + " €";

        weektotal = 0.0;



    }


    protected void grdWeekDetail1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblDays = e.Row.FindControl("lblDays") as Label;
                Label lblTotal = e.Row.FindControl("lblTotal") as Label;
                HtmlContainerControl divDays = e.Row.FindControl("divDays") as HtmlContainerControl;
                LinkButton lnkDays = e.Row.FindControl("lnkDays") as LinkButton;

                lblDays.Text = drv["DayType"].ToString();
                lnkDays.Visible = false;
                lblDays.Text = drv["DayType"].ToString();
                if (showStatisticsTitle == "salaries" || showStatisticsTitle == "expense" || showStatisticsTitle == "other")
                {
                    string date = lblDays.Text.Substring(lblDays.Text.IndexOf(' ') + 1);
                    string[] start_D = date.Split('/'); // start Date
                    string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
                    string url = "DailyIncome.aspx?dcid=" + start;
                    divDays.Attributes.Add("onclick", "getLocation('" + url + "')");
                    divDays.Style.Add("cursor", "pointer");
                }
                else if (showStatisticsTitle == "supply")
                {
                    lblDays.Visible = false;
                    lnkDays.Visible = true;
                    lnkDays.Text = drv["DayType"].ToString();
                    lnkDays.CommandArgument = drv["DayType"].ToString().Substring(drv["DayType"].ToString().IndexOf(' ') + 1);
                    string date = lblDays.Text.Substring(lblDays.Text.IndexOf(' ') + 1);
                    string[] start_D = date.Split('/'); // start Date
                    string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

                    tblSupplier supp = new tblSupplier();
                    supp.getSupplierOrdersForStats(Convert.ToDateTime(start), Convert.ToInt32(ddlSuppliers.SelectedValue));
                    grdOrderDetailPop.DataSource = supp.DefaultView;
                    grdOrderDetailPop.DataBind();

                    divDays.Attributes.Add("onclick", "javascript:showModel();");
                    divDays.Style.Add("cursor", "pointer");
                }

                double income = 0.0;
                try
                {
                    income = Convert.ToDouble(drv["income"].ToString());
                    weektotal += income;
                    arrLeftValue.Add(income);
                }
                catch (FormatException ex)
                {
                    weektotal += income;
                    arrLeftValue.Add(income);
                }
                lblTotal.Text = commonMethods.ChangetToUK(income.ToString("N")) + " €";

            }
        }
        catch (Exception ex)
        { }
    }
    protected void grdWeekDetail2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblDays = e.Row.FindControl("lblDays") as Label;
                LinkButton lnkDays = e.Row.FindControl("lnkDays") as LinkButton;
                Label lblTotal = e.Row.FindControl("lblTotal") as Label;
                HtmlContainerControl divDays = e.Row.FindControl("divDays") as HtmlContainerControl;
                lnkDays.Visible = false;
                lblDays.Text = drv["DayType"].ToString();
                if (showStatisticsTitle == "salaries" || showStatisticsTitle == "expense" || showStatisticsTitle == "other")
                {
                    string date = lblDays.Text.Substring(lblDays.Text.IndexOf(' ') + 1);
                    string[] start_D = date.Split('/'); // start Date
                    string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
                    string url = "DailyIncome.aspx?dcid=" + start;
                    divDays.Attributes.Add("onclick", "getLocation('" + url + "')");
                    divDays.Style.Add("cursor", "pointer");
                }
                else if (showStatisticsTitle == "supply")
                {

                    lblDays.Visible = false;
                    lnkDays.Visible = true;
                    lnkDays.Text = drv["DayType"].ToString();
                    lnkDays.CommandArgument = drv["DayType"].ToString().Substring(drv["DayType"].ToString().IndexOf(' ') + 1);
                    string date = lblDays.Text.Substring(lblDays.Text.IndexOf(' ') + 1);
                    string[] start_D = date.Split('/'); // start Date
                    string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];

                    tblSupplier supp = new tblSupplier();
                    supp.getSupplierOrdersForStats(Convert.ToDateTime(start), Convert.ToInt32(ddlSuppliers.SelectedValue));
                    grdOrderDetailPop.DataSource = supp.DefaultView;
                    grdOrderDetailPop.DataBind();

                    divDays.Attributes.Add("onclick", "$find('<%=ModalPopupExtender3.ClientID %>').show();return false;");
                    divDays.Style.Add("cursor", "pointer");
                }



                double income = 0.0;
                try
                {
                    income = Convert.ToDouble(drv["income"].ToString());
                    weektotal += income;
                    arrRightValue.Add(income);
                }
                catch (FormatException ex)
                {
                    weektotal += income;
                    arrRightValue.Add(income);
                }
                lblTotal.Text = commonMethods.ChangetToUK(income.ToString("N")) + " €";

            }
        }
        catch (Exception ex)
        { }
    }
    protected void grdWeekDetail2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            LinkButton btn = e.CommandSource as LinkButton;
            LinkButton lnk = btn.FindControl("lnkDays") as LinkButton;
            switch (e.CommandName.ToLower())
            {
                case "order":
                    string[] start_D = lnk.CommandArgument.ToString().Split('/');
                    string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
                    DateTime dd = Convert.ToDateTime(start);
                    tblBaseOrder b = new tblBaseOrder();
                    b.getOrdersForStatistics(dd.Day, dd.Month, dd.Year);

                    grdOrderDetail.DataSource = b.DefaultView;
                    grdOrderDetail.DataBind();
                    ModalPopupExtender1.Show();

                    break;
            }
        }
    }
    protected void grdWeekDetail1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            LinkButton btn = e.CommandSource as LinkButton;
            LinkButton lnk = btn.FindControl("lnkDays") as LinkButton;
            switch (e.CommandName.ToLower())
            {
                case "order":
                    string[] start_D = lnk.CommandArgument.ToString().Split('/');
                    string start = start_D[1] + "/" + start_D[0] + "/" + start_D[2];
                    DateTime dd = Convert.ToDateTime(start);
                    tblBaseOrder b = new tblBaseOrder();
                    b.getOrdersForStatistics(dd.Day, dd.Month, dd.Year);

                    grdOrderDetail.DataSource = b.DefaultView;
                    grdOrderDetail.DataBind();
                    ModalPopupExtender1.Show();

                    break;
            }
        }
    }

    
    protected void grdOrderDetailPop_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;


            Label lnkOldPrice = e.Row.FindControl("lnkOldPrice") as Label;
            Label lblAfterVat = e.Row.FindControl("lblAfterVat") as Label;
            Label lblSubtotalPop = e.Row.FindControl("lblSubtotalPop") as Label;

            if (lnkOldPrice != null)
            {
                lnkOldPrice.Text = commonMethods.ChangetToUK(lnkOldPrice.Text) + " €";
                lblAfterVat.Text = lblAfterVat.Text + " %";
                lblSubtotalPop.Text = commonMethods.ChangetToUK(lblSubtotalPop.Text) + " €";
            }
        }
    }

    protected void grdSalaries_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblIncome = e.Row.FindControl("lblIncome") as Label;
                Label lblSalary = e.Row.FindControl("lblSalary") as Label;
                Label lblTips = e.Row.FindControl("lblTips") as Label;
                Label lblBonus = e.Row.FindControl("lblBonus") as Label;
                Label lblAdvance = e.Row.FindControl("lblAdvance") as Label;
                Label lblPenalty = e.Row.FindControl("lblPenalty") as Label;
                CheckBox chkOnTime = e.Row.FindControl("chkOnTime") as CheckBox;
                Label lblLate = e.Row.FindControl("lblLate") as Label;

                lblIncome.Text = lblIncome.Text + " €";
                lblSalary.Text = lblSalary.Text + " €";
                lblTips.Text = lblTips.Text + " €";
                lblBonus.Text = lblBonus.Text + " €";
                lblAdvance.Text = lblAdvance.Text + " €";
                lblPenalty.Text = lblPenalty.Text + " €";

                weekIncome += Convert.ToDouble(drv["income"].ToString());
                weekSalary += Convert.ToDouble(drv["Salary"].ToString());
                weekTip += Convert.ToDouble(drv["Tip"].ToString());
                weekBonus += Convert.ToDouble(drv["Bonus"].ToString());
                weekAdvance += Convert.ToDouble(drv["Advance"].ToString());
                weekPenalty += Convert.ToDouble(drv["Penalty"].ToString());
                weekLateHours += Convert.ToInt32(drv["latehour"].ToString());



            }
        }
        catch (Exception ex)
        { }
    }



    public override void VerifyRenderingInServerForm(Control divWeeks)
    {
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htmlTW = new HtmlTextWriter(sw);
        divWeeks.Style.Add("width", "774px;");
        divWeeks.RenderControl(htmlTW);
        string html = sb.ToString();
        Session["html"] = html;
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "th", " $(function (){printreport();});", true);
    }
    protected void imgbtnpdf_Click(object sender, ImageClickEventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htmlTW = new HtmlTextWriter(sw);
        divWeeks.Style.Add("width", "774px;");
        divWeeks.RenderControl(htmlTW);
        string html = sb.ToString();
        Session["html"] = html;
        GetHtml.setHtml(html);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "th", " $(function (){pdfReport();});", true);
    }
    protected void imgbtnGraph_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string xLast = string.Empty;
            string xFirst = string.Empty;
            //int prevDay = 0;
            //int days = 0;
            DataTable dtDates = new DataTable();
            dtDates.Columns.Add("date");
            dtDates.Columns.Add("price");

            for (int i = 0; i < grdWeeks.Rows.Count; i++)
            {
                GridView grdOrders = grdWeeks.Rows[i].FindControl("grdWeekDetail1") as GridView;
                for (int j = 0; j < grdOrders.Rows.Count; j++)
                {
                    LinkButton lnkDays = grdOrders.Rows[j].FindControl("lnkDays") as LinkButton;
                    Label lblTotal = grdOrders.Rows[j].FindControl("lblTotal") as Label;
                    Label lblDays = grdOrders.Rows[j].FindControl("lblDays") as Label;
                    lblDays.Text = lblDays.Text.ToString().Substring(lblDays.Text.ToString().IndexOf(' ') + 1);
                    lblTotal.Text = commonMethods.ChangeToUS(lblTotal.Text.Replace(" €", "")).ToString("N");

                    try
                    {
                        if (Convert.ToDouble(lblTotal.Text) != 0)
                        {
                            DataRow dr = dtDates.NewRow();
                            if (lnkDays.CommandArgument != "")
                                dr["date"] = lnkDays.CommandArgument;
                            else
                                dr["date"] = lblDays.Text;
                            dr["price"] = lblTotal.Text;
                            dtDates.Rows.Add(dr);
                        }
                    }
                    catch (Exception ex)
                    { }
                }
            }


            //DateTime dtDate = ssp2.DModifiedDate;
            //prevDay = ssp2.DModifiedDate.Day;
            double price = 0.0;
            xLast = dtDates.Rows[0]["date"].ToString();
            xFirst = dtDates.Rows[dtDates.Rows.Count - 1]["date"].ToString();

            for (int i = 0; i < dtDates.Rows.Count; i++)
            {
                if (price < Convert.ToDouble(dtDates.Rows[i]["price"]))
                    price = Convert.ToDouble(dtDates.Rows[i]["price"]);
            }

            LineChart c = new LineChart(900, 480, Page);
            c.SetXAxisLabel(xFirst, xLast);
            c.strTitle = "Price Range";

            c.ftXorigin = 0;
            c.ftScaleX = dtDates.Rows.Count;
            c.ftXdivs = dtDates.Rows.Count;
            int divs = 0;
            if (price > 0 && price < 31)
                divs = Convert.ToInt32(price) / 3;
            else if (price > 30 && price < 51)
                divs = Convert.ToInt32(price) / 5;
            else if (price > 50 && price < 101)
                divs = Convert.ToInt32(price) / 10;
            else if (price > 100 && price < 151)
                divs = Convert.ToInt32(price) / 15;
            else if (price > 150 && price < 201)
                divs = Convert.ToInt32(price) / 20;
            else if (price > 200)
                divs = Convert.ToInt32(price) / 30;

            c.ftYorigin = 0;
            c.ftScaleY = (float)price;
            c.ftYdivs = divs + 1;

            for (int i = 1; i < dtDates.Rows.Count; i++)
            {
                c.AddValue(i, Convert.ToInt32(Convert.ToDouble(dtDates.Rows[i]["price"])));
            }
            c.Draw();

            // System.Drawing.Image image = System.Drawing.Image.FromStream(Serialization.gStream);
            BinaryFormatter formatter = new BinaryFormatter();
            Serialization.gStream.Position = 0;
            Bitmap bt = (Bitmap)formatter.Deserialize(Serialization.gStream);
            bt.SetResolution(1024, 768);
            //System.Drawing.Image image =  System.Drawing.Image.FromStream(Serialization.gStream).GetPropertyItem; 
            string tempGUI = Guid.NewGuid().ToString();
            string sName = tempGUI.Substring(0, 5);

            bt.Save(Server.MapPath("../Chart/chart" + sName + ".jpg"), ImageFormat.Jpeg);
            imgGraph.Src = "../Chart/chart" + sName + ".jpg";

            UpdatePanel3.Update();
            ModalPopupExtender2.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "kjkljlk", "alert('no data found for graph')", true);
        }
    }



    protected void grdOrderDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdOrderDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            LinkButton btn = e.CommandSource as LinkButton;

            int id = Convert.ToInt32(e.CommandArgument);
            tblBaseOrder b = new tblBaseOrder();
            switch (e.CommandName.ToLower())
            {
                case "order":
                    b.FlushData();
                    b.LoadByPrimaryKey(id);
                    if (b.RowCount > 0)
                    {
                        b.getorderbysessionfor_Stats(id);
                        grdOrderDetailPop.DataSource = b.DefaultView;
                        grdOrderDetailPop.DataBind();
                        ModalPopupExtender1.Hide();
                        ModalPopupExtender3.Show();
                    }
                    break;
            }
        }

    }
    protected void grdOrderDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            Label lblDate = e.Row.FindControl("lblDate") as Label;

            LinkButton lnkDate = e.Row.FindControl("lnkDate") as LinkButton;
            Label lblOrderNumber = e.Row.FindControl("lblOrderNumber") as Label;
            lblOrderNumber.Text = lblOrderNumber.Text.Replace("-", "").Substring(0, 9);
            Label lblSubTotal = e.Row.FindControl("lblSubTotal") as Label;

            if (drv["dCreatedDate"].ToString() != "")
            {
                int day = Convert.ToDateTime(drv["dCreatedDate"].ToString()).Day;
                int month = Convert.ToDateTime(drv["dCreatedDate"].ToString()).Month;
                int year = Convert.ToDateTime(drv["dCreatedDate"].ToString()).Year;
                //lblDate.Text = day.ToString() + "/" + month.ToString() + "/" + year.ToString();
                lnkDate.Text = day.ToString() + "/" + month.ToString() + "/" + year.ToString();


            }
            lblSubTotal.Text = commonMethods.ChangetToUK(lblSubTotal.Text) + " €";

        }

    }

}
