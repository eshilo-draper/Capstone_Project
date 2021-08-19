<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAccount.aspx.cs" Inherits="Capstone_Project.EditAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Edit Account Details</h1>
    <div id="accountEditWrapper">

        <p>Username:</p>
        <asp:TextBox ID="txt_username" runat="server" />

        <p>Password:</p>
        <asp:Button ID="btnChangePassword" Text="Change Password" runat="server" class="button"/>

        <p>Display Name:</p>
        <asp:TextBox ID="txt_displayName" runat="server" />

        <p>E-mail Address:</p>
        <asp:TextBox ID="txt_email" runat="server" />

        <p>Avatar:</p>
        <div id="uploadAvatar">
            <asp:FileUpload ID="btnUploadAvatar" runat="server" class="hidden"/>
            <label for="MainContent_btnUploadAvatar">
                <asp:Image ID="avatarPreview" runat="server"/>
            </label>
        </div>

        <p>Biography:</p>
        <asp:TextBox TextMode="MultiLine" ID="txt_bio" runat="server" />

        <asp:Button ID="btnSubmitEdit" Text="Update" runat="server" onClick="btnSubmitEdit_Click" class="button" />
        <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>
    <script>
                document.getElementById("MainContent_btnUploadAvatar").addEventListener("change", function () {
                    <%-- if a file has been uploaded --%>
                    if (this.files && this.files[0]) {
                        <%-- load uploaded image into preview --%>
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById('MainContent_avatarPreview').setAttribute('src', e.target.result);
                        }

                        reader.readAsDataURL(this.files[0]);
                    }
                });
    </script>

</asp:Content>