<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Expired.aspx.cs" Inherits="Expired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- ************************************************************************ !-->
    <!-- *****                                                              ***** !-->
    <!-- *****       ¤ Designed and Developed by  LEADconcept               ***** !-->
    <!-- *****               http://www.leadconcept.com                     ***** !-->
    <!-- *****                                                              ***** !-->
    <!-- ************************************************************************ !-->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>--:: WEST ::--</title>
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <div class="logo_login">
                <a href="#"></a>
            </div>
            <div class="clear_75">
            </div>
            <div class="content_area">
                <h1>
                    Account Inactive or Expired</h1>
                <div class="clear_30">
                </div>
                <p>
                    We're sorry, it seems like your account is not yet approved or it has expired!<br />
                    <br />
                    This could be due to a series of reasons!<br />
                    <br />
                    The most common causes for this error are listed below:<br />
                    <br />
                    <ul>
                        <li>Your account has just been created and an admin has not yet approved it. <span
                            class="label_red">(*)</span></li>
                        <li>Your were working only for a season and that season is now over. <span class="label_red">
                            (**)</span></li>
                        <li>An administrator has chosen to restrict your access rights.</li>
                    </ul>
                    <br />
                    <br />
                    <span class="label_red">(*)</span> Before you are able to access your account here,
                    an administrator has to approve it verifying that you are an active worker on this
                    company.<br />
                    <br />
                    <span class="label_red">(**)</span> Please keep in mind that all User Accounts are
                    kept active only for the period that you are an active worker in this company.<br />
                    <br />
                    Once your contrat expires, so does your account here! It will be reactivated once
                    you start working again for the company.<br />
                    <br />
                    There's no need to register all over if you have registered before!<br />
                    <br />
                    If you feel that you received this error by mistake please feel free to use the
                    form below to contact an administrator.</p>
                <div class="clear_10">
                </div>
                <div class="dropdown">
                    <select name="select2" id="select2">
                        <option>Please select department</option>
                    </select>
                </div>
                <div class="clear_10">
                </div>
                <div class="textarea">
                    <textarea name="" cols="" rows=""></textarea>
                </div>
                <div class="clear_10">
                </div>
                <div class="backtologinpage_new">
                    <img src="images/back_arrow.png" alt="" /><a href="West_Login.aspx">Back to Login Page</a><span>|</span>
                    <a href="Register.aspx">Register</a></div>
                <input name="" type="image" class="btn_send" src="images/btn_send.gif" /></div>
            <div class="clear">
            </div>
        </div>
        <div class="clear_75">
        </div>
        <div class="footer_main">
            <div class="footer">
                Copyright © 2011 West. All rights reserved.<br />
                <span>Designed & Developed by <a href="http://www.leadconcept.com" target="_blank">LEADconcept</a></span></div>
        </div>
    </form>
</body>
</html>
