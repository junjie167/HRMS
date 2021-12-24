<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ClaimSummary.aspx.cs" Inherits="HRMS.ClaimSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Claim Summary
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script>
        $(function () {
            $('[id*=gvSingleClaim]').footable();
        });
        function openConfirmationModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#promptModal').modal('show');
        }
        function closeModal() {
            $('#promptModal').modal('hide');
            $('#deleteModal').modal('hide');
            $('#deleteGroupModal').modal('hide');
        }
        function deleteConfirmationModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#deleteModal').modal('show');
        }
        function openSuccessModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#successModal').modal('show');
        }
        function deleteGroupConfirmationModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#deleteGroupModal').modal('show');
        }
        function opensingleRejectModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#singleRejectModal').modal('show');
        }
        function opengroupRejectModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#groupRejectModal').modal('show');
        }
    </script>
    <style type="text/css">

.column {
  flex: 50%;
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

.sg .b td{
    padding-left:10px
}

.sg tr:nth-child(even) {
  background-color: #f2f2f2;
}
.topmargin{
    margin-top:20px;
}
.btnApplyDesign{
    width:100%;
}
.popupApplyBtn{
     width:100%;
     font-size:2em;
     background-color: #F28C28;
     color:white;
}
.popup-table{
    border:none;
}
.icon-warning{
            margin-bottom: 20px;
            color: orange;
            font-size: 120px;
        }
.close{
    font-size:40px;
}
.ackbtn{
            width:100%;
        }
.icon-success{
            margin-bottom: 20px;
            color: green;
            font-size: 120px;
        }
 .headerRow{
            border: 1px solid black;
            border-radius:25px;
        }
         table.grid {
            border-collapse: collapse;
        }

            table.grid tr.space {
                border: 1px solid white;
                border-radius: 5px;
                border-width: 5px 0;
                border-top: 1px solid black;
            }
            th{
             padding: 5px;
            }
             .float-container {
                  width:100%;
            margin:auto;
            padding: 10px;
        }

        .float-child {

            width: 25%;
            float: left;
            padding: 10px;
        }
          .middle{
                width:26%;
            }
            .left{
                width:24%;
            }
            .apply_button{
                background-color:#F28C28;
                color:white;
            }
            .viewbtn{
                 background-color:#F28C28;
                color:white;
            }

            .help-tip{
                position:relative;
                bottom:5px;
                left:5px;
    text-align: center;
    background-color: #F28C28;
    border-radius: 50%;
    width: 24px;
    height: 24px;
    font-size: 14px;
    line-height: 26px;
    cursor: default;
}

.help-tip:before{
    content:'?';
    font-weight: bold;
    color:#fff;
}

.help-tip:hover .tip-border{
    display:block;
    transform-origin: 100% 0%;

    -webkit-animation: fadeIn 0.3s ease-in-out;
    animation: fadeIn 0.3s ease-in-out;

}

.help-tip .tip-border{    /* The tooltip */
    z-index:1;
    display: none;
    text-align: left;
    background-color: #1E2021;
    padding: 20px;
    width: 300px;
    position: absolute;
    border-radius: 3px;
    box-shadow: 1px 1px 1px rgba(0, 0, 0, 0.2);
    right: -276px;
    color: #FFF;
    font-size: 13px;
    line-height: 1.4;
}

.help-tip .tip-border:before{ /* The pointer of the tooltip */
    position: absolute;
    content: '';
    width:0;
    height: 0;
    border:6px solid transparent;
    border-bottom-color:#1E2021;
    right:282px;
    top:-12px;
}

.help-tip .tip-border:after{ /* Prevents the tooltip from being hidden */
    width:100%;
    height:40px;
    content:'';
    position: absolute;
    top:-40px;
    left:0;
}

/* CSS animation */

@-webkit-keyframes fadeIn {
    0% { 
        opacity:0; 
        transform: scale(0.6);
    }

    100% {
        opacity:100%;
        transform: scale(1);
    }
}

@keyframes fadeIn {
    0% { opacity:0; }
    100% { opacity:100%; }
}
.box {
  float: left;
  height: 20px;
  width: 20px;
  margin-bottom: 15px;
  border: 1px solid black;
  clear: both;
}


</style>
<div class="container-fluid mt-3">
    <div class="modal fade modal" id="promptModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Choose Claim Option</h2>
                    <button type="button" class="close" onclick="closeModal()" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-6 p-1">
                             <asp:Button ID="applySingle" class="btn  popupApplyBtn" Text="Single Claim" OnClick="applySingle_Click" runat="server" />
                        </div>
                        <div class="col-sm-6 p-1">
                             <asp:Button ID="applyMultiple" class="btn popupApplyBtn" Text="Multiple Claims" OnClick="applyMultiple_Click" runat="server" />
                        </div>
                    </div>
 
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade modal" id="deleteModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Confirmation</label>
                    </h2>
                </div>
                <div class="modal-body text-center">
                   <i class="fa fa-exclamation-circle icon-warning"></i>
                    <h4>Are you sure, to delete this claim ? This process cannot be undone.</h4>
                </div>
                <div class="modal_footer text-center">
                    <div class="row m-3">
                        <div class="col-sm-12 text-center">
                            <asp:Button ID="closeSingle" runat="server" class="btn btn-secondary" Text="Close" />
                            <asp:Button ID="btnConfirm" runat="server" class="btn btn-danger" Text="Delete" OnClick="btnConfirm_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>





     <div class="modal fade modal" id="deleteGroupModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Confirmation</label>
                    </h2>
                </div>
                <div class="modal-body text-center">
                   <i class="fa fa-exclamation-circle icon-warning"></i>
                    <h4>Are you sure, to delete this claim ? This process cannot be undone.</h4>
                </div>
                <div class="modal_footer text-center">
                    <div class="row m-3">
                        <div class="col-sm-12 text-center">
                            <asp:Button ID="close" runat="server" class="btn btn-secondary" Text="Close" />
                            <asp:Button ID="btnConfirmDelete" runat="server" class="btn btn-danger" Text="Delete" OnClick="btnConfirmDelete_Click"/>
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
                   <h3>Claim has been deleted successfully</h3>
                    <i class="fa fa-check-circle icon-success"></i>
                </div>
                <div class="modal_footer m-3">
                    <asp:Button ID="btnAcknowledge" runat="server" class="btn btn-secondary ackbtn" OnClick="btnAcknowledge_Click"  Text="Ok" CausesValidation="false"/>
                </div>
            </div>
        </div>
    </div>

     <%--Single Reject Message popup--%>
     <div class="modal fade modal" id="singleRejectModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Reason for Rejection</label>
                    </h2>
                </div>
                <div class="modal-body text-center">
                   <h5>
                       <asp:Label ID="singleRejectReason" runat="server"></asp:Label>
                   </h5>
                </div>
                <div class="modal_footer m-3">
                    <asp:Button ID="Button1" runat="server" class="btn btn-secondary ackbtn" OnClick="btnAcknowledge_Click"  Text="Close" CausesValidation="false"/>
                </div>
            </div>
        </div>
    </div>


     <%--Group Reject Message popup--%>
     <div class="modal fade modal" id="groupRejectModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Reason for Rejection</label>
                    </h2>
                </div>
                <div class="modal-body text-center">
                   <h5>
                       <asp:Label ID="groupRejectReason" runat="server"></asp:Label>
                   </h5>
                </div>
                <div class="modal_footer m-3">
                    <asp:Button ID="Button2" runat="server" class="btn btn-secondary ackbtn" OnClick="btnAcknowledge_Click" Text="Close" CausesValidation="false"/>
                </div>
            </div>
        </div>
    </div>


<div class="row">
  <div class="col-xl-6 col-md-6 mb-4">
      <div class="card border-left-primary shadow h-100 py-2">
          <div class="card-body">
              <div class="row no-gutters align-items-center">
                  <div class="col mr-2">
                      <p>Total Claim Remaining:</p>
                      <div class="h5 mb-0 font-weight-bold text-gray-800">
                          <asp:Label ID="lbltotalclaim" runat="server"></asp:Label>
                      </div>
                  </div>
                  <div class="col-auto">
                      <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                  </div>
              </div>
          </div>
  </div>
  </div>
 <div class="col-xl-6 col-md-6 mb-4">
      <div class="card border-left-success shadow h-100 py-2">
          <div class="card-body">
              <div class="row no-gutters align-items-center">
                  <div class="col mr-2">
                      <p>Total Approval Claims:</p>
                      <div class="h5 mb-0 font-weight-bold text-gray-800">
                          <asp:Label ID="lbltotalAvailable" runat="server"></asp:Label>
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
<div class="row topmargin">
    <div class="col-12">
        <asp:Button ID="btnApplyClaim" class="btn apply_button btnApplyDesign" Text="Apply Claims" OnClick="btnApplyClaim_Click" runat="server" />
    </div>
</div>

 <%--Single Claim--%>
<div class="row topmargin mb-2">
    <div class="col-8">
        <h4>My Single Claims</h4>
    </div>
    <div class="col-4 text-right">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblsingleFilter" Text="Filter by: " runat="server"></asp:Label>
                </td>
                <td class="w-75">
                    <asp:DropDownList ID="singleFilter" class="form-control" OnSelectedIndexChanged="singleFilter_SelectedIndexChanged" runat="server" AutoPostBack="true">
        <asp:ListItem Text="All" Value="All"></asp:ListItem>
        <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
        <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
        <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
</div>
    <div class="row mb-3">
        <div class="col-12">
            <h3 class="text-center w-100">
                <asp:Label ID="nosingleclaim" Text="Currently No Single Claim to view" Visible="false" runat="server"></asp:Label>
            </h3>
            <h3 class="text-center w-100">
                <asp:Label ID="noSingleAprove" Text="Currently No Approved Single Claim to view" Visible="false" runat="server"></asp:Label>
            </h3>
            <h3 class="text-center w-100">
                <asp:Label ID="noPending" Text="Currently No Pending Single Claim to view" Visible="false" runat="server"></asp:Label>
            </h3>
            <h3 class="text-center w-100">
                <asp:Label ID="norejectedsingle" Text="Currently No Rejected Single Claim to view" Visible="false" runat="server"></asp:Label>
            </h3>
                     <asp:GridView ID="gvSingleClaim" class="sg" runat="server" AutoGenerateColumns="false" CssClass="sg footable" GridLines="Horizontal" AllowPaging="true" PageSize="4" OnPageIndexChanging="gvSingleClaim_PageIndexChanging" EnableSortingAndPagingCallbacks="false">
            <Columns>
                <asp:TemplateField HeaderText="Claim ID">
                    <ItemTemplate>
                        <asp:Label ID="lblclaimID" runat="server" Text='<%# Eval("claimID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Claim Type">
                    <ItemTemplate>
                        <asp:Label ID="lblclaimType" runat="server" Text='<%# Eval("claimType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lbldescription" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Amount S$">
                    <ItemTemplate>
                        <asp:Label ID="lblclaimAmt" runat="server" Text='<%# Eval("claimAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblclaimStatus" runat="server" Text='<%# Eval("claimStatus") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-CssClass="col-xl-3 text-right">
                    <ItemTemplate>
                        <asp:Button ID="singleupdatebtn" Text="Update" class="btn btn-success" runat="server" OnClick="SingleUpdate_Click" />
                        <asp:Button  Text="Cancel" ID="c" class="btn btn-danger" runat="server" OnClick="SingleDelete_Click1" />
                        <asp:Button ID="viewSingleReject" class="btn viewbtn" runat="server" OnClick="viewSingleReject_Click" Text="View Reason" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
            
        </div>
    </div>

     <%--Group Claims--%>
    <div class="row mb-2 mt-4">
        <div class="col-8">
            <table>
                <tr>
                    <td>
                        <h4>My Group Claims</h4>
                    </td>
                    <td>
                        <div class="help-tip">
                            <div class="tip-border">
                             <div style="margin-bottom:17px;"><div class="box bg-success"></div> : Approved</div>
                             <div style="margin-bottom:17px;"><div class="box bg-danger"></div> : Rejected</div>
                            <div><div class="box bg-warning"></div> : Pending</div>
                        </div>
                        </div>
                    </td>
                </tr>
            </table>

        </div>
    <div class="col-4 text-right">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblGroupFilter" Text="Filter by:" runat="server"></asp:Label>
                </td>
                <td class="w-75">
                            
        <asp:DropDownList ID="groupFilter" class="form-control" OnSelectedIndexChanged="groupFilter_SelectedIndexChanged" AutoPostBack="true" runat="server">
            <asp:ListItem Text="All" Value="All"></asp:ListItem>
            <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
            <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
            <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
        </asp:DropDownList>
                </td>
            </tr>
        </table>

    </div>
</div>
    <div class="row mb-2">
        <div class="col-12">
             <h3 class="text-center w-100">
                <asp:Label ID="nogroupclaim" Text="Currently No Group Claim to view" Visible="false" runat="server"></asp:Label>
            </h3>
            <h3 class="text-center w-100">
                <asp:Label ID="noGroupApprove" Text="Currently No Approved Group Claim to view" Visible="false" runat="server"></asp:Label>
            </h3>
            <h3 class="text-center w-100">
                <asp:Label ID="noGroupPending" Text="Currently No Pending Group Claim to view" Visible="false" runat="server"></asp:Label>
            </h3>
            <h3 class="text-center w-100">
                <asp:Label ID="noGroupReject" Text="Currently No Rejected Group Claim to view" Visible="false" runat="server"></asp:Label>
            </h3>
          
            <asp:GridView ID="gvGroupClaim" CssClass="table-responsive" runat="server" AutoGenerateColumns="False" ShowHeader="False" GridLines="None" AllowPaging="true" PageSize="4" OnPageIndexChanging="gvGroupClaim_PageIndexChanging" EnableSortingAndPagingCallbacks="false" class="col-xl-12">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
               <div id="groupgrid" runat="server">
                <div class="card-body">
                    <table class="col-xl-12 grid">
                        <tr>

                            <th class="col-xl-3">
                                <asp:ImageButton ID="imgDetail" ImageUrl="images/dropdown.png" Width="5%" Height="5%" runat="server" OnClick="imgDetail_Click" />
                             &nbsp;
                                <asp:Label ID="Label2" runat="server" Text="Claim ID: "></asp:Label>
                                <asp:Label ID="lblCliamId" runat="server" Text='<%# Eval("claimID") %>'></asp:Label>
                            </th>

                            <th class="col-xl-2">
                                <asp:Label ID="Label1" runat="server" Text="Title: "></asp:Label>
                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                            </th>
                            <th class="col-xl-2">
                                <asp:Label ID="Label3" runat="server" Text="Total Amount S$: "></asp:Label>
                                <asp:Label ID="lblTAmt" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                            </th>
                            <th class="col-xl-2">
                                <asp:Label ID="Label5" runat="server" Text="Status"></asp:Label>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                            </th>
                            <th class="col-xl-3 text-right">
                                <asp:Button ID="btnUpdate" class="btn btn-success" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnCancel" class="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                <asp:Button ID="viewGroupReject" class="btn viewbtn" runat="server" Text="View Reason" OnClick="viewGroupReject_Click" Visible="false" />
                            </th>
                        </tr>
                        <tr>
                            <td colspan="7" class="col-xl-12">
                                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False" ShowHeader="False"  class="col-xl-12">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                 <div class="float-container">

                                                <div class="float-child left">
                                                     <asp:Label ID="Label7" runat="server" class="form-label" Text="Type: "></asp:Label>
                                                            <asp:Label ID="lblType" runat="server" Text='<%# Eval("claimType") %>'></asp:Label>
                                                </div>
                                                <div class="float-child middle">
                                                   <asp:Label ID="Label9" runat="server" Text="Description: "></asp:Label>
                                                            <asp:Label ID="lblDes" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                                </div>
                                                     <div class="float-child">
                                                    <asp:Label ID="Label11" runat="server" Text="Amount S$: "></asp:Label>
                                                         
                                                             <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("amount") %>'></asp:Label>
                                                </div>

                                            </div>
                                                <div class="row">
                                                    <div class="col">
                                                        <div class="form-outline">
                                                           
                                                        </div>
                                                    </div>
                                                    <div class="col">
                                                        <div class="form-outline">
                                                            
                                                        </div>
                                                    </div>
                                                    <div class="col">
                                                        <div class="form-outline">
                                                              
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
                    
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        </div>
    </div>

</div>
</asp:Content>
