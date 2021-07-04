using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;
using System.Web.UI.HtmlControls;
using System.Data;

public partial class Users_Payments : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    int WeekNumber;
    int Year;
    int Day;
    static DateTime salaryDate;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            if (user.AccessLevel != 1)
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
        if (Request.QueryString.Count == 0)
        {
            salaryDate = DateTime.Now;
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

        if (!IsPostBack)
        {
            LoadValues();
            LoadLegends();
            LoadWeeklSalaryContract();

        }
    }
    private void LoadWeeklSalaryContract()
    {
        tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
        weeklyPayment.GetWeeklySalariedUser(UserID, commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1), commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5));
        if (weeklyPayment.RowCount > 0)
        {
            if (weeklyPayment.PaidByAccountManager)
            {
                chkSalary.Visible = true;
                if (!weeklyPayment.ReceivedByUser)
                    chkSalary.Checked = false;
                else
                {
                    chkSalary.Checked = true;
                    chkSalary.Enabled = false;
                }
                lblSalaryMessage.Visible = true;
                lblSalaryMessage.Text = "I agree with weekly schedule and received my weekly Payment";
            }
        }
    }
    private void LoadLegends()
    {
        tblSpeciality specialty = new tblSpeciality();
        specialty.LoadSpecialtiesWithSeperator(DepartmentID);

        //tblPositions positions = new tblPositions();
        //positions.LoadAll();
        dlLegends.DataSource = specialty.DefaultView;
        dlLegends.DataBind();
    }
    private void LoadValues()
    {
        LoadUserWorkshift();
        LoadSalary();
        //LoadOtherOffDay();

    }
    private void LoadUserWorkshift()
    {
        try
        {
            lblWeekDates.Text = "Week " + WeekNumber.ToString() + " / " + Year.ToString() + " ( Sunday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1).ToString("dd/MM/yyyy") + " till Saturday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5).ToString("dd/MM/yyyy") + " )";

            tblUserWorkshifts uws = new tblUserWorkshifts();
            uws.LoadUserWorkShift(WeekNumber, UserID, Year);
            if (uws.RowCount > 0)
            {
                for (int count = 0; count < uws.RowCount; count++)
                {

                    if (uws.IDayNumber == 1)
                    {
                        if (uws.GetColumn("Timing").ToString() != "")
                            tdDay1.Style.Add("background-color", "White");
                        if (!Convert.ToBoolean(uws.GetColumn("bLate")))
                            lblUserSunday.Text = uws.GetColumn("Timing").ToString();
                        else
                            lblUserSunday.Text = "Day OFF";
                    }

                    if (uws.IDayNumber == 2)
                    {
                        if (uws.GetColumn("Timing").ToString() != "")
                            tdDay2.Style.Add("background-color", "White");
                        lblUserMonday.Text = uws.GetColumn("Timing").ToString();
                    }

                    if (uws.IDayNumber == 3)
                    {
                        if (uws.GetColumn("Timing").ToString() != "")
                            tdDay2.Style.Add("background-color", "White");
                        if (Convert.ToBoolean(uws.GetColumn("bLate")))
                            lblUserTuesday.Text = uws.GetColumn("Timing").ToString();
                        else
                            lblUserTuesday.Text = "Day OFF";
                    }

                    if (uws.IDayNumber == 4)
                    {
                        if (uws.GetColumn("Timing").ToString() != "")
                            tdDay4.Style.Add("background-color", "White");
                        if (Convert.ToBoolean(uws.GetColumn("bLate")))
                            lblUserWednesday.Text = uws.GetColumn("Timing").ToString();
                        else
                            lblUserWednesday.Text = "Day OFF";
                    }

                    if (uws.IDayNumber == 5)
                    {
                        if (uws.GetColumn("Timing").ToString() != "")
                            tdDay5.Style.Add("background-color", "White");
                        if (Convert.ToBoolean(uws.GetColumn("bLate")))
                            lblUserThursday.Text = uws.GetColumn("Timing").ToString();
                        else
                            lblUserThursday.Text = "Day OFF";
                    }

                    if (uws.IDayNumber == 6)
                    {
                        if (uws.GetColumn("Timing").ToString() != "")
                            tdDay6.Style.Add("background-color", "White");
                        if (Convert.ToBoolean(uws.GetColumn("bLate")))
                            lblUserFriday.Text = uws.GetColumn("Timing").ToString();
                        else
                            lblUserFriday.Text = "Day OFF";
                    }

                    if (uws.IDayNumber == 7)
                    {
                        if (uws.GetColumn("Timing").ToString() != "")
                            tdDay7.Style.Add("background-color", "White");
                        if (Convert.ToBoolean(uws.GetColumn("bLate")))
                            lblUserSaturday.Text = uws.GetColumn("Timing").ToString();
                        else
                            lblUserSaturday.Text = "Day OFF";
                    }

                    uws.MoveNext();
                }

            }
        }
        catch (Exception ex)
        { }
    }
    private void LoadSalary()
    {
        tblUserWorkshifts uws = new tblUserWorkshifts();

         
        
        //int selecteWeek = commonMethods.GetWeeknumber(salaryDate);
        //if (salaryDate.DayOfWeek == DayOfWeek.Sunday)
        //    selecteWeek += 1;


        DateTime d_start = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1);
        
        
         
        DataTable dt = uws.LoadCurrentUserSalary(WeekNumber,d_start, DepartmentID, Year, UserID);

        
        
        DataRow[] dr;
        double WeeklySalary = 0.00;
        double PerDaySalary = 0.00;

        double netSalary = 0;
        double netTips = 0;
        double netBonus = 0;
        double netAdvances = 0;
        double netPanelty = 0;

        for (int Counter = 1; Counter <= 6; Counter++)
        {
            HtmlTableRow tr = new HtmlTableRow();
            tr.Style.Add("height", "45px;");
            tr.Style.Add("font-size", "28px;");
            tr.Style.Add("font-weight", "normal");
            tr.Style.Add("font-family", "Calibri");

            if (Counter == 1)
            {
                double totalSalary = 0;
                HtmlTableCell tdSalary = new HtmlTableCell();
                tdSalary.InnerHtml = "Salary";
                tdSalary.Align = "Center";
                tdSalary.Style.Add("font-size", "23px;");
                tdSalary.Style.Add("background-color", "#d6efff");
                tr.Controls.Add(tdSalary);
                string strSpan = string.Empty;
                for (int salary = 1; salary <= 8; salary++)
                {
                    HtmlTableCell td = new HtmlTableCell();
                    td.Align = "Center";
                    td.Style.Add("font-size", "30px;");
                    td.Style.Add("font-weight", "bolder");
                    td.Style.Add("background-color", "#d6efff");
                    HtmlGenericControl span = new HtmlGenericControl("span");

                    if (salary == 8)
                    {
                        strSpan = "<span>" + totalSalary.ToString("N") + "€</span>";
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
                                        WeeklySalary = contract.GetUserSeasonSalary(Convert.ToDateTime(datepicker.Text), UserID,DepartmentID);
                                    else if (Request.QueryString.Count > 0)
                                    {
                                        WeeklySalary = contract.GetUserSeasonSalary(Convert.ToDateTime(commonMethods.GetWeekStartDate(Year, WeekNumber).ToShortDateString()), UserID,DepartmentID);                                        
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
                                    
                                    //PerDaySalary = Convert.ToDouble(dr[0]["MinimumPerday"]);
                                }
                                if (WeeklySalary != 0.0)
                                {
                                    //PerDaySalary = Convert.ToDouble(Convert.ToDouble(WeeklySalary / Convert.ToDouble(7)).ToString("N"));
                                    PerDaySalary = Convert.ToDouble(Convert.ToDouble(WeeklySalary).ToString("N"));
                                }
                                //else
                                //{
                                //    PerDaySalary = Convert.ToDouble(WeeklySalary.ToString("N"));
                                //}
                            }
                            else
                            {
                                PerDaySalary = Convert.ToDouble(WeeklySalary.ToString("N"));
                            }
                            //if (!Convert.ToBoolean(dr[0]["bOnTime"]))
                            //    PerDaySalary = 0.0;
                            totalSalary += PerDaySalary;
                            strSpan = "<span>" + PerDaySalary.ToString("N") + "€</span>";
                        }
                        else
                        {
                            strSpan = "<span> 0€ </span>";
                        }
                    }
                    span.InnerHtml = strSpan;
                    td.Controls.Add(span);
                    tr.Controls.Add(td);
                }

            }
            else if (Counter == 2)
            {
                HtmlTableCell tdTips = new HtmlTableCell();
                tdTips.InnerHtml = "Tips";
                tdTips.Align = "Center";
                tdTips.Style.Add("font-size", "23px;");
                tdTips.Style.Add("background-color", "White");
                tr.Controls.Add(tdTips);
               
                double totalTips = 0;
                string strSpan = string.Empty;
                for (int tips = 1; tips <= 8; tips++)
                {
                    HtmlTableCell td = new HtmlTableCell();
                    td.Align = "Center";
                    td.Style.Add("background-color", "White");
                    HtmlGenericControl span = new HtmlGenericControl("span");
                    if (tips == 8)
                    {
                        strSpan = "<span>" + totalTips.ToString("N") + "€</span>";
                        netTips = totalTips;
                    }
                    else
                    {
                        dr = dt.Select("iDayNumber = " + tips + "AND bOnTime = 1 AND bLate = 0");
                        if (dr.Length > 0)
                        {

                            if (dr[0]["userTip"].ToString() != "")
                            {
                                strSpan = "<span>" +dr[0]["userTip"].ToString() + "€</span>";
                                totalTips += Convert.ToDouble(dr[0]["userTip"]);
                            }
                            else
                            {
                                strSpan = "<span> 0€ </span>";
                            }
                        }
                        else
                        {
                            strSpan = "<span> 0€ </span>";
                        }
                    }
                    span.InnerHtml = strSpan;
                    td.Controls.Add(span);
                    tr.Controls.Add(td);
                }

            }
            else if (Counter == 3)
            {
                HtmlTableCell tdBonus = new HtmlTableCell();
                tdBonus.InnerHtml = "Bonus";
                tdBonus.Align = "Center";
                tdBonus.Style.Add("font-size", "23px;");
                tdBonus.Style.Add("background-color", "#d6efff");
                tr.Controls.Add(tdBonus);
                double totalBonus = 0;
                string strSpan = string.Empty;
                for (int bonus = 1; bonus <= 8; bonus++)
                {
                    HtmlTableCell td = new HtmlTableCell();
                    td.Align = "Center";
                    td.Style.Add("background-color", "#d6efff");
                    HtmlGenericControl span = new HtmlGenericControl("span");

                    if (bonus == 8)
                    {
                        strSpan = "<span>" + totalBonus.ToString("N") + "€</span>";
                        netBonus = totalBonus;

                    }
                    else
                    {
                        dr = dt.Select("iDayNumber = " + bonus + "AND bOnTime = 1 AND bLate = 0");
                        if (dr.Length > 0)
                        {


                            if (dr[0]["Bonus"].ToString() != "")
                            {
                                strSpan = "<span>" +dr[0]["Bonus"].ToString() + "€</span>";
                                totalBonus += Convert.ToDouble(dr[0]["Bonus"]);

                                //if (dr[0]["sNotes"].ToString() != "")
                                //{
                                //    tblUsers u = new tblUsers();
                                //    u.GetAccountManager();
                                //    string managerName = string.Empty;
                                //    if (u.RowCount > 0)
                                //    {
                                //        managerName = u.GetColumn("FullName").ToString();
                                //    }
                                //    HtmlGenericControl bonusSpan = new HtmlGenericControl();
                                //    bonusSpan.InnerHtml = "Bonus - Manager " + managerName +": "+dr[0]["sNotes"].ToString() + "<br />";
                                //    divNotes.Controls.Add(bonusSpan);
                                //}
                            }
                            else
                            {
                                strSpan = "<span> 0€ </span>";
                            }
                        }
                        else
                        {
                            strSpan = "<span> 0€ </span>";
                        }
                    }
                    span.InnerHtml = strSpan;
                    td.Controls.Add(span);
                    tr.Controls.Add(td);
                }

            }
            else if (Counter == 4)
            {
                HtmlTableCell tdAdvance = new HtmlTableCell();
                tdAdvance.InnerHtml = "Advance";
                tdAdvance.Align = "Center";
                tdAdvance.Style.Add("font-size", "23px;");
                tdAdvance.Style.Add("background-color", "White");
                tr.Controls.Add(tdAdvance);
                double totalAdvance = 0;
                string strSpan = string.Empty;
                for (int advance = 1; advance <= 8; advance++)
                {
                    HtmlTableCell td = new HtmlTableCell();
                    td.Align = "Center";
                    td.Style.Add("background-color", "White");
                    HtmlGenericControl span = new HtmlGenericControl("span");
                    if (advance == 8)
                    {
                        strSpan = "<span>-" + totalAdvance.ToString("N") + "€</span>";
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
                                strSpan = "<span>-" + dr[0]["uAdvance"].ToString() + "€</span>";

                            }
                            else
                            {
                                strSpan = "<span> 0€ </span>";
                            }
                        }
                        else
                        {
                            strSpan = "<span> 0€ </span>";
                        }
                    }
                    span.InnerHtml = strSpan;
                    td.Controls.Add(span);
                    tr.Controls.Add(td);
                }

            }
            else if (Counter == 5)
            {
                HtmlTableCell tdPenalty = new HtmlTableCell();
                tdPenalty.InnerHtml = "Penalty";
                tdPenalty.Align = "Center";
                tdPenalty.Style.Add("font-size", "23px;");
                tdPenalty.Style.Add("background-color", "#d6efff");
                tr.Controls.Add(tdPenalty);
                double totalPenalty = 0;
                string strSpan = string.Empty;
                for (int penalty = 1; penalty <= 8; penalty++)
                {
                    HtmlTableCell td = new HtmlTableCell();
                    td.Align = "Center";
                    td.Style.Add("background-color", "#d6efff");
                    HtmlGenericControl span = new HtmlGenericControl("span");
                    if (penalty == 8)
                    {
                        strSpan = "<span>" + totalPenalty.ToString("N") + "€</span>";
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
                                strSpan = "<span>" +dr[0]["Penalty"].ToString() + "€</span>";


                            }
                            else
                            {
                                strSpan = "<span> 0€ </span>";
                            }

                        }
                        else
                        {
                            strSpan = "<span> 0€ </span>";
                        }
                    }
                    span.InnerHtml = strSpan;
                    td.Controls.Add(span);
                    tr.Controls.Add(td);
                }

            }
            else if (Counter == 6)
            {
                HtmlTableCell td = new HtmlTableCell();
                td.ColSpan = 7;
                HtmlTableCell tdPayment = new HtmlTableCell();
                tdPayment.Align = "Center";
                tdPayment.InnerHtml = "Payment:";
                tdPayment.Style.Add("font-size", "17px;");
                tdPayment.Style.Add("font-weight", "normal");
                tdPayment.Style.Add("background-color", "White");

                HtmlTableCell tdNetPayment = new HtmlTableCell();
                tdNetPayment.Align = "Center";
                tdNetPayment.Style.Add("font-size", "28px;");
                tdNetPayment.Style.Add("font-weight", "bolder");
                tdNetPayment.Style.Add("background-color", "White");
                tdNetPayment.InnerHtml = (netSalary + netTips + netBonus - (netAdvances + netPanelty)).ToString("N") + "€";
                tr.Controls.Add(td);
                tr.Controls.Add(tdPayment);
                tr.Controls.Add(tdNetPayment);



            }
            tPayment.Controls.Add(tr);
        }
        for (int note = 1; note <= 7; note++)
        {
            dr = dt.Select("iDayNumber = " + note);
            if (dr.Length > 0)
            {
                if (dr[0]["sNotes"].ToString() != "")
                {
                    tblUsers u = new tblUsers();
                    u.GetAccountManager();
                    string managerName = string.Empty;
                    if (u.RowCount > 0)
                    {
                        managerName = u.GetColumn("FullName").ToString();
                    }
                    HtmlGenericControl bonusSpan = new HtmlGenericControl();
                    if (note == 1)
                        bonusSpan.InnerHtml = "Account Manager: " + managerName + ": <span style='font-weight:normal;'> " + dr[0]["sNotes"].ToString() + " <span style='color:gray;'>(Sunday)</span></span> <br />";
                    else if (note == 2)
                        bonusSpan.InnerHtml = "Account Manager: " + managerName + ": <span style='font-weight:normal;'>" + dr[0]["sNotes"].ToString() + " <span style='color:gray;'>(Monday)</span></span> <br />";
                    else if (note == 3)
                        bonusSpan.InnerHtml = "Account Manager: " + managerName + ": <span style='font-weight:normal;'>" + dr[0]["sNotes"].ToString() + " <span style='color:gray;'>(Tuesday)</span></span> <br />";
                    else if (note == 4)
                        bonusSpan.InnerHtml = "Account Manager: " + managerName + ": <span style='font-weight:normal;'>" + dr[0]["sNotes"].ToString() + " <span style='color:gray;'>(Wednesday)</span></span> <br />";
                    else if (note == 5)
                        bonusSpan.InnerHtml = "Account Manager: " + managerName + ": <span style='font-weight:normal;'>" + dr[0]["sNotes"].ToString() + " <span style='color:gray;'>(Thursday)</span></span> <br />";
                    else if (note == 6)
                        bonusSpan.InnerHtml = "Account Manager: " + managerName + ": <span style='font-weight:normal;'>" + dr[0]["sNotes"].ToString() + " <span style='color:gray;'>(Friday)</span></span> <br />";
                    else if (note == 7)
                        bonusSpan.InnerHtml = "Account Manager: " + managerName + ": <span style='font-weight:normal;'>" + dr[0]["sNotes"].ToString() + " <span style='color:gray;'>(Saturday)</span></span> <br />";
                    divNotes.Controls.Add(bonusSpan);
                }
            }
        }
    }
    protected void lnkPrevious_Click(object sender, EventArgs e)
    {
        if ((Request.QueryString["week"] != null) && (Request.QueryString["week"] != ""))
        {

            WeekNumber = Convert.ToInt32(Request.QueryString["week"]);
            WeekNumber = WeekNumber - 1;

        }
        else
        {
            WeekNumber = commonMethods.GetWeeknumber(DateTime.Now) - 1;
        }
        Response.Redirect("../Users/Payments.aspx?week=" + WeekNumber + "&year=" + Year);
    }
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        if ((Request.QueryString["week"] != null) && (Request.QueryString["week"] != ""))
        {

            WeekNumber = Convert.ToInt32(Request.QueryString["week"]);
            WeekNumber = WeekNumber + 1;

        }
        else
        {
            WeekNumber = commonMethods.GetWeeknumber(DateTime.Now) + 1;
        }
        Response.Redirect("../Users/Payments.aspx?week=" + WeekNumber + "&year=" + Year);
    }
    protected void btnGO_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnGO_Click(object sender, EventArgs e)
    {
        WeekNumber = commonMethods.GetWeekNumber_New(Convert.ToDateTime(datepicker.Text));
        salaryDate = Convert.ToDateTime(datepicker.Text);
        DateTime mydate = Convert.ToDateTime(datepicker.Text);
        //if (mydate.DayOfWeek == DayOfWeek.Sunday)
        //    WeekNumber += 1;
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;
        Day = days;
        txtCheck.Text = "2";
        txtMyDay.Text = days.ToString();
        Response.Redirect("../Users/Payments.aspx?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year + "&day=" + days);
        hidParam.Value = "?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year;
    }
    protected void chkSalary_CheckedChanged(object sender, EventArgs e)
    {
        if (chkSalary.Checked)
        {
            tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
            weeklyPayment.GetWeeklySalariedUser(UserID, commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1), commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5));
            if (weeklyPayment.RowCount > 0)
            {
                if (weeklyPayment.PaidByAccountManager)
                {
                    chkSalary.Enabled = false;
                    weeklyPayment.ReceivedByUser = chkSalary.Checked;
                    weeklyPayment.DModifiedDate = DateTime.Now;
                    weeklyPayment.Save();
                    lblSalaryMessage.Visible = true;
                    lblSalaryMessage.Text = "I agree with weekly schedule and received my weekly Payment";
                }
            }
        }
        LoadSalary();
    }
}
