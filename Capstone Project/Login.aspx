<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Capstone_Project.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="loginForm" runat="server" DefaultButton="submit">
        <h2>Login</h2>
        Username: <asp:TextBox ID="username" runat="server" />
        Password: <asp:TextBox ID="password" runat="server" TextMode="Password"/>
        <asp:Button Text="submit" ID="submit" OnClick="submit_Click" runat="server" />
        <div id="error">
            <asp:Label ID="errorLabel" runat="server"/>
        </div>
    </asp:Panel>
</asp:Content>