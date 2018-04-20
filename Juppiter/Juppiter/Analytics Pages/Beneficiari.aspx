<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Beneficiari.aspx.cs" Inherits="Juppiter.Analytics_Pages.Beneficiari" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <link href="/CSS/GlobalStylesheet.css" rel="stylesheet" />
    <div class="main_menu">
        <div class="divMenuPage">
            <ul>
                <li>
                    <a href="#">
                        <img src="/Resources/file16x16_gray.png" /><span>File</span></a>
                    <ul>
                        <li><a href="#">
                            <img src="/Resources/folder16x16_gray.png" /><span>Open</span></a></li>
                        <li><a href="#">
                            <img src="/Resources/newfile16x16_gray.png" /><span>New</span></a></li>
                        <li><a href="#">
                            <img src="/Resources/Save16x16_gray.png" /><span>Save</span></a></li>
                        <li><a href="#">
                            <img src="/Resources/save_as_16x16_gray.png" /><span>SaveAs</span></a></li>
                    </ul>
                </li>
                <li><a href="/Analytics Pages/ProgettazioneAnalisi.aspx">
                    <img src="/Resources/progettazione16x16_gray.png" /><span>Progettazione Analisi</span></a>
                     <ul>
                        <li><a href="ProgettazioneAnalisi.aspx">
                            <img src="../Resources/Movimenti16x16.png" /><span>N° Movimenti</span></a></li>
                        <li><a href="CCChiusi.aspx">
                            <img src="../Resources/cc_chiusi16x16.png" /><span>CC Chiusi</span></a></li>
                        <li><a href="#">
                            <img src="../Resources/Benefits16x16.png" /><span>Beneficiari</span></a></li>                       
                    </ul>
                </li>
                <li><a href="/Analytics Pages/Settings.aspx">
                    <img src="/Resources/settings_16x16_gray.png" /><span>Settings</span></a></li>
                <li><a href="#">
                    <img src="/Resources/arrow_left16x16_gray.png" /><span>Esci</span></a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
     <table class="main">
        <tr class="navbar">
            <td>
                <table>
                    <tr>
                        <td class="tdCollapsableDiv tdImageCollapsableDiv">
                            <asp:ImageButton runat="server" CssClass="tdCollapsableDiv" ImageUrl="~/Resources/arrow_down16x16.png" BorderStyle="None" CommandArgument="ContentImportazioneDati" OnClick="ImageButton_Show"></asp:ImageButton>
                        </td>
                        <td class="tdCollapsableDiv">
                            <label>IMPORTAZIONE DATI</label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
         </table>
</asp:Content>
