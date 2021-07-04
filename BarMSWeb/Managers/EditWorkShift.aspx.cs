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
using LC.Model.BMS.BLL;
using MyGeneration.dOOdads;

public partial class Managers_EditWorkShift : System.Web.UI.Page
{
    int CurrentWeek;
    int UserID;
    int DepartmentID;
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentWeek = commonMethods.GetWeeknumber(DateTime.Now);
        lblError.Visible = false;
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            UserID = user.UserID;
            DepartmentID = user.DepartmentID;
            if (user.AccessLevel != 2)
            {
                Session["UserLogin"] = null;
                Response.Redirect("../West_login.aspx");
            }
            //UserID = user.UserID;
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }
        if (!Page.IsPostBack)
        {
            LoadDropdowns();
        }
    }
    private void LoadDropdowns()
    {
        tblUsers user = new tblUsers();
        //user.LoadUsersForWorkShift(DepartmentID);

        commonMethods.FillDropDownList(ddlusers, user.DefaultView, "FullName", "pkUserID");

        tblSpeciality special = new tblSpeciality();
        special.LoadAll();
        commonMethods.FillDropDownList(ddlMondayPosition, special.DefaultView, "sSpeciality", "pkSpecialityID");
        commonMethods.FillDropDownList(ddlTuesdayPosition, special.DefaultView, "sSpeciality", "pkSpecialityID");
        commonMethods.FillDropDownList(ddlWednesdayPosition, special.DefaultView, "sSpeciality", "pkSpecialityID");
        commonMethods.FillDropDownList(ddlThursdayPosition, special.DefaultView, "sSpeciality", "pkSpecialityID");
        commonMethods.FillDropDownList(ddlFridayPosition, special.DefaultView, "sSpeciality", "pkSpecialityID");
        commonMethods.FillDropDownList(ddlSaturdayPosition, special.DefaultView, "sSpeciality", "pkSpecialityID");
        commonMethods.FillDropDownList(ddlSundayPosition, special.DefaultView, "sSpeciality", "pkSpecialityID");

    }
    protected void btnGO_Click(object sender, ImageClickEventArgs e)
    {
        DateTime mydate = Convert.ToDateTime(datepicker.Text);
        int selectedweek = commonMethods.GetWeeknumber(mydate);

        tblUserWorkshifts workshift = new tblUserWorkshifts();
        workshift.CkeckWorkShiftExist(selectedweek, Convert.ToInt32(ddlusers.SelectedValue));
        if (workshift.RowCount > 0)
        {
            LoadValues(workshift);
            workshift.Sort = "iDayNumber";
            
            LoadWorkShiftForm(mydate.Year, selectedweek);
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "Work Shift for week " + selectedweek + " does not exist.";
            return;
        }
       
        
    }
    private void LoadValues(tblUserWorkshifts uws)
    {
        //workshift.SStartTime = MondayStartHour.Text + ":" + MondayStartMin.Text;
        //workshift.SEndTime = MondayEndHour.Text + ":" + MondayEndMin.Text;

        string[] split = uws.SStartTime.Split(':');

        if (split.Length == 2)
        {
            MondayStartHour.Text = split[0];
            MondayStartMin.Text = split[1];
        }

        split = uws.SEndTime.Split(':');
        if (split.Length == 2)
        {
            MondayEndHour.Text = split[0];
            MondayEndMin.Text = split[1];
        }
        chkMondayOFF.Checked = uws.BIsOfDay;
        uws.MoveNext();

        //For Tuesday 

        split = uws.SStartTime.Split(':');

        if (split.Length == 2)
        {
            TuesdayStartHour.Text = split[0];
            TuesdayStartMin.Text = split[1];
        }

        split = uws.SEndTime.Split(':');
        if (split.Length == 2)
        {
            TuesdayEndHour.Text = split[0];
            TuesdayEndMin.Text = split[1];
        }
        chkTuesdayOFF.Checked = uws.BIsOfDay;
        uws.MoveNext();

        // For wednesday

        split = uws.SStartTime.Split(':');

        if (split.Length == 2)
        {
            WednesdayStartHour.Text = split[0];
            WednesdayStartMin.Text = split[1];
        }

        split = uws.SEndTime.Split(':');
        if (split.Length == 2)
        {
            WednesdayEndHour.Text = split[0];
            WednesdayEndMin.Text = split[1];
        }
        chkWednesdayOFF.Checked = uws.BIsOfDay;
        uws.MoveNext();

        //For Thursday

        split = uws.SStartTime.Split(':');

        if (split.Length == 2)
        {
            ThursdayStartHour.Text = split[0];
            ThursdayStartMin.Text = split[1];
        }

        split = uws.SEndTime.Split(':');
        if (split.Length == 2)
        {
            ThursdayEndHour.Text = split[0];
            ThursdayEndMin.Text = split[1];
        }
        chkThursdayOFF.Checked = uws.BIsOfDay;
        uws.MoveNext();

        // For Friday

        split = uws.SStartTime.Split(':');

        if (split.Length == 2)
        {
            FridayStartHour.Text = split[0];
            FridayStartMin.Text = split[1];
        }

        split = uws.SEndTime.Split(':');
        if (split.Length == 2)
        {
            FridayEndHour.Text = split[0];
            FridayEndMin.Text = split[1];
        }
        chkFridayOFF.Checked = uws.BIsOfDay;
        uws.MoveNext();

        // for Saturday

        split = uws.SStartTime.Split(':');

        if (split.Length == 2)
        {
            SaturdayStartHour.Text = split[0];
            SaturdayStartMin.Text = split[1];
        }

        split = uws.SEndTime.Split(':');
        if (split.Length == 2)
        {
            SaturdayEndHour.Text = split[0];
            SaturdayEndMin.Text = split[1];
        }
        chkSaturdayOFF.Checked = uws.BIsOfDay;
        uws.MoveNext();

        // for Sunday

        split = uws.SStartTime.Split(':');

        if (split.Length == 2)
        {
            SundayStartHour.Text = split[0];
            SundayStartMin.Text = split[1];
        }

        split = uws.SEndTime.Split(':');
        if (split.Length == 2)
        {
            SundayEndHour.Text = split[0];
            SundayEndMin.Text = split[1];
        }
        chkSundayOFF.Checked = uws.BIsOfDay;
        uws.MoveNext();

    }
    private void LoadWorkShiftForm(int year, int SelectedWeek)
    {
        ShowWorkshift.Style.Add("display", "block");

        DateTime startDate = commonMethods.GetWeekStartDate(year, SelectedWeek);

        lblMondayDate.Text = startDate.ToShortDateString();
        lblTuesdayDate.Text = startDate.AddDays(1).ToShortDateString();
        lblWednesdayDate.Text = startDate.AddDays(2).ToShortDateString();
        lblThursdayDate.Text = startDate.AddDays(3).ToShortDateString();
        lblFridayDate.Text = startDate.AddDays(4).ToShortDateString();
        lblSaturdayDate.Text = startDate.AddDays(5).ToShortDateString();
        lblSundayDate.Text = startDate.AddDays(6).ToShortDateString();
        lblWeek.Text = " Workshift for week " + SelectedWeek;

    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        DateTime mydate = Convert.ToDateTime(datepicker.Text);
        int selectedweek = commonMethods.GetWeeknumber(mydate);
        DateTime startDate = commonMethods.GetWeekStartDate(mydate.Year, selectedweek);

        tblUserWorkshifts workshiftOld = new tblUserWorkshifts();
        tblUserWorkshifts workshift = new tblUserWorkshifts();
        workshiftOld.CkeckWorkShiftExist(selectedweek, Convert.ToInt32(ddlusers.SelectedValue));
        if (workshiftOld.RowCount > 0)
        {



            TransactionMgr tx = TransactionMgr.ThreadTransactionMgr();
            try
            {
                tx.BeginTransaction();
                workshift.FlushData();

                workshift.LoadByPrimaryKey(workshiftOld.PkUserWorkshiftID);
                //workshift.FkUserID = Convert.ToInt32(ddlusers.SelectedValue);
                //workshift.s_FkSpecialityID = ddlMondayPosition.SelectedValue;
                //workshift.IWeekNumber = selectedweek;
               // workshift.DWeekStartDate = startDate;
                //workshift.DWeekEndDate = startDate.AddDays(6);
                //workshift.IDayNumber = 1;
                workshift.SStartTime = MondayStartHour.Text + ":" + MondayStartMin.Text;
                workshift.SEndTime = MondayEndHour.Text + ":" + MondayEndMin.Text;
                if (chkMondayOFF.Checked)
                {
                    workshift.BIsOfDay = true;
                }
                else
                {
                    workshift.BIsOfDay = false;
                }
                //workshift.DCreateDate = DateTime.Now;
                workshift.DModifiedDate = DateTime.Now;
                workshift.Save();

                //for tuesday
                workshiftOld.MoveNext();
                workshift.FlushData();

                workshift.LoadByPrimaryKey(workshiftOld.PkUserWorkshiftID);
                //workshift.FkUserID = Convert.ToInt32(ddlusers.SelectedValue);
                //workshift.s_FkSpecialityID = ddlTuesdayPosition.SelectedValue;
                //workshift.IWeekNumber = selectedweek;
                ////workshift.DWeekStartDate = startDate;
                workshift.DWeekEndDate = startDate.AddDays(6);
                //workshift.IDayNumber = 2;
                workshift.SStartTime = TuesdayStartHour.Text + ":" + TuesdayStartMin.Text;
                workshift.SEndTime = TuesdayEndHour.Text + ":" + TuesdayEndMin.Text;
                if (chkTuesdayOFF.Checked)
                {
                    workshift.BIsOfDay = true;
                }
                else
                {
                    workshift.BIsOfDay = false;
                }
                //workshift.DCreateDate = DateTime.Now;
                workshift.DModifiedDate = DateTime.Now;
                workshift.Save();

                // for wednesday

                workshiftOld.MoveNext();
                workshift.FlushData();

                workshift.LoadByPrimaryKey(workshiftOld.PkUserWorkshiftID);
                //workshift.FkUserID = Convert.ToInt32(ddlusers.SelectedValue);
                //workshift.s_FkSpecialityID = ddlWednesdayPosition.SelectedValue;
                //workshift.IWeekNumber = selectedweek;
                //workshift.DWeekStartDate = startDate;
                //workshift.DWeekEndDate = startDate.AddDays(6);
                //workshift.IDayNumber = 3;
                workshift.SStartTime = WednesdayStartHour.Text + ":" + WednesdayStartMin.Text;
                workshift.SEndTime = WednesdayEndHour.Text + ":" + WednesdayEndMin.Text;
                if (chkWednesdayOFF.Checked)
                {
                    workshift.BIsOfDay = true;
                }
                else
                {
                    workshift.BIsOfDay = false;
                }
                //workshift.DCreateDate = DateTime.Now;
                workshift.DModifiedDate = DateTime.Now;
                workshift.Save();

                //for thursday

                workshiftOld.MoveNext();
                workshift.FlushData();

                workshift.LoadByPrimaryKey(workshiftOld.PkUserWorkshiftID);
                //workshift.FkUserID = Convert.ToInt32(ddlusers.SelectedValue);
                //workshift.s_FkSpecialityID = ddlThursdayPosition.SelectedValue;
                //workshift.IWeekNumber = selectedweek;
                //workshift.DWeekStartDate = startDate;
                //workshift.DWeekEndDate = startDate.AddDays(6);
                //workshift.IDayNumber = 4;
                workshift.SStartTime = ThursdayStartHour.Text + ":" + ThursdayStartMin.Text;
                workshift.SEndTime = ThursdayEndHour.Text + ":" + ThursdayEndMin.Text;
                if (chkThursdayOFF.Checked)
                {
                    workshift.BIsOfDay = true;
                }
                else
                {
                    workshift.BIsOfDay = false;
                }
                //workshift.DCreateDate = DateTime.Now;
                workshift.DModifiedDate = DateTime.Now;
                workshift.Save();

                // for friday

                workshiftOld.MoveNext();
                workshift.FlushData();

                workshift.LoadByPrimaryKey(workshiftOld.PkUserWorkshiftID);
                //workshift.FkUserID = Convert.ToInt32(ddlusers.SelectedValue);
                //workshift.s_FkSpecialityID = ddlFridayPosition.SelectedValue;
                //workshift.IWeekNumber = selectedweek;
                //workshift.DWeekStartDate = startDate;
                //workshift.DWeekEndDate = startDate.AddDays(6);
                //workshift.IDayNumber = 5;
                workshift.SStartTime = FridayStartHour.Text + ":" + FridayStartMin.Text;
                workshift.SEndTime = FridayEndHour.Text + ":" + FridayEndMin.Text;
                if (chkFridayOFF.Checked)
                {
                    workshift.BIsOfDay = true;
                }
                else
                {
                    workshift.BIsOfDay = false;
                }
                //workshift.DCreateDate = DateTime.Now;
                workshift.DModifiedDate = DateTime.Now;
                workshift.Save();

                //for saturday

                workshiftOld.MoveNext();
                workshift.FlushData();

                workshift.LoadByPrimaryKey(workshiftOld.PkUserWorkshiftID);
                //workshift.FkUserID = Convert.ToInt32(ddlusers.SelectedValue);
                //workshift.s_FkSpecialityID = ddlSaturdayPosition.SelectedValue;
                //workshift.IWeekNumber = selectedweek;
                //workshift.DWeekStartDate = startDate;
                //workshift.DWeekEndDate = startDate.AddDays(6);
                //workshift.IDayNumber = 6;
                workshift.SStartTime = SaturdayStartHour.Text + ":" + SaturdayStartMin.Text;
                workshift.SEndTime = SaturdayEndHour.Text + ":" + SaturdayEndMin.Text;
                if (chkSaturdayOFF.Checked)
                {
                    workshift.BIsOfDay = true;
                }
                else
                {
                    workshift.BIsOfDay = false;
                }
                //workshift.DCreateDate = DateTime.Now;
                workshift.DModifiedDate = DateTime.Now;
                workshift.Save();

                //for sunday

                workshiftOld.MoveNext();
                workshift.FlushData();

                workshift.LoadByPrimaryKey(workshiftOld.PkUserWorkshiftID);
                //workshift.FkUserID = Convert.ToInt32(ddlusers.SelectedValue);
                //workshift.s_FkSpecialityID = ddlSundayPosition.SelectedValue;
                //workshift.IWeekNumber = selectedweek;
                //workshift.DWeekStartDate = startDate;
                //workshift.DWeekEndDate = startDate.AddDays(6);
                //workshift.IDayNumber = 7;
                workshift.SStartTime = SundayStartHour.Text + ":" + SundayStartMin.Text;
                workshift.SEndTime = SundayEndHour.Text + ":" + SundayEndMin.Text;
                if (chkSundayOFF.Checked)
                {
                    workshift.BIsOfDay = true;
                }
                else
                {
                    workshift.BIsOfDay = false;
                }
                //workshift.DCreateDate = DateTime.Now;
                workshift.DModifiedDate = DateTime.Now;
                workshift.Save();
                tx.CommitTransaction();
                lblError.Visible = true;
                lblError.Text = " Work Shift for week " + selectedweek + " is saved";
                ShowWorkshift.Style.Add("display", "none");
            }
            catch (Exception ex)
            {
                tx.RollbackTransaction();
                TransactionMgr.ThreadTransactionMgrReset();
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "Work Shift for week " + selectedweek + " does not exist.";
            return;
        }


    }
}
