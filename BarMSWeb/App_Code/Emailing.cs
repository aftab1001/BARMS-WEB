using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Configuration;

 public class Emailing
    {
            #region EmailDataMembers
        private string _Email_Subject;
        private string _ToAddress;
        private string _FromAddress;
        private string _CCAddress;
        private string _BCCAddress;
        private string _Message_Body;

        private string _userName;
        private string _Pwd;

        #endregion


        #region Public Properties

        public string P_Email_Subject
        {
            get { return this._Email_Subject; }
            set { this._Email_Subject = value; }
        }


        public string P_ToAddress
        {
            get { return this._ToAddress; }
            set { this._ToAddress = value; }
        }

     public string P_CCAddress
     {
         get { return this._CCAddress; }
         set { this._CCAddress = value; }
     }

     public string P_BCCAddress
     {
         get { return this._BCCAddress; }
         set { this._BCCAddress = value; }
     }

        public string P_FromAddress
        {
            get { return this._FromAddress; }
            set { this._FromAddress = value; }

        }

        public string P_Message_Body
        {
            get { return this._Message_Body; }
            set { this._Message_Body = value; }
        }

        #endregion


        public Emailing()
        {

            this._userName = System.Configuration.ConfigurationSettings.AppSettings["EmailUserName"];
            this._Pwd = System.Configuration.ConfigurationSettings.AppSettings["EmailPassword"];
        }

        public bool Send_Email_New()
        {
            System.Net.Mail.MailMessage mymail = new System.Net.Mail.MailMessage(this._FromAddress, this._ToAddress, this._Email_Subject, this._Message_Body);
            try
            {
                mymail.IsBodyHtml = true;
                System.Net.Mail.SmtpClient SmtpClnt = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["smtp"], Convert.ToInt32(ConfigurationManager.AppSettings["smtpPortNo"].ToString()));
                SmtpClnt.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpClnt.UseDefaultCredentials = false;
                //SmtpClnt.Credentials = new System.Net.NetworkCredential(this._userName, this._Pwd);
                SmtpClnt.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpEmail"].ToString(), ConfigurationManager.AppSettings["smtpPassword"].ToString());

                SmtpClnt.Send(mymail);

                return true;
            }

            catch (Exception ex)
            {
               // throw ex;
                return false;
            }
        }

        public bool Send_Email()
        {
            try
            {
           // MailMessage objMail = new  MailMessage(this._userName, this._ToAddress);
            //System.Net.Mail.MailMessage mymail = new System.Net.Mail.MailMessage(this._FromAddress, this._ToAddress, this._Email_Subject, this._Message_Body);
            System.Net.Mail.MailMessage mymail = new System.Net.Mail.MailMessage();
            
            MailAddress from = new MailAddress(this._FromAddress);

            mymail.To.Add(this._ToAddress);
            if (this._CCAddress != null)
            {
                if (this._CCAddress != "")
                {
                    mymail.CC.Add(this._CCAddress);
                }
            }
            mymail.From = from;
            mymail.Subject = this._Email_Subject;
            mymail.Body = this._Message_Body;
            
           
               
                //objMail.Body = (new System.Net.Mail.MailMessage).IsBodyHtml;
                mymail.IsBodyHtml = true;
                //mymail.CC
                //objMail.Subject = this._Email_Subject;
                //objMail.Body = this._Message_Body;
                //System.Net.NetworkCredential mailAuthentication = new System.Net.NetworkCredential("dotnetguts@gmail.com", "myPassword");

                              
                System.Net.Mail.SmtpClient SmtpClnt = new System.Net.Mail.SmtpClient(System.Configuration.ConfigurationSettings.AppSettings.Get("Smtp"),25);
                SmtpClnt.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpClnt.UseDefaultCredentials = false;
                SmtpClnt.Credentials = new System.Net.NetworkCredential(this._userName, this._Pwd);
                SmtpClnt.Send(mymail);
                return true;
               // return "s";
            }
            catch (Exception ex)
            {
                //return ex.Message;
                return false;
            }

        }

        public bool Send_Email(string file)
        {

            System.Net.Mail.MailMessage objMail = new System.Net.Mail.MailMessage();
            MailAddress from = new MailAddress(this._FromAddress);

            objMail.From = from;
            objMail.To.Add(this._ToAddress);
            if (this._CCAddress != "")
            {
                objMail.CC.Add(this._CCAddress);
            }
            objMail.Subject = this._Email_Subject;
            objMail.Body = this._Message_Body;

            try
            {
               
               // //objMail.Body = (new System.Net.Mail.MailMessage).IsBodyHtml;
                objMail.IsBodyHtml = true;

               // //objMail.Subject = this._Email_Subject;
               // //objMail.Body = this._Message_Body;
                
                Attachment attachFile = new Attachment(file);
                objMail.Attachments.Add(attachFile);

                 System.Net.Mail.SmtpClient SmtpClnt = new System.Net.Mail.SmtpClient(System.Configuration.ConfigurationSettings.AppSettings.Get("Smtp"), 25);
                 SmtpClnt.DeliveryMethod = SmtpDeliveryMethod.Network;
                 SmtpClnt.UseDefaultCredentials = false;
                 SmtpClnt.Credentials = new System.Net.NetworkCredential(this._userName, this._Pwd);
                 SmtpClnt.Send(objMail);
               
                 attachFile.ContentStream.Close();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
   
    }