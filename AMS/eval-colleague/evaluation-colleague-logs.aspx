<%@ Page Title="Colleague Evaluation Logs" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="evaluation-colleague-logs.aspx.cs" Inherits="AMS.eval_colleague.evaluation_colleague_logs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">My Colleague Evaluations</div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvColleagueEvaluation"
                            runat="server"
                            class="table table-striped table-hover dataTable"
                            GridLines="None"
                            DataSourceID="ColleagueEvaluationDataSource"
                            EmptyDataText="No Record/s found."
                            AutoGenerateColumns="false"
                            ShowHeaderWhenEmpty="true"
                            OnRowCommand="gvColleagueEvaluation_RowCommand"
                            OnSelectedIndexChanging="gvColleagueEvaluation_SelectedIndexChanging"
                            DataKeyNames="Id">
                            <Columns>
                                <asp:TemplateField HeaderText="Row Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date Evaluated" SortExpression="DateEvaluated">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnDateEvaluated" runat="server" Text='<%# Eval("DateEvaluated") %>' CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Evaluated By" SortExpression="EvaluatedBy">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEvaluatedBy" runat="server" Text='<%# Eval("EvaluatedBy") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:LinqDataSource ID="ColleagueEvaluationDataSource" runat="server" OnSelecting="ColleagueEvaluationDataSource_Selecting"></asp:LinqDataSource>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
