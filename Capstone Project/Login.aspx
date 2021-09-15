<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Capstone_Project.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="accountForm" runat="server" DefaultButton="submit">
        <div id="accountWrapper">
            <h2>Login</h2>
            <p>Username:</p> <asp:TextBox ID="username" runat="server" />
            <p>Password:</p> <asp:TextBox ID="password" runat="server" TextMode="Password"/>
            <asp:Button Text="submit" class="button" ID="submit" OnClick="submit_Click" runat="server" />
            <asp:Label ID="lblLoginError" runat="server"/>
        </div>
    </asp:Panel>
</asp:Content>