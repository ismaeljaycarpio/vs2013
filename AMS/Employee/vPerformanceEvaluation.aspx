<%@ Page Title="View Performance Evaluation" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="vPerformanceEvaluation.aspx.cs" Inherits="AMS.Employee.vPerformanceEvaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Performance Evaluation Form
                        <a href="pPerformanceEvaluation.aspx" runat="server" id="lnkprint">
                            <span class="glyphicon glyphicon-print pull-right"></span>
                        </a>
                    </h5>
                </div>

                <div class="panel-body">
                    <table class="table">
                        <tr class="text-center">
                            <td colspan="3">
                                <asp:Label ID="lblAgency" runat="server" Font-Bold="true">Sample</asp:Label></td>
                        </tr>
                        <tr class="text-center">
                            <td colspan="3"><b>EMPLOYEE PERFORMANCE EVALUATION</b></td>
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
                            <td colspan="3">Carefully read each Job Factor and its definitions prior to evaluation. Then indicate employee's rating scale below. Briefly explain the reason for each rating in the space provided for the Critical incidents.
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upEvaluation" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvEvaluation"
                                    runat="server"
                                    class="table table-striped table-hover"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="false"
                                    AllowPaging="true"
                                    ShowFooter="true"
                                    DataKeyNames="Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompetence" runat="server" Text='<%# Eval("Competence") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEvaluation_Score_Id" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompetenceCat" runat="server" Text='<%# Eval("CompetenceCat") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalStatic" runat="server" CssClass="pull-right">TOTAL</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rating">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" Text='<%# Eval("StaffRating") %>' CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="txtStaffRating"
                                                    ForeColor="Red"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1"
                                                    runat="server"
                                                    ForeColor="Red"
                                                    ControlToValidate="txtStaffRating"
                                                    Display="Dynamic"
                                                    MaximumValue="5"
                                                    MinimumValue="1"
                                                    Type="Integer"
                                                    ErrorMessage="(1-5) only"></asp:RangeValidator>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaffTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Evaluator Rating">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtEvaluatorRating" Width="50" Text='<%# Eval("EvaluatorRating") %>' CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="txtEvaluatorRating"
                                                    ForeColor="Red"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator2"
                                                    runat="server"
                                                    ForeColor="Red"
                                                    ControlToValidate="txtEvaluatorRating"
                                                    Display="Dynamic"
                                                    MaximumValue="5"
                                                    MinimumValue="1"
                                                    Type="Integer"
                                                    ErrorMessage="(1-5) only"></asp:RangeValidator>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblEvaluatorTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvEvaluation" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <asp:Panel ID="pnlEvaluator" runat="server">
                    <div class="panel-body">
                    <table class="table">
                        <tr>
                            <td>RATING (Total rating divided by number of elements applicable)</td>
                            <td>
                                <asp:Label ID="lblRemarksName" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>REMARKS:
                            </td>
                            <td>Other Areas of Improvement:
                            </td>
                        </tr>
                        <tr>
                            <td>(1 ) Unacceptable</td>
                            <td>
                                <asp:TextBox ID="txtUnacceptable" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>(2 ) Fall Short of Objectives</td>
                            <td>
                                <asp:TextBox ID="txtFallShort" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>(3 ) Effective</td>
                            <td>
                                <asp:TextBox ID="txtEffective" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>(4 ) Highly Effective</td>
                            <td>
                                <asp:TextBox ID="txtHighlyEffective" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>(5 ) Exceptional</td>
                            <td>
                                <asp:TextBox ID="txtExceptional" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Remarks/Recommendation</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtRecommendation" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Need Improvement</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtNeedImpro" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="panel-body">
                    <table class="table">
                        <tr>
                            <td><b>Evaluated By:</b>
                                <asp:Label ID="lblEvaluatedBy" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Approved By (Manager):</b>
                                <asp:Label ID="lblApprovedByManager" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Approved By (HR Manager):</b>
                                <asp:Label ID="lblApprovedByHRManager" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Acknowledged By:</b>
                                <asp:Label ID="lblAckBy" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Date Evaluated:</b>
                                <asp:Label ID="lblDateEvaluated" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                </asp:Panel>

                <div class="panel-footer text-center">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfEvaluationId" runat="server" />
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
