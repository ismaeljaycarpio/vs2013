<%@ Page Title="Leave Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Leave.aspx.cs" Inherits="AMS.Leave.Leave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <ul class="nav nav-tabs" id="myTab">
                <li class="active"><a href="#pendingTab" data-toggle="tab">Pending Leave Approvals</a></li>
                <li><a href="#approvedTab" data-toggle="tab">Approved Leaves</a></li>
                <li><a href="#cancelledTab" data-toggle="tab">Cancelled Leaves</a></li>
                <li><a href="#rejectTab" data-toggle="tab">Rejected Leave Approvals</a></li>
            </ul>

            <div id="myTabContent" class="tab-content">
                <div class="tab-pane fade" id="activeTab">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h5 class="panel-title">Pending Leave Approvals</h5>
                        </div>
                        <div class="panel-body">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
