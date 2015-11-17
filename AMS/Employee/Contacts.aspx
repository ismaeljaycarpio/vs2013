<%@ Page Title="Contacts" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="AMS.Employee.Contacts" %>
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
                            <h4 class="modal-title">Add Contact</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="txtAddAddress">Address</label>
                                    <asp:TextBox ID="txtAddAddress" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddAddress"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Address is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddHomeAddress">Home Address</label>
                                    <asp:TextBox ID="txtAddHomeAddress" runat="server" CssClass="form-control" placeholder="Home Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddHomeAddress"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Home Address is required"></asp:RequiredFieldValidator>
                                </div>


                                <div class="form-group">
                                    <label for="txtAddCity">City</label>
                                    <asp:TextBox ID="txtAddCity" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddCity"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="City is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddProvince">Province</label>
                                    <asp:TextBox ID="txtAddProvince" runat="server" CssClass="form-control" placeholder="Province"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddProvince"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Province is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddZipCode">Zipcode</label>
                                    <asp:TextBox ID="txtAddZipCode" runat="server" CssClass="form-control" placeholder="Zipcode"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddZipCode"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Details is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddCountry">Country</label>
                                    <asp:TextBox ID="txtAddCountry" runat="server" CssClass="form-control" placeholder="Country"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddCountry"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Country is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddPhoneNumber">Phone Number</label>
                                    <asp:TextBox ID="txtAddPhoneNumber" runat="server" CssClass="form-control" placeholder="Phone Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddPhoneNumber"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Phone Number is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddEmail">Email Address</label>
                                    <asp:TextBox ID="txtAddEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddEmail"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddGuardianName">Guardian Name</label>
                                    <asp:TextBox ID="txtAddGuardianName" runat="server" CssClass="form-control" placeholder="Guardian Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddGuardianName"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Guardian Name is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddRelationship">Relationship</label>
                                    <asp:TextBox ID="txtAddRelationship" runat="server" CssClass="form-control" placeholder="Relationship"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddRelationship"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Relationship is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddGuardianAddress">Guardian Address</label>
                                    <asp:TextBox ID="txtAddGuardianAddress" runat="server" CssClass="form-control" placeholder="Guardian Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddGuardianAddress"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Guardian Address is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtAddGuardianPhone">Guardian Phone</label>
                                    <asp:TextBox ID="txtAddGuardianPhone" runat="server" CssClass="form-control" placeholder="Guardian Phone"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddGuardianPhone"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Guardian Phone is required"></asp:RequiredFieldValidator>
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
                            <h4 class="modal-title">Edit Contact</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditAddress">Address</label>
                                    <asp:TextBox ID="txtEditAddress" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditAddress"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Address is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditHomeAddress">Home Address</label>
                                    <asp:TextBox ID="txtEditHomeAddress" runat="server" CssClass="form-control" placeholder="Home Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditHomeAddress"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Home Address is required"></asp:RequiredFieldValidator>
                                </div>


                                <div class="form-group">
                                    <label for="txtEditCity">City</label>
                                    <asp:TextBox ID="txtEditCity" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditCity"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="City is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditProvince">Province</label>
                                    <asp:TextBox ID="txtEditProvince" runat="server" CssClass="form-control" placeholder="Province"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditProvince"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Province is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditZipCode">Zipcode</label>
                                    <asp:TextBox ID="txtEditZipCode" runat="server" CssClass="form-control" placeholder="Zipcode"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditZipCode"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Details is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditCountry">Country</label>
                                    <asp:TextBox ID="txtEditCountry" runat="server" CssClass="form-control" placeholder="Country"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditCountry"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Country is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditPhoneNumber">Phone Number</label>
                                    <asp:TextBox ID="txtEditPhoneNumber" runat="server" CssClass="form-control" placeholder="Phone Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditPhoneNumber"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Phone Number is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditEmail">Email Address</label>
                                    <asp:TextBox ID="txtEditEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditEmail"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditGuardianName">Guardian Name</label>
                                    <asp:TextBox ID="txtEditGuardianName" runat="server" CssClass="form-control" placeholder="Guardian Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditGuardianName"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Guardian Name is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditRelationship">Relationship</label>
                                    <asp:TextBox ID="txtEditRelationship" runat="server" CssClass="form-control" placeholder="Relationship"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditRelationship"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Relationship is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditGuardianAddress">Guardian Address</label>
                                    <asp:TextBox ID="txtEditGuardianAddress" runat="server" CssClass="form-control" placeholder="Guardian Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditGuardianAddress"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Guardian Address is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditGuardianPhone">Guardian Phone</label>
                                    <asp:TextBox ID="txtEditGuardianPhone" runat="server" CssClass="form-control" placeholder="Guardian Phone"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditGuardianPhone"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Guardian Phone is required"></asp:RequiredFieldValidator>
                                </div>


                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vsEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvContacts" EventName="RowCommand" />
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
                    <h5>Contacts</h5>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upContacts" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvContacts"
                                    runat="server"
                                    class="table table-striped table-hover"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    DataKeyNames="Id"
                                    OnRowDeleting="gvContacts_RowDeleting"
                                    OnRowCommand="gvContacts_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="Action" ButtonType="Button" Text="Edit" CommandName="editRecord" />

                                        <asp:TemplateField HeaderText="Guardian">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGuardian" runat="server" Text='<%# Eval("G_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Relationship">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRelationship" runat="server" Text='<%# Eval("Relationship") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("G_Address") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Contact No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("G_Phone") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="true" />
                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <!-- Trigger the modal with a button -->
                                <asp:Button ID="btnOpenModal" runat="server" CssClass="btn btn-info btn-sm" Text="Add Contact" OnClick="btnOpenModal_Click" />
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
