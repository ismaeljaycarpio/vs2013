<%@ Page Title="Time In / Time Out"
    Language="C#"
    MasterPageFile="~/HRNested.master"
    AutoEventWireup="true"
    CodeBehind="TimeInTimeOut.aspx.cs"
    Inherits="AMS.HR.TimeInTimeOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
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
                            <label for="txtEmpId" class="col-sm-2 control-label"></label>
                            <div class="col-sm-6">                           
                                <asp:TextBox ID="txtRemarks" 
                                    TextMode="MultiLine"
                                    Height="100" 
                                    runat="server" 
                                    CssClass="form-control" 
                                    placeholder="Enter reason here if you're late...leave blank if not"></asp:TextBox>
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
