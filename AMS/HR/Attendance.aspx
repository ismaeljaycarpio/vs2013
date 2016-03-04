﻿<%@ Page Title="Timekeeping Logs"
    Language="C#"
    MasterPageFile="~/HRNested.master"
    AutoEventWireup="true"
    CodeBehind="Attendance.aspx.cs"
    Inherits="AMS.HR.Attendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4><strong>Timekeeping Logs</strong></h4>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <label for="ddlStatus" class="col-sm-2 control-label">Name:</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlName" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtStartDate" class="col-sm-2 control-label">Dates</label>
                            <div class="col-sm-10">
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
                            <div class="col-sm-10">
                                <asp:Button ID="btnSearch"
                                    runat="server"
                                    CssClass="btn btn-primary form-control"
                                    Text="Go"
                                    OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>

                    <asp:Button runat="server" Text="Word" ID="btnExportToPDF" OnClick="btnExportToPDF_Click" />
                    <asp:Button runat="server" ID="btnExcel" OnClick="btnExcel_Click" Text="Excel" />
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

                                <asp:TemplateField HeaderText="Hours Rendered" SortExpression="HoursRendered">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHoursRendered" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                                
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
