<%@ Page Title="ProgettazioneAnalisi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  EnableEventValidation="false"  CodeBehind="ProgettazioneAnalisi.aspx.cs" Inherits="Juppiter.Analytics_Pages.ProgettazioneAnalisi" %>
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
                    <img src="/Resources/progettazione16x16_gray.png" /><span>Progettazione Analisi</span></a></li>
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
                <asp:ImageButton runat="server" ImageAlign="Middle" ImageUrl="~/Immagini/minus.png" BorderStyle="None" CommandArgument="ContentImportazioneDati" OnClick="ImageButton_Show"></asp:ImageButton>
                <label>IMPORTAZIONE DATI</label>

            </td>
        </tr>
        <tr runat="server" id="ContentImportazioneDati" class="Content">
            <td class="LeftContent">
                <asp:Button CssClass="btn" runat="server" Text="Seleziona" ID="ButtonSeleziona" />
                <br />
                <br />
                <fieldset style="width: 60%; height: 40%">
                    <asp:Label runat="server" ID="LabelImporta"> </asp:Label>
                </fieldset>
                <br />
                <asp:Button CssClass="btn" runat="server" Text="OK" ID="ButtonOK" />
                <asp:Button CssClass="btn" runat="server" Text="ANNULLA" ID="ButtonAnnulla" />

            </td>
            <td class="RightContent">
                <fieldset style="height: 90%;">
                    <asp:Label runat="server" ID="LabelImportati"> </asp:Label>
                </fieldset>
            </td>
        </tr>
        <tr class="navbar">
            <td>
                <asp:ImageButton runat="server" ImageAlign="Middle" ImageUrl="~/Immagini/minus.png" BorderStyle="None" CommandArgument="ContentImpostazioneFiltri" OnClick="ImageButton_Show"></asp:ImageButton>
                <label>IMPOSTAZIONE FILTRI</label>

            </td>
        </tr>
        <tr class="Content" id="ContentImpostazioneFiltri" runat="server">
            <td class="LeftContent">
                <asp:ListView ID="LViewFilter" runat="server">
                    <ItemTemplate>
                        <asp:Button CssClass="btn" ID="ButtonSelectFilter" runat="server" Text='<%# Eval("Titolo") %>' ToolTip='<%# Eval("Descrizione") %>' OnClick="ButtonSelectFilter_Click" CommandArgument='<%# Eval("Page") %>' />
                        </ItemTemplate>
                </asp:ListView>
                <div runat="server" id="DivFiltro" Visible="false">
                <asp:GridView ID="GridViewFilter" runat="server" CellPadding="4" Height="100px" Width="100px">
                    <Columns>
                        <asp:TemplateField HeaderText="Seleziona">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckboxFiltro" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Button CssClass="btn" Text="Seleziona Elementi" OnClick="ButtonSelezione_Click" runat="server" ID="ButtonSelezione" />
                </div>
            </td>
            <td class="RightContent">
                <fieldset style="height: 90%;">
                    <asp:GridView ID="GridViewFilterScelti" runat="server" CellPadding="4" Height="100px" Width="100px">
                        <Columns>
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </td>
        </tr>
        <tr class="navbar">
            <td>
                <asp:ImageButton runat="server" ImageAlign="Middle" ImageUrl="~/Immagini/minus.png" BorderStyle="None" CommandArgument="ContentEsecuzioneAnalisi" OnClick="ImageButton_Show"></asp:ImageButton>
                <label>ESECUZIONE ANALISI</label>
            </td>
        </tr>
        <tr class="Content" id="ContentEsecuzioneAnalisi" runat="server">
            <td class="LeftContent">

                <asp:Button CssClass="btn" runat="server" Text="RUN" />

            </td>
            <td class="RightContent">

                <fieldset style="height: 90%;">
                    <asp:Label runat="server" ID="Label1"> </asp:Label>
                </fieldset>

            </td>
        </tr>
    </table>   
</asp:Content>
