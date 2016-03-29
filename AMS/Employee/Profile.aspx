<%@ Page Title="Personal Details" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="AMS.Employee.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        $(document).ready(function () {
            $('#dtpDOB').datepicker();
        });
    </script>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Personal Details
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                            <strong>Success!</strong> User successfully updated.
                        </asp:Panel>
                        <div class="form-group">
                            <label for="imgProfile" class="col-sm-2 control-label">&nbsp;</label>
                            <div class="col-sm-10">
                                <asp:Image ID="imgProfile" runat="server" AlternateText="Profile Image" Height="200" Width="200" />
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-10">
                                <asp:Button ID="btnUpload"
                                    runat="server"
                                    Text="Upload"
                                    OnClick="btnUpload_Click"
                                    CausesValidation="false"
                                    CssClass="btn btn-default" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtFirstName" class="col-sm-2 control-label">First Name</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtFirstName"
                                    CssClass="label label-danger"
                                    ErrorMessage="First Name is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtMiddleName" class="col-sm-2 control-label">Middle Name</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control" placeholder="Middle Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtMiddleName"
                                    CssClass="label label-danger"
                                    ErrorMessage="Middle Name is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtLastName" class="col-sm-2 control-label">Last Name</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtLastName"
                                    CssClass="label label-danger"
                                    ErrorMessage="Last Name is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtLastName" class="col-sm-2 control-label">Contact No</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" placeholder="Contact No"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtContactNo"
                                    CssClass="label label-danger"
                                    ErrorMessage="Contact No is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="rblGender" class="col-sm-2 control-label">Marital Status</label>
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="Single">&nbsp; Single &nbsp;</asp:ListItem>
                                    <asp:ListItem Value="Married">&nbsp; Married &nbsp;</asp:ListItem>
                                    <asp:ListItem Value="Widowed">&nbsp; Widowed &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="rblStatus"
                                    CssClass="label label-danger"
                                    ErrorMessage="Choose your marital status"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="rblGender" class="col-sm-2 control-label">Gender</label>
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="MALE">&nbsp; Male &nbsp;</asp:ListItem>
                                    <asp:ListItem Value="FEMALE">&nbsp;Female</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="rblGender"
                                    CssClass="label label-danger"
                                    ErrorMessage="Choose your Gender"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlPosition" class="col-sm-2 control-label">Nationality</label>
                            <div class="col-sm-6">
                                <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>


                        <div class="form-group">
                            <label for="txtDoB" class="col-sm-2 control-label">Date of Birth</label>
                            <div class="col-sm-4">
                                <div class="input-group date" id="dtpDOB">
                                    <asp:TextBox ID="txtDoB" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtDoB"
                                    CssClass="label label-danger"
                                    ErrorMessage="Date of Birth is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="rblBloodType" class="col-sm-2 control-label">Blood Type</label>
                            <div class="col-sm-10">
                                <asp:RadioButtonList ID="rblBloodType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0-">&nbsp; O- &nbsp;</asp:ListItem>
                                    <asp:ListItem Value="0+">&nbsp; O+ &nbsp;</asp:ListItem>
                                    <asp:ListItem Value="A-">&nbsp; A- &nbsp;</asp:ListItem>
                                    <asp:ListItem Value="A+">&nbsp; A+ &nbsp;</asp:ListItem>
                                    <asp:ListItem Value="B-">&nbsp; B- &nbsp;</asp:ListItem>
                                    <asp:ListItem Value="B+">&nbsp; B+ &nbsp;</asp:ListItem>
                                    <asp:ListItem Value="AB-">&nbsp; AB- &nbsp;</asp:ListItem>
                                    <asp:ListItem Value="AB+">&nbsp; AB+ &nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="rblBloodType"
                                    CssClass="label label-danger"
                                    ErrorMessage="Blood Type is required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtLastName" class="col-sm-2 control-label">Language</label>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtLanguage" runat="server" CssClass="form-control" placeholder="Language"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                    runat="server"
                                    Display="Dynamic"
                                    ControlToValidate="txtLanguage"
                                    CssClass="label label-danger"
                                    ErrorMessage="Language required"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-offset-2 col-sm-10">
                                <asp:Button ID="btnUpdate"
                                    runat="server"
                                    CssClass="btn btn-primary"
                                    Text="Update"
                                    CausesValidation="true"
                                    OnClick="btnUpdate_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
