<%@ Page Title="Pending Evaluation"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="PendingEvaluation.aspx.cs"
    Inherits="AMS.Evaluation.PendingEvaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h5>Pending Evaluation</h5>
                </div>
                <div class="panel-body">
                    <p>
                        Employees that needs to be evaluated on or before
                        <label class="label-danger label"><%=DateTime.Now.ToShortDateString() %> - <%=DateTime.Now.AddDays(14).ToShortDateString() %></label>
                    </p>
                    <div class="table-responsive">

                        <asp:GridView ID="gvEmployee"
                            runat="server"
                            class="table table-striped table-hover dataTable"
                            GridLines="None"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            ShowHeaderWhenEmpty="true"
                            AllowSorting="true"
                            DataKeyNames="UserId"
                            EmptyDataText="No Record(s) found"
                            OnSorting="gvEmployee_Sorting"
                            OnSelectedIndexChanging="gvEmployee_SelectedIndexChanging"
                            OnPageIndexChanging="gvEmployee_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Emp_Id" HeaderText="ID" SortExpression="Emp_Id" />
                                <asp:TemplateField HeaderText="Full Name" SortExpression="FullName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFNAME" runat="server" Text='<%# Eval("FullName") %>' CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                                <asp:TemplateField HeaderText="Last Evaluation Date" SortExpression="LastEvaluationDate">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblLastEvaluationDate" Text='<%# Eval("LastEvaluationDate", "{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Next Evaluation Date" SortExpression="NextEvaluationDate">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNextEvaluationDate" Text='<%# Eval("NextEvaluationDate", "{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>

                    </div>
                    <hr />
                    <asp:Button runat="server" Text="Word" ID="btnExportToPDF" OnClick="btnExportToPDF_Click" />
                    <asp:Button runat="server" ID="btnExcel" OnClick="btnExcel_Click" Text="Excel" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
