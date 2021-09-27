<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="tag.aspx.cs" Inherits="tag" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/js_scheduler.js"></script>
    
     <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="row">
        <div class="col-lg-6">
           <label>Tag Emails</label>

        </div>
        <div class="col-lg-6">
           

        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <label>Enter Emails:</label>
            <asp:TextBox ID="txtemail" CssClass="form-control" Height="80px" TextMode="MultiLine" runat="server"></asp:TextBox>

                <asp:Button ID="btnsearch" OnClick="btnsearch_Click" runat="server" Text="Search" />


            <asp:Button ID="btnsend" runat="server" OnClick="btnsend_Click" Text="Send Mail" /><asp:Button ID="btnaddscheduler" OnClick="btnaddscheduler_Click" runat="server" Text="Add Scheduler" />
            <asp:Button ID="btndelete" runat="server" OnClick="btndelete_Click" Text="Delete" />

        </div>

    </div>
    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblcount" runat="server" Text="" ForeColor="Red"></asp:Label>
            <asp:GridView ID="grdtag" runat="server" AutoGenerateColumns="false" CssClass="table-responsive table "> 
                <Columns>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkall"  OnCheckedChanged="chkall_CheckedChanged" AutoPostBack="true" Text="ALL" runat="server" />
                        </HeaderTemplate>
                        <ItemStyle  Width="5%"/>

                    </asp:TemplateField>


                          <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblmail" runat="server" Text='<%#Eval("mail")%>'></asp:Label>

                        </ItemTemplate>
                    
                              <ItemStyle  Width="50%"/>
                    </asp:TemplateField>






                </Columns>

            </asp:GridView>
        </div>
        
    </div>


</asp:Content>

