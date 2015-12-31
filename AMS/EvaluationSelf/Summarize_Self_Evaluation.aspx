<%@ Page Title="Summarize Self Evaluation"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="Summarize_Self_Evaluation.aspx.cs"
    Inherits="AMS.EvaluationSelf.Summarize_Self_Evaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h5>Summarize Self Evaluation</h5>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-6">
                                <div class="input-group">
                                    <span class="input-group-btn">
                                        <asp:Button ID="btnSearch"
                                            runat="server"
                                            CssClass="btn btn-primary"
                                            Text="Go"
                                            OnClick="btnSearch_Click" />
                                    </span>
                                    <asp:TextBox ID="txtStartDate" runat="server" data-provide="datepicker" CssClass="form-control" placeholder="Start Date"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Self Evaluation Form</h5>
                </div>

                <div class="panel-body">
                    <table class="table table-responsive">
                        <tr class="text-center">
                            <td colspan="3"><b>ARBa SCORE SHEET</b></td>
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
                    </table>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvSocialSkills"
                            runat="server"
                            class="table table-striped table-hover"
                            GridLines="None"
                            OnDataBound="gvSocialSkills_DataBound"
                            ShowHeaderWhenEmpty="true"
                            EmptyDataText="No Record(s) found"
                            AutoGenerateColumns="false"
                            DataKeyNames="Id">
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="EvaluatedBy">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Social Skills">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQ" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Score">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRating" Width="30" MaxLength="1" Text='<%# Eval("Rating") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server"
                                            Display="Dynamic"
                                            ControlToValidate="txtRating"
                                            ForeColor="Red"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator
                                            ID="RangeValidator1"
                                            runat="server"
                                            ForeColor="Red"
                                            MinimumValue="1"
                                            MaximumValue="3"
                                            Type="Integer"
                                            Display="Dynamic"
                                            ControlToValidate="txtRating"
                                            ErrorMessage="(1-3) only"></asp:RangeValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="pagination" />
                        </asp:GridView>

                        <hr />
                        <asp:GridView ID="gvCustomerService"
                            runat="server"
                            class="table table-striped table-hover"
                            GridLines="None"
                            ShowHeaderWhenEmpty="true"
                            AutoGenerateColumns="false"
                            DataKeyNames="Id">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="EvaluatedBy">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Customer Service">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQ" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Score">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRating" Width="30" MaxLength="1" Text='<%# Eval("Rating") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server"
                                            Display="Dynamic"
                                            ControlToValidate="txtRating"
                                            ForeColor="Red"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator
                                            ID="RangeValidator1"
                                            runat="server"
                                            ForeColor="Red"
                                            MinimumValue="1"
                                            MaximumValue="3"
                                            Type="Integer"
                                            Display="Dynamic"
                                            ControlToValidate="txtRating"
                                            ErrorMessage="(1-3) only"></asp:RangeValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="pagination" />
                        </asp:GridView>

                        <hr />

                        <asp:GridView ID="gvOriginality"
                            runat="server"
                            class="table table-striped table-hover"
                            GridLines="None"
                            ShowHeaderWhenEmpty="true"
                            AutoGenerateColumns="false"
                            DataKeyNames="Id">
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="EvaluatedBy">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Originality and Innovativeness">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQ" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Score">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRating" Width="30" MaxLength="1" Text='<%# Eval("Rating") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server"
                                            Display="Dynamic"
                                            ControlToValidate="txtRating"
                                            ForeColor="Red"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator
                                            ID="RangeValidator1"
                                            runat="server"
                                            ForeColor="Red"
                                            MinimumValue="1"
                                            MaximumValue="3"
                                            Type="Integer"
                                            Display="Dynamic"
                                            ControlToValidate="txtRating"
                                            ErrorMessage="(1-3) only"></asp:RangeValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="pagination" />
                        </asp:GridView>

                        <hr />

                        <asp:GridView ID="gvResponsibility"
                            runat="server"
                            class="table table-striped table-hover"
                            GridLines="None"
                            ShowHeaderWhenEmpty="true"
                            AutoGenerateColumns="false"
                            DataKeyNames="Id">
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="EvaluatedBy">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Responsibility and Accountability">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQ" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Score">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRating" Width="30" MaxLength="1" Text='<%# Eval("Rating") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server"
                                            Display="Dynamic"
                                            ControlToValidate="txtRating"
                                            ForeColor="Red"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator
                                            ID="RangeValidator1"
                                            runat="server"
                                            ForeColor="Red"
                                            MinimumValue="1"
                                            MaximumValue="3"
                                            Type="Integer"
                                            Display="Dynamic"
                                            ControlToValidate="txtRating"
                                            ErrorMessage="(1-3) only"></asp:RangeValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="pagination" />
                        </asp:GridView>

                        <hr />
                        <asp:GridView ID="gvExcellent"
                            runat="server"
                            class="table table-striped table-hover"
                            GridLines="None"
                            ShowHeaderWhenEmpty="true"
                            AutoGenerateColumns="false"
                            DataKeyNames="Id">
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="EvaluatedBy">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Excellent Job Knowledge">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQ" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Score">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" ID="txtRating" Width="30" MaxLength="1" Text='<%# Eval("Rating") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                            runat="server"
                                            Display="Dynamic"
                                            ControlToValidate="txtRating"
                                            ForeColor="Red"
                                            ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator
                                            ID="RangeValidator1"
                                            runat="server"
                                            ForeColor="Red"
                                            MinimumValue="1"
                                            MaximumValue="3"
                                            Type="Integer"
                                            Display="Dynamic"
                                            ControlToValidate="txtRating"
                                            ErrorMessage="(1-3) only"></asp:RangeValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control" Text='<%# Eval("Remarks") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="pagination" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
