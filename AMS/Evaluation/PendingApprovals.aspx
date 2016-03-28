<%@ Page Title="Pending Approvals"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="PendingApprovals.aspx.cs"
    Inherits="AMS.Evaluation.PendingApprovals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-warning">
                <div class="panel-heading">
                    <h5>Pending Evaluation Approvals</h5>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upPendingApprovals" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvPendingApprovals"
                                    runat="server"
                                    class="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    EmptyDataText="No Pending Evaluation Approvals"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    ShowHeaderWhenEmpty="true"
                                    AllowSorting="true"
                                    OnRowCommand="gvPendingApprovals_RowCommand"
                                    OnPageIndexChanging="gvPendingApprovals_PageIndexChanging"
                                    OnSelectedIndexChanging="gvPendingApprovals_SelectedIndexChanging"
                                    OnSorting="gvPendingApprovals_Sorting"
                                    DataKeyNames="Id,UserId">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Approve" CommandName="approveRecord" />

                                        <asp:TemplateField HeaderText="Name" SortExpression="FullName">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>' CommandName="Select"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DateEvaluated" HeaderText="DateEvaluated" SortExpression="DateEvaluated" />
                                        <asp:TemplateField HeaderText="Manager Approval" SortExpression="ApprovedByManagerId">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApprovalManager" runat="server" Text='<%# Guid.Parse(Eval("ApprovedByManagerId").ToString()) == Guid.Empty ? "Pending" : "Approved" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HR Approval" SortExpression="ApprovedByHRId">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHRApproval" runat="server" Text='<%# Guid.Parse(Eval("ApprovedByHRId").ToString()) == Guid.Empty ? "Pending" : "Approved" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Approve Modal -->
    <div id="approveModal" class="modal fade" tabindex="-1" aria-labelledby="approveModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Approve Leave</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to approve this leave ?
                            <asp:HiddenField ID="hfApproveId" runat="server" />
                            <asp:HiddenField ID="hfUserId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-success" Text="Approve" OnClick="btnApprove_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
