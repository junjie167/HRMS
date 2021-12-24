<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="EditMultipleClaim.aspx.cs" Inherits="HRMS.EditMultipleClaim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Edit Multiple Claims
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript">
        jQuery.browser = {};
        (function () {
            jQuery.browser.msie = false;
            jQuery.browser.version = 0;
            if (navigator.userAgent.match(/MSIE ([0-9]+)\./)) {
                jQuery.browser.msie = true;
                jQuery.browser.version = RegExp.$1;
            }
        })();
        $(function () {
            $("[id*=txtDateFrom]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                buttonImage: 'images/calendar.png',
                maxDate: '0'
            });
        });
        $(function () {
            $("[id*=txtDateTo]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                dateFormat: 'dd/mm/yy',
                buttonImage: 'images/calendar.png',
                 maxDate: '0'
            });
        });


        $(document).ready(function () {

            $("[id*=fulAtt]").change(function (e) {
                var data = this.id;
                var name = data.split("_");
                name[2] = "lbtnAtt";
                var lblAttname = "#" + name.join("_");
                var geekss = e.target.files[0].name;
                $(lblAttname).text(geekss);
            });
        });

        function openConfirmationModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#promptModal').modal('show');
        }

        function openErrorModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#errorModal').modal('show');
        }

        function openEmptyFieldModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#emptyFieldModal').modal('show');
        }
        function openSuccessModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#successModal').modal('show');
        }
        function isDigit(evt, txt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            var c = String.fromCharCode(charCode);

            if (txt.indexOf(c) > 0 && charCode == 46) {
                return false;
            }
            else if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            return true;
        }

        function openDateFromToModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#dateFromToModal').modal('show');
        }

        function handleInputKey(e) {
            if (!e) {
                var e = window.event;
            }
            if (((e.keyCode >= 48 && e.keyCode <= 57 || e.keyCode == 47) && (!e.shiftKey && !e.altKey && !e.ctrlKey))) {

                return true;
            }
            else {
                e.preventDefault();
                return false;
            }
        }

        document.getElementById('txtDateFrom').addEventListener("keypress", handleInputKey, false);
        document.getElementById('txtDateTo').addEventListener("keypress", handleInputKey, false);
    </script>
    <style>
        .ui-datepicker-trigger {
            height: 30px;
            width: 30px
        }

        input[type="file"] {
            width: 100px;
            color: transparent;
        }

        #lblAllAtt {
            color: transparent;
        }

        .auto-style1 {
            text-align: center;
        }

        .gvtable {
            border-collapse: collapse;
            border-spacing: 0;
            border: 1px solid #ddd;
        }

        th, td {
            text-align: left;
            padding: 16px;
        }

        .gvtable th {
            background-color: #f2f2f2;
        }

        .gvtable tr {
            background-color: #ecedf2;
        }

        .dateinputformat {
            display: inline-block;
            vertical-align: middle;
            width: 67%;
            margin: 0 5px 0 0;
        }

        .button-design {
            width: 100%;
        }

        .failed {
            color: red;
            font-size: 120px;
        }

        .icon-success {
            margin-bottom: 20px;
            color: green;
            font-size: 120px;
        }

        .blockclass {
            width: 50%;
            margin: auto;
            padding: 10px;
        }

        .float-container {
            padding: 10px;
        }

        .float-child {
            width: 50%;
            float: left;
            padding: 10px;
        }

        table.grid {
            border-collapse: collapse;
        }

            table.grid tr {
                border: 1px solid white;
                border-radius: 5px;
                background-color: lightgoldenrodyellow;
                border-width: 10px 0;
            }

        .container {
            display: flex;
            justify-content: center;
            align-items: center;
        }
    </style>
    <div class="container-fluid mt-3">
        <div class="row mb-3">
            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <h6>Meal Expenses Claims Remaining:</h6>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblMealEx" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <h6>Medical Claims Remaining:</h6>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblMedical" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <h6>Phone Allowance Claims Remaining:</h6>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblPA" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <h6>Transport Claims Remaining:</h6>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblTransport" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <fieldset style="border: 1px solid black; padding: 20px;">
            <legend style="width: auto; padding: 0 10px;" align="top">Multiple Claim details</legend>
            <br />
            <div class="row mb-3">
                <div class="col">
                    <div class="form-outline">
                        <asp:Label ID="lblgrouptitle" class="form-label" Text="Group Title: " runat="server">
                            <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                        </asp:Label>
                        <asp:TextBox ID="txtGroupTitle" class="form-control" placeholder="Title" runat="server"></asp:TextBox>
                    </div>
                </div>

            </div>
            <div class="row mb-5">
                <div class="col">
                    <div class="form-outline">
                        <asp:Label ID="lblfromdate" class="form-label" Text="Date From:" runat="server">
                            <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                        </asp:Label>
                        <asp:TextBox ID="txtDateFrom" class="form-control dateinputformat" runat="server" placeholder="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                    </div>
                </div>
                <div class="col">
                    <div class="form-outline">
                        <asp:Label ID="lbltodate" class="form-label" Text="Date To:" runat="server">
                            <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                        </asp:Label>
                        <asp:TextBox ID="txtDateTo" class="form-control dateinputformat" runat="server" placeholder="dd/mm/yyyy" MaxLength="10"></asp:TextBox>
                    </div>
                </div>
            </div>


            <div class="row mb-2">
                <div class="col-12 table-responsive">
                    <asp:GridView ID="gvMultiClaim" class="gvtable" runat="server" AutoGenerateColumns="False" Width="100%" Style="text-align: center">
                        <Columns>
                            <asp:TemplateField HeaderText="Claim Type">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlCliamType" runat="server">
                                        <asp:ListItem Text="Meal Expenses" Value="Meal Expenses"></asp:ListItem>
                                        <asp:ListItem Text="Medical" Value="Medical"></asp:ListItem>
                                        <asp:ListItem Text="Phone Allowance" Value="Phone Allowance"></asp:ListItem>
                                        <asp:ListItem Text="Transport" Value="Transport"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbDes" runat="server" Text='<%# Bind("description") %>' TextMode="MultiLine"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amt S$">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbAmt" runat="server" Text='<%# Bind("amt") %>' onkeypress="return isDigit(event,this.value);"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attachment">
                                <ItemTemplate>
                                    <input type="file" id="fulAtt" style="float: left;" runat="server" />
                                    <asp:LinkButton ID="lbtnAtt" runat="server" OnClick="lblAttOnClick" Text='<%# Bind("attachment") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDel" runat="server" ImageUrl="images/minus.png" Height="40px" Width="40px" OnClick="btnDel_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="mb-4 text-center">
                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="images/add.png" Height="40px" OnClick="btnAdd_Click" Width="40px" Style="text-align: center" />
            </div>
        </fieldset>
        <br />

        <div class="row mb-3">
            <div class="col-12">
                <asp:Button ID="btnSubmit" class="btn btn-success button-design" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </div>
        </div>

    </div>




    <div class="modal fade modal" id="promptModal" data-keyboard="false" data-backdrop="static">

        <%--<div class="modal-dialog modal-lg">--%>
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header text-center">
                    <%--<textarea id="ta_DeleteReason" cols="20" rows="2" runat="server"></textarea>--%>
                    <%--<button type="button" class="close" data-dismiss="modal" OnClick="btn_CancelMCQEdit_Click">&times;</button>--%>

                    <%--<asp:Button ID="btn_CloseMCQEdit" class="close unstyled-button" runat="server" OnClick="btn_CancelMCQEdit_Click" Text="X" />--%>
                    <h2 class="modal-title w-100">
                        <label class="control-label">Confirmation</label></h2>
                </div>
                <div class="modal-body">
                    <div class="row mb-3 table-responsive">
                        <table class="blockclass w-100">
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" class="col-sm-2 control-label" Text="Date From: " runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDateFrom" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label4" class="col-sm-2 control-label" Text="Date To: " runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDateTo" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" class="col-sm-2 control-label" Text="Title: " runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label7" class="col-sm-2 control-label" Text="Total Amt S$: " runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTAmt" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="row table-responsive">
                        <div class="col-12 ">
                            <asp:GridView ID="gv_Details" Width="100%" CssClass="grid" runat="server" AutoGenerateColumns="False" ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <div class="float-container">
                                                <div class="float-child">
                                                    <asp:Label ID="Label8" class="control-label" Text="Type: " runat="server"></asp:Label>
                                                    <asp:Label ID="lblType" runat="server" Text='<%# Bind("[claimType]") %>'></asp:Label>
                                                </div>
                                                <div class="float-child">
                                                    <asp:Label ID="Label6" class="control-label" Text="S$: " runat="server"></asp:Label>
                                                    <asp:Label ID="lblAmt" runat="server" Text='<%# Bind("[amt]") %>'></asp:Label>
                                                </div>
                                            </div>
                                            <div class="float-container">
                                                <div class="float-child">
                                                    <asp:Label ID="Label9" class="control-label" Text="Description:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("[description]") %>'></asp:Label>
                                                </div>
                                                <div class="float-child">
                                                    <asp:Label ID="Label10" class="control-label" Text="Attachment:" runat="server"></asp:Label>
                                                    <asp:Label ID="lblAtt" runat="server" Text='<%# Bind("[attachment]") %>'></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer text-center">
                    <div class="col-md-12">
                        <asp:Button ID="btnCancel" runat="server" class="btn btn-secondary" OnClick="btnCancel_Click" Text="Cancel" />
                        <asp:Button ID="btnConfirm" runat="server" class="btn btn-success" OnClick="btnConfirm_Click" Text="Confirm" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade modal" id="errorModal" data-keyboard="false" data-backdrop="static">

        <%--<div class="modal-dialog modal-lg">--%>
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header  text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Error</label></h2>
                </div>
                <div class="modal-body">
                    <div class="mb-2 text-center">
                        <i class="fa fa-times-circle failed"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4>
                                        <asp:Label ID="Label1" runat="server" Text="The following Claim Type Exceed the Quota Available:"></asp:Label>
                                    </h4>
                                </div>
                                <h5>
                                    <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                                </h5>
                            </div>

                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <i class=" icon-checkmark3 position-right"></i>
                    <asp:Button ID="Button2" runat="server" class="btn btn-secondary button-design" Text="Close" />
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade modal" id="emptyFieldModal" data-keyboard="false" data-backdrop="static">

        <%--<div class="modal-dialog modal-lg">--%>
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Error</label></h2>
                </div>
                <div class="modal-body">
                    <div class="mb-2 text-center">
                        <i class="fa fa-times-circle failed"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4>
                                        <asp:Label ID="Label2" runat="server" Text="Please Insert the Required Field: "></asp:Label>
                                    </h4>

                                </div>
                                <h5>
                                    <asp:Label ID="lblEmptyField" runat="server"></asp:Label>
                                </h5>
                            </div>

                        </div>

                    </div>

                </div>
                <div class="modal-footer">
                    <i class=" icon-checkmark3 position-right"></i>
                    <asp:Button ID="Button1" runat="server" class="btn btn-secondary button-design" Text="Close" />
                </div>
            </div>
        </div>
    </div>

    <%--Success Message popup--%>
    <div class="modal fade modal" id="successModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Confirmation</label>
                    </h2>
                </div>
                <div class="modal-body text-center">
                    <i class="fa fa-check-circle icon-success"></i>
                    <h4>Multiple claims has been successfully added</h4>
                </div>
                <div class="modal_footer m-3">
                    <asp:Button ID="btnAcknowledge" runat="server" class="btn btn-secondary button-design" OnClick="btnAcknowledge_Click" Text="Back to Claim Summary" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade modal" id="dateFromToModal" data-keyboard="false" data-backdrop="static">
        <%--<div class="modal-dialog modal-lg">--%>
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Error</label></h2>
                </div>
                <div class="modal-body">
                    <div class="mb-2 text-center">
                        <i class="fa fa-times-circle failed"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4>
                                        <asp:Label ID="Label12" runat="server" Text="Date To cannot be smaller than Date From"></asp:Label>
                                    </h4>
                                </div>

                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <i class=" icon-checkmark3 position-right"></i>
                    <asp:Button ID="Button4" runat="server" class="btn btn-secondary button-design" Text="Close" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
