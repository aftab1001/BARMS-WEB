<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DepartmentAdminMaster.master"
    AutoEventWireup="true" CodeFile="StartupCapital.aspx.cs" Inherits="DepartmentAdmin_StartupCapital" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(function() {
            $("#ContentPlaceHolder1_imgbtnAddStartupCapital").click(function() {
                alert('a');
            });

            $("#ctl00_ContentPlaceHolder1_txtDate").datepicker({
                dateFormat: 'dd/mm/yy'

            });
            $('.filter').watermark('00,00');
        });

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

        function fixedDecimalPlace2(obj) {
            var control = document.getElementById(obj.id);
            if (control.value != "") {
                if (control.value == "0") {
                    control.value = "";
                }
                else {
                    control.value = ChangeToUK(getNum(control.value).toFixed(2));
                }
            }
        }


        function RecordSaved() {

            $("#ctl00_ContentPlaceHolder1_lblMessage").fadeIn('slow');
            window.setTimeout(function() {
                // This will execute 5 seconds later
                var label = $("#ctl00_ContentPlaceHolder1_lblMessage");
                if (label != null) {
                    $("#ctl00_ContentPlaceHolder1_lblMessage").fadeOut('slow');
                }
            }, 4000);
        }

        function ChangeToUS(value) {

            var retValue = "";

            var tempArray = new Array();
            var BeforeComma = new Array();
            var AfterComma = "";

            tempArray = value.split(',');

            if (tempArray.length > 1) {
                BeforeComma = tempArray[0].split('.');
                AfterComma = tempArray[1].toString();
            }
            else {
                BeforeComma = tempArray[0].split('.');
                AfterComma = "00";
            }
            if (BeforeComma.length == 1) {
                if (BeforeComma[0].toString() == "0")
                    retValue += "00,";
                else
                    retValue += BeforeComma[0] + ',';
            }
            else {
                for (i = 0; i <= BeforeComma.length - 1; i++)
                    retValue += BeforeComma[i] + ',';
            }

            retValue = retValue.substr(0, retValue.lastIndexOf(',')) + '.';
            retValue += AfterComma;
            retValue = parseFloat(retValue, 10);
            retValue = roundNumber(retValue, 2);
            return retValue;
        }
        function ChangeToUK(value) {


            var retValue = "";

            var tempArray = new Array();
            var BeforeComma = new Array();
            var AfterComma = "";

            tempArray = value.split('.');

            if (tempArray.length > 1) {
                BeforeComma = tempArray[0].split(',');
                AfterComma = tempArray[1].toString();
            }
            else {
                BeforeComma = tempArray[0].split(',');
                AfterComma = "00";
            }
            if (BeforeComma.length == 1) {
                if (BeforeComma[0].toString() == "0")
                    retValue += "00.";
                else
                    retValue += BeforeComma[0] + '.';
            }
            else {
                for (i = 0; i <= BeforeComma.length - 1; i++)
                    retValue += BeforeComma[i] + '.';
            }

            retValue = retValue.substr(0, retValue.lastIndexOf('.')) + ',';
            retValue += AfterComma;

            return retValue;
        }
    </script>

    <div>
        <asp:Label ID="lblMessage" runat="server" Text="Successfully Saved" Style="display: none;
            font-weight: bold; color: #03a02c; position: relative; left: 325px;"></asp:Label>
        <table>
            <tr id="trAdd" runat="server">
                <td style="width: 100px;" colspan="4">
                    <div style="float: left; width: 830px;">
                        <asp:ImageButton ID="imgAddSpecialIncome" runat="server" ImageUrl="~/Images/addstart.png"
                            OnClick="imgAddSpecialIncome_Click" />
                    </div>
                </td>
            </tr>
            <tr>
            <td style="height:10px;"></td>
            </tr>
            <tr id="trLineBefore" runat="server">
                <td colspan="4" style="background-color: #bfb8bb">                
                </td>
                
            </tr>
            <tr id="trOtherIncome" runat="server" visible="false">
                <td style="width: 133px;">
                    <div>
                        Date:
                        <div class="textbox115">
                            <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </td>
                <td style="width: 193px;">
                    Startup Capital Amount:
                    <div class="textbox_small" style="float: left; margin-left: 2px;">
                        <asp:TextBox ID="txtSCAmount" runat="server" Style="text-align: center;" CssClass="filter"
                            onkeypress="javascript:CorrectValue(this);" onchange="javascript:fixedDecimalPlace2(this);"></asp:TextBox>
                    </div>
                    <span style="position: relative; float: right; left: -100px; line-height: 31px;">€</span>
                </td>
                <td valign="bottom" style="width: 333px;">
                    Notes:
                    <div class="textbox330">
                        <asp:TextBox ID="txtNote" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td valign="bottom">
                    <asp:ImageButton ID="imgBtnSaveOtherIncome" runat="server" ImageUrl="~/Images/btn_save_account.png"
                        OnClick="imgBtnSaveOtherIncome_Click" />
                    <asp:ImageButton ID="ImgBtnEdit" runat="server" ImageUrl="~/Images/btn_edi_smallt.gif"
                        OnClick="ImgBtnEdit_Click" Visible="false" />
                    <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/Images/btn_cancel.png"
                        OnClick="imgBtnCancel_Click" />
                </td>
            </tr>
            <tr id="trLineAfter" runat="server" visible="false">
                <td colspan="4" style="background-color: #bfb8bb">
                </td>
            </tr>
            <tr>
            <td style="height:10px;"></td>
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
                                    <asp:Label ID="lblDepartmentName" runat="server"></asp:Label>
                                    <div class="clear_10">
                                    </div>
                                    <asp:GridView ID="grdStartupCapital" runat="server" AutoGenerateColumns="false" Width="100%"
                                        EmptyDataText="No Startup Capital added yet." GridLines="None" HeaderStyle-CssClass="header_row"
                                        AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                        OnRowDataBound="grdStartupCapital_RowDataBound" OnRowCommand="grdStartupCapital_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Date" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Notes" ItemStyle-Width="400">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNotes" runat="server" Text="<%#Bind('Note') %>" onmouseover="javascript:OpenFeedbackWindow('Click to delete record')"
                                                        onmouseout="javascript:CloseFeedBackWindow()"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deposite Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text="<%#Bind('Amount') %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" ImageUrl="../Images/edit_icon.png"
                                                        CommandName="edt" CommandArgument="<%#Bind('pkStartupCapitalID') %>" />
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
</asp:Content>
