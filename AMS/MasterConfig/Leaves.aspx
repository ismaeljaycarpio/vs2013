<%@ Page Title="Leave Types" Language="C#" MasterPageFile="~/MasterConfigNested.master" AutoEventWireup="true" CodeBehind="Leaves.aspx.cs" Inherits="AMS.MasterConfig.Leaves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Add Modal -->
    <div id="addModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upAdd" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Leave Type</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="txtAddLeave">Leave Type</label>
                                    <asp:TextBox ID="txtAddLeave" runat="server" CssClass="form-control" placeholder="Leave Type"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddLeave"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Leave Type is required"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <label for="txtAddDays">No of Days</label>
                                    <asp:TextBox ID="txtAddDays" runat="server" CssClass="form-control" placeholder="No of Days"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddDays"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="No of Days is required"></asp:RequiredFieldValidator>
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
                            <h4 class="modal-title">Edit Leave Type</h4>
                        </div>

                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                            </div>

                            <div class="form-group">
                                <label for="txtEditLeave">Leave Type</label>
                                <asp:TextBox ID="txtEditLeave" runat="server" CssClass="form-control" placeholder="Leave Type"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditLeave"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEdit"
                                    ErrorMessage="Leave Type is required"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="txtEditDays">No of Days</label>
                                <asp:TextBox ID="txtEditDays" runat="server" CssClass="form-control" placeholder="No of Days"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEditDays"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgEdit"
                                    ErrorMessage="No of Days is required"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vgEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvLeave" EventName="RowCommand" />
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
                            <h4 class="modal-title">Delete Record</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to delete this record ?
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
                    <div class="pull-right">
                        <button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#addModal">Add</button>
                    </div>
                    <h5>Leave Types</h5>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upLeaves" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvLeave"
                                    runat="server"
                                    CssClass="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    ShowHeaderWhenEmpty="true"
                                    DataKeyNames="Id"
                                    OnRowCommand="gvLeave_RowCommand"
                                    OnPageIndexChanging="gvLeave_PageIndexChanging"
                                    PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField HeaderText="" ButtonType="Button" Text="Edit" CommandName="editRecord" />
                                        <asp:BoundField HeaderText="Leave" DataField="LeaveName" />
                                        <asp:BoundField HeaderText="No of Days" DataField="NoOfDays" />
                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Delete" CommandName="deleteRecord" />

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
</asp:Content>
