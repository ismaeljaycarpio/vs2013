<%@ Page Title="Print Leave Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="print-leave-form.aspx.cs" Inherits="AMS.Leave.print_leave_form" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12 col-md-offset-2">
            <rsweb:ReportViewer ID="ReportViewer1"
                runat="server"
                Height="800"
                KeepSessionAlive="true"
                SizeToReportContent="true">
            </rsweb:ReportViewer>
        </div>
    </div>
</asp:Content>
