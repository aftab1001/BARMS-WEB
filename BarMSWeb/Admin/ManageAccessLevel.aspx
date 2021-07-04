<%@ Page Language="C#" MasterPageFile="~/MasterPages/AdminMaster.master" AutoEventWireup="true"
    CodeFile="ManageAccessLevel.aspx.cs" Inherits="Admin_ManageAccessLevel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">

    function CheckValidity() {
        var chk = document.getElementById("ctl00_ContentPlaceHolder1_chkAccess");
        if (!chk.checked) {
            alert("Please check the Checkbox to Set Access Level");
            return false;
        }
    } 
</script>
    <table cellpadding="2" cellspacing="2" style="width: 100%;" border="0">
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Departments"></asp:Label>
            </td>
            <td>
                <div class="textbox204">
                    <asp:DropDownList ID="ddlDepartments" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartments_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbluser" runat="server" Text="Users"></asp:Label>
            </td>
            <td>
                
                <div class="textbox204">
                    <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlUsers_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </td>
            <td style="display:none;">
                <asp:Label ID="Label1" runat="server" Text="Access Levels"></asp:Label>
            </td>
            <td>
            <asp:CheckBox ID="chkAccess" runat="server" Text="Set as Department Admin" />
                <div class="textbox204"  style="display:none;">
                    <asp:DropDownList ID="ddlAccessLevel" runat="server" AutoPostBack="false">
                    </asp:DropDownList>
                </div>
            </td>
            <td>
                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/btn_save.png" OnClick="btnSave_Click" OnClientClick="javascript:return CheckValidity();"
                    BgColor="Transparent" />
            </td>
        </tr>
        <tr>
            <td colspan="5" align="center">
                <asp:Label ID="lblError" runat="server" CssClass="Error" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
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
                    <asp:Label ID="lblDepartmentName" runat="server"></asp:Label>
                    <div class="clear_10">
                    </div>
                    <asp:GridView ID="grdAccessLevels" AllowSorting="True" AllowPaging="True" runat="server"
                        AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                        RowStyle-CssClass="rowstyle" Width="100%" OnRowCommand="grdAccessLevels_RowCommand"
                        OnRowDataBound="grdAccessLevels_RowDataBound" CellPadding="0" BorderStyle="None"
                        BorderWidth="0" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="FullName" HeaderText="Name" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="sAccessLevel" HeaderText="Access Level" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="dCreateDate" HeaderText="Date Assigned" DataFormatString="{0:dd/MM/yyyy hh:mm:tt}" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
                                        <tr>
                                            <%--<td class="align_left" style="width:170px;">
                                    <asp:Label ID="lblAccess" runat="server"></asp:Label></td>
                                <td class="align_left">
                                    <asp:Label ID="lblDate" runat="server"></asp:Label></td>--%>
                                            <td class="align_right" style="width: 20px;">
                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" CommandArgument='<%# Bind("pkUserAccessLevel") %>'
                                                    ImageUrl="../images/close.png" ToolTip="Delete" />
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
</asp:Content>
