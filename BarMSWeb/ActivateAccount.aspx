 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActivateAccount.aspx.cs" Inherits="ActivateAccount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
            <div class="clear_95">
             
            </div>
            <div align="center">
            <asp:Label ID="lblError" runat="server" ForeColor="red"></asp:Label></div>
            <div class="ActivateAccountbg">
                <table border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="width10" rowspan="9">
                        </td>
                        <td colspan="3" class="height70">
                       
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="height22">
                            Account Activation<br />
                            <br />
                             Enter the Activation Code sent to you in email.</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="height23" colspan="3">
                            Activation Code</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div class="textbox">
                               <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCode" ErrorMessage="*" ValidationGroup="login"></asp:RequiredFieldValidator>
                            
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="align_left">
                            
                             <asp:ImageButton ID="btnOK" runat="server" ValidationGroup="login" ImageUrl="images/btn_activateaccount.png" OnClick="btnOK_Click" />   
                                </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height:55px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <img src="images/white_arrow.png" alt="" /><a href="West_Login.aspx" class="backtologinpage_link">Back
                                to Login Page</a><a href="Register.aspx" class="register">Register</a></td>
                    </tr>
                </table>
            </div>
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
