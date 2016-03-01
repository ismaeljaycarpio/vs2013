<%@ Page Title="Pending Approvals"
    Language="C#"
    MasterPageFile="~/EvaluationNested.master"
    AutoEventWireup="true"
    CodeBehind="PendingApprovals.aspx.cs"
    Inherits="AMS.Evaluation.PendingApprovals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
<!--
    function Check_Click(objRef) {
        //Get the Row based on checkbox
        var row = objRef.parentNode.parentNode;

        //Get the reference of GridView
        var GridView = row.parentNode;

        //Get all input elements in Gridview
        var inputList = GridView.getElementsByTagName("input");

        for (var i = 0; i < inputList.length; i++) {
            //The First element is the Header Checkbox
            var headerCheckBox = inputList[0];

            //Based on all or none checkboxes
            //are checked check/uncheck Header Checkbox
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;
    }

    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    if (row.rowIndex % 2 == 0) {
                        //row.style.backgroundColor = "#C2D69B";
                    }
                    else {
                        //row.style.backgroundColor = "white";
                    }
                    inputList[i].checked = false;
                }
            }
        }
    }
    //-->
    </script>

    <script type="text/javascript">
        function confirmApprove() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=gvPendingApprovals.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("No records to approve.");
                return false;
            }
            else {
                return confirm("Do you want to approve " + count + " record/s? ");
            }
        }
    </script>

    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-warning">
                <div class="panel-heading">
                    <h5>Pending Evaluation Approvals</h5>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvPendingApprovals"
                            runat="server"
                            class="table table-striped table-hover dataTable"
                            GridLines="None"
                            EmptyDataText="No Pending Evaluation Approvals"
                            AutoGenerateColumns="false"
                            AllowPaging="true"
                            ShowHeaderWhenEmpty="true"
                            AllowSorting="true"
                            OnPageIndexChanging="gvPendingApprovals_PageIndexChanging"
                            OnSelectedIndexChanging="gvPendingApprovals_SelectedIndexChanging"
                            DataKeyNames="Id,UserId">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server"
                                            onclick="checkAll(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server"
                                            onclick="Check_Click(this)" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblFullName" runat="server" Text='<%# Eval("FullName") %>' CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DateEvaluated" HeaderText="DateEvaluated" />
                                <asp:TemplateField HeaderText="Manager Approval">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApprovalManager" runat="server" Text='<%# Guid.Parse(Eval("ApprovedByManagerId").ToString()) == Guid.Empty ? "Pending" : "Approved" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HR Approval">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHRApproval" runat="server" Text='<%# Guid.Parse(Eval("ApprovedByHRId").ToString()) == Guid.Empty ? "Pending" : "Approved" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                        <asp:Button ID="btnApprove"
                            runat="server"
                            CssClass="btn btn-default"
                            OnClientClick="return confirmApprove();"
                            Text="Approve"
                            OnClick="btnApprove_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
</asp:Content>
