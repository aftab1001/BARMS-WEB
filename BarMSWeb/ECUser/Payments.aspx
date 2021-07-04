<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ECUserMaster.master"
    AutoEventWireup="true" CodeFile="Payments.aspx.cs" Inherits="ECUser_Payments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>

    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>--%>
    <link href="../JavaScript/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {

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

            //		$( "#ctl00_ContentPlaceHolder1_datepicker" ).datepicker({
            //			showOn: "button",
            //			buttonImage: "../images/calender.png",
            //			buttonImageOnly: true
            //		});
        });
    </script>

    <asp:TextBox ID="txtMyDay" runat="server" Style="display: none;" Text="1"></asp:TextBox>
    <asp:TextBox ID="txtCheck" runat="server" Style="display: none;" Text="1"></asp:TextBox>
    <asp:HiddenField ID="hidParam" runat="server" />
    <div style="margin: 0 auto; width: 375px; display: none;">
        <div class="demo workshifts_textbox" style="float: left;">
            <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="req1"
                ErrorMessage="*" ControlToValidate="datepicker" Display="Dynamic"></asp:RequiredFieldValidator>
            <%--<img id="btnCalender" src="../Images/calender.png" />
         <ajaxToolkit:CalendarExtender ID="extCalender" runat="server" Format="d" PopupPosition="bottomright" TargetControlID="datepicker" PopupButtonID="btnCalender"></ajaxToolkit:CalendarExtender>--%>
        </div>
        <%--<asp:ImageButton ID="btnGO" runat="server" ImageUrl="~/Images/btn_load.png" Style="float: left;
            margin-top: 2px;" OnClick="btnGO_Click" ValidationGroup="req1" /></div>--%>
        <asp:Button ID="btnGO" runat="server" OnClick="btnGO_Click" UseSubmitBehavior="false"
            Text="Button" /></div>
    <div id="Calendardate" style="margin-left: 385px;">
    </div>
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
    <div class="content_area_big">
        <div class="clear_30">
        </div>
        <div class="heading" align="center">
            My Payment</div>
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
                    <div class="rounded_box_big">
                        <table id="tPayment" runat="server" border="0" cellspacing="1" cellpadding="0" class="table_border">
                            <tr class="headerstyle" style="font-size: 17px; font-family: Calibri; font-weight: normal;">
                                <td class="align_center">
                                    LEGEND
                                </td>
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
                            <tr class="alternate_row">
                                <td>
                                </td>
                                <td id="tdDay1" runat="server" class="align_center">
                                    <asp:Label ID="lblUserSunday" runat="server" Text="Day OFF"></asp:Label>
                                </td>
                                <td id="tdDay2" runat="server" class="align_center">
                                    <asp:Label ID="lblUserMonday" runat="server" Text="Day OFF"></asp:Label>
                                </td>
                                <td id="tdDay3" runat="server" class="align_center">
                                    <asp:Label ID="lblUserTuesday" runat="server" Text="Day OFF"></asp:Label>
                                </td>
                                <td id="tdDay4" runat="server" class="align_center">
                                    <asp:Label ID="lblUserWednesday" runat="server" Text="Day OFF"></asp:Label>
                                </td>
                                <td id="tdDay5" runat="server" class="align_center">
                                    <asp:Label ID="lblUserThursday" runat="server" Text="Day OFF"></asp:Label>
                                </td>
                                <td id="tdDay6" runat="server" class="align_center">
                                    <asp:Label ID="lblUserFriday" runat="server" Text="Day OFF"></asp:Label>
                                </td>
                                <td id="tdDay7" runat="server" class="align_center">
                                    <asp:Label ID="lblUserSaturday" runat="server" Text="Day OFF"></asp:Label>
                                </td>
                                <td class="align_center" style="font-weight: bold;">
                                    Totals
                                </td>
                            </tr>
                        </table>
                        <div class="clear_10">
                        </div>
                        <div id="divNotes" runat="server">
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
        <table border="0" cellspacing="1" cellpadding="5" align="center">
            <tr>
                <td>
                    <asp:CheckBox ID="chkSalary" runat="server" Visible="false" AutoPostBack="true" OnCheckedChanged="chkSalary_CheckedChanged" />
                </td>
                <td>
                    <asp:Label ID="lblSalaryMessage" runat="server" Visible="false" Style="font-weight: bold;"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellspacing="1" cellpadding="5" align="center" style="display: none;">
            <tr>
                <td class="legend_table">
                    LEGEND
                </td>
                <td style="padding: 0px;">
                    <asp:DataList ID="dlLegends" runat="server" RepeatColumns="5">
                        <%--style="border-bottom: solid 1px #fff; border-right: solid 1px #fff;"--%>
                        <ItemTemplate>
                            <div>
                                <asp:Label ID="lblAbrv" runat="server" Text="<%#Bind('Abbrv') %>"></asp:Label>:
                                <asp:Label ID="lblPosition" runat="server" Text="<%#Bind('sSpeciality') %>"></asp:Label>
                            </div>
                            <%--<table border="0" cellspacing="0" cellpadding="5" width="100%;" >
                                <tr class="legend_table">
                                    <td>
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
        <div class="clear_10">
        </div>
        <table border="0" cellspacing="1" cellpadding="5" align="center" width="92%" style="display: none;">
            <tr class="legend_table_payment">
                <td style="font-weight: bold;">
                    LEGEND
                </td>
                <td style="background-color: #d6e3bc;">
                    Working
                </td>
                <td style="background-color: #e5b8b7;">
                    Day Off
                </td>
                <td style="background-color: #d9d9d9;">
                    Salary
                </td>
                <td style="background-color: #b8cce4;">
                    Tips
                </td>
                <td style="background-color: #c4bc96;">
                    Bonus
                </td>
                <td style="background-color: #ccc0d9;">
                    Advance
                </td>
                <td style="background-color: #fbd4b4;">
                    Penalty
                </td>
            </tr>
        </table>
    </div>
    <div class="clear_75">
    </div>
</asp:Content>
