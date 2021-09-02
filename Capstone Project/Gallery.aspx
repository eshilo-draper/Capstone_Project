<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gallery.aspx.cs" Inherits="Capstone_Project.User" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="userWrapper">
        <div id="galleryLeft">
            <asp:Image ID="imageBig" runat="server"/>
            <div id="caption">
                <asp:Label ID="lblImageTitle" runat="server"></asp:Label>
                <asp:Label ID="lblImageInfo" runat="server"></asp:Label>
            </div>
        </div>
        <div id="galleryRight">
        </div>
    </div>

    <%
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();     // used to get image data list from backend
    %>

    <script type="text/javascript">
        <%-- get list of images immediately --%>
        var galleryList = <%= serializer.Serialize(getImages()) %>;

        <%-- declare global variables --%>
        var avatar;
        var name;
        var bio;

        <%-- on page load --%>
        document.addEventListener("DOMContentLoaded", function ()
        {
            <%-- hide footer --%>
            document.getElementById("sticky-footer").classList.add("hidden");

            <%-- give clickable class to banner (just adds cursor change on hover) --%>
            document.getElementById("banner").classList.add("clickable");

            <%-- load gallery images --%>
            for (var i = 0; i < (galleryList.length / 5); i++) { <%-- serializer converts to 1-dimensional array, so use 1/5 of length --%>
                var img = document.createElement("img");
                img.classList.add("galleryImage");
                img.classList.add("clickable");
                img.setAttribute("src", "Uploads/" + galleryList[i * 5]);
                img.setAttribute("onclick", "maximizeImage(" + i + ")");
                document.getElementById("galleryRight").appendChild(img);
            }

            <%-- store bio, display name and avatar for later display --%>
            avatar = document.getElementById("MainContent_imageBig").src;
            name = document.getElementById("MainContent_lblImageTitle").innerHTML;
            bio = document.getElementById("MainContent_lblImageInfo").innerHTML;

            <%-- display name in banner --%>
            document.getElementById("banner").innerHTML = name;
        });

        <%-- function run when an image is clicked to call up its info --%>
        function maximizeImage(imageNumber) {
            document.getElementById("MainContent_imageBig").setAttribute("src", "Uploads/" + galleryList[imageNumber * 5])
            document.getElementById("MainContent_lblImageTitle").innerHTML = galleryList[imageNumber * 5 + 1];

            var description = "<em>Medium:</em> " + galleryList[imageNumber * 5 + 2];
            date = galleryList[imageNumber * 5 + 3]
            date = date.substring(0, date.length - 12);
            description += "<br /><em>Completed On:</em> " + date;
            description += "<br /><em>Description:</em> " + galleryList[imageNumber * 5 + 4];

            document.getElementById("MainContent_lblImageInfo").innerHTML = description;
        }

        <%-- event listener for when banner is clicked to call user info back up --%>
        document.getElementById("banner").addEventListener("click", function () {
            document.getElementById("MainContent_imageBig").setAttribute("src", avatar);
            document.getElementById("MainContent_lblImageTitle").innerHTML = name;
            document.getElementById("MainContent_lblImageInfo").innerHTML = bio;
        })

    </script>

</asp:Content>