<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Capstone_Project.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="registerForm" runat="server" DefaultButton="register">
        <div id="registerWrapper">
            <p>Username: </p> <asp:TextBox ID="txt_username" runat="server"/> <br />
            <p>Password: </p> <asp:TextBox ID="txt_password" runat="server" TextMode="Password"/> <br />
            <p>Confirm Password: </p> <asp:TextBox ID="txt_confirmPassword" runat="server" TextMode="Password" /> <br />
            <p>Display Name: </p> <asp:TextBox ID="txt_displayName" runat="server" /> <br />
            <p>Email Address: </p> <asp:TextBox ID="txt_email" runat="server" /> <br />
            <p>Date of Birth: </p> <asp:TextBox ID="dtp_dob" TextMode="Date" runat="server" />
        </div>

        <asp:Button ID="register" Text="Register" runat="server" OnClick="register_Click" />
        <asp:Label ID="lbl_Error" runat="server"/>
    </asp:Panel>
</asp:Content>