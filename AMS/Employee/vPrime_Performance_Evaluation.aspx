<%@ Page Title="View Prime Performance Evaluation"
    Language="C#"
    MasterPageFile="~/ProfileNested.master"
    AutoEventWireup="true"
    CodeBehind="vPrime_Performance_Evaluation.aspx.cs"
    Inherits="AMS.Employee.vPrime_Performance_Evaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        $(function () {
            $('#<%: txtLastDateEval.ClientID%>').datepicker();
        });
    </script>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Performance Evaluation Form<
                    <a href="pPrime_Performance_Evaluation">
                        <span class="glyphicon glyphicon-print pull-right"></span>
                    </a>
                    </h5>
                </div>

                <div class="panel-body">
                    <table class="table">
                        <tr class="text-center">
                            <td colspan="3">
                                <img src="../Images/PrimePowerLogo.png" alt="Prime Logo" />
                            </td>
                        </tr>
                        <tr class="text-center">
                            <td colspan="3">
                                <asp:Label ID="lblAgency" runat="server">MANPOWER SERVICES</asp:Label></td>
                        </tr>
                        <tr class="text-center">
                            <td colspan="3">EMPLOYEE PERFORMANCE EVALUATION</td>
                        </tr>
                        <tr>
                            <td colspan="2"><b>Employee Name:</b>
                                <asp:Label ID="lblEmpName" runat="server"></asp:Label></td>
                            <td><b>Department:</b>
                                <asp:Label ID="lblDepartment" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><b>Date Hired:</b>
                                <asp:Label ID="lblDateHired" runat="server"></asp:Label></td>
                            <td><b>Evaluation Date:</b>
                                <asp:Label ID="lblEvalDate" runat="server"></asp:Label></td>
                            <td><b>Date of Last Evaluation:</b>
                                <asp:TextBox ID="txtLastDateEval" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                    runat="server"
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    ControlToValidate="txtLastDateEval" 
                                    ErrorMessage="* Required"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"><b>Date of Next Evaluation: </b>
                                <asp:RadioButtonList ID="rblNextEvaluation"
                                    runat="server"
                                    RepeatDirection="Horizontal"
                                    CellSpacing="15">
                                    <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                    <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                    <asp:ListItem Value="Semi-Annual">Semi-Annual</asp:ListItem>
                                    <asp:ListItem Value="Annual">Annual</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                    runat="server"
                                    ControlToValidate="rblNextEvaluation"
                                    ForeColor="Red"
                                    Display="Dynamic"
                                    ErrorMessage="Choose one"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <p>Note: This form must be accomplished and submitted to the HR on or before the evaluation date and must be accomplished legibly and hand-written. Use whole or half numbers only in scoring (e.g 3, 2.5). Comment portion may be filled up by staff and/or evaluator.</p>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td colspan="2" class="text-center">Rating System</td>
                                    </tr>
                                    <tr>
                                        <td>5= significantly exceeds expectation</td>
                                        <td>3= Satisfactory</td>
                                    </tr>
                                    <tr>
                                        <td>4= Exceeds expectations</td>
                                        <td>2= Needs Improvement</td>
                                    </tr>
                                    <tr>
                                        <td>1= Unsatisfactory / Failed</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="upPrimePower" runat="server">
                            <ContentTemplate>
                                <b>SECTION I: INTERPERSONAL SKILLS   (10%)</b>
                                <asp:GridView ID="gvCooperation"
                                    runat="server"
                                    class="table table-striped table-hover"
                                    GridLines="None"
                                    ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="false"
                                    ShowFooter="true"
                                    OnRowDataBound="gvCooperation_RowDataBound"
                                    DataKeyNames="Id">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="A. Cooperation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaffTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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
                                            <FooterTemplate>
                                                <asp:Label ID="lblEvaluatorTotal" runat="server"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection1A" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                                <asp:GridView ID="gvAttendanceAndPunctuality"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="B. Attendance and Punctuality">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection1B" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                                <asp:GridView ID="gvInterpersonalRelationship"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="C. Interpersonal Relationship">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection1C" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                                <b>SECTION II: WORK ATTITUDE (30%)</b>
                                <asp:GridView ID="gvAttitude"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="A. Attitude">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection2A" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                                <asp:GridView ID="gvInitiative"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="B. Initiative">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection2B" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                                <asp:GridView ID="gvJudgement"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="C. Judgement">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection2C" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />
                                <br />

                                <b>SECTION III: WORK PERFORMANCE (60%)</b>
                                <asp:GridView ID="gvCommunication"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="A. Communication">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection3A" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                                <asp:GridView ID="gvSafety"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="B. Safety">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection3B" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                                <asp:GridView ID="gvDependability"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="C. Dependability">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection3C" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                                <asp:GridView ID="gvSpecificJobSkills"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="D. Specific Job Skills ( Write NA if not applicable)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection3D" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                                <asp:GridView ID="gvProductivity"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="E. Productivity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection3E" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                                <asp:GridView ID="gvOrganizationalSkills"
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
                                                <asp:Label ID="lblCompetenceCatQId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="F. Organizational Skills">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblStaticTotal" runat="server" CssClass="pull-right">Total</asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Staff">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtStaffRating" Width="50" MaxLength="3" Text='<%# Eval("StaffRating") %>'></asp:TextBox>
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

                                        <asp:TemplateField HeaderText="Evaluator" ControlStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEvaluatorRating" runat="server" MaxLength="3" Text='<%# Eval("EvaluatorRating")%>'></asp:TextBox>
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

                                    </Columns>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                                <asp:TextBox ID="txtCommentSection3F" runat="server" TextMode="MultiLine" CssClass="form-control" placeholder="Comments:"></asp:TextBox>
                                <br />

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="gvCooperation" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="panel-body">
                    <asp:Panel ID="pnlEvaluatorsOnly" runat="server">
                        <table class="table">
                            <tr>
                                <td colspan="2"><b>SECTION IV: (FOR EVALUATOR ONLY)</b></td>
                            </tr>
                            <tr>
                                <td colspan="2">1. What creative contribution   (new ideas, procedures, etc.) has the employee made to the company?
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtCreativeContribution" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td colspan="2">2. What new skills have the employee learned or shown improvement?
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtNewSkill" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td colspan="2">3. What is the employee’s greatest strength or area of contribution to the company?
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtEmployeesStrength" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td colspan="2">4. Where could there be improvement in the employee; what specific training should be considered?
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtImprovement" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td colspan="2">5. What changes would the employee like to see in the company operations?
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtChanges" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td colspan="2">6. What are the employee’s personal goals?
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtPersonalGoals" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>

                            <tr>
                                <td colspan="2">7. Recommendation
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtRecommendation" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox></td>
                            </tr>

                        </table>
                    </asp:Panel>
                </div>

                <div class="panel-body">
                    <asp:Panel ID="pnlHROnly" runat="server">
                        <table class="table">
                            <tr>
                                <td colspan="2"><b>EVALUATION: (HR USE ONLY)</b></td>
                            </tr>
                            <tr>
                                <td><b>A. Total Grade</b>
                                    <asp:Label ID="lblTotalGrade" runat="server"></asp:Label></td>
                                <td>SECTION III A:
                                <asp:Label ID="lblSection3A" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>SECTION I A:
                                <asp:Label ID="lblSection1A" runat="server"></asp:Label></td>
                                <td>B:
                                <asp:Label ID="lblSection3B" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>B:
                                <asp:Label ID="lblSection1B" runat="server"></asp:Label></td>
                                <td>C:
                                <asp:Label ID="lblSection3C" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>C:
                                <asp:Label ID="lblSection1C" runat="server"></asp:Label></td>
                                <td>D:
                                <asp:Label ID="lblSection3D" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>E:
                                <asp:Label ID="lblSection3E" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>F:
                                <asp:Label ID="lblSection3F" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>TOTAL :
                                    <asp:Label ID="lblTotalSection1" runat="server"></asp:Label>
                                    ÷3 =
                                    <asp:Label ID="lblDivTotalSection1" runat="server"></asp:Label></td>
                                <td>TOTAL  :
                                    <asp:Label ID="lblTotalSection3" runat="server"></asp:Label>
                                    ÷6 =
                                    <asp:Label ID="lblDivTotalSection3" runat="server"></asp:Label></td>
                            </tr>

                            <tr>
                                <td></td>
                            </tr>

                            <tr>
                                <td>SECTION II A:
                                    <asp:Label ID="lblSection2a" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>B:
                                    <asp:Label ID="lblSection2b" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>C:
                                    <asp:Label ID="lblSection2c" runat="server"></asp:Label></td>
                            </tr>

                            <tr>
                                <td colspan="2">TOTAL :
                                    <asp:Label ID="lblTotalSection2" runat="server"></asp:Label>
                                    ÷3 =
                                    <asp:Label ID="lblDivTotalSection2" runat="server"></asp:Label></td>
                            </tr>

                            <tr>
                                <td colspan="2">Section I    =   
                                    <asp:Label ID="lblDivDivTotalSection1" runat="server"></asp:Label>
                                    x 0.10 =
                                    <asp:Label ID="lblSection1P" runat="server" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">Section II   =   
                                    <asp:Label ID="lblDivDivTotalSection2" runat="server"></asp:Label>
                                    x 0.30 =
                                    <asp:Label ID="lblSection2P" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Section III  =   
                                    <asp:Label ID="lblDivDivTotalSection3" runat="server"></asp:Label>
                                    x 0.60 =
                                    <asp:Label ID="lblSection3P" runat="server" />
                                </td>
                                <td align="right">TOTAL:
                                    <asp:Label ID="lblSectionTotalP" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td><b>B.	Days Sick :</b>
                                    <asp:TextBox ID="txtDaysSick" runat="server"></asp:TextBox>
                                </td>
                                <td><b>Days Tardy :</b>
                                    <asp:TextBox ID="txtDaysTardy" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2"><b>C.	Comments / Notes :</b>
                                    <asp:TextBox ID="txtCommentsNNotes" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b>Evaluated By:</b>
                                    <asp:Label ID="lblEvaluatedBy" runat="server"></asp:Label>                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b>Approved By Manager:</b>
                                    <asp:Label ID="lblApprovedByManager" runat="server"></asp:Label>                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b>Approved By HR:</b>
                                    <asp:Label ID="lblApprovedByHR" runat="server"></asp:Label>                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b>Acknowledged By:</b>
                                    <asp:Label ID="lblAcknowledgeBy" runat="server"></asp:Label>                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <b>Date Evaulated:</b>
                                    <asp:Label ID="lblDateEvaluated" runat="server"></asp:Label>                                    
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>

                <div class="panel-footer text-center">
                    <asp:Button ID="btnUpdate"
                        runat="server"
                        Text="Update"
                        CssClass="btn btn-primary"
                        OnClick="btnUpdate_Click" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfEvaluationId" runat="server" />
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
