<%@ Page Language="C#" MasterPageFile="~/MasterPages/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManageDepartments.aspx.cs" Inherits="Admin_ManageDepartments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../JavaScript/ToolTip.js" type="text/javascript"></script>

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

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ImageButton ID="btnAddDepartment" runat="server" OnClick="btnAddDepartment_Click" ImageUrl="~/Images/btn_add_department.png" />
    <%--<asp:Button ID="btnAddDepartment" runat="server" Text="Add Department" OnClick="btnAddDepartment_Click" />--%>
    <div id="divDepartment" runat="server" visible="false">
        <table cellpadding="2" cellspacing="2" style="width: 100%;" border="0">
            <tr>
                <td width="150">
                    <asp:Label ID="lbluser" runat="server" Text="Add Department"></asp:Label>
                </td>
                <td width="204">
                    <div class="textbox204">
                        <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Phone #"></asp:Label>
                </td>
                <td>
                    <div class="textbox204">
                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Street Address"></asp:Label>
                </td>
                <td>
                    <div class="textbox204">
                        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td colspan="2" align="center">
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/btn_save.png" BgColor="Transparent"
                        OnClick="btnSave_Click" />
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/btn_edit_admin.png"
                        BgColor="Transparent" OnClick="btnEdit_Click" Visible="false" />
                    <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />--%>
                    <asp:ImageButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" ImageUrl="~/Images/btn_cancel.png" />
                    <asp:HiddenField ID="hdnID" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblError" runat="server" Visible="false" CssClass="Error" Text="Department Name already Exist"></asp:Label>
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
                    Departments
                    <div class="clear_10">
                    </div>
                    <asp:GridView ID="grdDepartment" AllowSorting="True" AllowPaging="True" runat="server"
                        AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                        RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                        BorderWidth="0" GridLines="None" OnRowCommand="grdDepartment_RowCommand" OnRowDataBound="grdDepartment_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Name" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDepartment" runat="server" Text="<%#Bind('sDepartmentName') %>"
                                        CommandName="Change" CommandArgument='<%# Bind("pkDepartmentID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Department Admin" ItemStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDepartmentAdmin" runat="server" CommandName="message" Text="<%#Bind('dname') %>"
                                        Style="text-decoration: none;" CommandArgument="<%#Bind('id') %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="sPhone" HeaderText="Phone" ItemStyle-Width="50px" />
                            <asp:BoundField DataField="sAddress" HeaderText="Address" ItemStyle-Width="100px" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                                        <tr>
                                            <%--<td class="align_left" style="width:170px;">
                                    <asp:Label ID="lblAccess" runat="server"></asp:Label></td>--%>
                                            <td class="align_right" style="padding-right: 10px;">
                                                <asp:ImageButton ID="imgedit" runat="server" Visible="false" CommandName="Change"
                                                    CommandArgument='<%# Bind("pkDepartmentID") %>' ImageUrl="../images/edit.png"
                                                    ToolTip="Edit" />
                                            </td>
                                            <%--<td class="align_right" style="width: 20px;">
                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkDepartmentID") %>'
                                                    ImageUrl="../images/close.png" ToolTip="Delete" />
                                            </td>--%>
                                             <td class="align_right" style="width: 20px;">
                                                <asp:ImageButton ID="imgBtnActive" runat="server" CommandName="active" CommandArgument='<%# Bind("pkDepartmentID") %>'
                                                    ImageUrl="~/Images/activate_icon.gif" ToolTip="Active" />
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
</asp:Content>
