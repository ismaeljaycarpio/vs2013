<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AMS.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-4">
            <h4><span class="glyphicon glyphicon-apple"></span>&nbsp;Today's Birthdays - <%: DateTime.Now.Date.ToShortDateString() %></h4>
            <p>
                <asp:Label ID="lblTodayBirthday" runat="server"></asp:Label>
            </p>
        </div>

        <div class="col-lg-4">
            <h4><span class="glyphicon glyphicon-bullhorn"></span>&nbsp;Announcements</h4>
            <div class="table table-responsive">
                <asp:ListView ID="lvAnn" runat="server">
                    <EmptyDataTemplate>
                        <p>No Announcement</p>
                    </EmptyDataTemplate>

                    <ItemSeparatorTemplate>
                        <hr />
                    </ItemSeparatorTemplate>
                    <ItemTemplate>
                        <h4><%# Eval("Title") %></h4>
                        <p class="text-info">Posted on 
                            <%# Eval("PostedDate") %>
                        </p>
                        <asp:Label ID="lblContent" runat="server" Text='<%# Eval("Content") %>' CssClass="wrapAnnouncement"></asp:Label>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>

        <div class="col-lg-4">
            <h4><span class="glyphicon glyphicon-flag"></span>&nbsp;Activities</h4>
            <div class="table table-responsive">
                <asp:ListView ID="lvAct" runat="server">
                    <EmptyDataTemplate>
                        <p>No Activities</p>
                    </EmptyDataTemplate>

                    <ItemSeparatorTemplate>
                        <hr />
                    </ItemSeparatorTemplate>
                    <ItemTemplate>
                        <h4><%# Eval("Title") %></h4>
                        <p class="text-info">Posted on 
                            <%# Eval("PostedDate") %>
                        </p>
                        <asp:Label ID="lblContent" runat="server" Text='<%# Eval("Content") %>' CssClass="wrapAnnouncement"></asp:Label>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>

    </div>
</asp:Content>
