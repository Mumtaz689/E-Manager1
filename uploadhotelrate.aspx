<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="uploadhotelrate.aspx.cs" Inherits="uploadhotelrate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<h4>Add/Upload Hotel Rate </h4>
</div>
</div>
    <div class="row">
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div class="row">
      <div class="col-lg-2">
          <label>File Name:</label>
      </div>
      <div class="col-lg-4">
      <asp:TextBox ID="txtfilename" runat="server" CssClass="form-control"></asp:TextBox>
      </div>
  </div>
 
  <div class="row">
      <div class="col-lg-2">
          <label>Upload Date:</label>
      </div>
      <div class="col-lg-4">
      <asp:TextBox ID="txtdate" runat="server" CssClass="form-control date"></asp:TextBox>
      </div>
  </div>
       <div class="row">
        <div class="col-lg-2">
            <label>File Upload <small>*</small>:</label>
        </div>
         <div class="col-lg-4">
          <asp:FileUpload ID="ffattachment" runat="server" />
        </div>
    </div>
      <div class="row">
      <div class="col-lg-2">
          <label>Status:</label>
      </div>
      <div class="col-lg-4">
          <asp:RadioButtonList ID="rdstatus" runat="server" RepeatDirection="Horizontal">
              <asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
              <asp:ListItem Value="0">In-Active</asp:ListItem>

          </asp:RadioButtonList>
      </div>
  </div>
    
    <div class="row">
      <div class="col-lg-2">
       
      </div>
      <div class="col-lg-4">
          <asp:Button ID="btndelete" runat="server" OnClick="btndelete_Click" Text="Delete" />
                    <asp:Button ID="btnsave" OnClick="btnsave_Click" runat="server" Text="Save" />

      </div>
  </div>
    <div class="row">
        <asp:GridView ID="gduploadfile" runat="server" CssClass="table-responsive table">
            <Columns>
                <asp:TemplateField HeaderText="FIle Name">
                    <ItemTemplate>
                    
                        <asp:LinkButton ID="lnkfiledownload" CommandName="update" runat="server"><%#Eval("filename")%></asp:LinkButton>

                        <asp:HiddenField ID="hdfilepath" runat="server" Value='<%#Eval("filepath")%>' />
                         <asp:HiddenField ID="hdid" runat="server" Value='<%#Eval("Id")%>' />
                          <asp:HiddenField ID="hdstatusid" runat="server" Value='<%#Eval("[status]")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="Uploaded Date">
                    <ItemTemplate>
                        <asp:Label ID="lbluploaddate" runat="server" Text='<%#Eval("uploaddate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("statustype")%>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    
</asp:Content>

