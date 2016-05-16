<%@ Page Title="My Leaves" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="MyLeave.aspx.cs" Inherits="AMS.Leave.MyLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Add Modal -->
    <div id="addModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upAdd" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">File Leave</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <label for="ddlLeave">Leave: </label>
                                    <asp:DropDownList ID="ddlLeave" 
                                        runat="server" 
                                        CssClass="form-control"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlLeave_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server"
                                        Display="Dynamic"
                                        InitialValue="0"
                                        ControlToValidate="ddlLeave"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Leave is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="lblRemainingDays">Remaining Days:</label>
                                    <asp:Label ID="lblRemainingDays" runat="server"></asp:Label>
                                </div>


                                <div class="form-group">
                                    <label for="txtFromDate">From:</label>
                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control clsDatePicker" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtFromDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="From Date is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtToDate">To:</label>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control clsDatePicker" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtToDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="To Date is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtNoOfDays">No Of Days:</label>
                                    <asp:TextBox ID="txtNoOfDays" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtToDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="No of Days is required"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtNoOfDays"
                                        ForeColor="Red"
                                        ValidationExpression="^[1-9]\d*$"
                                        ValidationGroup="vgAdd"
                                        ErrorMessage="Positive Numbers only">*</asp:RegularExpressionValidator>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblAddValidateRemainingDays" runat="server" CssClass="label label-danger"></asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save" ValidationGroup="vgAdd" OnClick="btnSave_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlLeave" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Edit Modal -->
    <div id="updateModal" class="modal fade" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true" role="dialog">
        <div class="modal-dialog">
            <!-- Update Modal content-->
            <div class="modal-content">
                <asp:UpdatePanel ID="upEdit" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Edit Filed Leave</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form">
                                <div class="form-group">
                                    <asp:Label ID="lblRowId" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label for="ddlEditLeave">Leave: </label>
                                    <asp:DropDownList ID="ddlEditLeave" 
                                        runat="server" 
                                        CssClass="form-control"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlEditLeave_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                        runat="server"
                                        Display="Dynamic"
                                        InitialValue="0"
                                        ControlToValidate="ddlEditLeave"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgEdit"
                                        ErrorMessage="Leave is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="lblEditRemainingDays">Remaining Days:</label>
                                    <asp:Label ID="lblEditRemainingDays" runat="server" Text="0"></asp:Label>
                                </div>


                                <div class="form-group">
                                    <label for="txtEditFromDate">From:</label>
                                    <asp:TextBox ID="txtEditFromDate" runat="server" CssClass="form-control clsDatePicker" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditFromDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgEdit"
                                        ErrorMessage="From Date is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditToDate">To:</label>
                                    <asp:TextBox ID="txtEditToDate" runat="server" CssClass="form-control clsDatePicker" placeholder="Date" data-provide="datepicker"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditToDate"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgEdit"
                                        ErrorMessage="To Date is required"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group">
                                    <label for="txtEditNoOfDays">No Of Days:</label>
                                    <asp:TextBox ID="txtEditNoOfDays" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditNoOfDays"
                                        CssClass="label label-danger"
                                        ValidationGroup="vgEdit"
                                        ErrorMessage="No of Days is required"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEditNoOfDays"
                                        ForeColor="Red"
                                        ValidationGroup="vgEdit"
                                        ValidationExpression="^[1-9]\d*$"
                                        ErrorMessage="Positive Numbers only">*</asp:RegularExpressionValidator>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblEditValidateRemainingDays" runat="server" CssClass="label label-danger"></asp:Label>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary" Text="Update" ValidationGroup="vgEdit" OnClick="btnUpdate_Click" />
                            <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancel</button>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvMyLeaves" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEditLeave" EventName="SelectedIndexChanged" />
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
                            Are you sure you want to delete this record ?
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

    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h5>My Leaves</h5>
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

                        <div class="table-responsive">
                            <asp:UpdatePanel ID="upMyLeaves" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvMyLeaves"
                                        runat="server"
                                        CssClass="table table-striped table-hover dataTable"
                                        GridLines="None"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true"
                                        AllowSorting="true"
                                        EmptyDataText="No Record(s) found"
                                        ShowHeaderWhenEmpty="true"
                                        DataKeyNames="Id"
                                        OnRowDataBound="gvMyLeaves_RowDataBound"
                                        OnPageIndexChanging="gvMyLeaves_PageIndexChanging"
                                        OnRowCommand="gvMyLeaves_RowCommand"
                                        OnSorting="gvMyLeaves_Sorting"
                                        PageSize="10">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Row Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbEdit" runat="server" Text="Edit" CommandName="editRecord" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

                                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnShowDelete" runat="server" Text="Delete" CommandName="deleteRecord" CssClass="btn btn-danger" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnPrintReport" 
                                                        runat="server" 
                                                        Text="Print" 
                                                        CommandName="printRecord" 
                                                        CssClass="btn btn-default" 
                                                        CommandArgument='<%#((GridViewRow) Container).RowIndex %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>
                                    <asp:Button ID="btnOpenModal"
                                        runat="server"
                                        CssClass="btn btn-info btn-sm"
                                        Text="File Leave"
                                        OnClick="btnOpenModal_Click"
                                        CausesValidation="false" />
                                </ContentTemplate>
                                <Triggers></Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
