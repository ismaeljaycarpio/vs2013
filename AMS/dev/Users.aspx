<%@ Page Title="Users Config" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="AMS.dev.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h5>Special Users</h5>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upSpecUsers" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvSpecUsers"
                                    runat="server"
                                    class="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="false"
                                    ShowFooter="true"
                                    DataKeyNames="UserId"
                                    OnRowDataBound="gvSpecUsers_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Username">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />

                                        <asp:TemplateField HeaderText="Account Status" SortExpression="IsApproved">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblStatus"
                                                    runat="server"
                                                    OnClick="lblStatus_Click"
                                                    Text='<%# (Boolean.Parse(Eval("IsApproved").ToString())) ? "Active" : "Inactive" %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Locked Out" SortExpression="IsLockedOut">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnLockedOut"
                                                    runat="server"
                                                    OnClick="lbtnLockedOut_Click"
                                                    Text='<%# (Boolean.Parse(Eval("IsLockedOut").ToString())) ? "Yes" : "No" %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reset Password">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblReset"
                                                    runat="server"
                                                    OnClick="lblReset_Click"
                                                    Text="Reset Password">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="FailedPasswordAttemptCount" HeaderText="Failed Password Count" SortExpression="FailedPasswordAttemptCount" />

                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Edit Role" CommandName="editRecord" />
                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Delete" CommandName="deleteRecord" />

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvSpecUsers" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
