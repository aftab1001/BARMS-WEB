<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ManagerMaster.master"
    AutoEventWireup="true" CodeFile="ManageSpecialties.aspx.cs" Inherits="Managers_ManageSpecialties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div style="float:left;width:500px;">
    <asp:ImageButton ID="imgBtnAddSpecialty" runat="server" 
        ImageUrl="~/Images/btn_add_specity.png" onclick="imgBtnAddSpecialty_Click" />
        <asp:Label ID="lblMessage" runat="server" Visible="false" Text="" ForeColor="Green" style="font-size: 16px;padding-top:5px;padding-left:10px; position:absolute;"></asp:Label>
        </div>
    <div id="divSpecialty" runat="server" visible="false">
        <table cellpadding="2" cellspacing="2" width="70%">
            <tr>
                <td style="width: 0px;">
                    <div class="textbox204" style="padding-left: 6px;">
                        <asp:TextBox ID="txtSpeciality" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSpeciality"
                            ErrorMessage="*" ValidationGroup="req"></asp:RequiredFieldValidator>
                    </div>
                </td>
                <td>
                    <asp:ImageButton ID="imgBtnSave" runat="server" ImageUrl="~/Images/btn_save.png"
                        ValidationGroup="req" OnClick="imgBtnSave_Click" />
                        <asp:ImageButton ID="imgCancel" runat="server" 
                        ImageUrl="~/Images/btn_cancel.png" onclick="imgCancel_Click" />
                </td>
                <td align="left" >
                    
                </td>
            </tr>
        </table>
    </div>
    <table style="float:left;">
        <tr>
            <td colspan="3">
                <br />
                <span style="font-weight: bold;">Note:-</span>You can only add and edit Specialties.
                Once added they can’t be deleted!
            </td>
        </tr>
        <tr>
            <td colspan="3">
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
                                <asp:GridView ID="grdSpecialities" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                                    EmptyDataText="Sorry! No Data Found." HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                                    RowStyle-CssClass="rowstyle" Width="100%" ellPadding="0" BorderStyle="None" BorderWidth="0"
                                    GridLines="None" OnRowCommand="grdSpecialities_RowCommand" OnRowDataBound="grdSpecialities_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 70%;">
                                                        </td>
                                                        <td style="width: 15%;">
                                                            Active
                                                        </td>
                                                        <td style="width: 15%;">
                                                            Special
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 70%;">
                                                            <asp:LinkButton ID="lnkSpecialtyName" runat="server" CommandArgument="<%#Bind('pkSpecialityTypeID') %>"
                                                                CommandName="edt" Text="<%#Bind('sSpecialityName') %>"></asp:LinkButton>
                                                        </td>
                                                        <td style="width: 15%;">
                                                            <asp:ImageButton ID="imgBtnActive" runat="server" CommandName="active" CommandArgument='<%# Bind("pkSpecialityTypeID") %>'
                                                                ImageUrl="~/Images/activate_icon.gif" ToolTip="Active" />
                                                        </td>
                                                        <td style="width: 15%;">
                                                            <asp:ImageButton ID="imgBtnSpecial" runat="server" CommandName="special" CommandArgument='<%# Bind("pkSpecialityTypeID") %>'
                                                                ImageUrl="~/Images/activate_icon.gif" ToolTip="Special" />
                                                        </td>
                                                    </tr>
                                                </table>
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
</asp:Content>
