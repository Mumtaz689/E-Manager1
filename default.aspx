<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Mail-Manager</title>
<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<link rel="Shortcut Icon" href="img/nav.png" />

<link href="css/bootstrap.css" rel="stylesheet" />
<link href="css/font-awesome.css" rel="stylesheet" />
<link href="css/font-awesome.min.css" rel="stylesheet" />

<link href="css/bootstrap.min.css" rel="stylesheet" />
<script src="js/bootstrap.min.js"></script>
<script src="js/jquery.min.js"></script>

<style  type="text/css">
.checkbox label, .radio label {
padding-left:0px;
}

</style>
</head>
<body class="jumbotron">
<form id="form1" runat="server">
    
<div >

<div class="row">
<div class="col-lg-4"  >

</div>
<div class="col-lg-4" style="background-color:white">
<div style="padding:5px" >
<img src="img/logoblack.png" draggable="false" />

<h4>User Login</h4>

<div class="form-group">
<label for="email">Email:</label>
 <asp:TextBox ID="txtemail" runat="server" class="form-control" placeholder="Enter email" TextMode="Email"></asp:TextBox>
</div>
<div class="form-group">
<label for="pwd">Password:</label>
<asp:TextBox ID="txtpwd" runat="server" class="form-control" placeholder="Enter password" TextMode="Password"></asp:TextBox>
</div>
<div class="checkbox">
<asp:CheckBox ID="chkremember" runat="server"  Text="Remember me"/>
<a href="#">Forget Password?</a>
</div>
<asp:Button ID="btnlogin" CssClass="btn btn-default" runat="server" Text="Login"  OnClick="btnlogin_Click"/>
<asp:Label ID="lblmsg" ForeColor="Red" runat="server" Text=""></asp:Label>
</div>
</div>
</div>
</div>
</form>
</body>
</html>
