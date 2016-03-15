<%@ Page Title="Time In / Time Out"
    Language="C#"
    MasterPageFile="~/HRNested.master"
    AutoEventWireup="true"
    CodeBehind="TimeInTimeOut.aspx.cs"
    Inherits="AMS.HR.TimeInTimeOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- Bootstrap Modal Dialog -->
    <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" Text=""></asp:Label></h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblModalBody" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="timeoutModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upConfirmTimeOut" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Confirm Time-out</h4>
                        </div>
                        <div class="modal-body">
                            <%--<p>
                                Your last time-in was
                                <asp:Label ID="lblLastTimeIn" runat="server" ForeColor="Red"></asp:Label>
                            </p>

                            <p>
                                If you forgot to time-in, please select <span class="label label-info">No Time-In</span> in the dropdown.
                            </p>--%>

                            <div class="form">
                                <%--<div class="form-group">
                                    <label for="ddlLastTimeIn">Last Time-in:</label>
                                    <asp:DropDownList ID="ddlLastTimeIn" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>

                                <div class="form-group">
                                    <label for="txtReasons">Reasons if no login:</label>
                                    <asp:TextBox ID="txtReasons"
                                        TextMode="MultiLine"
                                        Height="80"
                                        runat="server"
                                        CssClass="form-control"
                                        placeholder="Enter reason here if you have no login...leave blank if not"></asp:TextBox>
                                </div>--%>
                                <p>
                                    Select your schedule for todays time-out. 
                                If you dont see any value, please contact your supervisor.
                                </p>

                                <div class="form">
                                    <div class="form-group">
                                        <label for="ddlTimeOutSched">Your Time-Out Schedule Today:</label>
                                        <asp:DropDownList ID="ddlTimeOutSched" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmTimeout" runat="server" Text="Time Out" CssClass="btn btn-warning" OnClick="btnConfirmTimeout_Click" CausesValidation="false" />
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="timeinModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upConfirmTimeIn" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">Confirm Time-In</h4>
                        </div>
                        <div class="modal-body">
                            <p>
                                Select your schedule for todays time-in. 
                                If you dont see any value, please contact your supervisor.
                            </p>

                            <div class="form">
                                <div class="form-group">
                                    <label for="ddlTodaysSchedule">Your Time-In Schedule Today:</label>
                                    <asp:DropDownList ID="ddlSchedule" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnConfirmTimeIn" runat="server" Text="Time In" CssClass="btn btn-warning" OnClick="btnConfirmTimeIn_Click" CausesValidation="false" />
                            <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h4><strong>Time In / Time Out</strong></h4>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong>Success!</strong> User successfully created.
                    </asp:Panel>
                    <div class="form-horizontal" role="form">

                        <div class="form-group">
                            <label for="txtEmpId" class="col-sm-2 control-label">ID</label>
                            <div class="col-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                    <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control" placeholder="Employee ID"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtEmpId"
                                        CssClass="label label-danger"
                                        ErrorMessage="Employee ID is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtPassword" class="col-sm-2 control-label">Password</label>
                            <div class="col-sm-10">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                        runat="server"
                                        Display="Dynamic"
                                        ControlToValidate="txtPassword"
                                        CssClass="label label-danger"
                                        ErrorMessage="Password is required"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtRemarks" class="col-sm-2 control-label"></label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtRemarks"
                                    TextMode="MultiLine"
                                    Height="80"
                                    runat="server"
                                    CssClass="form-control"
                                    placeholder="Enter remarks here..."></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-5">
                                <asp:Button ID="btnTimeIn" runat="server" Text="Time In" CssClass="btn btn-primary form-control" OnClick="btnTimeIn_Click" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-5">
                                <asp:Button ID="btnTimeOut" runat="server" Text="Time Out" CssClass="btn btn-warning form-control" OnClick="TimeOut_Click" />
                            </div>
                        </div>


                        <div class="form-group">
                            <label for="lblError" class="col-sm-2 control-label">&nbsp;</label>
                            <div class="col-sm-10">
                                <div class="input-group">
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
