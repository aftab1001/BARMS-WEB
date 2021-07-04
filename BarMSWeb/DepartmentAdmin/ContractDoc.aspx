<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DepartmentAdminMaster.master"
    AutoEventWireup="true" CodeFile="ContractDoc.aspx.cs" Inherits="DepartmentAdmin_ContractDoc"
    ValidateRequest="false" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" Visible="false" ForeColor="Green"></asp:Label>
                </td>
            </tr>
            <%-- <tr>
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
                                    Bonus:
                                    <div class="clear_10">
                                    </div>
                                    <table>
                                        <tr>
                                            <td>
                                                <FTB:FreeTextBox ID="FreeTextBox1" Focus="true" SupportFolder="FreeTextBox/" JavaScriptLocation="ExternalFile"
                                                    ButtonImagesLocation="ExternalFile" ToolbarImagesLocation="ExternalFile" ToolbarStyleConfiguration="OfficeXP"
                                                    ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,                                   

FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,

Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,

JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;

CreateLink,Unlink,InsertImageFromGallery|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,

StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;

InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,

DeleteTableColumn|InsertForm,InsertTextBox,InsertTextArea,InsertRadioButton,

InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,

InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell" runat="Server" GutterBackColor="red" DesignModeCss="designmode.css"
                                                    ButtonSet="Office2000" ImageGalleryPath="~/images/" />
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
                     <div class="clear_10">
                                    </div>
                </td>
            </tr>--%>
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
                                    <div>
                                        <div style="width: 537px; float: left;">
                                            General Contract:</div>
                                        <div style="float: left;">
                                            <%--<asp:Button ID="btnRestore" runat="server" Text="Restore" OnClick="btnRestore_Click" />--%>
                                            <asp:ImageButton ID="imgBtnRestore" runat="server" ImageUrl="~/Images/btn_restore.png" OnClick="btnRestore_Click" />
                                        </div>
                                    </div>
                                    <div class="clear_10">
                                    </div>
                                    <table>
                                        <tr>
                                            <td>
                                                <FTB:FreeTextBox ID="FreeTextBox2" Focus="true" SupportFolder="FreeTextBox/" JavaScriptLocation="ExternalFile"
                                                    ButtonImagesLocation="ExternalFile" ToolbarImagesLocation="ExternalFile" ToolbarStyleConfiguration="OfficeXP"
                                                    ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,                                   

FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,

Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,

JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;

CreateLink,Unlink,InsertImageFromGallery|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,

StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;

InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,

DeleteTableColumn|InsertForm,InsertTextBox,InsertTextArea,InsertRadioButton,

InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,

InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell" runat="Server" GutterBackColor="red" DesignModeCss="designmode.css"
                                                    ButtonSet="Office2000" ImageGalleryPath="~/images/" />
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
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="imgbtnSubmit" runat="server" ImageUrl="~/Images/btn_save.png"
                        OnClick="imgbtnSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
