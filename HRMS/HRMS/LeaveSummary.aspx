<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="LeaveSummary.aspx.cs" Inherits="HRMS.LeaveSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Leave Summary
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style>
        .try {
            position: relative;
            top: 0;
            transition: top ease 0.5s;
        }

            .try:hover {
                top: -10px;
                cursor: pointer;
            }

        .icon-success {
            margin-bottom: 20px;
            color: green;
            font-size: 120px;
        }

        .failed {
            color: red;
            font-size: 95px;
        }
    </style>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" rel="stylesheet" />

    <br />
    <div class="row">

        <div class="col-xl-3 col-md-6 mb-4">
            <asp:LinkButton class="try" runat="server" ID="Annual" OnClick="Annual_Click">
                <div class="card border-left-primary shadow h-100 py-2">

                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <h6 class="m-0  text-secondary">My Annual Leave</h6>
                                <div class="h5 mb-0 font-weight-bold text-gray-800">
                                    <asp:Label runat="server" ID="annualcount"></asp:Label>
                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-calendar fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:LinkButton>
        </div>

        <!-- Earnings (Monthly) Card Example -->
        <div class="col-xl-3 col-md-6 mb-4">
            <asp:LinkButton class="try" runat="server" ID="Childcare" OnClick="Childcare_Click">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">

                                <h6 class="m-0  text-secondary">My Childcare Leave
                                </h6>
                                <div class="row no-gutters align-items-center">
                                    <div class="col-auto">
                                        <div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">
                                            <asp:Label runat="server" ID="childcarecount"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-calendar fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:LinkButton>
        </div>

        <!-- Earnings (Monthly) Card Example -->
        <div class="col-xl-3 col-md-6 mb-4">
            <asp:LinkButton class="try" runat="server" ID="medi" OnClick="medi_Click1">
                <div class="card border-left-primary shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center">
                            <div class="col mr-2">
                                <h6 class="m-0  text-secondary">My Medical Leave
                                </h6>
                                <div class="row no-gutters align-items-center">
                                    <div class="col-auto">
                                        <div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">
                                            <asp:Label runat="server" ID="medicalcount"></asp:Label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-auto">
                                <i class="fas fa-calendar fa-2x text-gray-300"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:LinkButton>
        </div>

        <!-- Pending Requests Card Example -->
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card border-left-warning shadow h-100 py-2">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <h6 class="m-0  text-secondary">My pending leave</h6>
                            <div class="h5 mb-0 font-weight-bold text-gray-800">
                                <asp:Label runat="server" ID="pendingcount"></asp:Label>
                            </div>
                        </div>
                        <div class="col-auto">
                            <i class="fas fa-clipboard-list fa-2x text-gray-300"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <div class="container-fluid">

        <!-- Area Chart -->
        <div>
            <div class="card shadow mb-4">
                <!-- Card Header - Dropdown -->
                <div
                    class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0  text-secondary">My pending leave</h6>

                    <div class="dropdown no-arrow">
                        <a class="btn btn-gradient" href="Leave_addLeave.aspx" style="background-color: #F28C28; color: white;">
                            <i class="fa fa-plus fa-xs" aria-hidden="true"></i>
                        </a>

                    </div>
                </div>
                <!-- Card Body -->
                <div class="card-body">
                    <div class="table-responsive">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                        <asp:Repeater ID="Repeater1" runat="server">

                            <ItemTemplate>
                                <div>
                                    <table class="table table-bordered" width="100%" cellspacing="0">
                                        <thead>
                                            <tr>
                                                <th>Id</th>
                                                <th>Type</th>
                                                <th>Start Date</th>
                                                <th>End Date</th>
                                                <th>Status</th>
                                                <th colspan="2">Action</th>

                                            </tr>
                                        </thead>

                                        <tbody>
                                            <tr runat="server" id="row">
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("leaveID")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("leaveType")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text='<%#string.Format("{0:ddd MMM yyyy}", Eval("startDate"))%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("endDate")%>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("leaveStatus")%>'></asp:Label></td>

                                                <td>
                                                    <asp:LinkButton runat="server" ID="editbtn" CommandName="edit" CommandArgument='<%#Eval("leaveID")%>' OnCommand="edit">
                                                <i class="fas fa-edit" style="color:green"></i>
                                                    </asp:LinkButton>
                                                </td>
                                                <td>
                                                    <asp:LinkButton runat="server" ID="btndel" CommandArgument='<%#Eval("leaveID")%>' OnCommand="trigger">
                                                    <i class="fas fa-minus-circle" style="color:red"></i>
                                                    </asp:LinkButton>
                                                </td>


                                            </tr>





                                        </tbody>
                                    </table>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>


                    </div>
                    <div style="overflow: hidden; float: right;">

                        <asp:Repeater ID="rptPaging" runat="server" OnItemCommand="rptPaging_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPage"
                                    CommandName="Page" CommandArgument="<%# Container.DataItem %>"
                                    runat="server" ForeColor="#0645AD">
                                        <%# Container.DataItem %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                    <asp:LinkButton runat="server" OnClick="Unnamed1_Click">Show me all leave</asp:LinkButton>
                </div>

            </div>

        </div>


    </div>






    <div class="modal fade modal" id="DeleteModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Confirmation</label></h2>
                </div>
                <div class="modal-body">
                    <div class=" text-center">
                        <i class="fa fa-exclamation-circle fa-5x" style="color: orange" aria-hidden="true"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4 style="margin-left: auto; margin-right: auto; text-align: center;">

                                        <asp:Label runat="server">Are you sure to delete leave
                                                <asp:Label runat="server" ID="getid"></asp:Label>?
                                        </asp:Label>
                                    </h4>


                                    <br />
                                    <div class="table-responsive">
                                        <table class="table table-bordered" width="100%" cellspacing="0">
                                            <thead>
                                                <tr>
                                                    <th>Id</th>
                                                    <th>Type</th>
                                                    <th>Start Date</th>
                                                    <th>End Date</th>
                                                    <th hidden="hidden">Duration</th>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr style="background-color: #FBEFE4">
                                                    <td>
                                                        <asp:Label runat="server" ID="id"></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="type"></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="start"></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="end"></asp:Label></td>
                                                    <td hidden="hidden">
                                                        <asp:Label runat="server" ID="duration"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="status"></asp:Label></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer text-center">
                    <div class="col-md-12">
                        <asp:Button class="btn btn-secondary" runat="server" type="button" OnCommand="changeback" Text="Cancel"></asp:Button>

                        <asp:Button runat="server" class="btn btn-danger" CommandName="remove" CommandArgument='<%#Eval("leaveID") %>' Text="Delete" OnCommand="deleting"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade modal" id="statuspopup" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <asp:Label runat="server" ID="Label6"></asp:Label></h2>
                </div>
                <div class="modal-body">
                    <div class="mb-2 text-center">
                        <i class="fa fa-check-circle icon-success " style="font-size: 120px;"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4 style="margin-left: auto; margin-right: auto; text-align: center;">
                                        <asp:Label runat="server" ID="Label7"> </asp:Label>
                                    </h4>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer text-center">
                    <div class="col-md-12">
                        <button class="btn btn-secondary" type="button" style="width: 100%" data-dismiss="modal" onclick="javascript:window.location.href='LeaveSummary.aspx'">Close</button>

                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

