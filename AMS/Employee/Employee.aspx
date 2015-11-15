<%@ Page Title="Employee"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Employee.aspx.cs"
    Inherits="AMS.Employee.Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>List of Employee</h5>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-10">
                                <div class="input-group">
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
                            OnPageIndexChanging="gvEmployee_PageIndexChanging"
                            OnSelectedIndexChanging="gvEmployee_SelectedIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Emp_ID" HeaderText="ID" SortExpression="Emp_ID" />
                                <asp:TemplateField HeaderText="First Name" SortExpression="FName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFNAME" runat="server" Text='<%# Eval("FName") %>' CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MName" HeaderText="Middle Name" SortExpression="MName" />
                                <asp:BoundField DataField="LName" HeaderText="Last Name" SortExpression="LName" />
                                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                                <%--<asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <a class="btn btn-default dropdown-toggle btn-xs" data-toggle="dropdown" href="#"><i class="icon-cog"></i><span class="caret"></span></a>
                                            <ul role="menu" class="dropdown-menu pull-right">
                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#"><span class="glyphicon glyphicon-edit"></span>&nbsp;Edit User Info</a></li>
                                                <li class="divider"></li>
                                                <li role="presentation"><a role="menuitem" tabindex="-1" href="#"><span class="glyphicon glyphicon-trash"></span>&nbsp;Delete user</a> </li>
                                            </ul>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
