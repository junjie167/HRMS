<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="AddSingleClaim.aspx.cs" Inherits="HRMS.AddSingleClaim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Add Single Claim
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        console.log($.fn.jquery);
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
            $("#<%= from_date.ClientID %>" ).datepicker({
                 showOn: 'button',
                 buttonImageOnly: true,
                 dateFormat: 'dd/mm/yy',
                 buttonImage: 'images/calendar.png',
                maxDate: '0'
            });
            $("#<%= from_date.ClientID %>").change(function (event) {
                console.log(event.target.id);
            });
        });
        
         $(function () {
             $("#<%= to_date.ClientID %>").datepicker({
                 showOn: 'button',
                 buttonImageOnly: true,
                 dateFormat: 'dd/mm/yy',
                 buttonImage: 'images/calendar.png',
                 maxDate: '0'
             });
         });

        $(document).ready(function () {
            $("#<%= FileUpload1.ClientID %>").change(function (e) {
                var data = this.id;
                var name = data.split("_");
                console.log(name);
                name[1] = "lbtnAtt";
                var lblAttname = "#" + name.join("_");
                var geekss = e.target.files[0].name;
                $(lblAttname).text(geekss);
            });
        });

        function openConfirmationModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#promptModal').modal('show');
        }

        function openSuccessModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#successModal').modal('show');
        }
        function closeModal() {
            $('#promptModal').modal('hide');
            $('#successModal').modal('hide');
            $('#emptyFieldModal').modal('hide');
            $('#errorModal').modal('hide');
        }
        function openEmptyFieldModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#emptyFieldModal').modal('show');
        }
        function openErrorModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#errorModal').modal('show');
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
    </script>
    <style>
        .ui-datepicker-trigger {
            height: 30px;
            width: 30px;
        }

        .dateinputformat {
            width: 87%;
            float: left;
        }

        td {
            padding: 12px;
        }

        .icon-success {
            margin-bottom: 20px;
            color: green;
            font-size: 120px;
        }

        .ackbtn {
            width: 100%;
        }

        .close {
            font-size: 40px;
        }

        .failed {
            color: red;
            font-size: 120px;
        }

        .blockclass {
            margin: auto;
            padding: 10px;
        }
        .container {
  display: flex;
  justify-content: center;
  align-items: center;
}
         input[type="file"] {
            width: 100px;
            color: transparent;
            display:inline-block;
        }
    </style>
    <div class="container-fluid mt-3">
        <%--Confirmation popup--%>
        <div class="modal fade modal" id="promptModal" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header text-center">
                        <h2 class="modal-title w-100">
                            <label class="control-label">Confirmation</label>
                        </h2>
                    </div>
                    <div class="modal-body">
                        <div class="container">
                            <div class="child">
                                <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblconfirm_claimtype" class="col-sm-2" Text="Claim Type: " runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="confirm_claimtype" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblconfirm_fromdate" class="col-sm-2" Text="From:" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="confrim_fromdate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblconfirm_todate" class="col-sm-2" Text="To: " runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="confirm_todate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblconfirm_description" class="col-sm-2" Text="Description: " runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="confirm_description" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblconfirm_amount" class="col-sm-2" Text="Amount :S$" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="confirm_amount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblconfirm_attachment" class="col-sm-2" Text="Attachment: " runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="confirm_attachment" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal_footer">
                        <div class="row m-1 text-center">
                            <div class="col-sm-12">
                                <asp:Button ID="btnCancel" runat="server" class="btn btn-secondary" OnClick="btnCancel_Click" Text="Cancel" CausesValidation="false" />
                                <asp:Button ID="btnConfirm" runat="server" class="btn btn-success" OnClick="btnConfirm_Click" Text="Confirm" CausesValidation="false" />
                            </div>
                        </div>
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
                        <h4>Single claim has been successfully added</h4>
                    </div>
                    <div class="modal_footer m-3">
                        <asp:Button ID="btnAcknowledge" runat="server" class="btn btn-secondary ackbtn" OnClick="btnAcknowledge_Click" Text="Back to Claim Summary" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>

        <%--Empty Field Error Message popup--%>
        <div class="modal fade modal" id="emptyFieldModal" data-keyboard="false" data-backdrop="static">
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
                                        <asp:Label ID="Label2" runat="server" Text="Please fill the Mandatory fields as shown below: "></asp:Label>
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
                        <asp:Button ID="Button1" runat="server" class="btn btn-secondary ackbtn" Text="Close" />
                    </div>
                </div>
            </div>
        </div>

        <%--Other Error Message popup--%>
        <div class="modal fade modal" id="errorModal" data-keyboard="false" data-backdrop="static">
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
                                    <h5>
                                    <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                                </h5>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <i class=" icon-checkmark3 position-right"></i>
                        <asp:Button ID="Button2" runat="server" class="btn btn-secondary ackbtn" Text="Close" />
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-xl-3 col-md-6 mb-4">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <h6>Meal Expenses Claims Remaining:</h6>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label ID="lblmeal" runat="server"></asp:Label>
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
                                    <asp:Label ID="lblmedical" runat="server"></asp:Label>
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
                                    <asp:Label ID="lblphone" runat="server"></asp:Label>
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
                                    <asp:Label ID="lbltransport" runat="server"></asp:Label>
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
            <legend style="width: auto; padding: 0 10px;" align="top">Single Claim details</legend>
            <br />
            <div class="row mb-3">
                <asp:Label ID="Ctype" class="col-sm-2 col-form-label" Text="Claim type: " runat="server">
                    <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                </asp:Label>
                <div class="col-sm-5">
                    <asp:DropDownList ID="claim_type" class="form-control" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row mb-3">
                <asp:Label ID="from_label" class="col-sm-2 col-form-label" Text="Date From: " runat="server">
                    <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                </asp:Label>
                <div class="col-sm-5">
                    <asp:TextBox ID="from_date" class="form-control dateinputformat" MaxLength="10" placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-3">
                <asp:Label ID="to_label" class="col-sm-2 col-form-label" Text="Date To: " runat="server">
                    <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                </asp:Label>
                <div class="col-sm-5">
                    <asp:TextBox ID="to_date" class="form-control dateinputformat" MaxLength="10" placeholder="dd/mm/yyyy" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-3">
                <asp:Label ID="descritpion_label" class="col-sm-2 col-form-label" Text="Description: " runat="server">
                      <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                </asp:Label>
                <div class="col-sm-5">
                    <asp:TextBox ID="claim_description" class="form-control" TextMode="multiline" placeholder="Description" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-3">
                <asp:Label ID="amount_label" class="col-sm-2 col-form-label" Text="Claim Amount: S$" runat="server">
                      <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                </asp:Label>
                <div class="col-sm-5">
                    <asp:TextBox ID="amount" class="form-control" runat="server" placeholder="Amount"  onkeypress="return isDigit(event,this.value);"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-3">
                <asp:Label ID="attachment_label" class="col-sm-2 col-form-label" Text="Attachment: " runat="server">
                     <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                </asp:Label>
                <div class="col-sm-5">
                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="Medium" class="form-control-file" />
                    <asp:LinkButton ID="lbtnAtt" OnClick="attachmentLinkbtn_Click" runat="server"></asp:LinkButton>
                </div>
            </div>
        </fieldset>
        <br />

        <div class="row text-center">
            <div class="col-sm-12">
                <asp:Button ID="back" runat="server" class="btn btn-secondary mb-2" Text="Clear" OnClick="back_Click" CausesValidation="false" />
                <asp:Button ID="submit" runat="server" class="btn btn-success mb-2" OnClick="submit_Click" Text="Submit" CausesValidation="true" />
            </div>
        </div>
    </div>



</asp:Content>
