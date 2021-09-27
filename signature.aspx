<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="signature.aspx.cs" Inherits="signature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


     <link href="css/editor.css" rel="stylesheet" />
    <script src="js/editor.js"></script>
    <script>
        $(document).ready(function () {
            $("#txtEditor").Editor();
        });
		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <div class="container-fluid">
			<div class="row">
				
				<div class="container">
                    <script src="js/js_scheduler.js"></script>
                    <div class="row">
                        <div class="col-lg-12">
                               <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"></asp:Label>

                        </div>

                    </div>

					<div class="row">
                     
                             <label>Signature Category:</label>

                         <asp:TextBox ID="txtcategory"  AutoComplete="off" Placeholder="Enter Mail signature category"  CssClass="form-control "  runat="server" ></asp:TextBox>
                        <%--<span style="font-size:10px; font-family:Verdana; color:#ff6a00">BCC mails should not be greater than 100 mails</span><br />--%>

						<div class="col-lg-12 nopadding" id="editor" runat="server">
							<textarea id="txtEditor" ></textarea> 
						</div>
                        <asp:TextBox ID="txtbody" Height="0" Width="0" runat="server"></asp:TextBox>


                     

                        <asp:Button ID="btnsend"  OnClick="btnsend_Click" OnClientClick="return validation()" runat="server" Text="Save" />
                        <br />
                        <br />

					</div>


                    <div class="row">
                        <div class="col-lg-12">
                            <asp:GridView ID="grdsignature" runat="server" AutoGenerateColumns="false" OnRowUpdating="grdsignature_RowUpdating" OnRowDeleting="grdsignature_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Signature Category">
                                        <ItemTemplate>

                                            <asp:Label ID="lblcategoryname" runat="server" Text='<%#Eval("categoryname")%>'></asp:Label>
                                            <asp:HiddenField ID="hdcategotyid" runat="server" Value='<%#Eval("id")%>' />
                                                         <asp:Label ID="lblbody" Visible="false" runat="server" Text='<%#Eval("signature")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Button ID="btnedit" runat="server" Text="Edit"  CommandName="update" />
                                              
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField>
                                        <ItemTemplate>

                                            <asp:Button ID="btndelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete')" Text="Delete"  CommandName="delete" />
                                              
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>

                    </div>
				</div>
			</div>
		</div>

    <script src="js/js_scheduler.js"></script>
    <asp:HiddenField ID="hdinvalidemail"  Value="0" runat="server" />
    <script type="text/javascript">

        $(document).ready(function () {


            $(".Editor-editor").html(document.getElementById('<%=txtbody.ClientID%>').value);


        });






        function validation() {
            try {

                document.getElementById('<%=txtbody.ClientID%>').value = $(".Editor-editor").html();


            }
            catch (ex) { }

        }

        function mailcounter(id) {
            try {
                var email = document.getElementById(id).value;


                var strarray = email.split(',');
                for (var i = 0; i < strarray.length; i++) {
                    if (strarray[i] != '') {
                        if (validateEmail(strarray[i])) {
                            //  alert(strarray[i])
                            document.getElementById('<%=hdinvalidemail.ClientID%>').value = 0;

                        }
                        else {

                            document.getElementById(id).value
                            document.getElementById(id).setAttribute('onclick', 'removestyle("this.id")');
                            document.getElementById(id).style.borderColor = "red";
                            document.getElementById('<%=hdinvalidemail.ClientID%>').value = 1;

                        }
                    }
                }




            }
            catch (ex) { }


        }

    </script>
    <script src="js/jsdynamic.js"></script>









</asp:Content>

