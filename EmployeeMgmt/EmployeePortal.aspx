<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeePortal.aspx.cs" Inherits="EmployeeManagement.EmployeePortal" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Employee Portal</h1>
        <p class="lead">Use this portal to manage hours and expenses - Files uploaded are saved to local storage </p>
        </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Manage Timesheets</h2>
            <p>
                Select timesheet file to submit</p>
            <p>
             
             <asp:FileUpload id="FileUploadControl" class="btn btn-default"  runat="server" /><br />
            <asp:Button runat="server" id="UploadButton" class="btn btn-primary"  text="Upload Timesheet" onclick="UploadButton_Click" />
            <br /><br />
            <asp:Label runat="server" id="StatusLabel" text="Upload status: " />

            </p>
        </div>
        
        
    </div>

</asp:Content>
