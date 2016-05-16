<%@ Page Title="Timekeeping Logs"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Attendance.aspx.cs"
    Inherits="AMS.HR.Attendance" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h5>Timekeeping Logs</h5>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <label for="ddlStatus" class="col-sm-2 control-label">Name:</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlName" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtStartDate" class="col-sm-2 control-label">Date:</label>
                            <div class="col-lg-4">
                                <div class="input-daterange">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtStartDate" runat="server" data-provide="datepicker" CssClass="form-control" placeholder="Start Date"></asp:TextBox>
                                        <span class="input-group-addon">to</span>
                                        <asp:TextBox ID="txtEndDate" runat="server" data-provide="datepicker" CssClass="form-control" placeholder="End Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="btnSearch" class="col-sm-2 control-label">&nbsp;</label>
                            <div class="col-sm-4">
                                <asp:Button ID="btnSearch"
                                    runat="server"
                                    CssClass="btn btn-primary form-control"
                                    Text="Go"
                                    OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>

                    <asp:Button runat="server" Text="Export to Word" ID="btnExportToPDF" OnClick="btnExportToPDF_Click" />
                    <asp:Button runat="server" ID="btnExcel" OnClick="btnExcel_Click" Text="Export to Excel" />
                    <div class="table-responsive">
                        <div class="text-center">
                            <asp:Label ID="lblCount" runat="server"></asp:Label>
                        </div>
                        <asp:GridView ID="gvEmployee"
                            runat="server"
                            class="table table-striped table-hover dataTable"
                            GridLines="None"
                            ShowHeader="true"
                            ShowHeaderWhenEmpty="true"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            AllowSorting="true"
                            DataKeyNames="UserId"
                            ShowFooter="true"
                            PageSize="20"
                            EmptyDataText="No Record(s) found"
                            OnSorting="gvEmployee_Sorting"
                            OnPageIndexChanging="gvEmployee_PageIndexChanging"
                            OnRowDataBound="gvEmployee_RowDataBound"
                            OnSelectedIndexChanging="gvEmployee_SelectedIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Emp_Id" HeaderText="ID" SortExpression="Emp_Id" />
                                <asp:TemplateField HeaderText="Full Name" SortExpression="FullName">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFNAME" runat="server" Text='<%# Eval("FullName") %>' CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="IN" SortExpression="TimeIn">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTimeIn" runat="server" Text='<%# Eval("TimeIn") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="OUT" SortExpression="TimeOut">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTimeOut" runat="server" Text='<%# Eval("TimeOut") %>'>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Hours Rendered">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHoursRendered" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />

                                <asp:TemplateField HeaderText="Schedule" SortExpression="TimeStart">
                                    <ItemTemplate>
                                        <asp:Label ID="lblOpenB" runat="server" Text=" [ "></asp:Label>
                                        <asp:Label ID="lblSchedStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        <asp:Label ID="lblCloseB" runat="server" Text=" ] "></asp:Label>
                                        <asp:Label ID="Label1" runat="server" Text="<BR>"></asp:Label>
                                        <asp:Label ID="lblTimeStart" runat="server" Text='<%# Eval("TimeStart") %>'></asp:Label>
                                        <asp:Label ID="lblTo" runat="server" Text=" - "></asp:Label>
                                        <asp:Label ID="lblTimeEnd" runat="server" Text='<%# Eval("TimeEnd") %>'></asp:Label>
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
