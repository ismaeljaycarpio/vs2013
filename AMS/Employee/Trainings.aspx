<%@ Page Title="Training" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="Trainings.aspx.cs" Inherits="AMS.Employee.Trainings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <!-- Add Modal -->
    <div id="addModal" class="modal">
        <div class="modal-dialog" role="dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upAdd" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Training</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="txtAddDescription">Description</label>
                                    <asp:TextBox ID="txtAddDescription" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddDescription"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Description is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddVenue">Venue</label>
                                    <asp:TextBox ID="txtAddVenue" runat="server" CssClass="form-control" placeholder="Venue"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddVenue"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Venue is required"></asp:RequiredFieldValidator>
                                </div>


                                <div class="form-group">
                                    <label for="txtAddDate">Date</label>
                                    <asp:TextBox ID="txtAddDate" runat="server" CssClass="form-control" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Date is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddDetails">Details</label>
                                    <asp:TextBox ID="txtAddDetails" runat="server" CssClass="form-control" placeholder="Details" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddDetails"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Details is required"></asp:RequiredFieldValidator>
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
    <div id="updateModal" class="modal">
        <div class="modal-dialog" role="dialog">

            <!-- Update Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Training</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditDescription">Description</label>
                                    <asp:TextBox ID="txtEditDescription" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditDescription"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Description is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditVenue">Venue</label>
                                    <asp:TextBox ID="txtEditVenue" runat="server" CssClass="form-control" placeholder="Venue"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditVenue"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Venue is required"></asp:RequiredFieldValidator>
                                </div>


                                <div class="form-group">
                                    <label for="txtEditDate">Date</label>
                                    <asp:TextBox ID="txtEditDate" runat="server" CssClass="form-control" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Date is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditDetails">Details</label>
                                    <asp:TextBox ID="txtEditDetails" runat="server" CssClass="form-control" placeholder="Details" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditDetails"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Details is required"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vsEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvTrainings" EventName="RowCommand" />
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
                    <h5>Trainings</h5>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upTrainings" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvTrainings"
                                    runat="server"
                                    class="table table-striped table-hover"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    DataKeyNames="Id"
                                    OnRowDeleting="gvTrainings_RowDeleting"
                                    OnRowCommand="gvTrainings_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="Action" ButtonType="Button" Text="Edit" CommandName="editRecord" />

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Venue">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVenue" runat="server" Text='<%# Eval("Venue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDetails" runat="server" Text='<%# Eval("Details") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="true" />
                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <!-- Trigger the modal with a button -->
                                <asp:Button ID="btnOpenModal" runat="server" CssClass="btn btn-info btn-sm" Text="Add Training" OnClick="btnOpenModal_Click" />
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
