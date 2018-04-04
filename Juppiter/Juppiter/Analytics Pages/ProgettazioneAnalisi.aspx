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
    <script>        
        (document).ready(function () {
            $("div.btn-group button.btn").click(function () {
                $("div.btn-group").find(".activeButton").removeClass("activeButton");
                $(this).addClass("activeButton");
            });
        });
    </script>
    <table class="main">
        <tr class="navbar">
            <td>
                <asp:ImageButton runat="server" ImageAlign="Middle" ImageUrl="~/Immagini/minus.png" BorderStyle="None" CommandArgument="ContentImportazioneDati" OnClick="ImageButton_Show"></asp:ImageButton>
                <label>IMPORTAZIONE DATI</label>

            </td>
        </tr>
        <tr runat="server" id="ContentImportazioneDati" class="Content">
            <td class="LeftContent">                   
                <select multiple="true" class="form-control" runat="server" id="selectColl">
                </select>
                <br />
                <br />
                <asp:Button CssClass="btn" runat="server" Text="OK" ID="ButtonOK" OnClick="ButtonOK_Click" />
                <asp:Button CssClass="btn" runat="server" Text="ANNULLA" ID="ButtonAnnulla" OnClick="ButtonAnnulla_Click" CommandArgument="AnnullaImportazione"/>
                <br />
                <br />
            </td>
            <td class="RightContent">
                <asp:Label runat="server" ID="LabelImportati"> </asp:Label>                
                <asp:GridView ID="GridViewDocumenti" runat="server" CellPadding="4" >
                    <Columns>                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr class="navbar">
            <td>
                <br />
                <asp:ImageButton runat="server" ImageAlign="Middle" ImageUrl="~/Immagini/minus.png" BorderStyle="None" CommandArgument="ContentImpostazioneFiltri" OnClick="ImageButton_Show"></asp:ImageButton>
                <label>IMPOSTAZIONE FILTRI</label>                
            </td>
        </tr>        
        <tr class="Content" id="ContentImpostazioneFiltri" runat="server">
            <td class="LeftContent" id="tdP">
                <div class="btn-group" role="group">
                    <asp:ListView ID="LViewFilter" runat="server">
                        <ItemTemplate>
                            <asp:Button CssClass="btn" ID="ButtonSelectFilter" runat="server" Text='<%# Eval("Titolo") %>' ToolTip='<%# Eval("Descrizione") %>' OnClick="ButtonSelectFilter_Click" CommandArgument='<%# Eval("Page") %>' />
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <div runat="server" id="DivFiltro" visible="false">
                    <asp:GridView ID="GridViewFilter" runat="server" CellPadding="4">
                        <Columns>
                            <asp:TemplateField HeaderText="Seleziona">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckboxFiltro" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div id="DivData" runat="server" visible="false">
                    <table>
                        <tr>
                            <td>
                                <label>Data Da</label>
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="CheckDataA" Checked="false" AutoPostBack="true" OnCheckedChanged="CheckDataA_CheckedChanged" />
                                <label>Data A</label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdCalendarContainer" style="padding: 5px">
                                <asp:Calendar CssClass="cssCalendar" ID="CalendarDataDa" runat="server"></asp:Calendar>
                            </td>
                            <td class="tdCalendarContainer" style="padding: 5px">
                                <asp:Calendar CssClass="cssCalendar" ID="CalendarDataA" runat="server" Enabled="false"></asp:Calendar>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="DivStato" runat="server" visible="false">
                    <asp:RadioButtonList ID="RadioStato" runat="server">
                        <asp:ListItem Value="Estinto"></asp:ListItem>
                        <asp:ListItem Value="Aperto"></asp:ListItem>
                        <asp:ListItem Value="Tutto"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div id="DivSegno" runat="server" visible="false">
                    <asp:RadioButtonList ID="RadioSegno" runat="server">
                        <asp:ListItem Value="Entrata"></asp:ListItem>
                        <asp:ListItem Value="Uscita"></asp:ListItem>
                        <asp:ListItem Value="Tutto"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div id="DivButton" runat="server" visible="false">
                    <asp:Button CssClass="btn" runat="server" Text="OK" ID="ButtonSelezione" OnClick="ButtonSelezione_Click" />
                    <asp:Button CssClass="btn" runat="server" Text="ANNULLA" ID="Button2" OnClick="ButtonAnnulla_Click" CommandArgument="AnnullaFiltri" />
                </div>
                <br />
                <br />
            </td>
            <td class="RightContent">
                <asp:GridView ID="GridViewFilterScelti" OnRowCommand="GridViewFilterScelti_RowCommand" runat="server" CellPadding="4">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButtonDeselectFilter" ImageUrl="~/Resources/deleteIcon16x16_gray.png" CommandName="Deseleziona" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
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
