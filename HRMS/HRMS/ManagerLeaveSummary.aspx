<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ManagerLeaveSummary.aspx.cs" Inherits="HRMS.ManagerLeaveSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Review Leave Summary
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style>
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
    <br />

    <div class="container-fluid">
        <div class="card shadow mb-4">
            <div class="table-responsive">
                <div
                    class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0  text-secondary">
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem>Pending List</asp:ListItem>
                            <asp:ListItem>Approved List</asp:ListItem>
                            <asp:ListItem>Rejected List</asp:ListItem>
                        </asp:DropDownList></h6>
                </div>

                <div class="tab-content">
                    <div class="card-body">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">

                            <HeaderTemplate>
                                <table class="table table-bordered" width="100%" cellspacing="0">
                                    <tr>
                                        <b>

                                            <th>Id</th>
                                            <th>Type</th>
                                            <th>Start Date</th>
                                            <th>End Date</th>
                                            <th>Status</th>
                                            <th>Submitted By</th>
                                            <th hidden="hidden">Duration</th>
                                            <th colspan="2">Actions</th>

                                        </b>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr runat="server" id="row">

                                    <td>
                                        <%#Eval("leaveID")%>  
                                    </td>
                                    <td>
                                        <%#Eval("leaveType")%>  
                                    </td>
                                    <td>
                                        <%#Eval("startDate")%>  
                                    </td>
                                    <td>
                                        <%#Eval("endDate")%>  
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("leaveStatus")%>'>
                                        </asp:Label>
                                    </td>

                                    <td>
                                        <%#Eval("employeeName")%>  
                                    </td>
                                    <td hidden="hidden">
                                        <asp:Label runat="server" ID="duration" Text='<%#Eval("duration") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="btnapprove" CommandName="approvepop" CommandArgument='<%#Eval("leaveID")%>' OnCommand="approvepop">
                                            <i class="fa fa-check" style="color: green"></i>
                                        </asp:LinkButton>


                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="btnreject" CommandName="rejectpop" CommandArgument='<%#Eval("leaveID")%>' OnCommand="rejectpop">
                                            <i class="fa fa-times fa-lg" style="color: red"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>

                            <AlternatingItemTemplate>
                                <tr runat="server" id="row">
                                    <td>
                                        <%#Eval("leaveID")%>  
                                    </td>
                                    <td>
                                        <%#Eval("leaveType")%>  
                                    </td>
                                    <td>
                                        <%#Eval("startDate")%>  
                                    </td>
                                    <td>
                                        <%#Eval("endDate")%>  
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblStatus" Text='<%#Eval("leaveStatus")%>'>
                                        </asp:Label>
                                    </td>

                                    <td>
                                        <%#Eval("employeeName")%> 
                                    </td>
                                    <td hidden="hidden">
                                        <asp:Label runat="server" ID="duration" Text='<%#Eval("duration") %>'></asp:Label>

                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="btnapprove" CommandName="approvepop" CommandArgument='<%#Eval("leaveID")%>' OnCommand="approvepop">
                                            <i class="fa fa-check" style="color: green"></i>
                                        </asp:LinkButton>

                                    </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="btnreject" CommandName="rejectpop" CommandArgument='<%#Eval("leaveID")%>' OnCommand="rejectpop">
                                            <i class="fa fa-times fa-lg" style="color: red"></i>
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>

                            <FooterTemplate>
                                </table>  
                                    <asp:Label ID="defaultItem" runat="server"
                                        Visible='<%# Repeater1.Items.Count == 0 %>' Text="No items found" />

                            </FooterTemplate>

                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade modal" id="approveModal" data-keyboard="false" data-backdrop="static">
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

                                        <asp:Label runat="server">Are you sure to approve leave
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
                                                    <th>Submitted By</th>
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
                                                    <td>
                                                        <asp:Label runat="server" ID="employeeName"></asp:Label>
                                                    </td>
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
                        <asp:Button class="btn btn-secondary" runat="server" type="button" Text="Cancel"></asp:Button>

                        <asp:Button runat="server" class="btn btn-success" Text="Approve" CommandName="Approve" CommandArgument='<%#Eval("leaveID") %>' OnCommand="Approve"></asp:Button>
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
                        <i class="fa fa-check-circle icon-success"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4 style="margin-left: auto; margin-right: auto; text-align: center;">Sucessfully approved
                                    </h4>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer text-center">
                    <div class="col-md-12">
                        <button class="btn btn-secondary" type="button" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade modal" id="statuspopup1" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <asp:Label runat="server" ID="Label2"></asp:Label></h2>
                </div>
                <div class="modal-body">
                    <div class="mb-2 text-center">
                        <i class="fa fa-check-circle icon-success"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4 style="margin-left: auto; margin-right: auto; text-align: center;">Sucessfully rejected
                                    </h4>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer text-center">
                    <div class="col-md-12">
                        <button class="btn btn-secondary" type="button" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade modal" id="rejectModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Reject Reason</label></h2>
                </div>
                <div class="modal-body">
                    <div class=" text-center">
                        <i class="fa fa-times-circle failed"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4 style="margin-left: auto; margin-right: auto; text-align: center;">
                                        <br />
                                        <asp:Label runat="server" ID="du" Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="empid" Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="type1" Visible="false"></asp:Label>
                                        <asp:Label runat="server">Are you sure to reject leave
                                                <asp:Label runat="server" ID="Label1"></asp:Label>?
                                        </asp:Label>
                                    </h4>

                                    <br />
                                    <div class="container-fluid">
                                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer text-center">
                    <div class="col-md-12">
                        <asp:Button class="btn btn-secondary" runat="server" type="button" Text="Cancel"></asp:Button>

                        <asp:Button runat="server" class="btn btn-danger" Text="Reject" CommandName="Reject" CommandArgument='<%#Eval("leaveID") %>' OnCommand="Reject"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
