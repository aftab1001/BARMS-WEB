<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ManagerMaster.master"
    AutoEventWireup="true" CodeFile="ManagerDayOff.aspx.cs" Inherits="Managers_ManagerDayOff" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>

    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>--%>
    <link href="../JavaScript/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.watermark.js" type="text/javascript"></script>

    <script>
        function datePic(value, iUserID) {

            document.getElementById("ctl00_ContentPlaceHolder1_hdnUserID").value = iUserID;
            document.getElementById("ctl00_ContentPlaceHolder1_txtDayOff").value = value;
            var txt = document.getElementById("ctl00_ContentPlaceHolder1_datepicker");
            txt.value = value;
            __doPostBack('ctl00$ContentPlaceHolder1$btnGo', '');
        }
        $(function() {
            //            $("#ctl00_ContentPlaceHolder1_datepicker").datepicker({
            //                showOn: "button",
            //                buttonImage: "../images/calender.png",
            //                buttonImageOnly: true
            //            });
            ShowDataPicker();

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
            $('#ctl00_ContentPlaceHolder1_txtReason').watermark('Need to rest');
            $('#ctl00_ContentPlaceHolder1_txtReasonLong').watermark('(Example: "Need to Rest")');


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


        function ShowDataPicker() {

            //            $("#ctl00_ContentPlaceHolder1_txtDayOff").datepicker({
            //                dateFormat: 'dd/mm/yy'

            //            });
            $("#ctl00_ContentPlaceHolder1_txtStartDate").datepicker({
                dateFormat: 'dd/mm/yy'
            });

            $("#ctl00_ContentPlaceHolder1_txtTillDate").datepicker({
                dateFormat: 'dd/mm/yy'

            });

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
    <asp:UpdatePanel ID="upMain" runat="server">
        <ContentTemplate>
            <div style="margin: 0 auto; width: 375px; display: none;">
                <div style="float: left;">
                    <asp:HiddenField ID="hdnUserID" runat="server" />
                    <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="datepicker"
                        ErrorMessage="*" Display="Dynamic" ValidationGroup="req1"></asp:RequiredFieldValidator>
                    <%--<img id="btnCalender" src="../Images/calender.png" />
         <ajaxToolkit:CalendarExtender ID="extCalender" runat="server" Format="d" PopupPosition="bottomright" TargetControlID="datepicker" PopupButtonID="btnCalender"></ajaxToolkit:CalendarExtender>--%>
                </div>
                <%-- <asp:ImageButton ID="btnGO" runat="server" ImageUrl="~/Images/btn_load.png" Style="float: left;
            margin-top: 2px;" OnClick="btnGO_Click" ValidationGroup="req1" />--%>
                <asp:Button ID="btnGO" runat="server" OnClick="btnGO_Click" UseSubmitBehavior="false"
                    Text="Button" />
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
                        <div style="margin: 0 auto; text-align: center;">
                            <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Size="Medium" Style="position: relative;
                                right: 65px;" Text="Manage Day Off"></asp:Label></div>
                        <div class="clear_30">
                        </div>
                        <div style="margin-left: 235px;">
                            <asp:Calendar ID="ManagerCalendar" runat="server" BackColor="White" BorderColor="Black"
                                Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="300px" Width="300px"
                                OnDayRender="ManagerCalendar_DayRender" NextPrevFormat="ShortMonth" BorderStyle="Solid"
                                CellSpacing="2" FirstDayOfWeek="Sunday">
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <DayStyle BackColor="#e6e6e6" Font-Bold="true" HorizontalAlign="Left" VerticalAlign="Top"
                                    BorderColor="Black" BorderWidth="1" />
                                <NextPrevStyle Font-Size="8pt" ForeColor="White" Font-Bold="True" />
                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt"
                                    BackColor="#ffffff" />
                                <TitleStyle BackColor="#d6d6d6" Font-Bold="True" Font-Size="11pt" ForeColor="Black"
                                    BorderStyle="Solid" Height="12pt" />
                            </asp:Calendar>
                        </div>
                        <%-- <div id="Calendardate" style="margin-left: 270px;">
                </div>--%>
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
            <div style="float: left; text-align: center; margin-left: 200px;" class="bold_label">
                <asp:Label ID="lblWeekDates" runat="server"></asp:Label>
            </div>
            <div class="clear">
            </div>
            <div style="float: left">
                <a href="MyWorkShift.aspx" style="float: left; text-decoration: underline; font-size: 12px; font-weight:bold;
                    color: blue;">Go to MyWorkShift</a>
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
                <asp:Label ID="lblRecordMessage" runat="server" Style="font-size: 15px; display: none;
                    font-weight: bold; color: Green; margin-left: 292px;"></asp:Label>
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
                            <table cellpadding="0" cellspacing="5" width="100%">
                                <tr id="trManagerName" runat="server">
                                    <td>
                                        <div style="background-color: #ea9999;">
                                            <asp:Label ID="lblManagerName" runat="server" Text=""></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="textbox_small" style="float: left;">
                                            <asp:TextBox ID="txtDayOff" runat="server" CssClass="filter" Style="text-align: center;
                                                float: left;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                ControlToValidate="txtDayOff" Display="Dynamic" ValidationGroup="msg"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td>
                                        <div class="dropdown" style="float: left;">
                                            <asp:TextBox ID="txtReason" runat="server" CssClass="filter" Style="text-align: left;"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgBtnMessage" runat="server" ImageUrl="../Images/Message.png"
                                            OnClick="imgBtnMessage_Click" ValidationGroup="msg" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgTrash" runat="server" Width="24" ImageUrl="../Images/trash.png"
                                            OnClick="imgTrash_Click" />
                                    </td>
                                </tr>
                                <tr id="trManagerName_line" runat="server">
                                    <td colspan="5" style="height: 2px; background-color: #525152;">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="center">
                                        Program a Day Off
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        From/At:
                                    </td>
                                    <td align="left">
                                        <div class="textbox_small" style="float: left;">
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="filter" Style="text-align: center;
                                                float: left;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                ControlToValidate="txtStartDate" Display="Dynamic" ValidationGroup="pro"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                    <td align="right">
                                        Till:
                                    </td>
                                    <td align="left" colspan="2">
                                        <div class="textbox_small" style="float: left;">
                                            <asp:TextBox ID="txtTillDate" runat="server" CssClass="filter" Style="text-align: center;
                                                float: left;"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Enter Reason:
                                    </td>
                                    <td colspan="4">
                                        <div class="dropdown" style="float: left;">
                                            <asp:TextBox ID="txtReasonLong" runat="server" CssClass="filter" Style="text-align: left;"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="4" align="left">
                                        <asp:ImageButton ID="imgBtnProgram" runat="server" ImageUrl="../Images/btn_programme.png"
                                            ValidationGroup="pro" OnClick="imgBtnProgram_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="height: 2px; background-color: #525152;">
                                    </td>
                                </tr>
                            </table>
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
            </div>
            <div class="clear_10">
            </div>
            <div>
                Color Code Reference</div>
            <div class="clear_10">
            </div>
            <div>
                <asp:DataList ID="dlColorCodes" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                    ShowHeader="false" Width="100%" GridLines="None" OnItemDataBound="dlColorCodes_RowDataBound">
                    <ItemTemplate>
                        <div id="divColorCode" runat="server" style="width: 125px; height: 30px; text-align: center;
                            line-height: 2.5;">
                            <asp:Label ID="lblManagername" runat="server"></asp:Label>
                        </div>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:DataList>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button2"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="Button2" runat="server" Style="display: none;" />
    <asp:Panel ID="Panel1" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="3" cellspacing="3" border="0" align="center">
                        <tr>
                            <td style="text-align: center;" colspan="2">
                                <asp:Label ID="lblOffDayMessage" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ID="imgBtnYes" runat="server" ImageUrl="../images/btn_ok.png" OnClick="imgBtnYes_Click" />
                            </td>
                            <td>
                                <img src="../Images/btn_cancel.png" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="txtMessage"
                                        ErrorMessage="*" Display="Dynamic" ValidationGroup="req"></asp:RequiredFieldValidator></span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/btn_send.gif"
                                    ValidationGroup="req" OnClick="imgBtnMessageSend_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
