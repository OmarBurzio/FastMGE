<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProgettazioneAnalisi.aspx.cs" Inherits="Juppiter.Pages_Analytics.ProgettazioneAnalisi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="navbar">
        <asp:imagebutton runat="server" ImageAlign="Middle" ImageUrl="~/Immagini/minus.png" BorderStyle="None"  ></asp:imagebutton>
        <label>IMPORTAZIONE DATI</label>
    </div>
    <div class="Content" id="ContentImportazioneDati"> 
        <div class="LeftContent">
            <asp:button runat="server" text="Seleziona" id="ButtonSeleziona" />
            <fieldset style="width:40vh;">
                <asp:label runat="server" id="LabelImporta"> </asp:label>               
            </fieldset>
            <asp:button runat="server" text="OK" id="ButtonOK" />
            <asp:button runat="server" text="ANNULLA" id="ButtonAnnulla" />
        </div>       
        <div class="RightContent">
            <fieldset style="height:100%;">
                <asp:label runat="server" id="LabelImportati"> </asp:label>                
            </fieldset>
        </div>
    </div>
</asp:Content>
