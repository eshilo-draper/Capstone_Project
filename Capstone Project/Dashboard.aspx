<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Capstone_Project.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="homeWrapper">
        <asp:ImageButton src="Site_Images/Upload.png" ID="btnUpload" runat="server" OnClick="btnUpload_Click"/>
        <div id="dashboardImageGrid">
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
        </div>
    </div>

    <%
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();     // used to get image data list from backend
    %>

    <script type="text/javascript">
        var galleryList = <%= serializer.Serialize(getImages()) %>;
        var images = document.getElementsByClassName("galleryImage");

        for (var i = 0; i < (galleryList.length / 5); i++) { <%-- serializer converts to 1-dimensional array, so use 1/5 of length --%>
            images[i].setAttribute("src", "Uploads/" + galleryList[i * 5]);
            images[i].setAttribute("onclick", "");
        }
    </script>
</asp:Content>