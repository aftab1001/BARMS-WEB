using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using LC.Model.BMS.BLL;
public partial class UserControls_Messaging : System.Web.UI.UserControl
{
    int UserID;
    int DepartmentID;
    int AccessLevel;
    static bool check = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //HtmlAnchor ankMessages = ((HtmlAnchor)((MasterPage)(sender)).FindControl("ankMessages")) as HtmlAnchor;
        if (Session["UserLogin"] != null)
        {
            SessionUser user = new SessionUser();
            user = (SessionUser)Session["UserLogin"];
            UserID = user.UserID;
            DepartmentID = user.DepartmentID;
            AccessLevel = user.AccessLevel;
            if (user.AccessLevel != 2)
            {
                //Session["UserLogin"] = null;
                //Response.Redirect("../West_login.aspx");
            }
            //UserID = user.UserID;
            if (user.AccessLevel == 1)
                lnkChangeDepartment.Visible = true;            
        }
        else
        {
            Session["UserLogin"] = null;
            Response.Redirect("../West_login.aspx");
        }

        if (!Page.IsPostBack)
        {
            BindGrid(3);
            //BindSentBox();
            BindInBox();
            BindDropDowns();
            string text = "By pressing the Send Request button you will send a request to work in another department of the company that you selected on the left drop down box. This request will be sent both internally and through proper email to the manager of the selected department. It’s up to the department’s manager to contact you as necessary.";
            btnSendRequest.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + text + "')");
            btnSendRequest.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
           
        }

    }
    private void BindDropDowns()
    {
        tblDepartments department = new tblDepartments();
        department.LoadAll();
        commonMethods.FillDropDownList(ddlDepartments, department.DefaultView, "sDepartmentName", "pkDepartmentID");
    }
    private void CreateUsers()
    {
        hidAllUsers.Value = "";
        tblUsers users = new tblUsers();
        users.GetUserDepartment(UserID);
        if (users.RowCount > 0)
        {
            tblUsers accessLevelUsers = new tblUsers();
            accessLevelUsers.GetAllUsersForMessages(UserID,Convert.ToInt32(users.GetColumn("pkDepartmentid")));
            if (accessLevelUsers.RowCount > 0)
            {
                for (int i = 0; i < accessLevelUsers.RowCount; i++)
                {
                    hidAllUsers.Value += accessLevelUsers.GetColumn("pkUserID").ToString() + ":" + accessLevelUsers.GetColumn("FullName").ToString()+",";
                    accessLevelUsers.MoveNext();
                }
                if(hidAllUsers.Value != "" && hidAllUsers.Value != null && hidAllUsers.Value != "0") 
                    hidAllUsers.Value = hidAllUsers.Value.Substring(0, hidAllUsers.Value.Length - 1);
            }
        }
    }
    private void BindSentBox()
    {

        tblUserSentBox userSent = new tblUserSentBox();
        userSent.GetAllSentMessages(UserID);
        
        grdSent.DataSource = userSent.DefaultView;
        grdSent.DataBind();
        if (userSent.RowCount == 0)
            btnDeleteOutBox.Visible = false;
        else
            btnDeleteOutBox.Visible = true;
        MV1.ActiveViewIndex = 3;
    }
    private void BindInBox()
    {
        tblUserInBox userIn = new tblUserInBox();
        userIn.GetAllReveivedMessages(UserID);
        grdReceive.DataSource = userIn.DefaultView;
        grdReceive.DataBind();
        if (userIn.RowCount == 0)
        {
            btnDeleteInBox.Visible = false;
            //userIn.FlushData();
            //userIn.GetAlleMessagebyReply(UserID);
            //DataView dv = userIn.DefaultView;
            //DataTable dt = dv.Table;

            //if (dt.Rows.Count > 1)
            //    dt.Rows.RemoveAt(dt.Rows.Count - 1);
            //grdReceive.DataSource = dt;
            //grdReceive.DataBind();
        }
        else
        {
            int countUnReadMessages = 0;
            btnDeleteInBox.Visible = true;
            for (int i = 0; i < userIn.RowCount; i++)
            {
                if (!Convert.ToBoolean(userIn.GetColumn("bIsread")))
                    countUnReadMessages++;
                userIn.MoveNext();
            }
            if (countUnReadMessages == 0)
                spInbox.InnerHtml = "";
            else
            {
                spInbox.InnerHtml = "(" + countUnReadMessages.ToString() + ")";
                

                //ankMessages.InnerHtml = "<span style='float:left;'> My Messages </span><span class='span'>(" + count.ToString() + " New)</span>";
            }
        }

        MV1.ActiveViewIndex = 0;
    }
/*

    protected void btnInbox_Click(object sender, EventArgs e)
    {
        hidUsers.Value = "";
        BindInBox();
        MV1.ActiveViewIndex = 0;
    }
    protected void btnCompose_Click(object sender, EventArgs e)
    {
        
        CreateUsers();
        if (hidUsers.Value != "" && hidUsers.Value != null && hidUsers.Value != "0")
        {
            string[] usersArray = hidUsers.Value.Split(',');
            string[] singleUser;
            for (int i = 0; i < usersArray.Length; i++)
            {
                singleUser = usersArray[i].Split(':');
                if (singleUser.Length > 0)
                {
                    HtmlContainerControl liU = new HtmlGenericControl("li");
                    if (singleUser.Length == 2)
                    {
                        liU.Attributes.Add("value", singleUser[0].ToString());
                        liU.InnerHtml = singleUser[1].ToString();
                    }
                    else if(singleUser.Length == 1)
                    {
                        liU.InnerHtml = singleUser[0].ToString();
                    }
                    preadded.Controls.Add(liU);
                }
            }
        }
        MV1.ActiveViewIndex = 1;
       
    }
    protected void btnUsers_Click(object sender, EventArgs e)
    {
        MV1.ActiveViewIndex = 2;
    }
    protected void btnSent_Click(object sender, EventArgs e)
    {
        hidUsers.Value = "";
        BindSentBox();
        MV1.ActiveViewIndex = 3;
    }
    */

    private void BindGrid(int active)
    {
        check = false;
        tblUsers user = new tblUsers();
        user.LoadUsers(active, DepartmentID, UserID);
        grdUsers.DataSource = user.DefaultView;
        grdUsers.DataBind();

    }
    protected void grdUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow drv = ((System.Data.DataRowView)e.Row.DataItem).Row;

                //ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //imgDelete.Attributes.Add("onclick", "javascript:return confirm('Are you sure you want to delete it ?');");
                //Label lblemail = (Label)e.Row.FindControl("lblemail");
                //lblemail.Text = drv["sEmail"].ToString();
                CheckBox chk = e.Row.FindControl("chk") as CheckBox;
                
                Label lblUseriD = (Label)e.Row.FindControl("lblUserID");
                lblUseriD.Text = drv["pkUserID"].ToString();
                LinkButton lnkUser = (LinkButton)e.Row.FindControl("lnkUser");
                lnkUser.PostBackUrl = "../Managers/EditUser.aspx?id=" + drv["pkUserID"].ToString();
                lnkUser.Text = drv["FullName"].ToString();

                string name = drv["FullName"].ToString();
                if (name.Length > 20)
                {
                    lnkUser.Text = lnkUser.Text.Substring(0, 20)+"...";
                    lnkUser.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lnkUser.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }


                

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {


            case "Active":
                int userid = Convert.ToInt32(e.CommandArgument);
                tblUsers user = new tblUsers();
                user.LoadByPrimaryKey(userid);
                if (user.BActiveByAdmin == false)
                {
                    user.BActiveByAdmin = true;
                    user.FkActivatedByAdminID = UserID;
                }
                else
                {
                    user.BActiveByAdmin = false;
                    user.FkActivatedByAdminID = UserID;
                }
                user.Save();

                break;
        }
        BindGrid(3);
    }
    protected void grdSent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int pkSentBoxID = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "message":
                    tblUserSentBox userSent = new tblUserSentBox();
                    userSent.GetAllSentMessages(UserID);
                    if (userSent.RowCount > 0)
                    {
                        for (int i = 0; i < userSent.RowCount; i++)
                        {
                            if (userSent.GetColumn("pkSentBoxID").ToString() == pkSentBoxID.ToString())
                            {
                                lblFrom.Text = userSent.GetColumn("UserFrom").ToString();
                                lblTo.Text = userSent.GetColumn("name").ToString();
                                lblDate.Text = Convert.ToDateTime(userSent.GetColumn("dSentDate")).ToLongDateString();
                                lblSubject.Text = userSent.GetColumn("sSubject").ToString();
                                divDetail.InnerHtml = userSent.GetColumn("sMessage").ToString();

                                hidDetailToUserID.Value = userSent.GetColumn("fkToUserID").ToString();
                                hidDetailFromUserID.Value = userSent.GetColumn("fkFromUserID").ToString();

                                hidReplySentboxID.Value = userSent.GetColumn("pkSentBoxID").ToString();
                                /*
                                tblReply reply = new tblReply();
                                tblMessages messages = new tblMessages();
                                messages.GetMessageRowBySentboxID(Convert.ToInt32(userSent.GetColumn("pkSentBoxID").ToString()));
                                if (messages.RowCount > 0)
                                {
                                    reply.GetAllReplies(messages.PkMessageID);
                                }
                                
                                grdReplies.DataSource = reply.DefaultView;
                                grdReplies.DataBind();
                                if (reply.RowCount > 0)
                                {
                                    lblReplyMessage.Visible = true;
                                }
                                else
                                {
                                    lblReplyMessage.Visible = false;
                                }
                                 */
                                break;
                            }
                            userSent.MoveNext();
                        }
                       
                        
                        MV1.ActiveViewIndex = 4;
                    }
                    
                    break;
            }

        }
    }
    protected void grdSent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            /*
            tblReply reply = new tblReply();
            tblMessages messages = new tblMessages();
            messages.GetMessageRowBySentboxID(Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "pkSentBoxID")));
            if (messages.RowCount > 0)
            {
                reply.GetAllReplies(messages.PkMessageID);

            }
             */
            Label lblToAddress = e.Row.FindControl("lblToAddress") as Label;
            
            Label lblDate = e.Row.FindControl("lblDate") as Label;
            LinkButton lnkSubject = e.Row.FindControl("lnkSubject") as LinkButton;
            /*
            int rows = 0;
            //reply.GetAllReplies(Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "pkSentBoxID")), Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "fkToUserID")), Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "fkFromUserID")));
            if (reply.RowCount > 0)
            {
                rows = reply.RowCount;
                lnkMessage.Text = "Re: ";
            }
             */
           
            CheckBox chkTo = e.Row.FindControl("chkTo") as CheckBox;
            if (DataBinder.GetPropertyValue(e.Row.DataItem, "fkFromUserID").ToString() == DataBinder.GetPropertyValue(e.Row.DataItem, "fkToUserID").ToString())
                lblToAddress.Text = "To: me";
            else
            {
                lblToAddress.Text = "To: " + DataBinder.GetPropertyValue(e.Row.DataItem, "name").ToString();
                string name = DataBinder.GetPropertyValue(e.Row.DataItem, "name").ToString();
                if (name.Length > 20)
                {
                    lblToAddress.Text = "To: " + name.Substring(0, 20)+"...";
                    lblToAddress.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblToAddress.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
            }

            //if (rows != 0)
            //    lblToAddress.Text += " (" + rows.ToString() + ")";
            if(DataBinder.GetPropertyValue(e.Row.DataItem, "sSubject") != null)
                lnkSubject.Text = DataBinder.GetPropertyValue(e.Row.DataItem, "sSubject").ToString();
           
            
            lblDate.Text = Convert.ToDateTime(DataBinder.GetPropertyValue(e.Row.DataItem, "dSentDate")).ToLongDateString();
        }
    }
    
    protected void grdReceive_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            int pkInBoxID = Convert.ToInt32(e.CommandArgument);
            switch (e.CommandName.ToLower())
            {
                case "message":
                    tblUserInBox userIn = new tblUserInBox();
                    userIn.LoadByPrimaryKey(pkInBoxID);
                    if (userIn.RowCount > 0)
                    {
                        if (!userIn.BIsread)
                        {
                            userIn.BIsread = true;
                            userIn.DReceivedDate = DateTime.Now;
                            userIn.Save();
                            int unread;
                            if (spInbox.InnerHtml.IndexOf('(') != -1)
                            {
                                unread = Convert.ToInt32(spInbox.InnerHtml.Substring(spInbox.InnerHtml.IndexOf('(') + 1, 1)) - 1;
                                if (unread != 0)
                                    spInbox.InnerHtml = "(" + unread.ToString() + ")";
                                else if (unread == 0)
                                    spInbox.InnerHtml = "";
                            }
                            HtmlAnchor ankMessages = Page.Master.FindControl("ankMessages") as HtmlAnchor;
                            if (ankMessages != null)
                            {
                                if (ankMessages.InnerText.IndexOf('(') != -1)
                                {
                                    unread = Convert.ToInt32(ankMessages.InnerText.Substring(ankMessages.InnerText.IndexOf('(') + 1, 1)) - 1;
                                    if (unread != 0)
                                        ankMessages.InnerHtml = "My Messages (" + unread.ToString() + " )";
                                    else if (unread == 0)
                                        ankMessages.InnerHtml = "My Messages";
                                }
                            }
                        }
                        
                    }

                    userIn.FlushData();
                    userIn.GetAllReveivedMessages(UserID);


                    if (userIn.RowCount > 0)
                    {
                        for (int i = 0; i < userIn.RowCount; i++)
                        {
                            if (userIn.GetColumn("pkInBoxID").ToString() == pkInBoxID.ToString())
                            {

                                lblFrom.Text = userIn.GetColumn("UserFrom").ToString();
                                lblTo.Text = userIn.GetColumn("name").ToString();
                                lblDate.Text = Convert.ToDateTime(userIn.GetColumn("dReceivedDate")).ToLongDateString();
                                lblSubject.Text = userIn.GetColumn("sSubject").ToString();
                                divDetail.InnerHtml = userIn.GetColumn("sMessage").ToString();

                                hidDetailToUserID.Value = userIn.GetColumn("fkFromUserID").ToString();
                                hidDetailFromUserID.Value = userIn.GetColumn("fkToUserID").ToString();
                                hidReplyInboxID.Value = userIn.GetColumn("pkInBoxID").ToString();

                                /*
                                tblReply reply = new tblReply();
                                tblMessages messages = new tblMessages();
                                messages.GetMessageRowByInboxID(Convert.ToInt32(userIn.GetColumn("pkInBoxID").ToString()));
                                if (messages.RowCount > 0)
                                {
                                    reply.GetAllReplies(messages.PkMessageID);
                                }

                                grdReplies.DataSource = reply.DefaultView;
                                grdReplies.DataBind();
                                if (reply.RowCount > 0)
                                {
                                    lblReplyMessage.Visible = true;
                                }
                                else
                                {
                                    lblReplyMessage.Visible = false;
                                }
                                */
                                break;
                            }
                            userIn.MoveNext();
                        }
                        MV1.ActiveViewIndex = 4;
                    }
                    /*
                    else
                    {
                        userIn.FlushData();
                        userIn.GetAlleMessagebyReply(UserID);
                        if (userIn.RowCount > 0)
                        {

                            for (int i = 0; i < userIn.RowCount; i++)
                            {
                                if (userIn.GetColumn("pkInBoxID").ToString() == pkInBoxID.ToString())
                                {
                                    tblUserSentBox sent = new tblUserSentBox();
                                    sent.GetAllSentMessages(UserID);
                                    if (sent.RowCount > 0)
                                    {
                                        lblFrom.Text = sent.GetColumn("UserFrom").ToString();
                                        lblTo.Text = sent.GetColumn("name").ToString();
                                        lblDate.Text = Convert.ToDateTime(userIn.GetColumn("dReceivedDate")).ToLongDateString();
                                        lblSubject.Text = sent.GetColumn("sSubject").ToString();
                                        divDetail.InnerHtml = sent.GetColumn("sMessage").ToString();

                                        hidDetailToUserID.Value = userIn.GetColumn("fkFromUserID").ToString();
                                        hidDetailFromUserID.Value = userIn.GetColumn("fkToUserID").ToString();
                                        hidReplyInboxID.Value = userIn.GetColumn("pkinboxid").ToString();
                                    }

                                    tblReply reply = new tblReply();
                                    tblMessages messages = new tblMessages();
                                    messages.GetMessageRowByInboxID(Convert.ToInt32(userIn.GetColumn("pkInBoxID").ToString()));
                                    if (messages.RowCount > 0)
                                    {
                                        reply.GetAllReplies(messages.PkMessageID);
                                    }

                                    grdReplies.DataSource = reply.DefaultView;
                                    grdReplies.DataBind();
                                    if (reply.RowCount > 0)
                                    {
                                        lblReplyMessage.Visible = true;
                                    }
                                    else
                                    {
                                        lblReplyMessage.Visible = false;
                                    }

                                    break;
                                }
                                userIn.MoveNext();
                            }
                            MV1.ActiveViewIndex = 4;
                        }
                    }
                     */

                    break;
            }

        }
    }
    protected void grdReceive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            /*
            tblReply reply = new tblReply();
            tblMessages messages = new tblMessages();
            messages.GetMessageRowByInboxID(Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "pkInBoxID")));
            if (messages.RowCount > 0)
            {
                reply.GetAllReplies(messages.PkMessageID);

            }
            */

            Label lblFromAddress = e.Row.FindControl("lblFromAddress") as Label;

            Label lblDateFrom = e.Row.FindControl("lblDateFrom") as Label;
            LinkButton lnkSubjectFrom = e.Row.FindControl("lnkSubjectFrom") as LinkButton;

            CheckBox chkFrom = e.Row.FindControl("chkFrom") as CheckBox;
            /*
            int rows = 0;
            //reply.GetAllReplies(Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "pkInBoxID")), Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "fkToUserID")), Convert.ToInt32(DataBinder.GetPropertyValue(e.Row.DataItem, "fkFromUserID")));
            if (reply.RowCount > 0)
            {
                rows = reply.RowCount;
                lnkMessageFrom.Text = "Re: ";
            }
            */
            if (DataBinder.GetPropertyValue(e.Row.DataItem, "fkFromUserID").ToString() == DataBinder.GetPropertyValue(e.Row.DataItem, "fkToUserID").ToString())
                lblFromAddress.Text = "me";
            else
            {
                lblFromAddress.Text = DataBinder.GetPropertyValue(e.Row.DataItem, "UserFrom").ToString();
                string name = DataBinder.GetPropertyValue(e.Row.DataItem, "UserFrom").ToString();
                if (name.Length > 20)
                {
                    lblFromAddress.Text = lblFromAddress.Text.Substring(0, 20)+"...";
                    lblFromAddress.Attributes.Add("onmouseover", "javascript:OpenFeedbackWindow('" + name + "')");
                    lblFromAddress.Attributes.Add("onmouseout", "javascript:CloseFeedBackWindow()");
                }
            }
            //if(rows != 0)
            //    lblFromAddress.Text += " ("+rows.ToString()+")";
            //if (DataBinder.GetPropertyValue(e.Row.DataItem, "sMessage").ToString().Length > 50)
            //    lnkSubjectFrom.Text = DataBinder.GetPropertyValue(e.Row.DataItem, "sMessage").ToString().Substring(0, 50) + " ...";
            //else
                lnkSubjectFrom.Text = DataBinder.GetPropertyValue(e.Row.DataItem, "sSubject").ToString();
            //lblDateFrom.Text = Convert.ToDateTime(DataBinder.GetPropertyValue(e.Row.DataItem, "dReceivedDate")).ToLongDateString();
                int day;
                string mnonthName;

                day = Convert.ToDateTime(DataBinder.GetPropertyValue(e.Row.DataItem, "dReceivedDate")).Day;
                mnonthName = Convert.ToDateTime(DataBinder.GetPropertyValue(e.Row.DataItem, "dReceivedDate")).ToString("MMMM");
            
                lblDateFrom.Text = day + " " + mnonthName + " " + Convert.ToDateTime(DataBinder.GetPropertyValue(e.Row.DataItem, "dReceivedDate")).Year;
            if (!Convert.ToBoolean(DataBinder.GetPropertyValue(e.Row.DataItem, "bIsread")))
            {
                e.Row.Style.Add("font-weight", "bold");
            }
            else
            {
                e.Row.Style.Add("font-weight", "normal");
            }
        }
    }
    protected void btnSubmitComposeMessage_Click(object sender, EventArgs e)
    {
        //if ((hidReplyInboxID.Value != "" && hidReplyInboxID.Value != "0" && hidReplyInboxID.Value != null) ||
        //    (hidReplySentboxID.Value != "" && hidReplySentboxID.Value != "0" && hidReplySentboxID.Value != null))
        //{
        //    if (hidUsers.Value != "" && hidUsers.Value != null && hidUsers.Value != "0")
        //    {
        //        string[] arr = hidUsers.Value.Split(',');
        //        tblUsers userCheck = new tblUsers();
        //        try
        //        {

        //            string[] Name;
        //            string firstName = string.Empty;
        //            for (int i = 0; i < arr.Length; i++)
        //            {
        //                if (arr[i].IndexOf(' ') != -1)
        //                {
        //                    Name = arr[i].Split(' ');
        //                    firstName = Name[0].ToString();
        //                }
        //                else
        //                {
        //                    firstName = arr[i].ToString();
        //                }
        //                userCheck.FlushData();
        //                userCheck.GetUsersByName(firstName);

        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //        tblReply reply = new tblReply();

        //        reply.AddNew();
        //        tblMessages message = new tblMessages();

        //        if (hidReplyInboxID.Value != "" && hidReplyInboxID.Value != "0" && hidReplyInboxID.Value != null)
        //        {
        //            message.GetMessageRowByInboxID(Convert.ToInt32(hidReplyInboxID.Value));
        //            if (message.RowCount > 0)
        //                reply.FkMessageID = message.PkMessageID;    
        //        }
        //        if (hidReplySentboxID.Value != "" && hidReplySentboxID.Value != "0" && hidReplySentboxID.Value != null)
        //        {
        //            message.GetMessageRowBySentboxID(Convert.ToInt32(hidReplySentboxID.Value));
        //            if (message.RowCount > 0)
        //                reply.FkMessageID = message.PkMessageID;
        //        }
        //        reply.FkReplyFromUserID = UserID;
        //        reply.FkReplyToUserID = userCheck.PkUserID;
        //        reply.ReplySubject = txtSubject.Text;
        //        reply.ReplyMessage = txtComposeMessage.Text;
        //        reply.DReplyDate = DateTime.Now;
        //        reply.Save();
        //        hidUsers.Value = "";
        //        txtSubject.Text = "";
        //        txtComposeMessage.Text = "";
        //        hidReplyInboxID.Value = "";
        //        hidReplySentboxID.Value = "";
        //        BindInBox();
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Users", "$(document).ready(function() {alert('Pls Select User')});", true);
        //    }

        //}
        //else 
        /*

        if (hidUsers.Value != "" && hidUsers.Value != null && hidUsers.Value != "0")
        {
            string[] arr = hidUsers.Value.Split(',');

            try
            {
                tblUsers userCheck = new tblUsers();
                string[] Name;
                string firstName = string.Empty;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].IndexOf(' ') != -1)
                    {
                        Name = arr[i].Split(' ');
                        firstName = Name[0].ToString();
                    }
                    else
                    {
                        firstName = arr[i].ToString();
                    }
                    userCheck.FlushData();
                    userCheck.GetUsersByName(firstName);

                    if ((hidReplyInboxID.Value != "" && hidReplyInboxID.Value != "0" && hidReplyInboxID.Value != null) ||
                        (hidReplySentboxID.Value != "" && hidReplySentboxID.Value != "0" && hidReplySentboxID.Value != null))
                    {
 
                    }

                    tblUserInBox userIn = new tblUserInBox();
                    userIn.AddNew();
                    userIn.FkFromUserID = UserID;
                    userIn.FkToUserID = userCheck.PkUserID;
                    userIn.SSubject = txtSubject.Text;
                    userIn.SMessage = txtComposeMessage.Text;
                    userIn.DReceivedDate = DateTime.Now;
                    userIn.BIsread = false;
                    userIn.Save();

                    tblUserSentBox userOut = new tblUserSentBox();
                    userOut.AddNew();
                    userOut.FkFromUserID = UserID;
                    userOut.FkToUserID = userCheck.PkUserID;
                    userOut.SSubject = txtSubject.Text;
                    userOut.SMessage = txtComposeMessage.Text;
                    userOut.DSentDate = DateTime.Now;
                    userOut.Save();

                    //tblMessages message = new tblMessages();
                    //message.AddNew();
                    //message.FkInboxID = userIn.PkInBoxID;
                    //message.FkSentBoxID = userOut.PkSentBoxID;
                    //message.DMessageDate = DateTime.Now;
                    //message.Save();

                     
                }
                hidUsers.Value = "";
                txtSubject.Text = "";
                txtComposeMessage.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Users", "$(document).ready(function() {alert('Pls Select User')});", true); 
        }

        BindInBox();
         */ 
        
    }
    /*
    protected void btnGridUsers_Click(object sender, EventArgs e)
    {
        hidUsers.Value = "";
        if (grdUsers.Rows.Count > 0)
        {
            for (int i = 0; i < grdUsers.Rows.Count; i++)
            {
                if (((CheckBox)grdUsers.Rows[i].FindControl("chk") as CheckBox).Checked)
                {
                    LinkButton lnkUser = grdUsers.Rows[i].FindControl("lnkUser") as LinkButton;
                    Label lblUseriD = grdUsers.Rows[i].FindControl("lblUseriD") as Label;
                        
                    hidUsers.Value += lblUseriD.Text + ':' + lnkUser.Text + ',';
                }

            }
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Users", "$(document).ready(function() {$.facebooklist('#ctl00_ContentPlaceHolder1_myID_fd', '#ctl00_ContentPlaceHolder1_myID_preadded', '#facebook-auto', { url: '../fetched.php', cache: 1 }, 10, { userfilter: 1, casesensetive: 0 });});", true);
            if(hidUsers.Value!= "" && hidUsers.Value != null && hidUsers.Value!= "0" )
                hidUsers.Value = hidUsers.Value.Substring(0, hidUsers.Value.Length - 1);


        }
    }
    
    protected void btnDeleteInBox_Click(object sender, EventArgs e)
    {
        if (grdReceive.Rows.Count > 0)
        {
            tblUserInBox uin = new tblUserInBox();
            for (int i = 0; i < grdReceive.Rows.Count; i++)
            {
                if (((CheckBox)grdReceive.Rows[i].FindControl("chkFrom") as CheckBox).Checked)
                {
                    Label lblInBoxID = (Label)grdReceive.Rows[i].FindControl("lblInBoxID") as Label;
                    uin.FlushData();
                    uin.LoadByPrimaryKey(Convert.ToInt32(lblInBoxID.Text));
                    if (uin.RowCount > 0)
                    {
                        uin.MarkAsDeleted();
                        uin.Save();
                    }
                }
            }
            BindInBox();
        }
    }
      
    protected void btnDeleteOutBox_Click(object sender, EventArgs e)
    {
        if (grdSent.Rows.Count > 0)
        {
            tblUserSentBox uOut = new tblUserSentBox();
            for (int i = 0; i < grdSent.Rows.Count; i++)
            {
                if (((CheckBox)grdSent.Rows[i].FindControl("chkTo") as CheckBox).Checked)
                {
                    Label lblOutBoxID = (Label)grdSent.Rows[i].FindControl("lblOutBoxID") as Label;
                    uOut.FlushData();
                    uOut.LoadByPrimaryKey(Convert.ToInt32(lblOutBoxID.Text));
                    if (uOut.RowCount > 0)
                    {
                        uOut.MarkAsDeleted();
                        uOut.Save();
                    }
                }
            }
            BindSentBox();
        }
    }
     
    protected void btnReply_Click(object sender, EventArgs e)
    {
        hidUsers.Value = "";
        tblUsers u = new tblUsers();
        u.LoadByPrimaryKey(Convert.ToInt32(hidDetailToUserID.Value));
        if (u.RowCount > 0)
        {
            hidUsers.Value += hidDetailToUserID.Value + ':' + u.SFirstName + " " +u.SLastName;
        }
        
        MV1.ActiveViewIndex = 1;
        btnCompose_Click(null, new ImageClickEventArgs(0, 0));
    }
    */
    protected void grdReplies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblFromGrid = e.Row.FindControl("lblFromGrid") as Label;
            Label lblToGrid = e.Row.FindControl("lblToGrid") as Label;
            Label lblDateGrid = e.Row.FindControl("lblDateGrid") as Label;
            Label lblSubjectGrid = e.Row.FindControl("lblSubjectGrid") as Label;
            HtmlGenericControl divDetailGrid = e.Row.FindControl("divDetailGrid") as HtmlGenericControl;
            lblDateGrid.Text = Convert.ToDateTime(lblDateGrid.Text).ToLongDateString();
            divDetailGrid.InnerHtml = DataBinder.GetPropertyValue(e.Row.DataItem, "ReplyMessage").ToString();
        }
    }
    protected void grdReceive_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdReceive.PageIndex = e.NewPageIndex;
        BindInBox();
    }
    protected void grdSent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSent.PageIndex = e.NewPageIndex;
        BindSentBox();
    }
    protected void btnInbox_Click(object sender, ImageClickEventArgs e)
    {
        hidUsers.Value = "";
        BindInBox();
        MV1.ActiveViewIndex = 0;
    }
    protected void btnCompose_Click(object sender, ImageClickEventArgs e)
    {
        CreateUsers();
        hidUsers.Value = "";
        if (hidUsers.Value != "" && hidUsers.Value != null && hidUsers.Value != "0")
        {
            string[] usersArray = hidUsers.Value.Split(',');
            string[] singleUser;
            for (int i = 0; i < usersArray.Length; i++)
            {
                singleUser = usersArray[i].Split(':');
                if (singleUser.Length > 0)
                {
                    HtmlContainerControl liU = new HtmlGenericControl("li");
                    if (singleUser.Length == 2)
                    {
                        liU.Attributes.Add("value", singleUser[0].ToString());
                        liU.InnerHtml = singleUser[1].ToString();
                    }
                    else if (singleUser.Length == 1)
                    {
                        liU.InnerHtml = singleUser[0].ToString();
                    }
                    preadded.Controls.Add(liU);
                }
            }
        }
        MV1.ActiveViewIndex = 1;
    }
    protected void btnUsers_Click(object sender, ImageClickEventArgs e)
    {
        MV1.ActiveViewIndex = 2;
    }
    protected void btnSent_Click(object sender, ImageClickEventArgs e)
    {
        hidUsers.Value = "";
        BindSentBox();
        MV1.ActiveViewIndex = 3;
    }
    protected void btnDeleteInBox_Click(object sender, ImageClickEventArgs e)
    {
        if (grdReceive.Rows.Count > 0)
        {
            tblUserInBox uin = new tblUserInBox();
            for (int i = 0; i < grdReceive.Rows.Count; i++)
            {
                if (((CheckBox)grdReceive.Rows[i].FindControl("chkFrom") as CheckBox).Checked)
                {
                    Label lblInBoxID = (Label)grdReceive.Rows[i].FindControl("lblInBoxID") as Label;
                    uin.FlushData();
                    uin.LoadByPrimaryKey(Convert.ToInt32(lblInBoxID.Text));
                    if (uin.RowCount > 0)
                    {
                        uin.MarkAsDeleted();
                        uin.Save();
                    }
                }
            }
            BindInBox();
        }
    }
    protected void btnDeleteOutBox_Click(object sender, ImageClickEventArgs e)
    {
        if (grdSent.Rows.Count > 0)
        {
            tblUserSentBox uOut = new tblUserSentBox();
            for (int i = 0; i < grdSent.Rows.Count; i++)
            {
                if (((CheckBox)grdSent.Rows[i].FindControl("chkTo") as CheckBox).Checked)
                {
                    Label lblOutBoxID = (Label)grdSent.Rows[i].FindControl("lblOutBoxID") as Label;
                    uOut.FlushData();
                    uOut.LoadByPrimaryKey(Convert.ToInt32(lblOutBoxID.Text));
                    if (uOut.RowCount > 0)
                    {
                        uOut.MarkAsDeleted();
                        uOut.Save();
                    }
                }
            }
            BindSentBox();
        }
    }
    protected void btnSubmitComposeMessage_Click(object sender, ImageClickEventArgs e)
    {
        //if ((hidReplyInboxID.Value != "" && hidReplyInboxID.Value != "0" && hidReplyInboxID.Value != null) ||
        //    (hidReplySentboxID.Value != "" && hidReplySentboxID.Value != "0" && hidReplySentboxID.Value != null))
        //{
        //    if (hidUsers.Value != "" && hidUsers.Value != null && hidUsers.Value != "0")
        //    {
        //        string[] arr = hidUsers.Value.Split(',');
        //        tblUsers userCheck = new tblUsers();
        //        try
        //        {

        //            string[] Name;
        //            string firstName = string.Empty;
        //            for (int i = 0; i < arr.Length; i++)
        //            {
        //                if (arr[i].IndexOf(' ') != -1)
        //                {
        //                    Name = arr[i].Split(' ');
        //                    firstName = Name[0].ToString();
        //                }
        //                else
        //                {
        //                    firstName = arr[i].ToString();
        //                }
        //                userCheck.FlushData();
        //                userCheck.GetUsersByName(firstName);

        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //        tblReply reply = new tblReply();

        //        reply.AddNew();
        //        tblMessages message = new tblMessages();

        //        if (hidReplyInboxID.Value != "" && hidReplyInboxID.Value != "0" && hidReplyInboxID.Value != null)
        //        {
        //            message.GetMessageRowByInboxID(Convert.ToInt32(hidReplyInboxID.Value));
        //            if (message.RowCount > 0)
        //                reply.FkMessageID = message.PkMessageID;    
        //        }
        //        if (hidReplySentboxID.Value != "" && hidReplySentboxID.Value != "0" && hidReplySentboxID.Value != null)
        //        {
        //            message.GetMessageRowBySentboxID(Convert.ToInt32(hidReplySentboxID.Value));
        //            if (message.RowCount > 0)
        //                reply.FkMessageID = message.PkMessageID;
        //        }
        //        reply.FkReplyFromUserID = UserID;
        //        reply.FkReplyToUserID = userCheck.PkUserID;
        //        reply.ReplySubject = txtSubject.Text;
        //        reply.ReplyMessage = txtComposeMessage.Text;
        //        reply.DReplyDate = DateTime.Now;
        //        reply.Save();
        //        hidUsers.Value = "";
        //        txtSubject.Text = "";
        //        txtComposeMessage.Text = "";
        //        hidReplyInboxID.Value = "";
        //        hidReplySentboxID.Value = "";
        //        BindInBox();
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Users", "$(document).ready(function() {alert('Pls Select User')});", true);
        //    }

        //}
        //else 
        if (hidUsers.Value != "" && hidUsers.Value != null && hidUsers.Value != "0")
        {
            string[] arr = hidUsers.Value.Split(',');

            try
            {
                tblUsers userCheck = new tblUsers();
                string[] Name_ID;
                string firstName = string.Empty;
                for (int i = 0; i < arr.Length; i++)
                {

                    Name_ID = arr[i].Split(':');

                    //if (arr[i].IndexOf(' ') != -1)
                    //{
                    //    Name = arr[i].Split(' ');
                    //    firstName = Name[0].ToString();
                    //}
                    //else
                    //{
                    //    firstName = arr[i].ToString();
                    //}
                    userCheck.FlushData();
                    //userCheck.GetUsersByName(firstName);
                    userCheck.LoadByPrimaryKey(Convert.ToInt32(Name_ID[1]));

                    if ((hidReplyInboxID.Value != "" && hidReplyInboxID.Value != "0" && hidReplyInboxID.Value != null) ||
                        (hidReplySentboxID.Value != "" && hidReplySentboxID.Value != "0" && hidReplySentboxID.Value != null))
                    {

                    }

                    tblUserInBox userIn = new tblUserInBox();
                    userIn.AddNew();
                    userIn.FkFromUserID = UserID;
                    userIn.FkToUserID = userCheck.PkUserID;
                    userIn.SSubject = txtSubject.Text;
                    userIn.SMessage = txtComposeMessage.Text;
                    userIn.DReceivedDate = DateTime.Now;
                    userIn.BIsread = false;
                    userIn.Save();

                    tblUserSentBox userOut = new tblUserSentBox();
                    userOut.AddNew();
                    userOut.FkFromUserID = UserID;
                    userOut.FkToUserID = userCheck.PkUserID;
                    userOut.SSubject = txtSubject.Text;
                    userOut.SMessage = txtComposeMessage.Text;
                    userOut.DSentDate = DateTime.Now;
                    userOut.Save();

                    //tblMessages message = new tblMessages();
                    //message.AddNew();
                    //message.FkInboxID = userIn.PkInBoxID;
                    //message.FkSentBoxID = userOut.PkSentBoxID;
                    //message.DMessageDate = DateTime.Now;
                    //message.Save();


                }
                hidUsers.Value = "";
                txtSubject.Text = "";
                txtComposeMessage.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Users", "$(document).ready(function() {alert('Pls Select User')});", true);
        }

        BindInBox();
    }
    protected void btnReply_Click(object sender, ImageClickEventArgs e)
    {
        hidUsers.Value = "";
        tblUsers u = new tblUsers();
        u.LoadByPrimaryKey(Convert.ToInt32(hidDetailToUserID.Value));
        if (u.RowCount > 0)
        {
            hidUsers.Value += hidDetailToUserID.Value + ':' + u.SFirstName + " " + u.SLastName;
        }
        if (hidUsers.Value != "" && hidUsers.Value != null && hidUsers.Value != "0")
        {
            string[] usersArray = hidUsers.Value.Split(',');
            string[] singleUser;
            for (int i = 0; i < usersArray.Length; i++)
            {
                singleUser = usersArray[i].Split(':');
                if (singleUser.Length > 0)
                {
                    HtmlContainerControl liU = new HtmlGenericControl("li");
                    if (singleUser.Length == 2)
                    {
                        liU.Attributes.Add("value", singleUser[0].ToString());
                        liU.InnerHtml = singleUser[1].ToString();
                    }
                    else if (singleUser.Length == 1)
                    {
                        liU.InnerHtml = singleUser[0].ToString();
                    }
                    preadded.Controls.Add(liU);
                }
            }
        }
        MV1.ActiveViewIndex = 1;
       // btnCompose_Click(null, new ImageClickEventArgs(0, 0));
    }
    protected void btnGridUsers_Click(object sender, ImageClickEventArgs e)
    {
        hidUsers.Value = "";
        if (grdUsers.Rows.Count > 0)
        {
            for (int i = 0; i < grdUsers.Rows.Count; i++)
            {
                if (((CheckBox)grdUsers.Rows[i].FindControl("chk") as CheckBox).Checked)
                {
                    LinkButton lnkUser = grdUsers.Rows[i].FindControl("lnkUser") as LinkButton;
                    Label lblUseriD = grdUsers.Rows[i].FindControl("lblUseriD") as Label;

                    hidUsers.Value += lblUseriD.Text + ':' + lnkUser.Text + ',';
                }

            }
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Users", "$(document).ready(function() {$.facebooklist('#ctl00_ContentPlaceHolder1_myID_fd', '#ctl00_ContentPlaceHolder1_myID_preadded', '#facebook-auto', { url: '../fetched.php', cache: 1 }, 10, { userfilter: 1, casesensetive: 0 });});", true);
            if (hidUsers.Value != "" && hidUsers.Value != null && hidUsers.Value != "0")
                hidUsers.Value = hidUsers.Value.Substring(0, hidUsers.Value.Length - 1);


            for (int i = 0; i < grdUsers.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)grdUsers.Rows[i].FindControl("chk") as CheckBox;
                chk.Checked = false;
            }

            if (hidUsers.Value != "" && hidUsers.Value != null && hidUsers.Value != "0")
            {
                string[] usersArray = hidUsers.Value.Split(',');
                string[] singleUser;
                for (int i = 0; i < usersArray.Length; i++)
                {
                    singleUser = usersArray[i].Split(':');
                    if (singleUser.Length > 0)
                    {
                        HtmlContainerControl liU = new HtmlGenericControl("li");
                        if (singleUser.Length == 2)
                        {
                            liU.Attributes.Add("value", singleUser[0].ToString());
                            liU.InnerHtml = singleUser[1].ToString();
                        }
                        else if (singleUser.Length == 1)
                        {
                            liU.InnerHtml = singleUser[0].ToString();
                        }
                        preadded.Controls.Add(liU);
                    }
                }
            }
            MV1.ActiveViewIndex = 1;

           // btnCompose_Click(null, new ImageClickEventArgs(0, 0));
        }
    }
    protected void btnSendRequest_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tblUsers uStaff = new tblUsers();
            uStaff.LoadByPrimaryKey(UserID);

            tblDepartments dStaff = new tblDepartments();
            dStaff.LoadByPrimaryKey(DepartmentID);

            tblUsers uManger = new tblUsers();
            uManger.LoadManagers(DepartmentID, UserID);

            tblUsers uRequestedManger = new tblUsers();
            uRequestedManger.LoadManagersRequested(Convert.ToInt32(ddlDepartments.SelectedValue));


            if (dStaff.PkDepartmentID != Convert.ToInt32(ddlDepartments.SelectedValue))
            {
                if (uRequestedManger.RowCount > 0)
                {
                    if (uManger.RowCount > 0 && uStaff.RowCount > 0)
                    {
                        string message = "I want to change my department from [" + dStaff.SDepartmentName + "] to [" + ddlDepartments.SelectedItem.Text + "].<br> Kindly accept this request.<br/><br/>" + uStaff.SFirstName + " " + uStaff.SLastName + "<br/>";

                        #region InBox
                        tblUserInBox userIn = new tblUserInBox();
                        // For Current Manger
                        userIn.AddNew();
                        userIn.FkFromUserID = UserID;
                        userIn.FkToUserID = uManger.PkUserID;
                        userIn.SSubject = "Department Change";
                        userIn.SMessage = message;
                        userIn.DReceivedDate = DateTime.Now;
                        userIn.BIsread = false;
                        userIn.Save();
                        userIn.FlushData();

                        // For Targeted Manger
                        
                        userIn.AddNew();
                        userIn.FkFromUserID = UserID;
                        userIn.FkToUserID = uRequestedManger.PkUserID;
                        userIn.SSubject = "Department Change";
                        userIn.SMessage = message;
                        userIn.DReceivedDate = DateTime.Now;
                        userIn.BIsread = false;
                        userIn.Save();
                          
                        #endregion

                        #region SentBox
                        tblUserSentBox userOut = new tblUserSentBox();
                        
                        //For Current Manager
                        userOut.AddNew();
                        userOut.FkFromUserID = UserID;
                        userOut.FkToUserID = uManger.PkUserID;
                        userOut.SSubject = "Department Change";
                        userOut.SMessage = message;
                        userOut.DSentDate = DateTime.Now;
                        userOut.Save();
                        userOut.FlushData();
                        
                        //For Requested Manager
                        
                        userOut.AddNew();
                        userOut.FkFromUserID = UserID;
                        userOut.FkToUserID = uRequestedManger.PkUserID;
                        userOut.SSubject = "Department Change";
                        userOut.SMessage = message;
                        userOut.DSentDate = DateTime.Now;
                        userOut.Save();
                         
                        #endregion

                        tblUserEmails userEmails = new tblUserEmails();
                        userEmails.LoadUserEmails(uStaff.PkUserID);

                        tblUserEmails managerEmails = new tblUserEmails();
                        managerEmails.LoadUserEmails(uManger.PkUserID);

                        tblUserEmails managerRequestedEmails = new tblUserEmails();
                        managerRequestedEmails.LoadUserEmails(uRequestedManger.PkUserID);

                        if (userEmails.RowCount > 0 && managerEmails.RowCount > 0)
                        {
                            #region Email to Current Manager & Requested Manager

                            Emailing emailToManager = new Emailing();
                            for (int i = 0; i < userEmails.RowCount; i++)
                            {
                                if (userEmails.BIsPrimary)
                                {
                                    emailToManager.P_FromAddress = userEmails.SEmail;
                                    break;
                                }
                                userEmails.MoveNext();
                            }
                            for (int i = 0; i < managerEmails.RowCount; i++)
                            {
                                if (managerEmails.BIsPrimary)
                                {
                                    emailToManager.P_ToAddress = "hamid@leadconcept.com";//managerEmails.SEmail;
                                    break;
                                }
                                managerEmails.MoveNext();
                            }

                            emailToManager.P_CCAddress = "hamidrashid23@gmail.com";//managerRequestedEmails.SEmail;

                            
                            for (int i = 0; i < managerRequestedEmails.RowCount; i++)
                            {
                                if (managerRequestedEmails.BIsPrimary)
                                {
                                    emailToManager.P_CCAddress = managerRequestedEmails.SEmail;
                                    break;
                                }
                                managerRequestedEmails.MoveNext();
                            }
                            
                            emailToManager.P_Email_Subject = "Department Change";
                            emailToManager.P_Message_Body = message;
                            emailToManager.Send_Email();

                            #endregion
                        }
                        lblSendDeparmentMessage.Visible = true;
                        lblSendDeparmentMessage.Text = "Request Is Sent to Department Manager";
                    }
                    else
                    {
                        lblSendDeparmentMessage.Visible = true;
                        lblSendDeparmentMessage.Text = "Request Failed by Department.";
                    }
                }
                else
                {
                    lblSendDeparmentMessage.Visible = true;
                    lblSendDeparmentMessage.Text = "Request Failed by Department.";
                }
            }
            else
            {
                lblSendDeparmentMessage.Visible = true;
                lblSendDeparmentMessage.Text = "You are already in your requested department.";
            }
        }catch(Exception ex)
        {

        }

    }
    protected void lnkChangeDepartment_Click(object sender, EventArgs e)
    {
        lnkChangeDepartment.Visible = false;
        MV1.SetActiveView(ViewSendRequest);
    }
}
