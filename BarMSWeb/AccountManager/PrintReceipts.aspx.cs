using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Enterprise;
using CrystalDecisions.ReportAppServer.ClientDoc;
using LC.Model.BMS.BLL;

public partial class AccountManager_PrintReceipts : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    int WeekNumber;
    int all;
    int usid;
    static double finalTotal = 0.0;
    DateTime startDate;
    DateTime endDate;
    static DSSalary DS = new DSSalary();
    int Year;
    static double total = 0.0;
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
        if ((Request.QueryString["startDate"] != null) && (Request.QueryString["startDate"] != ""))
        {
            startDate = Convert.ToDateTime(Request.QueryString["startDate"]);
        }
        else
        {
            startDate = DateTime.Now;
        }
        if ((Request.QueryString["endDate"] != null) && (Request.QueryString["endDate"] != ""))
        {
            endDate = Convert.ToDateTime(Request.QueryString["endDate"]);
        }
        else
        {
            endDate = DateTime.Now;
        }

        if ((Request.QueryString["all"] != null) && (Request.QueryString["all"] != ""))
        {
            all = Convert.ToInt32(Request.QueryString["all"]);
        }
        else
        {
            all = 0;
        }

        if ((Request.QueryString["usid"] != null) && (Request.QueryString["usid"] != ""))
        {
            usid = Convert.ToInt32(Request.QueryString["usid"]);
        }
        else
        {
            usid = 0;
        }
        if (!IsPostBack)
        {
            DS.Clear();
            BindReceipts();
        }
        //***
        //if (!IsPostBack)
        //{
        ReportDocument cryRpt = new ReportDocument();
        DataTable dataTable = DS.Tables[0];
        cryRpt.Load(Server.MapPath("../CrystalReports/SalaryReport2.rpt"));
        cryRpt.SetDataSource(dataTable);
        CRVRecipt.EnableDatabaseLogonPrompt = false;
        CRVRecipt.ReportSource = cryRpt;
        CRVRecipt.DataBind();
        // }
        //***
    }
    private void BindReceipts()
    {
        //if (all != 0)
        {
            tblUsers u = new tblUsers();
            u.GetAllSalaries(WeekNumber, DepartmentID, startDate, endDate);

            DataTable dt = new DataTable();
            dt = u.DefaultView.ToTable();

            #region Print all for manager
            u.FlushData();
            u.LoadManager_and_AccountManager(DepartmentID);

            foreach (DataRow drow in u.DefaultView.ToTable().Rows)
            {
                DataRow dr = dt.NewRow();
                dr["FullName"] = drow["FullName"];//u.s_SFirstName + " " + u.s_SLastName;
                dr["pkuserid"] = drow["pkuserid"];
                dr["uAdvance"] = 0;
                dr["dWeekStartDate"] = startDate.ToString();
                dr["dWeekEndDate"] = endDate.ToString();
                tblUserContract objContract = new tblUserContract();
                objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                if (objContract.RowCount > 0)
                {
                    switch (objContract.FkSalaryTypeID)
                    {
                        case 1:
                            objContract.FlushData();
                            dr["Sunday"] = objContract.GetUserSeasonSalary(startDate, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
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
                            dr["Monday"] = objContract.GetUserSeasonSalary(startDate, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
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
                            dr["Tuesday"] = objContract.GetUserSeasonSalary(startDate, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
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
                            dr["Wednesday"] = objContract.GetUserSeasonSalary(startDate, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
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
                            dr["Thursday"] = objContract.GetUserSeasonSalary(startDate, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
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
                            dr["Friday"] = objContract.GetUserSeasonSalary(startDate, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
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
                            dr["Saturday"] = objContract.GetUserSeasonSalary(startDate, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                            break;
                        case 2:
                            dr["Saturday"] = objContract.s_StandardSalary;
                            break;
                        case 3:
                            dr["Saturday"] = objContract.s_MinimumPerday;
                            break;
                    }
                }

                dr["SalaryStatus"] = "True";
                dr["permission"] = "True";

                tblManagerDayOff objManagerDayoff = new tblManagerDayOff();
                objManagerDayoff.getSingleDayOffforSalery(Convert.ToInt32(drow["pkuserid"]), startDate, endDate);
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
            #endregion


            #region print all ecuser
            u.FlushData();
            u.LoadECUser(DepartmentID);

            foreach (DataRow drow in u.DefaultView.ToTable().Rows)
            {
                tblECUserAssignments ua = new tblECUserAssignments();
                ua.CheckECUsersSaleryForWeek(Convert.ToInt32(drow["pkuserid"]), startDate, endDate);
                if (ua.RowCount > 0)
                {

                    DataRow dr = dt.NewRow();
                    dr["FullName"] = drow["FullName"];
                    dr["pkuserid"] = drow["pkuserid"];
                    dr["uAdvance"] = 0;
                    dr["dWeekStartDate"] = startDate.ToString();
                    dr["dWeekEndDate"] = endDate.ToString();
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
                        ua.CheckECUsersSalery(startDate.AddDays(j), Convert.ToInt32(drow["pkuserid"]));
                        if (ua.RowCount > 0)
                        {
                            string WeekDay = startDate.AddDays(j).DayOfWeek.ToString();
                            switch (WeekDay)
                            {
                                case "Sunday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                    if (objContract.RowCount > 0)
                                    {
                                        switch (objContract.FkSalaryTypeID)
                                        {
                                            case 1:
                                                objContract.FlushData();
                                                dr["Sunday"] = objContract.GetUserSeasonSalary(startDate, Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                                break;
                                            case 2:
                                                dr["Sunday"] = objContract.s_StandardSalary;
                                                break;
                                            case 3:
                                                dr["Sunday"] = objContract.s_MinimumPerday;
                                                break;
                                        }
                                    }
                                    break;
                                case "Monday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                    if (objContract.RowCount > 0)
                                    {
                                        switch (objContract.FkSalaryTypeID)
                                        {
                                            case 1:
                                                objContract.FlushData();
                                                dr["Monday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                                break;
                                            case 2:
                                                dr["Monday"] = objContract.s_StandardSalary;
                                                break;
                                            case 3:
                                                dr["Monday"] = objContract.s_MinimumPerday;
                                                break;
                                        }
                                    }
                                    break;
                                case "Tuesday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                    if (objContract.RowCount > 0)
                                    {
                                        switch (objContract.FkSalaryTypeID)
                                        {
                                            case 1:
                                                objContract.FlushData();
                                                dr["Tuesday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                                break;
                                            case 2:
                                                dr["Tuesday"] = objContract.s_StandardSalary;
                                                break;
                                            case 3:
                                                dr["Tuesday"] = objContract.s_MinimumPerday;
                                                break;
                                        }
                                    }
                                    break;
                                case "Wednesday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                    if (objContract.RowCount > 0)
                                    {
                                        switch (objContract.FkSalaryTypeID)
                                        {
                                            case 1:
                                                objContract.FlushData();
                                                dr["Wednesday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                                break;
                                            case 2:
                                                dr["Wednesday"] = objContract.s_StandardSalary;
                                                break;
                                            case 3:
                                                dr["Wednesday"] = objContract.s_MinimumPerday;
                                                break;
                                        }
                                    }
                                    break;
                                case "Thursday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                    if (objContract.RowCount > 0)
                                    {
                                        switch (objContract.FkSalaryTypeID)
                                        {
                                            case 1:
                                                objContract.FlushData();
                                                dr["Thursday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                                break;
                                            case 2:
                                                dr["Thursday"] = objContract.s_StandardSalary;
                                                break;
                                            case 3:
                                                dr["Thursday"] = objContract.s_MinimumPerday;
                                                break;
                                        }
                                    }
                                    break;
                                case "Friday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                    if (objContract.RowCount > 0)
                                    {
                                        switch (objContract.FkSalaryTypeID)
                                        {
                                            case 1:
                                                objContract.FlushData();
                                                dr["Friday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                                break;
                                            case 2:
                                                dr["Friday"] = objContract.s_StandardSalary;
                                                break;
                                            case 3:
                                                dr["Friday"] = objContract.s_MinimumPerday;
                                                break;
                                        }
                                    }
                                    break;
                                case "Saturday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
                                    if (objContract.RowCount > 0)
                                    {
                                        switch (objContract.FkSalaryTypeID)
                                        {
                                            case 1:
                                                objContract.FlushData();
                                                dr["Saturday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), Convert.ToInt32(drow["pkuserid"]), DepartmentID);
                                                break;
                                            case 2:
                                                dr["Saturday"] = objContract.s_StandardSalary;
                                                break;
                                            case 3:
                                                dr["Saturday"] = objContract.s_MinimumPerday;
                                                break;
                                        }
                                    }
                                    break;
                            }
                        }





                    }
                    dt.Rows.Add(dr);
                }
            }

            #endregion


            //DSSalary DS = new DSSalary();
            //DS.Tables[0].Merge(dt);

            //ReportDocument cryRpt = new ReportDocument();
            //cryRpt.Load(Server.MapPath("../CrystalReports/SaleryRecipt.rpt"));
            //cryRpt.SetDataSource(DS);

            //CRVRecipt.ReportSource = cryRpt; 
            grdReceipts.DataSource = dt.DefaultView;
            grdReceipts.DataBind();

            //ReportDocument cryRpt = new ReportDocument();
            //DataTable dataTable = DS.Tables[0];
            //cryRpt.Load(Server.MapPath("../CrystalReports/SaleryRecipt.rpt"));
            //cryRpt.SetDataSource(dataTable);
            //CRVRecipt.EnableDatabaseLogonPrompt = false;
            //CRVRecipt.ReportSource = cryRpt;
            //CRVRecipt.DataBind();

            //ReportDocument rpt = new ReportDocument();
            //rpt.Load(Request.PhysicalApplicationPath + @"\CrystalReports\SaleryRecipt.rpt");
            //rpt.SetDataSource(dataTable);
            //CRVRecipt.DisplayGroupTree = false;
            //CRVRecipt.ReportSource = rpt;
            //CRVRecipt.DataBind();
        }
        //else if (all == 0)
        //{
        //    tblUsers u = new tblUsers();
        //    u.GetUser(usid);
        //    grdReceipts.DataSource = u.DefaultView;
        //    grdReceipts.DataBind();
        //}
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "printWorkshift", "window.print();", true);

    }
    protected void grdReceipts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTitle = e.Row.FindControl("lblTitle") as Label;
            Label lblTotal = e.Row.FindControl("lblTotal") as Label;
            HiddenField hidUser = e.Row.FindControl("hidUser") as HiddenField;

            lblTitle.Text = "Payment for week #" + WeekNumber + "/" + Year + " (Sunday " + startDate.ToString("dd/MM/yyyy") + " till Saturday " + endDate.ToString("dd/MM/yyyy") + ")";

            GridView grdPaymentForSingleUser = e.Row.FindControl("grdPaymentForSingleUser") as GridView;

            tblUsers u = new tblUsers();
            u.GetPaymentForSingle(Convert.ToInt32(hidUser.Value), WeekNumber, DepartmentID, startDate, endDate);
            DataTable dt = new DataTable();
            dt = u.DefaultView.ToTable();
            if (u.RowCount > 0)
            {
                if (all == 0)
                {
                    if (usid == Convert.ToInt32(hidUser.Value))
                    {
                        int iCount = 0;
                        DataTable DT = new DataTable();
                        DT = u.DefaultView.ToTable();
                        DT.Columns.Add("WeekNumber");

                        foreach (DataRow dr in DT.Rows)
                        {
                            DT.Rows[iCount]["WeekNumber"] = WeekNumber;
                            DT.Rows[iCount]["dWeekStartDate"] = startDate;
                            DT.Rows[iCount]["dWeekEndDate"] = endDate;
                            iCount++;
                        }

                        grdPaymentForSingleUser.DataSource = u.DefaultView;
                        grdPaymentForSingleUser.DataBind();
                        DS.Tables[0].Merge(DT);
                    }
                }
                else
                {
                    int iCount = 0;
                    DataTable DT = new DataTable();
                    DT = u.DefaultView.ToTable();
                    DT.Columns.Add("WeekNumber");

                    foreach (DataRow dr in DT.Rows)
                    {
                        DT.Rows[iCount]["WeekNumber"] = WeekNumber;
                        DT.Rows[iCount]["dWeekStartDate"] = startDate;
                        DT.Rows[iCount]["dWeekEndDate"] = endDate;
                        iCount++;
                    }

                    grdPaymentForSingleUser.DataSource = u.DefaultView;
                    grdPaymentForSingleUser.DataBind();
                    DS.Tables[0].Merge(DT);
                }
            }
            else
            {
                #region salary for EC User

                //u.FlushData();
                //u.LoadByPrimaryKey(Convert.ToInt32(hidUser.Value));


                ////u.FlushData();
                ////u.LoadECUser(DepartmentID);

                ////foreach (DataRow drow in u.DefaultView.ToTable().Rows)
                ////{
                //tblECUserAssignments ua = new tblECUserAssignments();

                //ua.CheckECUsersSaleryForWeek(Convert.ToInt32(hidUser.Value), startDate, endDate);
                //if (ua.RowCount > 0)
                //{

                //    DataRow dr = dt.NewRow();
                //    dr["FullName"] = u.s_SFirstName + " " + u.s_SLastName;
                //    dr["pkuserid"] = u.PkUserID;
                //    dr["uAdvance"] = 0;
                //    dr["dWeekStartDate"] = startDate.ToString();
                //    dr["dWeekEndDate"] = endDate.ToString();
                //    tblUserContract objContract = new tblUserContract();

                //    dr["Sunday"] = 0;

                //    dr["Monday"] = 0;
                //    dr["Tuesday"] = 0;
                //    dr["Wednesday"] = 0;
                //    dr["Thursday"] = 0;
                //    dr["Friday"] = 0;
                //    dr["Saturday"] = 0;

                //    dr["SalaryStatus"] = "True";



                //    for (int j = 0; j < 7; j++)
                //    {
                //        ua.FlushData();
                //        ua.CheckECUsersSalery(startDate.AddDays(j), u.PkUserID);
                //        if (ua.RowCount > 0)
                //        {
                //            string WeekDay = startDate.AddDays(j).DayOfWeek.ToString();
                //            switch (WeekDay)
                //            {
                //                case "Sunday":
                //                    objContract.FlushData();
                //                    objContract.GetAgreedContract(u.PkUserID);
                //                    switch (objContract.FkSalaryTypeID)
                //                    {
                //                        case 1:
                //                            objContract.FlushData();
                //                            dr["Sunday"] = objContract.GetUserSeasonSalary(startDate, u.PkUserID, DepartmentID);
                //                            break;
                //                        case 2:
                //                            dr["Sunday"] = objContract.s_StandardSalary;
                //                            break;
                //                        case 3:
                //                            dr["Sunday"] = objContract.s_MinimumPerday;
                //                            break;
                //                    }

                //                    break;
                //                case "Monday":
                //                    objContract.FlushData();
                //                    objContract.GetAgreedContract(u.PkUserID);

                //                    switch (objContract.FkSalaryTypeID)
                //                    {
                //                        case 1:
                //                            objContract.FlushData();
                //                            dr["Monday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), u.PkUserID, DepartmentID);
                //                            break;
                //                        case 2:
                //                            dr["Monday"] = objContract.s_StandardSalary;
                //                            break;
                //                        case 3:
                //                            dr["Monday"] = objContract.s_MinimumPerday;
                //                            break;
                //                    }
                //                    break;
                //                case "Tuesday":
                //                    objContract.FlushData();
                //                    objContract.GetAgreedContract(u.PkUserID);
                //                    switch (objContract.FkSalaryTypeID)
                //                    {
                //                        case 1:
                //                            objContract.FlushData();
                //                            dr["Tuesday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), u.PkUserID, DepartmentID);
                //                            break;
                //                        case 2:
                //                            dr["Tuesday"] = objContract.s_StandardSalary;
                //                            break;
                //                        case 3:
                //                            dr["Tuesday"] = objContract.s_MinimumPerday;
                //                            break;
                //                    }
                //                    break;
                //                case "Wednesday":
                //                    objContract.FlushData();
                //                    objContract.GetAgreedContract(u.PkUserID);
                //                    switch (objContract.FkSalaryTypeID)
                //                    {
                //                        case 1:
                //                            objContract.FlushData();
                //                            dr["Wednesday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), u.PkUserID, DepartmentID);
                //                            break;
                //                        case 2:
                //                            dr["Wednesday"] = objContract.s_StandardSalary;
                //                            break;
                //                        case 3:
                //                            dr["Wednesday"] = objContract.s_MinimumPerday;
                //                            break;
                //                    }
                //                    break;
                //                case "Thursday":
                //                    objContract.FlushData();
                //                    objContract.GetAgreedContract(u.PkUserID);
                //                    switch (objContract.FkSalaryTypeID)
                //                    {
                //                        case 1:
                //                            objContract.FlushData();
                //                            dr["Thursday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), u.PkUserID, DepartmentID);
                //                            break;
                //                        case 2:
                //                            dr["Thursday"] = objContract.s_StandardSalary;
                //                            break;
                //                        case 3:
                //                            dr["Thursday"] = objContract.s_MinimumPerday;
                //                            break;
                //                    }
                //                    break;
                //                case "Friday":
                //                    objContract.FlushData();
                //                    objContract.GetAgreedContract(u.PkUserID);
                //                    switch (objContract.FkSalaryTypeID)
                //                    {
                //                        case 1:
                //                            objContract.FlushData();
                //                            dr["Friday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), u.PkUserID, DepartmentID);
                //                            break;
                //                        case 2:
                //                            dr["Friday"] = objContract.s_StandardSalary;
                //                            break;
                //                        case 3:
                //                            dr["Friday"] = objContract.s_MinimumPerday;
                //                            break;
                //                    }
                //                    break;
                //                case "Saturday":
                //                    objContract.FlushData();
                //                    objContract.GetAgreedContract(u.PkUserID);
                //                    switch (objContract.FkSalaryTypeID)
                //                    {
                //                        case 1:
                //                            objContract.FlushData();
                //                            dr["Saturday"] = objContract.GetUserSeasonSalary(startDate.AddDays(1), u.PkUserID, DepartmentID);
                //                            break;
                //                        case 2:
                //                            dr["Saturday"] = objContract.s_StandardSalary;
                //                            break;
                //                        case 3:
                //                            dr["Saturday"] = objContract.s_MinimumPerday;
                //                            break;
                //                    }
                //                    break;
                //            }
                //        }
                //    }
                //    dt.Rows.Add(dr);
                //}
                //// }
                ////*********************************

                #endregion

                #region Print all for manager

                //u.FlushData();
                //u.LoadByPrimaryKey(Convert.ToInt32(hidUser.Value));
                ////u.LoadManager_and_AccountManager(DepartmentID);

                ////foreach (DataRow drow in u.DefaultView.ToTable().Rows)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr["FullName"] =  u.s_SFirstName + " " + u.s_SLastName;
                //    dr["pkuserid"] = u.PkUserID;
                //    dr["uAdvance"] = 0;
                //    dr["dWeekStartDate"] = startDate.ToString();
                //    dr["dWeekEndDate"] = endDate.ToString();
                //    tblUserContract objContract = new tblUserContract();
                //    objContract.GetAgreedContract(Convert.ToInt32(hidUser.Value));

                //    switch (objContract.FkSalaryTypeID)
                //    {
                //        case 1:
                //            objContract.FlushData();
                //            dr["Sunday"] = objContract.GetUserSeasonSalary(startDate, u.PkUserID, DepartmentID);
                //            break;
                //        case 2:

                //            dr["Sunday"] = objContract.s_StandardSalary;
                //            break;
                //        case 3:
                //            dr["Sunday"] = objContract.s_MinimumPerday;
                //            break;
                //    }


                //    switch (objContract.FkSalaryTypeID)
                //    {
                //        case 1:
                //            objContract.FlushData();
                //            dr["Monday"] = objContract.GetUserSeasonSalary(startDate, u.PkUserID, DepartmentID);
                //            break;
                //        case 2:
                //            dr["Monday"] = objContract.s_StandardSalary;
                //            break;
                //        case 3:
                //            dr["Monday"] = objContract.s_MinimumPerday;
                //            break;
                //    }

                //    switch (objContract.FkSalaryTypeID)
                //    {
                //        case 1:
                //            objContract.FlushData();
                //            dr["Tuesday"] = objContract.GetUserSeasonSalary(startDate, u.PkUserID, DepartmentID);
                //            break;
                //        case 2:
                //            dr["Tuesday"] = objContract.s_StandardSalary;
                //            break;
                //        case 3:
                //            dr["Tuesday"] = objContract.s_MinimumPerday;
                //            break;
                //    }


                //    switch (objContract.FkSalaryTypeID)
                //    {
                //        case 1:
                //            objContract.FlushData();
                //            dr["Wednesday"] = objContract.GetUserSeasonSalary(startDate, u.PkUserID, DepartmentID);
                //            break;
                //        case 2:
                //            dr["Wednesday"] = objContract.s_StandardSalary;
                //            break;
                //        case 3:
                //            dr["Wednesday"] = objContract.s_MinimumPerday;
                //            break;
                //    }


                //    switch (objContract.FkSalaryTypeID)
                //    {
                //        case 1:
                //            objContract.FlushData();
                //            dr["Thursday"] = objContract.GetUserSeasonSalary(startDate, u.PkUserID, DepartmentID);
                //            break;
                //        case 2:
                //            dr["Thursday"] = objContract.s_StandardSalary;
                //            break;
                //        case 3:
                //            dr["Thursday"] = objContract.s_MinimumPerday;
                //            break;
                //    }


                //    switch (objContract.FkSalaryTypeID)
                //    {
                //        case 1:
                //            objContract.FlushData();
                //            dr["Friday"] = objContract.GetUserSeasonSalary(startDate, u.PkUserID, DepartmentID);
                //            break;
                //        case 2:
                //            dr["Friday"] = objContract.s_StandardSalary;
                //            break;
                //        case 3:
                //            dr["Friday"] = objContract.s_MinimumPerday;
                //            break;
                //    }

                //    switch (objContract.FkSalaryTypeID)
                //    {
                //        case 1:
                //            objContract.FlushData();
                //            dr["Saturday"] = objContract.GetUserSeasonSalary(startDate, u.PkUserID, DepartmentID);
                //            break;
                //        case 2:
                //            dr["Saturday"] = objContract.s_StandardSalary;
                //            break;
                //        case 3:
                //            dr["Saturday"] = objContract.s_MinimumPerday;
                //            break;
                //    }


                //    dr["SalaryStatus"] = "True";


                //    tblManagerDayOff objManagerDayoff = new tblManagerDayOff();
                //    objManagerDayoff.getSingleDayOffforSalery(u.PkUserID, startDate, endDate);
                //    if (objManagerDayoff.RowCount > 0)
                //    {
                //        for (int i = 0; i < objManagerDayoff.RowCount; i++)
                //        {
                //            string WeekDay = Convert.ToDateTime(objManagerDayoff.s_MSingleDate).DayOfWeek.ToString();
                //            switch (WeekDay)
                //            {
                //                case "Sunday":
                //                    dr["Sunday"] = 0;
                //                    break;
                //                case "Monday":
                //                    dr["Monday"] = 0;
                //                    break;
                //                case "Tuesday":
                //                    dr["Tuesday"] = 0;
                //                    break;
                //                case "Wednesday":
                //                    dr["Wednesday"] = 0;
                //                    break;
                //                case "Thursday":
                //                    dr["Thursday"] = 0;
                //                    break;
                //                case "Friday":
                //                    dr["Friday"] = 0;
                //                    break;
                //                case "Saturday":
                //                    dr["Saturday"] = 0;
                //                    break;
                //            }
                //            objManagerDayoff.MoveNext();
                //        }

                //    }
                //    dt.Rows.Add(dr);

                //}
                #endregion


                DataRow drv = ((DataRowView)e.Row.DataItem).Row;

                int iUserID = Convert.ToInt32(drv["pkuserid"]);

                if (all == 0)
                {
                    if (usid == iUserID)
                    {


                        DataTable dtNew = new DataTable();
                        dtNew.Columns.Add("FullName");
                        dtNew.Columns.Add("pkuserid", typeof(int));
                        dtNew.Columns.Add("uAdvance", typeof(Double));
                        dtNew.Columns.Add("dWeekStartDate", typeof(DateTime));
                        dtNew.Columns.Add("dWeekEndDate", typeof(DateTime));
                        dtNew.Columns.Add("Sunday", typeof(Double));
                        dtNew.Columns.Add("Monday", typeof(Double));
                        dtNew.Columns.Add("Tuesday", typeof(Double));
                        dtNew.Columns.Add("Wednesday", typeof(Double));
                        dtNew.Columns.Add("Thursday", typeof(Double));
                        dtNew.Columns.Add("Friday", typeof(Double));
                        dtNew.Columns.Add("Saturday", typeof(Double));
                        dtNew.Columns.Add("SalaryStatus", typeof(Boolean));
                        dtNew.Columns.Add("permission", typeof(Boolean));
                        dtNew.Columns.Add("Type");
                        dtNew.Columns.Add("WeekNumber");

                        DataRow drNew = dtNew.NewRow();

                        drNew["FullName"] = drv["FullName"];
                        drNew["pkuserid"] = drv["pkuserid"];
                        drNew["uAdvance"] = drv["uAdvance"];
                        drNew["dWeekStartDate"] = startDate;
                        drNew["dWeekEndDate"] = endDate;
                        drNew["Sunday"] = drv["Sunday"];
                        drNew["Monday"] = drv["Monday"];
                        drNew["Tuesday"] = drv["Tuesday"];
                        drNew["Wednesday"] = drv["Wednesday"];
                        drNew["Thursday"] = drv["Thursday"];
                        drNew["Friday"] = drv["Friday"];
                        drNew["Saturday"] = drv["Saturday"];
                        drNew["SalaryStatus"] = drv["SalaryStatus"];
                        drNew["permission"] = drv["permission"];
                        drNew["Type"] = "Salary";
                        drNew["WeekNumber"] = WeekNumber;

                        dtNew.Rows.Add(drNew);
                        grdPaymentForSingleUser.DataSource = dtNew.DefaultView;
                        grdPaymentForSingleUser.DataBind();
                        DS.Tables[0].Merge(dtNew);
                    }
                }
                else
                {
                    DataTable dtNew = new DataTable();
                    dtNew.Columns.Add("FullName");
                    dtNew.Columns.Add("pkuserid", typeof(int));
                    dtNew.Columns.Add("uAdvance", typeof(Double));
                    dtNew.Columns.Add("dWeekStartDate", typeof(DateTime));
                    dtNew.Columns.Add("dWeekEndDate", typeof(DateTime));
                    dtNew.Columns.Add("Sunday", typeof(Double));
                    dtNew.Columns.Add("Monday", typeof(Double));
                    dtNew.Columns.Add("Tuesday", typeof(Double));
                    dtNew.Columns.Add("Wednesday", typeof(Double));
                    dtNew.Columns.Add("Thursday", typeof(Double));
                    dtNew.Columns.Add("Friday", typeof(Double));
                    dtNew.Columns.Add("Saturday", typeof(Double));
                    dtNew.Columns.Add("SalaryStatus", typeof(Boolean));
                    dtNew.Columns.Add("permission", typeof(Boolean));
                    dtNew.Columns.Add("Type");
                    dtNew.Columns.Add("WeekNumber");

                    DataRow drNew = dtNew.NewRow();

                    drNew["FullName"] = drv["FullName"];
                    drNew["pkuserid"] = drv["pkuserid"];
                    drNew["uAdvance"] = drv["uAdvance"];
                    drNew["dWeekStartDate"] = startDate;
                    drNew["dWeekEndDate"] = endDate;
                    drNew["Sunday"] = drv["Sunday"];
                    drNew["Monday"] = drv["Monday"];
                    drNew["Tuesday"] = drv["Tuesday"];
                    drNew["Wednesday"] = drv["Wednesday"];
                    drNew["Thursday"] = drv["Thursday"];
                    drNew["Friday"] = drv["Friday"];
                    drNew["Saturday"] = drv["Saturday"];
                    drNew["SalaryStatus"] = drv["SalaryStatus"];
                    drNew["permission"] = drv["permission"];
                    drNew["Type"] = "Salary";
                    drNew["WeekNumber"] = WeekNumber;

                    dtNew.Rows.Add(drNew);
                    grdPaymentForSingleUser.DataSource = dtNew.DefaultView;
                    grdPaymentForSingleUser.DataBind();
                    DS.Tables[0].Merge(dtNew);
                }

            }

            finalTotal = 0.0;
            finalTotal += total;
            lblTotal.Text = finalTotal.ToString("N") + " €";
            finalTotal = 0.0;
            total = 0.0;
        }
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

            }
            catch (Exception ex)
            { }
        }
    }
}
