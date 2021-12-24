<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ManagerViewClaims.aspx.cs" Inherits="HRMS.ManagerViewClaims" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    View Claim
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script>
        $(function () {
            $('[id*=groupGV]').footable();
        });
        function openAttachmentModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#AttachmentModal').modal('show');
        }
        function openRejectModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#RejectModal').modal('show');
        }
        function openRejectConfrimModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#RejectConfirmModal').modal('show');
        }
        function openRejectSuccessModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#successRejectModal').modal('show');
        }
        function openWarningApproveModalModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#warningApproveModal').modal('show');
        }
        function openSuccessApproveModalModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#successApproveModal').modal('show');
        }
    </script>
    <style>
        table td {
            padding-bottom: 20px;
        }

        .attachment_img {
            max-width: 250%;
            height: auto;
        }
        .groupattachment_img {
            max-width: 100%;
            height: auto;
        }

        .blockclass {
            width: 50%;
            margin: auto;
            padding: 10px;
        }

        .sg {
            border-collapse: collapse;
            border-spacing: 0;
            width: 100%;
            border: 1px solid #ddd;
        }

            .sg th, .sg td {
                text-align: left;
                padding: 16px;
            }

            .sg tr:nth-child(even) {
                background-color: #f2f2f2;
            }
                  .container {
  display: flex;
  justify-content: center;
  align-items: center;
}
                  .label_bold{
                      font-weight:bold;
                  }
                  .icon-success {
            margin-bottom: 20px;
            color: green;
            font-size: 120px;
        }

        .ackbtn {
            width: 100%;
        }
        .icon-warning{
            margin-bottom: 20px;
            color: orange;
            font-size: 120px;
        }
    </style>
    <div class="container-fluid">

          <%--Reject popup--%>
    <div class="modal fade modal" id="RejectModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title w-100 text-center">
                        <label class="control-label">Reason for Rejection</label>
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:RequiredFieldValidator ID="reqRejectMsg" ControlToValidate="rejectTxtbox" runat="server" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator> 
                   <asp:TextBox ID="rejectTxtbox" class="form-control w-100" runat="server" TextMode="MultiLine" placeholder="Type your message here"></asp:TextBox>
                </div>
                <div class="modal_footer mb-3 text-center">
                       <asp:Button ID="closeReject" Text="Close" class="btn btn-secondary" CausesValidation="false" runat="server" />
                    <asp:Button ID="submitReject" Text="Submit" OnClick="submitReject_Click" class="btn btn-success" CausesValidation="true" runat="server" />
                </div>
            </div>
        </div>
    </div>

               <%--Reject confirmation popup--%>
    <div class="modal fade modal" id="RejectConfirmModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title w-100 text-center">
                        <label class="control-label">Confirmation</label>
                    </h2>
                </div>
                <div class="modal-body">
                    <div class="container">
                        <div class="child">
                            <asp:Label ID="lblconfirmReject" class="label_bold" Text="Reason for Rejection: " runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="confirmReject" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="modal_footer mb-3 text-center">
                       <asp:Button ID="ConfirmRejectClose" Text="Close" class="btn btn-secondary" runat="server" />
                    <asp:Button ID="confirmRejectBtn" Text="Confirm" OnClick="confirmRejectBtn_Click" class="btn btn-success" runat="server" />
                </div>
            </div>
        </div>
    </div>

        <%--Success reject popup--%>
         <div class="modal fade modal" id="successRejectModal" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

                <div class="modal-content">
                    <div class="modal-header text-center">
                        <h2 class="modal-title w-100">
                            <label class="control-label">Confirmation</label>
                        </h2>
                    </div>
                    <div class="modal-body text-center">
                        <h4>This claim has been rejected succcessfully</h4>
                        <i class="fa fa-check-circle icon-success"></i>
                    </div>
                    <div class="modal_footer m-3">
                        <asp:Button ID="btnAcknowledgeReject" runat="server" class="btn btn-secondary ackbtn" OnClick="btnAcknowledgeReject_Click"  Text="Back to Review Claims Summary" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>

        <%--Warning approve popup--%>
         <div class="modal fade modal" id="warningApproveModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Confirmation</label>
                    </h2>
                </div>
                <div class="modal-body text-center">
                   <i class="fa fa-exclamation-circle icon-warning"></i>
                    <h4>Are you sure, to approve this claim ? This process cannot be undone.</h4>
                </div>
                <div class="modal_footer text-center">
                    <div class="row m-3">
                        <div class="col-sm-12 text-center">
                            <asp:Button ID="closeSingle" runat="server" class="btn btn-secondary" Text="Close" CausesValidation="false" />
                            <asp:Button ID="btnConfirm" runat="server" class="btn btn-success" Text="Approve" OnClick="btnConfirm_Click" CausesValidation="false"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

        <%--Success approved popup--%>
         <div class="modal fade modal" id="successApproveModal" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

                <div class="modal-content">
                    <div class="modal-header text-center">
                        <h2 class="modal-title w-100">
                            <label class="control-label">Confirmation</label>
                        </h2>
                    </div>
                    <div class="modal-body text-center">
                        <h4>This claim has been approved succcessfully</h4>
                        <i class="fa fa-check-circle icon-success"></i>
                    </div>
                    <div class="modal_footer m-3">
                        <asp:Button ID="btnApproveSuccessful" runat="server" class="btn btn-secondary ackbtn" OnClick="btnAcknowledgeReject_Click"  Text="Back to Review Claims Summary" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>



        <div class="row mt-3 mb-3">
            <div class="col-12">
                <asp:Panel ID="singlePanel" Visible="false" runat="server">
                    <div class="card shadow mb-4">
                        <div class="card-header text-center">
                            <h2>
                                <asp:Label ID="lblclaimID" runat="server"></asp:Label>
                            </h2>
                        </div>
                        <div class="card-body">
                            <div class="container">
                                <div class="child">
                                        <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblclaimoption" class="label_bold" Text="Claim Option: " runat="server"></asp:Label>
                                <asp:Label ID="claimoption" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblclaimtyple" class="label_bold"  Text="Claim Type: " runat="server"></asp:Label>
                                <asp:Label ID="claimType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblfromdate" class="label_bold"  Text="Date: " runat="server"></asp:Label>
                                <asp:Label ID="fromdate" runat="server"></asp:Label> 
                                &nbsp; to &nbsp;
                                <asp:Label ID="todate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbldescription" class="label_bold"  Text="Description: " runat="server"></asp:Label>
                                <asp:Label ID="description" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblamt" class="label_bold"  Text="Total Amount S$: " runat="server"></asp:Label>
                                <asp:Label ID="amount" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td>
                                <asp:Label ID="lblAttactment" class="label_bold"  Text="Attachment: " runat="server"></asp:Label>
                                <asp:LinkButton ID="attachment" OnClick="attachment_Click" runat="server"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>

                                </div>
                            </div>
                           
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="groupPanel" Visible="false" runat="server">
                    <div class="card shadow mb-4">
                        <div class="card-header text-center">
                            <h2>
                                <asp:Label ID="lblgroupClaimID" runat="server"></asp:Label>
                            </h2>
                        </div>
                        <div class="card-body">
                                       <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblgroupClaimoption" class="label_bold" Text="Claim Option: " runat="server"></asp:Label>
                                <asp:Label ID="groupclaimOption" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbltitle" class="label_bold"  Text="Group Title: " runat="server"></asp:Label>
                                <asp:Label ID="title" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblGroupfromdate" class="label_bold"  Text="Date: " runat="server"></asp:Label>
                                <asp:Label ID="groupFromDate" runat="server"></asp:Label>
                                &nbsp; to &nbsp;
                                <asp:Label ID="groupToDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                            <asp:GridView ID="groupGV" AutoGenerateColumns="false" class="sg footable" runat="server">
                        <Columns>
                            <asp:TemplateField HeaderText="Claim Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblgroupclaimType" runat="server" Text='<%# Eval("claimType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblgroupdescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount S$">
                                <ItemTemplate>
                                    <asp:Label ID="lblgroupclaimAmt" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attachment">
                                <ItemTemplate>
                                    <asp:LinkButton ID="viewGroupAttachment" Text='<%# Eval("Attachment") %>' OnClick="viewGroupAttachment_Click" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="row mb-2 text-center">
            <div class="col-xl-12 mb-2 w-100">
                <asp:Button ID="btnReject" class="btn btn-danger" OnClick="btnReject_Click" CausesValidation="false" Text="Reject" runat="server" />
                <asp:Button ID="btnApprove" class="btn btn-success" Text="Approve" OnClick="btnApprove_Click" CausesValidation="false" runat="server" />
                <asp:Button ID="btnBackView" class="btn btn-secondary" Visible="false" Text="Back to Review Claims Summary" OnClick="btnBackView_Click" CausesValidation="false" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
