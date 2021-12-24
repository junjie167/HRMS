<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="ManagerClaimSummary.aspx.cs" Inherits="HRMS.ManagerClaimSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    Review Claims Summary
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <style>
        .tableborder {
            border: 1px solid black;
            margin-bottom: 5px;
        }

        .view_btn {
            background-color: #F28C28;
            color: white;
        }
                   .help-tip{
                position:relative;
                bottom:5px;
                left:5px;
    text-align: center;
    background-color: #F28C28;
    border-radius: 50%;
    width: 24px;
    height: 24px;
    font-size: 14px;
    line-height: 26px;
    cursor: default;
}

.help-tip:before{
    content:'?';
    font-weight: bold;
    color:#fff;
}

.help-tip:hover .tip-border{
    display:block;
    transform-origin: 100% 0%;

    -webkit-animation: fadeIn 0.3s ease-in-out;
    animation: fadeIn 0.3s ease-in-out;

}

.help-tip .tip-border{    /* The tooltip */
    z-index:1;
    display: none;
    text-align: left;
    background-color: #1E2021;
    padding: 20px;
    width: 300px;
    position: absolute;
    border-radius: 3px;
    box-shadow: 1px 1px 1px rgba(0, 0, 0, 0.2);
    right: -276px;
    color: #FFF;
    font-size: 13px;
    line-height: 1.4;
}

.help-tip .tip-border:before{ /* The pointer of the tooltip */
    position: absolute;
    content: '';
    width:0;
    height: 0;
    border:6px solid transparent;
    border-bottom-color:#1E2021;
    right:282px;
    top:-12px;
}

.help-tip .tip-border:after{ /* Prevents the tooltip from being hidden */
    width:100%;
    height:40px;
    content:'';
    position: absolute;
    top:-40px;
    left:0;
}

/* CSS animation */

@-webkit-keyframes fadeIn {
    0% { 
        opacity:0; 
        transform: scale(0.6);
    }

    100% {
        opacity:100%;
        transform: scale(1);
    }
}

@keyframes fadeIn {
    0% { opacity:0; }
    100% { opacity:100%; }
}
.box {
  float: left;
  height: 20px;
  width: 20px;
  margin-bottom: 15px;
  border: 1px solid black;
  clear: both;
}
    </style>
    <div class="container-fluid">
        <div class="row mt-3 mb-3">
            <table>
                <tr>
                    <td>
                        <h4>List of claims submitted by others</h4>
                    </td>
                    <td>
                        <div class="help-tip">
                            <div class="tip-border">
                                <div style="margin-bottom: 17px;">
                                    <div class="box bg-success"></div>
                                    : Approved</div>
                                <div style="margin-bottom: 17px;">
                                    <div class="box bg-danger"></div>
                                    : Rejected</div>
                                <div>
                                    <div class="box bg-warning"></div>
                                    : Pending</div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="row mb-2">
            <div class="col-4">
                <asp:Label ID="lblfilerby" Text="Filter By: " runat="server"></asp:Label>
                <asp:DropDownList ID="filterclaim" class="form-control" OnSelectedIndexChanged="filterclaim_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    <asp:ListItem Text="All" Value="All"></asp:ListItem>
                    <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                    <asp:ListItem Text="Approved" Value="Approved"></asp:ListItem>
                    <asp:ListItem Text="Rejected" Value="Rejected"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-4">
                <asp:Label ID="lblsort" Text="Sort by: " runat="server"></asp:Label>
                <asp:DropDownList ID="sortclaim" class="form-control" OnSelectedIndexChanged="sortclaim_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    <asp:ListItem Text="Latest" Value="Latest"></asp:ListItem>
                    <asp:ListItem Text="Oldest" Value="Oldest"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-4">
                Entries per page:
                <asp:DropDownList ID="pageEntries" class="form-control" OnSelectedIndexChanged="pageEntries_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-12  table-responsive">
                <h3 class="text-center w-100">
                    <asp:Label ID="noclaimview" Text="Currently there are no Claims to review" Visible="false" runat="server"></asp:Label>
                </h3>
                <asp:GridView ID="claimsGV" class="w-100" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="5" OnPageIndexChanging="claimsGV_PageIndexChanging" ShowHeader="False" GridLines="None">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div id="cardcolor" runat="server">
                                    <div class="card-body">
                                        <table>
                                            <tr>
                                                <td class="col-4">
                                                    <asp:Label ID="lblclaimID" Text="Claim ID: " runat="server"></asp:Label>
                                                    <asp:Label ID="claimID" Text='<%# Eval("claimID") %>' runat="server"></asp:Label>
                                                </td>
                                                <td class="col-4">
                                                    <asp:Label ID="lblclaimType" Text="Type: " runat="server" Visible="false"></asp:Label>
                                                    <asp:Label ID="claimType" Text='<%# Eval("claimType") %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblclaimTitle" Text="Group Title: " runat="server" Visible="false"></asp:Label>
                                                    <asp:Label ID="title" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="col-3">
                                                    <asp:Label ID="lblamount" Text="Total Amt S$: " runat="server"></asp:Label>
                                                    <asp:Label ID="amount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                                </td>
                                                <td class="col-3">
                                                    <asp:Label ID="lblsubmittedby" Text="Submitted by: " runat="server"></asp:Label>
                                                    <asp:Label ID="submitName" Text='<%# Eval("SubmittedBy") %>' runat="server"></asp:Label>
                                                </td>
                                                <td class="col-3">
                                                    <asp:Label ID="lblstatus" Text="Status: " runat="server"></asp:Label>
                                                    <asp:Label ID="status" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                </td>
                                                <td class="col-3">
                                                    <asp:Button ID="viewClaim" class="btn view_btn" OnClick="viewClaim_Click" Text="View" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
