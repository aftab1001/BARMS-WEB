<%@ Page Language="C#" MasterPageFile="~/MasterPages/ECUserMaster.master" AutoEventWireup="true"
    CodeFile="UserContract.aspx.cs" Inherits="ECUser_UserContract" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link href="../Styles/style.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript" src="../JavaScript/ToolTip.js"></script>

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
                                        <%--gdfgkldfgkldfjgkldfgjdfljggdfgkldfgkldfjgkldfgjdfljggdfgkldfgkldfjgkldfgjdfljggdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg gdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg< gdfgkldfgkldfjgkldfgjdfljggdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg gdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg<br />
                                        gdfgkldfgkldfjgkldfgjdfljg--%>
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
        <table style="width: 100%" >
            <tr>
                <td rowspan="3" align="left">
                    <img id="imgUser" runat="server" width="145" src="~/Images/no_image.gif" />
                </td>
                <td class="label_bold align_left_middle">
                    Agreed time of employment:
                </td>
                <td style="width: 241px;">
                    <span style="float: left; margin-top: 5px;">From: </span>
                    <div class="textbox204">
                        <%--<asp:TextBox ID="txtStartDate" runat="server" Enabled="false" ></asp:TextBox>--%>
                        <asp:Label ID="txtStartDate" runat="server"></asp:Label>
                    </div>
                </td>
                <td style="width: 241px;">
                    <span style="float: left; margin-top: 5px; margin-left: 15px;">Till:</span><div class="textbox204">
                        <%--<asp:TextBox ID="txtEndDate" runat="server" Enabled="false"></asp:TextBox>--%>
                        <asp:Label ID="txtEndDate" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <%--<div style="border-width: 2px; border-color: Black;">--%>
                    <div class="clear_10">
                    </div>
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
                                    <div style="border: #e7e7e7 1px solid; height: 100px;">
                                        <span style="position: relative; left: 30px; top: -10px; background-color: White;
                                            padding-right: 5px; padding-left: 5px;">Agreed Salary</span>
                                        <table id="tblSalary" runat="server" cellpadding="2" cellspacing="2">
                                            <tr id="trScaled" runat="server" visible="false">
                                                <td class="label_bold align_left_middle">
                                                    <asp:RadioButton ID="rdScaled" runat="server" Enabled="false" />
                                                </td>
                                                <td class="label_bold align_left_middle" style="width: 100px;">
                                                    Scaled Salary
                                                </td>
                                                <td class="label_bold align_left_middle">
                                                    <span style="margin-top: 7px; float: left; font-weight: normal;">Low Season:</span>
                                                    <div class="textbox_small_2">
                                                        <%--<asp:TextBox ID="txtLowSeason" runat="server" Enabled="false"></asp:TextBox>--%>
                                                        <asp:Label ID="txtLowSeason" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                                <td class="label_bold align_left_middle" colspan="2">
                                                    <span style="margin-top: 7px; float: left; padding-left: 14px; font-weight: normal;">
                                                        High Season:</span>
                                                    <div class="textbox_small_2">
                                                        <%--<asp:TextBox ID="txtHighSeason" runat="server" Enabled="false"></asp:TextBox>--%>
                                                        <asp:Label ID="txtHighSeason" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="trStandard" runat="server" visible="false">
                                                <td class="label_bold align_left_middle">
                                                    <asp:RadioButton ID="rdStandard" runat="server" Enabled="false" />
                                                </td>
                                                <td class="label_bold align_left_middle" style="width: 124px;">
                                                    Standard Salary
                                                </td>
                                                <td colspan="3">
                                                    <span style="margin-top: 7px; float: left; font-weight: normal;">Salary:</span>
                                                    <div class="textbox_small_2">
                                                        <%--<asp:TextBox ID="txtStandard" runat="server" Enabled="false"></asp:TextBox>--%>
                                                        <asp:Label ID="txtStandard" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="trPercentage" runat="server" visible="false">
                                                <td class="label_bold align_left_middle">
                                                    <asp:RadioButton ID="rdPercentage" runat="server" Enabled="false" />
                                                </td>
                                                <td class="label_bold align_left_middle" style="width: 100px;">
                                                    % Salary
                                                </td>
                                                <td>
                                                    <span style="margin-top: 7px; float: left; font-weight: normal;">Percentage:</span>
                                                    <div class="textbox_small_2">
                                                        <%--<asp:TextBox ID="txtPercentageSalary" runat="server" Enabled="false"></asp:TextBox>--%>
                                                        <asp:Label ID="txtPercentageSalary" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                                <td class="label_bold align_left_middle" style="padding-left: 5px; float: right;">
                                                    <span style="margin-top: 7px; float: left; font-weight: normal;">Minimum/day:</span>
                                                    <div class="textbox_small_2">
                                                        <%--<asp:TextBox ID="txtMinimumPerDay" runat="server" Enabled="false"></asp:TextBox>--%>
                                                        <asp:Label ID="txtMinimumPerDay" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                                <td class="label_bold align_left_middle" style="padding-left: 12px;">
                                                    <span style="margin-top: 7px; float: left; font-weight: normal;">% Over:</span>
                                                    <div class="textbox_small_2">
                                                        <%--<asp:TextBox ID="txtPerOver" runat="server" Enabled="false"></asp:TextBox>--%>
                                                        <asp:Label ID="txtPerOver" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
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
                    <%--</div>--%>
                </td>
                <tr>
                    <td colspan="3" align="center">
                        <ul style="text-align: center;">
                            <div style="float: left; width: 100%; margin-left: 130px; text-align: left;">
                                <ul>
                                    <li><span style="font-weight: bold;">Low Season:</span>
                                        <asp:Label ID="lblLowSeason" runat="server" Text=""></asp:Label>
                                    </li>
                                    <li><span style="font-weight: bold;">High Season:</span>
                                        <asp:Label ID="lblHighSeason" runat="server" Text=""></asp:Label></li>
                                </ul>
                            </div>
                        </ul>
                    </td>
                </tr>
            </tr>
            <tr>
                <td valign="top">
                    <div>
                        <table>
                            <tr>
                                <td class="label_bold align_left_middle">
                                    Name:
                                </td>
                                <td>
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="label_bold align_left_middle">
                                    Speciality:
                                </td>
                                <td>
                                    <asp:Label ID="lblSpeciality" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="label_bold align_left_middle">
                                    Email:
                                </td>
                                <td>
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="label_bold align_left_middle">
                                    Phone:
                                </td>
                                <td>
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td colspan="3" valign="top" class="label_bold align_left_middle" style="width: 100%;">
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
                                    <div id="divBonusDoc" runat="server" style="font-weight: normal;">
                                    </div>
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
                    <div class="clear_10">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="label_bold align_left_middle">
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
                                    <h2>
                                        Contract:</h2>
                                    <div id="divContractDoc" runat="server" style="font-weight: normal;">
                                    </div>
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
                <td colspan="4" align="center">
                    <div class="clear_10">
                    </div>
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
                                <div class="rounded_box" style="text-align: center;">
                                    <table align="center">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkAgreement" runat="server" OnCheckedChanged="chkAgreement_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCheckAgreement" runat="server" Text="I understand the above contract and I agree with all the referenced conditions."></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
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
    </div>
</asp:Content>
