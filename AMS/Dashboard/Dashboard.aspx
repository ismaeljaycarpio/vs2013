<%@ Page Title="My Dashboard"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="AMS.Dashboard.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-4">
            <h4><span class="glyphicon glyphicon-cutlery"></span> <b><%= DateTime.Now.ToString("MMMMM") %></b> Birthday Celebrants</h4>
            <p>Number of Employees with Birthday this month:
                <asp:LinkButton ID="lnkBdayCount" runat="server" OnClick="lnkBdayCount_Click"></asp:LinkButton></p>
        </div>

        <div class="col-lg-4">
            <h4>Dashboard category goes here...</h4>
            <p>sample text</p>
        </div>

        <div class="col-lg-4">
            <h4>Dashboard category goes here...</h4>
            <p>sample text</p>
        </div>
    </div>
</asp:Content>
