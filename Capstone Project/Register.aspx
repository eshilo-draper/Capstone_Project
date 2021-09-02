<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Capstone_Project.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="accountForm" runat="server" DefaultButton="register">
        <div id="accountWrapper">
            <h2>Register</h2>
            <p>Username: </p> <asp:TextBox ID="txt_username" runat="server"/>
            <p>Password: </p> <asp:TextBox ID="txt_password" runat="server" TextMode="Password"/>
            <p>Confirm Password: </p> <asp:TextBox ID="txt_confirmPassword" runat="server" TextMode="Password" />
            <p>Display Name: </p> <asp:TextBox ID="txt_displayName" runat="server" />
            <p>Email Address: </p> <asp:TextBox ID="txt_email" runat="server" />
            <p>Date of Birth: </p> <asp:TextBox ID="dtp_dob" TextMode="Date" runat="server" />
        </div>

        <asp:Button ID="register" class="button" Text="Register" runat="server" OnClick="register_Click" />
        <asp:Label ID="lblError" runat="server"/>
    </asp:Panel>
</asp:Content>