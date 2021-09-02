<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="Capstone_Project.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="uploadForm" runat="server" DefaultButton ="btnUpload">
        <asp:Label ID="lblImageID" runat="server" />
        <div id="uploadLeft">
            <asp:FileUpload ID="imageUpload" runat="server" class="hidden"/>
            <label for="MainContent_imageUpload" id="uploadLabel">
                <asp:Label ID="tempLabel" runat="server">Upload Image</asp:Label>
                <asp:Image ID="imagePreview" runat="server"/>
            </label>
            <script>
                document.getElementById("MainContent_imageUpload").addEventListener("change", function () {
                    <%-- if a file has been uploaded --%>
                    if (this.files && this.files[0]) {
                        <%-- hide temp text label --%>
                        document.getElementById("MainContent_tempLabel").classList.add("hidden");

                        <%-- load uploaded image into preview --%>
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
            <asp:Button ID="btnSaveChanges" class="button" runat="server" Text="Save Changes"  OnClick="btnSaveChanges_Click"/>
        </div>
    </asp:Panel>
</asp:Content>