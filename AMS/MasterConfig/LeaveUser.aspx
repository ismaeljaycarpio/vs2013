<%@ Page Title="User Leaves" Language="C#" MasterPageFile="~/MasterConfigNested.master" AutoEventWireup="true" CodeBehind="LeaveUser.aspx.cs" Inherits="AMS.MasterConfig.LeaveUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="updateModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Update Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Update Leaves</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <label for="gvLeaves">Leaves available</label>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvLeaves"
                                            runat="server"
                                            class="table table-striped table-hover dataTable"
                                            GridLines="None"
                                            AutoGenerateColumns="false"
                                            DataKeyNames="Id">
                                            <Columns>
                                                <asp:TemplateField HeaderText="LeaveTypeId" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeaveTypeId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Leave Name" SortExpression="LeaveTypeName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeaveTypeName" runat="server" Text='<%# Eval("LeaveName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Default Days" SortExpression="DefaultDays">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDefaultDays" runat="server" Text='<%# Eval("DefaultDays") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assign Remaining Days" SortExpression="NoOfDays">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemainingDays" runat="server" CssClass="form-control" Text='<%# Eval("NoOfDays") %>' TextMode="Number"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                                            runat="server"
                                                            Display="Dynamic"
                                                            ControlToValidate="txtRemainingDays"
                                                            CssClass="label label-danger"
                                                            ValidationGroup="vgEdit"
                                                            ErrorMessage="*">*</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                            runat="server"
                                                            Display="Dynamic"
                                                            ControlToValidate="txtRemainingDays"
                                                            ForeColor="Red"
                                                            ValidationGroup="vgEdit"
                                                            ValidationExpression="^[0-9]\d*$"
                                                            ErrorMessage="*">*</asp:RegularExpressionValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vgEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvEmployee" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
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
                            <h4 class="modal-title">Delete Leaves</h4>
                        </div>
                        <div class="modal-body">
                            <p class="text-danger">
                                Are you sure you want to delete this record ? 
                            All information associated with this user will also be removed.
                            </p>
                            <asp:HiddenField ID="hfDeleteId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h5>Configure Leaves for Users</h5>
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
                        <asp:UpdatePanel ID="upEmployee" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvEmployee"
                                    runat="server"
                                    class="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    ShowFooter="true"
                                    AllowSorting="true"
                                    DataKeyNames="UserId"
                                    OnSorting="gvEmployee_Sorting"
                                    OnRowDataBound="gvEmployee_RowDataBound"
                                    OnRowCommand="gvEmployee_RowCommand"
                                    OnPageIndexChanging="gvEmployee_PageIndexChanging"
                                    OnSelectedIndexChanging="gvEmployee_SelectedIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" SortExpression="Emp_Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmp_Id" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                        <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                        <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                                        <asp:BoundField DataField="Agency" HeaderText="Agency" SortExpression="Agency" />
                                        <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />

                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Edit Leaves" CommandName="editRecord" />
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvEmployee" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
