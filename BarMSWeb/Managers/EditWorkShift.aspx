<%@ Page Language="C#" MasterPageFile="~/MasterPages/ManagerMaster.master" AutoEventWireup="true" CodeFile="EditWorkShift.aspx.cs" Inherits="Managers_EditWorkShift"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script>
	$(function() {
		$( "#ctl00_ContentPlaceHolder1_datepicker" ).datepicker({
			showOn: "button",
			buttonImage: "../images/calender.png",
			buttonImageOnly: true
		});
	});
    </script>

    <table cellpadding="1" cellspacing="1" border="0" width="100%">
        <tr>
            <td colspan="3" style="text-align: center;">
                <asp:Label ID="lblError" runat="server" CssClass="Error" Visible="false"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px;">
                <asp:Label ID="lblUser" runat="server" Text="Users"></asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlusers" runat="server" AutoPostBack="false">
                </asp:DropDownList></td>
            <td style="text-align: right;">
                <a href="ManageWorkShifts.aspx">Manage Work shifts</a>
                
                
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Enter Date"></asp:Label></td>
            <td>
                <div class="demo textbox_date">
                    <%--<input id="datepicker" type="text" />--%>
                    <asp:TextBox ID="datepicker" runat="server"></asp:TextBox>
                </div>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:ImageButton ID="btnGO" runat="server" ImageUrl="~/Images/btn_load.png" OnClick="btnGO_Click" /></td>
            <td>
            </td>
        </tr>
    </table>
    <div class="clear_30">
    </div>
    <div id="ShowWorkshift" runat="server" style="display: none;">
        <table cellpadding="1" cellspacing="1" border="0" align="left">
            <tr>
                <td colspan="10">
                    <asp:Label ID="lblWeek" Font-Bold="true" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="10" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td style="width:100px;">
                    <asp:Label ID="lblMonday" Font-Bold="true" runat="server" Text="Monday"></asp:Label></td>
                <td>
                    <asp:Label ID="lblMondayDate" runat="server" Text=""></asp:Label></td>
                <td>
                    <asp:DropDownList ID="ddlMondayPosition" runat="server">
                    </asp:DropDownList></td>
                <td>
                    <asp:TextBox ID="MondayStartHour" runat="server" Width="30"></asp:TextBox></td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label2" runat="server" Text=":"></asp:Label></td>
                <td>
                    <asp:TextBox ID="MondayStartMin" runat="server" Width="30"></asp:TextBox></td>
                <td>
                    <asp:TextBox ID="MondayEndHour" runat="server" Width="30"></asp:TextBox></td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label3" runat="server" Text=":"></asp:Label></td>
                <td>
                    <asp:TextBox ID="MondayEndMin" runat="server" Width="30"></asp:TextBox></td>
                <td>
                    <asp:CheckBox ID="chkMondayOFF" runat="server" Text="OFF Day" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTuesday" Font-Bold="true" runat="server" Text="Tuesday"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTuesdayDate" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTuesdayPosition" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="TuesdayStartHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label4" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TuesdayStartMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TuesdayEndHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label5" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TuesdayEndMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="chkTuesdayOFF" runat="server" Text="OFF Day" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblWednesday" Font-Bold="true" runat="server" Text="Wednesday"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblWednesdayDate" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlWednesdayPosition" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="WednesdayStartHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label6" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="WednesdayStartMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="WednesdayEndHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label7" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="WednesdayEndMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="chkWednesdayOFF" runat="server" Text="OFF Day" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblThursday" Font-Bold="true" runat="server" Text="Thursday"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblThursdayDate" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlThursdayPosition" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="ThursdayStartHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label8" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ThursdayStartMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ThursdayEndHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label9" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="ThursdayEndMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="chkThursdayOFF" runat="server" Text="OFF Day" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFriday" Font-Bold="true" runat="server" Text="Friday"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblFridayDate" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlFridayPosition" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="FridayStartHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label10" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="FridayStartMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="FridayEndHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label11" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="FridayEndMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="chkFridayOFF" runat="server" Text="OFF Day" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSaturday" Font-Bold="true" runat="server" Text="Saturday"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSaturdayDate" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSaturdayPosition" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="SaturdayStartHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label12" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="SaturdayStartMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="SaturdayEndHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label13" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="SaturdayEndMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="chkSaturdayOFF" runat="server" Text="OFF Day" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSunday" Font-Bold="true" runat="server" Text="Sunday"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblSundayDate" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSundayPosition" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="SundayStartHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label14" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="SundayStartMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="SundayEndHour" runat="server" Width="30"></asp:TextBox>
                </td>
                <td style="width:20px; text-align:center;">
                    <asp:Label ID="Label15" runat="server" Text=":"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="SundayEndMin" runat="server" Width="30"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="chkSundayOFF" runat="server" Text="OFF Day" />
                </td>
            </tr>
            <tr>
                <td colspan="10" style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td colspan="10" style="text-align: left;">
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/btn_save.png" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>




</asp:Content>

