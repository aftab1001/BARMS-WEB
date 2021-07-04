<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

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

    <script type="text/javascript" language="javascript" src="JavaScript/jquery-1.6.2.min.js"></script>

    <script type="text/javascript" language="javascript" src="JavaScript/jquery-ui-1.8.16.custom.min.js"></script>

    <link href="Styles/custom-theme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />

    <script src="JavaScript/ToolTip.js" type="text/javascript"></script>

    <script>
        $(function() {
            $("#datepicker").datepicker({
                showOn: "button",
                buttonImage: "images/calender.png",
                buttonImageOnly: true
            });
        });
    </script>

    <script type="text/javascript">
        function OpenFeedbackWindow(FeedBack) {
            var feedbackText;
            feedbackText = FeedBack;
            document.getElementById("divBalloon").innerHTML = feedbackText;
            ShowContent("divFeedbackWindow");
        }
        function CloseFeedBackWindow() {

            HideContent("divFeedbackWindow");
        }
    </script>

    <script language="javascript" type="text/javascript">
        function OpenDialouge() {

            //document.getElementById

        }
        function numbersCheck(txt) {
            //var lblerror = document.getElementById("ctl00_lblMsg");
            if (txt.value == "") {
                return;
            }
            var m = regIsNumber(txt.value);
            if (!m) {
                txt.value = txt.value.substr(0, txt.value.length - 1);

            }
        }
        var isWhole_re = /^\s*\d+\s*$/;
        function regIsNumber(s) {
            return String(s).search(isWhole_re) != -1


        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="divFeedbackWindow" style="display: none; position: absolute;">
        <%--<asp:Label ID="lblFeedBackWindow" runat="server"></asp:Label>--%>
        <div style="width: auto">
            <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <img src="images/pop_shape.png" class="pop_shape" alt="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="6">
                                    <img src="images/box_left_topbg.png" width="6" height="6" alt="" />
                                </td>
                                <td class="box_top_bg">
                                </td>
                                <td align="right">
                                    <img src="images/box_right_topbg.png" width="6" height="6" alt="" />
                                </td>
                            </tr>
                            <tr>
                                <td class="box_left_bg">
                                </td>
                                <td>
                                    <div id="divBalloon" style="background-color: White; max-width: 300px;">
                                    </div>
                                </td>
                                <td align="right" class="box_right_bg">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img src="images/box_left_bottombg.png" width="6" height="6" alt="" />
                                </td>
                                <td class="box_bottom_bg">
                                </td>
                                <td align="right">
                                    <img src="images/box_right_bottombg.png" width="6" height="6" alt="" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
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
                <table border="0" cellspacing="0" cellpadding="0" class="width100percent">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="width100percent">
                                <tr>
                                    <td class="width22percent align_center_top">
                                        <img src="images/no_image.gif" alt="" /><br />
                                        <br />
                                        <%--<img src="images/btn_browse.gif" alt="" style="cursor: pointer;" onclick="javascript:document.getElementById('fileupload').click();" />
                                            <img src="images/btn_browse.gif" alt="" style="cursor: pointer;" onclick="javascript:document.getElementById('fileupload').click();__dopostback('fileupload','');" />--%>
                                        <asp:FileUpload ID="fileupload" runat="server" EnableViewState="true" Style="width: 151px;" />
                                    </td>
                                    <td class="align_top width30percent">
                                        <table border="0" cellspacing="4" cellpadding="4">
                                            <tr>
                                                <td class="align_left_middle">
                                                    <div class="textbox_email">
                                                        <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqfld1" runat="server" ControlToValidate="txtUsername"
                                                            ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="align_left_middle">
                                                    <div class="textbox_password">
                                                        <asp:TextBox ID="txtPassword" Text="" runat="server" TextMode="password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                                                            ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="align_left_middle">
                                                    <div class="textbox_repassword">
                                                        <asp:TextBox ID="txtPasswordAgain" runat="server" TextMode="password"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPasswordAgain"
                                                            ErrorMessage="Enter Confirm Password" Text="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="compare1" runat="server" ControlToCompare="txtPassword"
                                                            ControlToValidate="txtPasswordAgain" ErrorMessage="*" ValidationGroup="Register"></asp:CompareValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="width4percent align_center_top">
                                        <img src="images/vertical_line.png" alt="" />
                                    </td>
                                    <td class="width44percent align_top">
                                        <table border="0" align="center" cellpadding="4" cellspacing="4">
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="align_left">
                                                    <div class="facebook">
                                                        <asp:TextBox ID="txtFacebook" runat="server" onmouseover="javascript:OpenFeedbackWindow('Please enter your facebook profile name in the text box below so that the text box should read:<br/> https://www.facebook.com/yourprofile.name')"
                                                            onmouseout="javascript:CloseFeedBackWindow();"></asp:TextBox>
                                                        <%--<asp:RegularExpressionValidator ID="rev" runat="server" Display="Dynamic" ControlToValidate="txtFacebook" 
                                                    ErrorMessage="Enter Valid facebook Link" Text="*" ValidationGroup="Register" EnableClientScript="true" SetFocusOnError="true" 
                                                    ValidationExpression="(?:http:\/\/)?(?:www.)?facebook.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[?\w\-]*\/)?(?:profile.php\?id=(?=\d.*))?([\w\-]*)?"></asp:RegularExpressionValidator>--%>
                                                    </div>
                                                </td>
                                                <td class="align_center">
                                                    <div class="skype">
                                                        <asp:TextBox ID="txtSkype" runat="server" onmouseover="javascript:OpenFeedbackWindow('Please enter your Skype user name in the text box below')"
                                                            onmouseout="javascript:CloseFeedBackWindow();"></asp:TextBox>
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ControlToValidate="txtSkype" 
                                                    ErrorMessage="Enter Valid skype Link" Text="*" ValidationGroup="Register" EnableClientScript="true" SetFocusOnError="true" 
                                                    ValidationExpression="(?:http:\/\/)?(?:www.)?facebook.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[?\w\-]*\/)?(?:profile.php\?id=(?=\d.*))?([\w\-]*)?"></asp:RegularExpressionValidator>--%>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="align_left">
                                                    <div class="twitter">
                                                        <asp:TextBox ID="txtTwitter" runat="server" onmouseover="javascript:OpenFeedbackWindow('Please enter your twitter account name in the text box below so that the text box should read:<br/> https://twitter.com/#!/youraccountname')"
                                                            onmouseout="javascript:CloseFeedBackWindow();"></asp:TextBox>
                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ControlToValidate="txtTwitter" 
                                                    ErrorMessage="Enter Valid twitter Link" Text="*" ValidationGroup="Register" EnableClientScript="true" SetFocusOnError="true" 
                                                    ValidationExpression="(?:http:\/\/)?(?:www.)?facebook.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[?\w\-]*\/)?(?:profile.php\?id=(?=\d.*))?([\w\-]*)?"></asp:RegularExpressionValidator>--%>
                                                    </div>
                                                </td>
                                                <td class="align_center">
                                                    <div class="messenger">
                                                        <asp:TextBox ID="txtMessenger" runat="server" onmouseover="javascript:OpenFeedbackWindow('Please enter your MSN Windows Live ID in the text box below')"
                                                            onmouseout="javascript:CloseFeedBackWindow();"></asp:TextBox>
                                                        <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ControlToValidate="txtMessenger" 
                                                    ErrorMessage="Enter Valid messenger Link" Text="*" ValidationGroup="Register" EnableClientScript="true" SetFocusOnError="true" 
                                                    ValidationExpression="(?:http:\/\/)?(?:www.)?facebook.com\/(?:(?:\w)*#!\/)?(?:pages\/)?(?:[?\w\-]*\/)?(?:profile.php\?id=(?=\d.*))?([\w\-]*)?"></asp:RegularExpressionValidator>--%>
                                                        <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server"  ShowMessageBox="true" ShowSummary="false" ValidationGroup="Register" 
                                                 HeaderText="Social Links Validation:" DisplayMode="BulletList" EnableClientScript="true" />--%>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="height30">
                            <img src="images/horizontal_line.png" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="width100percent">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="3" cellspacing="3" class="width100percent">
                                            <tr>
                                                <td class="label_bold">
                                                    Email
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:TextBox ID="txtEmail" runat="server" style="width:204px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
                                                            ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label_bold">
                                                    Mobile Phone
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:TextBox ID="txtMobile" runat="server"></asp:TextBox></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="width22">
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="3" cellspacing="3" class="width100percent">
                                            <tr>
                                                <td class="label_bold">
                                                    <span class="width15percent label_bold">Department</span>
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label_bold">
                                                    Speciality
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:DropDownList ID="ddlSpeciality" runat="server" AutoPostBack="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="height30">
                            <img src="images/horizontal_line.png" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" class="width100percent">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="3" cellspacing="3" class="width100percent">
                                            <tr>
                                                <td class="label_bold" width="29%">
                                                    First Name
                                                </td>
                                                <td width="71%">
                                                    <div class="textbox">
                                                        &nbsp;<asp:TextBox ID="txtFirstName" runat="server"  style="width:204px;"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFirstName"
                                                            ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label_bold">
                                                    Last Name
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label_bold">
                                                    Gender
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:DropDownList ID="ddlGender" runat="server" AutoPostBack="false">
                                                            <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label_bold">
                                                    Birth Date
                                                </td>
                                                <td>
                                                    <table border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td>
                                                                <div class="textbox_small">
                                                                    <asp:TextBox ID="txtDate" runat="server" onkeyup="javascript:numbersCheck(this);"  style="width:60px;"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDate"
                                                                        ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="textbox_small">
                                                                    <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="false">
                                                                        <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                                                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                                                        <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                                                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                                                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                                                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                                                        <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                                                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                                                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="textbox_small">
                                                                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="false">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div class="demo textbox_date" style="display: none;">
                                                        <%--<input id="datepicker" type="text" />--%>
                                                        <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label_bold">
                                                    Nationality
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:DropDownList ID="ddlNationality" runat="server" AutoPostBack="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="width22">
                                    </td>
                                    <td>
                                        <table border="0" cellpadding="3" cellspacing="3" class="width100percent">
                                            <tr>
                                                <td class="label_bold" width="26%">
                                                    Address
                                                </td>
                                                <td width="72%">
                                                    <div class="textbox">
                                                        &nbsp;<asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label_bold">
                                                    Town
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:TextBox ID="txtTown" runat="server"></asp:TextBox></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label_bold">
                                                    Post Code
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:TextBox ID="txtPostcode" runat="server"></asp:TextBox></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label_bold">
                                                    Region
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:TextBox ID="txtRegion" runat="server"></asp:TextBox></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="label_bold">
                                                    Country
                                                </td>
                                                <td>
                                                    <div class="textbox">
                                                        &nbsp;<asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="false">
                                                        </asp:DropDownList>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div class="clear">
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="bottom">
            </div>
            <div class="clear">
            </div>
            <div class="bottom_graybox_left">
                <img src="images/back_arrow.png" alt="" /><a href="West_Login.aspx">Back to Login Page</a></div>
            <div class="bottom_graybox_right">
                <asp:ImageButton ID="btnOK" runat="server" ImageUrl="images/btn_register.gif" OnClick="btnOK_Click"
                    ValidationGroup="Register" />
            </div>
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
