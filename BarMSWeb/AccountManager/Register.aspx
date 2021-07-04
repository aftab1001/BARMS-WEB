<%@ Page Language="C#" MasterPageFile="~/MasterPages/AccountMaster.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="AccountManager_Register" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- ************************************************************************ !-->
    <!-- *****                                                              ***** !-->
    <!-- *****       ¤ Designed and Developed by  LEADconcept               ***** !-->
    <!-- *****               http://www.leadconcept.com                     ***** !-->
    <!-- *****                                                              ***** !-->
    <!-- ************************************************************************ !-->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>--:: WEST ::--</title>
    <%--<link href="../Styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="../JavaScript/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="JavaScript/ToolTip.js" type="text/javascript"></script>

    <script>
        $(function() {
            $("#datepicker").datepicker({
                showOn: "button",
                buttonImage: "../images/calender.png",
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

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

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

    <div id="divFeedbackWindow" style="display: none; position: absolute;">
        <%--<asp:Label ID="lblFeedBackWindow" runat="server"></asp:Label>--%>
        <div style="width: auto">
            <table border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        <img src="../images/pop_shape.png" class="pop_shape" alt="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="6">
                                    <img src="../images/box_left_topbg.png" width="6" height="6" alt="" />
                                </td>
                                <td class="box_top_bg">
                                </td>
                                <td align="right">
                                    <img src="../images/box_right_topbg.png" width="6" height="6" alt="" />
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
                                    <img src="../images/box_left_bottombg.png" width="6" height="6" alt="" />
                                </td>
                                <td class="box_bottom_bg">
                                </td>
                                <td align="right">
                                    <img src="../images/box_right_bottombg.png" width="6" height="6" alt="" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <asp:MultiView ID="mvUser" runat="server" ActiveViewIndex="0">
            <asp:View ID="vSpecialUsers" runat="server">
                <table border="0" cellpadding="3" cellspacing="0">
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgBtnAdd" runat="server" ImageUrl="../Images/addspecialuser_btn.png"
                                OnClick="imgBtnAdd_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="whitebox_topleftcorner">
                                    </td>
                                    <td class="whitebox_top_bg">
                                    </td>
                                    <td class="whitebox_toprightcorner">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="whitebox_centerleftbg">
                                    </td>
                                    <td class="whitebox_centermiddlebg">
                                        <div class="rounded_box">
                                            Special Users
                                            <div class="clear_10">
                                            </div>
                                            <asp:GridView ID="grdUsers" runat="server" RowStyle-CssClass="rowstyle" HeaderStyle-CssClass="header_row"
                                                AlternatingRowStyle-CssClass="alternate_row" GridLines="None" AutoGenerateColumns="false"
                                                OnRowDataBound="grdUsers_RowDataBound" OnRowCommand="grdUsers_RowCommand" EmptyDataText="No Special User Exist"
                                                Style="width: 867px;">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text="<%#Bind('FullName') %>" Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lnkName" runat="server" Text="<%#Bind('FullName') %>" CommandName="edt"
                                                                CommandArgument="<%#Bind('pkuserid') %>"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="sSpecialityName" HeaderText="Specialty" />
                                                    <asp:TemplateField HeaderText="Active">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnActive" runat="server" ImageUrl="../Images/close.png"
                                                                CommandName="active" CommandArgument="<%#Bind('pkuserid') %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Salary" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnSalary" runat="server" ImageUrl="~/Images/Dollar.png"
                                                                CommandName="Salary" CommandArgument='<%# Bind("pkUserID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bonus" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnBonus" runat="server" ImageUrl="~/Images/icon_b_greypng.png"
                                                                CommandName="Bonus" CommandArgument='<%# Bind("pkUserID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contract" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgBtnContract" runat="server" ImageUrl="~/Images/icon_c_red.png"
                                                                OnClientClick="return false;" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Message">
                                                        <ItemTemplate>
                                                            <table border="0" cellspacing="0" cellpadding="0" style="width: 100px;">
                                                                <tr>
                                                                    <%--<td class="align_right">
                                    <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkUserID") %>'
                                        ImageUrl="../images/close.png" ToolTip="Delete" />
                                </td>--%>
                                                                    <td class="align_left" style="width: 200px;">
                                                                        <asp:ImageButton ID="imgMessage" runat="server" CommandName="Message" CommandArgument='<%# Bind("pkUserID") %>'
                                                                            ImageUrl="~/Images/Message.png" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="3%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                    <td class="whitebox_centerrightbg">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="whitebox_bottomleftcorner">
                                    </td>
                                    <td class="whitebox_bottom_bg">
                                    </td>
                                    <td class="whitebox_bottomrightcorner">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vNewUser" runat="server">
                <table border="0" cellpadding="5" cellspacing="">
                    <tr>
                        <td align="center">
                            <img src="../images/no_image.gif" alt="" /><br />
                            <br />
                            <asp:FileUpload ID="fileupload" runat="server" EnableViewState="true" Style="width: 151px;" />
                        </td>
                        <td colspan="3">
                            <table border="0" cellspacing="4" cellpadding="4">
                                <tr>
                                    <td>
                                        Username:
                                    </td>
                                    <td>
                                        <div class="textbox">
                                            <asp:TextBox ID="txtUsername" runat="server" Style="width: 207px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqfld1" runat="server" ControlToValidate="txtUsername"
                                                ErrorMessage="*" ValidationGroup="Reg"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Password:
                                    </td>
                                    <td>
                                        <div class="textbox">
                                            <asp:TextBox ID="txtPassword" Text="" runat="server" TextMode="password" Style="width: 207px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                                ErrorMessage="*" ValidationGroup="Reg"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Re-Password:
                                    </td>
                                    <td style="width: 371px;">
                                        <div class="textbox" style="float: left;">
                                            <asp:TextBox ID="txtPasswordAgain" runat="server" TextMode="password" Style="width: 203px;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPasswordAgain"
                                                ErrorMessage="Enter Confirm Password" Text="*" ValidationGroup="Reg"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="compare1" runat="server" ControlToCompare="txtPassword"
                                                ControlToValidate="txtPasswordAgain" ErrorMessage="*" ValidationGroup="Reg"></asp:CompareValidator>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="height30" colspan="4">
                            <img src="../images/horizontal_line.png" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td style="display: none;">
                            Email
                        </td>
                        <td style="display: none;">
                            <div class="textbox">
                                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></div>
                        </td>
                        <td>
                            First Name
                        </td>
                        <td>
                            <div class="textbox">
                                <asp:TextBox ID="txtFirstName" runat="server" Style="float: left; margin-left: 6px;
                                    width: 206px;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="txtFirstName"
                                    ValidationGroup="Reg" Display="Dynamic" ErrorMessage="*" Style="top: 7px; position: relative;"></asp:RequiredFieldValidator>
                            </div>
                        </td>
                        <td style="display: none;">
                            Department
                        </td>
                        <td style="display: none;">
                            <div class="textbox">
                                <asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="display: none;">
                            Mobile Phone
                        </td>
                        <td style="display: none;">
                            <div class="textbox">
                                <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox></div>
                        </td>
                        <td>
                            Last Name
                        </td>
                        <td>
                            <div class="textbox">
                                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></div>
                        </td>
                        <td>
                            Speciality
                        </td>
                        <td>
                            <div class="textbox">
                                <asp:DropDownList ID="ddlSpeciality" runat="server" >
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td colspan="4" class="height30">
                            <img src="../images/horizontal_line.png" alt="" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Address
                        </td>
                        <td>
                            <div class="textbox">
                                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></div>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Town
                        </td>
                        <td>
                            <div class="textbox">
                                <asp:TextBox ID="txtTown" runat="server"></asp:TextBox></div>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Gender
                        </td>
                        <td>
                            <div class="textbox">
                                <asp:DropDownList ID="ddlGender" runat="server" AutoPostBack="false">
                                    <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            Post Code
                        </td>
                        <td>
                            <div class="textbox">
                                <asp:TextBox ID="txtPostcode" runat="server"></asp:TextBox></div>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Birth Date
                        </td>
                        <td>
                            <div class="textbox_small" style="float: left;">
                                <asp:TextBox ID="txtDate" runat="server" Style="float: left;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                                    ValidationGroup="Register" Display="Dynamic" ErrorMessage="*" Style="top: 7px;
                                    position: relative;"></asp:RequiredFieldValidator>
                            </div>
                            <div class="textbox_small" style="float: left;">
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
                            <div class="textbox_small" style="float: left;">
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            Region
                        </td>
                        <td>
                            <div class="textbox">
                                <asp:TextBox ID="txtRegion" runat="server"></asp:TextBox></div>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            Nationality
                        </td>
                        <td>
                            <div class="textbox">
                                <asp:DropDownList ID="ddlNationality" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            Country
                        </td>
                        <td>
                            <div class="textbox">
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="right">
                            <asp:ImageButton ID="btnOK" runat="server" ImageUrl="../images/register_btn.png"
                                OnClick="btnOK_Click" ValidationGroup="Reg" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vUpdate" runat="server">
                <div class="top">
                </div>
                <div class="clear">
                </div>
                <div class="centermiddle">
                    <table border="0" cellspacing="0" cellpadding="0" width="100%" align="center">
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" Text="Updated successfully!" Visible="false"
                                    ForeColor="Green" Style="font-weight: bold; font-size: 15px; margin-top: -24px;
                                    padding-left: 288px; position: absolute;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td class="width22percent align_center_top">
                                            <img id="userImage" runat="server" width="149" src="../images/no_image.gif" alt="" /><br />
                                            <br />
                                            <%--<a id="ankUpload" href="javascript:document.getElementById('ctl00_ContentPlaceHolder1_fpUploadPic').click();">
                                        <img src="../images/btn_browse.gif" alt="" /></a>--%>
                                            <asp:FileUpload ID="fpUploadPic" runat="server" />
                                        </td>
                                        <td class="align_top width30percent">
                                            <table border="0" cellpadding="4" cellspacing="4">
                                                <tr>
                                                    <td class="label_bold align_left_middle">
                                                        User Name
                                                    </td>
                                                    <td class="align_left_middle">
                                                        <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="align_left_middle" colspan="2">
                                                        <div class="old_password">
                                                            <asp:TextBox ID="txtOldPassword" Text="" runat="server" TextMode="password" Style="margin-right: -3px;"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOldPassword"
                                                                ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="align_left_middle" colspan="2">
                                                        <div class="new_password">
                                                            <asp:TextBox ID="txtNewPassword" Text="" runat="server" TextMode="password" Style="margin-right: -3px;"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNewPassword"
                                                                ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="align_left_middle" colspan="2">
                                                        <div class="textbox_repassword">
                                                            <asp:TextBox ID="txtReNewPassword" Text="" runat="server" TextMode="password" Style="margin-right: -3px;"></asp:TextBox>
                                                            <asp:CompareValidator ID="compre" runat="server" ControlToValidate="txtReNewPassword"
                                                                ControlToCompare="txtNewPassword" ErrorMessage="*"></asp:CompareValidator>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="width4percent align_center_top" style="display: none;">
                                            <img src="../images/vertical_line.png" alt="" />
                                        </td>
                                        <td class="width44percent align_top" style="display: none;">
                                            <table align="center" border="0" cellpadding="4" cellspacing="4">
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
                                <img src="../images/horizontal_line.png" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="align_top">
                                            <asp:Panel ID="pnlEmail" runat="server" DefaultButton="btnEmail">
                                                <asp:UpdatePanel ID="upnlEmail" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td class="whitebox_topleftcorner">
                                                                            </td>
                                                                            <td class="whitebox_top_bg">
                                                                            </td>
                                                                            <td class="whitebox_toprightcorner">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="whitebox_centerleftbg">
                                                                            </td>
                                                                            <td class="whitebox_centermiddlebg">
                                                                                <div class="rounded_box">
                                                                                    Email Address
                                                                                    <div class="clear_10">
                                                                                    </div>
                                                                                    <asp:GridView ID="grdEmails" AllowSorting="True" AllowPaging="True" runat="server"
                                                                                        AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                        RowStyle-CssClass="rowstyle" Width="100%" OnRowCommand="grdEmails_RowCommand"
                                                                                        OnRowDataBound="grdEmails_RowDataBound" CellPadding="0" BorderStyle="None" BorderWidth="0"
                                                                                        GridLines="None">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                                        <tr>
                                                                                                            <td class="align_left">
                                                                                                                <asp:Label ID="lblemail" runat="server"></asp:Label>
                                                                                                            </td>
                                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                                <asp:ImageButton ID="imgbtnSetActiveEmail" runat="server" CommandName="main" CommandArgument='<%# Bind("pkEmailID") %>'
                                                                                                                    ImageUrl="~/Images/activate_icon.gif" ToolTip="Non-Active" />
                                                                                                            </td>
                                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkEmailID") %>'
                                                                                                                    ImageUrl="../images/close.png" ToolTip="Delete" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="3%" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                            <td class="whitebox_centerrightbg">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="whitebox_bottomleftcorner">
                                                                            </td>
                                                                            <td class="whitebox_bottom_bg">
                                                                            </td>
                                                                            <td class="whitebox_bottomrightcorner">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" class="height10">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="width45percent">
                                                                    <div class="textbox204">
                                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td class="width55percent">
                                                                    <asp:ImageButton ID="btnEmail" runat="server" ImageUrl="../images/btn_addanotheremail.gif"
                                                                        OnClick="btnEmail_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                        <td class="width30">
                                        </td>
                                        <td class="align_top">
                                            <asp:Panel ID="pnlMobile" runat="server" DefaultButton="btnMobile">
                                                <asp:UpdatePanel ID="upnlMobile" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td class="whitebox_topleftcorner">
                                                                            </td>
                                                                            <td class="whitebox_top_bg">
                                                                            </td>
                                                                            <td class="whitebox_toprightcorner">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="whitebox_centerleftbg">
                                                                            </td>
                                                                            <td class="whitebox_centermiddlebg">
                                                                                <div class="rounded_box">
                                                                                    Mobile Phone
                                                                                    <div class="clear_10">
                                                                                    </div>
                                                                                    <asp:GridView ID="grdMobile" AllowSorting="True" AllowPaging="True" runat="server"
                                                                                        AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                        RowStyle-CssClass="rowstyle" Width="100%" OnRowCommand="grdMobile_RowCommand"
                                                                                        OnRowDataBound="grdMobile_RowDataBound" CellPadding="0" BorderStyle="None" BorderWidth="0"
                                                                                        GridLines="None">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                                        <tr>
                                                                                                            <td class="align_left">
                                                                                                                <asp:Label ID="lblMobile" runat="server"></asp:Label>
                                                                                                            </td>
                                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                                <asp:ImageButton ID="imgBtnActiveMobile" runat="server" CommandName="main" CommandArgument='<%# Bind("pkPhineID") %>'
                                                                                                                    ImageUrl="~/Images/activate_icon.gif" ToolTip="Non-Active" />
                                                                                                            </td>
                                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkPhineID") %>'
                                                                                                                    ImageUrl="../images/close.png" ToolTip="Delete" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="3%" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                            <td class="whitebox_centerrightbg">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="whitebox_bottomleftcorner">
                                                                            </td>
                                                                            <td class="whitebox_bottom_bg">
                                                                            </td>
                                                                            <td class="whitebox_bottomrightcorner">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" class="height10">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="width45percent">
                                                                    <div class="textbox204">
                                                                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td class="width55percent">
                                                                    <asp:ImageButton ID="btnMobile" runat="server" ImageUrl="../images/btn_anotherphone.gif"
                                                                        OnClick="btnMobile_Click1" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="height30">
                                <img src="../images/horizontal_line.png" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="align_top" width="446" style="display: none;">
                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                <%-- <tr>
                                    <td colspan="2">
                                        <div class="rounded_box">
                                            Department
                                            <div class="clear_10">
                                            </div>
                                            <table border="0" cellspacing="0" cellpadding="0" class="table_border">
                                                <tr class="rowstyle">
                                                    <td class="align_left">
                                                        West bar, Kos island, Greece</td>
                                                    <td class="align_right">
                                                        <img src="../images/close.png" alt="" /></td>
                                                </tr>
                                                <tr class="alternate_row">
                                                    <td class="align_left">
                                                        West bar, Malmo, Sweden</td>
                                                    <td class="align_right">
                                                        <img src="../images/close.png" alt="" /></td>
                                                </tr>
                                                <tr class="rowstyle">
                                                    <td class="align_left">
                                                        West bar, Kos island, Greece</td>
                                                    <td class="align_right">
                                                        <img src="../images/close.png" alt="" /></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>--%>
                                                <tr>
                                                    <td colspan="2" class="height10">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="width45percent" style="display: none;">
                                                        <div class="textbox204">
                                                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td class="width55percent">
                                                        <asp:ImageButton ID="btnSendRequest" runat="server" ImageUrl="../images/btn_sendrequest.png"
                                                            OnClick="btnSendRequest_Click" />
                                                    </td>
                                                </tr>
                                                <tr height="10">
                                                    <td colspan="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="lblRequest" runat="server" CssClass="Error" Visible="false" Text="Request Is Sent to Department Manager"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="width30" style="display: none;">
                                        </td>
                                        <td class="align_top">
                                            <asp:Panel ID="pnlSpecialty" runat="server" DefaultButton="btnSpeciality">
                                                <asp:UpdatePanel ID="upnlSpecialty" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table border="0" cellspacing="0" cellpadding="0" width="48%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td class="whitebox_topleftcorner">
                                                                            </td>
                                                                            <td class="whitebox_top_bg">
                                                                            </td>
                                                                            <td class="whitebox_toprightcorner">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="whitebox_centerleftbg">
                                                                            </td>
                                                                            <td class="whitebox_centermiddlebg">
                                                                                <div class="rounded_box">
                                                                                    Specialty
                                                                                    <div class="clear_10">
                                                                                    </div>
                                                                                    <asp:GridView ID="grdSpeciality" AllowSorting="True" AllowPaging="True" runat="server"
                                                                                        AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                        RowStyle-CssClass="rowstyle" Width="100%" OnRowCommand="grdSpeciality_RowCommand"
                                                                                        OnRowDataBound="grdSpeciality_RowDataBound" CellPadding="0" BorderStyle="None"
                                                                                        BorderWidth="0" GridLines="None">
                                                                                        <Columns>
                                                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                <ItemTemplate>
                                                                                                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                                                                                                        <tr>
                                                                                                            <td class="align_left">
                                                                                                                <asp:Label ID="lblSpeciality" runat="server"></asp:Label>
                                                                                                            </td>
                                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                                <asp:ImageButton ID="imgBtnActiveSpeciality" runat="server" CommandName="main" CommandArgument='<%# Bind("pkUserSpecialityID") %>'
                                                                                                                    ImageUrl="~/Images/activate_icon.gif" ToolTip="Non-Active" />
                                                                                                            </td>
                                                                                                            <td class="align_right" style="width: 20px;">
                                                                                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkUserSpecialityID") %>'
                                                                                                                    ImageUrl="../images/close.png" ToolTip="Delete" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle Width="3%" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </div>
                                                                            </td>
                                                                            <td class="whitebox_centerrightbg">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="whitebox_bottomleftcorner">
                                                                            </td>
                                                                            <td class="whitebox_bottom_bg">
                                                                            </td>
                                                                            <td class="whitebox_bottomrightcorner">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" class="height10">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="width45percent" style="width: 3%;">
                                                                    <div class="textbox204">
                                                                        <asp:DropDownList ID="ddlSpecialtyUpdate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSpecialtyUpdate_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                                <td class="width55percent">
                                                                    <asp:ImageButton ID="btnSpeciality" runat="server" ImageUrl="../images/btn_addanotherspeciality.gif"
                                                                        OnClick="btnSpeciality_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="height30">
                                <img src="../images/horizontal_line.png" alt="" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellspacing="2" cellpadding="2" width="100%">
                                    <tr>
                                        <td>
                                            <table border="0" cellspacing="2" cellpadding="2" width="100%">
                                                <tr>
                                                    <td>
                                                        <table border="0" cellspacing="4" cellpadding="3" width="100%">
                                                            <tr>
                                                                <td class="label_bold" style="width: 113px;">
                                                                    First Name
                                                                </td>
                                                                <td style="width: 327px;">
                                                                    <div class="textbox204">
                                                                        <asp:TextBox ID="txtFirstNameUpdate" runat="server"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtFirstNameUpdate"
                                                                            ValidationGroup="reqb" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </td>
                                                                <td class="label_bold" style="width: 100px;">
                                                                    Birth Date
                                                                </td>
                                                                <td>
                                                                    <table border="0" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="textbox_small">
                                                                                    <asp:TextBox ID="txtBirthDateUpdate" runat="server" onkeyup="javascript:numbersCheck(this);"
                                                                                        Style="width: 57px;"></asp:TextBox>
                                                                                    <%--<asp:RequiredFieldValidator ID="reqbir" runat="server" ValidationGroup="reqb" ControlToValidate="txtBirthDateUpdate"
                                                                                        ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>--%></div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="textbox_small">
                                                                                    <asp:DropDownList ID="ddlMonthUpdate" runat="server" AutoPostBack="false">
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
                                                                                    <asp:DropDownList ID="ddlYearUpdate" runat="server" AutoPostBack="false">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label_bold">
                                                                    Last Name
                                                                </td>
                                                                <td>
                                                                    <div class="textbox204">
                                                                        <asp:TextBox ID="txtLastNameUpdate" runat="server"></asp:TextBox>
                                                                    </div>
                                                                </td>
                                                                <td class="label_bold">
                                                                    Gender
                                                                </td>
                                                                <td>
                                                                    <div class="textbox204">
                                                                        <asp:DropDownList ID="ddlGenderUpdate" runat="server" AutoPostBack="false">
                                                                            <asp:ListItem Text="Male" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Female" Value="2"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="label_bold">
                                                                    Nationality
                                                                </td>
                                                                <td>
                                                                    <div class="textbox204">
                                                                        <asp:DropDownList ID="ddlNationalityUpadate" runat="server" AutoPostBack="false">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="height30">
                                                        <img src="../images/horizontal_line.png" alt="" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="pnlAddress" runat="server" DefaultButton="btnAddress">
                                                            <asp:UpdatePanel ID="upnlAdd" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <table border="0" cellspacing="4" cellpadding="3" width="100%">
                                                                        <tr>
                                                                            <td class="label_bold" colspan="4">
                                                                                Add Another Address
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label_bold" style="width: 113px;">
                                                                                Address
                                                                            </td>
                                                                            <td style="width: 327px;">
                                                                                <div class="textbox204">
                                                                                    <asp:TextBox ID="txtAddressUpdate" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </td>
                                                                            <td class="label_bold" style="width: 100px;">
                                                                                Town
                                                                            </td>
                                                                            <td>
                                                                                <div class="textbox204">
                                                                                    <asp:TextBox ID="txtTownUpdate" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label_bold">
                                                                                Post Code
                                                                            </td>
                                                                            <td>
                                                                                <div class="textbox204">
                                                                                    <asp:TextBox ID="txtPostCodeUpdate" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </td>
                                                                            <td class="label_bold">
                                                                                Region
                                                                            </td>
                                                                            <td>
                                                                                <div class="textbox204">
                                                                                    <asp:TextBox ID="txtRegionUpdate" runat="server"></asp:TextBox>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="label_bold">
                                                                                Country
                                                                            </td>
                                                                            <td>
                                                                                <div class="textbox204">
                                                                                    <asp:DropDownList ID="ddlCountryUpdate" runat="server" AutoPostBack="false">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </td>
                                                                            <td class="label_bold">
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="btnAddress" runat="server" ImageUrl="../images/btn_addanotheraddress.gif"
                                                                                    OnClick="btnAddress_Click" />
                                                                            </td>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="height30">
                                                        <img src="../images/horizontal_line.png" alt="" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:UpdatePanel ID="upnlAddress" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td class="whitebox_topleftcorner">
                                                                                    </td>
                                                                                    <td class="whitebox_top_bg">
                                                                                    </td>
                                                                                    <td class="whitebox_toprightcorner">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="whitebox_centerleftbg">
                                                                                    </td>
                                                                                    <td class="whitebox_centermiddlebg">
                                                                                        <div class="rounded_box">
                                                                                            Department
                                                                                            <div class="clear_10">
                                                                                            </div>
                                                                                            <asp:GridView ID="grdAddress" AllowSorting="True" AllowPaging="True" runat="server"
                                                                                                AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                                                RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                                                                                BorderWidth="0" GridLines="None" OnRowCommand="grdAddress_RowCommand" OnRowDataBound="grdAddress_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="sAddressStreet" HeaderText="Address" ItemStyle-CssClass="align_left"
                                                                                                        ItemStyle-Width="124px" />
                                                                                                    <asp:BoundField DataField="sAddressTown" HeaderText="Town" ItemStyle-CssClass="align_left"
                                                                                                        ItemStyle-Width="124px" />
                                                                                                    <asp:BoundField DataField="sAddressRegion" HeaderText="Region" ItemStyle-CssClass="align_left"
                                                                                                        ItemStyle-Width="124px" />
                                                                                                    <asp:BoundField DataField="sAddressPostCode" HeaderText="Post Code" ItemStyle-CssClass="align_left"
                                                                                                        ItemStyle-Width="124px" />
                                                                                                    <asp:BoundField DataField="sCountry" HeaderText="Country" ItemStyle-CssClass="align_left"
                                                                                                        ItemStyle-Width="124px" />
                                                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                                                        <ItemTemplate>
                                                                                                            <table border="0" cellspacing="0" cellpadding="0" class="table_border" style="border: 0px;">
                                                                                                                <tr>
                                                                                                                    <td>
                                                                                                                        <asp:ImageButton ID="imgBtnActiveAddress" runat="server" CommandName="main" CommandArgument='<%# Bind("pkAddressID") %>'
                                                                                                                            ImageUrl="~/Images/activate_icon.gif" ToolTip="Non-Active" />
                                                                                                                    </td>
                                                                                                                    <td>
                                                                                                                        <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkAddressID") %>'
                                                                                                                            ImageUrl="../images/close.png" ToolTip="Delete" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="3%" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                            <%--<table border="0" cellspacing="0" cellpadding="0" class="table_border">
                                                            <tr class="rowstyle">
                                                                <td class="align_left" style="width: 124px;">
                                                                    West bar, Kos island, Greece</td>
                                                                <td class="align_left" style="width: 124px;">
                                                                    West bar, Kos island, Greece</td>
                                                                <td class="align_left" style="width: 124px;">
                                                                    West bar, Kos island, Greece</td>
                                                                <td class="align_left" style="width: 124px;">
                                                                    West bar, Kos island, Greece</td>
                                                                <td class="align_left" style="width: 124px;">
                                                                    West bar, Kos island, Greece</td>
                                                                <td class="align_right" style="width: 19px;">
                                                                    <img src="../images/close.png" alt="" /></td>
                                                            </tr>
                                                            <tr class="alternate_row">
                                                                <td class="align_left">
                                                                    West bar, Malmo, Sweden</td>
                                                                <td class="align_left">
                                                                    West bar, Malmo, Sweden</td>
                                                                <td class="align_left">
                                                                    West bar, Malmo, Sweden</td>
                                                                <td class="align_left">
                                                                    West bar, Malmo, Sweden</td>
                                                                <td class="align_left">
                                                                    West bar, Malmo, Sweden</td>
                                                                <td class="align_right">
                                                                    <img src="../images/close.png" alt="" /></td>
                                                            </tr>
                                                            <tr class="rowstyle">
                                                                <td class="align_left">
                                                                    West bar, Kos island, Greece</td>
                                                                <td class="align_left">
                                                                    West bar, Kos island, Greece</td>
                                                                <td class="align_left">
                                                                    West bar, Kos island, Greece</td>
                                                                <td class="align_left">
                                                                    West bar, Kos island, Greece</td>
                                                                <td class="align_left">
                                                                    West bar, Kos island, Greece</td>
                                                                <td class="align_right">
                                                                    <img src="../images/close.png" alt="" /></td>
                                                            </tr>
                                                        </table>--%>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td class="whitebox_centerrightbg">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="whitebox_bottomleftcorner">
                                                                                    </td>
                                                                                    <td class="whitebox_bottom_bg">
                                                                                    </td>
                                                                                    <td class="whitebox_bottomrightcorner">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" class="height10">
                                                                </td>
                                                            </tr>
                                                        </table>
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
                <div class="bottom_graybox_right">
                    <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="../images/btn_update.png"
                        ValidationGroup="reqb" OnClick="btnUpdate_Click" />
                </div>
                <div class="clear_30">
                </div>
            </asp:View>
        </asp:MultiView>
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnExtent1"
            PopupControlID="pnlAddresses" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Button ID="btnExtent1" runat="server" Style="display: none;" />
        <asp:Panel ID="pnlAddresses" runat="server">
            <asp:UpdatePanel ID="upnlAddresses" runat="server">
                <ContentTemplate>
                    <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                    <div class="lightbox-header">
                        <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender1.ClientID %>').hide();return false;">
                            <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                    <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td>
                                    From:
                                </td>
                                <td>
                                    <div class="textbox204_message">
                                        <asp:Label ID="lblFromAddress" runat="server" Text="Amjad Latif"></asp:Label></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    To:
                                </td>
                                <td>
                                    <div class="textbox204_message">
                                        <asp:Label ID="lblToAddress" runat="server" Text="Hamid"></asp:Label></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Subject:
                                </td>
                                <td>
                                    <div class="textbox204_message">
                                        <asp:TextBox ID="txtSubject" runat="server" Style="border-style: none; border-color: inherit;
                                            border-width: 0px; color: Black; text-align: center; font-family: Verdana,Geneva,sans-serif;
                                            font-size: 12px; background: ; margin-top: 8px;"> </asp:TextBox></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Message:
                                </td>
                                <td>
                                    <div class="textboxmulti" style="float: left;">
                                        <asp:TextBox ID="txtMessage" runat="server" ValidationGroup="req" TextMode="MultiLine"> </asp:TextBox>
                                    </div>
                                    <span style="float: left;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMessage"
                                            ErrorMessage="*" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator></span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ImageButton ID="imgBtnMessage" runat="server" ImageUrl="~/Images/btn_send.gif"
                                        ValidationGroup="req" OnClick="imgBtnMessage_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%-- </div>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnExtent2"
            PopupControlID="pnlBonus" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Button ID="btnExtent2" runat="server" Style="display: none;" />
        <asp:Panel ID="pnlBonus" runat="server">
       
        <asp:UpdatePanel ID="upnlBonus" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="3" cellspacing="3" border="0" width="100%" align="center">
                    <tr >
                    <td align="left">
                    Last Bonus:
                    </td>
                    <td align="left">
                     <div class="textbox_small" style="float:left;margin-left:2px;">
                            <asp:TextBox ID="lblLastBouns" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:2;margin-left:1px;float:left;"> €</span>
                    
                    </td>
                    </tr>
                    
                    <tr>
                    <td>
                    <asp:Label runat="server" ID="lblMessagepopup" Visible="false"></asp:Label>
                    </td>
                    </tr>
                    <tr id="trSalaryType" runat="server">
                <td>
                    Salary Type:
                </td>
                <td>
                    <asp:RadioButton ID="rdScaled" runat="server" Text="Scaled" GroupName="sal" Checked="true" />
                    <asp:RadioButton ID="rdStandardSalary" runat="server" Text="Standard Salary" GroupName="sal" />
                    <asp:RadioButton ID="rdPerSalary" runat="server" Text="%Salary" GroupName="sal" />
                </td>
            </tr>
                    <tr id="trSalary" runat="server">
                    <td colspan="2">
                     <div id="divScaled" runat="server" style="display: none;">
            <table>
                <tr>
                    <td colspan="2">
                        <h3>
                            Scaled Salary</h3>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100px">
                        Low Season:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtLowSeason" CssClass="filter" runat="server"></asp:TextBox>
                        </div><span style="font-weight:bold;line-height:3;margin-left:1px;float:left;"> €</span>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100px">
                        High Season:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtHighSeason" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:3;margin-left:1px;float:left;"> €</span>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divStandard" runat="server" style="display: none;">
            <table>
                <tr>
                    <td colspan="2">
                        <h3>
                            Standard Salary
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100px">
                        Standard Salary:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtStandardSalary" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:3;margin-left:1px;float:left;"> €</span> 
                    </td>
                </tr>
            </table>
        </div>
        <div id="divPercentageSalary" runat="server" style="display: none;">
            <table>
                <tr>
                    <td colspan="2">
                        <h3>
                            Percentage Salary</h3>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100px">
                        Percentage:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtPercentage" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:2;margin-left:1px;float:left;"> %</span>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Minimum/day:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtMinimumPerDay" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:2;margin-left:1px;float:left;"> €</span>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        % Over:
                    </td>
                    <td>
                        <div class="textbox_small" style="float:left;">
                            <asp:TextBox ID="txtPercentageOver" CssClass="filter" runat="server"></asp:TextBox>
                        </div>
                        <span style="font-weight:bold;line-height:2;margin-left:1px;float:left;"> €</span>
                    </td>
                </tr>
            </table>
        </div>
                    </td>
                    </tr>
                        <tr>
                        <td colspan="2">
                        
                        <div id="divActiveDeactiveBonusMessage" runat="server" visible="false"></div>
                        </td>
                        </tr>
                        
                        <tr>
                        <td align="left">
                        Bonus Amount:
                        </td>
                        <td>
                        <div class="textbox_small" style="float: left; margin-left: 2px;">
                  <asp:TextBox ID="txtBonusAmount" runat="server"  Style="text-align: center;" CssClass="filter"
                  onkeypress="javascript:CorrectValue(this);" onchange="javascript:fixedDecimalPlace2(this);"></asp:TextBox>
                         </div>
                        <span style="position: relative; float: left;font-weight:bold; left: 1px; line-height: 31px;">€</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" style="margin-left:-18px;float:left;" runat="server" ControlToValidate="txtBonusAmount"
                                                        ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                        
                        </td>
                            
                        </tr>
                         <tr>
                        <td align="left">
                        Notes:
                        </td>
                        <td>
                        <div class="textboxmulti"  style="margin-left: 2px;">
                            <asp:TextBox ID="txtNoteBonus" TextMode="MultiLine" runat="server"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtNoteBonus"
                                                        ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                          </div>
                        </td>
                        </tr>
                        
                        <tr>
                            <td align="right">
                                <asp:CheckBox ID="chkBonus" runat="server" />
                            </td>
                            <td align="left">
                                <asp:Label ID="lblBonusActive" runat="server" Text="You can allow bonus by clicking on Checkbox"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ImageButton ID="imgBtnActiveBonus" ValidationGroup="Register" runat="server" ImageUrl="~/Images/btn_submit.png"
                                    OnClick="imgBtnActiveBonus_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
  
    </asp:Panel>
    </div>
</asp:Content>
