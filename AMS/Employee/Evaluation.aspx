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
                                <asp:BoundField DataField="EvaluatedBy" HeaderText="Evaluated By" />
                                <asp:BoundField DataField="ApprovedByManager" HeaderText="Manager Approval" />
                                <asp:BoundField DataField="ApprovedByHR" HeaderText="HR Approval" />         
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Button ID="btnPerfEval" runat="server" CssClass="btn btn-default"
                            CausesValidation="false" Text="Performance Evaluation" OnClick="btnPerfEval_Click" />
                        <%--<asp:Button ID="btnSelfEval" runat="server" CssClass="btn btn-default"
                            CausesValidation="false" Text="Self Evaluation" OnClick="btnSelfEval_Click" />--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfAgency" runat="server"  Visible="true"/>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
