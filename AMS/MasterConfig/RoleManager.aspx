<%@ Page Title="Role Manager"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="RoleManager.aspx.cs"
    Inherits="AMS.MasterConfig.RoleManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Add Modal -->
    <div id="addModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upAdd" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Position</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="ddlAddRole">Role</label>
                                    <asp:DropDownList ID="ddlAddRole" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label for="ddlAddDepartment">Department</label>
                                    <asp:DropDownList ID="ddlAddDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddPosition">Position</label>
                                    <asp:TextBox ID="txtAddPosition" runat="server" CssClass="form-control" placeholder="Position"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddPosition"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Position is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vgAdd" OnClick="btnSave_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Edit Modal -->
    <div id="updateModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">

            <!-- Update Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Position</h4>
                        </div>

                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label for="ddlEditRole">Role</label>
                                <asp:DropDownList ID="ddlEditRole" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="ddlEditDepartment">Department</label>
                                <asp:DropDownList ID="ddlEditDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="txtAddPosition">Position</label>
                                <asp:TextBox ID="txtEditPosition" runat="server" CssClass="form-control" placeholder="Position"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditPosition"
                                    CssClass="label label-danger"
                                    ValidationGroup="vsEdit"
                                    ErrorMessage="Position is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vsEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvRoles" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="pull-right">
                        <button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#addModal">Add</button>
                    </div>
                    <h5>Positions</h5>
                </div>

                <div class="panel-body">

                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-10">
                                <div class="input-group">
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSearch"
                                            runat="server"
                                            CssClass="btn btn-primary"
                                            Text="Go"                                            
                                            OnClick="btnSearch_Click" />
                                    </span>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upRoles" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvRoles"
                                    runat="server"
                                    cssclass="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    AllowSorting="true"
                                    ShowHeaderWhenEmpty="true"
                                    DataKeyNames="Id"
                                    OnPageIndexChanging="gvRoles_PageIndexChanging"
                                    OnRowCommand="gvRoles_RowCommand"
                                    OnSorting="gvRoles_Sorting"
                                    PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="Action" ButtonType="Button" Text="Edit" CommandName="editRecord" />

                                        <asp:TemplateField HeaderText="Positions" SortExpression="Position">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("Position") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Department" SortExpression="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Role" SortExpression="RoleName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRole" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers></Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </asp:Panel>
</asp:Content>
