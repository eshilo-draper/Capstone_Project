<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Capstone_Project._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="homeWrapper">
        <h1>About MyPortfolio</h1>
        <div id="homeTop">
            <div class="homeText">
                <h2>MyPortfolio is an easy way to show off your art</h2>
                <hr />
                <p>With MyPortfolio, just a few clicks and a gallery of your art creates itself for you. Just grab the link, and share your art with anyone! Good for showing to prospective employers, people looking to commission, or anyone you want to see your creations!</p>
            </div>
            <img class="homeImage" src="Site_Images/placeholder.png" /> <!-- REPLACE IMAGE -->
        </div>
        <div id="homeBottom">
            <img class="homeImage" src="Site_Images/placeholder.png" /> <!-- REPLACE IMAGE -->
            <div class="homeText">
                <h2>A gallery curated by the user</h2>
                <hr />
                <p>Viewers of a portfolio can click on each image to maximize it and read a bit about it. Images can be sorted by medium or arranged by date, ascending or descending. To return to the artist’s photo and biography, just click the name at the top of the page.</p>
            </div>
        </div>
    </div>

</asp:Content>
