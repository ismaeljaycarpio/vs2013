<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AMS.Login" %>

<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-10">
            <div class="panel panel-default">
                <div class="panel-body">
                        <asp:Login runat="server" ViewStateMode="Disabled" RenderOuterTable="false" OnLoggedIn="Unnamed_LoggedIn">
                            <LayoutTemplate>
                                <p class="validation-summary-errors">
                                    <asp:Literal runat="server" ID="FailureText" />
                                    <div class="col-sm-10"></div>
                                </p>
                                <br />
                                <div class="form">
                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="UserName">User name</asp:Label>
                                        <asp:TextBox runat="server" ID="UserName" CssClass="form-control" />
                                        <asp:RequiredFieldValidator 
                                            runat="server" 
                                            ControlToValidate="UserName" 
                                            CssClass="label label-danger" ErrorMessage="The user name field is required." />
                                    </div>

                                    <div class="form-group">
                                        <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
                                        <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                                        <asp:RequiredFieldValidator runat="server" 
                                            ControlToValidate="Password" 
                                            CssClass="label label-danger" 
                                            ErrorMessage="The password field is required." />
                                    </div>
                                    <div class="form-group">
                                        <asp:CheckBox runat="server" ID="RememberMe" />
                                        <asp:Label runat="server" AssociatedControlID="RememberMe" CssClass="checkbox">Remember me?</asp:Label>
                                    </div>
                                    <asp:Button runat="server" CommandName="Login" Text="Log in" CssClass="btn btn-primary" />
                                </div>
                            </LayoutTemplate>
                        </asp:Login>
                        <%--<p>
            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Register</asp:HyperLink>
            if you don't have an account.
        </p>--%>

                </div>
            </div>
        </div>
    </div>
    <%--<section id="socialLoginForm">
        <h2>Use another service to log in.</h2>
        <uc:OpenAuthProviders runat="server" ID="OpenAuthLogin" />
    </section>--%>
</asp:Content>
