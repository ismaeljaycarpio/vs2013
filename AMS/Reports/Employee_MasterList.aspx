<%@ Page Title="Reports - Employee Master List"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Employee_MasterList.aspx.cs"
    Inherits="AMS.Reports.Employee_MasterList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h5>Employee Master List</h5>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-10">

                                <div class="input-group">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSearch"
                                            runat="server"
                                            CssClass="btn btn-primary"
                                            Text="Go"
                                            OnClick="btnSearch_Click" />

                                    </span>
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                </div>

                            </div>
                        </div>
                    </div>

                    <asp:Button runat="server" Text="Word" ID="btnExportToPDF" OnClick="btnExportToPDF_Click" />
                    <asp:Button runat="server" ID="btnExcel" OnClick="btnExcel_Click" Text="Excel" />
                    <div class="table-responsive">
                        <asp:GridView ID="gvEmployee"
                            runat="server"
                            class="table table-striped table-hover dataTable"
                            GridLines="None"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            AllowSorting="true"
                            ShowHeaderWhenEmpty="true"
                            DataKeyNames="UserId"
                            EmptyDataText="No Record(s) found"
                            OnSorting="gvEmployee_Sorting"
                            OnSelectedIndexChanging="gvEmployee_SelectedIndexChanging"
                            OnPageIndexChanging="gvEmployee_PageIndexChanging"
                            PageSize="7">
                            <Columns>
                                <asp:BoundField DataField="Emp_Id" HeaderText="ID" SortExpression="Emp_Id" />
                                <asp:TemplateField HeaderText="Full Name" SortExpression="FullName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFNAME" runat="server" Text='<%# Eval("FullName") %>' CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                                <asp:BoundField DataField="AccountStatus" HeaderText="Contract Status" SortExpression="AccountStatus" />
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h5><span class="glyphicon glyphicon-alert"></span>&nbsp;Expiring Contracts <b><%= DateTime.Now.ToShortDateString() %> - <%= DateTime.Now.AddDays(14).ToShortDateString() %></b></h5>
                </div>
                <div class="panel-body">
                    <asp:Button runat="server" Text="Word" ID="btnExpiringContract_Word" OnClick="btnExpiraingContract_Word_Click" />
                    <asp:Button runat="server" Text="Excel" ID="btnExpiringContract_Excel" OnClick="btnExpiringContract_Excel_Click" />
                    <div class="table-responsive">
                        <asp:GridView ID="gvExpiringContract"
                            runat="server"
                            class="table table-striped table-hover dataTable"
                            GridLines="None"
                            AutoGenerateColumns="false"
                            ShowHeaderWhenEmpty="true"
                            DataKeyNames="UserId"
                            EmptyDataText="No Record(s) found"
                            OnSelectedIndexChanging="gvExpiringContract_SelectedIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Emp_Id" HeaderText="ID" SortExpression="Emp_Id" />
                                <asp:TemplateField HeaderText="Full Name" SortExpression="FullName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFNAME" runat="server" Text='<%# Eval("FullName") %>' CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                                <asp:BoundField DataField="Contract_SD" HeaderText="Contract Start Date" SortExpression="Contract_SD" />
                                <asp:BoundField DataField="Contract_ED" HeaderText="Contract End Date" SortExpression="Contract_ED" />
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
