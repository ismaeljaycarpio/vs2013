<%@ Page Title="Employee Master List"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Employee_Account_MasterList.aspx.cs"
    Inherits="AMS.MasterConfig.Employee_Account_MasterList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h5>Accounts</h5>
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
                        <asp:UpdatePanel ID="upEmployee" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvEmployee"
                                    runat="server"
                                    class="table table-striped table-hover dataTable"
                                    GridLines="None"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    ShowFooter="true"
                                    AllowSorting="true"
                                    DataKeyNames="UserId"
                                    OnSorting="gvEmployee_Sorting"
                                    OnRowDataBound="gvEmployee_RowDataBound"
                                    OnRowCommand="gvEmployee_RowCommand"
                                    OnPageIndexChanging="gvEmployee_PageIndexChanging"
                                    OnSelectedIndexChanging="gvEmployee_SelectedIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ID" SortExpression="Emp_Id">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmp_Id" runat="server" Text='<%# Eval("Emp_Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Full Name" SortExpression="FullName">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkFNAME" runat="server" Text='<%# Eval("FullName") %>' CommandName="Select"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Agency" HeaderText="Agency" SortExpression="Agency" />
                                        <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName" />

                                        <asp:TemplateField HeaderText="Account Status" SortExpression="IsApproved">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblStatus"
                                                    runat="server"
                                                    OnClick="lblStatus_Click"
                                                    Text='<%# (Boolean.Parse(Eval("IsApproved").ToString())) ? "Active" : "Inactive" %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Locked Out" SortExpression="IsLockedOut">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnLockedOut"
                                                    runat="server"
                                                    OnClick="lbtnLockedOut_Click"
                                                    Text='<%# (Boolean.Parse(Eval("IsLockedOut").ToString())) ? "Yes" : "No" %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Reset Password">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblReset"
                                                    runat="server"
                                                    OnClick="lblReset_Click"
                                                    Text="Reset Password">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Edit Role" CommandName="editRecord" />
                                        <asp:ButtonField HeaderText="" ButtonType="Link" Text="Delete" CommandName="deleteRecord" />

                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvEmployee" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="updateModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">

            <!-- Update Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Role</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditName">Name</label>
                                    <asp:TextBox ID="txtEditName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>


                                <div class="form-group">
                                    <label for="DDLRole">Role</label>
                                    <asp:DropDownList ID="DDLRole" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vsEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvEmployee" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Delete Modal -->
    <div id="deleteModal" class="modal fade" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Delete Record</h4>
                        </div>
                        <div class="modal-body">
                            <p class="text-danger">
                                Are you sure you want to delete this record ? 
                            All information associated with this user will also be removed.
                            </p>
                            <asp:HiddenField ID="hfDeleteId" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
