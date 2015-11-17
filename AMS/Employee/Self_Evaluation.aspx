<%@ Page Title="Self Evaluation" Language="C#" MasterPageFile="~/ProfileNested.master" AutoEventWireup="true" CodeBehind="Self_Evaluation.aspx.cs" Inherits="AMS.Employee.Self_Evaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                            <td><b>Period Covered:</b>
                                <asp:TextBox ID="txtPeriodCovered" runat="server"></asp:TextBox></td>
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
                                        <asp:TextBox runat="server" ID="txtRating" Width="30" MaxLength="1"></asp:TextBox>
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
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
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
                                        <asp:TextBox runat="server" ID="txtRating" Width="30" MaxLength="1"></asp:TextBox>
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
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
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
                                        <asp:TextBox runat="server" ID="txtRating" Width="30" MaxLength="1"></asp:TextBox>
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
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
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
                                        <asp:TextBox runat="server" ID="txtRating" Width="30" MaxLength="1"></asp:TextBox>
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
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
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
                                        <asp:TextBox runat="server" ID="txtRating" Width="30" MaxLength="1"></asp:TextBox>
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
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="pagination" />
                        </asp:GridView>
                    </div>
                </div>


                <div class="panel-footer text-center">
                    <asp:Button ID="btnSumbit" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="btnSumbit_Click" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
