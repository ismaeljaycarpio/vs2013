<%@ Page Title="Database Backup" Language="C#" MasterPageFile="~/MasterConfigNested.master" AutoEventWireup="true" CodeBehind="BackupDatabase.aspx.cs" Inherits="AMS.MasterConfig.BackupDatabase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnBackup" runat="server" Text="Backup" OnClick="btnBackup_Click"/>
    <asp:Label ID="lbl" runat="server"></asp:Label>
</asp:Content>
