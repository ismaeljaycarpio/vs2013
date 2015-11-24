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
        <div class="col-lg-6">
            <h4><b><%= DateTime.Now.ToString("MMMMM") %></b></h4>
            <p>Number of  this month:
                <asp:LinkButton ID="lnkBdayCount" runat="server"></asp:LinkButton></p>
        </div>

        <div class="col-lg-6"></div>
    </div>
</asp:Content>
