<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="vEvaluation_Self.aspx.cs" Inherits="AMS.EvaluationSelf.vEvaluation_Self" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Self Evaluation Form</h5>
                </div>

                <div class="panel-body">
                    <table class="table">
                        <tr class="text-center">
                            <td colspan="2"><b>ARBa SCORE SHEET</b></td>
                        </tr>
                        <tr>
                            <td><b>Employee Name:</b>
                                <asp:Label ID="lblEmpName" runat="server"></asp:Label></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td><b>Designation</b>
                                <asp:Label ID="lblDesignation" runat="server"></asp:Label></td>
                            <td><b>Start Date of Employment</b>
                                <asp:TextBox ID="txtHiredDate" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </div>

                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvSocialSkills"
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
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
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


                <div class="panel-footer text-center">
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
