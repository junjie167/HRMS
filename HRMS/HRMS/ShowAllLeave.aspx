<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ShowAllLeave.aspx.cs" Inherits="HRMS.ShowAllLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Show All Leave
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
    <div>
        <div class="container-fluid">
            <div class="card shadow mb-4">

                <div class="table-responsive">
                    <div
                        class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                        <h6 class="m-0  text-secondary">My leave</h6>
                        <div class="dropdown no-arrow">
                            <a class="btn btn-gradient" href="Leave_addLeave.aspx" style="background-color: #F28C28; color: white;">
                                <i class="fa fa-plus fa-xs" aria-hidden="true"></i>
                            </a>

                        </div>
                    </div>
                    <div class="card-body">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlfilter" runat="server"
                                    AutoPostBack="true" class="form-control" Width="100%" EnableViewState="true" OnSelectedIndexChanged="ddlfilter_SelectedIndexChanged">
                                    <asp:ListItem>All</asp:ListItem>
                                    <asp:ListItem>Pending</asp:ListItem>
                                    <asp:ListItem>Approved</asp:ListItem>
                                    <asp:ListItem>Rejected</asp:ListItem>
                                </asp:DropDownList>
                                <div style="float: right;">
                                    <asp:LinkButton runat="server" Text="Latest" ID="latest" OnClick="latest_Click"></asp:LinkButton>
                                    &nbsp; &nbsp;
                                    <asp:LinkButton ID="oldest" runat="server" Text="Oldest" OnClick="oldest_Click"></asp:LinkButton>
                                </div>

                                <asp:Repeater ID="RepeatInformation" runat="server" OnItemDataBound="RepeatInformation_ItemDataBound">
                                    <HeaderTemplate>
                                        <table class="table table-bordered" width="100%" cellspacing="0">
                                            <tr>
                                                <b>

                                                    <th>Id</th>
                                                    <th>Type</th>
                                                    <th>Start Date</th>
                                                    <th>End Date</th>
                                                    <th>Status</th>
                                                    <th>Rejected Reason</th>
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
                                                <%#Eval("rejectedReason")%>  
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="editbtn" runat="server" CommandName="edit" CommandArgument='<%#Eval("leaveID")%>' OnCommand="edit">
                                                    <i class="fas fa-edit" style="color:green"></i>
                                                </asp:LinkButton>

                                            </td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="btndel" CommandArgument='<%#Eval("leaveID") %>' OnCommand="trigger">
                                                    <i class="fas fa-minus-circle" style="color:red"></i>
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
                                                <%#Eval("rejectedReason")%>  
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="editbtn" runat="server" CommandName="edit" CommandArgument='<%#Eval("leaveID")%>' OnCommand="edit">
                                                    <i class="fas fa-edit" style="color:green"></i>
                                                </asp:LinkButton>

                                            </td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="btndel" CommandArgument='<%#Eval("leaveID") %>' OnCommand="trigger">
                                                    <i class="fas fa-minus-circle" style="color:red"></i>
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>

                                    <FooterTemplate>
                                        </table>  
                                 <asp:Label ID="defaultItem" runat="server"
                                     Visible='<%# RepeatInformation.Items.Count == 0 %>' Text="No items found" />
                                    </FooterTemplate>
                                </asp:Repeater>

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
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
                        <i class="fa fa-check-circle icon-success"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4 style="margin-left: auto; margin-right: auto; text-align: center;">The deletion is successful
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

</asp:Content>
