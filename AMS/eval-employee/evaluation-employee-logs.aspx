<%@ Page Title="Employee Evaluation Logs" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="evaluation-employee-logs.aspx.cs" Inherits="AMS.eval_employee.evaluation_employee_logs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">My Employee Evaluations</div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upEmployeeEvaluation" runat="server">
                            <ContentTemplate>
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

                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Delete" CommandName="deleteRecord" />

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

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

    <asp:LinqDataSource ID="EmployeeEvaluationDataSource" runat="server" OnSelecting="EmployeeEvaluationDataSource_Selecting"></asp:LinqDataSource>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
