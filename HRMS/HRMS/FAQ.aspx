<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="HRMS.FAQ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">

    <style>
        .accordionFAQ {
            background-color: #eee;
            color: black;
            cursor: pointer;
            padding: 18px;
            width: 100%;
            border: none;
            text-align: left;
            outline: none;
            font-size: 15px;
            transition: 0.4s;
            margin-bottom: 0;
        }

            .active1, .accordionFAQ:hover {
                background-color: #ccc;
            }

            .accordionFAQ:after {
                content: '\25BC';
                color: #777;
                font-weight: bold;
                float: right;
                margin-left: 5px;
            }

        .active1:after {
            content: "\25B2";
        }

        .panel {
            padding: 0 18px;
            background-color: white;
            max-height: 0;
            overflow: hidden;
            transition: max-height 0.2s ease-out;
        }

        #evenFAQ {
            background-color: whitesmoke;
        }

            .active1, #evenFAQ:hover {
                background-color: #ccc;
            }

        .faq p {
            margin-top:8px;
            color:black;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--INSERT PAGE NAME HERE-->
    Frequently Asked Question
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

        <div class="faq">    

            <p class="accordionFAQ">What is this HRMS?</p>
            <div class="panel">
                <p>This is a Human Resource Management System (HRMS) that helps to manage the payslip, leave and claims processes</p>
            </div>

            <p id="evenFAQ" class="accordionFAQ">How to use this HRMS?</p>
            <div class="panel">
                <p>Payslip - Enter password to view the payslip</p>
                <p>Leave - Leave section to apply or manage leave</p>
                <p>Claims - Claims section to apply or manage claims</p>
            </div>

            <p class="accordionFAQ">What to do when I forget my password?</p>
            <div class="panel">
                <p>Click on the 'Forgot password' on the login page and follow the instruction</p>
            </div>

            <p id="evenFAQ" class="accordionFAQ">Who do I look for to change my bank details?</p>
            <div class="panel">
                <p>You can contact the administrator at (+65)6444 3339 during office hours or through email at <a href="">administrator@company.com</a></p>
            </div>

            <br />

        </div>







    <script>
        var acc = document.getElementsByClassName("accordionFAQ");
        var i;

        for (i = 0; i < acc.length; i++) {
            acc[i].addEventListener("click", function () {
                this.classList.toggle("active1");
                var panel = this.nextElementSibling;
                if (panel.style.maxHeight) {
                    panel.style.maxHeight = null;
                } else {
                    panel.style.maxHeight = panel.scrollHeight + "px";
                }
            });
        }
    </script>

</asp:Content>
