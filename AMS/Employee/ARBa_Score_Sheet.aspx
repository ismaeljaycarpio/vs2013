<%@ Page Title="ARBa Score Sheet" 
    Language="C#" 
    MasterPageFile="~/ProfileNested.master" 
    AutoEventWireup="true" 
    CodeBehind="ARBa_Score_Sheet.aspx.cs" 
    Inherits="AMS.Employee.ARBa_Score_Sheet" %>
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
                            <td colspan="2">
                                <asp:Label ID="lblAgency" runat="server" Font-Bold="true">Sample</asp:Label></td>
                        </tr>
                        <tr class="text-center">
                            <td colspan="2"><b>ARBa Score Sheet</b></td>
                        </tr>
                        <tr>
                            <td><b>Employee Name:</b>
                                <asp:Label ID="lblEmpName" runat="server"></asp:Label></td>
                            <td><b>Perion Covered:</b>
                                <asp:TextBox ID="txtPerionCovered" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="height: 37px"><b>Designation</b>
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label></td>
                            <td style="height: 37px"><b>Starte Date of Employment:</b> <asp:Label ID="lblStartDateofEmp" runat="server"></asp:Label></td>
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
                                    OnDataBound="gvEvaluation_DataBound"
                                    OnRowDataBound="gvEvaluation_RowDataBound"
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
                                                <asp:TextBox runat="server" ID="txtRating" Width="50"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                    runat="server"
                                                    Display="Dynamic"
                                                    ControlToValidate="txtRating"
                                                    ForeColor="Red"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblRating" Width="50"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Average Rating" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAveRating" runat="server" Text='<%# Eval("TSIRating") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="lblTSIRating" Width="50" Enabled="false" Font-Bold="true">40.00</asp:Label>
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

                <div class="panel-body">
                    <table class="table">
                        <%--<tr>
                            <td>RATING (Total rating divided by number of elements applicable)</td>
                            <td>
                                <asp:Label ID="lblRemarksName" runat="server"></asp:Label></td>
                        </tr>--%>
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

                <%--<div class="panel-body">
                    <table class="table">
                        <tr>
                            <td>Evaluated By:
                                <asp:Label ID="lblEvaluatedBy" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Approved By (Manager):
                                <asp:Label ID="lblApprovedByManager" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Approved By (HR Manager):
                                <asp:Label ID="lblApprovedByHRManager" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>Acknowledged By:
                                <asp:Label ID="lblAckBy" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>--%>

                <div class="panel-footer text-center">
                    <asp:Button ID="btnSumbit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSumbit_Click" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
