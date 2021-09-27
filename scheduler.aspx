<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" ValidateRequest = "false"  AutoEventWireup="true" CodeFile="scheduler.aspx.cs" Inherits="scheduler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="css/editor.css" rel="stylesheet" />
    <script src="js/editor.js"></script>
     <style type="text/css">
        .ui-icon-circle-triangle-e{
            background-color:black;
        }
        .ui-icon-circle-triangle-w {
              background-color:black;
        }
    </style>
     <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="js/jquery-ui.js"></script>

      <script>
          $(document).ready(function () {
              $("#txtEditor").Editor();
              $(".date").datepicker();
          });
		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <div class="container-fluid">
			<div class="row"><script src="js/js_scheduler.js"></script>
				
				<div class="container">


                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>

                        </div>

                    </div>

                    <div class="row">
                        <div class="col-lg-4">
                               <label>Add Scheduler</label>
                        </div>
                            <div class="col-lg-4">
                                <asp:Label ID="lblmessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </div>
                    </div>

					<div class="row">
                             <label>Mail category :</label>
                                 
                         <asp:DropDownList ID="ddlmailcategory" CssClass="form-control alertmsg" runat="server"></asp:DropDownList>


                     <label>From Mail:</label>
                        <asp:DropDownList ID="ddlfrommail" CssClass="form-control" runat="server"></asp:DropDownList>
                           <label title=" carbon copy"> TO:</label>:<asp:TextBox ID="txtto"  CssClass="form-control alertmsg"  runat="server"></asp:TextBox>
                          


                       <label title=" carbon copy"> CC:</label>:<asp:TextBox ID="txtcc"   CssClass="form-control" Placeholder="Multiple email enter with separated,(comma) Example: info@bookingaccess.com,info@bamwt.com" runat="server"></asp:TextBox>
                           <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
                         <label>BCC:</label>

                        <asp:TextBox ID="txtBCC" Placeholder="Example: info@bookingaccess.com,info@bamwt.com"  Height="80px" CssClass="form-control alertmsg"  onkeyup="return mailcounter(this.id)" TextMode="MultiLine" runat="server" ></asp:TextBox>
                        
                             <label>Subject:</label>

                         <asp:TextBox ID="txtsubjext" Placeholder=""  CssClass="form-control "  runat="server" ></asp:TextBox>
                        

                        <div class="row">

                            <div class="col-lg-4">
                                <label> Schedule Type:</label>
                                <asp:DropDownList ID="ddlscheduler"  onchange="return select(this.id)" runat="server" CssClass="form-control sche">
                                    <asp:ListItem Value="0">Custom</asp:ListItem>
                                    <asp:ListItem Value="1">Every Date</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-4">
                                <label> Scheduled Date:</label>
                                <asp:TextBox ID="txtscheduleddate" runat="server" CssClass="form-control date"></asp:TextBox>
                            </div>

                            <div class="col-lg-2">
                                  <label> Bulk Process:</label>
                                <asp:TextBox ID="txtbulk" runat="server" CssClass="form-control" TextMode="Number" Text="50"></asp:TextBox>
                            </div>
                        </div>

						<div class="col-lg-12 nopadding" id="editor" runat="server">
							<textarea id="txtEditor" ></textarea> 
						</div>
                        <asp:TextBox ID="txtbody" Height="0" Width="0" runat="server"></asp:TextBox>
                           <label> Attachment</label>:<asp:FileUpload ID="fffileupload" AllowMultiple="true" runat="server" />
                        <span style="font-size:10px; font-family:Verdana; color:#ff6a00">You can select maximum 5 files .Per file size must have less than 3MB</span>
                        <br />

                        <asp:Button ID="btnadd" OnClick="btnadd_Click" OnClientClick="return validation()" runat="server" Text="Add" />
                        <br />
                        <br />

					</div>
				</div>
			</div>
		</div>
    <asp:HiddenField ID="hdinvalidemail"  Value="0" runat="server" />
    <script type="text/javascript">

        $(document).ready(function () {


            $(".Editor-editor").html(document.getElementById('<%=txtbody.ClientID%>').value);


        });



        function select(id) {
            try {

              

                //if ($(".sche").val() == 1)
                //{

                //    $('.date').attr('readonly', 'true');
                //}
                //else
                //{
                //    $('.date').attr('readonly', 'false');

                //}
            }
            catch (ex) { }

        }




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
    <script src="js/js_scheduler.js"></script>
</asp:Content>

