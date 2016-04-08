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
                                    OnRowCommand="gvSpecUsers_RowCommand"
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

                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Change Password" CommandName="changePassword" />
                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Edit Role" CommandName="editRole" />
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

    <!-- Change Password Modal -->
    <div id="changePasswordModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upChangePassword" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Change Password</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="txtNewPassword">New Password</label>
                                    <asp:TextBox ID="txtNewPassword" 
                                        runat="server"
                                        MaxLength="20"
                                        TextMode="Password"
                                        CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                        runat="server"
                                        ForeColor="Red"
                                        Display="Dynamic"
                                        ValidationGroup="vgChangePassword"
                                        ControlToValidate="txtNewPassword"
                                        ErrorMessage="">*</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfUserId" runat="server" />
                        <div class="modal-footer">
                            <asp:Button ID="btnChangePassword" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vgChangePassword" OnClick="btnChangePassword_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvSpecUsers" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnChangePassword" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Edit Role Modal -->
    <div id="editRoleModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Role</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="txtNewPassword">New Password</label>
                                    <asp:DropDownList ID="ddlRole" 
                                        runat="server"
                                        CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hfRoleId" runat="server" />
                        <div class="modal-footer">
                            <asp:Button ID="btnEditRole" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vgEditRole" OnClick="btnEditRole_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvSpecUsers" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnEditRole" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Delete Modal -->
    <div id="deleteModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Delete Record</h4>
                        </div>
                        <div class="modal-body">
                            <p class="text-danger">
                                Are you sure you want to delete this record ? 
                            All information associated with this user will also be removed.
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                        <asp:HiddenField ID="hfDeleteId" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvSpecUsers" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
