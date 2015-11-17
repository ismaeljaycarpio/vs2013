<%@ Page Title="Performance Evaluation Report" 
    Language="C#" 
    MasterPageFile="~/ProfileNested.master" 
    AutoEventWireup="true" 
    CodeBehind="pPerformanceEvaluation.aspx.cs" 
    Inherits="AMS.Employee.pPerformanceEvaluation" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-10">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="794px" Font-Names="Verdana" Font-Size="8pt" Height="906px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                <LocalReport ReportPath="Employee\Report2.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </div>
    <asp:HiddenField ID="hfUserId" runat="server" />
</asp:Content>
