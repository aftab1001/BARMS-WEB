<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DepartmentAdminMaster.master"
    AutoEventWireup="true" CodeFile="Payments.aspx.cs" Inherits="DepartmentAdmin_Payments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../JavaScript/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.price_format.1.7.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.watermark.js" type="text/javascript"></script>

    <script>
        $(function() {
            ApplyJquery();
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



        });
        function userAbsent(message) {

            alert(message);
        }

        function printreport(url) {

            var hidParam = document.getElementById("ctl00_ContentPlaceHolder1_hidParam");
            var a = window.open(url, 'print', 'status=0,toolbar=0,location=0,scrollbars = 1,resizable=1,menubar=0,width=700,height=700');
        }
        function ShowDataPicker() {
            $("#ctl00_ContentPlaceHolder1_txtFromDate").datepicker({
                dateFormat: 'dd/mm/yy',
                yearRange: "2000:2010"
            });

            $("#ctl00_ContentPlaceHolder1_txtTillDate").datepicker({
                dateFormat: 'dd/mm/yy'
            });
        }
        function ApplyJquery() {
            $('.tab1').click(function() {

                $('#ctl00_ContentPlaceHolder1_divTab1').show();
                $('#ctl00_ContentPlaceHolder1_divTab2').hide();
                $('#ctl00_ContentPlaceHolder1_divTab3').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').hide();

                $(this).css("background-color", "#BFB8BB");
                $('.tab2').css("background-color", "#e8e8e8");
                $('.tab3').css("background-color", "#e8e8e8");
                $('.tab4').css("background-color", "#e8e8e8");
            });

            $('.tab2').click(function() {

                $('#ctl00_ContentPlaceHolder1_divTab2').show();
                $('#ctl00_ContentPlaceHolder1_divTab1').hide();
                $('#ctl00_ContentPlaceHolder1_divTab3').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').hide();
                $(this).css("background-color", "#BFB8BB");
                $('.tab1').css("background-color", "#e8e8e8");
                $('.tab3').css("background-color", "#e8e8e8");
                $('.tab4').css("background-color", "#e8e8e8");
            });

            $('.tab3').click(function() {

                $('#ctl00_ContentPlaceHolder1_divTab3').show();
                $('#ctl00_ContentPlaceHolder1_divTab1').hide();
                $('#ctl00_ContentPlaceHolder1_divTab2').hide();
                $('#ctl00_ContentPlaceHolder1_divTab4').hide();
                $(this).css("background-color", "#BFB8BB");
                $('.tab1').css("background-color", "#e8e8e8");
                $('.tab2').css("background-color", "#e8e8e8");
                $('.tab4').css("background-color", "#e8e8e8");
            });

            $('.tab4').click(function() {

                $('#ctl00_ContentPlaceHolder1_divTab4').show();
                $('#ctl00_ContentPlaceHolder1_divTab3').hide();
                $('#ctl00_ContentPlaceHolder1_divTab1').hide();
                $('#ctl00_ContentPlaceHolder1_divTab2').hide();
                $(this).css("background-color", "#BFB8BB");
                $('.tab1').css("background-color", "#e8e8e8");
                $('.tab2').css("background-color", "#e8e8e8");
                $('.tab3').css("background-color", "#e8e8e8");
            });
        }
        function SalaryTab() {
            $('#ctl00_ContentPlaceHolder1_divTab2').show();
            $('#ctl00_ContentPlaceHolder1_divTab1').hide();
            $('#ctl00_ContentPlaceHolder1_divTab3').hide();
            $('#ctl00_ContentPlaceHolder1_divTab4').hide();
            $(".tab2").css("background-color", "#BFB8BB");
            $('.tab1').css("background-color", "#e8e8e8");
            $('.tab3').css("background-color", "#e8e8e8");
            $('.tab4').css("background-color", "#e8e8e8");
        }
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

    <asp:HiddenField ID="hidChange" runat="server" Value="0" />
    <asp:TextBox ID="txtMyDay" runat="server" Style="display: none;"></asp:TextBox>
    <asp:HiddenField ID="hidParam" runat="server" Value="" />
    <div id="divBackground" style="width: 100%; height: 100%; background-color: Black;
        opacity: .5; position: absolute; left: 0; top: 0; bottom: 0; right: 0; display: none;">
    </div>
    <asp:TextBox ID="txtDay" runat="server" Style="display: none;" Text="-1"></asp:TextBox>
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
    <asp:TextBox ID="txtCheck" runat="server" Style="display: none;" Text="1"></asp:TextBox>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
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
                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Size="Medium" Text="Manage Payments"></asp:Label></div>
                <div class="clear_30">
                </div>
                <asp:UpdatePanel ID="upnlNew" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                    <ContentTemplate>
                        <table cellpadding="1" cellspacing="1" border="0" align="center">
                            <tr>
                                <td colspan="3" style="text-align: center;">
                                    <asp:Label ID="lblError" runat="server" CssClass="Error" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: right;">
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text=""></asp:Label>
                                </td>
                                <td>
                                    <div class="demo textbox_date_new">
                                        <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="datepicker"
                                        CssClass="Error" ErrorMessage="Date Required" ValidationGroup="load"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td colspan="3" style="height: 5px;">
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnGO" runat="server" OnClick="btnGO_Click" UseSubmitBehavior="false" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <div>
                                        <div id="Calendardate">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
    <div>
        <table width="100%">
            <tr>
                <td colspan="7" valign="top">
                    <div class="previousDay" style="display: none;">
                        <img src="../images/back_arrow.png" alt="" />
                    </div>
                    <div style="float: left; width: 880px; text-align: center; margin-top: 10px;" class="bold_label">
                        <asp:Label ID="lblWeek" Font-Bold="true" runat="server"></asp:Label>
                    </div>
                    <div class="nextDay" style="display: none;">
                        <img src="../images/next_arrow.png" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="clear_10">
                    </div>
                    <div id="ShowWorkshift" runat="server" style="display: block;">
                        <div class="rounded_box_big">
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
                                    <td class="whitebox_centermiddlebg" style="padding: 0;">
                                        <div class="rounded_box_big">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td valign="top">
                                                        <div style="background-color: Transparent; width: 860px;">
                                                            <div style="background-color: Transparent; width: 860px; float: left;">
                                                                <div style="background-color: #BFB8BB; width: 150px; height: 40px; line-height: 36px;
                                                                    float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab1">
                                                                    Reference</div>
                                                                <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                                    float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab2">
                                                                    Salaries</div>
                                                                <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                                    float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab3">
                                                                    Receipts Printout</div>
                                                                <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                                    display: none; float: left; margin-right: 5px; text-align: center; cursor: pointer;"
                                                                    class="tab4">
                                                                    Expenses</div>
                                                            </div>
                                                            <div style="background-color: Transparent; width: 860px; float: left;">
                                                                <div id="divTab1" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                    width: auto; display: block; padding-left: 10px;">
                                                                    <asp:UpdatePanel ID="upnlReference" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table border="0" cellpadding="5" cellspacing="0" align="center">
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        From:
                                                                                    </td>
                                                                                    <td colspan="2" align="left">
                                                                                        Till:
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <div class="textbox115" style="float: left;">
                                                                                            <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtFromDate"
                                                                                            ErrorMessage="*" ValidationGroup="rmm" Display="Dynamic" Style="line-height: 38px;"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <div class="textbox115" style="float: left;">
                                                                                            <asp:TextBox ID="txtTillDate" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtTillDate"
                                                                                            ErrorMessage="*" ValidationGroup="rmm" Display="Dynamic" Style="line-height: 38px;"></asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ImageButton ID="imgBtnFilter" runat="server" ImageUrl="../Images/btn_filter.png"
                                                                                            ValidationGroup="rmm" OnClick="imgBtnFilter_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="3" align="center">
                                                                                        <asp:Label ID="lblDatePeriod" runat="server" Text="" Style="color: #979fa8; font-style: italic;
                                                                                            position: relative; margin-right: 88px;"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="3" style="height: 20px;">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table border="0" cellpadding="0" cellspacing="0" align="center">
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
                                                                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                                                                        <div class="clear_10">
                                                                                                        </div>
                                                                                                        <asp:GridView ID="grdWeeklyReference" runat="server" AutoGenerateColumns="false"
                                                                                                            Width="100%" EmptyDataText="No Record Found!." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                                            AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowDataBound="grdWeeklyReference_RowDataBound"
                                                                                                            OnRowCommand="grdWeeklyReference_RowCommand">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="Week#" ItemStyle-Width="360">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblDate" runat="server" Text="<%#Bind('iWeekNumber') %>" Style="display: none;"></asp:Label>
                                                                                                                        <asp:LinkButton ID="lnkDate" runat="server" CommandName="week" CommandArgument="<%#Bind('iWeekNumber') %>"></asp:LinkButton>
                                                                                                                        <asp:HiddenField ID="hidWeekStart" runat="server" Value="<%#Bind('dWeekStartDate') %>" />
                                                                                                                        <asp:HiddenField ID="hidWeekEnd" runat="server" Value="<%#Bind('dWeekEndDate') %>" />
                                                                                                                        <asp:HiddenField ID="hidStatus" runat="server" Value="<%#Bind('SalaryStatus') %>" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Total Salaries" ItemStyle-Width="130" ItemStyle-HorizontalAlign="Right">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblTotalSalaries" runat="server" Text="<%#Bind('salary') %>" Style="position: relative;
                                                                                                                            margin-right: 40px;"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Paid" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Right">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkPaid" runat="server" Enabled="false" OnCheckedChanged="chkPaid_Clicked"
                                                                                                                            AutoPostBack="true" />
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Allow Edit" ItemStyle-HorizontalAlign="Right" >
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:CheckBox ID="chkAllow" runat="server" OnCheckedChanged="chkAllow_Clicked" AutoPostBack="true" />
                                                                                                                    </ItemTemplate>
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
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                     <asp:UpdateProgress ID="upProgressPopUp" runat="server" AssociatedUpdatePanelID="upnlReference">
                                                                        <ProgressTemplate>
                                                                            <div id="imgloadar" class="loader" style="position: relative;margin-left:365px;bottom:200px;">
                                                                                <img src="../Images/img_Loader.gif" alt="Please Wait ..." />
                                                                            </div>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </div>
                                                                <div id="divTab2" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                    width: auto; padding-top: 10px; display: none;">
                                                                    <asp:UpdatePanel ID="upnlSalaries" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table id="tblSalary" runat="server" border="0" cellpadding="5" cellspacing="0" align="center"
                                                                                width="100%" visible="false">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:GridView ID="grdSalaries" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                            EmptyDataText="No Record Found!." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                            AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowDataBound="grdSalaries_RowDataBound"
                                                                                            OnRowCommand="grdSalaries_RowCommand">
                                                                                            <Columns>
                                                                                                <asp:TemplateField HeaderText="Name">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnkName" runat="server" CommandName="user" CommandArgument="<%#Bind('pkuserid') %>"
                                                                                                            Text="<%#Bind('FullName') %>"></asp:LinkButton>
                                                                                                        <asp:HiddenField ID="hidWeekStart" runat="server" Value="<%#Bind('dWeekStartDate') %>" />
                                                                                                        <asp:HiddenField ID="hidWeekEnd" runat="server" Value="<%#Bind('dWeekEndDate') %>" />
                                                                                                        <asp:HiddenField ID="hidUserIDCheck" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:Label ID="lblSundayHeader" runat="server"></asp:Label>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblSundaySalary" runat="server" Text="<%#Bind('Sunday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:Label ID="lblMondayHeader" runat="server"></asp:Label>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblMondaySalary" runat="server" Text="<%#Bind('Monday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:Label ID="lblTuesdayHeader" runat="server"></asp:Label>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblTuesdaySalary" runat="server" Text="<%#Bind('Tuesday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:Label ID="lblWednesdayHeader" runat="server"></asp:Label>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblWednesdaySalary" runat="server" Text="<%#Bind('Wednesday') %>"
                                                                                                            Style="margin-right: 27px;"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:Label ID="lblThursdayHeader" runat="server"></asp:Label>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblThursdaySalary" runat="server" Text="<%#Bind('Thursday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:Label ID="lblFridayHeader" runat="server"></asp:Label>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblFridaySalary" runat="server" Text="<%#Bind('Friday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:Label ID="lblSaturdayHeader" runat="server"></asp:Label>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblSaturdaySalary" runat="server" Text="<%#Bind('Saturday') %>" Style="margin-right: 27px;"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Week's Subtotal" HeaderStyle-HorizontalAlign="Right"
                                                                                                    ItemStyle-HorizontalAlign="Right">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblWeekSubtotal" runat="server" Style="margin-right: 17px;"></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Center">
                                                                                                    <HeaderTemplate>
                                                                                                        Paid
                                                                                                        <asp:LinkButton ID="lnkSelectAll" runat="server" Text="(Select All)" OnClick="lnkSelectAll_Click"></asp:LinkButton>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="chkPaid" runat="server" Enabled="false" />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Total Salaries:
                                                                                                </td>
                                                                                                <td>
                                                                                                    <div class="textbox_small">
                                                                                                        <asp:TextBox ID="txtTotal" runat="server" Style="text-align: right;" ReadOnly="true"></asp:TextBox>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trMark" runat="server" visible="false">
                                                                                    <td align="right">
                                                                                        <asp:LinkButton ID="lnkMarkAsPaid" runat="server" Style="text-decoration: none;"
                                                                                            OnClick="lnkMarkAsPaid_Click">
                                                                                <div style="background-image:url(../Images/textbox_red.png);width:115px;height:32px;font-family:Arial;color:White;text-align:center;line-height:29px;float:right;">
                                                                                Mark as Paid
                                                                                </div>
                                                                                    
                                                                                        </asp:LinkButton>
                                                                                        <asp:ImageButton ID="imgBtnMarkAsPaid" runat="server" ImageUrl="../Images/textbox_red.png"
                                                                                            Style="display: none;" OnClick="imgBtnMarkAsPaid_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trChecked" runat="server" visible="false">
                                                                                    <td align="right">
                                                                                        <asp:CheckBox ID="chkSalaryPaid" runat="server" Checked="true" Enabled="false" Text="Week's Salary Paid" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trAsk" runat="server" visible="false">
                                                                                    <td align="right">
                                                                                        <asp:LinkButton ID="lnkAskForEdit" runat="server" Style="text-decoration: none;"
                                                                                            OnClick="lnkAskForEdit_Click">
                                                                                <div style="background-image:url(../Images/textbox_red.png);width:115px;height:32px;font-family:Arial;color:White;text-align:center;line-height:29px;float:right;">
                                                                                Ask For Edits!
                                                                                </div>
                                                                                    
                                                                                        </asp:LinkButton>
                                                                                        <asp:ImageButton ID="imgBtnAskForEdit" runat="server" AlternateText="Ask For Edit"
                                                                                            ImageUrl="../Images/ss.png" Style="display: none;" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div id="divTab3" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                    width: auto; display: none; padding: 10px; padding-top: 15px;">
                                                                    <asp:UpdatePanel ID="upnlPrint" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <fieldset>
                                                                                            <legend>Receipts </legend>
                                                                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                                <tr>
                                                                                                    <td colspan="6" align="center" style="padding-bottom: 20px;">
                                                                                                        <asp:Label ID="lblSelected" runat="server" Text="No Receipt Found!"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trPrint" runat="server" visible="false">
                                                                                                    <td>
                                                                                                        <div class="textbox204">
                                                                                                            <asp:DropDownList ID="ddlPaperSize" runat="server">
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td style="width: 2px; height: 20px; background-color: Gray;">
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:LinkButton ID="lnkPrintAll" runat="server" Style="text-decoration: none;" OnClick="lnkPrintAll_Click">
                                                                                <div style="background-image:url(../Images/textbox_red.png);width:115px;height:32px;font-family:Arial;color:white;text-align:center;line-height:29px;float:right;">
                                                                                Print All Receipts
                                                                                </div>
                                                                                    
                                                                                                        </asp:LinkButton>
                                                                                                    </td>
                                                                                                    <td style="width: 2px; height: 20px; background-color: Gray;">
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <div class="textbox204">
                                                                                                            <asp:DropDownList ID="ddlSelectReceipt" runat="server">
                                                                                                            </asp:DropDownList>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:LinkButton ID="lnkPrint" runat="server" Style="text-decoration: none;" OnClick="lnkPrint_Click">
                                                                                <div style="background-image:url(../Images/textbox_red.png);width:115px;height:32px;font-family:Arial;color:white;text-align:center;line-height:29px;float:right;">
                                                                                Print
                                                                                </div>
                                                                                    
                                                                                                        </asp:LinkButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </fieldset>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="display: none;">
                                                                                    <td>
                                                                                        <fieldset>
                                                                                            <legend>Envelops</legend>
                                                                                        </fieldset>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="display: none;">
                                                                                    <td>
                                                                                        <fieldset>
                                                                                            <legend>Label Stickers</legend>
                                                                                        </fieldset>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div id="divTab4" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                    width: auto; display: none; padding: 10px; padding-top: 15px;">
                                                                </div>
                                                            </div>
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
                        </div>
                        <div class="clear_10">
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
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
                                            font-size: 12px; background: ; margin-top: 8px; width: 415px;"> </asp:TextBox></div>
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
        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="Button1"
            PopupControlID="pnlPayment" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <asp:Button ID="Button1" runat="server" Style="display: none;" />
        <asp:Panel ID="pnlPayment" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                    <div class="lightbox-header" style="width: 762px !important">
                        <asp:Label ID="lblPopupTitle" runat="server" Style="color: White; font-size: 15px;
                            font-weight: bold; line-height: 36px; padding-left: 10px;"></asp:Label>
                        <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender2.ClientID %>').hide();return false;">
                            <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                    <div class="small-lightbox-content" style="background-color: White; text-align: center;
                        width: 742px !important">
                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr id="trEdit" runat="server" visible="false">
                                <td>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" align="center">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblRecordMessage" runat="server" Text="Successfully Updated" Style="font-size: 15px;
                                                    font-weight: bold; color: Green; display: none;"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblEditItem" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <div class="textbox_small">
                                                    <asp:TextBox ID="txtEditItem" runat="server" Style="text-align: right;"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnSaveEdit" runat="server" ImageUrl="../Images/btn_save_account.png"
                                                    OnClick="imgBtnSaveEdit_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnCancelEdit" runat="server" ImageUrl="../Images/btn_cancel.png"
                                                    OnClick="imgBtnCancelEdit_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="height: 2px; background-color: Gray;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="grdPaymentForSingleUser" runat="server" AutoGenerateColumns="false"
                                        ShowFooter="true" Width="100%" EmptyDataText="No Record Found!." GridLines="None"
                                        HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                        RowStyle-CssClass="rowstyle" OnRowDataBound="grdPaymentForSingleUser_RowDataBound"
                                        OnRowCommand="grdPaymentForSingleUser_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Text="<%#Bind('Type') %>"></asp:Label>
                                                    <asp:HiddenField ID="hidWeekStart" runat="server" Value="<%#Bind('dWeekStartDate') %>" />
                                                    <asp:HiddenField ID="hidWeekEnd" runat="server" Value="<%#Bind('dWeekEndDate') %>" />
                                                    <asp:HiddenField ID="hidUserID" runat="server" Value="<%#Bind('pkuserid') %>" />
                                                    <asp:LinkButton ID="lnkName" runat="server" Text="<%#Bind('FullName') %>" Visible="false"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSundayHeader" runat="server"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSundaySalary" runat="server" Text="<%#Bind('Sunday') %>" Style="margin-right: 27px;
                                                        display: none;"></asp:Label>
                                                    <asp:LinkButton ID="lnkSunday" runat="server" Text="<%#Bind('Sunday') %>" Style="margin-right: 27px;"
                                                        CommandName="sunday" CommandArgument="<%#Bind('pkuserid') %>"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblMondayHeader" runat="server"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMondaySalary" runat="server" Text="<%#Bind('Monday') %>" Style="margin-right: 27px;
                                                        display: none;"></asp:Label>
                                                    <asp:LinkButton ID="lnkMonday" runat="server" Text="<%#Bind('Monday') %>" Style="margin-right: 27px;"
                                                        CommandName="monday" CommandArgument="<%#Bind('pkuserid') %>"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblTuesdayHeader" runat="server"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTuesdaySalary" runat="server" Text="<%#Bind('Tuesday') %>" Style="margin-right: 27px;
                                                        display: none;"></asp:Label>
                                                    <asp:LinkButton ID="lnkTuesday" runat="server" Text="<%#Bind('Tuesday') %>" Style="margin-right: 27px;"
                                                        CommandName="tuesday" CommandArgument="<%#Bind('pkuserid') %>"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblWednesdayHeader" runat="server"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWednesdaySalary" runat="server" Text="<%#Bind('Wednesday') %>"
                                                        Style="margin-right: 27px; display: none;"></asp:Label>
                                                    <asp:LinkButton ID="lnkWednesday" runat="server" Text="<%#Bind('Wednesday') %>" Style="margin-right: 27px;"
                                                        CommandName="wednesday" CommandArgument="<%#Bind('pkuserid') %>"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblThursdayHeader" runat="server"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblThursdaySalary" runat="server" Text="<%#Bind('Thursday') %>" Style="margin-right: 27px;
                                                        display: none;"></asp:Label>
                                                    <asp:LinkButton ID="lnkThursday" runat="server" Text="<%#Bind('Thursday') %>" Style="margin-right: 27px;"
                                                        CommandName="thursday" CommandArgument="<%#Bind('pkuserid') %>"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblFridayHeader" runat="server"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFridaySalary" runat="server" Text="<%#Bind('Friday') %>" Style="margin-right: 27px;
                                                        display: none;"></asp:Label>
                                                    <asp:LinkButton ID="lnkFriday" runat="server" Text="<%#Bind('Friday') %>" Style="margin-right: 27px;"
                                                        CommandName="friday" CommandArgument="<%#Bind('pkuserid') %>"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSaturdayHeader" runat="server"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSaturdaySalary" runat="server" Text="<%#Bind('Saturday') %>" Style="margin-right: 27px;
                                                        display: none;"></asp:Label>
                                                    <asp:LinkButton ID="lnkSaturday" runat="server" Text="<%#Bind('Saturday') %>" Style="margin-right: 27px;"
                                                        CommandName="saturday" CommandArgument="<%#Bind('pkuserid') %>"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Payment
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblTotal" runat="server" Text="Total" Style="position: relative; right: 20px;
                                                        text-align: right;"></asp:Label>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWeekSubtotal" runat="server" Style="margin-right: 17px;"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalPayment" runat="server" Text="180  €" Style="position: relative;
                                                        text-align: right;"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:CheckBox ID="chkAlreadyPaid" runat="server" Enabled="false" Text="Already Paid" />
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
