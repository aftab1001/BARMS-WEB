<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AccountMaster.master"
    AutoEventWireup="true" CodeFile="ManagersWorkshift.aspx.cs" Inherits="AccountManager_ManagersWorkshift" %>

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
        function RecordSaved() {
            $("#ctl00_ContentPlaceHolder1_lblRecordMessage").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblRecordMessage");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblRecordMessage").fadeOut('slow');
                }
            }, 4000);
        }
    </script>

    <asp:ScriptManager ID="scriptmanager1" runat="server">
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
                <div id="Calendardate" style="margin-left: 328px;">
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
        <asp:LinkButton ID="lnkPrevious" runat="server" Text="Previous Week"></asp:LinkButton>
    </div>
    <div style="float: left; text-align: center; margin-left: 263px;" class="bold_label">
        <asp:Label ID="lblWeekDates" runat="server"></asp:Label>
    </div>
    <div class="nextweek" style="display: none;">
        <img src="../images/next_arrow.png" alt="" />
        <asp:LinkButton ID="lnkNext" runat="server" Text="Next Week"></asp:LinkButton>
    </div>
    <div>
        <div class="clear_30">
        </div>
        <div class="heading" align="center">
            <div style="float: right; display: none;">
                <input type="button" value="print" style="float: right;" onclick="javascript:printreport();" /></div>
        </div>
        <div class="clear_10">
        </div>
        <asp:UpdatePanel ID="upnl" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lblRecordMessage" runat="server" Style="font-size: 15px; font-weight: bold;
                    color: Green; margin-left: 400px; display: none;"></asp:Label>
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
                                <table border="0" cellspacing="1" cellpadding="0" width="100%">
                                    <tr class="headerstyle">
                                        <td>
                                        </td>
                                        <td class="align_center" style="height: 50px;">
                                            Sunday
                                            <asp:Label ID="lblSundayDate" runat="server"></asp:Label>
                                        </td>
                                        <td class="align_center">
                                            Monday<asp:Label ID="lblMondayDate" runat="server"></asp:Label>
                                        </td>
                                        <td class="align_center">
                                            Tuesday<asp:Label ID="lblTuesdayDate" runat="server"></asp:Label>
                                        </td>
                                        <td class="align_center">
                                            Wednesday<asp:Label ID="lblWednesdayDate" runat="server"></asp:Label>
                                        </td>
                                        <td class="align_center">
                                            Thursday<asp:Label ID="lblThrusdayDate" runat="server"></asp:Label>
                                        </td>
                                        <td class="align_center">
                                            Friday<asp:Label ID="lblFridayDate" runat="server"></asp:Label>
                                        </td>
                                        <td class="align_center">
                                            Saturday<asp:Label ID="lblSaturdayDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td valign="top">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td style="line-height: 15px; height: 141px; vertical-align:top;">
                                                                    C<br />
                                                                    o<br />
                                                                    n<br />
                                                                    t<br />
                                                                    r<br />
                                                                    o<br />
                                                                    l
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="line-height: 15px; height: 141px; vertical-align: top;">
                                                                    M<br />
                                                                    a<br />
                                                                    n<br />
                                                                    a<br />
                                                                    g<br />
                                                                    e<br />
                                                                    r
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr>
                                                                <td style="line-height: 15px; height: 100px;">
                                                                    O<br />
                                                                    t<br />
                                                                    h<br />
                                                                    e<br />
                                                                    r
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr class="alternate_row">
                                                                <td class="align_left" valign="top" style="height: 105px;">
                                                                    <asp:GridView ID="grdSundayECUser" AllowSorting="True" AllowPaging="True" ShowHeader="false"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdSundayECUser_RowDataBound" OnRowCommand="grdSundayECUser_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSunday" runat="server" />
                                                                                    <asp:HiddenField ID="hidECUserid" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <%--<asp:Label ID="lblName" runat="server" Text="<%#Bind('FullName') %>"></asp:Label>--%>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="color: #454545;" CommandName="assign"
                                                                                        CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdMondayECUser" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdMondayECUser_RowDataBound" OnRowCommand="grdMondayECUser_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkMonday" runat="server" />
                                                                                    <asp:HiddenField ID="hidECUserid" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        CommandName="assign" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdTuesdayECUser" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdTuesdayECUser_RowDataBound" OnRowCommand="grdTuesdayECUser_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkTuesday" runat="server" />
                                                                                    <asp:HiddenField ID="hidECUserid" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        CommandName="assign" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdWednesdayECUser" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdWednesdayECUser_RowDataBound" OnRowCommand="grdWednesdayECUser_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkWednesday" runat="server" />
                                                                                    <asp:HiddenField ID="hidECUserid" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        CommandName="assign" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdThursdayECUser" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdThursdayECUser_RowDataBound" OnRowCommand="grdThursdayECUser_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkThursday" runat="server" />
                                                                                    <asp:HiddenField ID="hidECUserid" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        CommandName="assign" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdFridayECUser" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdFridayECUser_RowDataBound" OnRowCommand="grdFridayECUser_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkFriday" runat="server" />
                                                                                    <asp:HiddenField ID="hidECUserid" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        CommandName="assign" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdSaturdayECUser" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdSaturdayECUser_RowDataBound" OnRowCommand="grdSaturdayECUser_RowCommand">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSaturday" runat="server" />
                                                                                    <asp:HiddenField ID="hidECUserid" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        CommandName="assign" CommandArgument="<%#Bind('pkuserid') %>" Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 20px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7" style="background-color: Gray; height: 2px;">
                                                                </td>
                                                            </tr>
                                                            <tr class="alternate_row" style="display: none;">
                                                                <td style="height: 105px;">
                                                                    <asp:CheckBox ID="chkSundayManger" runat="server" Text="Manager" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkMondayManger" runat="server" Text="Manager" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkTuesdayManger" runat="server" Text="Manager" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkWednesdayManger" runat="server" Text="Manager" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkThursdayManger" runat="server" Text="Manager" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkFridayManager" runat="server" Text="Manager" />
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkSaturdayManager" runat="server" Text="Manager" />
                                                                </td>
                                                            </tr>
                                                            <tr class="alternate_row">
                                                                <td class="align_left" valign="top" style="height: 105px;">
                                                                    <asp:GridView ID="grdSundayManager" AllowSorting="True" AllowPaging="True" ShowHeader="false"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdSundayManager_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSunday" runat="server" Enabled="false" Checked="true" />
                                                                                    <asp:HiddenField ID="hidManagerID" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <%--<asp:Label ID="lblName" runat="server" Text="<%#Bind('FullName') %>"></asp:Label>--%>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkuserid') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdMondayManager" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdMondayManager_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkMonday" runat="server" Enabled="false" Checked="true" />
                                                                                    <asp:HiddenField ID="hidManagerID" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkuserid') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdTuesdayManager" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdTuesdayManager_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkTuesday" runat="server" Enabled="false" Checked="true" />
                                                                                    <asp:HiddenField ID="hidManagerID" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkuserid') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdWednesdayManager" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdWednesdayManager_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkWednesday" runat="server" Enabled="false" Checked="true" />
                                                                                    <asp:HiddenField ID="hidManagerID" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkuserid') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdThursdayManager" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdThursdayManager_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkThursday" runat="server" Enabled="false" Checked="true" />
                                                                                    <asp:HiddenField ID="hidManagerID" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkuserid') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdFridayManager" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdFridayManager_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkFriday" runat="server" Enabled="false" Checked="true" />
                                                                                    <asp:HiddenField ID="hidManagerID" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkuserid') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdSaturdayManager" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdSaturdayManager_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSaturday" runat="server" Enabled="false" Checked="true" />
                                                                                    <asp:HiddenField ID="hidManagerID" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkuserid') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 20px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7" style="background-color: Gray; height: 2px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="align_left" valign="top" style="height: 100px;">
                                                                    <asp:GridView ID="grdSunday" AllowSorting="True" AllowPaging="True" ShowHeader="false"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdSunday_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSundayOther" runat="server" />
                                                                                    <asp:HiddenField ID="hidUserid" runat="server" Value="<%#Bind('pkUserID') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <%--<asp:Label ID="lblName" runat="server" Text="<%#Bind('FullName') %>"></asp:Label>--%>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkUserID') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdMonday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdMonday_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkMondayOther" runat="server" />
                                                                                    <asp:HiddenField ID="hidUserid" runat="server" Value="<%#Bind('pkUserID') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkUserID') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdTuesday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdTuesday_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkTuesdayOther" runat="server" />
                                                                                    <asp:HiddenField ID="hidUserid" runat="server" Value="<%#Bind('pkUserID') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkUserID') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdWednesday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdWednesday_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkWednesdayOther" runat="server" />
                                                                                    <asp:HiddenField ID="hidUserid" runat="server" Value="<%#Bind('pkUserID') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkUserID') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdThursday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdThursday_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkThursdayOther" runat="server" />
                                                                                    <asp:HiddenField ID="hidUserid" runat="server" Value="<%#Bind('pkUserID') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkUserID') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdFriday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdFriday_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkFridayOther" runat="server" />
                                                                                    <asp:HiddenField ID="hidUserid" runat="server" Value="<%#Bind('pkUserID') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkUserID') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                                <td class="align_left" valign="top">
                                                                    <asp:GridView ID="grdSaturday" AllowSorting="True" ShowHeader="false" AllowPaging="True"
                                                                        runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="alternate_row"
                                                                        Width="100%" CellPadding="0" CellSpacing="0" BorderStyle="None" BorderWidth="0"
                                                                        GridLines="None" OnRowDataBound="grdSaturday_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkSaturdayOther" runat="server" />
                                                                                    <asp:HiddenField ID="hidUserid" runat="server" Value="<%#Bind('pkUserID') %>" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkName" runat="server" Style="text-decoration: none; color: #454545;"
                                                                                        OnClientClick="return false;" CommandName="message" CommandArgument="<%#Bind('pkUserID') %>"
                                                                                        Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:BoundField DataField="FullName" />--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 20px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="7" style="background-color: Gray; height: 2px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 20px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
                                                                </td>
                                                                <td align="right">
                                                                    <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="../Images/btn_cancel.png"
                                                                        OnClick="imgBtnCancel_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="../Images/btn_save_account.png"
                                                                        OnClick="imgBtnSave_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--<td class="whitebox_bottomleftcorner">
        </td>
        <td class="whitebox_bottom_bg">
        </td>
        <td class="whitebox_bottomrightcorner">
        </td>
        </tr> </table>
        <div class="clear_10">
        </div>--%>
    </div>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnExtent2"
        PopupControlID="pnlBonus" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent2" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlBonus" runat="server">
        <asp:UpdatePanel ID="upnlBonus" runat="server">
            <ContentTemplate>
                <div class="lightbox-header">
                    <span style="float: left; color: White; line-height: 37px; padding-left: 10px; font-weight: bold;">
                        Assign Positions</span> <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;">
                            <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="3" cellspacing="3" border="0" width="60%" align="center">
                        <tr>
                            <td>
                                <asp:GridView ID="grdNormalUsers" runat="server" AutoGenerateColumns="false" GridLines="None"
                                    HeaderStyle-CssClass="headerstyle" AlternatingRowStyle-CssClass="alternate_row"
                                    RowStyle-CssClass="alternate_row" Width="100%" CellPadding="0" CellSpacing="0"
                                    BorderStyle="None" BorderWidth="0" OnRowDataBound="grdNormalUsers_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" 
                                            HeaderText="Positions" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr id="hrSeprator" runat="server" style="display: none;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                       <td style="text-align:left;">
                                                            <asp:Label ID="lblNormalUserName" runat="server" Text="<%#Bind('sSpeciality') %>"></asp:Label>
                                                            <asp:HiddenField ID="hidspid" runat="server" Value="<%#Bind('pkSpecialityID') %>" />
                                                            <asp:HiddenField ID="hdnOrderID" runat="server" Value="<%#Bind('OrderId') %>" />
                                                            <asp:HiddenField ID="hidECID" runat="server" Value="0" />
                                                        </td>
                                                        <td style="text-align:right;">
                                                            <asp:CheckBox ID="chkBox" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdRegisters" runat="server" AutoGenerateColumns="false" GridLines="None"
                                    HeaderStyle-CssClass="headerstyle" AlternatingRowStyle-CssClass="alternate_row"
                                    RowStyle-CssClass="alternate_row" Width="100%" CellPadding="0" CellSpacing="0"
                                    BorderStyle="None" BorderWidth="0" OnRowDataBound="grdRegisters_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                            HeaderText="Registers" ItemStyle-Width="255">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRegisterName" runat="server" Text="<%#Bind('rName') %>"></asp:Label>
                                                <asp:HiddenField ID="hidRegisterid" runat="server" Value="<%#Bind('pkRegisterID') %>" />
                                                <asp:HiddenField ID="hidECID" runat="server" Value="0" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkBox" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:ImageButton ID="imgBtnSaveAssignment" runat="server" ImageUrl="../images/btn_save_account.png"
                                    OnClick="imgBtnSaveAssignment_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
