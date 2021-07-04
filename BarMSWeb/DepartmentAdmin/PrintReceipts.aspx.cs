using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LC.Model.BMS.BLL;

public partial class DepartmentAdmin_PrintReceipts : System.Web.UI.Page
{
    int UserID;
    int DepartmentID;
    int WeekNumber;
    int all;
    int usid;
    static double finalTotal = 0.0;
    DateTime startDate;
    DateTime endDate;
    int Year;
    static double total = 0.0;
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
            BindReceipts();
        }

    }


    private void BindReceipts()
    {
        if (all != 0)
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
                dr["FullName"] = drow["FullName"];// u.s_SFirstName + " " + u.s_SLastName;
                dr["pkuserid"] = drow["pkuserid"];
                dr["uAdvance"] = 0;
                dr["dWeekStartDate"] = startDate.ToString();
                dr["dWeekEndDate"] = endDate.ToString();
                tblUserContract objContract = new tblUserContract();
                objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));

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

                                    break;
                                case "Monday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));

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
                                    break;
                                case "Tuesday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
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
                                    break;
                                case "Wednesday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
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
                                    break;
                                case "Thursday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
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
                                    break;
                                case "Friday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
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
                                    break;
                                case "Saturday":
                                    objContract.FlushData();
                                    objContract.GetAgreedContract(Convert.ToInt32(drow["pkuserid"]));
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
                                    break;
                            }
                        }





                    }
                    dt.Rows.Add(dr);
                }
            }

            #endregion


            grdReceipts.DataSource = dt.DefaultView;
            grdReceipts.DataBind();
        }
        else if (all == 0)
        {
            tblUsers u = new tblUsers();
            u.GetUser(usid);
            grdReceipts.DataSource = u.DefaultView;
            grdReceipts.DataBind();
        }
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "printWorkshift", "window.print();", true);

    }

    //private void BindReceipts()
    //{
    //    if (all != 0)
    //    {
    //        tblUsers u = new tblUsers();
    //        u.GetAllSalaries(WeekNumber, DepartmentID, startDate, endDate);
    //        grdReceipts.DataSource = u.DefaultView;
    //        grdReceipts.DataBind();
    //    }
    //    else if (all == 0)
    //    {
    //        tblUsers u = new tblUsers();
    //        u.GetUser(usid);
    //        grdReceipts.DataSource = u.DefaultView;
    //        grdReceipts.DataBind();
    //    }

    //}
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
                grdPaymentForSingleUser.DataSource = u.DefaultView;
                grdPaymentForSingleUser.DataBind();
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

                DataTable ddd = drv.Table;
                DataTable dtNew = new DataTable();

                dtNew.Columns.Add("Type");
                dtNew.Columns.Add("Sunday");
                dtNew.Columns.Add("Monday");
                dtNew.Columns.Add("Tuesday");
                dtNew.Columns.Add("Wednesday");
                dtNew.Columns.Add("Thursday");
                dtNew.Columns.Add("Friday");
                dtNew.Columns.Add("Saturday");

                dtNew.Columns.Add("FullName");
                dtNew.Columns.Add("pkuserid");
                dtNew.Columns.Add("uAdvance");
                dtNew.Columns.Add("dWeekStartDate");
                dtNew.Columns.Add("dWeekEndDate");
                DataRow drNew = dtNew.NewRow();
                drNew["Type"] = "Salary";

                drNew["Sunday"] = drv["Sunday"];
                drNew["Monday"] = drv["Monday"];
                drNew["Tuesday"] = drv["Tuesday"];
                drNew["Wednesday"] = drv["Wednesday"];
                drNew["Thursday"] = drv["Thursday"];
                drNew["Friday"] = drv["Friday"];
                drNew["Saturday"] = drv["Saturday"];


                dtNew.Rows.Add(drNew);

                grdPaymentForSingleUser.DataSource = dtNew.DefaultView;
                grdPaymentForSingleUser.DataBind();
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
