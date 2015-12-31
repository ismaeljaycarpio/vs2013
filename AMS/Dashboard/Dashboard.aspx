<%@ Page Title="My Dashboard"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs"
    Inherits="AMS.Dashboard.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <asp:Panel ID="pnlNotification" runat="server" CssClass="alert alert-info" Visible="false">
                <strong>
                    <asp:Label ID="lblDepartment" runat="server"></asp:Label></strong>
                <p>
                    Displaying Dashboard for Department specific only.
                </p>
            </asp:Panel>
        </div>

    </div>
    <div class="row">

        <div class="col-lg-4">
            <h4><a href="~/Reports/BirthDay_Celeb.aspx" runat="server"><span class="glyphicon glyphicon-cutlery"></span><b><%= DateTime.Now.ToString("MMMMM") %></b> Birthday Celebrants</a></h4>
            <p>
                Number of Employees with Birthday this month:
                <asp:LinkButton ID="lnkBdayCount" runat="server" OnClick="lnkBdayCount_Click" CssClass="label label-info"></asp:LinkButton>
            </p>
        </div>

        <div class="col-lg-4">
            <h4><b><a href="~/Reports/Employee_MasterList.aspx" runat="server" id="aEmp">Employee Master List</a></b></h4>
            <div class="table table-responsive">
                <asp:GridView ID="gvEmployeeMasterList"
                    runat="server"
                    GridLines="Horizontal"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false"
                    DataKeyNames="Id">
                    <Columns>
                        <asp:BoundField DataField="AccountStatus" HeaderText="Status" />
                        <asp:BoundField DataField="MasterListCount" HeaderText="Count" />
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
                <p>
                    <b>Expiring Contract in 2 weeks (<%=DateTime.Now.ToString("MM/dd/yyyy") %>) - (<%=DateTime.Now.AddDays(14).ToString("MM/dd/yyyy") %>):</b>
                    <asp:LinkButton ID="lnkCountExpiringContracts"
                        runat="server"
                        ForeColor="Red"
                        PostBackUrl="~/Reports/Employee_MasterList.aspx?Exp=5"></asp:LinkButton>
                </p>
            </div>
        </div>

        <%--<div class="col-lg-4">
            <h4><a href="~/Reports/NewlyHired.aspx?mm=n" runat="server"><b>Newly Hired - <%= DateTime.Now.ToString("MMMMM") %></b></a></h4>
            <p>
                Number of newly Hired Employees for these Month :
                <asp:Label ID="lblCountNewlyHired" runat="server" CssClass="label label-info"></asp:Label>
            </p>
        </div>--%>

        <asp:Panel ID="pnlPendingEvaluationNotice" runat="server">
            <div class="col-lg-4">
                <h4><a href="~/Evaluation/PendingEvaluation.aspx" runat="server"><b>Pending Evaluation </b>(<%=DateTime.Now.ToString("MM/dd/yyyy") %>) - (<%=DateTime.Now.AddDays(14).ToString("MM/dd/yyyy") %>)</a></h4>
                <p>
                    Number of Employees that are to be evaluated:
                <asp:Label ID="lblCountPendingEvaluation" runat="server" CssClass="label label-info"></asp:Label>
                </p>
            </div>
        </asp:Panel>


    </div>

    <%--<div class="row">
        <div class="col-lg-4">
            <h4><a href="~/Reports/BirthDay_Celeb.aspx" runat="server"><span class="glyphicon glyphicon-cutlery"></span><b><%= DateTime.Now.ToString("MMMMM") %></b> Birthday Celebrants</a></h4>
            <p>
                Number of Employees with Birthday this month:
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkBdayCount_Click" CssClass="label label-info"></asp:LinkButton>
            </p>
        </div>

        <div class="col-lg-4">
            <h4><a href="~/Reports/Employee_MasterList.aspx" runat="server">Employee Master List</a></h4>
            <div class="table table-responsive">
                <asp:GridView ID="GridView1"
                    runat="server"
                    GridLines="None"
                    AutoGenerateColumns="false"
                    DataKeyNames="Id">
                    <Columns>
                        <asp:BoundField DataField="AccountStatus" HeaderText="Status" />
                        <asp:BoundField DataField="MasterListCount" HeaderText="Count" />
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                </asp:GridView>
                <p>Expiring Contract in 2 weeks:
                    <asp:Label ID="Label1" runat="server"></asp:Label></p>
            </div>
        </div>

        <div class="col-lg-4">
            <h4><a href="~/Reports/NewlyHired.aspx" runat="server"><b>Newly Hired - <%= DateTime.Now.ToString("MMMMM") %></b></a></h4>
            <p>
                Number of newly Hired Employees for these Month :
                <asp:Label ID="Label2" runat="server" CssClass="label label-info"></asp:Label>
            </p>
        </div>
    </div>--%>
</asp:Content>
