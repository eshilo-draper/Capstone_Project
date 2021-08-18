<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Capstone_Project.Logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="logoutWrapper">
        <h3>Are you sure you want to log out?</h3><br />
        <asp:Button ID="btnLogoutConfirm" class="button" Text="Logout" runat="server" OnClick="btnLogoutConfirm_Click"/>
        <asp:Button ID="btnCancel" class="button" Text="Cancel" runat="server" OnClick="btnCancel_Click"/>
    </div>
</asp:Content>