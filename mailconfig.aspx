<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="mailconfig.aspx.cs" Inherits="mailconfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container">
        <div class="row">
            <div class="col-lg-4">
                <label>Email address:</label>
                <asp:TextBox ID="txtemail" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>

            </div> <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
            <script src="js/js_scheduler.js"></script>
        </div>
        <div class="row">
            <div class="col-lg-4">
                <label>Password:</label> 
                <asp:TextBox ID="txtpassword" autocomplete="off" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                <small>Your password and other details are confidental and secure</small>

            </div>

        </div>
        <div class="row">
            <div class="col-lg-4">
                <label>SMTP server:</label>
                <asp:TextBox ID="txtserver" CssClass="form-control" runat="server"></asp:TextBox>
                <small>smtp.gmail.com (for only gmail account)</small>

            </div>

        </div>
           <div class="row">
            <div class="col-lg-4">
                <label>Port:</label>
                <asp:TextBox ID="txtport" CssClass="form-control" runat="server" Text="25"></asp:TextBox>
                   <small>587 (for only gmail account)</small>

            </div>

        </div>
         <div class="row">
            <div class="col-lg-4">
                <label>Status:</label>
                <asp:RadioButtonList ID="rdstatus" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Selected="True" >Active</asp:ListItem>
                    <asp:ListItem Value="1">In-Active</asp:ListItem>

                </asp:RadioButtonList>

            </div>

        </div>

         <div class="row">
            <div class="col-lg-2">
                <label>EnableSsl:</label>
                <asp:DropDownList ID="ddlenablessl" CssClass="form-control" runat="server">
                    <asp:ListItem Value="false" Selected="True">false</asp:ListItem>
                    <asp:ListItem Value="true">true</asp:ListItem>
                </asp:DropDownList>

            </div>

        </div>

        <div class="row">
            <div class="col-lg-12">
                <asp:Button ID="btnadd" runat="server" OnClick="btnadd_Click" Text="Add mail" /><asp:Label ID="lblmessage" runat="server" Text="" ForeColor="Red"></asp:Label>

            </div>

        </div>

        <div class="row">
            <div class="col-lg-12" style="overflow:auto">
                <asp:Label ID="lblcount" runat="server" Text="" ForeColor="Red"></asp:Label>
                <asp:GridView ID="grdmail" runat="server" OnRowUpdating="grdmail_RowUpdating" AutoGenerateColumns="false" CssClass="table-bordered table">
                    <Columns>
                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email")%>'></asp:Label>
                                <asp:HiddenField ID="hdstatus" runat="server"  Value='<%#Eval("status")%>'/>
                                  <asp:HiddenField ID="hdid" runat="server"  Value='<%#Eval("id")%>'/>
                            </ItemTemplate>
                            <ItemStyle Width="20%" />


                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Password">
                            <ItemTemplate>
                                <asp:Label ID="lblpassword" runat="server" Text='<%#Eval("password")%>'></asp:Label>
                            </ItemTemplate>
                                 <ItemStyle Width="40%" />

                        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Smtp">
                            <ItemTemplate>
                                <asp:Label ID="lblsmtp" runat="server" Text='<%#Eval("smtp")%>'></asp:Label>
                            </ItemTemplate>

                                  <ItemStyle Width="20%" />
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Port">
                            <ItemTemplate>
                                <asp:Label ID="lblport" runat="server" Text='<%#Eval("port")%>'></asp:Label>
                            </ItemTemplate>

                                <ItemStyle Width="10%" />
                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="EnableSsl">
                            <ItemTemplate>
                                <asp:Label ID="lblEnableSsl" runat="server" Text='<%#Eval("EnableSsl")%>'></asp:Label>
                            </ItemTemplate>

                                <ItemStyle Width="10%" />
                        </asp:TemplateField>

                        
                          <asp:TemplateField HeaderText="status">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("statustype")%>'></asp:Label>
                            </ItemTemplate>
                               <ItemStyle Width="5%" />

                        </asp:TemplateField>
                           <asp:TemplateField >
                            <ItemTemplate>
                                <asp:Button ID="btnedit" CommandName="update" runat="server" Text="Edit" />
                            </ItemTemplate>

                                <ItemStyle Width="5%" />
                        </asp:TemplateField>
                    </Columns>


                </asp:GridView>



            </div>

        </div>
    </div>
</asp:Content>

