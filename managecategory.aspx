<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="managecategory.aspx.cs" Inherits="managecategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="row">

        <div class="col-lg-4">
              <h4>Mail Category Manage</h4>
        </div>

         <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
        <div class="col-lg-8">
              <asp:Label ID="lblmessage" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>

</div>

    
    <div class="row">

            
            <div class="col-lg-4">
                <label>Mail Category</label>
                <asp:DropDownList ID="ddlmailcategory" CssClass="form-control alertmsg" runat="server"></asp:DropDownList>
            



            </div>
          <div class="col-lg-4">
                <label>Identification</label>
                <asp:DropDownList ID="ddlidentification" CssClass="form-control alertmsg" runat="server">
                    <asp:ListItem Value="0">All</asp:ListItem>
                    <asp:ListItem Value="1">Unidentified Mails</asp:ListItem>
                    <asp:ListItem Value="2">Identified mails</asp:ListItem>

                </asp:DropDownList>
            



            </div>
            </div>

     <div class="row">
       
            
            <div class="col-lg-12" style="overflow:auto">
                <label>Enter Emails:</label>
                <asp:TextBox ID="txtmails" CssClass="form-control" Height="80px" TextMode="MultiLine"  runat="server"></asp:TextBox> 
                 <span style="font-size:10px; font-family:Verdana; color:#ff6a00">Every emails must have with comma separated(,) like : info@bookingaccess.com, info@bamwt.com . Do't use comma for single mail like info@bookingaccess</span>



            </div>
          
            </div>
    <div class="row">
        <div class="col-lg-12">
             <asp:Button ID="btnsearch" OnClick="btnsearch_Click" runat="server" Text="Search" />
            <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" Text="Add" />
            <asp:Button ID="btnupdate"  OnClientClick="return confirm('Are you sure\n You want to  Update records')"  runat="server" OnClick="btnupdate_Click" Text="Update" />
               <asp:Button ID="Btnsend" OnClick="Btnsend_Click"  OnClientClick="return confirm('Are you sure\n You want to send new mail on selected email address')" runat="server" Text="Send mails"    />

            <asp:Button ID="btnscheduler" OnClick="btnscheduler_Click"  OnClientClick="return confirm('Are you sure\n You want to add scheduler of selected email address')" runat="server" Text="Add scheduler"    />
            <asp:Button ID="btntag" OnClick="btntag_Click"  runat="server" Text="Tag"    />
        </div>

    </div><script src="js/js_scheduler.js"></script>

            <div class="row">
              <div class="col-lg-6 text-left">
                  <asp:Label ID="lblcounter" ForeColor="Red" runat="server" Text=""></asp:Label>      <asp:Label ID="lblselection" ForeColor="Green" runat="server" Text=""></asp:Label>

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
        <div class="col-lg-12" style="overflow:auto">
            <asp:GridView ID="grdmail" runat="server" CssClass="table table-responsive " OnRowDeleting="grdmail_RowDeleting" OnRowUpdating="grdmail_RowUpdating" OnRowDataBound="grdmail_RowDataBound" AutoGenerateColumns="false">
                <Columns>

                    <asp:TemplateField>
                        <ItemTemplate>

                            <asp:CheckBox ID="chkselect"  runat="server"  Text='<%#" "+Eval("RowNumber") %>'/>
                     </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkall" OnCheckedChanged="chkselect_CheckedChanged" AutoPostBack="true" Text="ALL" runat="server" />

                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>

                               <asp:HiddenField ID="hdmailcategoryid" runat="server"  Value='<%#Eval("mailcategoryid")%>'/>
                              <asp:HiddenField ID="hdemailid" runat="server"  Value='<%#Eval("id")%>'/>
                            
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Mail Category">
                        <ItemTemplate>

                            <asp:DropDownList ID="ddlcategory" runat="server"></asp:DropDownList>
                            <asp:Button ID="btnupdaterow" OnClientClick="return confirm('Are you sure\n You want to change mail category')" CommandName="update" runat="server" Text="Move" />

                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="">
                        <ItemTemplate>

                            
                            <a href='<%#"mailsetup.aspx?mid="+Eval("id")%>'>Update Detail</a>

                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                        <ItemTemplate>

                            <asp:Button ID="btndelete" OnClientClick="return confirm('Are you sure\n You want to delete this email')" CommandName="delete" runat="server" Text="Delete" />


                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>


            </asp:GridView>
                    <asp:Repeater ID="rptPager" runat="server">
<ItemTemplate>
    <asp:LinkButton ID="lnkPage" runat="server" Text ='<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' Enabled ='<%# Eval("Enabled") %>' OnClick = "Page_Changed"></asp:LinkButton>
</ItemTemplate>
</asp:Repeater>
        </div>

    </div>

</asp:Content>

