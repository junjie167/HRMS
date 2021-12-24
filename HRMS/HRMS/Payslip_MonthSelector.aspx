<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Payslip_MonthSelector.aspx.cs" Inherits="HRMS.Payslip_MonthSelector" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style>
        .link {
            width: 60px;
            height: 35px;
            background-color: #F28C28;
            text-align: center;
            padding-top: 7px;
        }
    </style>
    <div class="row justify-content-center ">
        <div class="col-sm-5 .col-md-6">
            <div class="test">
                <div class="card border-left-warning shadow h-100 py-2">
                    <div class="card-body">
                        <div class="row no-gutters align-items-center ">
                            <div class="col mr-2">
                                <h6 class="m-0  text-secondary" style="font-size: 30px;">Days to PayDay </h6>
                                <div class="row no-gutters align-items-center">
                                    <div class="col-auto">
                                        <div class="h5 mb-0 mr-3 font-weight-bold text-gray-800">
                                            <asp:Label runat="server" ID="numOfleaves" Font-Size="40px" Font-Bold="true" Font-Italic="true" CssClass="numbertext"></asp:Label>
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
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <div class="container-fluid">

        <!-- Area Chart -->
        <div class="row justify-content-center">
            <div class=" col-sm-8 ">
                <div class="card shadow mb-4">
                    <div class="card-body">
                        <div class="table-responsive">
                            <div class="d-flex justify-content-center ">
                                <asp:Label runat="server" Font-Size="20px">Select the month to view your payslip</asp:Label>
                            </div>
                            <input type="month" class="form-control" id="payslipmonth" value="2021-11" runat="server" /><br />
                            <br />
                            <div class="d-flex justify-content-end">
                                <asp:LinkButton runat="server" ID="viewpayslip" OnClick="nextPage_payslip" CssClass="link">
                                 <i class="fa fa-arrow-right" style="color:white; " aria-hidden="true"></i>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--Other Error Message popup--%>
    <div class="modal fade" id="verify" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="display: inline-block; text-align: center">
                <div class="modal-header">
                    <h5 class="modal-title">Verification</h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body" style="display: inline-block; text-align: center">
                    <i class="fa fa-exclamation-circle fa-2x" style="color: #cc0000; text-align: center; display: inline-block; width: 100%" aria-hidden="true"></i>
                    <p>Please enter your password to view your payslip.</p>
                    <asp:TextBox type="text" TextMode="password" ID="verifypw" runat="server"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="button1" runat="server" class="btn btn-secondary warningConfirmBtn" Text="Confirm" OnClick="goToPaySlip" />

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="fail" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="display: inline-block; text-align: center">
                <div class="modal-header">
                    <h5 class="modal-title">Error</h5>
                    <button class="close" type="button" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body" style="display: inline-block; text-align: center">
                    <i class="fa fa-exclamation-circle fa-2x" style="color: #cc0000; text-align: center; display: inline-block; width: 100%" aria-hidden="true"></i>
                    <p>Please enter your password to view your payslip.</p>
                    <asp:TextBox type="text" TextMode="password" ID="cfmpw" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="errorMsg" runat="server"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="LinkButton4" runat="server" class="btn btn-secondary warningConfirmBtn" Text="Confirm" OnClick="checkForPaySlip" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
