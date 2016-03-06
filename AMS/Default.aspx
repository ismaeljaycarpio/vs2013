<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AMS.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-4">
            <h4><span class="glyphicon glyphicon-apple"></span>Today's Birthdays</h4>
            <p>
                <asp:Label ID="lblTodayBirthday" runat="server"></asp:Label>
            </p>
        </div>

        <div class="col-lg-4">
            <h4><span class="glyphicon glyphicon-calendar"></span>Announcements</h4>
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
                        <asp:Label ID="lblAnn" Text='<%# Eval("Content").ToString().Replace("<br />", Environment.NewLine) %>' runat="server"></asp:Label>

                        <pre>
                            <%# Eval("Content").ToString().Replace(Environment.NewLine, "<br />") %>
                        </pre>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>

        <div class="col-lg-4">
            <h4><span class="glyphicon glyphicon-flag"></span>Activities</h4>
            <div class="table table-responsive">
                <asp:GridView ID="GridView2"
                    runat="server"
                    GridLines="Horizontal"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false"
                    DataKeyNames="Id">
                    <Columns>
                        <asp:BoundField DataField="AccountStatus" HeaderText="Status" />
                        <asp:BoundField DataField="MasterListCount" HeaderText="Count" />
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>
