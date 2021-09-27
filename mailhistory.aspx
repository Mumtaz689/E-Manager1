<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="mailhistory.aspx.cs" Inherits="mailhistory" %>

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
        })

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div class="container">
          <h4 >Mail History / Export Mail</h4>
        <div class="row">
                    <div class="col-lg-3"  >
                     <div class="row">
                    <div class="col-lg-12" >
                        <label>From Mail  :</label>
                         <asp:DropDownList ID="ddlfrommail" CssClass="form-control alertmsg" runat="server"></asp:DropDownList>
                     
                    </div>

                </div>

                </div>

           
            <div class="col-lg-3"  >
                     <div class="row">
                    <div class="col-lg-12" >
                        <label>Mail Category :</label>
                         <asp:DropDownList ID="ddlmailcategory" CssClass="form-control alertmsg" runat="server"></asp:DropDownList>
              
                    </div>

                </div>

                </div>
              <div class="col-lg-3">
                    <label>Name:</label>
                        <asp:TextBox ID="txtname" runat="server"  CssClass="form-control alertmsg"  ></asp:TextBox>
                       

            </div>
                <div class="col-lg-3">
                    <label>Phone|Mobile:</label>
                        <asp:TextBox ID="txtphone" runat="server"  CssClass="form-control alertmsg"  ></asp:TextBox>
                       

            </div>

          
            </div>
              <div class="row">


                   <div class="col-lg-3">
                   <label>Mail body text:</label>
                        <asp:TextBox ID="txtmailbody" TextMode="MultiLine" Height="40px" runat="server"  CssClass="form-control alertmsg"  ></asp:TextBox>
                       <span style="font-size:10px; font-family:Verdana; color:#ff6a00">Write mail contain text only used for hint</span>
                      

            </div>
               


            <div class="col-lg-3"  >
                     <div class="row">
                    <div class="col-lg-12" >
                        <label>Date From:</label>
                        <asp:TextBox ID="txtdatefrom" runat="server" onkeyup="return false" placeholder="Date From" CssClass="form-control date"></asp:TextBox>
                     
                    </div>

                </div>

                </div>
            <div class="col-lg-3">
                    <div class="row">
                    <div class="col-lg-12" >
                        <label>Date To:</label>
                        <asp:TextBox ID="txtdateto" runat="server"  onkeyup="return false" placeholder="Date To" CssClass="form-control date"></asp:TextBox>
                     
                    </div>

                </div>

            </div>

            <div class="col-lg-3">
                    <label>Mail Status:</label>
                      <asp:DropDownList ID="ddlstatus" CssClass="form-control alertmsg" runat="server">
                          <asp:ListItem Value="0"> All</asp:ListItem>
                            <asp:ListItem Value="1"> Sent</asp:ListItem>
                              <asp:ListItem Value="2"> Failure</asp:ListItem>
                      </asp:DropDownList>
                       

            </div>
            </div>

          <div class="row">
              <div class="col-lg-12">
                  <label>Emails:</label>
                  <asp:TextBox ID="txtmail" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                      <span style="font-size:10px; font-family:Verdana; color:#ff6a00">Every emails must have with comma separated(,) like : info@bookingaccess.com, info@bamwt.com . Do't use comma for single mail like info@bookingaccess</span>
              </div>

          </div>
            <div class="row">
              <div class="col-lg-12">
                  <label>Subject:</label>
                  <asp:TextBox ID="txtsubject"  CssClass="form-control" runat="server"></asp:TextBox>
                      
              </div>

          </div>


          <div class="row">
              <div class="col-lg-12 text-center">
                  <asp:Button ID="btnsearch"  OnClick="btnsearch_Click" runat="server" Text="Search" />

              </div>

          </div>

          <div class="row">
              <div class="col-lg-6 text-left"  >
                  <asp:Label ID="lblcounter" ForeColor="Red" runat="server" Text=""></asp:Label>

              </div>
                <div class="col-lg-6 text-right"  >
                    <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                      
                        <asp:ListItem Value="10" Selected="True"  >10</asp:ListItem>
                        <asp:ListItem Value="20"  >20</asp:ListItem>
                        <asp:ListItem Value="50"  >50</asp:ListItem>
                        <asp:ListItem Value="100"  >100</asp:ListItem>
                        <asp:ListItem Value="150" >150</asp:ListItem>
                    </asp:DropDownList>
                    </div>
          
          </div>
          <div class="row">
              <div class="col-lg-12" style="overflow:auto">
                  <asp:GridView ID="grdmailhistory"  OnRowDataBound="grdmailhistory_RowDataBound" OnRowUpdating="grdmailhistory_RowUpdating"  OnRowDeleting="grdmailhistory_RowDeleting" CssClass="table-bordered table" AutoGenerateColumns="false" runat="server">
                      <Columns>
                          <asp:TemplateField HeaderText="From Mail">

                              <ItemTemplate>
                                  <asp:Label ID="lblfrommail" runat="server" Text='<%#Eval("frommail")%>'></asp:Label>
                                  <asp:HiddenField ID="hdid" runat="server"  Value='<%#Eval("id")%>'/>
                                  <asp:HiddenField ID="hdfrommailid" runat="server"  Value='<%#Eval("frommailid")%>'/>
                                  <asp:HiddenField ID="hdmailcategory_id" runat="server"  Value='<%#Eval("mailcategory_id")%>'/>
                              </ItemTemplate>

                              <ItemStyle   Width="10%"/>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="Mail Category">

                              <ItemTemplate>
                                  <asp:Label ID="lblmailcategory" runat="server" Text='<%#Eval("mailcategory")%>'></asp:Label>
                              </ItemTemplate>
                               <ItemStyle   Width="10%"/>
                          </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subject">

                              <ItemTemplate>
                                 
                                  <asp:Label ID="lblsubject" runat="server" Text='<%#Eval("subjectmail")%>'></asp:Label>
                                     
                              </ItemTemplate>
                                <ItemStyle   Width="10%"/>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="Send Time">

                              <ItemTemplate>
                                  <asp:Label ID="lblsendtime" runat="server" Text='<%#Eval("sendtime")%>'></asp:Label>
                              </ItemTemplate>
                               <ItemStyle   Width="10%"/>
                          </asp:TemplateField>
                         <asp:TemplateField HeaderText="Status">

                              <ItemTemplate>
                                  <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("status")%>'></asp:Label>
                              </ItemTemplate>
                               <ItemStyle   Width="5%"/>
                          </asp:TemplateField>
                           <asp:TemplateField HeaderText="">

                              <ItemTemplate>
                                  
                                  <a onclick="return viewsendmaildetail(<%#Eval("id")%>)">View</a>|
                                  <asp:LinkButton ID="lnkresend" CommandName="update" runat="server">Resend</asp:LinkButton>|
                                    <asp:LinkButton ID="lnkdelete" OnClientClick="return confirm('Are you sure\you want to delete this mail')" CommandName="delete" runat="server">Delete</asp:LinkButton>
                              </ItemTemplate>
                                <ItemStyle   Width="5%"/>
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



          

          </div>

</asp:Content>

