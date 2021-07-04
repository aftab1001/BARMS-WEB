<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoEmail.aspx.cs" Inherits="AutoEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="logo_login">
            <a href="#"></a>
        </div>
        <div class="clear_30">
        </div>
        <div align="center">
            <asp:Label ID="lblError" runat="server" Text="Error Occoured" Visible="false"></asp:Label>
        </div>
        <div class="graybox_register">
            <div class="top">
            </div>
            <div class="clear">
            </div>
            <div class="centermiddle">
                <table style="border:1px solid #e8e8e8" align="center">
                    <tr>
                        <td align="left" class="email_header">
                            Week 4 / 2012 ( Sunday 22/01/2012 till Saturday 28/01/2012 )
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="email_body" valign="top">
                            <div style="padding-top: 10px;">
                                <span style="font-weight: bold; margin-top: 1px;">Dear berry jhon.</span>
                                <br /><br />
                                <span>The Department’s Account Manager has marked you as
                                    “paid off” for the given week.<br />
                                    Please check your “My Payments” page and sign up for your payment! </span>
                            </div>
                        </td>
                    </tr>
                </table>
                <div>
                    <table>
                        <tr>
                            <td align="center" class="email_header">
                                Week 4 / 2012 ( Sunday 22/01/2012 till Saturday 28/01/2012 )
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="email_body" valign="top">
                                <div style="padding-top: 10px;">
                                    <span style="font-weight: bold; margin-top: 1px;">Dear berry jhon.</span><br />
                                    <span style="padding-left: 50px;">The Department’s Account Manager has marked you as
                                        “paid off” for the given week.<br />
                                        Please check your “My Payments” page and sign up for your payment! </span>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="bottom">
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="clear_75">
    </div>
    </form>
</body>
</html>
