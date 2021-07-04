using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using LC.Model.BMS.BLL;
using MyGeneration.dOOdads;

public partial class AccountManager_ManageControl : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    int WeekNumber;
    int Day;
    int Year;
    int iOrderID = 0;
    static int managerid;
    static int ECUserid;
    protected void Page_Load(object sender, EventArgs e)
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
            LoadECUser();
            LoadManager();
            LoadUserWorkshift();
        }
        lblWeekDates.Text = "Week " + WeekNumber.ToString() + " / " + Year.ToString() + " ( Sunday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1).ToString("dd/MM/yyyy") + " till Saturday " + commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5).ToString("dd/MM/yyyy") + " )";

    }

    #region ECUser Workshift
    private void LoadECUser()
    {
        // imgBtnSaveAssignment.Enabled = false;
        hidParam.Value = "?week=" + WeekNumber + "&year=" + Year;
        tblUserWorkshifts uws = new tblUserWorkshifts();

        uws.FlushData();
        uws.LoadECUserWordkshift(DepartmentID, WeekNumber, Year, 1);
        grdSundayECUser.DataSource = uws.DefaultView;
        grdSundayECUser.DataBind();

        uws.FlushData();
        uws.LoadECUserWordkshift(DepartmentID, WeekNumber, Year, 2);
        grdMondayECUser.DataSource = uws.DefaultView;
        grdMondayECUser.DataBind();

        uws.FlushData();
        uws.LoadECUserWordkshift(DepartmentID, WeekNumber, Year, 3);
        grdTuesdayECUser.DataSource = uws.DefaultView;
        grdTuesdayECUser.DataBind();

        uws.FlushData();
        uws.LoadECUserWordkshift(DepartmentID, WeekNumber, Year, 4);
        grdWednesdayECUser.DataSource = uws.DefaultView;
        grdWednesdayECUser.DataBind();

        uws.FlushData();
        uws.LoadECUserWordkshift(DepartmentID, WeekNumber, Year, 5);
        grdThursdayECUser.DataSource = uws.DefaultView;
        grdThursdayECUser.DataBind();

        uws.FlushData();
        uws.LoadECUserWordkshift(DepartmentID, WeekNumber, Year, 6);
        grdFridayECUser.DataSource = uws.DefaultView;
        grdFridayECUser.DataBind();

        uws.FlushData();
        uws.LoadECUserWordkshift(DepartmentID, WeekNumber, Year, 7);
        grdSaturdayECUser.DataSource = uws.DefaultView;
        grdSaturdayECUser.DataBind();
    }

    protected void grdSundayECUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                HiddenField hidECUserid = e.Row.FindControl("hidECUserid") as HiddenField;

                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (lblName.Text.Length > 11)
                {
                    lblName.Text = lblName.Text.Substring(0, 11) + "...";
                }
                DateTime d = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1);
                lblSundayDate.Text = " " + d.Day + "/" + d.Month;
                tblSpeciality s = new tblSpeciality();
                s.getECUserPosition(Convert.ToInt32(hidECUserid.Value), d);
                if (s.RowCount > 0)
                {
                    for (int i = 0; i < s.RowCount; i++)
                    {
                        name += "<br/>" + s.GetColumn("sSpeciality").ToString();
                        lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                        lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                        s.MoveNext();
                    }
                }
                else
                {
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }




                CheckBox chk = e.Row.FindControl("chkSunday") as CheckBox;
                //**************************** By Rehan
                tblECUserAssignments ecUserAssignments = new tblECUserAssignments();
                ecUserAssignments.CheckAssignedToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                ecUserAssignments.FlushData();
                ecUserAssignments.CheckAssignedRegisterToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                //***************************

                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdSundayECUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            lblMassage.Visible = false;
            ImgbtnCancelAssignment.Visible = false;
            imgBtnSaveAssignment.Visible = true;
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "assign":
                    ECUserid = id;
                    ViewState["date"] = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1);
                    DateTime date = Convert.ToDateTime(ViewState["date"]);
                    int iDayno = Convert.ToInt32(date.DayOfWeek + 1);
                    tblUserWorkshifts objUserWorkshift = new tblUserWorkshifts();
                    objUserWorkshift.getAssignmentsForDay(WeekNumber, Year, iDayno, DepartmentID);
                    grdNormalUsers.DataSource = objUserWorkshift.DefaultView;
                    grdNormalUsers.DataBind();
                    //tblECUserAssignments sp = new tblECUserAssignments(); 
                    tblSpeciality sp = new tblSpeciality();
                    sp.GetRegistersForECuser();
                    grdRegisters.DataSource = sp.DefaultView;
                    grdRegisters.DataBind();

                    ModalPopupExtender2.Show();
                    break;
            }
        }
    }

    protected void grdMondayECUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                HiddenField hidECUserid = e.Row.FindControl("hidECUserid") as HiddenField;

                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (lblName.Text.Length > 11)
                {
                    lblName.Text = lblName.Text.Substring(0, 11) + "...";
                }
                DateTime d = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(0);
                lblMondayDate.Text = " " + d.Day + "/" + d.Month;
                tblSpeciality s = new tblSpeciality();
                s.getECUserPosition(Convert.ToInt32(hidECUserid.Value), d);
                if (s.RowCount > 0)
                {
                    for (int i = 0; i < s.RowCount; i++)
                    {
                        name += "<br/>" + s.GetColumn("sSpeciality").ToString();
                        lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                        lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                        s.MoveNext();
                    }
                }
                else
                {
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                CheckBox chk = e.Row.FindControl("chkMonday") as CheckBox;

                //**************************** By Rehan
                tblECUserAssignments ecUserAssignments = new tblECUserAssignments();
                ecUserAssignments.CheckAssignedToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                ecUserAssignments.FlushData();
                ecUserAssignments.CheckAssignedRegisterToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                //***************************

                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdMondayECUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            lblMassage.Visible = false;
            ImgbtnCancelAssignment.Visible = false;
            imgBtnSaveAssignment.Visible = true;
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "assign":
                    ECUserid = id;
                    ViewState["date"] = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(0);
                    DateTime date = Convert.ToDateTime(ViewState["date"]);
                    int iDayno = Convert.ToInt32(date.DayOfWeek + 1);
                    tblUserWorkshifts objUserWorkshift = new tblUserWorkshifts();
                    objUserWorkshift.getAssignmentsForDay(WeekNumber, Year, iDayno, DepartmentID);
                    grdNormalUsers.DataSource = objUserWorkshift.DefaultView;
                    grdNormalUsers.DataBind();
                    //tblECUserAssignments sp = new tblECUserAssignments(); 
                    tblSpeciality sp = new tblSpeciality();
                    sp.GetRegistersForECuser();
                    grdRegisters.DataSource = sp.DefaultView;
                    grdRegisters.DataBind();
                    ModalPopupExtender2.Show();
                    break;
            }
        }
    }

    protected void grdTuesdayECUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                HiddenField hidECUserid = e.Row.FindControl("hidECUserid") as HiddenField;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (lblName.Text.Length > 11)
                {
                    lblName.Text = lblName.Text.Substring(0, 11) + "...";
                }
                DateTime d = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(1);
                lblTuesdayDate.Text = " " + d.Day + "/" + d.Month;
                tblSpeciality s = new tblSpeciality();
                s.getECUserPosition(Convert.ToInt32(hidECUserid.Value), d);
                if (s.RowCount > 0)
                {
                    for (int i = 0; i < s.RowCount; i++)
                    {
                        name += "<br/>" + s.GetColumn("sSpeciality").ToString();
                        lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                        lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                        s.MoveNext();
                    }
                }
                else
                {
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                CheckBox chk = e.Row.FindControl("chkTuesday") as CheckBox;
                //**************************** By Rehan
                tblECUserAssignments ecUserAssignments = new tblECUserAssignments();
                ecUserAssignments.CheckAssignedToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                ecUserAssignments.FlushData();
                ecUserAssignments.CheckAssignedRegisterToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                //***************************

                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdTuesdayECUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            lblMassage.Visible = false;
            ImgbtnCancelAssignment.Visible = false;
            imgBtnSaveAssignment.Visible = true;
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "assign":
                    ECUserid = id;
                    ViewState["date"] = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(1);
                    DateTime date = Convert.ToDateTime(ViewState["date"]);
                    int iDayno = Convert.ToInt32(date.DayOfWeek + 1);
                    tblUserWorkshifts objUserWorkshift = new tblUserWorkshifts();
                    objUserWorkshift.getAssignmentsForDay(WeekNumber, Year, iDayno, DepartmentID);
                    grdNormalUsers.DataSource = objUserWorkshift.DefaultView;
                    grdNormalUsers.DataBind();
                    //tblECUserAssignments sp = new tblECUserAssignments(); 
                    tblSpeciality sp = new tblSpeciality();
                    sp.GetRegistersForECuser();
                    grdRegisters.DataSource = sp.DefaultView;
                    grdRegisters.DataBind();
                    ModalPopupExtender2.Show();
                    break;
            }
        }
    }

    protected void grdWednesdayECUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                HiddenField hidECUserid = e.Row.FindControl("hidECUserid") as HiddenField;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (lblName.Text.Length > 11)
                {
                    lblName.Text = lblName.Text.Substring(0, 11) + "...";
                }
                DateTime d = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(2);
                lblWednesdayDate.Text = " " + d.Day + "/" + d.Month;
                tblSpeciality s = new tblSpeciality();
                s.getECUserPosition(Convert.ToInt32(hidECUserid.Value), d);
                if (s.RowCount > 0)
                {
                    for (int i = 0; i < s.RowCount; i++)
                    {
                        name += "<br/>" + s.GetColumn("sSpeciality").ToString();
                        lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                        lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                        s.MoveNext();
                    }
                }
                else
                {
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                CheckBox chk = e.Row.FindControl("chkWednesday") as CheckBox;
                //**************************** By Rehan
                tblECUserAssignments ecUserAssignments = new tblECUserAssignments();
                ecUserAssignments.CheckAssignedToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                ecUserAssignments.FlushData();
                ecUserAssignments.CheckAssignedRegisterToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                //***************************

                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdWednesdayECUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            lblMassage.Visible = false;
            ImgbtnCancelAssignment.Visible = false;
            imgBtnSaveAssignment.Visible = true;
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "assign":
                    ECUserid = id;
                    ViewState["date"] = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(2);
                    DateTime date = Convert.ToDateTime(ViewState["date"]);
                    int iDayno = Convert.ToInt32(date.DayOfWeek + 1);
                    tblUserWorkshifts objUserWorkshift = new tblUserWorkshifts();
                    objUserWorkshift.getAssignmentsForDay(WeekNumber, Year, iDayno, DepartmentID);
                    grdNormalUsers.DataSource = objUserWorkshift.DefaultView;
                    grdNormalUsers.DataBind();
                    //tblECUserAssignments sp = new tblECUserAssignments(); 
                    tblSpeciality sp = new tblSpeciality();
                    sp.GetRegistersForECuser();
                    grdRegisters.DataSource = sp.DefaultView;
                    grdRegisters.DataBind();
                    ModalPopupExtender2.Show();
                    break;
            }
        }
    }

    protected void grdThursdayECUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                HiddenField hidECUserid = e.Row.FindControl("hidECUserid") as HiddenField;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (lblName.Text.Length > 11)
                {
                    lblName.Text = lblName.Text.Substring(0, 11) + "...";
                }
                DateTime d = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(3);
                lblThrusdayDate.Text = " " + d.Day + "/" + d.Month;
                tblSpeciality s = new tblSpeciality();
                s.getECUserPosition(Convert.ToInt32(hidECUserid.Value), d);
                if (s.RowCount > 0)
                {
                    for (int i = 0; i < s.RowCount; i++)
                    {
                        name += "<br/>" + s.GetColumn("sSpeciality").ToString();
                        lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                        lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                        s.MoveNext();
                    }
                }
                else
                {
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                CheckBox chk = e.Row.FindControl("chkThursday") as CheckBox;
                //**************************** By Rehan
                tblECUserAssignments ecUserAssignments = new tblECUserAssignments();
                ecUserAssignments.CheckAssignedToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                ecUserAssignments.FlushData();
                ecUserAssignments.CheckAssignedRegisterToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                //***************************

                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdThursdayECUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            lblMassage.Visible = false;
            ImgbtnCancelAssignment.Visible = false;
            imgBtnSaveAssignment.Visible = true;
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "assign":
                    ECUserid = id;
                    ViewState["date"] = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(3);
                    DateTime date = Convert.ToDateTime(ViewState["date"]);
                    int iDayno = Convert.ToInt32(date.DayOfWeek + 1);
                    tblUserWorkshifts objUserWorkshift = new tblUserWorkshifts();
                    objUserWorkshift.getAssignmentsForDay(WeekNumber, Year, iDayno, DepartmentID);
                    grdNormalUsers.DataSource = objUserWorkshift.DefaultView;
                    grdNormalUsers.DataBind();

                    //tblECUserAssignments sp = new tblECUserAssignments(); 
                    tblSpeciality sp = new tblSpeciality();
                    sp.GetRegistersForECuser();
                    grdRegisters.DataSource = sp.DefaultView;
                    grdRegisters.DataBind();
                    ModalPopupExtender2.Show();
                    break;
            }
        }
    }

    protected void grdFridayECUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                HiddenField hidECUserid = e.Row.FindControl("hidECUserid") as HiddenField;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (lblName.Text.Length > 11)
                {
                    lblName.Text = lblName.Text.Substring(0, 11) + "...";
                }
                DateTime d = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(4);
                lblFridayDate.Text = " " + d.Day + "/" + d.Month;
                tblSpeciality s = new tblSpeciality();
                s.getECUserPosition(Convert.ToInt32(hidECUserid.Value), d);
                if (s.RowCount > 0)
                {
                    for (int i = 0; i < s.RowCount; i++)
                    {
                        name += "<br/>" + s.GetColumn("sSpeciality").ToString();
                        lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                        lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                        s.MoveNext();
                    }
                }
                else
                {
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                CheckBox chk = e.Row.FindControl("chkFriday") as CheckBox;
                //**************************** By Rehan
                tblECUserAssignments ecUserAssignments = new tblECUserAssignments();
                ecUserAssignments.CheckAssignedToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                ecUserAssignments.FlushData();
                ecUserAssignments.CheckAssignedRegisterToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                //***************************

                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdFridayECUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            lblMassage.Visible = false;
            ImgbtnCancelAssignment.Visible = false;
            imgBtnSaveAssignment.Visible = true;
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "assign":
                    ECUserid = id;
                    ViewState["date"] = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(4);
                    DateTime date = Convert.ToDateTime(ViewState["date"]);
                    int iDayno = Convert.ToInt32(date.DayOfWeek + 1);
                    tblUserWorkshifts objUserWorkshift = new tblUserWorkshifts();
                    objUserWorkshift.getAssignmentsForDay(WeekNumber, Year, iDayno, DepartmentID);
                    grdNormalUsers.DataSource = objUserWorkshift.DefaultView;
                    grdNormalUsers.DataBind();
                    //tblECUserAssignments sp = new tblECUserAssignments(); 
                    tblSpeciality sp = new tblSpeciality();
                    sp.GetRegistersForECuser();
                    grdRegisters.DataSource = sp.DefaultView;
                    grdRegisters.DataBind();
                    ModalPopupExtender2.Show();
                    break;
            }
        }
    }

    protected void grdSaturdayECUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;
                //Label lblName = e.Row.FindControl("lblName") as Label;
                LinkButton lblName = e.Row.FindControl("lnkName") as LinkButton;
                HiddenField hidECUserid = e.Row.FindControl("hidECUserid") as HiddenField;
                lblName.Text = drv["FullName"].ToString();
                string name = drv["FullName"].ToString();
                if (lblName.Text.Length > 11)
                {
                    lblName.Text = lblName.Text.Substring(0, 11) + "...";
                }
                DateTime d = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5);
                lblSaturdayDate.Text = " " + d.Day + "/" + d.Month;
                tblSpeciality s = new tblSpeciality();
                s.getECUserPosition(Convert.ToInt32(hidECUserid.Value), d);
                if (s.RowCount > 0)
                {
                    for (int i = 0; i < s.RowCount; i++)
                    {
                        name += "<br/>" + s.GetColumn("sSpeciality").ToString();
                        lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                        lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                        s.MoveNext();
                    }
                }
                else
                {
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                CheckBox chk = e.Row.FindControl("chkSaturday") as CheckBox;
                //**************************** By Rehan
                tblECUserAssignments ecUserAssignments = new tblECUserAssignments();
                ecUserAssignments.CheckAssignedToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
                ecUserAssignments.FlushData();
                ecUserAssignments.CheckAssignedRegisterToECUsersByDateandID(d, Convert.ToInt32(hidECUserid.Value));
                if (ecUserAssignments.RowCount > 0)
                {
                    chk.Checked = true;
                }
                //***************************

                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdSaturdayECUser_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            lblMassage.Visible = false;
            ImgbtnCancelAssignment.Visible = false;
            imgBtnSaveAssignment.Visible = true;
            int id = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "assign":
                    ECUserid = id;
                    ViewState["date"] = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5);
                    DateTime date = Convert.ToDateTime(ViewState["date"]);
                    int iDayno = Convert.ToInt32(date.DayOfWeek + 1);
                    tblUserWorkshifts objUserWorkshift = new tblUserWorkshifts();
                    objUserWorkshift.getAssignmentsForDay(WeekNumber, Year, iDayno, DepartmentID);

                    grdNormalUsers.DataSource = objUserWorkshift.DefaultView;
                    grdNormalUsers.DataBind();

                    // //tblECUserAssignments sp = new tblECUserAssignments(); 
                    tblSpeciality sp = new tblSpeciality();
                    sp.GetRegistersForECuser();
                    grdRegisters.DataSource = sp.DefaultView;
                    grdRegisters.DataBind();
                    ModalPopupExtender2.Show();
                    break;
            }
        }
    }

    protected void grdNormalUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                int tempOrder = iOrderID;
                HiddenField hidECID = e.Row.FindControl("hidECID") as HiddenField;
                HiddenField hidspid = e.Row.FindControl("hidspid") as HiddenField;
                HiddenField hdnOrderID = e.Row.FindControl("hdnOrderID") as HiddenField;

                CheckBox chkBox = e.Row.FindControl("chkBox") as CheckBox;
                HtmlGenericControl hrSeprator = e.Row.FindControl("hrSeprator") as HtmlGenericControl;
                HtmlGenericControl hrSeprator2 = e.Row.FindControl("hrSeprator2") as HtmlGenericControl;
                Label lblNormalUserName = e.Row.FindControl("lblNormalUserName") as Label;

                tblSpeciality ObjSpeciality = new tblSpeciality();
                ObjSpeciality.CheckSeperator(iOrderID, Convert.ToInt32(hdnOrderID.Value));
                iOrderID = Convert.ToInt32(hdnOrderID.Value);

                if (ObjSpeciality.RowCount > 0 && tempOrder != 0)
                {
                    for (int i = 0; i < ObjSpeciality.RowCount; i++)
                    {
                        if (ObjSpeciality.s_SSpeciality.Contains("Separator"))
                        {
                            hrSeprator.Style.Add("display", "block");

                        }
                        ObjSpeciality.MoveNext();
                    }
                }

                tblECUserAssignments userAssignment = new tblECUserAssignments();
                userAssignment.CheckAssignedToECUsers(Convert.ToDateTime(ViewState["date"]));
                if (userAssignment.RowCount > 0)
                {
                    for (int j = 0; j < userAssignment.RowCount; j++)
                    {
                        if (hidspid.Value == userAssignment.FkSpecialtyID.ToString())
                        {
                            chkBox.Checked = true;
                            if (userAssignment.ECUserID != ECUserid)
                            {
                                chkBox.Enabled = false;
                            }
                        }
                        userAssignment.MoveNext();
                    }
                }
                else
                {
                    chkBox.Checked = false;
                }


            }
            catch (Exception ex)
            { }
        }
    }
    protected void grdRegisters_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                HiddenField hidECID = e.Row.FindControl("hidECID") as HiddenField;
                HiddenField hidRegisterid = e.Row.FindControl("hidRegisterid") as HiddenField;

                CheckBox chkBox = e.Row.FindControl("chkBox") as CheckBox;
                tblECUserAssignments userAssignment = new tblECUserAssignments();
                userAssignment.CheckAssignedRegisters(Convert.ToInt32(hidRegisterid.Value), ECUserid, Convert.ToDateTime(ViewState["date"]));
                if (userAssignment.RowCount > 0)
                {
                    chkBox.Checked = true;
                }
                else
                {
                    userAssignment.FlushData();
                    userAssignment.CheckAssignedRegistersToECUsers(Convert.ToInt32(hidRegisterid.Value), Convert.ToDateTime(ViewState["date"]));
                    if (userAssignment.RowCount > 0)
                    {
                        chkBox.Checked = true;
                        chkBox.Enabled = false;
                    }
                    else
                    {
                        chkBox.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            { }
        }
    }
    protected void ImgbtnCancelAssignment_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender2.Hide();
    
    }
    protected void imgBtnSaveAssignment_Click(object sender, ImageClickEventArgs e)
    {
        bool Flag = false;
        #region Checking Positions
        if (grdNormalUsers.Rows.Count > 0)
        {
            for (int i = 0; i < grdNormalUsers.Rows.Count; i++)
            {
                CheckBox chk = grdNormalUsers.Rows[i].FindControl("chkBox") as CheckBox;
                if (chk.Checked)
                {
                    if (chk.Enabled == false)
                    {

                    }
                    else
                    {
                        HiddenField hid = grdNormalUsers.Rows[i].FindControl("hidspid") as HiddenField;
                        tblECUserAssignments userAssign = new tblECUserAssignments();
                        userAssign.FlushData();
                        userAssign.CheckAssignedUsers(Convert.ToInt32(hid.Value), ECUserid, Convert.ToDateTime(ViewState["date"]));
                        if (userAssign.RowCount == 0)
                        {
                            userAssign.FlushData();
                            userAssign.AddNew();
                            userAssign.FkSpecialtyID = Convert.ToInt32(hid.Value);
                            userAssign.ECUserID = ECUserid;
                            userAssign.DCreatedDate = DateTime.Now;
                            userAssign.DModifiedDate = Convert.ToDateTime(ViewState["date"]);
                            userAssign.Save();
                            //SetWorkshift(ref chk, Convert.ToInt32(Convert.ToDateTime(ViewState["date"]).DayOfWeek)+1, ECUserid, false);

                        }
                        Flag = true;
                    }
                }
                else if (!chk.Checked)
                {
                    HiddenField hid = grdNormalUsers.Rows[i].FindControl("hidspid") as HiddenField;
                    tblECUserAssignments ecusrs = new tblECUserAssignments();
                    ecusrs.CheckAssignedUsers(Convert.ToInt32(hid.Value), ECUserid, Convert.ToDateTime(ViewState["date"]));
                    if (ecusrs.RowCount > 0)
                    {
                        ecusrs.MarkAsDeleted();
                        ecusrs.Save();
                    }

                }
            }
            LoadECUser();

        }

        #endregion

        #region Checking Registers

        if (grdRegisters.Rows.Count > 0)
        {
            for (int i = 0; i < grdRegisters.Rows.Count; i++)
            {
                CheckBox chk = grdRegisters.Rows[i].FindControl("chkBox") as CheckBox;
                if (chk.Enabled)
                {
                    if (chk.Checked)
                    {
                        HiddenField hid = grdRegisters.Rows[i].FindControl("hidRegisterid") as HiddenField;
                        tblECUserAssignments userAssign = new tblECUserAssignments();
                        userAssign.FlushData();
                        userAssign.CheckAssignedRegisters(Convert.ToInt32(hid.Value), ECUserid, Convert.ToDateTime(ViewState["date"]));
                        if (userAssign.RowCount == 0)
                        {
                            userAssign.FlushData();
                            userAssign.AddNew();
                            userAssign.FkRegisterID = Convert.ToInt32(hid.Value);
                            userAssign.ECUserID = ECUserid;
                            userAssign.DCreatedDate = DateTime.Now;
                            userAssign.DRegisterDate = Convert.ToDateTime(ViewState["date"]);
                            userAssign.Save();
                            //SetWorkshift(ref chk, Convert.ToInt32(Convert.ToDateTime(ViewState["date"]).DayOfWeek)+1, ECUserid, false);

                        }
                        Flag = true;
                    }
                    else if (!chk.Checked)
                    {
                        HiddenField hid = grdRegisters.Rows[i].FindControl("hidRegisterid") as HiddenField;
                        tblECUserAssignments ecusrs = new tblECUserAssignments();
                        ecusrs.CheckAssignedRegisters(Convert.ToInt32(hid.Value), ECUserid, Convert.ToDateTime(ViewState["date"]));
                        if (ecusrs.RowCount > 0)
                        {
                            //ecusrs.s_DRegisterDate = "";
                            ecusrs.MarkAsDeleted();
                            ecusrs.Save();
                        }

                    }
                }
            }
            LoadECUser();
            upnl.Update();
            ModalPopupExtender2.Hide();
        }
        #endregion
        CheckBox chktemp = new CheckBox();
        if (Flag == true)
        {
            chktemp.Checked = true;
            lblMassage.Visible = true;
            lblMassage.Text = "Successfully Save this position";
        }
        else
        {
            chktemp.Checked = false;
            lblMassage.Visible = true;
            lblMassage.Text = "Not Save this position";

        }
        //SetWorkshift(ref chktemp, Convert.ToInt32(Convert.ToDateTime(ViewState["date"]).DayOfWeek) + 1, ECUserid, false);
        upnl.Update();
       
        ImgbtnCancelAssignment.Visible = true;
        imgBtnSaveAssignment.Visible = false;
        ModalPopupExtender2.Show();

    }
    #endregion

    #region Manger Workshift
    private void LoadManager()
    {
        tblUsers u = new tblUsers();
        u.getDepartmentManager(DepartmentID);
        if (u.RowCount > 0)
        {
            chkSundayManger.Text = u.GetColumn("FullName").ToString();
            chkMondayManger.Text = u.GetColumn("FullName").ToString();
            chkTuesdayManger.Text = u.GetColumn("FullName").ToString();
            chkWednesdayManger.Text = u.GetColumn("FullName").ToString();
            chkThursdayManger.Text = u.GetColumn("FullName").ToString();
            chkFridayManager.Text = u.GetColumn("FullName").ToString();
            chkSaturdayManager.Text = u.GetColumn("FullName").ToString();
            managerid = u.PkUserID;
        }

        tblUserWorkshifts uws = new tblUserWorkshifts();

        uws.FlushData();
        uws.LoadManagersWorkshift(DepartmentID, WeekNumber, Year, 1);
        grdSundayManager.DataSource = uws.DefaultView;
        grdSundayManager.DataBind();

        uws.FlushData();
        uws.LoadManagersWorkshift(DepartmentID, WeekNumber, Year, 2);
        grdMondayManager.DataSource = uws.DefaultView;
        grdMondayManager.DataBind();

        uws.FlushData();
        uws.LoadManagersWorkshift(DepartmentID, WeekNumber, Year, 3);
        grdTuesdayManager.DataSource = uws.DefaultView;
        grdTuesdayManager.DataBind();

        uws.FlushData();
        uws.LoadManagersWorkshift(DepartmentID, WeekNumber, Year, 4);
        grdWednesdayManager.DataSource = uws.DefaultView;
        grdWednesdayManager.DataBind();

        uws.FlushData();
        uws.LoadManagersWorkshift(DepartmentID, WeekNumber, Year, 5);
        grdThursdayManager.DataSource = uws.DefaultView;
        grdThursdayManager.DataBind();

        uws.FlushData();
        uws.LoadManagersWorkshift(DepartmentID, WeekNumber, Year, 6);
        grdFridayManager.DataSource = uws.DefaultView;
        grdFridayManager.DataBind();

        uws.FlushData();
        uws.LoadManagersWorkshift(DepartmentID, WeekNumber, Year, 7);
        grdSaturdayManager.DataSource = uws.DefaultView;
        grdSaturdayManager.DataBind();
    }



    protected void grdSundayManager_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (lblName.Text.Length > 15)
                {
                    lblName.Text = lblName.Text.Substring(0, 15) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                string singleDate = string.Empty;
                string rangeStartDate = string.Empty;
                string rangeEndDate = string.Empty;


                CheckBox chk = e.Row.FindControl("chkSunday") as CheckBox;
                DateTime stdate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1);
                tblManagerDayOff dayOff = new tblManagerDayOff();
                dayOff.getSingleDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }

                dayOff.FlushData();
                dayOff.getRangeDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }
                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdMondayManager_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (lblName.Text.Length > 15)
                {
                    lblName.Text = lblName.Text.Substring(0, 15) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                CheckBox chk = e.Row.FindControl("chkMonday") as CheckBox;
                DateTime stdate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(0);
                tblManagerDayOff dayOff = new tblManagerDayOff();
                dayOff.getSingleDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }

                dayOff.FlushData();
                dayOff.getRangeDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }
                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdTuesdayManager_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (lblName.Text.Length > 15)
                {
                    lblName.Text = lblName.Text.Substring(0, 15) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                CheckBox chk = e.Row.FindControl("chkTuesday") as CheckBox;
                DateTime stdate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(1);
                tblManagerDayOff dayOff = new tblManagerDayOff();
                dayOff.getSingleDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }

                dayOff.FlushData();
                dayOff.getRangeDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }
                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdWednesdayManager_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (lblName.Text.Length > 15)
                {
                    lblName.Text = lblName.Text.Substring(0, 15) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                CheckBox chk = e.Row.FindControl("chkWednesday") as CheckBox;
                DateTime stdate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(2);
                tblManagerDayOff dayOff = new tblManagerDayOff();
                dayOff.getSingleDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }

                dayOff.FlushData();
                dayOff.getRangeDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }
                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdThursdayManager_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (lblName.Text.Length > 15)
                {
                    lblName.Text = lblName.Text.Substring(0, 15) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                CheckBox chk = e.Row.FindControl("chkThursday") as CheckBox;
                DateTime stdate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(3);
                tblManagerDayOff dayOff = new tblManagerDayOff();
                dayOff.getSingleDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }

                dayOff.FlushData();
                dayOff.getRangeDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }
                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdFridayManager_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (lblName.Text.Length > 15)
                {
                    lblName.Text = lblName.Text.Substring(0, 15) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                CheckBox chk = e.Row.FindControl("chkFriday") as CheckBox;
                DateTime stdate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(4);
                tblManagerDayOff dayOff = new tblManagerDayOff();
                dayOff.getSingleDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }

                dayOff.FlushData();
                dayOff.getRangeDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }
                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void grdSaturdayManager_RowDataBound(object sender, GridViewRowEventArgs e)
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
                if (lblName.Text.Length > 15)
                {
                    lblName.Text = lblName.Text.Substring(0, 15) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                CheckBox chk = e.Row.FindControl("chkSaturday") as CheckBox;
                DateTime stdate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5);
                tblManagerDayOff dayOff = new tblManagerDayOff();
                dayOff.getSingleDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }

                dayOff.FlushData();
                dayOff.getRangeDayOff(Convert.ToInt32(drv["pkuserid"]), stdate.Date);
                if (dayOff.RowCount > 0)
                {
                    chk.Checked = false;
                }
                //if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                //    chk.Checked = true;
                //else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                //    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    #endregion

    #region User Workshift
    private void LoadUserWorkshift()
    {
        hidParam.Value = "?week=" + WeekNumber + "&year=" + Year;
        tblUserWorkshifts uws = new tblUserWorkshifts();

        uws.FlushData();
        uws.getSpecialUserWorkshift(DepartmentID, WeekNumber, Year, 1);
        grdSunday.DataSource = uws.DefaultView;
        grdSunday.DataBind();

        uws.FlushData();
        uws.getSpecialUserWorkshift(DepartmentID, WeekNumber, Year, 2);
        grdMonday.DataSource = uws.DefaultView;
        grdMonday.DataBind();

        uws.FlushData();
        uws.getSpecialUserWorkshift(DepartmentID, WeekNumber, Year, 3);
        grdTuesday.DataSource = uws.DefaultView;
        grdTuesday.DataBind();

        uws.FlushData();
        uws.getSpecialUserWorkshift(DepartmentID, WeekNumber, Year, 4);
        grdWednesday.DataSource = uws.DefaultView;
        grdWednesday.DataBind();

        uws.FlushData();
        uws.getSpecialUserWorkshift(DepartmentID, WeekNumber, Year, 5);
        grdThursday.DataSource = uws.DefaultView;
        grdThursday.DataBind();

        uws.FlushData();
        uws.getSpecialUserWorkshift(DepartmentID, WeekNumber, Year, 6);
        grdFriday.DataSource = uws.DefaultView;
        grdFriday.DataBind();

        uws.FlushData();
        uws.getSpecialUserWorkshift(DepartmentID, WeekNumber, Year, 7);
        grdSaturday.DataSource = uws.DefaultView;
        grdSaturday.DataBind();
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
                if (lblName.Text.Length > 12)
                {
                    lblName.Text = lblName.Text.Substring(0, 12) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                CheckBox chk = e.Row.FindControl("chkSundayOther") as CheckBox;
                if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                    chk.Checked = true;
                else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                    chk.Checked = false;
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
                if (lblName.Text.Length > 12)
                {
                    lblName.Text = lblName.Text.Substring(0, 12) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                CheckBox chk = e.Row.FindControl("chkMondayOther") as CheckBox;
                if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                    chk.Checked = true;
                else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                    chk.Checked = false;
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
                if (lblName.Text.Length > 12)
                {
                    lblName.Text = lblName.Text.Substring(0, 12) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }

                CheckBox chk = e.Row.FindControl("chkTuesdayOther") as CheckBox;
                if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                    chk.Checked = true;
                else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                    chk.Checked = false;
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
                if (lblName.Text.Length > 12)
                {
                    lblName.Text = lblName.Text.Substring(0, 12) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                CheckBox chk = e.Row.FindControl("chkWednesdayOther") as CheckBox;
                if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                    chk.Checked = true;
                else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                    chk.Checked = false;
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
                if (lblName.Text.Length > 12)
                {
                    lblName.Text = lblName.Text.Substring(0, 12) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                CheckBox chk = e.Row.FindControl("chkThursdayOther") as CheckBox;
                if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                    chk.Checked = true;
                else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                    chk.Checked = false;
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
                if (lblName.Text.Length > 12)
                {
                    lblName.Text = lblName.Text.Substring(0, 12) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                CheckBox chk = e.Row.FindControl("chkFridayOther") as CheckBox;
                if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                    chk.Checked = true;
                else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                    chk.Checked = false;
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
                if (lblName.Text.Length > 12)
                {
                    lblName.Text = lblName.Text.Substring(0, 12) + "...";
                    lblName.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblName.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                CheckBox chk = e.Row.FindControl("chkSaturdayOther") as CheckBox;
                if (Convert.ToInt32(drv["workshiftid"].ToString()) > 0)
                    chk.Checked = true;
                else if (Convert.ToInt32(drv["workshiftid"].ToString()) == 0)
                    chk.Checked = false;
            }
            catch (Exception ex)
            {

            }
        }
    }
    #endregion

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
        Response.Redirect("../AccountManager/ManageControl.aspx?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year + "&day=" + days);
        hidParam.Value = "?week=" + WeekNumber + "&year=" + Convert.ToDateTime(datepicker.Text).Year;
    }
    protected void imgBtnSave_Click(object sender, EventArgs e)
    {

        #region saving & removing ECUSer WorkShift
        for (int i = 0; i < grdSundayECUser.Rows.Count; i++)
        {
            HiddenField hidECUserid = grdSundayECUser.Rows[i].FindControl("hidECUserid") as HiddenField;
            CheckBox chk = grdSundayECUser.Rows[i].FindControl("chkSunday") as CheckBox;
            if (chk != null)
            {
                //SetWorkshift(ref chk, 1, Convert.ToInt32(hidECUserid.Value), false);
            }
        }

        for (int i = 0; i < grdMondayECUser.Rows.Count; i++)
        {
            HiddenField hidECUserid = grdMondayECUser.Rows[i].FindControl("hidECUserid") as HiddenField;
            CheckBox chk = grdMondayECUser.Rows[i].FindControl("chkMonday") as CheckBox;
            //SetWorkshift(ref chk, 2, Convert.ToInt32(hidECUserid.Value), false);
        }

        for (int i = 0; i < grdTuesdayECUser.Rows.Count; i++)
        {
            HiddenField hidECUserid = grdTuesdayECUser.Rows[i].FindControl("hidECUserid") as HiddenField;
            CheckBox chk = grdTuesdayECUser.Rows[i].FindControl("chkTuesday") as CheckBox;
            //SetWorkshift(ref chk, 3, Convert.ToInt32(hidECUserid.Value), false);
        }

        for (int i = 0; i < grdWednesdayECUser.Rows.Count; i++)
        {
            HiddenField hidECUserid = grdWednesdayECUser.Rows[i].FindControl("hidECUserid") as HiddenField;
            CheckBox chk = grdWednesdayECUser.Rows[i].FindControl("chkWednesday") as CheckBox;
            //SetWorkshift(ref chk, 4, Convert.ToInt32(hidECUserid.Value), false);
        }

        for (int i = 0; i < grdThursdayECUser.Rows.Count; i++)
        {
            HiddenField hidECUserid = grdThursdayECUser.Rows[i].FindControl("hidECUserid") as HiddenField;
            CheckBox chk = grdThursdayECUser.Rows[i].FindControl("chkThursday") as CheckBox;
            //SetWorkshift(ref chk, 5, Convert.ToInt32(hidECUserid.Value), false);
        }

        for (int i = 0; i < grdFridayECUser.Rows.Count; i++)
        {
            HiddenField hidECUserid = grdFridayECUser.Rows[i].FindControl("hidECUserid") as HiddenField;
            CheckBox chk = grdFridayECUser.Rows[i].FindControl("chkFriday") as CheckBox;
            //SetWorkshift(ref chk, 6, Convert.ToInt32(hidECUserid.Value), false);
        }

        for (int i = 0; i < grdSaturdayECUser.Rows.Count; i++)
        {
            HiddenField hidECUserid = grdSaturdayECUser.Rows[i].FindControl("hidECUserid") as HiddenField;
            CheckBox chk = grdSaturdayECUser.Rows[i].FindControl("chkSaturday") as CheckBox;
            //SetWorkshift(ref chk, 7, Convert.ToInt32(hidECUserid.Value), false);
        }

        #endregion

        #region saving & removing User WorkShift

        for (int i = 0; i < grdSunday.Rows.Count; i++)
        {
            HiddenField hidUserid = grdSunday.Rows[i].FindControl("hidUserid") as HiddenField;
            CheckBox chk = grdSunday.Rows[i].FindControl("chkSundayOther") as CheckBox;
            SetWorkshift(ref chk, 1, Convert.ToInt32(hidUserid.Value), true);
        }

        for (int i = 0; i < grdMonday.Rows.Count; i++)
        {
            HiddenField hidUserid = grdMonday.Rows[i].FindControl("hidUserid") as HiddenField;
            CheckBox chk = grdMonday.Rows[i].FindControl("chkMondayOther") as CheckBox;
            SetWorkshift(ref chk, 2, Convert.ToInt32(hidUserid.Value), true);
        }

        for (int i = 0; i < grdTuesday.Rows.Count; i++)
        {
            HiddenField hidUserid = grdTuesday.Rows[i].FindControl("hidUserid") as HiddenField;
            CheckBox chk = grdTuesday.Rows[i].FindControl("chkTuesdayOther") as CheckBox;
            SetWorkshift(ref chk, 3, Convert.ToInt32(hidUserid.Value), true);
        }

        for (int i = 0; i < grdWednesday.Rows.Count; i++)
        {
            HiddenField hidUserid = grdWednesday.Rows[i].FindControl("hidUserid") as HiddenField;
            CheckBox chk = grdWednesday.Rows[i].FindControl("chkWednesdayOther") as CheckBox;
            SetWorkshift(ref chk, 4, Convert.ToInt32(hidUserid.Value), true);
        }

        for (int i = 0; i < grdThursday.Rows.Count; i++)
        {
            HiddenField hidUserid = grdThursday.Rows[i].FindControl("hidUserid") as HiddenField;
            CheckBox chk = grdThursday.Rows[i].FindControl("chkThursdayOther") as CheckBox;
            SetWorkshift(ref chk, 5, Convert.ToInt32(hidUserid.Value), true);
        }

        for (int i = 0; i < grdFriday.Rows.Count; i++)
        {
            HiddenField hidUserid = grdFriday.Rows[i].FindControl("hidUserid") as HiddenField;
            CheckBox chk = grdFriday.Rows[i].FindControl("chkFridayOther") as CheckBox;
            SetWorkshift(ref chk, 6, Convert.ToInt32(hidUserid.Value), true);
        }

        for (int i = 0; i < grdSaturday.Rows.Count; i++)
        {
            HiddenField hidUserid = grdSaturday.Rows[i].FindControl("hidUserid") as HiddenField;
            CheckBox chk = grdSaturday.Rows[i].FindControl("chkSaturdayOther") as CheckBox;
            SetWorkshift(ref chk, 7, Convert.ToInt32(hidUserid.Value), true);
        }
        #endregion

        #region saving & removing Manager Workshift
        /*
        SetWorkshift(ref chkSundayManger, 1, managerid);
        SetWorkshift(ref chkMondayManger, 2, managerid);
        SetWorkshift(ref chkTuesdayManger, 3, managerid);
        SetWorkshift(ref chkWednesdayManger, 4, managerid);
        SetWorkshift(ref chkThursdayManger, 5, managerid);
        SetWorkshift(ref chkFridayManager, 6, managerid);
        SetWorkshift(ref chkSaturdayManager, 7, managerid);
       */
        #endregion

    }
    protected void imgBtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("../AccountManager/ManageControl.aspx?week=" + WeekNumber + "&year=" + Year + "&day=" + Day);
    }
    private void SetWorkshift(ref CheckBox chk, int day, int userid, bool special)
    {
        if (!special)
        {
            tblUserWorkshifts userWorkshifts = new tblUserWorkshifts();
            if (chk.Checked)
            {

                userWorkshifts.FlushData();
                userWorkshifts.getECUserForDay(WeekNumber, Year, day, userid);
                if (userWorkshifts.RowCount > 0)
                {

                }
                else
                {
                    userWorkshifts.FlushData();
                    userWorkshifts.AddNew();
                    userWorkshifts.FkUserID = userid;
                    userWorkshifts.IDayNumber = day;
                    userWorkshifts.IWeekNumber = WeekNumber;
                    userWorkshifts.IYear = Year;
                    userWorkshifts.DWeekStartDate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1);
                    userWorkshifts.DWeekEndDate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5);
                    userWorkshifts.BLate = false;
                    userWorkshifts.BOnTime = false;

                    tblSpeciality s = new tblSpeciality();
                    s.getECUserSpeciality(userid);
                    if (s.RowCount > 0)
                    {
                        userWorkshifts.FkSpecialityID = Convert.ToInt32(s.GetColumn("fkspecialitytypeid").ToString());
                    }

                    userWorkshifts.DCreateDate = DateTime.Now;
                    userWorkshifts.DModifiedDate = DateTime.Now;
                    userWorkshifts.Save();

                    lblRecordMessage.Text = "Successfully Saved";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "JquSaved", "$(function(){RecordSaved();});", true);
                    upnl.Update();
                }
            }
            else if (!chk.Checked)
            {
                userWorkshifts.FlushData();
                userWorkshifts.getECUserForDay(WeekNumber, Year, day, userid);
                if (userWorkshifts.RowCount > 0)
                {
                    userWorkshifts.MarkAsDeleted();
                    userWorkshifts.Save();

                    lblRecordMessage.Text = "Successfully Removed";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "JquSaved", "$(function(){RecordSaved();});", true);
                    upnl.Update();
                }
            }
        }
        else if (special)
        {
            tblUserWorkshifts uws = new tblUserWorkshifts();
            if (chk.Checked)
            {
                uws.FlushData();
                uws.getSpeciaUserForDay(WeekNumber, Year, day, userid);
                if (uws.RowCount > 0)
                {

                }
                else
                {
                    uws.FlushData();
                    uws.AddNew();
                    uws.FkUserID = userid;
                    uws.IDayNumber = day;
                    uws.IWeekNumber = WeekNumber;
                    uws.IYear = Year;
                    uws.DWeekStartDate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(-1);
                    uws.DWeekEndDate = commonMethods.GetWeekStartDate(Year, WeekNumber).AddDays(5);
                    uws.BLate = false;
                    uws.BOnTime = false;
                    uws.DCreateDate = DateTime.Now;
                    uws.DModifiedDate = DateTime.Now;
                    uws.Save();

                    lblRecordMessage.Text = "Successfully Saved";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "JquSaved", "$(function(){RecordSaved();});", true);
                    upnl.Update();
                }
            }
            else if (!chk.Checked)
            {
                uws.FlushData();
                uws.getSpeciaUserForDay(WeekNumber, Year, day, userid);
                if (uws.RowCount > 0)
                {
                    uws.MarkAsDeleted();
                    uws.Save();

                    lblRecordMessage.Text = "Successfully Removed";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "JquSaved", "$(function(){RecordSaved();});", true);
                    upnl.Update();
                }
            }
        }
    }

    
}
