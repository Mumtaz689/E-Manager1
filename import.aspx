<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="import.aspx.cs" Inherits="import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <h2>Import Mail</h2>

            </div>

        </div><script src="js/js_scheduler.js"></script>

        <div class="row">
            <div class="col-lg-6">

                <div class="row">
                    <div class="col-lg-12">

                              <label>Mail Category :</label>   <asp:Label ID="lblmesg" runat="server" Text="" ForeColor="Red"></asp:Label>
                         <asp:DropDownList ID="ddlmailcategory" CssClass="form-control alertmsg" runat="server"></asp:DropDownList>


           <label>    Select file for import:</label> <asp:FileUpload ID="ffimport" runat="server" />
                           <asp:Button ID="btnimport"  OnClick="btnimport_Click" runat="server" Text="Import" />
                        <span style="font-size:10px; font-family:Verdana; color:#ff6a00">Only .xls, .xlsx File allowed</span>
                    </div>

                </div>

      

            </div>
                <div class="col-lg-6">
                 <span style="font-size:10px; font-family:Verdana; color:#ff6a00">Mail category code</span>

                 
                    <asp:GridView ID="grdcategory" runat="server" CssClass="table-responsive table"  AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Category Code">
                                <ItemTemplate>
                                    <%#Eval("ID")%>
                                </ItemTemplate>
                            </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Category">
                                <ItemTemplate>
                                    <%#Eval("mailcategory")%>
                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>



                    </asp:GridView>

                    <asp:LinkButton ID="lnkdownload" OnClick="lnkdownload_Click" runat="server">Download Template</asp:LinkButton>
                    </div>

        </div>
       

       <div class="row">
           <div class="col-lg-12">
                  <asp:Label ID="lblcounter" runat="server" Text="" ForeColor="Red"></asp:Label>
               <asp:GridView ID="grdimport" runat="server" AutoGenerateColumns="false" >
                   <Columns>

                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                            
                            <HeaderTemplate>
                                    <asp:CheckBox ID="chkall" AutoPostBack="true" OnCheckedChanged="chkall_CheckedChanged" Text="All" runat="server" />

                            </HeaderTemplate>
                            </asp:TemplateField>

                       <asp:TemplateField HeaderText="Code">
                           <ItemTemplate>
                               <asp:Label ID="lblcode" runat="server" Text='<%#Eval("code")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>
                          <asp:TemplateField HeaderText="email">
                           <ItemTemplate>
                               <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>
                        <asp:TemplateField HeaderText="name">
                           <ItemTemplate>
                               <asp:Label ID="lblname" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>
                          <asp:TemplateField HeaderText="mobile">
                           <ItemTemplate>
                               <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>
                          <asp:TemplateField HeaderText="phone">
                           <ItemTemplate>
                               <asp:Label ID="lblphone" runat="server" Text='<%#Eval("phone")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>
                          <asp:TemplateField HeaderText="zip">
                           <ItemTemplate>
                               <asp:Label ID="lblzip" runat="server" Text='<%#Eval("zip")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>


                         <asp:TemplateField HeaderText="hotelname">
                           <ItemTemplate>
                               <asp:Label ID="lblhotelname" runat="server" Text='<%#Eval("hotelname")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>
                             <asp:TemplateField HeaderText="city">
                           <ItemTemplate>
                               <asp:Label ID="lblcity" runat="server" Text='<%#Eval("city")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>
                            <asp:TemplateField HeaderText="country">
                           <ItemTemplate>
                               <asp:Label ID="lblcountry" runat="server" Text='<%#Eval("country")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>

                        <asp:TemplateField HeaderText="address1">
                           <ItemTemplate>
                               <asp:Label ID="lbladdress1" runat="server" Text='<%#Eval("address1")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>
                         <asp:TemplateField HeaderText="address2">
                           <ItemTemplate>
                               <asp:Label ID="lbladdress2" runat="server" Text='<%#Eval("address2")%>'></asp:Label>
                                   
                           </ItemTemplate>
                       </asp:TemplateField>












                   </Columns>

               </asp:GridView>
               <asp:Button ID="btnupload"  OnClick="btnupload_Click" Visible="false" OnClientClick="return confirm('Are you sure ')" runat="server" Text="Upload" />
           </div>

         </div>

    </div>







</asp:Content>

