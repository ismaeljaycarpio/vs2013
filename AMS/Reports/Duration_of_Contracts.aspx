<%@ Page Title="Duration Of Contracts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Duration_of_Contracts.aspx.cs" Inherits="AMS.Reports.Duration_of_Contracts" %>

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
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-10">

                                <div class="input-group input-group-btn">

                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search..."></asp:TextBox>
                                    <asp:Button ID="btnSearch"
                                        runat="server"
                                        CssClass="btn btn-primary"
                                        Text="Go"
                                        OnClick="btnSearch_Click" />
                                </div>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-10">
                                <div class="input-daterange">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtStartDate" runat="server" data-provide="datepicker" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-addon">to</span>
                                        <asp:TextBox ID="txtEndDate" runat="server" data-provide="datepicker" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="table-responsive">
                        <asp:Button runat="server" Text="Word" ID="btnExportToPDF" OnClick="btnExportToPDF_Click" />
                        <asp:Button runat="server" ID="btnExcel" OnClick="btnExcel_Click" Text="Excel" />
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
                            OnPageIndexChanging="gvEmployee_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Emp_Id" HeaderText="ID" />
                                <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                                <asp:BoundField DataField="Department" HeaderText="Department" />
                                <asp:BoundField DataField="Position" HeaderText="Position" />
                                <asp:BoundField DataField="Contract_SD" HeaderText="Starting Date" />
                                <asp:BoundField DataField="Contract_ED" HeaderText="Ending Date" />
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
