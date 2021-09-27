<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="followreport.aspx.cs" Inherits="followreport" %>

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
            $(".datefrom").datepicker();
            $(".dateto").datepicker();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
       <div class="row">
        <div class="col-lg-6">
            <h4>Search/FollowUp Report </h4>
         
        </div>
        
    </div>
 
    <div class="row">
       <div class="col-lg-2">
           <label>Date Form</label>
       </div>
        <div class="col-lg-4">
            <asp:TextBox ID="txtdateform" CssClass="form-control datefrom" onkeyup="return false" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-2">
            <label>Date To</label>
        </div>
        <div class="col-lg-4">
            <asp:TextBox ID="txtdateto" runat="server" onkeyup="return false" CssClass="form-control dateto"></asp:TextBox>
        </div>
        <div class="row">
            <div class="col-lg-12 text-center" style="padding-top:30px;">
                <asp:Button ID="btn_search" runat="server" CssClass="btn-primary btn" OnClick="btn_search_Click" Text="Search" />
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12 text-left">
                <asp:LinkButton ID="lnkexport" runat="server" OnClick="lnkexport_Click" Style="text-decoration:underline">Export</asp:LinkButton>
            </div>
        </div>
           <div class="row">
<asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
       
        <div class="row" style="padding-top:30px;" >
            <asp:GridView ID="gdvfollowreport" CssClass="table-responsive table" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText=" Person Name">
                        <ItemTemplate>
                            <asp:Label ID="lblpersonname" runat="server" Text='<%#Eval("person_name") %>'></asp:Label>
                        </ItemTemplate>
                   </asp:TemplateField>
                      <asp:TemplateField HeaderText=" Email">
                        <ItemTemplate>
                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                        </ItemTemplate>
                   </asp:TemplateField>
                      <asp:TemplateField HeaderText=" Mobile">
                        <ItemTemplate>
                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("mobile") %>'></asp:Label>
                        </ItemTemplate>
                   </asp:TemplateField>


                       <asp:TemplateField HeaderText="Contact By">
                        <ItemTemplate>
                            <asp:Label ID="lblfollowupdate" runat="server" Text='<%#Eval("t") %>'></asp:Label>
                        </ItemTemplate>
                   </asp:TemplateField>
                       <asp:TemplateField HeaderText="Follow Up Date">
                        <ItemTemplate>
                            <asp:Label ID="lblfollowupdate" runat="server" Text='<%#Eval("followupdate") %>'></asp:Label>
                        </ItemTemplate>
                   </asp:TemplateField>
                   <%--      <asp:TemplateField HeaderText="Contact Date">
                        <ItemTemplate>
                            <asp:Label ID="lblfollowupdate" runat="server" Text="#"></asp:Label>
                        </ItemTemplate>
                   </asp:TemplateField>--%>
                   


                </Columns>

            </asp:GridView>
        </div>
      
      
    </div>

</asp:Content>

