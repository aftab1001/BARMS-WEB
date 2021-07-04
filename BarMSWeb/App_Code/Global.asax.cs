using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.Mail;
using System.Web.Caching;
using System.Web.SessionState;
using System.IO;
using LC.Model.BMS.BLL;
//using System.Messaging;


/// <summary>
/// Summary description for Global.
/// </summary>
//namespace Global
//{
public class Global : System.Web.HttpApplication
{
    public static string global_Str = string.Empty;
    private const string DummyPageUrl = "http://localhost:49260/BarMSWeb/AutoEmail.aspx";

    //private const string DummyPageUrl = "http://localhost:1253/VardassistansWeb/Admin/admin_login.aspx";
    //private const string DummyPageUrl = "http://localhost/Vard/Admin/admin_login.aspx";

    private const string DummyCacheItemKey = "Babar_Bilal";

    private System.ComponentModel.IContainer components = null;

    public Global()
    {
        InitializeComponent();
    }

    protected void Application_Start(Object sender, EventArgs e)
    {
        RegisterCacheEntry();

    }

    protected void Session_Start(Object sender, EventArgs e)
    {
        int x = 0;

    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {
        // If the dummy page is hit, then it means we want to add another item in cache

        if (HttpContext.Current.Request.Url.ToString() == DummyPageUrl)
        {
            // Add the item in cache and when succesful, do the work.
            RegisterCacheEntry();
        }
    }

    protected void Application_EndRequest(Object sender, EventArgs e)
    {

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {

    }

    protected void Application_Error(Object sender, EventArgs e)
    {
        Debug.WriteLine(Server.GetLastError());
    }

    protected void Session_End(Object sender, EventArgs e)
    {
        //global_Str = null;
        //if (Session["Duration"] != null)
        //{
        //    string[] strDuration = new string[2];
        //    strDuration = (string[])Session["Duration"];
        //    LC.Modal.Vardassistans.BLL.tblDoctorLogins DoctorLogin = new LC.Modal.Vardassistans.BLL.tblDoctorLogins();
        //    DoctorLogin.LoadByPrimaryKey(Convert.ToInt32(strDuration[1]));
        //    DateTime EndTime = DateTime.Now;
        //    DateTime StartTime = Convert.ToDateTime(strDuration[0]);

        //    TimeSpan TS = EndTime - StartTime;
        //    int Totalmins = Convert.ToInt32(TS.TotalMinutes);
        //    DoctorLogin.Duration = Totalmins;
        //    DoctorLogin.Save();
        //}
        //if (Session["AdminDuration"] != null)
        //{
        //    string[] strAdminDuration = new string[2];
        //    strAdminDuration = (string[])Session["AdminDuration"];
        //    LC.Modal.Vardassistans.BLL.tblAdminLogins AdminLogin = new LC.Modal.Vardassistans.BLL.tblAdminLogins();
        //    AdminLogin.LoadByPrimaryKey(Convert.ToInt32(strAdminDuration[1]));
        //    DateTime EndTime = DateTime.Now;
        //    DateTime StartTime = Convert.ToDateTime(strAdminDuration[0]);

        //    TimeSpan TS = EndTime - StartTime;
        //    int Totalmins = Convert.ToInt32(TS.TotalMinutes);
        //    AdminLogin.Duration= Totalmins;
        //    AdminLogin.Save();
        //}


    }

    protected void Application_End(Object sender, EventArgs e)
    {
        global_Str = null;
    }

    #region Web Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }
    #endregion

    /// ***** CUSTOM METHODS *********************************************** 

    /// <summary>
    /// Register a cache entry which expires in 1 minute and gives us a callback.
    /// </summary>
    /// <returns></returns>

    private void RegisterCacheEntry()
    {
        // Prevent duplicate key addition
        if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return;

        //HttpContext.Current.Cache.Add(DummyCacheItemKey,"Test", null, System.Web.Caching.Cache.NoAbsoluteExpiration,
        //    new TimeSpan(23,50,10), CacheItemPriority.NotRemovable,
        //    new CacheItemRemovedCallback(CacheItemRemovedCallback));

        HttpContext.Current.Cache.Add(DummyCacheItemKey, "Test", null, System.Web.Caching.Cache.NoAbsoluteExpiration,
            new TimeSpan(1, 0, 0), CacheItemPriority.NotRemovable,
            new CacheItemRemovedCallback(CacheItemRemovedCallback));

    }


    public void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
    {
        //Debug.WriteLine("Cache item callback: " + DateTime.Now.ToString());

        //if (DateTime.Now.Hour == 1 || DateTime.Now.Hour > 1)
        //{
        
        //}


        // Do the service works
        int hour = DateTime.Now.Hour;
        int min = DateTime.Now.Minute;
        if (hour == 23 && min > 55)
        {
            DayOffEmail();
            SalaryMail();
        }


        // We need to register another cache item which will expire again in one
        // minute. However, as this callback occurs without any HttpContext, we do not
        // have access to HttpContext and thus cannot access the Cache object. The
        // only way we can access HttpContext is when a request is being processed which
        // means a webpage is hit. So, we need to simulate a web page hit and then 
        // add the cache item.
        HitPage();
    }
    private void DayOffEmail()
    {
        tblManagerDayOff dayoff = new tblManagerDayOff();
        dayoff.LoadAll();
        if (dayoff.RowCount > 0)
        {
            for (int i = 0; i < dayoff.RowCount; i++)
            {
                string manageName = string.Empty;
                string emailAddress = string.Empty;

                tblUsers u = new tblUsers();
                u.LoadByPrimaryKey(dayoff.Fkuserid);
                if (u.RowCount > 0)
                {
                    manageName = u.SFirstName + " " + u.SLastName;
                }
                u.FlushData();
                tblUserDepartment userDepartment = new tblUserDepartment();
                userDepartment.LoadUserDepartment(dayoff.Fkuserid);
                if (userDepartment.RowCount > 0)
                    u.GetDepartmentAdminID(userDepartment.FkDepartmentID);
                else
                    u.GetDepartmentAdminID(1);

                tblUserEmails ue = new tblUserEmails();
                ue.LoadUserEmailsActive(Convert.ToInt32(u.GetColumn("fkuserid").ToString()));
                if (ue.RowCount > 0)
                {
                    emailAddress = ue.s_SEmail.ToString();
                }


                if (dayoff.MSingleDate != null)
                {
                    TimeSpan s = dayoff.MSingleDate.Subtract(DateTime.Now.Date);
                    if (s.Days == 0)
                    {
                        Emailing email = new Emailing();
                        email.P_ToAddress = emailAddress;
                        email.P_FromAddress = "noreply@west.com";
                        email.P_Email_Subject = "system notification";
                        email.P_Message_Body = "This is a system reminder that the manager ”" + manageName + "” is having a programmed day off tonight!";
                        email.Send_Email();
                    }
                    else if (s.Days == 1)
                    {
                        Emailing email = new Emailing();
                        email.P_ToAddress = emailAddress;
                        email.P_FromAddress = "noreply@west.com";
                        email.P_Email_Subject = "system notification";
                        email.P_Message_Body = "This is a system reminder that the manager ”" + manageName + "” is having a programmed day off tomorrow!";
                        email.Send_Email();
                    }

                }
                
                dayoff.MoveNext();
            }
        }

    }
    private void SalaryMail()
    {
        tblDepartments departments = new tblDepartments();
        departments.LoadAll();
        DateTime SalaryDate = Convert.ToDateTime("11/20/2011 12:00:00 AM");
        if (departments.RowCount > 0)
        {
            for (int dp = 0; dp < departments.RowCount; dp++)
            {
                tblUsers u = new tblUsers();
                u.GetSalariedUsers(departments.PkDepartmentID);
                if (u.RowCount > 0)
                {
                    for (int i = 0; i < u.RowCount; i++)
                    {
                        try
                        {
                            TimeSpan ts = DateTime.Now.Subtract(Convert.ToDateTime(u.GetColumn("emailDate").ToString()));
                            if (ts.Days > 3)
                            {
                                string weekDate = "week " + u.GetColumn("iWeekNumber").ToString() + " /" + Convert.ToDateTime(u.GetColumn("dWeekStartDate").ToString()).Year + " (" + Convert.ToDateTime(u.GetColumn("dWeekStartDate").ToString()).DayOfWeek + " " + Convert.ToDateTime(u.GetColumn("dWeekStartDate").ToString()).Day + "/" + Convert.ToDateTime(u.GetColumn("dWeekStartDate").ToString()).Month + "/" + Convert.ToDateTime(u.GetColumn("dWeekStartDate").ToString()).Year + " till " + Convert.ToDateTime(u.GetColumn("dWeekEndDate").ToString()).DayOfWeek + " " + Convert.ToDateTime(u.GetColumn("dWeekEndDate").ToString()).Day + "/" + Convert.ToDateTime(u.GetColumn("dWeekEndDate").ToString()).Month + "/" + Convert.ToDateTime(u.GetColumn("dWeekEndDate").ToString()).Year + ")";
                                string fromAddress = string.Empty;
                                tblUsers uProfile = new tblUsers();
                                uProfile.GetUserProfile(u.PkUserID);
                                Emailing email = new Emailing();
                                email.P_ToAddress = uProfile.GetColumn("sEmail").ToString();
                                email.P_FromAddress = "noreply@west.com";
                                email.P_Email_Subject = "Payment";
                                //email.P_Message_Body = " Attention! The " + u.GetColumn("SUsername").ToString() + " " + uProfile.GetColumn("FullName").ToString() + " has still not signed up for his payment for the<br/>";
                                email.P_Message_Body = "<div><table style='border:1px solid #e8e8e8' align='center'><tr><td align='left' style='font-weight:bold;height:30px;background-color:Red;vertical-align:middle; padding-left:15px;'>" + weekDate + "</td></tr> ";
                                email.P_Message_Body += "<tr><td align='left' style='background-color:White;font-family:Verdana, Geneva, sans-serif;height:200px;' valign='top'>";
                                email.P_Message_Body += "<div style='padding-top: 10px;'><span style='font-weight: bold; margin-top: 1px;'> Attention! The" + u.GetColumn("SUsername").ToString() + " " + uProfile.GetColumn("FullName").ToString() + "</span><br /><br />";
                                email.P_Message_Body += "<span>You have still not signed up for his payment for the given Week.</span>";
                                email.P_Message_Body += "</div></td></tr></table></div>";
                                //email.P_Message_Body += "the " + weekDate + "<br/>";
                                email.Send_Email();

                                tblUserWeeklyPayments weeklyPayment = new tblUserWeeklyPayments();
                                weeklyPayment.LoadByPrimaryKey(Convert.ToInt32(u.GetColumn("pkUserWeeklyPaymentID").ToString()));
                                if (weeklyPayment.RowCount > 0)
                                {
                                    weeklyPayment.EmailDate = DateTime.Now;
                                    weeklyPayment.DModifiedDate = DateTime.Now;
                                    weeklyPayment.Save();
                                }
                            }
                        }
                        catch (Exception ex)
                        { }
                        u.MoveNext();
                    }
                }
                departments.MoveNext();
            }
        }
    }

    /// Hits a local webpage in order to add another expiring item in cache
    private void HitPage()
    {
        WebClient client = new WebClient();
        client.UseDefaultCredentials = true;
        client.DownloadData(DummyPageUrl);
    }
    private void AdminDisable()
    {
        //LC.Modal.Vardassistans.BLL.tblAdministrators admin = new LC.Modal.Vardassistans.BLL.tblAdministrators();
        // admin.DisableAdmin();
    }
    //private void Birthday()
    //{
    //    //====================NEW CODE BY BILAL 11-AUG-2010 For Availabilty Delettion==========================
    //    LC.Modal.Vardassistans.BLL.tblAvailability objAvailability = new LC.Modal.Vardassistans.BLL.tblAvailability();
    //    objAvailability.VA_DeleteAvailability();
    //    //=====================================================================================================
    //    DateTime CurrentTime = DateTime.Now;
    //    int hour = CurrentTime.Hour;
    //    int min = CurrentTime.Minute;

    //    //if (hour != 0)
    //    //{
    //    //    return;
    //    //}
    //    //if (min >= 10)
    //    //{
    //    //    return;
    //    //}
    //    AdminDisable();

    //    LC.Modal.Vardassistans.BLL.tblAdministrators admin = new LC.Modal.Vardassistans.BLL.tblAdministrators();
    //    admin.GetBirthdays();
    //    if (admin.RowCount > 0)
    //    {
    //        string strMessageBody = string.Empty;

    //        strMessageBody += "<table width='500' border='0' align='center' cellpadding='0' cellspacing='0' style='background-color:#F8F7F2; border:solid 1px #E5E0D4;'>";
    //        strMessageBody += "<tr>";
    //        strMessageBody += "<td  align='left' valign='top' style='padding:10px;'>";
    //        strMessageBody += "<table width='100%' border='0' cellspacing='1' cellpadding='1'>";
    //        strMessageBody += "<tr style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold; color: #000000; text-decoration: none; text-align:left; vertical-align:middle; background-color:#E5E0D4; height:18px;'>";

    //        strMessageBody += "<td width='25%'>Läkare</td>";
    //        strMessageBody += "<td width='18%'>Föddes </td>";
    //        strMessageBody += "<td width='25%'>Fyller</td>";
    //        strMessageBody += "<td width='32%'>Handläggare</td></tr>";
    //        for (int count = 0; count < admin.RowCount; count++)
    //        {

    //            string anchor = string.Empty;
    //            anchor += "<a style='color:#000000; text-decoration:none; cursor:pointer;' href='http://www.vardassistans.se/admin/adminprofile.aspx?dirid=" + Convert.ToString(admin.GetColumn("pkDoctorID")) + "'>";
    //            strMessageBody += "<tr style='font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: normal; color: #000000; text-decoration: none; text-align:left; vertical-align:middle; background-color:#e1e0d9; height:18px;'>";
    //            strMessageBody += "<td>";
    //            strMessageBody += anchor + Convert.ToString(admin.GetColumn("FullName")) + "</a></td>";
    //            strMessageBody += "<td>" + Convert.ToString(commonMethods.ChangeDateFormat((Convert.ToDateTime(admin.GetColumn("BD"))))) + "</td>";
    //            strMessageBody += "<td>" + Convert.ToString(admin.GetColumn("age")) + "</td>";
    //            strMessageBody += "<td>" + Convert.ToString(admin.GetColumn("AdminName")) + "</td>";
    //            strMessageBody += "</tr>";

    //            admin.MoveNext();
    //        }
    //        strMessageBody += "</table>";
    //        strMessageBody += "</td>";
    //        strMessageBody += "</tr>";
    //        strMessageBody += "</table>";


    //        LC.Modal.Vardassistans.BLL.tblAdministrators adminEmails = new LC.Modal.Vardassistans.BLL.tblAdministrators();
    //        adminEmails.LoadAdmins();
    //        string strEmails = string.Empty;
    //        if (adminEmails.RowCount > 0)
    //        {
    //            for (int mailcount = 0; mailcount < adminEmails.RowCount; mailcount++)
    //            {
    //                if ((adminEmails.s_SEmail != "") && (adminEmails.s_SEmail != null))
    //                {
    //                    strEmails += adminEmails.SEmail + ",";

    //                }
    //                adminEmails.MoveNext();
    //            }
    //            //strEmails = "amjad@leadconcept.net,ukd@leadconcept.net,";
    //            if (strEmails.EndsWith(","))
    //            {
    //                strEmails = strEmails.Substring(0, strEmails.Length - 1);
    //            }
    //        }

    //        LC.Modal.Vardassistans.BLL.tblEmailSender objEmailSender = new LC.Modal.Vardassistans.BLL.tblEmailSender();

    //        objEmailSender.LoadAll();
    //        if (objEmailSender.RowCount > 0)
    //        {
    //            LC.Modal.Vardassistans.BLL.tblEmailSender objEmailSenderNew = new LC.Modal.Vardassistans.BLL.tblEmailSender();
    //            objEmailSenderNew.VA_EmailSender();
    //            if (objEmailSenderNew.RowCount == 0)
    //            {
    //                Emailing email = new Emailing();
    //                email.P_ToAddress = strEmails;
    //                email.P_FromAddress = "admin@vardassistans.se";
    //                //email.P_BCCAddress = "bilal@leadconcept.com";
    //                email.P_Email_Subject = "Birthday Reminder";
    //                email.P_Message_Body = strMessageBody;
    //                email.Send_Email();
    //                objEmailSender.PkReminder = 1;
    //                objEmailSender.DReminderDate = DateTime.Now.Date;
    //                objEmailSender.Save();
    //            }
    //        }
    //        else
    //        {
    //            Emailing email = new Emailing();
    //            email.P_ToAddress = strEmails;
    //            email.P_FromAddress = "admin@vardassistans.se";
    //            //email.P_BCCAddress = "bilal@leadconcept.com";
    //            email.P_Email_Subject = "Birthday Reminder";
    //            email.P_Message_Body = strMessageBody;
    //            email.Send_Email();
    //            objEmailSender.AddNew();
    //            objEmailSender.PkReminder = 1;
    //            objEmailSender.DReminderDate = DateTime.Now.Date;
    //            objEmailSender.Save();
    //        }
    //    }


    //}
}
//}

