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
using System.Drawing;
using LC.Model.BMS.BLL;

public partial class Users_UserWorkShift : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    int WeekNumber;
    int Year;
    int Day;
    static int ToUserID = 0;
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
            LoadValues();
            LoadLegends();
           
        }
    }
    private void LoadValues()
    {
        LoadUserWorkshift();
        LoadOtherUserWorkshift();
        LoadOtherOffDay();

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
    private void LoadLegends()
    {
        tblSpeciality specialty = new tblSpeciality();
        specialty.GetAllActiveSpecialties(DepartmentID);

        //tblPositions positions = new tblPositions();
        //positions.LoadAll();
        dlLegends.DataSource = specialty.DefaultView;
        dlLegends.DataBind();
    }
    private void LoadUserWorkshift()
    {
        

        lblWeekDates.Text = "Week " + WeekNumber.ToString() + " / " + Year.ToString() + " ( Sunday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1).ToString("dd/MM/yyyy") + " till Saturday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5).ToString("dd/MM/yyyy") + " )";
        hidParam.Value = "?week=" + WeekNumber + "&year=" + Year;
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
                    if (uws.GetColumn("sStartTime").ToString() != WeekSTime || uws.GetColumn("sEndTime").ToString() != WeekETime)
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
                    if (uws.GetColumn("sStartTime").ToString() != WeekendETime || uws.GetColumn("sEndTime").ToString() != WeekendETime)
                    {
                        style = "<span style='color:Blue'>";
                    }
                    lblUserFriday.Text = uws.GetColumn("Abbrv").ToString() + " [" + style + uws.GetColumn("sStartTime").ToString() + " " + uws.GetColumn("sEndTime").ToString() + "</span>]";
                    //lblUserFriday.Text = uws.GetColumn("Timing").ToString();
                }
                
                if (uws.IDayNumber == 7)
                {
                    style = "<span style='color:Black'>";
                    if (uws.GetColumn("sStartTime").ToString() != WeekendETime || uws.GetColumn("sEndTime").ToString() != WeekendETime)
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
    //private void LoadOtherUserWorkshift()
    //{
    //    tblUserWorkshifts uws = new tblUserWorkshifts();
    //    uws.LoadOtherUsersWorkShift(WeekNumber, UserID,DepartmentID ,Year);
    //    if (uws.RowCount > 0)
    //    {
    //        int limit = uws.RowCount / 7;
    //        for (int Counter = 1; Counter <= limit; Counter ++)
    //        {
    //            HtmlTableRow tr = new HtmlTableRow();
    //            string css = string.Empty;
    //            if (Counter % 2 == 0)
    //            {
    //                css = "rowstyle";
    //            }
    //            else
    //            {
    //                css = "alternate_row";
    //            }
    //            tr.Attributes.Add("class", css);
    //            HtmlTableCell td1 = new HtmlTableCell();
    //            td1.Attributes.Add("class", "align_center");
    //            if (uws.BIsOfDay)
    //            {
    //                td1.InnerHtml = "DAY OFF";
    //            }
    //            else
    //            {
    //                td1.InnerHtml = uws.GetColumn("FullName").ToString();
    //            }
    //            tr.Cells.Add(td1);
    //            uws.MoveNext();

    //            HtmlTableCell td2 = new HtmlTableCell();
    //            td1.Attributes.Add("class", "align_center");
    //            if (uws.BIsOfDay)
    //            {
    //                td2.InnerHtml = "DAY OFF";
    //            }
    //            else
    //            {
    //                td2.InnerHtml = uws.GetColumn("FullName").ToString();
    //            }
    //            tr.Cells.Add(td2);
    //            uws.MoveNext();

    //            HtmlTableCell td3 = new HtmlTableCell();
    //            td3.Attributes.Add("class", "align_center");
    //            if (uws.BIsOfDay)
    //            {
    //                td3.InnerHtml = "DAY OFF";
    //            }
    //            else
    //            {
    //                td3.InnerHtml = uws.GetColumn("FullName").ToString();
    //            }
    //            tr.Cells.Add(td3);
    //            uws.MoveNext();

    //            HtmlTableCell td4 = new HtmlTableCell();
    //            td4.Attributes.Add("class", "align_center");
    //            if (uws.BIsOfDay)
    //            {
    //                td4.InnerHtml = "DAY OFF";
    //            }
    //            else
    //            {
    //                td4.InnerHtml = uws.GetColumn("FullName").ToString();
    //            }
    //            tr.Cells.Add(td4);
    //            uws.MoveNext();

    //            HtmlTableCell td5 = new HtmlTableCell();
    //            td5.Attributes.Add("class", "align_center");
    //            if (uws.BIsOfDay)
    //            {
    //                td5.InnerHtml = "DAY OFF";
    //            }
    //            else
    //            {
    //                td5.InnerHtml = uws.GetColumn("FullName").ToString();
    //            }
    //            tr.Cells.Add(td5);
    //            uws.MoveNext();

    //            HtmlTableCell td6 = new HtmlTableCell();
    //            td6.Attributes.Add("class", "align_center");
    //            if (uws.BIsOfDay)
    //            {
    //                td6.InnerHtml = "DAY OFF";
    //            }
    //            else
    //            {
    //                td6.InnerHtml = uws.GetColumn("FullName").ToString();
    //            }
    //            tr.Cells.Add(td6);
    //            uws.MoveNext();

    //            HtmlTableCell td7 = new HtmlTableCell();
    //            td7.Attributes.Add("class", "align_center");
    //            if (uws.BIsOfDay)
    //            {
    //                td7.InnerHtml = "DAY OFF";
    //            }
    //            else
    //            {
    //                td7.InnerHtml = uws.GetColumn("FullName").ToString();
    //            }
    //            tr.Cells.Add(td7);
    //            uws.MoveNext();

    //            tblOtherUsersWorshift.Rows.Add(tr);

    //        }
    //    }
    //}

    private void LoadOtherUserWorkshift()
    {
        tblUserWorkshifts uws = new tblUserWorkshifts();

        uws.FlushData();
        uws.LoadOtherUsersWorkShift(WeekNumber, UserID, DepartmentID, Year , 1);
        grdSunday1.DataSource = uws.DefaultView;
        grdSunday1.DataBind();

        uws.FlushData();
        uws.LoadOtherUsersWorkShift(WeekNumber, UserID, DepartmentID, Year, 2);
        grdMonday1.DataSource = uws.DefaultView;
        grdMonday1.DataBind();


        uws.FlushData();
        uws.LoadOtherUsersWorkShift(WeekNumber, UserID, DepartmentID, Year, 3);
        grdTuesday1.DataSource = uws.DefaultView;
        grdTuesday1.DataBind();


        uws.FlushData();
        uws.LoadOtherUsersWorkShift(WeekNumber, UserID, DepartmentID, Year, 4);
        grdWednesday1.DataSource = uws.DefaultView;
        grdWednesday1.DataBind();

        uws.FlushData();
        uws.LoadOtherUsersWorkShift(WeekNumber, UserID, DepartmentID, Year, 5);
        grdThursday1.DataSource = uws.DefaultView;
        grdThursday1.DataBind();


        uws.FlushData();
        uws.LoadOtherUsersWorkShift(WeekNumber, UserID, DepartmentID, Year, 6);
        grdFriday1.DataSource = uws.DefaultView;
        grdFriday1.DataBind();

        uws.FlushData();
        uws.LoadOtherUsersWorkShift(WeekNumber, UserID, DepartmentID, Year, 7);
        grdSaturday1.DataSource = uws.DefaultView;
        grdSaturday1.DataBind();


    }
    private void LoadOtherOffDay()
    {
        //LoadOtherUsersOFFDay
        tblUserWorkshifts uws = new tblUserWorkshifts();

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, WeekNumber, Year, 1);
        grdSunday.DataSource = uws.DefaultView;
        grdSunday.DataBind();
        
        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, WeekNumber, Year, 2);
        grdMonday.DataSource = uws.DefaultView;
        grdMonday.DataBind();

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, WeekNumber, Year, 3);
        grdTuesday.DataSource = uws.DefaultView;
        grdTuesday.DataBind();

      

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, WeekNumber, Year, 4);
        grdWednesday.DataSource = uws.DefaultView;
        grdWednesday.DataBind();

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, WeekNumber, Year, 5);
        grdThursday.DataSource = uws.DefaultView;
        grdThursday.DataBind();

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, WeekNumber, Year, 6);
        grdFriday.DataSource = uws.DefaultView;
        grdFriday.DataBind();

        uws.FlushData();
        uws.LoadOffdayUsers(DepartmentID, WeekNumber, Year, 7);
        grdSaturday.DataSource = uws.DefaultView;
        grdSaturday.DataBind();

        

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
        Response.Redirect("../Users/UserWorkShift.aspx?week=" + WeekNumber + "&year=" + Year);
        
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
        Response.Redirect("../Users/UserWorkShift.aspx?week=" + WeekNumber + "&year=" + Year);
    }
    protected void btnGO_Click(object sender, ImageClickEventArgs e)
    {
        WeekNumber = commonMethods.GetWeeknumber(Convert.ToDateTime(datepicker.Text));
        Response.Redirect("../Users/UserWorkShift.aspx?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year);
        hidParam.Value = "?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year;
    }
    protected void grdSunday1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string style = "<span style='color:Black'>";
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                
                Label lblDate = e.Row.FindControl("lblDate") as Label;


                if (drv["sStartTime"].ToString() != WeekSTime || drv["sEndTime"].ToString() != WeekETime)
                {
                    style = "<span style='color:Blue'>";
                }
                lblDate.Text = drv["Abbrv"].ToString() + " " + style + drv["sStartTime"].ToString() + " " + drv["sEndTime"].ToString() + "</span>";
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;

                lblName.Text = drv["Name"].ToString();
                string name = drv["Name"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18)+"...";
                    name_With_Message = name + name_With_Message;

                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('"+name+"')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                    
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {
 
            }
        }
    }
    protected void grdMonday1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string style = "<span style='color:Black'>";
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                if (drv["sStartTime"].ToString() != WeekSTime || drv["sEndTime"].ToString() != WeekETime)
                {
                    style = "<span style='color:Blue'>";
                }

                lblDate.Text = drv["Abbrv"].ToString() + " " + style + drv["sStartTime"].ToString() + " " + drv["sEndTime"].ToString() + "</span>";
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["Name"].ToString();
                string name = drv["Name"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdTuesday1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string style = "<span style='color:Black'>";
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                if (drv["sStartTime"].ToString() != WeekSTime || drv["sEndTime"].ToString() != WeekETime)
                {
                    style = "<span style='color:Blue'>";
                }
                lblDate.Text = drv["Abbrv"].ToString() + " " + style + drv["sStartTime"].ToString() + " " + drv["sEndTime"].ToString() + "</span>";
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["Name"].ToString();
                string name = drv["Name"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdWednesday1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string style = "<span style='color:Black'>";
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                if (drv["sStartTime"].ToString() != WeekSTime || drv["sEndTime"].ToString() != WeekETime)
                {
                    style = "<span style='color:Blue'>";
                }
                lblDate.Text = drv["Abbrv"].ToString() + " " + style + drv["sStartTime"].ToString() + " " + drv["sEndTime"].ToString() + "</span>";
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["Name"].ToString();
                string name = drv["Name"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdThursday1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string style = "<span style='color:Black'>";
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                if (drv["sStartTime"].ToString() != WeekSTime || drv["sEndTime"].ToString() != WeekETime)
                {
                    style = "<span style='color:Blue'>";
                }
                lblDate.Text = drv["Abbrv"].ToString() + " " + style + drv["sStartTime"].ToString() + " " + drv["sEndTime"].ToString() + "</span>";
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["Name"].ToString();
                string name = drv["Name"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdFriday1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string style = "<span style='color:Black'>";
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                if (drv["sStartTime"].ToString() != WeekendSTime || drv["sEndTime"].ToString() != WeekendETime)
                {
                    style = "<span style='color:Blue'>";
                }
                lblDate.Text = drv["Abbrv"].ToString() + " " + style + drv["sStartTime"].ToString() + " " + drv["sEndTime"].ToString() + "</span>";
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["Name"].ToString();
                string name = drv["Name"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdSaturday1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string style = "<span style='color:Black'>";
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                Label lblDate = e.Row.FindControl("lblDate") as Label;
                if (drv["sStartTime"].ToString() != WeekendSTime || drv["sEndTime"].ToString() != WeekendETime)
                {
                    style = "<span style='color:Blue'>";
                }
                lblDate.Text = drv["Abbrv"].ToString() + " " + style + drv["sStartTime"].ToString() + " " + drv["sEndTime"].ToString() + "</span>";
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["Name"].ToString();
                string name = drv["Name"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void btnGO_Click(object sender, EventArgs e)
    {
        WeekNumber = commonMethods.GetWeekNumber_New(Convert.ToDateTime(datepicker.Text));
        DateTime mydate = Convert.ToDateTime(datepicker.Text);
        //if (mydate.DayOfWeek == DayOfWeek.Sunday)
        //    WeekNumber += 1;
        int days = 0;
        TimeSpan t = mydate.Subtract(Convert.ToDateTime(DateTime.Now.ToShortDateString()));
        days = t.Days;
        Day = days;
        txtCheck.Text = "2";
        txtMyDay.Text = days.ToString();
        Response.Redirect("../Users/UserWorkShift.aspx?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year + "&day=" + days);
        hidParam.Value = "?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year;
    }
    protected void grdSunday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdMonday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdTuesday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdWednesday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdThursday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdFriday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdSaturday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                string name_With_Message = "<br/>Please click to select user as recipient for a new message!";
                if (lblName.Text.Length > 18)
                {
                    lblName.Text = lblName.Text.Substring(0, 18) + "...";
                    name_With_Message = name + name_With_Message;
                    //lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    //lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
                lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name_With_Message + "')");
                lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void imgBtnMessage_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tblUserInBox userIn = new tblUserInBox();
            userIn.AddNew();
            userIn.FkFromUserID = UserID;
            userIn.FkToUserID = ToUserID;
            userIn.SSubject = txtSubject.Text;
            userIn.SMessage = txtMessage.Text;
            userIn.DReceivedDate = DateTime.Now;
            userIn.BIsread = false;
            userIn.Save();

            tblUserSentBox userOut = new tblUserSentBox();
            userOut.AddNew();
            userOut.FkFromUserID = UserID;
            userOut.FkToUserID = ToUserID;
            userOut.SSubject = txtSubject.Text;
            userOut.SMessage = txtMessage.Text;
            userOut.DSentDate = DateTime.Now;
            userOut.Save();
            ModalPopupExtender1.Hide();
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdSunday1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                {

                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                    if (lblFromAddress.Text.Length > 20)
                    {
                        lblFromAddress.Text = lblFromAddress.Text.Substring(0, 20);
                    }
                }

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdMonday1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdTuesday1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdWednesday1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdThursday1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdFriday1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdSaturday1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdSunday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdMonday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdTuesday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdWednesday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdThursday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdFriday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
    protected void grdSaturday_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "message":
                int uid = Convert.ToInt32(e.CommandArgument);
                ToUserID = uid;
                tblUsers messageUser = new tblUsers();
                messageUser.LoadByPrimaryKey(uid);
                if (messageUser.RowCount > 0)
                    lblToAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;
                messageUser.FlushData();
                messageUser.LoadByPrimaryKey(UserID);
                if (messageUser.RowCount > 0)
                    lblFromAddress.Text = messageUser.SFirstName + " " + messageUser.SLastName;

                ModalPopupExtender1.Show();


                break;
        }
    }
}
