<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="Capstone_Project.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="uploadForm" runat="server" DefaultButton ="btnUpload">
        <asp:FileUpload ID="imageUpload" runat="server" /> <!-- TO BE REPLACED BY CROPPER.JS -->
        <p>Title: </p><asp:Textbox ID="txt_Title" runat="server" /><br />
        <p>Medium: </p><asp:TextBox ID="txt_Medium" runat="server" /><br />
        <p>Completion Date: </p><asp:TextBox TextMode="Date" ID="dtp_completionDate" runat="server" /><br />
        <p>Description: </p><asp:TextBox TextMode="MultiLine" ID="txt_Description" runat="server" /><br />
        <br />
        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload"/>
    </asp:Panel>
</asp:Content>