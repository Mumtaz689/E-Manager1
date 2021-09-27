<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" ValidateRequest = "false" CodeFile="sendmail.aspx.cs" Inherits="sendmail" %>

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

                                    <label>Send Mail :</label><asp:Label ID="lblmessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                         <asp:DropDownList ID="ddlmailcategory" CssClass="form-control alertmsg" runat="server"></asp:DropDownList>


                     <label>From Mail:</label>
                        <asp:DropDownList ID="ddlfrommail" CssClass="form-control" runat="server"> </asp:DropDownList>
                           <label title=" carbon copy"> TO:</label>:<asp:TextBox ID="txtto" AutoComplete="off"  CssClass="form-control alertmsg"  runat="server"></asp:TextBox>
                          <span style="font-size:10px; font-family:Verdana; color:#ff6a00">CC mails should not be greater than 100 mails</span><br />
                       

                       <label title=" carbon copy"> CC:</label>:<asp:TextBox ID="txtcc"  AutoComplete="off"  CssClass="form-control" Placeholder="Multiple email enter with separated,(comma) Example: info@bookingaccess.com,info@bamwt.com" runat="server"></asp:TextBox>
                          <span style="font-size:10px; font-family:Verdana; color:#ff6a00">CC mails should not be greater than 100 mails</span><br />
                         <label>BCC:</label>

                        <asp:TextBox ID="txtBCC" Placeholder="Example: info@bookingaccess.com,info@bamwt.com"  Height="80px" CssClass="form-control alertmsg"  onkeyup="return mailcounter(this.id)" TextMode="MultiLine" runat="server" ></asp:TextBox>
                        <span style="font-size:10px; font-family:Verdana; color:#ff6a00">BCC mails should not be greater than 100 mails</span><br />
                             <label>Subject:</label>

                         <asp:TextBox ID="txtsubjext"  AutoComplete="off" Placeholder=""  CssClass="form-control "  runat="server" ></asp:TextBox>
                         <label>Add Signature:</label>

                                <asp:DropDownList ID="ddlsignature" AutoPostBack="true" OnSelectedIndexChanged="ddlsignature_SelectedIndexChanged" CssClass="form-control alertmsg" runat="server"></asp:DropDownList>

                        

						<div class="col-lg-12 nopadding" id="editor" runat="server">
							<textarea id="txtEditor" ></textarea> 
						</div>
                        <asp:TextBox ID="txtbody" Height="0" Width="0" runat="server"></asp:TextBox>


                           <label> Attachment</label>:<asp:FileUpload ID="fffileupload" AllowMultiple="true" runat="server" />
                        <span style="font-size:10px; font-family:Verdana; color:#ff6a00">
                            You can select maximum 5 files .Per file size must have less than 3MB</span>
                        <br />

                        <asp:Button ID="btnsend" OnClick="btnsend_Click" OnClientClick="return validation()" runat="server" Text="Send" />
                        <br />
                        <br />

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






        function validation()
        {
            try
            {

                document.getElementById('<%=txtbody.ClientID%>').value = $(".Editor-editor").html();


            }
            catch (ex) { }

        }

        function mailcounter(id)
        {
            try
            {
                var email = document.getElementById(id).value;
             
                
                var strarray = email.split(',');
                for (var i = 0; i < strarray.length; i++)
                {
                    if (strarray[i] != '')
                    {
                        if (validateEmail(strarray[i]))
                        {
                            //  alert(strarray[i])
                            document.getElementById('<%=hdinvalidemail.ClientID%>').value = 0;

                        }
                        else
                        {

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

