<%@ Page Language="C#" MasterPageFile="~/MasterPages/DepartmentAdminMaster.master" AutoEventWireup="true" CodeFile="ManageAdminExpenses.aspx.cs" Inherits="DepartmentAdmin_ManageAdminExpenses"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <link href="../JavaScript/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.price_format.1.7.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.watermark.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function() {
            //var height = document.body.offsetHeight;
            //var width = document.body.offsetWidth;
            var height = document.body.clientWidth;
            var width = document.body.clientHeight;


            //var change = 0;  // show/hide popup dialog box for save changing for daily icome

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
            $("#ctl00_ContentPlaceHolder1_imgAddSpecialIncome").click(function() {
                $("#ctl00_ContentPlaceHolder1_trOtherIncome").show();
                return true;
            });

            $('.filter').change(function() {
                $("#ctl00_ContentPlaceHolder1_hidChange").val(1);
            });
            $('.note').change(function() {
                $("#ctl00_ContentPlaceHolder1_hidChange").val(1);
            });

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
                        // __doPostBack('ctl00$ContentPlaceHolder1$upnlGo', '');
                        //SaveHelp();
                    },

                    beforeShowDay: function(date) {
                        var cssClass = '';

                        if (date >= startDate && date <= endDate)
                            cssClass = 'ui-datepicker-current-day';
                        //cssClass = '';
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
                        // __doPostBack('ctl00$ContentPlaceHolder1$upnlGo', '');
                        //SaveHelp();
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



            $('.filter').watermark('00,00');
            $('#ctl00_ContentPlaceHolder1_txtIncomeAmount').watermark('00,00');
           



        });
        </script>
        <script>
         function CorrectValue(obj) {
            $('#' + obj.id).bind('keyup blur', function() {
                if (this.value.match(/[^,.0-9]/g)) {
                    this.value = this.value.replace(/[^,.0-9]/g, '');
                }
                else {
                    if (this.value.split(',').length > 2) {
                        this.value = this.value.substring(0, this.value.lastIndexOf(','));
                    }
                }
            });
        }

        function changeVal() {

            $("#ctl00_ContentPlaceHolder1_hidChange").val(0);

        }
        
        </script>
        <asp:HiddenField ID="hidChange" runat="server" Value="0" />
    <div id="divBackground" style="width: 100%; height: 100%; background-color: Black;
        opacity: .5; position: absolute; left: 0; top: 0; bottom: 0; right: 0; display: none;">
    </div>
    <asp:TextBox ID="txtDay" runat="server" Style="display: none;" Text="-1"></asp:TextBox>
    <asp:HiddenField ID="hdnUserCount" runat="server" />
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
                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Font-Size="Medium" Text="Manage Admin Expense"></asp:Label></div>
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
      <table width="100%">
      
            <tr>
                <td colspan="7" valign="top">
                    <div class="previousDay" style="display: none;">
                        <img src="../images/back_arrow.png" alt="" />
                    </div>
                    <div style="float: left; width: 880px; text-align: center; margin-top: 10px;" class="bold_label">
                        <%--<asp:UpdatePanel ID="upnlDate" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                        <asp:Label ID="lblWeek" Font-Bold="true" runat="server"></asp:Label>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                    <div class="nextDay" style="display: none;">
                        <img src="../images/next_arrow.png" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
            <%--<tr>
            <td class="whitebox_topleftcorner">
            </td>
            <td class="whitebox_top_bg">
            </td>
            <td class="whitebox_toprightcorner">
            </td>
        </tr>--%>
            <td colspan="7" valign="top" >
            <table width="100%">
            
                                                                                    <tr id="trMessageExpense" runat="server" visible="false">
                                                                                        <td colspan="4">
                                                                                            <h3>
                                                                                                <asp:Label ID="lblMessageExpense" runat="server"></asp:Label>
                                                                                            </h3>
                                                                                        </td>
                                                                                    </tr>
                                                                                    
                                                                                    
                                                                                    <tr>
                                                                                        <td colspan="4" style="height: 4px;">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trLineBeforeExpense" runat="server">
                                                                                        <td colspan="4" >
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr id="trExpense" runat="server" visible="true">
                                                                                        <td style="width: 207px;" valign="bottom">
                                                                                            <div>
                                                                                                Expense Name:
                                                                                                <div class="textbox204" style="text-align: left; padding-left: 10px;">
                                                                                                    <asp:TextBox ID="txtExpenseName" runat="server"></asp:TextBox>   
                                                                                                </div>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td style="width: 109px;">
                                                                                            Expense Amount:
                                                                                            <div class="textbox_small" style="float: left; margin-left: 2px;">
                                                                                                <asp:TextBox ID="txtEnpenseAmount" runat="server" Style="text-align: center;" CssClass="filter"
                                                                                                    onkeypress="javascript:CorrectValue(this);" onchange="javascript:fixedDecimalPlace2(this);"></asp:TextBox>
                                                                                            </div>
                                                                                            <span style="position: relative; float: right; left: -20px; line-height: 31px;">€</span>
                                                                                        </td>
                                                                                        <td valign="bottom" style="width: 333px;">
                                                                                            Notes:
                                                                                            <div class="textbox330">
                                                                                                <asp:TextBox ID="txtNoteExpense" runat="server"></asp:TextBox>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td valign = "bottom"  >
                                                                                      <asp:ImageButton ID="imgBtnSaveExpense" runat="server" ImageUrl="~/Images/btn_save_account.png"
                                                                                                OnClick="imgBtnSaveExpense_Click" />
                                                                                            <asp:ImageButton ID="imgBtnEditExpense" runat="server" ImageUrl="~/Images/btn_edi_smallt.gif"
                                                                                                OnClick="imgBtnEditExpense_Click" />
                                                                                            <asp:ImageButton ID="imgBtnCancelExpense" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                                                                                OnClick="imgBtnCancelExpense_Click" />
                                                                                    </td>
                                                                                      
                                                                                    </tr>
                                                                                   
                                                                                    <tr id="trLineAfterExpense" runat="server" visible="false">
                                                                                        <td colspan="4" style="background-color: #bfb8bb">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="4">
                                                                                       
                                                                                        
                                                                                        
                                                                                            <asp:GridView ID="grdExpenses" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                                EmptyDataText="No Other Expense added yet." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                                AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                                                                OnRowCommand="grdExpenses_RowCommand" OnRowDataBound="grdExpenses_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="Expense Name">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkExpenseType" runat="server" CommandName="expensetype" CommandArgument="<%#Bind('ExpId') %>"
                                                                                                                Text="<%#Bind('ExpenseName') %>"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Expense" ItemStyle-Width="100">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkExpense" runat="server" CommandName="expense" CommandArgument="<%#Bind('ExpId') %>"
                                                                                                                Text="<%#Bind('Amount') %>"></asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="imgBtnDeleteExpense" runat="server" ImageUrl="~/Images/close.png"
                                                                                                                CommandName="delExpense" CommandArgument="<%#Bind('ExpId') %>" OnClientClick="javascript:return confirm('Are you sure that you want to remove this income record?');"
                                                                                                                onmouseover="javascript:OpenFeedbackWindow('Click to delete record')" onmouseout="javascript:CloseFeedBackWindow()" />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                       
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td id="tdExpenseTotal" runat="server" align="center" colspan="4">
                                                                                            <div style="font-weight: bold; width: 100px;">
                                                                                                Subtotal Total:</div>
                                                                                            <div style="background-color: Gray; color: white; height: 40px; line-height: 36px;
                                                                                                vertical-align: middle; width: 100px;">
                                                                                                <asp:Label ID="lblExpenseSubtotal" runat="server" Style="font-weight: bold; font-size: 15px;"></asp:Label>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
            </td>
            </tr>
            <%--<tr>
            <td class="whitebox_bottomleftcorner">
            </td>
            <td class="whitebox_bottom_bg">
            </td>
            <td class="whitebox_bottomrightcorner">
            </td>
        </tr>--%>
            </table>  
    
</asp:Content>

