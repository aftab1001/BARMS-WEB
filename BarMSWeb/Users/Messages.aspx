<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/UserMaster.master" AutoEventWireup="true" CodeFile="Messages.aspx.cs" Inherits="Managers_Messages" %>
<%@ Register TagPrefix="BMS" TagName="Messaging" Src="~/UserControls/Messaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<link rel="stylesheet" type="text/css" href="../Styles/jstyle.css" />    

<script src="../JavaScript/ToolTip.js" type="text/javascript"></script>
 <script src="../JavaScript/jquery.js" type="text/javascript"></script>
 <script src="../JavaScript/fcbkcomplete.min.js" type="text/javascript"></script>
 <asp:HiddenField ID="hidSelectedUsers" runat="server" />
<div class="content_area_big">
        <div class="clear_30">
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td class="graybox_topleftcorner"></td>
    <td class="graybox_top_bg"></td>    
    <td class="graybox_toprightcorner"></td>
  </tr>
  <tr>
    <td class="graybox_centerleftbg"></td>
    <td class="graybox_centermiddlebg"><div class="graybox_big">
                    
            <div class="heading" align="center">
                My Messages</div>
            <div class="clear_10">
            </div>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td class="whitebox_topleftcorner"></td>
    <td class="whitebox_top_bg"></td>    
    <td class="whitebox_toprightcorner"></td>
  </tr>
  <tr>
    <td class="whitebox_centerleftbg"></td>
    <td class="whitebox_centermiddlebg">
    <div class="rounded_box_big">
                <BMS:Messaging ID="myID" runat="server" />
            </div></td>
    <td class="whitebox_centerrightbg"></td>
  </tr>
  <tr>
    <td class="whitebox_bottomleftcorner"></td>
    <td class="whitebox_bottom_bg"></td>
    <td class="whitebox_bottomrightcorner"></td>
  </tr>
</table>
            
        </div></td>
    <td class="graybox_centerrightbg"></td>
  </tr>
  <tr>
    <td class="graybox_bottomleftcorner"></td>
    <td class="graybox_bottom_bg"></td>
    <td class="graybox_bottomrightcorner"></td>
  </tr>
</table>
    </div>
<div class="clear_75">
    </div>

</asp:Content>

