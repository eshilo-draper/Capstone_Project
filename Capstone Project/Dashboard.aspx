<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Capstone_Project.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="homeWrapper">
        <asp:ImageButton src="Site_Images/Upload.png" ID="btnUpload" runat="server" OnClick="btnUpload_Click"/>
        <div id="dashboardImageGrid">
            <!-- TODO: add image grid here -->
        </div>
    </div>
</asp:Content>