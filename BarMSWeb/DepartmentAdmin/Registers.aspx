<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DepartmentAdminMaster.master"
    AutoEventWireup="true" CodeFile="Registers.aspx.cs" Inherits="DepartmentAdmin_Registers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../JavaScript/jquery-ui-1.8.7.custom.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.price_format.1.7.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery.watermark.js" type="text/javascript"></script>

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $('input.filter').bind('keyup blur', function() {
                if (this.value.match(/[^%.0-9]/g)) {
                    this.value = this.value.replace(/[^%.0-9]/g, '');
                }
            });
        });
    </script>

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
        <asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">
            <asp:View ID="vRegister" runat="server">
                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/btn_add_register.png"
                    OnClick="btnAdd_Click" />
                <asp:Label ID="lblMessage" runat="server" Visible="false" Text="" Style="position: relative;
                    left: 62px; top: -9px; font-size: 15px; font-weight: bold; color: Green;"></asp:Label>
                <asp:LinkButton ID="lnkSalesVat" runat="server" Text="Sales VAT" Style="position: relative;
                    margin-right: 66px; text-decoration: underline; color: Blue; float: right; display: none;"
                    OnClick="lnkSalesVat_Click"></asp:LinkButton>
                <div id="divIncome" runat="server" visible="false">
                    <table cellpadding="2" cellspacing="2" style="width: 100%;" border="0">
                        <tr>
                            <td width="150">
                                Add Register:
                            </td>
                            <td width="204">
                                <div class="textbox204">
                                    <asp:TextBox ID="txtRegisterName" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="req"
                                        ControlToValidate="txtRegisterName" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </td>
                            <td align="left">
                                <asp:HiddenField ID="hdnID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="150">
                                Description:
                            </td>
                            <td colspan="2">
                                <div class="textboxmulti">
                                    <asp:TextBox ID="txtRegisterDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td width="150">
                                Vat:
                            </td>
                            <td colspan="2">
                                <div class="textbox204">
                                    <asp:DropDownList ID="ddlVat" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" Style="float: right;
                                        position: relative; top: 6px;" ControlToValidate="ddlVat" Display="Dynamic" InitialValue="0"
                                        ErrorMessage="*" ></asp:RequiredFieldValidator>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="150">
                                &nbsp;
                            </td>
                            <td colspan="2">
                                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/btn_save.png" BgColor="Transparent"
                                    ValidationGroup="req" OnClick="btnSave_Click" />
                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/btn_edit_admin.png"
                                    ValidationGroup="req" BgColor="Transparent" OnClick="btnEdit_Click" Visible="false" />
                                <%-- <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />--%>
                                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/Images/btn_cancel.png" ValidationGroup="dddd"
                                    OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                            </td>
                        </tr>
                    </table>
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
                            <div class="rounded_box">
                                Registers
                                <div class="clear_10">
                                </div>
                                <asp:GridView ID="grdRegisters" AllowSorting="True" AllowPaging="True" runat="server"
                                    AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                    RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                                    EmptyDataText="Sorry! No Register Exists." BorderWidth="0" GridLines="None" OnRowCommand="grdRegisters_RowCommand"
                                    OnRowDataBound="grdRegisters_RowDataBound" OnPageIndexChanging="grdRegisters_PageIndexChanging">
                                    <Columns>
                                        <%--<asp:BoundField DataField="sIncomType" HeaderText="Name" ItemStyle-Width="100px" />--%>
                                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkIncomeType" runat="server" Text="<%#Bind('rName') %>" CommandName="change"
                                                    CommandArgument='<%# Bind("pkRegisterID") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <div id="divDescription" runat="server">
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="vat" HeaderText="Vat" Visible="false" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                                                    <tr>
                                                        <td class="align_right" style="width: 25px;">
                                                            <asp:ImageButton ID="imgBtnActive" runat="server" CommandName="active" CommandArgument='<%# Bind("pkRegisterID") %>'
                                                                ImageUrl="~/Images/activate_icon.gif" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
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
                <div class="clear_30">
                </div>
            </asp:View>
            <asp:View ID="vSalesVat" runat="server">
                <asp:LinkButton ID="lnkBackToRegisters" runat="server" Style="font-size: 12px; font-weight: normal;
                    color: #619ae9" OnClick="lnkBackToRegisters_Click">
                                                                    <img src="../images/back_arrow.png" alt="" />Back to Registers
                </asp:LinkButton>
                <table width="50%" cellpadding="0" cellspacing="10" border="0" align="center">
                    <tr id="trMessageVat" runat="server" visible="false">
                        <td colspan="3">
                            <h3>
                                <asp:Label ID="lblMessageVat" runat="server"></asp:Label>
                            </h3>
                        </td>
                    </tr>
                    <tr id="trAddVat" runat="server">
                        <td style="width: 100px;" colspan="3">
                            <asp:ImageButton ID="imgBtnAddVat" runat="server" ImageUrl="~/Images/btn_addvat.png"
                                OnClick="imgBtnAddVat_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 5px;">
                        </td>
                    </tr>
                    <tr id="trLineBeforeVat" runat="server">
                        <td colspan="3" style="background-color: #bfb8bb; height: 2px;">
                        </td>
                    </tr>
                    <tr id="trVat" runat="server" visible="false">
                        <td valign="middle">
                            Vat:
                        </td>
                        <td valign="middle">
                            <div class="textbox_small">
                                <asp:TextBox ID="txtVatAmount" runat="server" Style="text-align: center;" CssClass="filter"></asp:TextBox>
                                <span style="position: relative; float: right; left: 11px; margin-top: -19px;">%</span>
                            </div>
                        </td>
                        <td valign="bottom">
                            <asp:ImageButton ID="imgBtnSaveVat" runat="server" ImageUrl="~/Images/btn_save.png"
                                OnClick="imgBtnSaveVat_Click" />
                            <asp:ImageButton ID="imgBtnEditVat" runat="server" ImageUrl="~/Images/btn_edit_admin.png"
                                OnClick="imgBtnEditVat_Click" />
                            <asp:ImageButton ID="imgBtnCancelVat" runat="server" ImageUrl="~/Images/btn_cancel.png"
                                OnClick="imgBtnCancelVat_Click" />
                        </td>
                    </tr>
                    <tr id="trLineAfterVat" runat="server" visible="false">
                        <td colspan="3" style="background-color: #bfb8bb; height: 2px;">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div class="clear_10">
                            </div>
                            <table width="50%" border="0" cellspacing="0" cellpadding="0">
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
                                            <div class="clear_10">
                                            </div>
                                            <asp:GridView ID="grdVat" runat="server" AutoGenerateColumns="false" Width="100%"
                                                EmptyDataText="No Vat Exists." GridLines="None" HeaderStyle-CssClass="header_row"
                                                AlternatingRowStyle-CssClass="alternate_row" RowStyle-CssClass="rowstyle" Style="width: 100%;"
                                                OnRowCommand="grdVat_RowCommand" OnRowDataBound="grdVat_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Vat">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkVat" runat="server" CommandName="vat" CommandArgument="<%#Bind('pkvatid') %>"
                                                                Text="<%#Bind('vat') %>"></asp:LinkButton>
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
                            <div class="clear_30">
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
