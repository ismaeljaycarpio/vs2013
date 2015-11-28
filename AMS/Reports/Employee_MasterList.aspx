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
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Employee Master List</h5>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-10">

                                <div class="input-group">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="All">All Status</asp:ListItem>
                                        <asp:ListItem Value="Active">Active</asp:ListItem>
                                        <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
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
                            DataKeyNames="UserId"
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

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus"
                                            runat="server"
                                            Text='<%# (Boolean.Parse(Eval("IsActive").ToString())) ? "Active" : "Inactive" %>'>
                                        </asp:Label>
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

</asp:Content>
