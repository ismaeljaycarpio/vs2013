<%@ Page Title="Employee Movement"
    Language="C#"
    MasterPageFile="~/ProfileNested.master"
    AutoEventWireup="true"
    CodeBehind="EmployeeMovement.aspx.cs"
    Inherits="AMS.Employee.EmployeeMovement" %>

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
                            <h4 class="modal-title">Add Employee Movement</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="ddlEMovement">Movement:</label>
                                    <asp:DropDownList ID="ddlEMovement" runat="server" CssClass="form-control" placeholder="Description"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="ddlEMovement"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="E-Movement is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtRemarks">Remarks</label>
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" placeholder="Remarks" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtRemarks"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Remarks is required"></asp:RequiredFieldValidator>
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
                            <h4 class="modal-title">Edit Training</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label for="ddlEditMovement">Movement:</label>
                                    <asp:DropDownList ID="ddlEditMovement" runat="server" CssClass="form-control" placeholder="Description"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="ddlEditMovement"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgEdit"
                                        ErrorMessage="E-Movement is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditRemarks">Remarks</label>
                                    <asp:TextBox ID="txtEditRemarks" runat="server" CssClass="form-control" placeholder="Remarks" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditRemarks"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgEdit"
                                        ErrorMessage="Remarks is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vgEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvEMovement" EventName="RowCommand" />
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
                    <h5>Employee Movement</h5>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upEMovement" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvEMovement"
                                    runat="server"
                                    class="table table-striped table-hover"
                                    GridLines="None"
                                    EmptyDataText="No Record(s) found"
                                    ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    DataKeyNames="Id"
                                    OnRowDeleting="gvEMovement_RowDeleting"
                                    OnRowCommand="gvEMovement_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="Action" ButtonType="Button" Text="Edit" CommandName="editRecord" />

                                        <asp:TemplateField HeaderText="Movement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEMovement" runat="server" Text='<%# Eval("EMovement") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="true" />
                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <!-- Trigger the modal with a button -->
                                <asp:Button ID="btnOpenModal"
                                    runat="server"
                                    CssClass="btn btn-info btn-sm"
                                    Text="Add Movement"
                                    OnClick="btnOpenModal_Click"
                                    CausesValidation="false" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvEMovement" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
