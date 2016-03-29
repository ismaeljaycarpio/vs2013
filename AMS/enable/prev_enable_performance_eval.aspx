<%@ Page Title="View Evaluation Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="prev_enable_performance_eval.aspx.cs" Inherits="AMS.enable.prev_enable_performance_eval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Enable Performance Evaluation Form</h5>
                </div>

                <div class="panel-body">
                    <table class="table">
                        <tr class="text-center">
                            <td colspan="3">
                                <asp:Label ID="lblAgency" runat="server"></asp:Label></td>
                        </tr>

                        <tr class="text-center">
                            <td colspan="3">PERFORMANCE EVALUATION FORM (MANAGERS & EXECUTIVES)</td>
                        </tr>
                        <tr>
                            <td><b>Employee Name:</b>
                                <asp:Label ID="lblEmpName" runat="server"></asp:Label></td>
                            <td><b>Department:</b>
                                <asp:Label ID="lblDepartment" runat="server"></asp:Label></td>
                            <td><b>Position:</b>
                                <asp:Label ID="lblPosition" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td><b>Date Hired:</b>
                                <asp:Label ID="lblDateHired" runat="server"></asp:Label>
                            </td>
                            <td><b>Evaluation Date:</b>
                                <asp:Label ID="lblEvalDate" runat="server"></asp:Label></td>
                            <td><b>Date of Last Evaluation:</b>
                                <asp:Label ID="lblDateLastEvaluation" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"><b>Next Evaluation Date:</b>
                                <asp:TextBox ID="txtNextEvaluationDate" runat="server" data-provide="datepicker" placeholder="Next Evaluation Date" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server"
                                    Display="Dynamic"
                                    ForeColor="Red"
                                    ControlToValidate="txtNextEvaluationDate"
                                    ErrorMessage="Next Evaluation Date is Required"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <p>
                                    <b>Objective</b>
                                    To be able to gauge the employee through fair evaluation and come up on an agreed resolutions and appropriate actions necessary to determine the next procedure to be taken.	

                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td><b>Rating System</b></td>
                                    </tr>
                                    <tr>
                                        <td><b>Scale</b></td>
                                        <td><b>Rating</b></td>
                                        <td><b>Description</b></td>
                                    </tr>
                                    <tr>
                                        <td>5</td>
                                        <td><b>Excellent</b></td>
                                        <td>Exceeds target and deliver outputs beyond expected schedule</td>
                                    </tr>
                                    <tr>
                                        <td>4.00 - 4.99</td>
                                        <td><b>Very Good</b></td>
                                        <td>Delivers results as expected, open to new learnings, etc.</td>
                                    </tr>
                                    <tr>
                                        <td>3.00 - 3.99</td>
                                        <td><b>Satisfactory</b></td>
                                        <td>Meets targets timely, shows enough stamina, behavior & mindworks</td>
                                    </tr>
                                    <tr>
                                        <td>2.00 - 2.99</td>
                                        <td><b>Needs Improvement</b></td>
                                        <td>Lacks initiative and drive in the pursuit of the work targets</td>
                                    </tr>
                                    <tr>
                                        <td>1.00 - 2.99</td>
                                        <td><b>Failed</b></td>
                                        <td>Works behind schedule, attitude and aptitude are inappropriate</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upEHI" runat="server">
                            <ContentTemplate>

                                <asp:GridView ID="gvOrientation"
                                    runat="server"
                                    GridLines="None"
                                    ShowFooter="true"
                                    class="table table-striped table-hover"
                                    ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="false"
                                    OnRowDataBound="gvOrientation_RowDataBound"
                                    DataKeyNames="Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="A.Orientation of Task/Core Competencies (50%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ratee">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" CssClass="form-control" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="txtStaffRating"
                                                    ForeColor="Red"
                                                    ErrorMessage="This is required"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1"
                                                    runat="server"
                                                    ControlToValidate="txtStaffRating"
                                                    Display="Dynamic"
                                                    MaximumValue="5"
                                                    MinimumValue="1"
                                                    Type="Double"
                                                    ErrorMessage="must be 1-5"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rater" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" CssClass="form-control" Text='<%# Eval("EvaluatorRating") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="txtEvaluatorRating"
                                                    ForeColor="Red"
                                                    ErrorMessage="This is required"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator2"
                                                    runat="server"
                                                    ControlToValidate="txtEvaluatorRating"
                                                    Display="Dynamic"
                                                    MaximumValue="5"
                                                    MinimumValue="1"
                                                    Type="Double"
                                                    ForeColor="Red"
                                                    ErrorMessage="must be 1-5 (can contain decimal)"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Grade">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalGrade" runat="server">-</asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>

                                <asp:GridView ID="gvBehavior"
                                    runat="server"
                                    GridLines="None"
                                    ShowFooter="true"
                                    class="table table-striped table-hover"
                                    ShowHeaderWhenEmpty="true"
                                    OnRowDataBound="gvBehavior_RowDataBound"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="B. Behavior and Characterisitics (25%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ratee">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" CssClass="form-control" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="txtStaffRating"
                                                    ForeColor="Red"
                                                    ErrorMessage="This is required"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1"
                                                    runat="server"
                                                    ControlToValidate="txtStaffRating"
                                                    Display="Dynamic"
                                                    MaximumValue="5"
                                                    MinimumValue="1"
                                                    Type="Double"
                                                    ErrorMessage="must be 1-5"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rater" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" CssClass="form-control" Text='<%# Eval("EvaluatorRating") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="txtEvaluatorRating"
                                                    ForeColor="Red"
                                                    ErrorMessage="This is required"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator2"
                                                    runat="server"
                                                    ControlToValidate="txtEvaluatorRating"
                                                    Display="Dynamic"
                                                    MaximumValue="5"
                                                    MinimumValue="1"
                                                    Type="Double"
                                                    ForeColor="Red"
                                                    ErrorMessage="must be 1-5 (can contain decimal)"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Grade">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalGrade" runat="server">-</asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>

                                <asp:GridView ID="gvManagement"
                                    runat="server"
                                    GridLines="None"
                                    ShowFooter="true"
                                    class="table table-striped table-hover"
                                    ShowHeaderWhenEmpty="true"
                                    OnRowDataBound="gvManagement_RowDataBound"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="C. Management Critical Areas (25%)	)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ratee">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" CssClass="form-control" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="txtStaffRating"
                                                    ForeColor="Red"
                                                    ErrorMessage="This is required"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1"
                                                    runat="server"
                                                    ControlToValidate="txtStaffRating"
                                                    Display="Dynamic"
                                                    MaximumValue="5"
                                                    MinimumValue="1"
                                                    Type="Double"
                                                    ErrorMessage="must be 1-5"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rater" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" CssClass="form-control" Text='<%# Eval("EvaluatorRating") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="txtEvaluatorRating"
                                                    ForeColor="Red"
                                                    ErrorMessage="This is required"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator2"
                                                    runat="server"
                                                    ControlToValidate="txtEvaluatorRating"
                                                    Display="Dynamic"
                                                    MaximumValue="5"
                                                    MinimumValue="1"
                                                    Type="Double"
                                                    ForeColor="Red"
                                                    ErrorMessage="must be 1-5 (can contain decimal)"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Grade">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalGrade" runat="server">-</asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>

                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <asp:Panel ID="pnlStaffOnly" runat="server" Visible="false" CssClass="panel-body">
                    <table class="table">
                        <tr>
                            <td colspan="2"><b>D. Narrative Evaluation</b></td>
                        </tr>
                        <tr>
                            <td colspan="2">1. What would you identify as the manager/administrator’s strengths, expressed in terms of principal results achieved during the evaluation period?
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtEnableManagerStength" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td colspan="2">2. What performance areas would you identify as needing improvement? Why? What constructive, positive suggestions can you offer to enhance performance?
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtEnableNeedImprovement" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                    </table>
                </asp:Panel>


                <asp:Panel ID="pnlEvaluatorsOnly" runat="server" Visible="false" CssClass="panel-body">
                    <table class="table">
                        <tr>
                            <td colspan="2"><b>Rater's Recommendation/Additional Notes:	</b>
                                <asp:TextBox ID="txtEnableRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <div class="panel-body">
                    <table class="table">
                        <tr>
                            <td><b>Evaluated By:</b>
                                <asp:Label ID="lblEvaluatedBy" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblForManager" runat="server" Font-Bold="true">Manager</asp:Label>
                                <asp:Label ID="lblApprovedByManager" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblForHRManager" runat="server" Font-Bold="true">HR Manager</asp:Label>
                                <asp:Label ID="lblApprovedByHRManager" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Acknowledged By:</b>
                                <asp:Label ID="lblAckBy" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>

                <div class="panel-footer text-center">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" CausesValidation="true" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
    <asp:HiddenField ID="hfEvaluationId" runat="server" />
</asp:Content>
