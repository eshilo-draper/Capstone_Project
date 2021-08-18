<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="Capstone_Project.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="uploadForm" runat="server" DefaultButton ="btnUpload">
        <div id="uploadLeft">
            <asp:FileUpload ID="imageUpload" runat="server" />
            <img id="imagePreview"/>
            <script>
                document.getElementById("MainContent_imageUpload").addEventListener("change", function () {
                    if (this.files && this.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById('imagePreview').setAttribute('src', e.target.result);
                        }

                        reader.readAsDataURL(this.files[0]);
                    }
                });
            </script>
        </div>
        <div id="uploadRight">
            <p>Title: </p><asp:Textbox ID="txt_Title" runat="server" /><br />
            <p>Medium: </p><asp:TextBox ID="txt_Medium" runat="server" /><br />
            <p>Completion Date: </p><asp:TextBox TextMode="Date" ID="dtp_completionDate" runat="server" /><br />
            <p>Description: </p><asp:TextBox TextMode="MultiLine" cols="50" Rows="10" ID="txt_Description" runat="server" /><br />
            <br />
            <asp:Button ID="btnUpload" class="button" runat="server" OnClick="btnUpload_Click" Text="Upload"/>
        </div>
    </asp:Panel>
</asp:Content>