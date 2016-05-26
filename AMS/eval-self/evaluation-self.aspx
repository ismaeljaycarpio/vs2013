<%@ Page Title="Self Evaluation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="evaluation-self.aspx.cs" Inherits="AMS.eval_self.evaluation_self" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HyperLink ID="hlSelfEvaluationForm" runat="server" NavigateUrl="~/eval-self/evaluation-self-form.aspx" Text="Create Self Evaluation"></asp:HyperLink>
</asp:Content>
