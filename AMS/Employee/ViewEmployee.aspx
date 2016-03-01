<%@ Page Title="Profile Overview"
    Language="C#"
    MasterPageFile="~/ProfileNested.master"
    AutoEventWireup="true"
    CodeBehind="ViewEmployee.aspx.cs"
    Inherits="AMS.Employee.ViewEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            Profile Overview
        </div>
        <div class="panel-body">

            <div class="form-horizontal">
                <div class="form-group">
                    <label for="imgProfile" class="col-sm-2 control-label">&nbsp;</label>
                    <div class="col-sm-10">
                        <asp:Image ID="imgProfile" runat="server" AlternateText="Profile Image" CssClass="img-responsive" />
                    </div>
                </div>

                <div class="form-group">
                    <label for="lblName" class="col-sm-2 control-label">Name: </label>
                    <div class="col-sm-10">
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="form-group">
                    <label for="lblPosition" class="col-sm-2 control-label">Position: </label>
                    <div class="col-sm-10">
                        <asp:Label ID="lblPosition" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="form-group">
                    <label for="lblDepartment" class="col-sm-2 control-label">Department: </label>
                    <div class="col-sm-10">
                        <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="form-group">
                    <label for="lblEmpNo" class="col-sm-2 control-label">ID: </label>
                    <div class="col-sm-10">
                        <asp:Label ID="lblEmpNo" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="form-group">
                    <label for="lblMarriedStatus" class="col-sm-2 control-label">Marital Status: </label>
                    <div class="col-sm-10">
                        <asp:Label ID="lblMarriedStatus" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="form-group">
                    <label for="lblContactNo" class="col-sm-2 control-label">Contact No: </label>
                    <div class="col-sm-10">
                        <asp:Label ID="lblContactNo" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="form-group">
                    <label for="lblAgency" class="col-sm-2 control-label">Agency: </label>
                    <div class="col-sm-10">
                        <asp:Label ID="lblAgency" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="form-group">
                    <label for="lblEmergencyContactPerson" class="col-sm-2 control-label">Contact Person: </label>
                    <div class="col-sm-10">
                        <asp:Label ID="lblEmergencyContactPerson" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel-body">
            <h5>Employee Movement</h5>
            <div class="table table-responsive">
                <asp:GridView ID="gvEMovement"
                    runat="server"
                    CssClass="table table-striped"
                    AllowPaging="true"
                    AutoGenerateColumns="false"
                    ShowHeaderWhenEmpty="true"
                    EmptyDataText="No Record(s) found"
                    OnPageIndexChanging="gvEMovement_PageIndexChanging"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="EMovement" HeaderText="Movement" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                        <asp:BoundField DataField="EffectivityDate" HeaderText="Effectivity Date" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />

        <div class="panel-body">
            <h5>Job Experience</h5>
            <div class="table table-responsive">
                <asp:GridView ID="gvExperience"
                    runat="server"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false"
                    ShowHeaderWhenEmpty="true"
                    AllowPaging="true"
                    EmptyDataText="No Record(s) found"
                    OnPageIndexChanging="gvExperience_PageIndexChanging"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Job" HeaderText="Job" />
                        <asp:BoundField DataField="Company" HeaderText="Company" />
                        <asp:BoundField DataField="FromDate" HeaderText="From" />
                        <asp:BoundField DataField="ToDate" HeaderText="To" />
                        <asp:BoundField DataField="Reason" HeaderText="Reason" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />

        <div class="panel-body">
            <h5>Education</h5>
            <div class="table table-responsive">
                <asp:GridView ID="gvEducation"
                    runat="server"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false"
                    ShowHeaderWhenEmpty="true"
                    AllowPaging="true"
                    OnPageIndexChanging="gvEducation_PageIndexChanging"
                    EmptyDataText="No Record(s) found"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Course" HeaderText="Course" />
                        <asp:BoundField DataField="School" HeaderText="School" />
                        <asp:BoundField DataField="Achievement" HeaderText="Achievement" />
                        <asp:BoundField DataField="Year" HeaderText="Year" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />

        <div class="panel-body">
            <h5>Trainings</h5>
            <div class="table table-responsive">
                <asp:GridView ID="gvTrainings"
                    runat="server"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false"
                    ShowHeaderWhenEmpty="true"
                    AllowPaging="true"
                    OnPageIndexChanging="gvTrainings_PageIndexChanging"
                    EmptyDataText="No Record(s) found"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Venue" HeaderText="Venue" />
                        <asp:BoundField DataField="Details" HeaderText="Details" />
                        <asp:BoundField DataField="Date" HeaderText="Date" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />

        <div class="panel-body">
            <h5>Awards</h5>
            <div class="table table-responsive">
                <asp:GridView ID="gvAwards"
                    runat="server"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false"
                    ShowHeaderWhenEmpty="true"
                    AllowPaging="true"
                    OnPageIndexChanging="gvAwards_PageIndexChanging"
                    EmptyDataText="No Record(s) found"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Description" HeaderText="Description" />
                        <asp:BoundField DataField="Venue" HeaderText="Venue" />
                        <asp:BoundField DataField="Date" HeaderText="Date" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />

        <div class="panel-body">
            <h5>Violations</h5>
            <div class="table table-responsive">
                <asp:GridView ID="gvViolations"
                    runat="server"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false"
                    ShowHeaderWhenEmpty="true"
                    AllowPaging="true"
                    OnPageIndexChanging="gvViolations_PageIndexChanging"
                    EmptyDataText="No Record(s) found"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Violation" HeaderText="Description" />
                        <asp:BoundField DataField="Code" HeaderText="Code" />
                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                        <asp:BoundField DataField="Date" HeaderText="Date" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />

        <div class="panel-body">
            <h5>Emergency Contacts</h5>
            <div class="table table-responsive">
                <asp:GridView ID="gvForEmergency"
                    runat="server"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false"
                    ShowHeaderWhenEmpty="true"
                    AllowPaging="true"
                    OnPageIndexChanging="gvForEmergency_PageIndexChanging"
                    EmptyDataText="No Record(s) found"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="G_Name" HeaderText="Guardian" />
                        <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                        <asp:BoundField DataField="G_Address" HeaderText="Address" />
                        <asp:BoundField DataField="G_Phone" HeaderText="Contact No" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <hr />

        <div class="panel-body">
            <h5>Membership Cards</h5>
            <div class="table table-responsive">
                <asp:GridView ID="gvPersonalCards"
                    runat="server"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false"
                    ShowHeaderWhenEmpty="true"
                    AllowPaging="true"
                    OnPageIndexChanging="gvPersonalCards_PageIndexChanging"
                    EmptyDataText="No Record(s) found"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="Type" HeaderText="Type" />
                        <asp:BoundField DataField="Number" HeaderText="Number" />
                        <asp:BoundField DataField="IDate" HeaderText="Issued Date" />
                        <asp:BoundField DataField="EDate" HeaderText="Expiry Date" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="panel-body">
            <h5>Uploaded Documents</h5>
            <div class="table table-responsive">
                <asp:GridView ID="gvDocuments"
                    runat="server"
                    CssClass="table table-striped"
                    AutoGenerateColumns="false"
                    ShowHeaderWhenEmpty="true"
                    AllowPaging="true"
                    OnPageIndexChanging="gvDocuments_PageIndexChanging"
                    EmptyDataText="No File(s) Uploaded"
                    GridLines="None">
                    <Columns>
                        <asp:BoundField DataField="FileName" HeaderText="File" />
                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" Text="Download" CommandArgument='<%# Eval("FilePath") %>' runat="server" OnClick="lnkDownload_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" Text="Delete" CommandArgument='<%# Eval("FilePath") %>' runat="server" OnClick="lnkDelete_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div class="form-group" id="divDocs" runat="server">
                    <label for="txtDocs" class="col-sm-2 control-label">Documents</label>
                    <div class="col-sm-10">
                        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                        <asp:Label ID="lblFileStatus" runat="server" CssClass="label label-success"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
