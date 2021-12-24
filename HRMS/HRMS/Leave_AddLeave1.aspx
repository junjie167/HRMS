<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Leave_AddLeave1.aspx.cs" Inherits="HRMS.Leave_AddLeave1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>

    <script>
        $(function () {
            var dtToday = new Date();

            var month = dtToday.getMonth() + 1;
            var day = dtToday.getDate();
            var year = dtToday.getFullYear();
            if (month < 10)
                month = '0' + month.toString();
            if (day < 10)
                day = '0' + day.toString();

            var maxDate = year + '-' + month + '-' + day;

            $('#drop_date').attr('min', maxDate);
            $('#pick_date').attr('min', maxDate);

            const picker = document.getElementById('pick_date');
            const picker1 = document.getElementById('drop_date');
            picker.addEventListener('input', function (e) {
                var day = new Date(this.value).getUTCDay();
                if ([6, 0].includes(day)) {
                    e.preventDefault();
                    this.value = '';
                    $('#no').modal('show');
                }
            });
            picker1.addEventListener('input', function (e) {
                var day = new Date(this.value).getUTCDay();
                if ([6, 0].includes(day)) {
                    e.preventDefault();
                    this.value = '';
                    $('#no').modal('show');
                }
            });

        });


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Add Leave
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">

        function getBusinessDatesCount() {
            var dropdt = new Date(document.getElementById("drop_date").value);
            var pickdt = new Date(document.getElementById("pick_date").value);
            let count = 0;
            const curDate = new Date(pickdt.getTime());
            while (curDate <= dropdt) {
                const dayOfWeek = curDate.getDay();
                if (dayOfWeek !== 0 && dayOfWeek !== 6) count++;
                curDate.setDate(curDate.getDate() + 1);
            }

            return count;
        }

        function cal() {
            if (document.getElementById("drop_date")) {
                document.getElementById("numdays2").value = getBusinessDatesCount();

            }
        }


    </script>
    <style>
        .Space input[type="checkbox"] {
            margin-right: 5px;
        }

        .popup {
            position: relative;
            display: inline-block;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

            /* The actual popup */
            .popup .popuptext {
                visibility: hidden;
                width: 160px;
                background-color: #555;
                color: #fff;
                text-align: center;
                border-radius: 6px;
                padding: 8px 0;
                position: absolute;
                z-index: 1;
                bottom: 125%;
                left: 50%;
                margin-left: -80px;
            }

                /* Popup arrow */
                .popup .popuptext::after {
                    content: "";
                    position: absolute;
                    top: 100%;
                    left: 50%;
                    margin-left: -5px;
                    border-width: 5px;
                    border-style: solid;
                    border-color: #555 transparent transparent transparent;
                }

            /* Toggle this class - hide and show the popup */
            .popup .show {
                visibility: visible;
                -webkit-animation: fadeIn 1s;
                animation: fadeIn 1s;
            }

        /* Add animation (fade in the popup) */
        @-webkit-keyframes fadeIn {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
        }

        @keyframes fadeIn {
            from {
                opacity: 0;
            }

            to {
                opacity: 1;
            }
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

            #legend #NA {
                background-color: #FBEFE4;
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
    <script>
        // When the user clicks on div, open the popup
        function myFunction() {
            var popup = document.getElementById("myPopup");
            popup.classList.toggle("show");
        }
    </script>
    <script>
        // When the user clicks on div, open the popup
        function myFunction1() {
            var popup = document.getElementById("calendar");
            popup.classList.toggle("show");
        }
    </script>

    <br />
    <!-- START CONTENT -->
    <div class="container-fluid" style="background: white">
        <br />
        <div class="col-xl-12">
            <asp:Label ID="getCurrentDate" runat="server" Text="Label" Style="text-align: right; display: block;"></asp:Label>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


        <fieldset>
            <legend align="center">Leave Application Form

                <br />
                <asp:Label runat="server" ID="Label4" Style="font-size: 15px" Text="Employee Id:"></asp:Label>
                <asp:Label runat="server" ID="tbEmpId" Style="font-size: 15px"></asp:Label>
            </legend>


            <!-- GROUP LEAVE DETAILS -->
            <fieldset style="border: 1px solid black;">
                <legend style="width: auto; padding: 0 10px;" align="top">Leave details</legend>
                <br />
                <div class="container-fluid">
                    <!-- DDL -->
                    <div class="form-outline mb-12">
                        <asp:Label runat="server" class="form-label" for="DdlTypeLeave" Width="100%">Leave Type
                        <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                        </asp:Label>

                        <asp:UpdatePanel ID="upnlMain" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="DdlTypeLeave" runat="server"
                                    OnSelectedIndexChanged="DdlTypeLeave_SelectedIndexChanged" AutoPostBack="true" class="form-control" Width="100%" EnableViewState="true">
                                    <asp:ListItem>Choose:</asp:ListItem>
                                    <asp:ListItem>Annual</asp:ListItem>
                                    <asp:ListItem>Medical</asp:ListItem>
                                    <asp:ListItem>Childcare</asp:ListItem>
                                </asp:DropDownList>

                                <asp:Label runat="server" class="form-label" Visible="false" ID="error"></asp:Label>
                                <asp:Label runat="server" class="form-label" Visible="false" ID="remainingText">Remaining Leave for </asp:Label>
                                <asp:Label runat="server" class="form-label" Visible="false" ID="choosen"></asp:Label>

                            </ContentTemplate>

                        </asp:UpdatePanel>

                    </div>
                    <!--END OF DDL -->

                    <div class="row mb-4"></div>
                    <!-- START DATE -->
                    <div class="row mb-4">
                        <div class="col">
                            <div class="form-outline">
                                <asp:Label runat="server" class="form-label" Width="100%" ID="datefrom">Date From 
                                            <asp:Label runat="server" style="color:red; font-size:20px">*</asp:Label>
                                </asp:Label>
                                <input type="date" class="form-control" id="pick_date" name="pickup_date" onchange="cal()" />
                            </div>
                            <br />
                            <div class="form-outline">
                                <asp:Label runat="server" class="form-label" Width="100%" ID="dateto">Date To 
                                             <asp:Label runat="server" style="color:red;font-size:20px">*</asp:Label>
                                </asp:Label>
                                <input type="date" class="form-control" id="drop_date" name="dropoff_date" onchange="cal()" />
                            </div>
                            <br />
                            <div class="form-outline">
                                <asp:Label runat="server" class="form-label" Width="100%">Number of days
                                    <div class="popup" onclick="myFunction()"><i class="fa fa-question-circle" aria-hidden="true"></i>
                                      <span class="popuptext" id="myPopup">Caculated based on no. of business days (excluding weekends)</span>
                                    </div>
                                </asp:Label>
                                <input type="text" class="form-control" id="numdays2" name="numdays" readonly="true" value="0" />


                            </div>
                            <asp:Label runat="server" ID="requiredStartEnd" Visible="false" Text="Please select date"></asp:Label>

                        </div>
                        <div class="col">
                            <div class="row mb-4"></div>
                            <div class="container-fluid">
                                <div class="card border-left-primary h-100 py-1">

                                    <div class="card-body">
                                        <div class="row no-gutters align-items-center">
                                            <div class="container-fluid">
                                                <div class="popup" onclick="myFunction1()" style="float: right">
                                                    <i class="fa fa-question-circle" aria-hidden="true"></i>
                                                    <span class="popuptext" id="calendar">A-Annnual
                                                        <br />
                                                        C-Childcare
                                                        <br />
                                                        M-Medical</span>
                                                </div>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <!--Calendar display!-->
                                                        <asp:Calendar ID="Calendar" runat="server" BackColor="White"
                                                            BorderColor="Gray" BorderWidth="2px" Font-Size="9pt" ForeColor="Black" Width="100%"
                                                            OnDayRender="Calendar1_DayRender" Font-Names="Verdana" CellPadding="10" CellSpacing="9" SelectionMode="None">
                                                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                                                            <NextPrevStyle Font-Size="8pt" ForeColor="#333333" Font-Bold="True" VerticalAlign="Bottom" />
                                                            <OtherMonthDayStyle ForeColor="#999999" />
                                                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                                            <TitleStyle BackColor="White" Font-Bold="True" Font-Size="12pt"
                                                                ForeColor="#333399" BorderColor="Black" BorderWidth="2px" />
                                                            <TodayDayStyle BackColor="#CCCCCC" />
                                                        </asp:Calendar>
                                                        <ul id="legend">

                                                            <li>
                                                                <span class="d-flex justify-content-sm-center" id="NA"></span>Dates Not Available</li>
                                                        </ul>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>




                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- END OF START DATE -->

                    <br />
                </div>

            </fieldset>


            <div class="row mb-4"></div>
            <!--BUTTON SUBMIT CLEAR-->
            <div class="col-md-12 text-center">
                <asp:Button runat="server" class="btn btn-secondary" Style="color: white" Text="Clear" ID="clear" OnClick="clear_Click" />
                <asp:Button runat="server" class="btn btn-success" Style="color: white" Text="Submit" ID="submit" OnClick="submit_Click" />
            </div>
            <br />
            <br />


        </fieldset>

    </div>

    <br />
    <br />



    <div class="modal fade modal" id="no" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">Error</h2>
                </div>
                <div class="modal-body">
                    <div class="mb-2 text-center">
                        <i class="fa fa-times-circle failed"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4 style="margin-left: auto; margin-right: auto; text-align: center;">Weekends are not allowed. Please choose again
                                    </h4>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer text-center">
                    <div class="col-md-12">
                        <button class="btn btn-secondary" type="button" data-dismiss="modal" style="width:100%">Close</button>
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
                                    <h4 style="margin-left: auto; margin-right: auto; text-align: center;">
                                        <asp:Label runat="server" ID="label7"> </asp:Label>
                                    </h4>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer text-center">
                    <div class="col-md-12">
                        <button class="btn btn-secondary" style="width:100%" type="button" data-dismiss="modal" onclick="javascript:window.location.href='LeaveSummary.aspx'">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade modal" id="fails" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <asp:Label runat="server" ID="Label1"></asp:Label></h2>
                </div>
                <div class="modal-body">
                    <div class="mb-2 text-center">
                        <i class="fa fa-times-circle failed"></i>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4 style="margin-left: auto; margin-right: auto; text-align: center;">
                                        <asp:Label runat="server" ID="label2"> </asp:Label>
                                    </h4>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer text-center">
                    <div class="col-md-12">
                        <button class="btn btn-secondary" type="button" style="width:100%" data-dismiss="modal" onclick="javascript:window.location.href='Leave_addLeave1.aspx'">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

 

    <div class="modal fade modal" id="Submitmodal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Confirmation</label></h2>
                </div>
                <div class="modal-body">
                    <div class="mb-2 text-center">
                        <%-- <i class="fa fa-check-circle icon-success"></i>--%>
                    </div>
                    <div class="form-group">
                        <div class="container">
                            <div class="child">
                                <div class="mb-2">
                                    <h4 style="margin-left: auto; margin-right: auto; text-align: center;">

                                        <asp:Label runat="server">Are you sure to submit the following leave
                                            <asp:Label runat="server" ID="getid"></asp:Label>?
                                        </asp:Label>
                                    </h4>
                                    <br />
                                    <div class="table-responsive">
                                        <table class="table table-bordered" width="100%" cellspacing="0">
                                            <thead>
                                                <tr>
                                                    <th style="display: none;">Id</th>
                                                    <th>Type</th>
                                                    <th>Start Date</th>
                                                    <th>End Date</th>
                                                    <th>Duration</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td style="display: none;">
                                                        <asp:Label runat="server" ID="id"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="type"></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="start"></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="end"></asp:Label></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="duration"></asp:Label>
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
                    <asp:Button class="btn btn-secondary" runat="server" type="button" Text="Cancel" aria-hidden="true"></asp:Button>

                    <asp:Button runat="server" class="btn btn-success" CommandName="insert" Text="Submit" OnCommand="insert"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
