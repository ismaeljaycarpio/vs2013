<%@ Page Title="Job Experiences" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="JobExperience.aspx.cs" Inherits="AMS.Employee.JobExperience" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Add Modal -->
    <div id="addModal" class="modal" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upAdd" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Job Experience</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="txtAddCompany">Company</label>
                                    <asp:TextBox ID="txtAddCompany" runat="server" CssClass="form-control" placeholder="Company"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddCompany"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Company is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddJob">Job</label>
                                    <asp:TextBox ID="txtAddJob" runat="server" CssClass="form-control" placeholder="Job"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddJob"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Job is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="duration">Duration</label>

                                    <div class="form-inline">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtAddFrom" runat="server" CssClass="form-control" placeholder="From" data-provide="datepicker"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                                runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="txtAddFrom"
                                                CssClass="label label-danger"
                                                ValidationGroup="vgAdd"
                                                ErrorMessage="From Date is required"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtAddTo" runat="server" CssClass="form-control" placeholder="To" data-provide="datepicker"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                                runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="txtAddTo"
                                                CssClass="label label-danger"
                                                ValidationGroup="vgAdd"
                                                ErrorMessage="To Date is required"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddReason">Reason</label>
                                    <asp:TextBox ID="txtAddReason" runat="server" CssClass="form-control" placeholder="Reason" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddTo"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Reason is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="form-group">
                                <label for="txtAddDescription">Description</label>
                                <asp:TextBox ID="txtAddDescription" runat="server" CssClass="form-control" placeholder="Description" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtAddDescription"
                                    CssClass="label label-danger"
                                    ValidationGroup="vgAdd"
                                    ErrorMessage="Description is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vgAdd" OnClick="btnSave_Click" />
                            <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
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
    <div id="updateModal" class="modal" tabindex="-1" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="dialog">

            <!-- Update Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Job Experience</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditCompany">Company</label>
                                    <asp:TextBox ID="txtEditCompany" runat="server" CssClass="form-control" placeholder="Company"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditCompany"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Company is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditJob">Job</label>
                                    <asp:TextBox ID="txtEditJob" runat="server" CssClass="form-control" placeholder="Job"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditJob"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Job is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="duration">Duration</label>

                                    <div class="form-inline">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtEditFromDate" runat="server" CssClass="form-control" placeholder="From" data-provide="datepicker"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                                runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="txtEditFromDate"
                                                CssClass="label label-danger"
                                                ValidationGroup="vsEdit"
                                                ErrorMessage="From Date is required"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtEditToDate" runat="server" CssClass="form-control" placeholder="To" data-provide="datepicker"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                                runat="server"
                                                Display="Dynamic"
                                                ControlToValidate="txtEditToDate"
                                                CssClass="label label-danger"
                                                ValidationGroup="vsEdit"
                                                ErrorMessage="To Date is required"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditReason">Reason</label>
                                    <asp:TextBox ID="txtEditReason" runat="server" CssClass="form-control" placeholder="Reason" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditReason"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Reason is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditDescription">Description</label>
                                    <asp:TextBox ID="txtEditDescription" runat="server" CssClass="form-control" placeholder="Description" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditDescription"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Description is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vsEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvJobExp" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Job Experiences</h5>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upJobExp" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvJobExp"
                                    runat="server"
                                    class="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    DataKeyNames="Id"
                                    OnRowDeleting="gvJobExp_RowDeleting"
                                    OnRowCommand="gvJobExp_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="Action" ButtonType="Button" Text="Edit" CommandName="editRecord" />

                                        <asp:TemplateField HeaderText="Job">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJob" runat="server" Text='<%# Eval("Job") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="From">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFrom" runat="server" Text='<%# Eval("FromDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTo" runat="server" Text='<%# Eval("ToDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reason">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="true" />
                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <!-- Trigger the modal with a button -->
                                <asp:Button ID="btnOpenModal" runat="server" CssClass="btn btn-info btn-sm" Text="Add Experience" OnClick="btnOpenModal_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
