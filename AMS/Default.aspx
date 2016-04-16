<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AMS.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script src="/jquery.bxslider/jquery.bxslider.min.js"></script>
    <!-- bxSlider CSS file -->
    <link href="/jquery.bxslider/jquery.bxslider.css" rel="stylesheet" />--%>

    <div class="row">
        <%--<div class="col-md-12">
            <ul class="bxslider">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <li></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>--%>
                <div class="col-lg-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4><span class="glyphicon glyphicon-bullhorn"></span>&nbsp;Announcements</h4>
                </div>
                <div class="panel-body">
                    <asp:ListView ID="lvAnn" runat="server" >
                        <EmptyDataTemplate>
                            <p>No Announcement</p>
                        </EmptyDataTemplate>

                        <ItemSeparatorTemplate>
                            <hr />
                        </ItemSeparatorTemplate>
                        <ItemTemplate>
                            <h4><%# Eval("Title") %></h4>
                            <p class="text-info">
                                Posted on 
                            <%# Eval("PostedDate") %>
                            </p>
                            <asp:Label ID="lblContent" runat="server" Text='<%# Eval("Content") %>' CssClass="wrapAnnouncement"></asp:Label>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>

        <div class="col-lg-4">
            <h4><span class="glyphicon glyphicon-calendar"></span>&nbsp;Today's Birthdays - <%: DateTime.Now.Date.ToShortDateString() %></h4>
            <p>
                <asp:Label ID="lblTodayBirthday" runat="server"></asp:Label>
            </p>
        </div>

        <div class="col-lg-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h4><span class="glyphicon glyphicon-flag"></span>&nbsp;Activities</h4>
                </div>
                <div class="panel-body">
                    <asp:ListView ID="lvAct" runat="server">
                        <EmptyDataTemplate>
                            <p>No Activities</p>
                        </EmptyDataTemplate>

                        <ItemSeparatorTemplate>
                            <hr />
                        </ItemSeparatorTemplate>
                        <ItemTemplate>
                            <h4><%# Eval("Title") %></h4>
                            <p class="text-info">
                                Posted on 
                            <%# Eval("PostedDate") %>
                            </p>
                            <asp:Label ID="lblContent" runat="server" Text='<%# Eval("Content") %>' CssClass="wrapAnnouncement"></asp:Label>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>

    <%--<script>
        $(document).ready(function () {
            $('.bxslider').bxSlider();
        });
    </script>--%>
</asp:Content>
