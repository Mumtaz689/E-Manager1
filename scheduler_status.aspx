<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="scheduler_status.aspx.cs" Inherits="scheduler_status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/js_scheduler.js"></script>

 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <div class="container">
        <div class="row">

            <div class="col-lg-12">
                <asp:Label ID="lblcounter" ForeColor="Red" runat="server" Text=""></asp:Label>   <asp:Label ID="lblmessage" ForeColor="Red" runat="server" Text=""></asp:Label>
                <div class="row">
                    <div class="col-lg-12">

                    
                        <asp:GridView ID="grdscheduler" OnRowDataBound="grdscheduler_RowDataBound" OnRowDeleting="grdscheduler_RowDeleting" OnRowUpdating="grdscheduler_RowUpdating" CssClass="table-responsive table" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="From Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblfrom" runat="server" Text='<%#Eval("email")%>'></asp:Label>
                                        <asp:HiddenField ID="hid" runat="server"  Value='<%#Eval("id")%>'/>
                                         <asp:HiddenField ID="hdstatus" runat="server"  Value='<%#Eval("status")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmail_category" runat="server" Text='<%#Eval("mail_category")%>'></asp:Label>
                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Subject">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsubject" runat="server" Text='<%#Eval("subject")%>'></asp:Label>
                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Scheduled Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblscheduleddate" runat="server" Text='<%#Eval("scheduler_status_date")%>'></asp:Label>
                     
                                    </ItemTemplate>

                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Schedule Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblscheduletype" runat="server" Text='<%#Eval("daily")%>'></asp:Label>
                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bulk">
                                    <ItemTemplate>
                                        <asp:Label ID="lblbulk" runat="server" Text='<%#Eval("bulk")%>'></asp:Label>
                     
                                    </ItemTemplate>
                                </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotal" runat="server" Text='<%#Eval("total")%>'></asp:Label>
                     
                                    </ItemTemplate>
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Active Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcreateddate" runat="server" Text='<%#Eval("createddate")%>'></asp:Label>
                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcurrentsatus" runat="server" Text='<%#Eval("currentstatus")%>'></asp:Label>
                     
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                     
                                        

                                        <asp:LinkButton style="text-decoration:underline" ID="lnkdeactive" CommandName="update" runat="server">Deactive</asp:LinkButton>|
                                          <asp:LinkButton style="text-decoration:underline" ID="lbldelete" CommandName="delete" OnClientClick="return confirm('Are you sure\n You want to delete this schedule')" runat="server">Delete</asp:LinkButton>|
                                       
                                        <a onclick="return openpopup(<%#Eval("id")%>)" style="text-decoration:underline">View</a>
                     
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                        </asp:GridView>
                    </div>

                </div>
            </div>

            </div>
         </div>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>

