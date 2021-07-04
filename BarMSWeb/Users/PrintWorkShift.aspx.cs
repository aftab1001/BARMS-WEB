using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;

public partial class Users_PrintWorkShift : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    int WeekNumber;
    int Year;
    string WeekSTime;
    string WeekETime;

    string WeekendSTime;
    string WeekendETime;
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
            tblUsers objUsers = new tblUsers();
            objUsers.LoadByPrimaryKey(user.UserID);
            if (objUsers.RowCount > 0)
            {
                lblName.Text = objUsers.SFirstName + " " + objUsers.SLastName;
            }
            UserID = user.UserID;
            DepartmentID = user.DepartmentID;
            loadDefautlTime();
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

        if (!IsPostBack)
        {
            LoadUserWorkshift();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "printWorkshift", "window.print();", true);

        }
    }
    private void loadDefautlTime()
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
    private void LoadUserWorkshift()
    {
        lblWeekDates.Text = "Week " + WeekNumber.ToString() + " / " + Year.ToString() + " ( Sunday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1).ToString("dd/MM/yyyy") + " till Saturday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5).ToString("dd/MM/yyyy") + " )";
        tblUserWorkshifts uws = new tblUserWorkshifts();
        uws.LoadUserWorkShift(WeekNumber, UserID, Year);
        if (uws.RowCount > 0)
        {
            string style = string.Empty;
            for (int count = 0; count < uws.RowCount; count++)
            {

                if (uws.IDayNumber == 1)
                {
                    style = "<span style='color:Black'>";
                    if (uws.GetColumn("sStartTime").ToString() != WeekendSTime || uws.GetColumn("sEndTime").ToString() != WeekendETime)
                    {
                        style = "<span style='color:Blue'>";
                    }
                    lblUserSunday.Text = uws.GetColumn("Abbrv").ToString() + " [" + style + uws.GetColumn("sStartTime").ToString() + " " + uws.GetColumn("sEndTime").ToString() + "</span>]";
                    //lblUserSunday.Text = uws.GetColumn("Timing").ToString();
                }

                if (uws.IDayNumber == 2)
                {
                    style = "<span style='color:Black'>";
                    if (uws.GetColumn("sStartTime").ToString() != WeekSTime || uws.GetColumn("sEndTime").ToString() != WeekETime)
                    {
                        style = "<span style='color:Blue'>";
                    }
                    lblUserMonday.Text = uws.GetColumn("Abbrv").ToString() + " [" + style + uws.GetColumn("sStartTime").ToString() + " " + uws.GetColumn("sEndTime").ToString() + "</span>]";
                    //lblUserMonday.Text = uws.GetColumn("Timing").ToString();
                }

                if (uws.IDayNumber == 3)
                {
                    style = "<span style='color:Black'>";
                    if (uws.GetColumn("sStartTime").ToString() != WeekSTime || uws.GetColumn("sEndTime").ToString() != WeekETime)
                    {
                        style = "<span style='color:Blue'>";
                    }
                    lblUserTuesday.Text = uws.GetColumn("Abbrv").ToString() + " [" + style + uws.GetColumn("sStartTime").ToString() + " " + uws.GetColumn("sEndTime").ToString() + "</span>]";
                    //lblUserTuesday.Text = uws.GetColumn("Timing").ToString();
                }

                if (uws.IDayNumber == 4)
                {
                    style = "<span style='color:Black'>";
                    if (uws.GetColumn("sStartTime").ToString() != WeekSTime || uws.GetColumn("sEndTime").ToString() != WeekETime)
                    {
                        style = "<span style='color:Blue'>";
                    }
                    lblUserWednesday.Text = uws.GetColumn("Abbrv").ToString() + " [" + style + uws.GetColumn("sStartTime").ToString() + " " + uws.GetColumn("sEndTime").ToString() + "</span>]";
                    //lblUserWednesday.Text = uws.GetColumn("Timing").ToString();
                }

                if (uws.IDayNumber == 5)
                {
                    style = "<span style='color:Black'>";
                    if (uws.GetColumn("sStartTime").ToString() != WeekSTime || uws.GetColumn("sEndTime").ToString() != WeekETime)
                    {
                        style = "<span style='color:Blue'>";
                    }
                    lblUserThursday.Text = uws.GetColumn("Abbrv").ToString() + " [" + style + uws.GetColumn("sStartTime").ToString() + " " + uws.GetColumn("sEndTime").ToString() + "</span>]";
                    //lblUserThursday.Text = uws.GetColumn("Timing").ToString();
                }

                if (uws.IDayNumber == 6)
                {
                    style = "<span style='color:Black'>";
                    if (uws.GetColumn("sStartTime").ToString() != WeekSTime || uws.GetColumn("sEndTime").ToString() != WeekETime)
                    {
                        style = "<span style='color:Blue'>";
                    }
                    lblUserFriday.Text = uws.GetColumn("Abbrv").ToString() + " [" + style + uws.GetColumn("sStartTime").ToString() + " " + uws.GetColumn("sEndTime").ToString() + "</span>]";
                    //lblUserFriday.Text = uws.GetColumn("Timing").ToString();
                }

                if (uws.IDayNumber == 7)
                {
                    style = "<span style='color:Black'>";
                    if (uws.GetColumn("sStartTime").ToString() != WeekendSTime || uws.GetColumn("sEndTime").ToString() != WeekendETime)
                    {
                        style = "<span style='color:Blue'>";
                    }
                    lblUserSaturday.Text = uws.GetColumn("Abbrv").ToString() + " [" + style + uws.GetColumn("sStartTime").ToString() + " " + uws.GetColumn("sEndTime").ToString() + "</span>]";
                    //lblUserSaturday.Text = uws.GetColumn("Timing").ToString();
                }

                uws.MoveNext();
            }

        }
    }
}
