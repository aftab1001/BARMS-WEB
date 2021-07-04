<%@ Page Language="C#" AutoEventWireup="true" CodeFile="West_Login.aspx.cs" Inherits="West_Login" %>

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
            <div class="clear_95">
            </div>
            <div class="loginbox">
                <table border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td class="width10" rowspan="8">
                        </td>
                        <td colspan="3"  class="height82" align="center">
                        <br /><br />
                        <asp:Label ID="lblError" runat="server" CssClass="Error" Visible="false" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="height22">
                            Username</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqfld1" runat="server" ControlToValidate="txtUsername" ErrorMessage="*" ValidationGroup="login"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="height15">
                        </td>
                    </tr>
                    <tr>
                        <td class="height23" colspan="3">
                            Password</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" ValidationGroup="login"></asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="height15">
                        </td>
                    </tr>
                    <tr>
                        <td class="width22">
                            <input type="checkbox" name="checkbox" id="checkbox" /></td>
                        <td class="width275">
                            Remember me</td>
                        <td class="width93">
                            
                            <asp:ImageButton ID="btnOK" runat="server" ValidationGroup="login" ImageUrl="images/signin_btn.png" OnClick="btnOK_Click" />
                            </td>
                    </tr>
                    <tr>
                        <td class="width10">
                        </td>
                        <td class="height46" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td class="width10">
                        </td>
                        <td colspan="3">
                            <a href="Forgot.aspx" class="forgot">Forgot Password</a><a href="Register.aspx" class="register">Register</a></td>
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
