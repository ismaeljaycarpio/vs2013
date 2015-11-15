<%@ Page Title="Job Details" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="JobDetails.aspx.cs" Inherits="AMS.Employee.JobDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        $(function () {
            $('#joinDate').datepicker();
            $('#cStartingDate').datepicker();
            $('#cEndingDate').datepicker();
        });
    </script>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Job Details
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="txtEmpId" class="col-sm-2 control-label">Employee ID</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtEmpId" runat="server" CssClass="form-control" placeholder="Employee ID"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEmpId"
                                    CssClass="label label-danger"
                                    ErrorMessage="Employee ID is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlPosition" class="col-sm-2 control-label">Position</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="upPos" runat="server">
                            <ContentTemplate>
                                <div class="form-group" style="display: none;">
                                    <label for="lblRole" class="col-sm-2 control-label">Position</label>
                                    <div class="col-sm-10">
                                        <asp:Label ID="lblRole" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="ddlDepartment" class="col-sm-2 control-label">Department</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlPosition" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        

                        <div class="form-group">
                            <label for="txtSubUnit" class="col-sm-2 control-label">Sub-Unit</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtSubUnit" runat="server" CssClass="form-control" placeholder="Sub Unit"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtSubUnit"
                                    CssClass="label label-danger"
                                    ErrorMessage="Sub Unit is required"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="upRank" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <label for="txtManager" class="col-sm-2 control-label">Manager</label>
                                    <div class="col-sm-10">
                                        <asp:Label ID="lblManager" runat="server" ></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="lblSupervisor" class="col-sm-2 control-label">Supervisor</label>
                                    <div class="col-sm-10">
                                        <asp:Label ID="lblSupervisor" runat="server"  ></asp:Label>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlPosition" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        

                        <div class="form-group">
                            <label for="ddlAgency" class="col-sm-2 control-label">Agency</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="ddlAgency" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtEmpStatus" class="col-sm-2 control-label">Employment Status</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtEmpStatus" runat="server" CssClass="form-control" placeholder="Employment Status"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server"
                                    Display="Static"
                                    ControlToValidate="txtEmpStatus"
                                    CssClass="label label-danger"
                                    ErrorMessage="Employment Status is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>


                        <div class="form-group">
                            <label for="txtJoinDate" class="col-sm-2 control-label">Join Date</label>
                            <div class="col-sm-4">
                                <div class="input-group date" id="joinDate">
                                    <asp:TextBox ID="txtJoinDate" runat="server" CssClass="form-control" placeholder="Join Date"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtJoinDate"
                                    CssClass="label label-danger"
                                    ErrorMessage="Join Date is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtContractStartingDate" class="col-sm-2 control-label">Contract Starting Date</label>
                            <div class="col-sm-4">
                                <div class="input-group date" id="cStartingDate">
                                    <asp:TextBox ID="txtContractStartingDate" runat="server" CssClass="form-control" placeholder="Contract Starting Date"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtContractStartingDate"
                                    CssClass="label label-danger"
                                    ErrorMessage="Contract Starting Date is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtContractEndingDate" class="col-sm-2 control-label">Contract Ending Date</label>
                            <div class="col-sm-4">
                                <div class="input-group date" id="cEndingDate">
                                    <asp:TextBox ID="txtContractEndingDate" runat="server" CssClass="form-control" placeholder="Contract Ending Date"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtContractEndingDate"
                                    CssClass="label label-danger"
                                    ErrorMessage="Contract Ending Date is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtEMovement" class="col-sm-2 control-label">E-Movement</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtEMovement" runat="server" CssClass="form-control" placeholder="E-Movement"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtEMovement"
                                    CssClass="label label-danger"
                                    ErrorMessage="E-Movement is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-10">
                                <asp:Button ID="btnUpdateJob" runat="server" CssClass="btn btn-primary" Text="Update" OnClick="btnUpdateJob_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
