<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="mailsetup.aspx.cs" Inherits="mailsetup"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       

<div  class="row"><div class="col-lg-6 text-left">
<h4>Add/Update Person detail</h4>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</div>
<div class="col-lg-6 text-left">
<asp:Label ID="lblmsg" ForeColor="Red" runat="server" Text=""></asp:Label>
</div>
</div>
<script src="js/js_scheduler.js"></script>



<div class="row">
       
            
<div class="col-lg-4">
<label>Mail Category</label>
<asp:DropDownList ID="ddlmailcategory" CssClass="form-control alertmsg" runat="server"></asp:DropDownList>
</div>
<div class="col-lg-4">
<label>Person Name</label>
<asp:TextBox ID="txtpersonname" CssClass="form-control" runat="server"></asp:TextBox>
                

</div>
<div class="col-lg-4">
<label>Email<small>*</small></label>
<asp:TextBox ID="txtemail" CssClass="form-control alertmsg" runat="server"></asp:TextBox>
            

</div>
</div>


    
<div class="row">
<div class="col-lg-4">
<label>Phone</label>
<asp:TextBox ID="txtphone" CssClass="form-control" runat="server"></asp:TextBox>

</div>
<div class="col-lg-4">
<label>Mobile</label>
<asp:TextBox ID="txtmobile" CssClass="form-control " runat="server"></asp:TextBox>
</div>
<div class="col-lg-4">
<label>Zip/Postal code</label>
<asp:TextBox ID="txtzip" CssClass="form-control" runat="server"></asp:TextBox>
</div>

</div>
<div class="row">
<div class="col-lg-4">
<label>Organization/Company/Hotel</label>
<asp:TextBox ID="txtorganization" CssClass="form-control" runat="server"></asp:TextBox>

</div>
<div class="col-lg-4">
<label>City</label>
<asp:TextBox ID="txtcity" CssClass="form-control" runat="server"></asp:TextBox>
</div>
<div class="col-lg-4">
<label>Country</label>
<asp:TextBox ID="txtcountry" CssClass="form-control" runat="server"></asp:TextBox>
</div>

</div>

<div class="row">
<div class="col-lg-4">
<label>Address1</label>
<asp:TextBox ID="txtaddress1" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
</div>
<div class="col-lg-4">
<label>Address2</label>
<asp:TextBox ID="txtaddress2" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>

</div>
<div class="col-lg-4">

<label>Remark</label>
    <asp:TextBox ID="txtremark" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
              </div>
<div class="row">
<div class="col-lg-12 text-center">
<asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" OnClientClick="return confirm('Are you sure\n All fillup details are right')" Text="Add" />&nbsp
    <asp:Button  OnClick="bntsearch_Click" ID="bntsearch" runat="server" Text="Search" />
<asp:Button  OnClick="btnsend_Click" ID="btnsend" runat="server" Text="Send mail" /> 
 <asp:Button  OnClick="btnscheduler_Click" ID="btnscheduler" runat="server" Text="Scheduler" />    
 <asp:Button  OnClick="btntag_Click" ID="btntag" runat="server" Text="Tag" />

</div>

</div>

<div class="row">
<div class="col-lg-6 text-left"  >
<asp:Label ID="lblcounter" ForeColor="Red" runat="server" Text=""></asp:Label>      
<asp:Label ID="lblselection" ForeColor="Green" runat="server" Text=""></asp:Label>

</div>
    
<div class="col-lg-6 text-right"  >
<asp:DropDownList ID="ddlfile" runat="server">
                 
<asp:ListItem Value="excel">Excel</asp:ListItem>
<asp:ListItem Value="doc">Word .doc</asp:ListItem>
                                      
              
</asp:DropDownList>
<asp:LinkButton ID="lnkexport" OnClick="lnkexport_Click"  style="text-decoration:underline" runat="server">Export</asp:LinkButton>&nbsp &nbsp Page Size
<asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                      
<asp:ListItem Value="10"  >10</asp:ListItem>
<asp:ListItem Value="20"  >20</asp:ListItem>
<asp:ListItem Value="50"  >50</asp:ListItem>
<asp:ListItem Value="100" Selected="True">100</asp:ListItem>
<asp:ListItem Value="150" >150</asp:ListItem>
<asp:ListItem Value="200" >200</asp:ListItem>
<asp:ListItem Value="300" >300</asp:ListItem>
<asp:ListItem Value="500" >500</asp:ListItem>
<asp:ListItem Value="700" >700</asp:ListItem>
<asp:ListItem Value="800" >800</asp:ListItem>
<asp:ListItem Value="1000" >1000</asp:ListItem>
<asp:ListItem Value="99999999" >All</asp:ListItem>
</asp:DropDownList>
</div>

</div>



<div class="row">
<div class="col-lg-12">
<asp:GridView ID="grdperson" runat="server" Font-Size="10px" AutoGenerateColumns="false"  OnRowUpdating="grdperson_RowUpdating" CssClass="table table-responsive">

<Columns>



<asp:TemplateField HeaderText="">
<ItemTemplate>
    <asp:RadioButton runat="server" ID="ckh" />

<%--<asp:CheckBox ID="ckh" runat="server" />--%>

        </ItemTemplate>
<HeaderTemplate>
<%--<asp:CheckBox ID="ckhall" AutoPostBack="true" OnCheckedChanged="ckhall_CheckedChanged"  runat="server" />--%>
    <asp:RadioButton ID="ckhall"  AutoPostBack="true" OnCheckedChanged="ckhall_CheckedChanged1" runat="server"/>
</HeaderTemplate>
                       
</asp:TemplateField>


<asp:TemplateField HeaderText="Name">
<ItemTemplate>
<asp:Label  id="lblperson" runat="server" Text='<%#Eval("person") %>'></asp:Label>


<asp:HiddenField ID="hdcategorymail" runat="server" value='<%#Eval("categortyid") %>' />
<asp:HiddenField ID="hdpersonid" runat="server" value='<%#Eval("id") %>' />

</ItemTemplate>
<ItemStyle Width="10%" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Mail Category">
<ItemTemplate>
<asp:Label  id="lblmail_category" runat="server" Text='<%#Eval("mail_category") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="10%" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Email">
<ItemTemplate>
<asp:Label  id="lblemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="10%" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Phone">
<ItemTemplate>
<asp:Label  id="lblphone" runat="server" Text='<%#Eval("phone") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="10%" />
</asp:TemplateField>
                   
<asp:TemplateField HeaderText="Mobile">
<ItemTemplate>
<asp:Label  id="lblmobile" runat="server" Text='<%#Eval("mobile") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="10%" />
</asp:TemplateField>
                     
<asp:TemplateField HeaderText="Zip code">
<ItemTemplate>
<asp:Label  id="lblzipcode" runat="server" Text='<%#Eval("zipcode") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="5%" />
</asp:TemplateField>
                          
<asp:TemplateField HeaderText="Company">
<ItemTemplate>
<asp:Label  id="lblcomany" runat="server" Text='<%#Eval("company") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="5%" />
</asp:TemplateField>
<asp:TemplateField HeaderText="City">
<ItemTemplate>
<asp:Label  id="lblcity" runat="server" Text='<%#Eval("city") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="5%" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Country">
<ItemTemplate>
<asp:Label  id="lblcountry" runat="server" Text='<%#Eval("country") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="5%" />
</asp:TemplateField>
<asp:TemplateField HeaderText="Address1/Address2">
<ItemTemplate>
<asp:Label  id="lbladdress1" runat="server" Text='<%#Eval("address1") %>'></asp:Label>
<asp:Label  id="lbladdress2" runat="server" Text='<%#Eval("address2") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="15%" />
</asp:TemplateField>

<asp:TemplateField HeaderText="Remark">
<ItemTemplate>
<asp:Label  id="lblremark" runat="server" Text='<%#Eval("remark") %>'></asp:Label>
</ItemTemplate>
<ItemStyle Width="10%" />
</asp:TemplateField>
<asp:TemplateField HeaderText="">
<ItemTemplate>  
<asp:LinkButton ID="lbledit" runat="server" CommandName="update">Edit</asp:LinkButton>

<a href="followup.aspx?id=<%#Eval("id") %>">Follow Up</a>
</ItemTemplate>
<ItemStyle Width="5%" />
</asp:TemplateField>

</Columns>



</asp:GridView>
<asp:Repeater ID="rptPager" runat="server">
<ItemTemplate>
<asp:LinkButton ID="lnkPage" runat="server" Text = '<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' Enabled = '<%# Eval("Enabled") %>' OnClick = "Page_Changed"></asp:LinkButton>
</ItemTemplate>
</asp:Repeater>

</div>

</div>



</asp:Content>

