<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gallery.aspx.cs" Inherits="Capstone_Project.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userWrapper">
        <div id="galleryLeft">
            <img id="imageBig" src="Site_Images/placeholder.png" />
            <div id="caption">
                <h1 id="imageTitle">@Username</h1>
                <p id="imageInfo"></p>
            </div>
        </div>
        <asp:Panel ID="galleryRight" runat="server">
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
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
            <img class="galleryImage" />
        </asp:Panel>
    </div>

    <%
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();     // used to get image data list from backend
    %>

    <script type="text/javascript">
        var galleryList = <%= serializer.Serialize(getImages()) %>;
        var images = document.getElementsByClassName("galleryImage");

        <%-- function run when an image is clicked to call up its info --%>
        function maximizeImage(imageNumber) {
            document.getElementById("imageBig").setAttribute("src", "Uploads/" + galleryList[imageNumber * 5])
            document.getElementById("imageTitle").innerHTML = galleryList[imageNumber * 5 + 1];

            var description = "<em>Medium:</em> " + galleryList[imageNumber * 5 + 2];
            date = galleryList[imageNumber * 5 + 3]
            date = date.substring(0, date.length - 12);
            description += "<br /><em>Completed On:</em> " + date;
            description += "<br /><em>Description:</em> " + galleryList[imageNumber * 5 + 4];

            document.getElementById("imageInfo").innerHTML = description;
        }

        for (var i = 0; i < (galleryList.length / 5); i++) { <%-- serializer converts to 1-dimensional array, so use 1/5 of length --%>
            images[i].setAttribute("src", "Uploads/" + galleryList[i * 5]);
            images[i].setAttribute("onclick", "maximizeImage(" + i + ")");
        }
    </script>

</asp:Content>