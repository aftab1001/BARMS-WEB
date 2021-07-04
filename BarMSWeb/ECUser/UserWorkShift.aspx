<%@ Page Language="C#" MasterPageFile="~/MasterPages/ECUserMaster.master" AutoEventWireup="true"
    CodeFile="UserWorkShift.aspx.cs" Inherits="ECUser_UserWorkShift" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>

    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>--%>
    <link href="../JavaScript/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script>
        $(function() {
            //            $("#ctl00_ContentPlaceHolder1_datepicker").datepicker({
            //                showOn: "button",
            //                buttonImage: "../images/calender.png",
            //                buttonImageOnly: true
            //            });


            var startDate;
            var endDate;
            var Dateselect;

            var myDay = document.getElementById("ctl00_ContentPlaceHolder1_txtMyDay");
            var check = document.getElementById("ctl00_ContentPlaceHolder1_txtCheck");


            var author_value = '';
            author_value = getQuerystring('day').toString();

            if (author_value != '') {
                $("#Calendardate").datepicker({
                    defaultDate: author_value + 'd',
                    showOtherMonths: true,
                    selectOtherMonths: true,
                    onSelect: function(dateText, inst) {
                        var date = $(this).datepicker('getDate');

                        Dateselect = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                        startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
                        endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);

                        var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;

                        $('#ctl00_ContentPlaceHolder1_datepicker').text($.datepicker.formatDate(dateFormat, Dateselect, inst.settings));

                        selectCurrentWeek();
                        var txt = document.getElementById("ctl00_ContentPlaceHolder1_datepicker");
                        txt.value = $('#ctl00_ContentPlaceHolder1_datepicker').text();
                        __doPostBack('ctl00$ContentPlaceHolder1$btnGo', '');

                    },

                    beforeShowDay: function(date) {
                        var cssClass = '';

                        if (date >= startDate && date <= endDate)
                            cssClass = 'ui-datepicker-current-day';
                        return [true, cssClass];
                    },
                    onChangeMonthYear: function(year, month, inst) {
                        selectCurrentWeek();
                    }
                });

            }
            else {
                $("#Calendardate").datepicker({

                    showOtherMonths: true,
                    selectOtherMonths: true,
                    onSelect: function(dateText, inst) {
                        var date = $(this).datepicker('getDate');

                        Dateselect = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                        startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay());
                        endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() - date.getDay() + 6);
                        var dateFormat = inst.settings.dateFormat || $.datepicker._defaults.dateFormat;

                        $('#ctl00_ContentPlaceHolder1_datepicker').text($.datepicker.formatDate(dateFormat, Dateselect, inst.settings));

                        selectCurrentWeek();
                        var txt = document.getElementById("ctl00_ContentPlaceHolder1_datepicker");
                        txt.value = $('#ctl00_ContentPlaceHolder1_datepicker').text();
                        __doPostBack('ctl00$ContentPlaceHolder1$btnGo', '');
                    },

                    beforeShowDay: function(date) {
                        var cssClass = '';
                        if (date >= startDate && date <= endDate)
                            cssClass = 'ui-datepicker-current-day';
                        return [true, cssClass];
                    },
                    onChangeMonthYear: function(year, month, inst) {
                        selectCurrentWeek();
                    }
                });
            }
            var selectCurrentWeek = function() {
                window.setTimeout(function() {
                    $('#Calendardate').find('.ui-datepicker-current-day a').addClass('ui-state-active')
                }, 1);
            }


            $('#Calendardate .ui-datepicker-calendar tr').live('mousemove', function() {
                $(this).find('td a').addClass('ui-state-hover');
            });
            $('#Calendardate .ui-datepicker-calendar tr').live('mouseleave', function() {
                $(this).find('td a').removeClass('ui-state-hover');
            });



        });

        function getQuerystring(key, default_) {

            if (default_ == null) default_ = "";
            key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
            var qs = regex.exec(window.location.href);
            if (qs == null)
                return default_;
            else
                return qs[1];
        }

        function printreport() {
            var hidParam = document.getElementById("ctl00_ContentPlaceHolder1_hidParam");
            var a = window.open("../Users/PrintWorkShift.aspx" + hidParam.value, 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');
        }
        function OpenFeedbackWindow(FeedBack) {
            var feedbackText;
            feedbackText = FeedBack;
            document.getElementById("divBalloon").innerHTML = feedbackText;
            ShowContent("divFeedbackWindow");
        }
        function CloseFeedBackWindow() {

            HideContent("divFeedbackWindow");
        }
        function showPopup() {
            $find("ModalPopupExtender1").show();
        }
        function HidePopup() {

        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
    <asp:TextBox ID="txtMyDay" runat="server" Style="display: none;" Text="1"></asp:TextBox>
    <asp:TextBox ID="txtCheck" runat="server" Style="display: none;" Text="1"></asp:TextBox>
    <%--<div style="width:20px;height:30px;border:solid 1px red;clear:both;"></div>--%>
    <asp:HiddenField ID="hidParam" runat="server" />
    <div style="margin: 0 auto; width: 375px; display: none;">
        <div style="float: left;">
            <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="datepicker"
                ErrorMessage="*" Display="Dynamic" ValidationGroup="req1"></asp:RequiredFieldValidator>
            <%--<img id="btnCalender" src="../Images/calender.png" />
         <ajaxToolkit:CalendarExtender ID="extCalender" runat="server" Format="d" PopupPosition="bottomright" TargetControlID="datepicker" PopupButtonID="btnCalender"></ajaxToolkit:CalendarExtender>--%>
        </div>
        <%-- <asp:ImageButton ID="btnGO" runat="server" ImageUrl="~/Images/btn_load.png" Style="float: left;
            margin-top: 2px;" OnClick="btnGO_Click" ValidationGroup="req1" />--%>
        <asp:Button ID="btnGO" runat="server" OnClick="btnGO_Click" UseSubmitBehavior="false"
            Text="Button" /></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
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
                <div style="margin: 0 auto; text-align: center;">
                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Size="Medium" Text="Manage Work Shifts"></asp:Label></div>
                <div class="clear_30">
                </div>
                <div id="Calendardate" style="margin-left: 362px;">
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
    <div class="clear_40">
    </div>
    <div class="previousweek" style="display: none;">
        <img src="../images/back_arrow.png" alt="" />
        <asp:LinkButton ID="lnkPrevious" runat="server" Text="Previous Week" OnClick="lnkPrevious_Click"></asp:LinkButton>
    </div>
    <div style="float: left; width: 1000px; text-align: center;" class="bold_label">
        <asp:Label ID="lblWeekDates" runat="server"></asp:Label>
    </div>
    <div class="nextweek" style="display: none;">
        <img src="../images/next_arrow.png" alt="" />
        <asp:LinkButton ID="lnkNext" runat="server" Text="Next Week" OnClick="lnkNext_Click"></asp:LinkButton>
    </div>
    <div class="clear_30">
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
                <div class="rounded_box_big">
                    <div class="heading" align="center">
                        My Schedule
                        <div style="float: right;">
                            <input type="button" value="print" style="float: right;" onclick="javascript:printreport();" /></div>
                    </div>
                    <div class="clear_10">
                    </div>
                    <table border="0" cellspacing="1" cellpadding="0" class="table_border">
                        <tr class="rowstyle">
                            <td class="align_center">
                                Sunday
                            </td>
                            <td class="align_center">
                                Monday
                            </td>
                            <td class="align_center">
                                Tuesday
                            </td>
                            <td class="align_center">
                                Wednesday
                            </td>
                            <td class="align_center">
                                Thursday
                            </td>
                            <td class="align_center">
                                Friday
                            </td>
                            <td class="align_center">
                                Saturday
                            </td>
                        </tr>
                        <tr class="alternate_row" style="vertical-align:top;">
                            <td class="align_center" >
                                <asp:Label ID="lblUserSunday" runat="server" Text="Day OFF"></asp:Label>
                            </td>
                            <td class="align_center">
                                <asp:Label ID="lblUserMonday" runat="server" Text="Day OFF"></asp:Label>
                            </td>
                            <td class="align_center">
                                <asp:Label ID="lblUserTuesday" runat="server" Text="Day OFF"></asp:Label>
                            </td>
                            <td class="align_center">
                                <asp:Label ID="lblUserWednesday" runat="server" Text="Day OFF"></asp:Label>
                            </td>
                            <td class="align_center">
                                <asp:Label ID="lblUserThursday" runat="server" Text="Day OFF"></asp:Label>
                            </td>
                            <td class="align_center">
                                <asp:Label ID="lblUserFriday" runat="server" Text="Day OFF"></asp:Label>
                            </td>
                            <td class="align_center">
                                <asp:Label ID="lblUserSaturday" runat="server" Text="Day OFF"></asp:Label>
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
    <div class="clear_10">
    </div>
    <div class="heading" align="center" style="display: none;">
        Other people working</div>
    <div class="clear_10">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="display: none;">
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
                <div class="rounded_box_big">
                    <table id="tblOtherUsersWorshift" runat="server" border="0" cellspacing="1" cellpadding="0"
                        class="table_border">
                        <tr class="headerstyle">
                            <td class="align_left">
                                Sunday
                            </td>
                            <td class="align_left">
                                Monday
                            </td>
                            <td class="align_left">
                                Tuesday
                            </td>
                            <td class="align_left">
                                Wednesday
                            </td>
                            <td class="align_left">
                                Thursday
                            </td>
                            <td class="align_left">
                                Friday
                            </td>
                            <td class="align_left">
                                Saturday
                            </td>
                        </tr>
                        <tr>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdSunday1" AllowSorting="True" AllowPaging="True" ShowHeader="false"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdSunday1_RowDataBound" OnRowCommand="grdSunday1_RowCommand">
                                    <Columns>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('fkuserid') %>"></asp:LinkButton>
                                                <%--<a href="#" onclick="$find('<%=ModalPopupExtender1.ClientID %>').show();return false;">Name</a>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdMonday1" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdMonday1_RowDataBound" OnRowCommand="grdMonday1_RowCommand">
                                    <Columns>
                                        <%-- <asp:BoundField DataField="FullName" />--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('fkuserid') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdTuesday1" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdTuesday1_RowDataBound" OnRowCommand="grdTuesday1_RowCommand">
                                    <Columns>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('fkuserid') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdWednesday1" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdWednesday1_RowDataBound" OnRowCommand="grdWednesday1_RowCommand">
                                    <Columns>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('fkuserid') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left">
                                <asp:GridView ID="grdThursday1" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdThursday1_RowDataBound" OnRowCommand="grdThursday1_RowCommand">
                                    <Columns>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('fkuserid') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdFriday1" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdFriday1_RowDataBound" OnRowCommand="grdFriday1_RowCommand">
                                    <Columns>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('fkuserid') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdSaturday1" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdSaturday1_RowDataBound" OnRowCommand="grdSaturday1_RowCommand">
                                    <Columns>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server"></asp:Label><br />
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('fkuserid') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <%--   <tr class="alternate_row">
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
        </tr>
        <tr class="rowstyle">
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
        </tr>
        <tr class="alternate_row">
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
        </tr>
        <tr class="rowstyle">
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
          <td class="align_left">B2 20.30 - 04.30 Andreas J.</td>
        </tr>--%>
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
    <div class="clear_10">
    </div>
    <div class="heading" align="center" style="display: none;">
        People with Days off</div>
    <div class="clear_10">
    </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="display: none;">
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
                <div class="rounded_box_big">
                    <table border="0" cellspacing="1" cellpadding="0" class="table_border">
                        <tr class="headerstyle">
                            <td class="align_left" style="width: 70px;">
                                Sunday
                            </td>
                            <td class="align_left" style="width: 70px;">
                                Monday
                            </td>
                            <td class="align_left" style="width: 70px;">
                                Tuesday
                            </td>
                            <td class="align_left" style="width: 70px;">
                                Wednesday
                            </td>
                            <td class="align_left" style="width: 70px;">
                                Thursday
                            </td>
                            <td class="align_left" style="width: 70px;">
                                Friday
                            </td>
                            <td class="align_left" style="width: 70px;">
                                Saturday
                            </td>
                        </tr>
                        <tr>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdSunday" AllowSorting="True" AllowPaging="True" ShowHeader="false"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdSunday_RowDataBound" OnRowCommand="grdSunday_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblName" runat="server" Text="<%#Bind('FullName') %>"></asp:Label>--%>
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdMonday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdMonday_RowDataBound" OnRowCommand="grdMonday_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdTuesday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdTuesday_RowDataBound" OnRowCommand="grdTuesday_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdWednesday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdWednesday_RowDataBound" OnRowCommand="grdWednesday_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdThursday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdThursday_RowDataBound" OnRowCommand="grdThursday_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdFriday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdFriday_RowDataBound" OnRowCommand="grdFriday_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                    </Columns>
                                </asp:GridView>
                            </td>
                            <td class="align_left" valign="top">
                                <asp:GridView ID="grdSaturday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" Width="100%"
                                    CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None"
                                    OnRowDataBound="grdSaturday_RowDataBound" OnRowCommand="grdSaturday_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                    CommandName="message" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="FullName" />--%>
                                    </Columns>
                                </asp:GridView>
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
    <div class="clear_10">
    </div>
    <table border="0" cellspacing="2" cellpadding="5" align="center" style="display: none;">
        <tr>
            <td class="legend_table">
                LEGEND
            </td>
            <td style="padding: 0px;">
                <asp:DataList ID="dlLegends" runat="server" RepeatColumns="5">
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblAbrv" runat="server" Text="<%#Bind('Abbrv') %>"></asp:Label>:
                            <asp:Label ID="lblPosition" runat="server" Text="<%#Bind('sSpeciality') %>"></asp:Label>
                        </div>
                        <%--<table border="0" cellspacing="1" cellpadding="5" style="height: 25px;" width="100%">
                                <tr class="legend_table">
                                    <td>
                                        <asp:Label ID="lblAbrv" runat="server" Text="<%#Bind('Abbrv') %>"></asp:Label>:
                                        <asp:Label ID="lblPosition" runat="server" Text="<%#Bind('sSpeciality') %>"></asp:Label>
                                    </td>
                                </tr>
                            </table>--%>
                    </ItemTemplate>
                </asp:DataList>
                <%--B1: Position Bartender 1 etc.--%>
            </td>
            <%--<td>
                    W1: Position Waiter 1 etc.
                </td>
                <td>
                    DJ1: Position DJ etc.
                </td>
                <td>
                    D1: Position Doorman 1 etc.
                </td>--%>
        </tr>
    </table>
    <div class="clear_75">
    </div>
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
                                    <asp:TextBox ID="txtSubject" runat="server" Style="color: Black; text-align: center;
                                        background: Transparent; border-width: 0; width: 185px; margin-top: 8px; font-family: Verdana,Geneva,sans-serif;
                                        font-size: 12px;"> </asp:TextBox></div>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="txtMessage"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ImageButton ID="imgBtnMessage" runat="server" ImageUrl="~/Images/btn_save.png"
                                    ValidationGroup="req" OnClick="imgBtnMessage_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
