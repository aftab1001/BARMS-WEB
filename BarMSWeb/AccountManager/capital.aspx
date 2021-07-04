<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AccountMaster.master"
    AutoEventWireup="true" CodeFile="capital.aspx.cs" Inherits="AccountManager_capital" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(function() {

            filterAmount();
            ShowDataPicker();
            ApplyJquery();

        });

        function filterAmount() {
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
        }

        function SmallChangeSaved() {

            $("#ctl00_ContentPlaceHolder1_lblMessageSmallChange").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblMessageSmallChange");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblMessageSmallChange").fadeOut('slow');
                }
            }, 4000);
        }
        function TransactionSaved() {

            $("#ctl00_ContentPlaceHolder1_lblMessageTransaction").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblMessageTransaction");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblMessageTransaction").fadeOut('slow');
                }
            }, 4000);
        }

        function ShowDataPicker() {

            $("#ctl00_ContentPlaceHolder1_txtFromDate").datepicker({
                dateFormat: 'dd/mm/yy',
                yearRange: "2000:2010"

            });

            
            
            $("#ctl00_ContentPlaceHolder1_txtTillDate").datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $("#ctl00_ContentPlaceHolder1_txtDateTransaction").datepicker({
                dateFormat: 'dd/mm/yy'
            });
            $("#ctl00_ContentPlaceHolder1_txtDateSmallChange").datepicker({
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
    </script>

    <asp:ScriptManager ID="scriptmanager" runat="server">
    </asp:ScriptManager>
    <div>
        <table border="0" width="100%">
            <tr>
                <td colspan="2" align="left">
                    From:
                </td>
                <td colspan="2" align="left">
                    Till:
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <div class="textbox115" style="float: left;">
                        <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtFromDate"
                        ErrorMessage="*" ValidationGroup="rmm" Display="Dynamic" Style="line-height: 38px;"></asp:RequiredFieldValidator>
                </td>
                <td colspan="2" align="left">
                    <div class="textbox115" style="float: left;">
                        <asp:TextBox ID="txtTillDate" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtTillDate"
                        ErrorMessage="*" ValidationGroup="rmm" Display="Dynamic" Style="line-height: 38px;"></asp:RequiredFieldValidator>
                </td>
                <td colspan="3">
                    <asp:ImageButton ID="imgBtnFilter" runat="server" ImageUrl="../Images/btn_filter.png"
                        ValidationGroup="rmm" OnClick="imgBtnFilter_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <div class="height30" colspan="4" style="margin-top: 20px;">
                        <img src="../images/horizontal_line.png" alt="" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    Total Startup
                </td>
                <td colspan="2" align="left">
                    Total Turnover
                </td>
                <td colspan="2" align="left">
                    Total Transactions
                </td>
                <td align="left">
                    Operating Capital
                </td>
            </tr>
            <tr>
                <td style="width: 140px;">
                    <div class="textbox115">
                        <asp:TextBox ID="txtTotalStartup" runat="server" style="text-align:right;"></asp:TextBox>
                    </div>
                </td>
                <td align="left" style="width: 60px;">
                    <img src="../images/plus.png" />
                </td>
                <td style="width: 140px;">
                    <div class="textbox115">
                        <asp:TextBox ID="txtTotalTurnover" runat="server" style="text-align:right;"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 60px;">
                    <img src="../images/minus.png" />
                </td>
                <td style="width: 140px;">
                    <div class="textbox115">
                        <asp:TextBox ID="txtTotalTransactions" runat="server" style="text-align:right;"></asp:TextBox>
                    </div>
                </td>
                <td style="width: 60px;">
                    <img src="../images/aero_right.png" />
                </td>
                <td style="width: 140px;">
                    <div class="textbox115">
                        <asp:TextBox ID="txtTotalOperatingCapital" runat="server" style="text-align:right;"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="7">
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
                                                    <div style="background-color: Transparent; width: 775px;">
                                                        <div style="background-color: Transparent; width: 775px; float: left;">
                                                            <div style="background-color: #BFB8BB; width: 150px; height: 40px; line-height: 36px;
                                                                float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab1">
                                                                Startup</div>
                                                            <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                                float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab2">
                                                                Weekly Turnover</div>
                                                            <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                                float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab3">
                                                                Transactions</div>
                                                            <div style="background-color: #e8e8e8; width: 150px; height: 40px; line-height: 36px;
                                                                float: left; margin-right: 5px; text-align: center; cursor: pointer;" class="tab4">
                                                                Operating Capital</div>
                                                        </div>
                                                        <div style="background-color: Transparent; width: 775px; float: left;">
                                                            <asp:Label ID="lblDatePeriod" runat="server" Text="" Style="color: #979fa8; font-style: italic;
                                                                position: relative; top: 30px; left: 20px;"></asp:Label>
                                                            <div id="divTab1" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                width: auto; display: block; padding-left: 10px; padding-top: 10px; padding-top: 15px;
                                                                margin-top: -20px; position: relative;">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="height: 40px;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <div style="float: right;">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Total Startup
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="textbox115" style="float: left;">
                                                                                                <asp:TextBox ID="txtTotalStartupCapital" runat="server" style="text-align:right;"></asp:TextBox>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div class="height30" colspan="4" style="margin-top: 20px;">
                                                                                <img src="../images/horizontal_line.png" alt="" />
                                                                            </div>
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
                                                                                            <asp:Label ID="lblDepartmentName" runat="server"></asp:Label>
                                                                                            <div class="clear_10">
                                                                                            </div>
                                                                                            <asp:GridView ID="grdStartupCapital" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                                EmptyDataText="No Startup Capital added yet." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                                AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                                                                OnRowDataBound="grdStartupCapital_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="100" >
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Notes" ItemStyle-Width="400" >
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblNotes" runat="server" Text="<%#Bind('Note') %>" onmouseover="javascript:OpenFeedbackWindow('Click to delete record')"
                                                                                                                onmouseout="javascript:CloseFeedBackWindow()"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Deposite Amount" ItemStyle-HorizontalAlign="Right">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblAmount" runat="server" Text="<%#Bind('Amount') %>" style="margin-right:96px;"></asp:Label>
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
                                                            </div>
                                                            <div id="divTab2" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                width: auto; padding: 10px; padding-top: 10px; padding-top: 15px; display: none;
                                                                margin-top: -20px; position: relative;">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="height: 40px;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <div style="float: left;">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Total Turnover
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="textbox115" style="float: left;">
                                                                                                <asp:TextBox ID="txtWeeklyTurnover" runat="server" style="text-align:right;"></asp:TextBox>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div class="height30" colspan="4" style="margin-top: 20px;">
                                                                                <img src="../images/horizontal_line.png" alt="" />
                                                                            </div>
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
                                                                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                                                                            <div class="clear_10">
                                                                                            </div>
                                                                                            <asp:GridView ID="grdTotalTurnover" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                                EmptyDataText="No Record Found!." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                                AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                                                                OnRowDataBound="grdTotalTurnover_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="Week#" ItemStyle-Width="550">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblDate" runat="server" Text="<%#Bind('iWeekNumber') %>"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Total Income" ItemStyle-Width="400" ItemStyle-HorizontalAlign="Right">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblTotalIncome" runat="server" Text="<%#Bind('income') %>" style="position:relative;margin-right:40px;"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Total Salaries" ItemStyle-Width="400" ItemStyle-HorizontalAlign="Right">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblTotalSalaries" runat="server" Text="<%#Bind('salary') %>" style="position:relative;margin-right:40px;"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Total Expenses" ItemStyle-Width="400" ItemStyle-HorizontalAlign="Right">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblTotalExpenses" runat="server" Text="" style="position:relative;margin-right:40px;"></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Weekly Turnover" ItemStyle-Width="400" ItemStyle-HorizontalAlign="Right">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblWeeklyTurnover" runat="server" Text="" style="position:relative;margin-right:40px;"></asp:Label>
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
                                                            </div>
                                                            <div id="divTab3" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                width: auto; display: none; padding: 10px; padding-top: 15px; margin-top: -20px;
                                                                position: relative;">
                                                                <asp:UpdatePanel ID="upnlTransaction" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:Label ID="lblMessageTransaction" runat="server" Style="display: none; color: Green;
                                                                            position: relative; padding-left: 260px; font-size: 15px; font-weight: bold;"
                                                                            Text="Successfully Saved!"></asp:Label>
                                                                        <div style="padding-top: 40px;">
                                                                            <asp:ImageButton ID="imgBtnAddTransaction" runat="server" ImageUrl="../images/add_trans.png"
                                                                                OnClick="imgBtnAddTransaction_Click" />
                                                                            <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="../images/btn_save.png"
                                                                                OnClick="imgBtnSave_Click" Visible="false" ValidationGroup="req" />
                                                                            <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="../images/btn_cancel.png"
                                                                                OnClick="imgBtnCancel_Click" Visible="false" />
                                                                            <br />
                                                                            <table border="0" width="100%">
                                                                                <tr>
                                                                                    <td style="height: 10px;">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trLineBefore" runat="server">
                                                                                    <td colspan="4" style="background-color: #bfb8bb">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trContent" runat="server" visible="false">
                                                                                    <td colspan="4">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <div class="textbox115" style="float: left;">
                                                                                                        <asp:TextBox ID="txtDateTransaction" runat="server" Style="width: 94px;"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateTransaction"
                                                                                                            ErrorMessage="*" ValidationGroup="req" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <div class="textbox204" style="float: left;">
                                                                                                        <asp:DropDownList ID="ddlDepartmentAdmin" runat="server" Style="width: 175px;">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDepartmentAdmin"
                                                                                                            InitialValue="0" ErrorMessage="*" ValidationGroup="req" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <div class="textbox204" style="float: left;">
                                                                                                        <asp:DropDownList ID="ddlTransactions" runat="server" Style="width: 175px;">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlTransactions"
                                                                                                            InitialValue="0" ErrorMessage="*" ValidationGroup="req" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <div class="textbox115" style="float: left;">
                                                                                                        <asp:TextBox ID="txtTransactionAmount" runat="server" Style="width: 94px;" CssClass="filter"></asp:TextBox>
                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtTransactionAmount"
                                                                                                            ErrorMessage="*" ValidationGroup="req" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="4">
                                                                                                    <div class="textboxmulti_738">
                                                                                                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4" style="height: 10px;">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trLineAfter" runat="server" visible="false">
                                                                                    <td colspan="4" style="background-color: #bfb8bb">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4" style="height: 10px;">
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" style="width: 500px;">
                                                                                    </td>
                                                                                    <td align="right">
                                                                                        Total Transactions
                                                                                    </td>
                                                                                    <td align="right">
                                                                                        <div class="textbox115" style="float: left;">
                                                                                            <asp:TextBox ID="txtTotalTrasactions" runat="server" style="text-align:right;"></asp:TextBox>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
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
                                                                                                        <asp:GridView ID="grdTransactions" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                                            EmptyDataText="No Transaction added yet." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                                            AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                                                                            OnRowDataBound="grdTransactions_RowDataBound" OnRowCommand="grdTransactions_RowCommand">
                                                                                                            <Columns>
                                                                                                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="100">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblDate" runat="server" Text="<%#Bind('dModifiedDate') %>"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Department Admin">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblDepartmentAdmin" runat="server" Text="<%#Bind('FullName') %>"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Transaction Type">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblTransactionType" runat="server" Text="<%#Bind('TransactionType') %>"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Notes">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblNotes" runat="server" Text="<%#Bind('Notes') %>"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                                                                                    <ItemTemplate>
                                                                                                                        <asp:Label ID="lblAmount" runat="server" Text="<%#Bind('Amount') %>" style="position:relative;right:10px;"></asp:Label>
                                                                                                                    </ItemTemplate>
                                                                                                                </asp:TemplateField>
                                                                                                                <asp:TemplateField HeaderText="Received">
                                                                                                                    <ItemTemplate>
                                                                                                                        <img id="img" runat="server" src="../images/close.png" />
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
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div id="divTab4" runat="server" style="background-color: Transparent; border: solid 2px #c7c7c7;
                                                                width: auto; display: none; padding: 10px; padding-top: 15px; margin-top: -20px;
                                                                position: relative;">
                                                                <asp:UpdatePanel ID="upnlOperatingCapital" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div style="padding-top: 40px;">
                                                                            <table width="70%" cellspacing="0">
                                                                                <tr>
                                                                                    <td style="background-color: #a2c4c9;">
                                                                                        Total Operating Capital
                                                                                    </td>
                                                                                    <td style="background-color: #a2c4c9;">
                                                                                        <div class="textbox115" style="float: left;">
                                                                                            <asp:TextBox ID="txtOperatingCapical" runat="server" Style="width: 94px;text-align:right;" CssClass="filter"></asp:TextBox>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td style="width: 50px;">
                                                                                        <img src="../Images/vertical_line.png" height="35px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        Small Change
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox115" style="float: left;">
                                                                                            <asp:TextBox ID="txtSmallChange" runat="server" Style="width: 94px;text-align:right;" CssClass="filter"></asp:TextBox>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        Real Operating Capital
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox115" style="float: left;">
                                                                                            <asp:TextBox ID="txtRealOperatingCapital" runat="server" Style="width: 94px;text-align:right;" CssClass="filter"></asp:TextBox>
                                                                                        </div>
                                                                                    </td>
                                                                                    <td style="width: 50px;">
                                                                                        <img src="../Images/vertical_line.png" height="35px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        Advances
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="textbox115" style="float: left;">
                                                                                            <asp:TextBox ID="txtAdvances" runat="server" Style="width: 94px;text-align:right;" CssClass="filter"></asp:TextBox>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <fieldset style="width:730px;">
                                                                                            <legend>Small Change </legend>
                                                                                            <asp:Label ID="lblMessageSmallChange" runat="server" Style="display: none; color: Green;
                                                                                                position: relative; padding-left: 260px; font-size: 15px; font-weight: bold;"
                                                                                                Text="Successfully Saved!"></asp:Label>
                                                                                            <br />
                                                                                            <asp:ImageButton ID="imgBtnAddSmallChange" runat="server" ImageUrl="../Images/btn_addremove.png"
                                                                                                OnClick="imgBtnAddSmallChange_Click" />
                                                                                            <asp:ImageButton ID="imgBtnSaveChange" runat="server" ImageUrl="../Images/btn_save.png"
                                                                                                OnClick="imgBtnSaveChange_Click" Visible="false" />
                                                                                            <asp:ImageButton ID="imgBtnEditChange" runat="server" ImageUrl="../Images/btn_edit.png"
                                                                                                OnClick="imgBtnEditChange_Click" Visible="false" />
                                                                                            <asp:ImageButton ID="imgBtnCancelChange" runat="server" ImageUrl="../Images/btn_Cancel.png"
                                                                                                OnClick="imgBtnCancelChange_Click" Visible="false" />
                                                                                            <br />
                                                                                            <table border="0" width="100%">
                                                                                                <tr>
                                                                                                    <td style="height: 10px;">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trbefore" runat="server">
                                                                                                    <td colspan="4" style="background-color: #bfb8bb">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trSmallChange" runat="server" visible="false">
                                                                                                    <td colspan="4">
                                                                                                        <table>
                                                                                                            <tr>
                                                                                                                <td>
                                                                                                                    <div class="textbox115" style="float: left;">
                                                                                                                        <asp:TextBox ID="txtDateSmallChange" runat="server" Style="width: 94px;"></asp:TextBox>
                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDateSmallChange"
                                                                                                                            ErrorMessage="*" ValidationGroup="req" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    Amount
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <div class="textbox115" style="float: left;">
                                                                                                                        <asp:TextBox ID="txtSmallChangeAmount" runat="server" Style="width: 94px;" CssClass="filter"></asp:TextBox>
                                                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtSmallChangeAmount"
                                                                                                                            ErrorMessage="*" ValidationGroup="req" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                                <td>
                                                                                                                    <div class="textbox204_message">
                                                                                                                        <asp:Label ID="lblNoteHeader" runat="server" Text="Notes:" Style="float: left; padding-left: 5px;
                                                                                                                            margin-top: 4px;"></asp:Label>
                                                                                                                        <asp:TextBox ID="txtNoteSmallChange" runat="server" Style="width: 370px; border: 0;
                                                                                                                            background: Transparent; margin-top: 8px;"></asp:TextBox>
                                                                                                                    </div>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4" style="height: 10px;">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trAfter" runat="server" visible="false">
                                                                                                    <td colspan="4" style="background-color: #bfb8bb">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4" style="height: 10px;">
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="2" style="width: 410px;">
                                                                                                    </td>
                                                                                                    <td align="right">
                                                                                                        Total Small Change
                                                                                                    </td>
                                                                                                    <td align="right">
                                                                                                        <div class="textbox115" style="float: left;">
                                                                                                            <asp:TextBox ID="txtSmallChangeTab" runat="server" style="text-align:right;"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td colspan="4">
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
                                                                                                                        <asp:Label ID="Label4" runat="server"></asp:Label>
                                                                                                                        <div class="clear_10">
                                                                                                                        </div>
                                                                                                                        <asp:GridView ID="grdSmallChange" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                                                            EmptyDataText="No Small Change added yet." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                                                            AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                                                                                            OnRowDataBound="grdSmallChange_RowDataBound" OnRowCommand="grdSmallChange_RowCommand">
                                                                                                                            <Columns>
                                                                                                                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="100">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblDate" runat="server" Text="<%#Bind('dModifiedDate') %>"></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderText="Notes">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblNotes" runat="server" Text="<%#Bind('Notes') %>"></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblAmount" runat="server" Text="<%#Bind('Amount') %>" style="right:145px;position:relative;"></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField>
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:ImageButton ID="lnkEdit" runat="server" CommandName="edt" CommandArgument="<%#Bind('pkSmallChangeID') %>"
                                                                                                                                            ImageUrl="../Images/edit_icon.png"></asp:ImageButton>
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
                                                                                        </fieldset>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5" style="height: 20px;">
                                                                                        <img src="../Images/horizontal_line.png" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="5">
                                                                                        <fieldset style="width:730px;">
                                                                                            <legend>Advances</legend>
                                                                                            <table width="100%">
                                                                                                <tr>
                                                                                                    <td style="width: 500px; text-align: right;">
                                                                                                        Total Advances
                                                                                                    </td>
                                                                                                    <td style="text-align: right;">
                                                                                                        <div class="textbox115" style="float: left;">
                                                                                                            <asp:TextBox ID="txtTotalAdvances" runat="server" Style="width: 94px;text-align:right;" CssClass="filter"></asp:TextBox>
                                                                                                        </div>
                                                                                                    </td>
                                                                                                </tr>
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
                                                                                                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                                                                                                        <div class="clear_10">
                                                                                                                        </div>
                                                                                                                        <asp:GridView ID="grdAdvance" runat="server" AutoGenerateColumns="false" Width="100%"
                                                                                                                            EmptyDataText="No Advance." GridLines="None" HeaderStyle-CssClass="header_row"
                                                                                                                            AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                                                                                            OnRowDataBound="grdAdvance_RowDataBound" OnRowCommand="grdAdvance_RowCommand">
                                                                                                                            <Columns>
                                                                                                                                <asp:TemplateField HeaderText="Name">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:Label ID="lblName" runat="server" Text="<%#Bind('FullName') %>"></asp:Label>
                                                                                                                                    </ItemTemplate>
                                                                                                                                </asp:TemplateField>
                                                                                                                                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                                                                                                    <ItemTemplate>
                                                                                                                                        <asp:LinkButton ID="lnkAmount" runat="server" CommandName="edt" CommandArgument="<%#Bind('pkuserid') %>" style="right:208px;position:relative;"></asp:LinkButton>
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
                                                                                        </fieldset>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
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
                </td>
            </tr>
        </table>
    </div>
    <div class="clear_10">
    </div>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnExtent1"
        PopupControlID="pnlAddresses" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Button ID="btnExtent1" runat="server" Style="display: none;" />
    <asp:Panel ID="pnlAddresses" runat="server">
        <asp:UpdatePanel ID="upnlAddresses" runat="server">
            <ContentTemplate>
                <%--<div style="height:600px;Width: 790px;overflow:auto;">--%>
                <div class="lightbox-header" style="width: 718px !important;">
                    <asp:Label ID="lblPopAdvanceTitle" runat="server" Style="float: left; color: White;
                        font-size: 15px; line-height: 39px; padding-left: 5px;" Text="">
                        </asp:Label>
                    <a href="#" title="Close" onclick="$find('<%=ModalPopupExtender1.ClientID %>').hide();return false;">
                        <img style="float: right; padding: 9px 5px 5px;" title="Stäng" alt="stäng" src="../images/lightbox-close.png"></a></div>
                <div class="small-lightbox-content" style="background-color: White; text-align: center;">
                    <table cellpadding="3" cellspacing="3" border="0" width="100%">
                        <tr>
                            <td align="left" colspan="3">
                                <asp:Label ID="lblPopupDate" runat="server" Style="color: #979fa8; font-style: italic;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAdvanceUserName" runat="server"></asp:Label>
                            </td>
                            <td align="right">
                                Total Advances:
                            </td>
                            <td style="width: 215px;">
                                <div class="textbox204" style="float: left;">
                                    <asp:TextBox ID="txtTotalAdvancesPopup" runat="server" style="text-align:right;"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 20px;">
                                <img src="../Images/horizontal_line.png" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
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
                                                <asp:Label ID="Label5" runat="server"></asp:Label>
                                                <div class="clear_10">
                                                </div>
                                                <asp:GridView ID="grdAdvancepopup" runat="server" AutoGenerateColumns="false" Width="100%"
                                                    EmptyDataText="No Advance." GridLines="None" HeaderStyle-CssClass="header_row"
                                                    AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                    OnRowDataBound="grdAdvancepopup_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDatePopup" runat="server" Text="<%#Bind('dmodifieddate') %>"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Advance Amount" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server" Text="<%#Bind('uAdvance') %>" style="position:relative;right:291px;"></asp:Label>
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
                </div>
                <%-- </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <%--</td> </tr> </table> </div>--%>
</asp:Content>
