<%@ Page Title="Config Evaluation Period"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Config_Evaluation_Periond.aspx.cs"
    Inherits="AMS.Evaluation.Config_Evaluation_Periond" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Add Modal -->
    <div id="addModal" class="modal">
        <div class="modal-dialog" role="dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upAdd" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Add Evaluation Period</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="txtAddEvaluationPeriod">Evaluation Period:</label>
                                    <asp:TextBox ID="txtAddEvaluationPeriod" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtAddEvaluationPeriod"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Evaluation Period is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtStartDate">Start Date</label>
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtStartDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Start Date is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEndDate">End Date</label>
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEndDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="End Date is required"></asp:RequiredFieldValidator>
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
                            <h4 class="modal-title">Update Evaluation Period</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditEvaluationPeriod">Evaluation Period:</label>
                                    <asp:TextBox ID="txtEditEvaluationPeriod" runat="server" CssClass="form-control" placeholder="Description"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditEvaluationPeriod"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Evaluation Period is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditStartDate">Start Date</label>
                                    <asp:TextBox ID="txtEditStartDate" runat="server" CssClass="form-control" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditStartDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="Start Date is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditEndDate">End Date</label>
                                    <asp:TextBox ID="txtEditEndDate" runat="server" CssClass="form-control" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditEndDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vsEdit"
                                        ErrorMessage="End Date is required"></asp:RequiredFieldValidator>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vsEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvEvaluationPeriod" EventName="RowCommand" />
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
                    <h5>Evaluation Period Configuration</h5>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upTrainings" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvEvaluationPeriod"
                                    runat="server"
                                    class="table table-striped table-hover"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="Id"
                                    EmptyDataText="No Record(s) found"
                                    OnRowDeleting="gvEvaluationPeriod_RowDeleting"
                                    OnRowCommand="gvEvaluationPeriod_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="Action" ButtonType="Button" Text="Upate" CommandName="editRecord" />

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEvaluationPeriod" runat="server" Text='<%# Eval("EvaluationPeriod") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Start Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartEvaluation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="End Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndEvaluation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <!-- Trigger the modal with a button -->
                                <asp:Button ID="btnOpenModal" runat="server" CssClass="btn btn-info btn-sm" Text="Add Evaluation Period" OnClick="btnOpenModal_Click" />
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
