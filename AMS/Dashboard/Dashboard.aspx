﻿<%@ Page Title="My Dashboard"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="AMS.Dashboard.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-4">
            <h4><a href="~/Reports/BirthDay_Celeb.aspx" runat="server"><span class="glyphicon glyphicon-cutlery"></span><b><%= DateTime.Now.ToString("MMMMM") %></b> Birthday Celebrants</a></h4>
            <p>
                Number of Employees with Birthday this month:
                <asp:LinkButton ID="lnkBdayCount" runat="server" OnClick="lnkBdayCount_Click"></asp:LinkButton>
            </p>
        </div>

        <div class="col-lg-4">
            <h4><a href="~/Reports/Employee_MasterList.aspx" runat="server">Employee Master List</a></h4>
            <div class="table table-responsive">
                <asp:GridView ID="gvEmployeeMasterList"
                    runat="server"
                    class="table table-striped table-hover dataTable"
                    GridLines="None"
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    AllowSorting="true"
                    DataKeyNames="Id">
                    <Columns>
                        <asp:BoundField DataField="AccountStatus" HeaderText="Status"  />
                        <asp:BoundField DataField="MasterListCount" HeaderText="Count"  />
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
            </div>
        </div>

        <div class="col-lg-4">
            <h4><a href="~/Reports/NewlyHired.aspx" runat="server"><b>Newly Hired - <%= DateTime.Now.ToString("MMMMM") %></b></a></h4>
            <p>
                Number of newly Hired Employees for these Month :
                <asp:Label ID="lblCountNewlyHired" runat="server"></asp:Label> 
            </p>
        </div>
    </div>
</asp:Content>
