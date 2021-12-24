<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HRMS.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--INSERT PAGE NAME HERE-->
    Dashboard
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script>
        function openManagerAccessModal() {
            document.write("<style>.container-menu-header {z-index: 0; }</style>");
            $('#managerAccessModal').modal('show');
        }
    </script>
    <style>
        img {
            width: 90px;
            height: 90px;
        }

        #calendarsec {
            margin-left: 50px;
        }

        #sidebuttons {
            list-style-type: none;
            margin-right: 100px;
            margin-bottom: 40px;
        }

            #sidebuttons li {
                margin: 10px;
                padding-right: 90px;
                padding-left: 20px;
                border: solid 2px black;
            }

        #legend {
            list-style: none;
        }

            #legend span {
                border: 1px solid #ccc;
                float: left;
                width: 12px;
                height: 12px;
                margin: 5px;
            }

            #legend #approved {
                background-color: #8FFDA6;
            }

            #legend #pending {
                background-color: #FFB485;
            }


        #subact {
            height: 200px;
            width: 300px;
            border: solid 2px black;
            padding: 10px;
        }

        .link {
            width: 60px;
            height: 35px;
            background-color: #F28C28;
            text-align: center;
            padding-top: 7px;
        }

        .plus {
            width: 35px;
            height: 35px;
            text-align: center;
            padding-top: 7px;
            background-color: black;
        }

        .numbertext {
            margin-left: 65px;
            margin-top: 8px;
        }

        #leaveButtons {
            margin-right: 20px;
        }

        .failed {
            color: red;
            font-size: 120px;
        }

        .container {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .ackbtn {
            width: 100%;
        }

        .popupApplyBtn {
            width: 100%;
            font-size: 2em;
            background-color: #F28C28;
            color: white;
        }
    </style>

    <!--INSERT CONTENT HERE-->
    <section class="d-flex bd-highlight" id="calendarsec">
        <div class="p-2 flex-grow-1 bd-highlight">
            <%-- <asp:Calendar ID="Calendar" runat="server" BackColor="White"
                BorderColor="Gray" BorderWidth="2px" Font-Size="9pt" ForeColor="Black" Height="300px" Width="550px"
                OnDayRender="Calendar1_DayRender" Font-Names="Verdana" CellPadding="15" CellSpacing="15">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <NextPrevStyle Font-Size="8pt" ForeColor="#333333" Font-Bold="True" VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <TitleStyle BackColor="White" Font-Bold="True" Font-Size="12pt"
                    ForeColor="#333399" BorderColor="Black" BorderWidth="2px" />
                <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar> --%>

            <!--Calendar display!-->
            <asp:Calendar ID="Calendar" runat="server" BackColor="White" SelectionMode="None" DayNameFormat="Full"
                BorderColor="Gray" BorderWidth="2px" Font-Size="9pt" ForeColor="Black" Width="425px"
                OnDayRender="Calendar1_DayRender" Font-Names="Verdana" CellPadding="20">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <NextPrevStyle Font-Size="8pt" ForeColor="#333333" Font-Bold="True" VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <TitleStyle BackColor="White" Font-Bold="True" Font-Size="12pt"
                    ForeColor="#333399" BorderColor="Black" BorderWidth="2px" />
                <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar>
            <ul class="d-flex bd-highlight justify-content-sm-center" id="legend">
                <li><span id="approved"></span>Approved</li>
                <li><span id="pending"></span>Pending</li>
            </ul>
        </div>
        <div class="p-2 align-self-center align-content-center">
            <ul id="sidebuttons">
                <li>
                    <asp:LinkButton ID="managerBtn" OnClick="managerBtn_Click" runat="server">
                        <img src="img/manager.png" id="managerimg" class="img-fluid" />
                        Manager Mode
                    </asp:LinkButton>
                </li>
                <li><a href="FAQ.aspx">
                    <img src="img/faq.png" id="faqimg" class="img-fluid" />FAQ</a></li>
            </ul>
        </div>
    </section>
    <section class="d-flex justify-content-around" id="activities">
        <div id="subact">
            <u>View Payslip</u>
            <br />
            <br />
            <div>
                <input type="month" class="form-control" id="payslipmonth" value="2021-11" runat="server" />
            </div>
            <div class="d-flex align align-items-center">
                <asp:Label runat="server" ID="numOfleaves" Font-Size="30px" Font-Bold="true" Font-Italic="true" CssClass="numbertext"></asp:Label>
            </div>
            <div id="daysleft" class="d-flex bd-highlight justify-content-around">
                <p>Days to PayDay</p>

                <asp:LinkButton runat="server" ID="viewpayslip" OnClick="nextPage_payslip" CssClass="link">
                     <i class="fa fa-arrow-right" style="color:white; " aria-hidden="true"></i>
                </asp:LinkButton>
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

            <%--Manager Access Message popup--%>
            <div class="modal fade modal" id="managerAccessModal" data-keyboard="false" data-backdrop="static">
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
                                                <asp:Label ID="Label2" runat="server" Text="You do not have the access to the Manager Mode "></asp:Label>
                                            </h4>
                                        </div>
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

        </div>
        <div id="subact">
            <u>Leaves</u>
            <br />
            <div class="d-flex align justify-content-around">
                <asp:Label runat="server" ID="numAnnual" Font-Size="30px" Font-Bold="true" Font-Italic="true" CssClass="leavetexts"></asp:Label>
                <asp:Label runat="server" ID="numMedical" Font-Size="30px" Font-Bold="true" Font-Italic="true" CssClass="leavetexts"></asp:Label>
                <asp:Label runat="server" ID="numChildcare" Font-Size="30px" Font-Bold="true" Font-Italic="true" CssClass="leavetexts"></asp:Label>
            </div>
            <div class="d-flex align justify-content-around">
                <asp:Label runat="server" ID="Annual" CssClass="leavetexts"></asp:Label>
                <asp:Label runat="server" ID="Medical" CssClass="leavetexts"></asp:Label>
                <asp:Label runat="server" ID="Childcare" CssClass="leavetexts"></asp:Label>
            </div>
            <br />
            <div class="d-flex bd-highlight mb-3 justify-content-end" id="leaveButtons">
                <asp:LinkButton runat="server" ID="Linkbutton1" OnClick="addLeaves" CssClass="plus">
                     <i class="fa fa-plus" style="color:white;" aria-hidden="true"></i>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="viewLeaves" OnClick="nextPage_leaves" CssClass="link">
                     <i class="fa fa-arrow-right" style="color:white; " aria-hidden="true"></i>
                </asp:LinkButton>
            </div>

        </div>
        <div id="subact">
            <u>Claims</u>
            <br />
            <div class="d-flex align align-items-center justify-content-center">
                <asp:Label runat="server" ID="numoOfPendingClaims" Font-Size="30px" Font-Bold="true" Font-Italic="true"></asp:Label>
            </div>
            <div class="d-flex align align-items-center justify-content-center">
                Pending Claims
            </div>
            <br />
            <div class="d-flex bd-highlight mb-3 justify-content-end" id="claimButtons">
                <asp:LinkButton runat="server" ID="Linkbutton2" OnClick="nextPage_claims" CssClass="plus">
                     <i class="fa fa-plus" style="color:white;" aria-hidden="true"></i>
                </asp:LinkButton>
                <asp:LinkButton runat="server" ID="Linkbutton3" CssClass="link" OnClick="nextPage_claims">
                     <i class="fa fa-arrow-right" style="color:white; " aria-hidden="true"></i>
                </asp:LinkButton>
            </div>

        </div>
    </section>


    <div class="modal fade modal" id="statuspopup" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">Select to review employees leave/claims</h2>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-6 p-1">
                            <asp:Button ID="pendingLeave_manager" class="btn  popupApplyBtn" Text="Leave" runat="server" OnClick="pendingLeave_manager_Click" />
                        </div>
                        <div class="col-sm-6 p-1">
                            <asp:Button ID="pendingClaims_manager" class="btn popupApplyBtn" Text="Claims"  runat="server" OnClick="pendingClaims_manager_Click" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer text-center">
                    <%--  <div class="col-md-12">
                        <button class="btn btn-secondary" type="button" style="width: 100%" data-dismiss="modal" onclick="javascript:window.location.href='LeaveSummary.aspx'">Close</button>

                        <button class="btn btn-secondary" type="button" style="width: 100%" data-dismiss="modal" onclick="javascript:window.location.href='LeaveSummary.aspx'">Close</button>
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
