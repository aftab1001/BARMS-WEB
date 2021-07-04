<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ManagerMaster.master"
    AutoEventWireup="true" CodeFile="ManagePositions.aspx.cs" Inherits="Managers_ManagePositions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../JavaScript/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScript/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script src="../JavaScript/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

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
        function CheckValidity() {
            var chk = document.getElementById("ctl00_ContentPlaceHolder1_chkBonus");
            if (!chk.checked) {
                return false;
            }
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
    <asp:Label ID="lblHeading" runat="server"></asp:Label><br />
    <br />
    <asp:HiddenField ID="hidSortNo" runat="server" />
    <asp:ImageButton ID="imgBtnAdd" runat="server" ImageUrl="~/Images/btn_addposition.png"
        OnClick="imgBtnAdd_Click" />
    <asp:ImageButton ID="imgBtnSeperator" runat="server" Visible="true" ImageUrl="~/Images/btn_addseprator.png"
        OnClick="imgBtnSeperator_Click" />
    <div id="divAddPosition" runat="server" visible="false">
        <table cellpadding="2" cellspacing="2" style="width: 100%;" border="0">
            <tr>
                <td width="150">
                    <asp:Label ID="Label3" runat="server" Text="Specialties"></asp:Label>
                </td>
                <td width="204">
                    <div class="textbox204">
                        <asp:DropDownList ID="ddlSpecialities" runat="server" AutoPostBack="false">
                        </asp:DropDownList>
                    </div>
                </td>
                <td style="width: 100px;">
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Order"></asp:Label>
                </td>
                <td valign="middle">
                    <div class="textbox204">
                        <asp:TextBox ID="txtOrder" runat="server" ValidationGroup="req"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="150">
                    <asp:Label ID="lbluser" runat="server" Text="Add Position Name"></asp:Label>
                </td>
                <td width="204">
                    <div class="textbox204">
                        <asp:TextBox ID="txtPosition" runat="server" ValidationGroup="req"></asp:TextBox>
                    </div>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Is Position Active ?"></asp:Label>
                </td>
                <td>
                    <div class="checkbox">
                        <asp:CheckBox ID="chkActive" runat="server" AutoPostBack="false" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Position Abbriv."></asp:Label>
                </td>
                <td>
                    <div class="textbox204">
                        <asp:TextBox ID="txtAbbrv" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td colspan="2" align="center">
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/btn_addit.png" BgColor="Transparent"
                        OnClick="btnSave_Click" OnClientClick="javascript:return confirm('Are you sure that you wish to create a new position? After adding it, you will only be allowed to enable or disable it! Deleting a position is not allowed!');" />
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/btn_savechanges.png"
                        BgColor="Transparent" OnClick="btnEdit_Click" Visible="false" />
                    <asp:ImageButton ID="imgBtnCancel" runat="server" ImageUrl="~/Images/btn_cancel.png"
                        OnClick="imgBtnCancel_Click" />
                    <asp:HiddenField ID="hdnID" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
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
                    Positions
                    <div class="clear_10">
                    </div>
                    <asp:GridView ID="grdDepartment" AllowSorting="True" AllowPaging="True" runat="server"
                        AutoGenerateColumns="False" HeaderStyle-CssClass="header_row" AlternatingRowStyle-CssClass="alternate_row"
                        RowStyle-CssClass="rowstyle" Width="100%" CellPadding="0" BorderStyle="None"
                        BorderWidth="0" GridLines="None" OnRowCommand="grdDepartment_RowCommand" OnRowDataBound="grdDepartment_RowDataBound"
                        OnPageIndexChanging="grdDepartment_PageIndexChanging">
                        <Columns>
                            <%--<asp:BoundField DataField="sSpeciality" HeaderText="Position Name" ItemStyle-Width="150px" />--%>
                            <asp:TemplateField HeaderText="Position Name">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPosition" runat="server" CommandName="edt" CommandArgument="<%#Bind('pkSpecialityID') %>"
                                        Text="<%#Bind('sSpeciality') %>"> </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Abbrv" HeaderText="Abbriv." ItemStyle-Width="150px" />
                            <asp:BoundField DataField="OrderID" HeaderText="Order" ItemStyle-Width="100px" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <table border="0" cellspacing="0" cellpadding="0" align="right">
                                        <tr>
                                            <td class="align_left" style="width: 200px;">
                                                <asp:ImageButton ID="imgBtnIncome" runat="server" ImageUrl="~/Images/Dollar_grey.png"
                                                    CommandName="income" CommandArgument='<%# Bind("pkSpecialityID") %>' />
                                            </td>
                                            <td class="align_left" style="width: 200px;">
                                                <%--<img ID="imgActive" runat="server" src="../images/activate_icon.gif" />--%>
                                                <asp:ImageButton ID="imgbtnActive" runat="server" CommandName="Active" CommandArgument='<%# Bind("pkSpecialityID") %>'
                                                    ImageUrl="../images/activate_icon.gif" />
                                            </td>
                                            <td class="align_right" style="padding-right: 10px;" style="width: 20px;">
                                                <asp:ImageButton ID="imgedit" runat="server" CommandName="Change" Visible="false"
                                                    CommandArgument='<%# Bind("pkSpecialityID") %>' ImageUrl="../images/edit.png"
                                                    ToolTip="Edit" />
                                            </td>
                                            <td class="align_right" style="width: 20px;">
                                                <asp:ImageButton ID="imgDelete" runat="server" CommandName="Del" Visible="false"
                                                    CommandArgument='<%# Bind("pkSpecialityID") %>' ImageUrl="../images/close.png"
                                                    ToolTip="Delete" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle />
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
