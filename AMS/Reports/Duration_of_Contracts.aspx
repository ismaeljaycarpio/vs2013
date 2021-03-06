﻿<%@ Page Title="Duration Of Contracts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Duration_of_Contracts.aspx.cs" Inherits="AMS.Reports.Duration_of_Contracts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h5>Duration Of Contracts</h5>
                </div>
                <div class="panel-body">
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div class="input-daterange">
                                <div class="input-group">
                                    <asp:TextBox ID="txtStartDate" runat="server" data-provide="datepicker" CssClass="form-control" placeholder="Start Date"></asp:TextBox>
                                    <span class="input-group-addon">to</span>
                                    <asp:TextBox ID="txtEndDate" runat="server" data-provide="datepicker" CssClass="form-control" placeholder="End Date"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:Button ID="btnSearch"
                            runat="server"
                            CssClass="btn btn-primary"
                            Text="Go"
                            OnClick="btnSearch_Click" />

                        <div class="pull-right">
                            <asp:Button runat="server" Text="Word" ID="btnExportToPDF" OnClick="btnExportToPDF_Click" CssClass="btn btn-default btn-sm" />
                        <asp:Button runat="server" ID="btnExcel" OnClick="btnExcel_Click" Text="Excel" CssClass="btn btn-default btn-sm"/>
                        </div>
                    </div>
                </div>

                <div class="panel-body">
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
                            OnPageIndexChanging="gvEmployee_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Emp_Id" HeaderText="ID" SortExpression="Emp_Id" />
                                <asp:BoundField DataField="FullName" HeaderText="Full Name" SortExpression="FullName" />
                                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                                <asp:BoundField DataField="Position" HeaderText="Position" SortExpression="Position" />
                                <asp:TemplateField HeaderText="Starting Date" SortExpression="Contract_SD">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblStartingDate" Text='<%# Eval("Contract_SD", "{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="End Date" SortExpression="Contract_ED">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblEndDate" Text='<%# Eval("Contract_ED", "{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Duration" SortExpression="Years">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYears" runat="server" Text='<%# Eval("Years") + " Years " %>'></asp:Label>
                                        <asp:Label ID="lblMonths" runat="server" Text='<%# Eval("Months") + " Months" %>'></asp:Label>
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
