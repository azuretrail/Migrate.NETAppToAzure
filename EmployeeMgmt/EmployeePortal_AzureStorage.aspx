<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeePortal_AzureStorage.aspx.cs" Inherits="EmployeeManagement.EmployeePortalAzureStorage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Employee Portal</h1>
        <p class="lead">Use this portal to manage hours and expenses - Files uploaded are saved to Azure storage </p>
     
    </div>

    <div class="row">
    
        
         <div class="col-md-4">
            <h2>Manage Timesheets</h2>
            <p>
                Select timesheet file to submit</p>
            <p>
             
             <asp:FileUpload id="fileUploadAzure" class="btn btn-default"  runat="server" /><br />
            <asp:Button runat="server" id="btnUploadFileAzure" class="btn btn-primary"  text="Upload Timesheet" onclick="fileUploadAzure_Click" />
            <br /><br />
            <asp:Label runat="server" id="StatusLabelAzure" text="Upload status: " />

            </p>
        </div>
    </div>

</asp:Content>
