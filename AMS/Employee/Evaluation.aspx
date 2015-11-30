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
                            OnSelectedIndexChanged="gvEvaluation_SelectedIndexChanged"
                            OnSelectedIndexChanging="gvEvaluation_SelectedIndexChanging">
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
        </div>
    </div>
    <asp:HiddenField ID="hfAgency" runat="server" Visible="true" />
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
