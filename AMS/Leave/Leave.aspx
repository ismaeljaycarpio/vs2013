<%@ Page Title="Leave Approvals" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Leave.aspx.cs" Inherits="AMS.Leave.Leave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Approve Modal -->
    <div id="approveModal" class="modal fade" tabindex="-1" aria-labelledby="approveModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Approve Leave</h4>
                        </div>
                        <div class="modal-body">
                            Are you sure you want to approve this leave ?
                            <asp:HiddenField ID="hfApproveId" runat="server" />
                            <asp:HiddenField ID="hfUserId" runat="server" />
                            <asp:HiddenField ID="hfNoOfDays" runat="server" />
                            <asp:HiddenField ID="hfLeaveTypeUserId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-success" Text="Approve" OnClick="btnApprove_Click" />
                            <asp:Button ID="btnDisapprove" runat="server" CssClass="btn btn-danger" Text="Disapprove" OnClick="btnDisapprove_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <ul class="nav nav-tabs" id="myTab">
                <li><a href="#pendingTab" data-toggle="tab">Pending Leave Approvals 
                    <asp:UpdatePanel ID="upPendingCount" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblPendingCount" runat="server" CssClass="badge"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDisapprove" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </a></li>
                <li><a href="#approvedTab" data-toggle="tab">Approved Leaves
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblApprovedCount" runat="server" CssClass="badge"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDisapprove" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </a></li>
                <li><a href="#cancelledTab" data-toggle="tab">Dispproved Leaves
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblDisapprovedCount" runat="server" CssClass="badge"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnApprove" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDisapprove" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </a></li>
                <%--<li><a href="#rejectTab" data-toggle="tab">Rejected Leave Approvals</a></li>--%>
            </ul>

            <div id="myTabContent" class="tab-content">
                <div class="tab-pane" id="pendingTab">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h5 class="panel-title">Pending Leave Approvals</h5>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="upPendingLeaveApproval" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvPendingLeaveApprovals"
                                            runat="server"
                                            CssClass="table table-striped table-hover dataTable"
                                            GridLines="None"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            AllowSorting="true"
                                            EmptyDataText="No Record(s) found"
                                            ShowHeaderWhenEmpty="true"
                                            DataKeyNames="Id"
                                            OnPageIndexChanging="gvPendingLeaveApprovals_PageIndexChanging"
                                            OnRowCommand="gvPendingLeaveApprovals_RowCommand"
                                            PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:ButtonField HeaderText="" ButtonType="Link" Text="Approve" CommandName="approveRecord" />

                                                <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                                <asp:BoundField DataField="LeaveName" HeaderText="Leave" SortExpression="LeaveName" />

                                                <asp:TemplateField HeaderText="No Of Days" SortExpression="NumberOfDays">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Eval("NumberOfDays") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="FiledDate" HeaderText="Filed Date" SortExpression="FiledDate" />

                                                <asp:TemplateField HeaderText="Duration" SortExpression="FromDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("FromDate") %>'></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text=" - "></asp:Label>
                                                        <asp:Label ID="lblToDate" runat="server" Text='<%# Eval("ToDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Department Head Approval" SortExpression="DepartmentHeadApproval">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepartmentHeadApproval" runat="server" Text='<%# Eval("DepartmentHeadApproval") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HR Approval" SortExpression="HRApproval">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHRApproval" runat="server" Text='<%# Eval("HRApproval") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="approvedTab">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h5 class="panel-title">Approved Leave Approvals</h5>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvApproved"
                                            runat="server"
                                            CssClass="table table-striped table-hover dataTable"
                                            GridLines="None"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            AllowSorting="true"
                                            EmptyDataText="No Record(s) found"
                                            ShowHeaderWhenEmpty="true"
                                            DataKeyNames="Id"
                                            OnPageIndexChanging="gvApproved_PageIndexChanging"
                                            OnRowCommand="gvApproved_RowCommand"
                                            PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:ButtonField HeaderText="" ButtonType="Link" Text="Approve" CommandName="approveRecord" />--%>

                                                <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                                <asp:BoundField DataField="LeaveName" HeaderText="Leave" SortExpression="LeaveName" />
                                                <asp:TemplateField HeaderText="No Of Days" SortExpression="NumberOfDays">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoOfDays" runat="server" Text='<%# Eval("NumberOfDays") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FiledDate" HeaderText="Filed Date" SortExpression="FiledDate" />

                                                <asp:TemplateField HeaderText="Duration" SortExpression="FromDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("FromDate") %>'></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text=" - "></asp:Label>
                                                        <asp:Label ID="lblToDate" runat="server" Text='<%# Eval("ToDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Department Head Approval" SortExpression="DepartmentHeadApproval">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepartmentHeadApproval" runat="server" Text='<%# Eval("DepartmentHeadApproval") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HR Approval" SortExpression="HRApproval">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHRApproval" runat="server" Text='<%# Eval("HRApproval") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="cancelledTab">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h5 class="panel-title">Disapproved Leave Approvals</h5>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvRejected"
                                            runat="server"
                                            CssClass="table table-striped table-hover dataTable"
                                            GridLines="None"
                                            AutoGenerateColumns="false"
                                            AllowPaging="true"
                                            AllowSorting="true"
                                            EmptyDataText="No Record(s) found"
                                            ShowHeaderWhenEmpty="true"
                                            DataKeyNames="Id"
                                            OnPageIndexChanging="gvRejected_PageIndexChanging"
                                            OnRowCommand="gvRejected_RowCommand"
                                            PageSize="10">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:ButtonField HeaderText="" ButtonType="Link" Text="Approve" CommandName="approveRecord" />--%>

                                                <asp:BoundField DataField="FullName" HeaderText="Name" SortExpression="FullName" />
                                                <asp:BoundField DataField="LeaveName" HeaderText="Leave" SortExpression="LeaveName" />
                                                <asp:BoundField DataField="NumberOfDays" HeaderText="No Of Days" SortExpression="NumberOfDays" />
                                                <asp:BoundField DataField="FiledDate" HeaderText="Filed Date" SortExpression="FiledDate" />

                                                <asp:TemplateField HeaderText="Duration" SortExpression="FromDate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("FromDate") %>'></asp:Label>
                                                        <asp:Label ID="Label1" runat="server" Text=" - "></asp:Label>
                                                        <asp:Label ID="lblToDate" runat="server" Text='<%# Eval("ToDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Department Head Approval" SortExpression="DepartmentHeadApproval">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepartmentHeadApproval" runat="server" Text='<%# Eval("DepartmentHeadApproval") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="HR Approval" SortExpression="HRApproval">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHRApproval" runat="server" Text='<%# Eval("HRApproval") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="pagination-ys" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="tab-pane fade" id="rejectTab">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h5 class="panel-title">Rejected Leave Approvals</h5>
                        </div>
                        <div class="panel-body">
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
