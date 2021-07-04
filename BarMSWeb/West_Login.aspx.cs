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
public partial class West_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string str = Guid.NewGuid().ToString();
        lblError.Visible = false;
        //DateTime.Now.DayOfWeek != DayOfWeek.Monday
        //int week = commonMethods.GetWeekNumber_New(DateTime.Now);
        //DateTime startdate = commonMethods.GetWeekStartDate(DateTime.Now.Year, 46);
        //DateTime enddate = startdate.AddDays(6);
    }
    protected void btnOK_Click(object sender, ImageClickEventArgs e)
    {
        tblUsers user = new tblUsers();

        user.UserExist(txtUsername.Text);
        if (user.RowCount > 0)
        {
            if (user.s_SPassword == txtPassword.Text)
            {
                if (user.BActiveByAdmin && user.BActiveByUser)
                {
                    tblUserDepartment Udepart = new tblUserDepartment();
                    Udepart.LoadUserDepartment(user.PkUserID);
                    tblUserAccessLevel AcLevel = new tblUserAccessLevel();
                    AcLevel.LoadAccessLevel(user.PkUserID);
                    SessionUser sessionuser = new SessionUser();
                    sessionuser.UserID = user.PkUserID;
                    sessionuser.AccessLevel = Convert.ToInt32(AcLevel.GetColumn("AccessLevel"));
                    if (sessionuser.AccessLevel == 6)
                    {
                        sessionuser.DepartmentID = 0;
                    }
                    else
                    {
                        sessionuser.DepartmentID = Udepart.FkDepartmentID;
                    }
                    //sessionuser.DepartmentID = Udepart.FkDepartmentID;
                    Session["UserLogin"] = sessionuser;
                    if (sessionuser.AccessLevel == 1)
                    {
                        Response.Redirect("Users/EditAccount.aspx");
                    }
                    else if (sessionuser.AccessLevel == 2)
                    {
                        Response.Redirect("Managers/ManageUsers.aspx");
                    }
                    else if (sessionuser.AccessLevel == 6)
                    {
                        Response.Redirect("Admin/ManageDepartments.aspx");
                    }
                    else if (sessionuser.AccessLevel == 5)
                    {
                        Response.Redirect("DepartmentAdmin/BonusDoc.aspx");
                    }
                    else if (sessionuser.AccessLevel == 4)
                    {
                        Response.Redirect("AccountManager/WorkshiftsAttendance.aspx");
                    }
                    else if (sessionuser.AccessLevel == 3)
                    {
                        Response.Redirect("ECUser/DailyIncome.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Expired.aspx");
                }

            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Invalid Password";
            }

        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "Invalid Username";
        }


        //user.AuthenticateUser(txtUsername.Text, txtPassword.Text);
        //if (user.RowCount > 0)
        //{
        //    tblUserAccessLevel AcLevel = new tblUserAccessLevel();
        //    AcLevel.LoadAccessLevel(user.PkUserID);
        //    SessionUser sessionuser = new SessionUser();
        //    sessionuser.UserID = user.PkUserID;
        //    sessionuser.AccessLevel =Convert.ToInt32(AcLevel.GetColumn("AccessLevel"));
        //    Session["UserLogin"] = sessionuser;
        //    if (sessionuser.AccessLevel == 1)
        //    {
        //        Response.Redirect("Users/EditAccount.aspx");
        //    }
        //    else if (sessionuser.AccessLevel == 2)
        //    {
        //        Response.Redirect("Managers/ManagerHome.aspx");
        //    }
        //    else if (sessionuser.AccessLevel == 5)
        //    {
        //        Response.Redirect("Admin/ManageUsers.aspx");
        //    }
        //}
        //else
        //{
        //    Response.Redirect("Expired.aspx");
        //}
    }
}
