<%@ Page Title="Colleague Evaluation Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="evaluation-colleague-form.aspx.cs" Inherits="AMS.eval_colleague.evaluation_colleague_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h5>Colleague Evaluation Form</h5>
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
