<%@ Page Title="Self Evaluation"
    Language="C#"
    MasterPageFile="~/ProfileNested.master"
    AutoEventWireup="true"
    CodeBehind="evaluation-self.aspx.cs"
    Inherits="AMS.eval_self.evaluation_self" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">My Self Evaluation</div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvSelfEvaluation"
                            runat="server"
                            class="table table-striped table-hover dataTable"
                            GridLines="None"
                            DataSource="SelfEvaluationDataSource"
                            EmptyDataText="No Record/s found."
                            AutoGenerateColumns="false"
                            ShowHeaderWhenEmpty="true"
                            OnRowCommand="gvSelfEvaluation_RowCommand"
                            DataKeyNames="Id,UserId">
                            <Columns>
                                <asp:TemplateField HeaderText="Row Id" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name" SortExpression="FullName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>' CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DateEvaluated" HeaderText="DateEvaluated" SortExpression="DateEvaluated" />
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                    <asp:HyperLink ID="hlSelfEvaluation" runat="server" NavigateUrl="~/eval-self/evaluation-self-form.aspx">Create Self Evaluation</asp:HyperLink>
                </div>
            </div>
        </div>
    </div>

    <asp:LinqDataSource ID="SelfEvaluationDataSource" runat="server" OnSelecting="SelfEvaluationDataSource_Selecting"></asp:LinqDataSource>
</asp:Content>
