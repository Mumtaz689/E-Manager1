<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="changepassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="row">
        <div class="col-lg-12">
            <script src="js/js_scheduler.js"></script>
            <h2 class="panel-heading">Change Password</h2>

        </div>

    </div>
    
    <div class="row">
   
        <div class="col-lg-2">
            <label>New Password:</label>
        </div>
         <div class="col-lg-4">
              <asp:TextBox ID="txtnewpwd" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>

           
        </div>
    </div>
     <div class="row">
        <div class="col-lg-1">

        </div>
        <div class="col-lg-2">
            <label>Confirm Password:</label>
        </div>
         <div class="col-lg-4">
              <asp:TextBox ID="txtconfirm" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>

           
        </div>
    </div>
     <div class="row">
        <div class="col-lg-1">

        </div>
        <div class="col-lg-2">
            
        </div>
         <div class="col-lg-4">
             <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
             <asp:Label ID="lblmessage" ForeColor="Red" runat="server" Text=""></asp:Label>

           
        </div>
    </div>
</asp:Content>

