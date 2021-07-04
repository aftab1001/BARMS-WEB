<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ManagerMaster.master" AutoEventWireup="true" CodeFile="Messages.aspx.cs" Inherits="Managers_Messages" %>
<%@ Register TagPrefix="BMS" TagName="Messaging" Src="~/UserControls/Messaging.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<link rel="stylesheet" type="text/css" href="../Styles/jstyle.css" />    

<script src="../JavaScript/ToolTip.js" type="text/javascript"></script>
 <script src="../JavaScript/jquery.js" type="text/javascript"></script>
 <script src="../JavaScript/fcbkcomplete.min.js" type="text/javascript"></script>
 <asp:HiddenField ID="hidSelectedUsers" runat="server" />

<BMS:Messaging ID="myID" runat="server" />

</asp:Content>

