<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="followup.aspx.cs" Inherits="followup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<style type="text/css">
.ui-icon-circle-triangle-e{
background-color:black;
}
.ui-icon-circle-triangle-w {
background-color:black;
}
</style>
<script src="js/js_scheduler.js"></script>
<link href="css/jquery-ui.css" rel="stylesheet" />
<script src="js/jquery-ui.js"></script>
<script type="text/javascript">

$(document).ready(function () {
$(".date").datepicker();
});

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="row">
<div class="col-lg-6">
<h4>Add/Follow Up</h4>

</div>
        
</div>
<div class="row">
<asp:Label ID="lblmsg" ForeColor="Red" runat="server"></asp:Label>
</div>
<div class="row">
     
<div class="col-lg-2">
<label>Person Name<small></small>:</label>
</div>
<div class="col-lg-2">
<asp:TextBox ID="txtpersonname" CssClass="form-control" runat="server"></asp:TextBox>
     
</div>
<div class="col-lg-2">
<label>Email<small></small>:</label>
</div>
      
<div class="col-lg-2">
<asp:TextBox ID="txtemail" CssClass="form-control" runat="server"></asp:TextBox>
</div>
<div class="col-lg-2">
<label>Mobile<small></small>:</label>
</div>
   
<div class="col-lg-2">
<asp:TextBox ID="txtmobile" runat="server" CssClass="form-control"></asp:TextBox>
</div>
</div>
<div class="row">
        
<div class="col-lg-2">
<label>Contact By<small></small>:</label>
</div>
<div class="col-lg-4">
<asp:DropDownList ID="ddlContact" runat="server" CssClass="form-control" placeholder="Select">
               
<asp:ListItem Value="0">Mobile</asp:ListItem>
<asp:ListItem Value="1">Email</asp:ListItem>
<asp:ListItem Value="2">Visit Office</asp:ListItem>
    <asp:ListItem Value="3">Other</asp:ListItem>
</asp:DropDownList>
</div>
   
<div class="col-lg-2">
<label>Follow Up Date<small></small>:</label>
</div>
<div class="col-lg-4">
<asp:TextBox ID="txtdate" CssClass="form-control date" placeholder="mm/dd/yyyy" onkeyup="return false" runat="server"></asp:TextBox>
</div>
</div>
<div class="row">
        
<div class="col-lg-2">
<label>Remark<small></small>:</label>
</div>
<div class="col-lg-6">
<asp:TextBox ID="txtremark" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
</div>
</div>
<div class="row text-center" style="padding-top:30px;">
<asp:Button ID="btn_add" runat="server" Text="Add" OnClick="btn_add_Click" CssClass="btn-primary btn" />
<asp:Button ID="btn_sendmail" OnClick="btn_sendmail_Click" runat="server" Text="Send Mail" CssClass="btn-primary btn" />


</div>
    <div class="row">
        <div class="col-lg-12 text-right">
        <asp:LinkButton ID="lnkexport" runat="server" OnClick="lnkexport_Click" style="text-decoration:underline" >Export</asp:LinkButton>
            </div>
    </div>
<div class="row" style="padding-top:30px;">
<asp:GridView ID="gdvfollowup" CssClass="table table-responsive" OnRowUpdating="gdvfollowup_RowUpdating" AutoGenerateColumns="false" runat="server">
<Columns>
       <asp:TemplateField HeaderText="Person Name">
<ItemTemplate>
<asp:Label ID="lblpersoname" runat="server" Text='<%#Eval("personname") %>'></asp:Label>
    <asp:HiddenField  ID="hdid" runat="server" Value='<%#Eval("id") %>'/>
        <asp:HiddenField  ID="hdcontactid" runat="server" Value='<%#Eval("contactid") %>'/>

</ItemTemplate>
</asp:TemplateField>
    <asp:TemplateField HeaderText="Email">
<ItemTemplate>
<asp:Label ID="lblemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Remark">
<ItemTemplate>
<asp:Label ID="lblremark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Contact By">
<ItemTemplate>
<asp:Label ID="lblcontactby" runat="server" Text='<%#Eval("contactby") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Follow Up Date">
<ItemTemplate>
<asp:Label ID="lblfollowupdate" runat="server" Text='<%#Eval("nextfollowupdate") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
    <asp:TemplateField HeaderText="">
        <ItemTemplate>
        <asp:LinkButton id="lnkedit" CommandName="update" runat="server">Edit</asp:LinkButton>
        </ItemTemplate>
    </asp:TemplateField>
 
</Columns>
</asp:GridView>
</div>
    

</asp:Content>

