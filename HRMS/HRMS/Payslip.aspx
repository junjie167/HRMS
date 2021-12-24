<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Payslip.aspx.cs" Inherits="HRMS.Payslip" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--INSERT PAGE NAME HERE-->
    <%--Payslip--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.3/html2pdf.bundle.min.js" integrity="sha512-YcsIPGdhPK4P/uRW6/sruonlYj+Q7UHWeKfTAkBW+g83NKM+jMJFJ4iAPfSnVp7BKD4dKMHmVSvICUbE/V1sSw==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>


    <script type="text/javascript">
        $("#datepicker").datepicker({
            format: "mm-yyyy",
            startView: "months",
            minViewMode: "months"
        });

        function pdfDownload() {
            var element = document.getElementById('printPDF');
            var opt = {
                margin: 1,
                filename: 'Payslip.pdf',
                image: { type: 'jpeg', quality: 0.98 },
                html2canvas: { scale: 2 },
                jsPDF: { unit: 'in', format: 'letter', orientation: 'landscape' }
            };
            // New Promise-based usage:
            html2pdf().set(opt).from(element).save();
            //html2pdf(element);
        }

        function openUpdateModal() {
            $('#updateModal').modal('show');
        }
        function openErrorModal() {
            $('#errorModal').modal('show');
        }

    </script>

    <style>
        .btnDownload {
            /*background-color: DodgerBlue;*/
            background-color: orange;
            border: none;
            color: black;
            padding: 12px 30px;
            cursor: pointer;
            font-size: 20px;
        }

            /* Darker background on mouse-over */
            .btnDownload:hover {
                background-color: darkorange;
            }

        table, td, tr {
            /*border: 2px solid black;*/
            color: black;
            font-size: 20px;
        }

        #psDate {
            text-align: right;
        }


        #container {
            padding: 25px;
            width: 100%;
            padding: 25px;
            background-color: white;
        }

        #earningsTable {
            width: 100%;
            border: 1.5px solid orange;
        }

            #earningsTable td + td, th + th {
                border-left: 1px solid orange;
            }

        .btnGetUpdate {
            background-color: #F28C28;
            width: 10%;
            color: white;
            padding: 9px;
            border: none;
            cursor: pointer;
            font-weight: bold;
            border-radius: 20px;
        }

        .errorConfirmBtn {
            text-align: center;
            margin: auto;
            width: 100%;
            background-color: red;
        }

        .failed {
            color: red;
            font-size: 150px; 
        }

    </style>

    
    <asp:Label ID="lblChooseDate" runat="server" Text="Choose Date: "></asp:Label></td>
    <input type="month" id="monthYear" name="payslipMthYear" style="width:16%;" runat="server" value="2021-11" />
    <asp:Button ID="BtnMthYear" CssClass="btnGetUpdate" runat="server" Text="Get Payslip" OnClick="BtnMthYear_Click" />
    <%--<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>--%>

    <%--    <asp:DropDownList ID="DDLMth" runat="server">
        <asp:ListItem Value="">Please Select</asp:ListItem>
        <asp:ListItem>Jan</asp:ListItem>
        <asp:ListItem>Feb</asp:ListItem>
        <asp:ListItem>Mar</asp:ListItem>
        <asp:ListItem>Apr</asp:ListItem>
        <asp:ListItem>May</asp:ListItem>
        <asp:ListItem>Jun</asp:ListItem>
        <asp:ListItem>Jul</asp:ListItem>
        <asp:ListItem>Aug</asp:ListItem>
        <asp:ListItem>Sep</asp:ListItem>
        <asp:ListItem>Oct</asp:ListItem>
        <asp:ListItem>Nov</asp:ListItem>
        <asp:ListItem>Dec</asp:ListItem>
    </asp:DropDownList>


    <asp:DropDownList ID="DDLYear" runat="server">
        <asp:ListItem Value="">Please Select</asp:ListItem>
        <asp:ListItem>2018</asp:ListItem>
        <asp:ListItem>2019</asp:ListItem>
        <asp:ListItem>2020</asp:ListItem>
        <asp:ListItem>2021</asp:ListItem>
        <asp:ListItem>2022</asp:ListItem>
    </asp:DropDownList>--%>


    <div id="printPDF">
        <div id="container">
            <h2>Payslip</h2>


            <div id="psDate">
                 <p style="font-weight: bold; color: black;font-size: 20px;">Period: </p>
                 <asp:Label ID="lblpsMth" runat="server" Text="Month"></asp:Label>
                 <p style="font-weight: bold; color: black;font-size: 20px;">Date of Payment Received: </p>
                <asp:Label ID="lblpsDateReceived" runat="server" Text="Date"></asp:Label>
            </div>

          
            <table>
                <tr>
                    <td>Company Name: </td>
                    <td>
                        <asp:Label ID="lblComName" runat="server" Text="HRMS"></asp:Label></td>
                </tr>
                <tr>
                    <td>Employee Name: </td>
                    <td>
                        <asp:Label ID="lblEmpName" runat="server" Text="$$$"></asp:Label></td>
                </tr>
                <tr>
                    <td>Employee ID: </td>
                    <td>
                        <asp:Label ID="lblEmpID" runat="server" Text="$$$"></asp:Label></td>
                </tr>
                <tr>
                    <td>Employee Status: </td>
                    <td>
                        <asp:Label ID="lblEmpStat" runat="server" Text="$$$"></asp:Label></td>
                </tr>
                <tr>
                    <td>Job Title: </td>
                    <td>
                        <asp:Label ID="lblJobTitle" runat="server" Text="$$$"></asp:Label></td>
                </tr>
            </table>
            <br />
            <br />

            <table id="earningsTable">
                <tr>
                    <th>Earnings</th>
                    <th></th>
                    <th></th>
                    <th></th>
                </tr>
                <tr>
                    <td>Basic Pay: </td>
                    <td>
                        <asp:Label ID="lblBasicPay" runat="server" Text="$$$"></asp:Label></td>
                    <td>Employee CPF: </td>
                    <td>
                        <asp:Label ID="lblCPF" runat="server" Text="$$$"></asp:Label></td>
                </tr>
                <tr>
                    <td>Overtime: </td>
                    <td>
                        <asp:Label ID="lblOT" runat="server" Text="$$$"></asp:Label></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Allowance: </td>
                    <td>
                        <asp:Label ID="lblAllowance" runat="server" Text="-"></asp:Label></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>Total Earnings: </td>
                    <td>
                        <asp:Label ID="lblTE" runat="server" Text="$$$"></asp:Label></td>
                    <td>Total Deduction</td>
                    <td>
                        <asp:Label ID="lblTD" runat="server" Text="$$$"></asp:Label></td>
                </tr>
            </table>
            <br />
            <%--<button type="button" class="btn" onclick="pdfDownload()"><i class="fa fa-download"></i>Download</button>--%>
        </div>
        <button type="button" class="btnDownload" onclick="pdfDownload()"><i class="fa fa-download"></i>Download</button>
    </div>





    <%--Update Message popup--%>
    <%--<div class="modal fade modal" id="updateModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Confirmation</label></h2>
                </div>
                <div class="modal-body">
                    <div class="mb-2 text-center">
                        <p>Personal particulars updated</p>
                        <i class="fas fa-check-circle success "></i>
                    </div>

                </div>
                <div class="modal-footer">
                    <i class=" icon-checkmark3 position-right"></i>
                    <asp:Button ID="Button2" runat="server" class="btn btn-success confirmBtn" Text="Confirm" OnClick="BtnReload_Click"/>

                </div>
            </div>
        </div>
    </div>--%>

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
                        <i class="fa fa-times-circle failed" ></i>
                        <p>You have selected an INVALID date for payslip</p>
                        <p>Please make sure to select the valid date</p>
                    </div>

                </div>
                <div class="modal-footer">
                    <i class=" icon-checkmark3 position-right"></i>
                    <asp:Button ID="Button1" runat="server" class="btn btn-danger errorConfirmBtn" Text="Confirm" OnClick="Button_reload"/>
                    <%--<asp:Button ID="Button1" runat="server" class="btn btn-danger errorConfirmBtn" Text="Confirm"/>--%>

                </div>
            </div>
        </div>
    </div>



</asp:Content>
