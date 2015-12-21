<%@ Page Title="Evaluation"
    Language="C#"
    MasterPageFile="~/ProfileNested.master"
    AutoEventWireup="true"
    CodeBehind="Evaluation.aspx.cs"
    Inherits="AMS.Employee.Evaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        <asp:GridView ID="gvEvaluation"
                            runat="server"
                            class="table table-striped table-hover dataTable"
                            GridLines="None"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            DataKeyNames="Id"
                            ShowHeaderWhenEmpty="true"
                            EmptyDataText="No record(s) found"
                            OnPageIndexChanging="gvEvaluation_PageIndexChanging"
                            OnSelectedIndexChanged="gvEvaluation_SelectedIndexChanged">
                            <Columns>
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
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Button ID="btnPerfEval"
                            runat="server"
                            CssClass="btn btn-primary"
                            CausesValidation="false"
                            Text="Performance Evaluation"
                            OnClick="btnPerfEval_Click" />
                    </div>
                </div>
            </div>

            <div class="panel panel-info">
                <div class="panel-heading">Self Evaluation</div>
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
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfAgency" runat="server" Visible="true" />
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
