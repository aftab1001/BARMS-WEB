<%@ Page Language="C#" MasterPageFile="~/MasterPages/ManagerMaster.master" AutoEventWireup="true"
    CodeFile="EditUser.aspx.cs" Inherits="Managers_EditUser" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
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
    <div class="graybox_account">
        <div class="top">
        </div>
        <div class="clear">
        </div>
        <div class="centermiddle">
            <table border="0" cellspacing="0" cellpadding="0" width="100%" align="center">
                <tr>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" Text="Updated successfully!" Visible="false" ForeColor="Green" style="font-weight:bold;font-size:15px;margin-top:-24px;padding-left:288px;position:absolute;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="width22percent align_center_top">
                                    <img id="userImage" runat="server" width="149" src="../images/no_image.gif" alt="" /><br />
                                    <br />
                                    <%-- <a id="ankUpload" href="javascript:document.getElementById('ctl00_ContentPlaceHolder1_fpUploadPic').click();">
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
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword"
                                                        ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="align_left_middle" colspan="2">
                                                <div class="new_password">
                                                    <asp:TextBox ID="txtNewPassword" Text="" runat="server" TextMode="password" Style="margin-right: -3px;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPassword"
                                                        ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="align_left_middle" colspan="2">
                                                <div class="textbox_repassword">
                                                    <asp:TextBox ID="txtReNewPassword" Text="" runat="server" TextMode="password" Style="margin-right: -3px;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtReNewPassword"
                                                        ErrorMessage="*" ValidationGroup="Register"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="width4percent align_center_top">
                                    <img src="../images/vertical_line.png" alt="" />
                                </td>
                                <td class="width44percent align_top">
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
                                                    <asp:TextBox ID="txtFacebook" runat="server" onmouseover="javascript:OpenFeedbackWindow('Please enter your facebook profile name only in the text box below so that the text box should read:<br/> https://www.facebook.com/yourprofile.name')"
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
                                                    <asp:TextBox ID="txtTwitter" runat="server" onmouseover="javascript:OpenFeedbackWindow('Please enter your twitter account name only in the text box below so that the text box should read:<br/> https://twitter.com/#!/youraccountname')"
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
                                                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="width55percent">
                                                <asp:ImageButton ID="btnEmail" runat="server" ImageUrl="../images/btn_addanotheremail.gif"
                                                    OnClick="btnEmail_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="width30">
                                </td>
                                <td class="align_top">
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
                                                    <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="width55percent">
                                                <asp:ImageButton ID="btnMobile" runat="server" ImageUrl="../images/btn_anotherphone.gif"
                                                    OnClick="btnMobile_Click1" />
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
                                                    <asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="false">
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
                                                                Speciality
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
                                                    <asp:DropDownList ID="ddlSpeciality" runat="server" AutoPostBack="false">
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td class="width55percent">
                                                <asp:ImageButton ID="btnSpeciality" runat="server" ImageUrl="../images/btn_addanotherspeciality.gif"
                                                    OnClick="btnSpeciality_Click" />
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
                                                                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
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
                                                                            <asp:TextBox ID="txtDate" runat="server" onkeyup="javascript:numbersCheck(this);"></asp:TextBox></div>
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label_bold">
                                                            Last Name
                                                        </td>
                                                        <td>
                                                            <div class="textbox204">
                                                                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <td class="label_bold">
                                                            Gender
                                                        </td>
                                                        <td>
                                                            <div class="textbox204">
                                                                <asp:DropDownList ID="ddlGender" runat="server" AutoPostBack="false">
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
                                                                <asp:DropDownList ID="ddlNationality" runat="server" AutoPostBack="false">
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
                                                                <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <td class="label_bold" style="width: 100px;">
                                                            Town
                                                        </td>
                                                        <td>
                                                            <div class="textbox204">
                                                                <asp:TextBox ID="txtTown" runat="server"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label_bold">
                                                            Post Code
                                                        </td>
                                                        <td>
                                                            <div class="textbox204">
                                                                <asp:TextBox ID="txtPostCode" runat="server"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                        <td class="label_bold">
                                                            Region
                                                        </td>
                                                        <td>
                                                            <div class="textbox204">
                                                                <asp:TextBox ID="txtRegion" runat="server"></asp:TextBox>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="label_bold">
                                                            Country
                                                        </td>
                                                        <td>
                                                            <div class="textbox204">
                                                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="false">
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
                ValidationGroup="req" OnClick="btnUpdate_Click" />
        </div>
        <div class="clear_30">
        </div>
    </div>
</asp:Content>
