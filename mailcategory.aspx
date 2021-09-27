<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="mailcategory.aspx.cs" Inherits="mailcategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="container">
<div class="row">
<div class="col-lg-1">
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</div>
<div class="col-lg-6">
<asp:Label ID="lblmessage" ForeColor="#ff3300" runat="server" Text=""></asp:Label>
<script src="js/js_scheduler.js"></script>
</div>

</div>
<div class="row">
          
<div class="col-lg-2">
<label>Mail Category:</label>
</div>
<div class="col-lg-4">
<asp:TextBox ID="txtcategory"  placeholder="Enter category like : Agent mails Category" CssClass="form-control" runat="server"></asp:TextBox>
</div>
<div class="col-lg-1">
<asp:Button ID="btnadd"  CssClass="btn-default btn" OnClick="btnadd_Click" runat="server" Text="Add" />
              
</div>

</div>
<div class="row">
<div class="col-lg-2">
<label>Status:</label>
</div>
<div class="col-lg-6">
<asp:RadioButtonList ID="rdactive" runat="server" RepeatDirection="Horizontal">
<asp:ListItem Value="0" Selected="True" >Active</asp:ListItem>
<asp:ListItem Value="1">In-Active</asp:ListItem>
</asp:RadioButtonList>
              
</div>
</div>
        
<div class="row">
         
           
<div class="col-lg-4">
<small id="counter" runat="server"></small>
</div>
          
    <%--  --%>
</div>
<div class="row">
         
           
<div class="col-lg-12">

<div  style="overflow:auto">
<asp:GridView ID="grdmailcategory" runat="server" OnRowUpdating="grdmailcategory_RowUpdating" AutoGenerateColumns="false" CssClass="table-bordered table">
<Columns>
<asp:TemplateField HeaderText="Mail category">
<ItemTemplate>
<asp:Label ID="lblcategory" runat="server" Text='<%#Eval("mailcategory")%>'></asp:Label>
<asp:HiddenField ID="hdid" runat="server" Value='<%#Eval("id")%>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Status">
<ItemTemplate>
<asp:HiddenField ID="hdstatus" runat="server" Value='<%#Eval("status")%>' />
<span><%# Eval("status").ToString() == "1" ? "In-active" : "Active" %></span>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="">
<ItemTemplate>
                              
<asp:LinkButton ID="lnkedit" CommandName="update" runat="server">Edit</asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
</Columns>

</asp:GridView>
</div>
            
</div>
          

</div>

       
</div>

</asp:Content>

