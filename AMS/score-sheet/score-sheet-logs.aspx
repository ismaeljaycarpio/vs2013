<%@ Page Title="Score Sheet Logs" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="score-sheet-logs.aspx.cs" Inherits="AMS.score_sheet.score_sheet_logs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">My SCORE Sheet</div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upScoresheet" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvScoresheet"
                                    runat="server"
                                    class="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    DataSourceID="ScoresheetDataSource"
                                    EmptyDataText="No Record/s found."
                                    AutoGenerateColumns="false"
                                    ShowHeaderWhenEmpty="true"
                                    OnRowCommand="gvScoresheet_RowCommand"
                                    OnSelectedIndexChanging="gvScoresheet_SelectedIndexChanging"
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

                                        <asp:TemplateField HeaderText="Name" SortExpression="FullName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Delete" CommandName="deleteRecord" />

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:HyperLink ID="hlScoresheet" runat="server" NavigateUrl="~/score-sheet/score-sheet-form.aspx">Create SCORE Sheet</asp:HyperLink>
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

    <asp:LinqDataSource ID="ScoresheetDataSource" runat="server" OnSelecting="ScoresheetDataSource_Selecting"></asp:LinqDataSource>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
