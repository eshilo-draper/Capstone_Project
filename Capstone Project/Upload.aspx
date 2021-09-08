<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="Capstone_Project.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="uploadForm" runat="server" DefaultButton ="btnUpload">
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
                            document.getElementById('MainContent_imagePreview').setAttribute('src', e.target.result);
                        }

                        reader.readAsDataURL(this.files[0]);
                    }
                });

                document.addEventListener("DOMContentLoaded", function () {
                    <%
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();     // used to get image data list from backend
                    %>
                    var imageData = <%= serializer.Serialize(getCurrentMetadata()) %>;

                    <%-- if data was returned (i.e. editing previous upload) --%>
                    if (imageData.length != 0) {
                        document.getElementById("MainContent_tempLabel").classList.add("hidden");

                        document.getElementById("MainContent_imagePreview").src = "Uploads/" + imageData[2];
                        document.getElementById("MainContent_txt_Title").value = imageData[3];
                        document.getElementById("MainContent_txt_Medium").value = imageData[4];
                        document.getElementById("MainContent_dtp_completionDate").value = imageData[5];
                        document.getElementById("MainContent_txt_Description").value = imageData[6];
                    }
                })

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