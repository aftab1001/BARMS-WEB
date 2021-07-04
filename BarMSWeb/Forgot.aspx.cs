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

public partial class Forgot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Visible = false;
    }
    protected void btnOK_Click(object sender, ImageClickEventArgs e)
    {
        tblUsers user = new tblUsers();
        user.UserExist(txtEmail.Text);
        if (user.RowCount > 0)
        {
            Emailing email = new Emailing();
            email.P_ToAddress = txtEmail.Text;
            email.P_FromAddress = "noreply@West.com";
            email.P_Email_Subject = "Login information @ West Bar";
            string strMessage = string.Empty;
            strMessage += " Dear " + user.s_SFirstName + " " + user.s_SLastName + "<br/>";
            strMessage += " your username is " + user.s_SUsername + " and password is  " + user.s_SPassword + "<br/>";
            strMessage += " Thank you"; 
            email.P_Message_Body = strMessage;
            email.Send_Email();
            lblError.Visible = true;
            lblError.Text = " Email is sent. Please check your email";
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = " Username does not exist";
        }
    }
}
