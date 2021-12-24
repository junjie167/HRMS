<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="HRMS.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--INSERT PAGE NAME HERE-->
    Profile
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" k />

    <script type="text/javascript">

        function openUpdateModal() {
            $('#updateModal').modal('show');
        }
        function openErrorModal() {
            $('#errorModal').modal('show');
        }

        
    </script>
    <style>
        #container {
            border-width: inherit;
        }

        table {
            font-size: 20px;
            color: black;
        }

            table td {
                width: 50%;
            }

        .btnUpdate {
            background-color: #F28C28;
            width: 100%;
            color: white;
            padding: 10px;
            border: none;
            cursor: pointer;
            font-weight: bold;
            border-radius: 8px;
        }

        .confirmBtn {
            text-align: center;
            margin: auto;
            width: 100%;
            background-color: #5cb85c;
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
        .success {
            font-size: 150px; 
            color: #5cb85c;
        }

    </style>

    <fieldset style="border: 2px solid;">
        <legend style="width: 215px; margin-left: 10px; color: black;">Personal Particular</legend>
        <div id="container">
            <table>
                <tr>
                    <td>Home Address: </td>
                    <td>
                        <asp:TextBox ID="tbHome" runat="server" Text="Label"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Phone Number: </td>
                    <td>
                        <asp:TextBox ID="tbPhone" runat="server" MaxLength="8" Text="Label" onkeydown="return (!((event.keyCode>=65 && event.keyCode <= 95) || event.keyCode >= 106 || (event.keyCode >= 48 && event.keyCode <= 57 && isNaN(event.key))) && event.keyCode!=32);"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>Date of Birth: </td>
                    <td>
                        <asp:Label ID="LabelDOB" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Employee Number: </td>
                    <td>
                        <asp:Label ID="LabelEmpNum" runat="server" Text="Label"></asp:Label></td>
                </tr>

                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="BtnUpdate" CssClass="btnUpdate" runat="server" Text="Update" OnClick="BtnUpdate_Click" /></td>
                </tr>

            </table>

        </div>
    </fieldset>

    <br />
    <br />



    <%--<h3>Bank Particular</h3>--%>
    <fieldset style="border: 2px solid;">
        <legend style="width: 175px; margin-left: 10px; color: black;">Bank Particular</legend>
        <div id="container">
            <table>
                <tr>
                    <td>Bank Account Number: </td>
                    <td>
                        <asp:Label ID="LabelBankAccNum" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Bank Name: </td>
                    <td>
                        <asp:Label ID="LabelBankName" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Branch Name: </td>
                    <td>
                        <asp:Label ID="LabelBranch" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <td>Account Status: </td>
                    <td>
                        <asp:Label ID="LabelAccStatus" runat="server" Text="Label"></asp:Label></td>
                </tr>

                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>

            </table>

            <a href="">&#x1F4DE Contact administrator to change</a>

        </div>
    </fieldset>



<%--Update Message popup--%>
    <div class="modal fade modal" id="updateModal" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">

            <div class="modal-content">
                <div class="modal-header text-center">
                    <h2 class="modal-title w-100">
                        <label class="control-label">Confirmation</label></h2>
                </div>
                <div class="modal-body">
                    <div class="mb-2 text-center">
                        <i class="fas fa-check-circle success "></i>
                        <p>Personal particulars updated</p>
                    </div>

                </div>
                <div class="modal-footer">
                    <i class=" icon-checkmark3 position-right"></i>
                    <asp:Button ID="Button2" runat="server" class="btn btn-success confirmBtn" Text="Confirm" OnClick="BtnReload_Click"/>

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
                        <i class="fa fa-times-circle failed" ></i>
                        <p>Please make sure that Address field is valid</p>
                        <p>Please make sure that Phone number field is only in NUMBER</p>
                    </div>

                </div>
                <div class="modal-footer">
                    <i class=" icon-checkmark3 position-right"></i>
                    <asp:Button ID="Button1" runat="server" class="btn btn-danger errorConfirmBtn" Text="Confirm" OnClick="BtnReload_Click"/>

                </div>
            </div>
        </div>
    </div>




</asp:Content>
