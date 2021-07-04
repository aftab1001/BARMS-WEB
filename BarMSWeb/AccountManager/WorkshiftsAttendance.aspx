<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AccountMaster.master"
    AutoEventWireup="true" CodeFile="WorkshiftsAttendance.aspx.cs" Inherits="AccountManager_WorkshiftsAttendance"
     %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <link type="text/css" rel="Stylesheet" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script>

    <script type="text/javascript" src="http://ajax.microsoft.com/ajax/jquery.ui/1.8.6/jquery-ui.min.js"></script>--%>
    <%--<link href="../JavaScript/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>--%>
    <link href="../JavaScript/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.watermark.js" type="text/javascript"></script>

    <script type="text/javascript">
        function  abcd() {
            //            $("#ctl00_ContentPlaceHolder1_datepicker").datepicker({
            //                showOn: "button",
            //                buttonImage: "../images/calender.png",
            //                buttonImageOnly: true
            //            });

            $('input.filter').bind('keyup blur', function() {
                if (this.value.match(/[^,.0-9]/g)) {
                    this.value = this.value.replace(/[^,.0-9]/g, '');
                }
                else {
                    if (this.value.split(',').length > 2) {
                        this.value = this.value.substring(0, this.value.lastIndexOf(','));
                    }
                }
            });
            $('.filter').watermark('00,00');
            var startDate;
            var endDate;
            var Dateselect;

            var myDay = document.getElementById("ctl00_ContentPlaceHolder1_txtDay");
            var check = document.getElementById("ctl00_ContentPlaceHolder1_txtCheck");

            if (check.value != "1") {
                $("#Calendardate").datepicker({
                    defaultDate: myDay.value + 'd',
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


        function EditRecord(id, weekpart) {
            if (weekpart == 1) {
                $("#ctl00_ContentPlaceHolder1_hidWeekPart").val(weekpart);
            }
            else {
                $("#ctl00_ContentPlaceHolder1_hidWeekPart").val(0);
            }

            var txtdel = document.getElementById("ctl00_ContentPlaceHolder1_txtdel");
            txtdel.value = id;
            __doPostBack('ctl00$ContentPlaceHolder1$btnEditRecord', '');
        }
    </script>

    <%--<asp:HiddenField ID="hidStartDate" runat="server" />
    
    <input id="hid" runat="server" type="hidden" value="" />--%>
    <%--<asp:TextBox ID="txtMonth" runat="server" Style="display: none;" Text="1"></asp:TextBox>--%>
    <asp:TextBox ID="txtDay" runat="server" Style="display: none;" Text="1"></asp:TextBox>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <%--<asp:TextBox ID="txtYear" runat="server" Style="display: none;" Text="1"></asp:TextBox>
    <asp:TextBox ID="txtDateLast" runat="server" Style="display: none;" Text="1"></asp:TextBox>--%>
    <div>
        <asp:UpdatePanel ID="upMain" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
             
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
                <asp:TextBox ID="txtCheck" runat="server" Style="display: none;" Text="1"></asp:TextBox>
               
                
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
                                <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Size="Medium" Text="Manage Attendance"></asp:Label></div>
                            <div class="clear_30">
                            </div>
                            <asp:UpdatePanel ID="upnlCalender" runat="server" UpdateMode="Conditional">
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
                                                    <%--<input id="datepicker" type="text" />--%>
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
                                                <%--<asp:ImageButton ID="btnGO" runat="server" ImageUrl="~/Images/btn_load.png" OnClick="btnGO_Click"/>--%>
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
                                <%--class="previousweek"--%>
                                <div class="previousDay" style="display: none;">
                                    <img src="../images/back_arrow.png" alt="" />
                                    <asp:LinkButton ID="lnkPrevious" runat="server" Text="Previous Day" OnClick="lnkPrevious_Click"></asp:LinkButton>
                                </div>
                                <div style="float: left; width: 880px; text-align: center; margin-top: 10px;" class="bold_label">
                                    <asp:Label ID="lblWeek" Font-Bold="true" runat="server"></asp:Label>
                                </div>
                                <div class="nextDay" style="display: none;">
                                    <img src="../images/next_arrow.png" alt="" />
                                    <asp:LinkButton ID="lnkNext" runat="server" Text="Next Day" OnClick="lnkNext_Click"></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="clear_10">
                                </div>
                                <div id="ShowWorkshift" runat="server" style="display: none;">
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
                                                <td class="whitebox_centermiddlebg">
                                                    <div class="rounded_box_big">
                                                        <asp:Panel ID="pnlUsers" runat="server" DefaultButton="imgBtnSaveTop">
                                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td align="right">
                                                                        <div>
                                                                            <div style="float: left">
                                                                                <asp:CheckBox ID="chkSalaryPaid" runat="server" Text="Weekly Salary Paid" AutoPostBack="true"
                                                                                    OnCheckedChanged="chkSalaryPaid_CheckedChanged" Style="display: none;" />
                                                                            </div>
                                                                            <div style="float: right">
                                                                                <asp:ImageButton ID="imgBtnSaveTop" runat="server" Visible="false" ImageUrl="~/Images/btn_save_account.png"
                                                                                    OnClick="imgBtnSaveTop_Click" />
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:GridView ID="GrdUsers" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                            EmptyDataText="Sorry! Workshift is not created." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                            AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowDataBound="GrdUsers_RowDataBound"
                                                                            OnRowCancelingEdit="GrdUsers_RowCancelingEdit" OnRowEditing="GrdUsers_RowEditing"
                                                                            OnRowUpdating="GrdUsers_RowUpdating" OnRowCommand="GrdUsers_RowCommand">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Position" ItemStyle-Width="90px">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblpkUserID" runat="server" Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lblpkUserWorkshiftID" runat="server" Visible="false"></asp:Label>
                                                                                        <asp:Label ID="lblSpeciality" runat="server"></asp:Label>
                                                                                        <asp:HiddenField ID="hidWorkshipid" runat="server" Value="<%#Bind('pkUserWorkshiftID') %>" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblFullName" runat="server" Style="float: left;"></asp:Label><br />
                                                                                        <asp:Label ID="lblShift" Style="float: left; cursor: pointer;" runat="server"></asp:Label>
                                                                                        <div id="divOffDay" runat="server">
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="On time" ControlStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"
                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnCheck" runat="server" ImageUrl="~/Images/check_box.png"
                                                                                            Visible="false" CommandName="chk" CommandArgument="<%#Bind('pkUserWorkshiftID') %>" />
                                                                                        <asp:CheckBox ID="chkOnTime" runat="server" Checked="<%#Bind('bOnTime') %>" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Salary (estimated)" HeaderStyle-Width="47" HeaderStyle-HorizontalAlign="Center"
                                                                                    ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAgreedSalary" runat="server" Text=""></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Penalty" ControlStyle-Width="52px" HeaderStyle-CssClass="headerstylenew"
                                                                                    HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <div class="textbox_small" style="float: left;">
                                                                                            <asp:TextBox ID="txtPenalty" Text="<%#Bind('Penalty') %>" CssClass="filter" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                        <span style="position: absolute; font-weight: bold; line-height: 3; margin-left: -10px;">
                                                                                            €</span>
                                                                                    </ItemTemplate>
                                                                                    <%-- <EditItemTemplate>
                                                                        <div class="textbox_small">
                                                                            <asp:TextBox ID="txtPenalty" onkeypress="return validate(event)" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </EditItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Bonus" ControlStyle-Width="52px" HeaderStyle-CssClass="headerstylenew"
                                                                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <div class="textbox_small" style="float: left;">
                                                                                            <asp:TextBox ID="txtBonus" Text="<%#Bind('Bonus') %>" CssClass="filter" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                        <span style="position: absolute; font-weight: bold; line-height: 3; margin-left: -10px;">
                                                                                            €</span>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
                                                                        <div class="textbox_small">
                                                                            <asp:TextBox ID="txtBonus" onkeypress="return validate(event)" runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </EditItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Notes" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                                                                    ItemStyle-Width="328px">
                                                                                    <ItemTemplate>
                                                                                        <div class="textbox330">
                                                                                            <asp:TextBox ID="txtNotes" Text="<%#Bind('sNotes') %>" runat="server"></asp:TextBox>
                                                                                        </div>
                                                                                    </ItemTemplate>
                                                                                    <%--<EditItemTemplate>
                                                                        <div class="textbox204">
                                                                            <asp:TextBox ID="txtNotes" Text=" "  onkeypress="return validatealphanumeric(event)"
                                                                                runat="server"></asp:TextBox>
                                                                        </div>
                                                                    </EditItemTemplate>--%>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/close.png" CommandArgument="<%#Bind('pkUserWorkshiftID') %>"
                                                                                            CommandName="del" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <%--<asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="~/Images/btn_save_account.png"
                                                                            CommandName="save" CommandArgument="<%#Bind('pkUserWorkshiftID') %>" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px; background-color: Gray;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:GridView ID="grdEcusers" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                            EmptyDataText="Sorry! ECUser Workshift is not created." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                            AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowCommand="grdEcusers_RowCommand"
                                                                            OnRowDataBound="grdEcusers_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="ECUsers" ItemStyle-Width="270">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEcuserName" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="60" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkOnTime" runat="server" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSalary" runat="server"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/close.png" CommandArgument="<%#Bind('ecuserid') %>"
                                                                                            Style="position: relative; right: 3px;" CommandName="del" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px; background-color: Gray;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:GridView ID="grdManagers" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                            EmptyDataText="Sorry! Manager Workshift is not created." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                            AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" OnRowCommand="grdManagers_RowCommand"
                                                                            OnRowDataBound="grdManagers_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Managers" ItemStyle-Width="270">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEcuserName" runat="server" Text="<%#Bind('FullName') %>"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="271">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSalary" runat="server" Text="<%#Bind('salary') %>"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/close.png" CommandArgument="<%#Bind('pkuserid') %>"
                                                                                            Style="position: relative; right: 3px;" CommandName="del" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 2px; background-color: Gray;">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:GridView ID="grdSpecialUsers" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                            EmptyDataText="Sorry! Special User Workshift is not created." GridLines="None"
                                                                            HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                                                            RowStyle-CssClass="rowstyle" OnRowCommand="grdSpecialUsers_RowCommand" OnRowDataBound="grdSpecialUsers_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Special Users" ItemStyle-Width="270">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblEcuserName" runat="server" Text="<%#Bind('FullName') %>"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-Width="271">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSalary" runat="server" Text="<%#Bind('salary') %>"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imgBtnDelete" runat="server" ImageUrl="~/Images/close.png" CommandArgument="<%#Bind('pkUserWorkshiftID') %>"
                                                                                            Style="position: relative; right: 3px;" CommandName="del" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <br />
                                                                        <asp:ImageButton ID="imgBtnSaveBottom" runat="server" Visible="false" ImageUrl="~/Images/btn_save_account.png"
                                                                            OnClick="imgBtnSaveTop_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <table id="tblWork" runat="server">
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
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
                                    <%--Previous week off days--%>
                                    <div class="clear_10">
                                    </div>
                                    <%--OffDay Grids--%>
                                    <div class="clear_10">
                                    </div>
                                    <div style="margin: 0 auto; width: 430px; text-align: center;">
                                        <asp:Label ID="lblOffDayPeople" runat="server" Font-Bold="true" Text="People with DAY OFF on… Sunday 20/11"></asp:Label>
                                    </div>
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
                                                    <table border="0" cellspacing="1" cellpadding="0" style="border-left: none 0; border-top: none 0;
                                                        border-bottom: none 0;" align="center">
                                                        <tr>
                                                            <td style="background: #fff; border-right: solid 1px #d6efff;">
                                                                &nbsp;
                                                            </td>
                                                            <td align="center">
                                                            <asp:HiddenField runat="server" ID="lblSundayDate2" />
                                                                <asp:Label ID="lblSundayDate1" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-right: solid 1px #d6efff;">
                                                                &nbsp;
                                                            </td>
                                                            <td class="align_left" style="text-align: center;" valign="top" style="border-bottom: solid 1px #d6efff;">
                                                                <asp:GridView ID="grdSunday" AllowSorting="True" AllowPaging="True" ShowHeader="false"
                                                                    runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="headerstyle"
                                                                    AlternatingRowStyle-CssClass="rowstyle" RowStyle-CssClass="alternate_row" CellPadding="0"
                                                                    CellSpacing="0" BorderStyle="None" BorderWidth="0" GridLines="None" 
                                                                    OnRowDataBound="grdSunday_RowDataBound" 
                                                                    onrowcommand="grdSunday_RowCommand" onrowediting="grdSunday_RowEditing">
                                                                    <Columns>
                                                                        <%--<asp:BoundField DataField="FullName" />--%>
                                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                            <asp:LinkButton runat="server" CommandName="Edit" CommandArgument="<%#Bind('pkuserid')%>" ID="lnkUserButton">
                                                                                <asp:Label ID="lnkUser" runat="server" Style="float: left;"></asp:Label></asp:LinkButton>
                                                                                <div id="mydiv" runat="server" style="float: right;">
                                                                                </div>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="3%" />
                                                                        </asp:TemplateField>
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
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label ID="lblErrorMsg" runat="server"></asp:Label>
                </div>
                
               <%-- <a id="autoload" onclick="hs.htmlExpand(this,{ contentId: 'lightbox-s' } )"></a>--%>
                <%--Jquery popup for edit time--%>
                <div class="small-lightbox-container" style="display: none;" id="Editlightbox-s">
                    <div class="lightbox-header">
                        <h3>
                            Work Shift</h3>
                        <a id="small-lightbox-close" href="#" onclick="hs.close(this)">
                            <img src="../images/lightbox-close.png" alt="stäng" title="Stäng" /></a>
                    </div>
                    <div class="small-lightbox-content" id="div2" runat="server">
                        <%--<img id="icon-check" src="../images/icon-cross.png" />--%>
                        <div>
                            <%--Pop Up Html Start--%>
                            <table cellpadding="3" cellspacing="3" border="0" align="center">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label10" runat="server" Font-Bold="true" Text="User Name :"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <div class="textbox204">
                                            <asp:Label ID="lblEditUser" runat="server"></asp:Label>
                                            <asp:TextBox ID="lblID" runat="server" Style="display: none;"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="clear_30">
                                        <asp:Label ID="lblWeekPartEdit" runat="server" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="padding-right: 15px; text-align: center;">
                                        <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="Hours"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="padding-right: 15px; text-align: center;">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="true" Text="Minutes"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Start Time"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="textbox_small">
                                            <%--<asp:TextBox ID="StartHour" runat="server" Width="30"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlStartHourEdit" runat="server" AutoPostBack="false">
                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 20px; text-align: center;">
                                        <asp:Label ID="Label14" runat="server" Font-Bold="true" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="textbox_small">
                                            <%--<asp:TextBox ID="StartMin" runat="server" Width="30"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlStartMinEdit" runat="server" AutoPostBack="false">
                                                <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label15" runat="server" Font-Bold="true" Text="End Time"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="textbox_small">
                                            <%--<asp:TextBox ID="EndHour" runat="server" Width="30"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlEndHourEdit" runat="server" AutoPostBack="false">
                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                <asp:ListItem Text="04" Value="04" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 20px; text-align: center;">
                                        <asp:Label ID="Label16" runat="server" Font-Bold="true" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="textbox_small">
                                            <%--<asp:TextBox ID="EndMin" runat="server" Width="30"></asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlEndMinEdit" runat="server" AutoPostBack="false">
                                                <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center; padding-right: 10px;">
                                        <asp:Button ID="btnEdit" UseSubmitBehavior="false" runat="server" Style="display: none;"
                                            OnClick="btnEdit_Click" />
                                        <%--<img id="abc" src="../Images/btn_save.png" alt="" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$btnSave','');" />--%>
                                        <img id="Img1" src="../Images/btn_save.png" alt="" onclick="javascript:Editme();" />
                                        <%--Pop Up HTML end--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="clear_10">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <a id="Editautoload" onclick="hs.htmlExpand(this,{ contentId: 'Editlightbox-s' } )">
                </a>
                
               
                <div style="display: none;">
                    <asp:TextBox ID="txtsh" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtsm" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txteh" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtem" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtddl" runat="server"></asp:TextBox>
                    <asp:TextBox ID="IdUser" runat="server"></asp:TextBox>
                </div>
                <div style="display: none;">
                    <asp:TextBox ID="txtSpecialityTypeID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtSp" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtdel" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtParams" runat="server"></asp:TextBox>
                    <asp:Button ID="btndel" runat="server" Text="Add" OnClick="btndel_Click" />
                    <asp:Button ID="btnEditRecord" runat="server" OnClick="btnEditRecord_Click" />
                    <asp:HiddenField ID="hidWeekPart" runat="server" />
                </div>
           </ContentTemplate>
        </asp:UpdatePanel> 
        
        <asp:HiddenField runat="server" ID="hdnDay" Value="1" />
    </div>
 <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnExtent1"
        PopupControlID="pnlAddresses" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent1" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlAddresses" runat="server">
        <asp:UpdatePanel ID="upnlAddresses" runat="server">
            <ContentTemplate>
            
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <%--<div class="lightbox-header">
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender1.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>--%>
                 <div class="lightbox-header">
                        <h3>
                            Work Shift</h3>
                        <a id="A2" href="#" style="float:right;  margin-right: 6px;margin-top: 8px;" onclick="$find('<%=ModalPopupExtender1.ClientID %>').hide();return false;">
                            <img src="../images/lightbox-close.png" alt="stäng" title="Stäng" /></a>
                    </div>
                    <div class="small-lightbox-content" style=" background-color: #FFFFFF;" id="div4" runat="server">
                        <%--<img id="icon-check" src="../images/icon-cross.png" />--%>
                        <div>
                            <%--Pop Up Html Start--%>
                            <table cellpadding="3" cellspacing="3" border="0" align="center">
                                <tr>
                                    <td>
                                    <asp:HiddenField runat="server" ID = "WorkassinUserID" Value="1" />
                                        <asp:Label ID="Label19" runat="server" Font-Bold="true" Text="User Name :"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <div class="textbox204">
                                            <asp:Label ID="Label20" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtUserName" runat="server" Style=""></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                <td style="font-weight:bold;">
                                Position:
                                </td>
                                <td colspan="3">
                                <div class="textbox204">
                                <asp:DropDownList runat="server" ID="dlUserSpitilaty" >
                                
                                </asp:DropDownList>
                                </div>
                                </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="clear_30">
                                        <asp:Label ID="lblDayDate" runat="server" ForeColor="Green"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="padding-right: 15px; text-align: center;">
                                        <asp:Label ID="Label22" runat="server" Font-Bold="true" Text="Hours"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="padding-right: 15px; text-align: center;">
                                        <asp:Label ID="Label23" runat="server" Font-Bold="true" Text="Minutes"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label24" runat="server" Font-Bold="true" Text="Start Time"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="textbox_small">
                                            <%--<asp:TextBox ID="StartHour" runat="server" Width="30"></asp:TextBox>--%>
                                            <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="false">
                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 20px; text-align: center;">
                                        <asp:Label ID="Label25" runat="server" Font-Bold="true" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="textbox_small">
                                            <%--<asp:TextBox ID="StartMin" runat="server" Width="30"></asp:TextBox>--%>
                                            <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="false">
                                                <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label26" runat="server" Font-Bold="true" Text="End Time"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="textbox_small">
                                            <%--<asp:TextBox ID="EndHour" runat="server" Width="30"></asp:TextBox>--%>
                                            <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="false">
                                                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                                                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                                                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                                                <asp:ListItem Text="04" Value="04" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                                                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                                                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                                                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                                                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td style="width: 20px; text-align: center;">
                                        <asp:Label ID="Label27" runat="server" Font-Bold="true" Text=":"></asp:Label>
                                    </td>
                                    <td>
                                        <div class="textbox_small">
                                            <%--<asp:TextBox ID="EndMin" runat="server" Width="30"></asp:TextBox>--%>
                                            <asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="false">
                                                <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center; padding-right: 10px;">
                                        <asp:Button ID="btnSave" UseSubmitBehavior="false" runat="server" Style="display: none;"
                                            OnClick="btnSave_Click" />
                                        <%--<img id="abc" src="../Images/btn_save.png" alt="" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$btnSave','');" />--%>
                                        <img id="Img3" src="../Images/btn_save.png" alt="" onclick="javascript:EditmeNew();" />
                                        <%--Pop Up HTML end--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="clear_10">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <script language="javascript" type="text/javascript">
     function EditmeNew() {
            var sh = document.getElementById("ctl00_ContentPlaceHolder1_txtsh");
            //var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_StartHour");
            var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList5");
            //sh.value = oldsh.value;
            sh.value = oldsh.options[oldsh.selectedIndex].text;

            var sm = document.getElementById("ctl00_ContentPlaceHolder1_txtsm");
            //var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_StartMin");
            var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList6");
            //sm.value = oldsm.value;
            sm.value = oldsm.options[oldsm.selectedIndex].text;

            var eh = document.getElementById("ctl00_ContentPlaceHolder1_txteh");
            //    var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_EndHour");
            //    eh.value = oldeh.value;
            var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList7");
            eh.value = oldeh.options[oldeh.selectedIndex].text;

            var em = document.getElementById("ctl00_ContentPlaceHolder1_txtem");
            //    var oldem = document.getElementById("ctl00_ContentPlaceHolder1_EndMin");
            //    em.value = oldem.value;
            var oldem = document.getElementById("ctl00_ContentPlaceHolder1_DropDownList8");
            em.value = oldem.options[oldem.selectedIndex].text;

            var e = document.getElementById("ctl00_ContentPlaceHolder1_dlUserSpitilaty");
            var txtddl = document.getElementById("ctl00_ContentPlaceHolder1_txtddl");
            //alert(e.value);
            txtddl.value = e.value;
             var e = document.getElementById("ctl00_ContentPlaceHolder1_WorkassinUserID");
            var txtIdUser = document.getElementById("ctl00_ContentPlaceHolder1_IdUser");
            //alert(e.value);
            txtIdUser.value = e.value;

            __doPostBack('ctl00$ContentPlaceHolder1$btnSave', '');
        }

        function Editme() {
            var sh = document.getElementById("ctl00_ContentPlaceHolder1_txtsh");
            //var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_StartHour");
            var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_ddlStartHourEdit");
            //sh.value = oldsh.value;
            sh.value = oldsh.options[oldsh.selectedIndex].text;

            var sm = document.getElementById("ctl00_ContentPlaceHolder1_txtsm");
            //var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_StartMin");
            var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_ddlStartMinEdit");
            //sm.value = oldsm.value;
            sm.value = oldsm.options[oldsm.selectedIndex].text;

            var eh = document.getElementById("ctl00_ContentPlaceHolder1_txteh");
            //    var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_EndHour");
            //    eh.value = oldeh.value;
            var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndHourEdit");
            eh.value = oldeh.options[oldeh.selectedIndex].text;

            var em = document.getElementById("ctl00_ContentPlaceHolder1_txtem");
            //    var oldem = document.getElementById("ctl00_ContentPlaceHolder1_EndMin");
            //    em.value = oldem.value;
            var oldem = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndMinEdit");
            em.value = oldem.options[oldem.selectedIndex].text;

            var e = document.getElementById("ctl00_ContentPlaceHolder1_lblID");
            var txtddl = document.getElementById("ctl00_ContentPlaceHolder1_txtddl");
            txtddl.value = e.value;

            __doPostBack('ctl00$ContentPlaceHolder1$btnEdit', '');
        }

        function callme() {
            var sh = document.getElementById("ctl00_ContentPlaceHolder1_txtsh");
            //var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_StartHour");
            var oldsh = document.getElementById("ctl00_ContentPlaceHolder1_ddlStartHour");
            //sh.value = oldsh.value;
            sh.value = oldsh.options[oldsh.selectedIndex].text;

            var sm = document.getElementById("ctl00_ContentPlaceHolder1_txtsm");
            //var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_StartMin");
            var oldsm = document.getElementById("ctl00_ContentPlaceHolder1_ddlStartMin");
            //sm.value = oldsm.value;
            sm.value = oldsm.options[oldsm.selectedIndex].text;

            var eh = document.getElementById("ctl00_ContentPlaceHolder1_txteh");
            //    var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_EndHour");
            //    eh.value = oldeh.value;
            var oldeh = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndHour");
            eh.value = oldeh.options[oldeh.selectedIndex].text;

            var em = document.getElementById("ctl00_ContentPlaceHolder1_txtem");
            //    var oldem = document.getElementById("ctl00_ContentPlaceHolder1_EndMin");
            //    em.value = oldem.value;
            var oldem = document.getElementById("ctl00_ContentPlaceHolder1_ddlEndMin");
            em.value = oldem.options[oldem.selectedIndex].text;

            var e = document.getElementById("ctl00_ContentPlaceHolder1_ddlusers");
            var txtddl = document.getElementById("ctl00_ContentPlaceHolder1_txtddl");
            txtddl.value = e.options[e.selectedIndex].value;

            __doPostBack('ctl00$ContentPlaceHolder1$btnSave', '');
        }

        function DefaultTime() {
            alert('a');
            __doPostBack('ctl00$ContentPlaceHolder1$Button1', '');
        }
    
    </script>

</asp:Content>
