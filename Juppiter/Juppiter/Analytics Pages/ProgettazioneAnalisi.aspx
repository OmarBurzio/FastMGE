<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProgettazioneAnalisi.aspx.cs" Inherits="Juppiter.Analytics_Pages.ProgettazioneAnalisi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <table class="main">
        <tr>
            <td class="navbar">
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
        <tr>
            <td class="navbar">
                <asp:ImageButton runat="server" ImageAlign="Middle" ImageUrl="~/Immagini/minus.png" BorderStyle="None" CommandArgument="ContentImpostazioneFiltri" OnClick="ImageButton_Show"></asp:ImageButton>
                <label>IMPOSTAZIONE FILTRI</label>

            </td>
        </tr>
        <tr class="Content" id="ContentImpostazioneFiltri" runat="server">
            <td class="LeftContent">               
                    <asp:ListView ID="LViewFilter" runat="server">
                        <ItemTemplate>
                            <asp:Button CssClass="btn" ID="ButtonSelectFilter" runat="server" Text='<%# Eval("Titolo") %>' ToolTip='<%# Eval("Descrizione") %>' CommandArgument='<%# Eval("Page") %>' />
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:ListView>                
            </td>
            <td class="RightContent">               
                    <fieldset style="height: 90%;">
                        <asp:Label runat="server" ID="Label2"> </asp:Label>
                    </fieldset>               
            </td>
        </tr>
        <tr >
            <td class="navbar">                
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
