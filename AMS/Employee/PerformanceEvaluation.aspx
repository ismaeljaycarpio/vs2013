<%@ Page Title="Performance Evaluation"
    Language="C#"
    MasterPageFile="~/ProfileNested.master"
    AutoEventWireup="true"
    CodeBehind="PerformanceEvaluation.aspx.cs"
    Inherits="AMS.Employee.PerformanceEvaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Performance Evaluation Form</h5>
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

                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompetenceCatId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
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
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50"></asp:TextBox>
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
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Evaluator">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtEvaluatorRating" Width="50"></asp:TextBox>
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

                <asp:Panel ID="pnlEvaluatorOnly" runat="server" Visible="false">
                    <div class="panel-body">
                        <table class="table">
                            <tr>
                                <td>REMARKS:
                                </td>
                                <td>Other Areas of Improvement:
                                </td>
                            </tr>
                            <tr>
                                <td>(1 ) Unacceptable</td>
                                <td>
                                    <asp:TextBox ID="txtUnacceptable" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>(2 ) Fall Short of Objectives</td>
                                <td>
                                    <asp:TextBox ID="txtFallShort" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>(3 ) Effective</td>
                                <td>
                                    <asp:TextBox ID="txtEffective" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>(4 ) Highly Effective</td>
                                <td>
                                    <asp:TextBox ID="txtHighlyEffective" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>(5 ) Exceptional</td>
                                <td>
                                    <asp:TextBox ID="txtExceptional" runat="server"></asp:TextBox></td>
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
                </asp:Panel>
                <div class="panel-footer text-center">
                    <asp:Button ID="btnSumbit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSumbit_Click" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
