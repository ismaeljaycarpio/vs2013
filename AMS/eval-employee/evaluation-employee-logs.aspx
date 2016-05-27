<%@ Page Title="Employee Evaluation Logs" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="evaluation-employee-logs.aspx.cs" Inherits="AMS.eval_employee.evaluation_employee_logs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">My Employee Evaluations</div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvEmployeeEvaluation"
                            runat="server"
                            class="table table-striped table-hover dataTable"
                            GridLines="None"
                            DataSourceID="EmployeeEvaluationDataSource"
                            EmptyDataText="No Record/s found."
                            AutoGenerateColumns="false"
                            ShowHeaderWhenEmpty="true"
                            OnRowCommand="gvEmployeeEvaluation_RowCommand"
                            OnSelectedIndexChanging="gvEmployeeEvaluation_SelectedIndexChanging"
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

    <asp:LinqDataSource ID="EmployeeEvaluationDataSource" runat="server" OnSelecting="EmployeeEvaluationDataSource_Selecting"></asp:LinqDataSource>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
