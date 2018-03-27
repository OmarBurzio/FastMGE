<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Analytics.aspx.cs" Inherits="Juppiter.Analytics" %>
<asp:Content ID="ContentMenu" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <link href="/CSS/GlobalStylesheet.css" rel="stylesheet" />
    <div class="main_menu">
        <%--<asp:Menu ID="MenuPage" runat="server" Orientation="Horizontal">
             <Items>
                <asp:MenuItem Text="File" Value="File" ImageUrl="~/Resources/file16x16_gray.png">
                    <asp:MenuItem Text="Open" Value="Open" ImageUrl="~/Resources/folder16x16_gray.png"></asp:MenuItem>
                    <asp:MenuItem Text="New" Value="New" ImageUrl="~/Resources/newfile16x16_gray.png"></asp:MenuItem>
                    <asp:MenuItem Text="Save" Value="Save" ImageUrl="~/Resources/Save16x16_gray.png"></asp:MenuItem>
                    <asp:MenuItem Text="SaveAs" Value="SaveAs" ImageUrl="~/Resources/save_as_16x16_gray.png"></asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Progettazione Analisi" Value="Progettazione Analisi" ImageUrl="~/Resources/progettazione16x16_gray.png" NavigateUrl="~/Pages Analytics/ProgettazioneAnalisi.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Settings" Value="Settings" ImageUrl="~/Resources/settings_16x16_gray.png"  NavigateUrl="~/Pages Analytics/Settings.aspx"></asp:MenuItem>
                <asp:MenuItem Text="Esci" Value="Esci" ImageUrl="~/Resources/arrow_left16x16_gray.png" ></asp:MenuItem>
            </Items>
        </asp:Menu>--%>

        <div class="divMenuPage">
            <ul>
                <li>
                    <a href="#"><img src="./Resources/file16x16_gray.png" /><span>File</span></a>
                    <ul>
                        <li><a href="#"><img src="./Resources/folder16x16_gray.png" /><span>Open</span></a></li>
                        <li><a href="#"><img src="./Resources/newfile16x16_gray.png" /><span>New</span></a></li>
                        <li><a href="#"><img src="./Resources/Save16x16_gray.png" /><span>Save</span></a></li>
                        <li><a href="#"><img src="./Resources/save_as_16x16_gray.png" /><span>SaveAs</span></a></li>
                    </ul>
                </li>
                <li><a href="./Analytics Pages/ProgettazioneAnalisi.aspx"><img src="Resources/progettazione16x16_gray.png" /><span>Progettazione Analisi</span></a></li>
                <li><a href="./Analytics Pages/Settings.aspx"><img src="./Resources/settings_16x16_gray.png" /><span>Settings</span></a></li>
                <li><a href="#"><img src="./Resources/arrow_left16x16_gray.png" /><span>Esci</span></a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">
    <div class="main">
        &nbsp;
    </div>
</asp:Content>
