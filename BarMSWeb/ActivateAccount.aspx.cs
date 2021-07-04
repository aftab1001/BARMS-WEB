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
public partial class ActivateAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
    }
    protected void btnOK_Click(object sender, ImageClickEventArgs e)
    {
        tblUsers user = new tblUsers();
        user.AuthenticateActivationCode(txtCode.Text);
        if (user.RowCount > 0)
        {
            user.BActiveByUser = true;
            user.Save();
            lblError.Visible = true;
            lblError.Text = " Account Activated By User. Please wait while account is also activated by Admin";
            tblUserDepartment tbld = new tblUserDepartment();
            tbld.LoadUserDepartment(Convert.ToInt32(user.GetColumn("pkUserID").ToString()));
            string DepartmentId = tbld.FkDepartmentID.ToString();
            tblUserAccessLevel tblua = new tblUserAccessLevel();
            DataTable dt = tblua.LoadAllUserId(5, Convert.ToInt32(DepartmentId));
            if (dt.Rows.Count > 0)
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                   
                    tblUserInBox userIn = new tblUserInBox();
                    userIn.AddNew();
                    userIn.FkFromUserID = Convert.ToInt32(user.GetColumn("pkUserID").ToString());// UserID;
                    userIn.FkToUserID = Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString());//userCheck.PkUserID;
                    userIn.SSubject = "New Activate  Account @ West Bar"; //txtSubject.Text;
                    userIn.SMessage = "New user name " + user.s_SFirstName + " " + user.s_SLastName + "<br/>Thank you";//txtComposeMessage.Text;
                    userIn.DReceivedDate = DateTime.Now;
                    userIn.BIsread = false;
                    userIn.Save();

                    tblUserSentBox userOut = new tblUserSentBox();
                    userOut.AddNew();
                    userOut.FkFromUserID = Convert.ToInt32(user.GetColumn("pkUserID").ToString()); //UserID;
                    userOut.FkToUserID = Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString());// userCheck.PkUserID;
                    userOut.SSubject = "New Activate  Account @ West Bar"; //txtSubject.Text;
                    userOut.SMessage = "New user name " + user.s_SFirstName + " " + user.s_SLastName + "<br/>Thank you";//txtComposeMessage.Text;
                    userOut.DSentDate = DateTime.Now;
                    userOut.Save();
                }
            }
            else
            {
            tblUserInBox userIn = new tblUserInBox();
            userIn.AddNew();
            userIn.FkFromUserID =Convert.ToInt32(user.GetColumn("pkUserID").ToString());// UserID;
            userIn.FkToUserID = 10;//userCheck.PkUserID;
            userIn.SSubject = "New Activate  Account @ West Bar"; //txtSubject.Text;
            userIn.SMessage = "New user name " + user.s_SFirstName + " " + user.s_SLastName + "<br/>Thank you";//txtComposeMessage.Text;
            userIn.DReceivedDate = DateTime.Now;
            userIn.BIsread = false;
            userIn.Save();

            tblUserSentBox userOut = new tblUserSentBox();
            userOut.AddNew();
            userOut.FkFromUserID = Convert.ToInt32(user.GetColumn("pkUserID").ToString()); //UserID;
            userOut.FkToUserID = 10;// userCheck.PkUserID;
            userOut.SSubject = "New Activate  Account @ West Bar"; //txtSubject.Text;
            userOut.SMessage = "New user name " + user.s_SFirstName + " " + user.s_SLastName + "<br/>Thank you";//txtComposeMessage.Text;
            userOut.DSentDate = DateTime.Now;
            userOut.Save();
            //Emailing email = new Emailing();
            //email.P_ToAddress = "max@gmail.com";
            //email.P_FromAddress = ConfigurationManager.AppSettings["EmailUserName"].ToString();
            //email.P_Email_Subject = "New Activate  Account @ West Bar";
            //string strMessage = string.Empty;
            //string strCode = new Guid().ToString();
            //strMessage += "Dear admin new user name " + user.s_SFirstName + " " + user.s_SLastName+ "<br/>";
            //strMessage += "Thank you";
            //email.P_Message_Body = strMessage;
            //email.Send_Email_New();
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = " Activation Code does not match";
        }
    }
}
