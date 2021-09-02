<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Capstone_Project.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="accountForm" runat="server" DefaultButton="btnChangePW">
        <div id="accountWrapper">
            <h2>Change Password</h2>
            <p>Confirm Old Password:</p><asp:TextBox ID="txtOldPW" TextMode="Password" runat="server" />
            <p>New Password:</p><asp:TextBox ID="txtNewPW" TextMode="Password" runat="server" />
            <p>Confirm New Password:</p><asp:TextBox ID="txtConfirmNewPW" TextMode="Password" runat="server" />
            <asp:Button ID="btnChangePW" text="Change Password" runat="server" OnClick="btnChangePW_Click" class="button"/>
            <asp:Label ID="lblError" runat="server"/>
        </div>
    </asp:Panel>
</asp:Content>