<%@ Page Title="Evaluation"
    Language="C#"
    MasterPageFile="~/ProfileNested.master"
    AutoEventWireup="true"
    CodeBehind="Evaluation.aspx.cs"
    Inherits="AMS.Employee.Evaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Evaluations</h5>
                </div>
                <div class="panel-body">
                    <p>
                        <b>Last Evaluation Date:</b>
                        <asp:Label ID="lblLastEvaluationDate" runat="server" CssClass="label label-info"></asp:Label>
                    </p>
                    <p>
                        <b>Next Evaluation Date:</b>
                        <asp:Label ID="lblNextEvaluationDate" runat="server" CssClass="label label-info"></asp:Label>
                    </p>
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upEval" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvEvaluation"
                                    runat="server"
                                    class="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    DataKeyNames="Id"
                                    ShowHeaderWhenEmpty="true"
                                    EmptyDataText="No record(s) found"
                                    OnRowCommand="gvEvaluation_RowCommand"
                                    OnPageIndexChanging="gvEvaluation_PageIndexChanging"
                                    OnSelectedIndexChanged="gvEvaluation_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Row Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEvaluationType" runat="server" Text='<%# Eval("EvaluationType") %>' CommandName="Select"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="DateEvaluated" HeaderText="Evaluation Date" />

                                        <asp:TemplateField HeaderText="Manager Approval">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApprovalManager" runat="server" Text='<%# Guid.Parse(Eval("ApprovedByManagerId").ToString()) == Guid.Empty ? "Pending" : "Approved" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="HR Approval">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHRApproval" runat="server" Text='<%# Guid.Parse(Eval("ApprovedByHRId").ToString()) == Guid.Empty ? "Pending" : "Approved" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Is Evaluated ?">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIsEvaluated" runat="server" Text='<%# Guid.Parse(Eval("EvaluatedById").ToString()) == Guid.Empty ? "Not Yet" : "Evaluated" %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Delete" CommandName="deleteRecord" />
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                                <asp:Button ID="btnPerfEval"
                                    runat="server"
                                    CssClass="btn btn-primary"
                                    CausesValidation="false"
                                    Text="Performance Evaluation"
                                    OnClick="btnPerfEval_Click" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvEvaluation" />
                                <asp:AsyncPostBackTrigger ControlID="gvEvaluation" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>

            <%--<div class="panel panel-info">
                <div class="panel-heading">Self Evaluation | <asp:LinkButton ID="lnkSummarize" runat="server" CssClass="label label-warning" Text="Go To Summarize Report" OnClick="lnkSummarize_Click"></asp:LinkButton></div>
                <div class="panel-body">
                    <asp:GridView ID="gvSelfEvaluation"
                        runat="server"
                        class="table table-striped table-hover dataTable"
                        GridLines="None"
                        AutoGenerateColumns="false"
                        AllowPaging="true"
                        DataKeyNames="Id"
                        PageSize="10"
                        ShowHeaderWhenEmpty="true"
                        ShowFooter="true"
                        EmptyDataText="No record(s) found"
                        OnPageIndexChanging="gvSelfEvaluation_PageIndexChanging"
                        OnRowDataBound="gvSelfEvaluation_RowDataBound"
                        OnSelectedIndexChanged="gvSelfEvaluation_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="Evaluation Date">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDateEvaluated" runat="server" Text='<%# Eval("DateEvaluated") %>' CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Evaluated By">
                                <ItemTemplate>
                                    <asp:Label ID="lblEvaluatedBy" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="pagination-ys" />
                    </asp:GridView>
                    <asp:Panel ID="pnlInfo" runat="server" CssClass="alert alert-info" Visible="false">
                        <strong class="label label-danger">Information!</strong> Evaluation to you by other <i>Employees</i> are hidden from you.
                    </asp:Panel>
                </div>
            </div>--%>
        </div>
    </div>
    <asp:HiddenField ID="hfAgency" runat="server" Visible="true" />
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
