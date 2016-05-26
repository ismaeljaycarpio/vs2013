<%@ Page Title="Colleague Evaluation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="evaluation-colleague.aspx.cs" Inherits="AMS.eval_colleague.evaluation_colleague" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>List of Colleague You Can Evaluate</h5>
                </div>
                <div class="panel-body">
                    <div class="form-inline">
                        <div class="form-group">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:Button ID="btnSearch"
                                        runat="server"
                                        CssClass="btn btn-primary"
                                        Text="Go"
                                        OnClick="btnSearch_Click" />
                                </span>
                            </div>
                        </div>
                    </div>

                    <br />

                    <div class="table-responsive">
                        <asp:GridView ID="gvEmployee"
                            runat="server"
                            class="table table-striped table-hover dataTable"
                            GridLines="None"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            ShowHeaderWhenEmpty="true"
                            ShowFooter="true"
                            AllowSorting="true"
                            DataKeyNames="UserId"
                            EmptyDataText="No Employee(s) found"
                            OnSorting="gvEmployee_Sorting"
                            OnRowDataBound="gvEmployee_RowDataBound"
                            OnPageIndexChanging="gvEmployee_PageIndexChanging"
                            OnSelectedIndexChanging="gvEmployee_SelectedIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Emp_Id" HeaderText="ID" SortExpression="Emp_Id" />
                                <asp:TemplateField HeaderText="Full Name" SortExpression="FullName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFNAME" runat="server" Text='<%# Eval("FullName") %>' CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                                <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hfUserId" runat="server" />
    <asp:HyperLink ID="hlEvaluationColleague" runat="server" NavigateUrl="~/eval-colleague/evaluation-colleague-form.aspx"></asp:HyperLink>
</asp:Content>
