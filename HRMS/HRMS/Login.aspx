<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HRMS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="shortcut icon" href="../img/logo.png" />
    <link href="../css/sb-admin-2.css" rel="stylesheet" />
    <link href="../css/sb-admin-2.min.css" rel="stylesheet" />
    <link href="../vendor/fontawesome-free/css/all.min.css" rel="stylesheet" />
    <script src="../vendor/jquery/jquery.min.js"></script>
    <script src="../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i" rel="stylesheet" />
    <title>Login Page </title>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" k />

    
    <script type="text/javascript">
        function openErrorModal() {
            $('#errorModal').modal('show');
        }
    </script>

    <style>
        Body {
            font-family: Calibri, Helvetica, sans-serif;
            background-color: #dcdddd;
        }

        .button {
            background-color: #F28C28;
            width: 25%;
            color: white;
            padding: 15px;
            margin: 10px 75%;
            border: none;
            cursor: pointer;
            font-weight: bold;
            border-radius: 8px;
        }

        input[type=text], input[type=password] {
            width: 100%;
            margin: 8px 0;
            padding: 12px 20px;
            display: inline-block;
            border: 2px solid #dcdddd;
            box-sizing: border-box;
        }

        .button:hover {
            opacity: 0.7;
        }

        .container {
            margin: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            -ms-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
            background-color: whitesmoke;
            padding: 25px;
            width: 50%;
            padding: 25px;
            background-color: white;
            max-width: 500px;
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
</head>
<body>
    <form runat="server">


        <div class="container">
            <center>
                <p>Human Resource Management System</p>
            </center>
            <h1 style="margin-bottom: 0px;">Login </h1>

            <asp:TextBox CssClass="form-control" ID="TbID" runat="server" placeholder="Enter EmployeeID"></asp:TextBox>
            <asp:TextBox CssClass="form-control" ID="TbPass" runat="server" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
            <a href="#">Forgot password? </a>
            <asp:Button ID="Button1" CssClass="button" runat="server" Text="Login" OnClick="Button1_Click" />


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
                            <i class="fa fa-times-circle failed"></i>
                            <p>Incorrect EmployeeID and Password</p>
                            <p>Please make sure that Employee ID is valid and ONLY in NUMBER</p>
                            <p>Please make sure that Password is valid</p>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <i class=" icon-checkmark3 position-right"></i>
                        <asp:Button ID="Button2" runat="server" class="btn btn-danger errorConfirmBtn" Text="Confirm" />

                    </div>
                </div>
            </div>
        </div>

    </form>



</body>
</html>
