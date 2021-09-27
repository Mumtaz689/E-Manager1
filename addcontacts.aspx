<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" AutoEventWireup="true" CodeFile="addcontacts.aspx.cs" Inherits="addcontacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    .<script src="js/js_scheduler.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="container">

        <div class="row">
            <div class="col-lg-3">
                <label>Mode:</label>
             
               
                <select id="ddlmode" onchange="return selectmode(this.id)" class="form-control" >
                    <option value="0">Add</option>
                      <option value="1">Update</option>
                    <option value="2">Search</option>

                </select>

            </div>

        </div>
        <div class="row">

            
            <div class="col-lg-4">
                <label>Mail Category<small>*</small></label>
                <asp:DropDownList ID="ddlmailcategory" CssClass="form-control alertmsg" runat="server"></asp:DropDownList>
            



            </div>
            <div class="col-lg-4">
                       <label>Person Name</label>
                <asp:TextBox ID="txtpersonname" CssClass="form-control" runat="server"></asp:TextBox>
                

                  </div>
            <div class="col-lg-4">
                   <label>Email<small>*</small></label>
                <asp:TextBox ID="txtemail" CssClass="form-control alertmsg" runat="server"></asp:TextBox>
            

            </div>
            </div>
        <div class="row">
            <div class="col-lg-4">
                        <label>Phone</label>
                <asp:TextBox ID="txtphone" CssClass="form-control" runat="server"></asp:TextBox>

            </div>
               <div class="col-lg-4">
                   <label>Mobile</label>
                   <asp:TextBox ID="txtmobile" CssClass="form-control " runat="server"></asp:TextBox>
                   </div>
            <div class="col-lg-4">
                   <label>Zip/Postal code</label>
                   <asp:TextBox ID="txtzip" CssClass="form-control" runat="server"></asp:TextBox>
                   </div>

        </div>
              <div class="row">
            <div class="col-lg-4">
                        <label>Organization/Company/Hotel</label>
                <asp:TextBox ID="txtorganization" CssClass="form-control" runat="server"></asp:TextBox>

            </div>
               <div class="col-lg-4">
                   <label>City</label>
                   <asp:TextBox ID="txtcity" CssClass="form-control" runat="server"></asp:TextBox>
                   </div>
            <div class="col-lg-4">
                   <label>Country</label>
                   <asp:TextBox ID="txtcountry" CssClass="form-control" runat="server"></asp:TextBox>
                   </div>

        </div>

        <div class="row">
            <div class="col-lg-4">
                        <label>Address1</label>
                <asp:TextBox ID="txtaddress1" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>

            </div>
             <div class="col-lg-4">
                        <label>Address2</label>
                <asp:TextBox ID="txtaddress2" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>

            </div>
               <div class="col-lg-4">
                        <label>Remark</label>
                <asp:TextBox ID="txtremark" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>

            </div>
              

        </div>

            <div class="row">

         
            <div class="col-lg-12 text-center">

                <input id="btnadd" runat="server" type="button" value="Add" onclick="return addpersondetail()" />
                <%--  <input id="btnsearch" runat="server" type="button" value="Search"  />
                   <input id="btnupdate" runat="server" type="button" value="Update"  />--%>
                   <label id="loadingontrolid"></label>
                </div>

                </div>

        <div class="row">
            <div class="col-lg-12" id="htmlcontrolid">

            </div>


        </div>

          <div class="row">
            <div class="col-lg-12" style="overflow:auto">


                <table class="table-bordered table">
                    <thead>
                       <tr>
                           <td>Mail category</td>
                           <td>Person name</td>
                           <td>Email</td>
                           <td>Phone/Mobile</td>
                           
                           <td>Zip/postal</td>
                           <td>Company</td>
                           <td>City/Country</td>
                         
                           <td>Address1/Address2</td>
                                
                              <td>Remark</td>
                           <td>Mail Status</td>
                       </tr>


                    </thead>

                    <tbody id="Persondata">

                    </tbody>

                </table>
            </div>


        </div>




        </div>
    <script type="text/javascript">

        $(document).ready(function () 
        {
          
            getdata();
      
        });



       function addpersondetail()
        {
            try
            {

                var category = document.getElementById('<%=ddlmailcategory.ClientID%>');
                if (category.options[category.selectedIndex].value==0)
                {
                    document.getElementById('<%=ddlmailcategory.ClientID%>').style.borderColor = "red";
                    document.getElementById('<%=ddlmailcategory.ClientID%>').setAttribute('onclick', 'removestyle("this.id")');
                  //  alert('Please select mail category');
                   
                    return false;

                }
                

                if (validateEmail(document.getElementById('<%=txtemail.ClientID%>').value))
                {
               

                    if (document.getElementById('<%=txtemail.ClientID%>').value != '')
                    {

                        addperson(1, 1, category.options[category.selectedIndex].value, document.getElementById('<%=txtpersonname.ClientID%>').value, document.getElementById('<%=txtemail.ClientID%>').value, document.getElementById('<%=txtphone.ClientID%>').value, document.getElementById('<%=txtmobile.ClientID%>').value, document.getElementById('<%=txtzip.ClientID%>').value, document.getElementById('<%=txtorganization.ClientID%>').value, document.getElementById('<%=txtcity.ClientID%>').value, document.getElementById('<%=txtcountry.ClientID%>').value, document.getElementById('<%=txtaddress1.ClientID%>').value, document.getElementById('<%=txtaddress2.ClientID%>').value, document.getElementById('<%=txtremark.ClientID%>').value, 0);
                    }
                }
                else {

                    document.getElementById('<%=txtemail.ClientID%>').setAttribute('onclick', 'removestyle("this.id")');
                    document.getElementById('<%=txtemail.ClientID%>').style.borderColor = "red";
                    return false;
                
                }
            }
            catch (ex) {
                alert(ex);
            }


        }
       
        function getdata()
        {
            try{
                var category = document.getElementById('<%=ddlmailcategory.ClientID%>');
                searchperson(3, 1, category.options[category.selectedIndex].value, document.getElementById('<%=txtpersonname.ClientID%>').value, document.getElementById('<%=txtemail.ClientID%>').value, document.getElementById('<%=txtphone.ClientID%>').value, document.getElementById('<%=txtmobile.ClientID%>').value, document.getElementById('<%=txtzip.ClientID%>').value, document.getElementById('<%=txtorganization.ClientID%>').value, document.getElementById('<%=txtcity.ClientID%>').value, document.getElementById('<%=txtcountry.ClientID%>').value, document.getElementById('<%=txtaddress1.ClientID%>').value, document.getElementById('<%=txtaddress2.ClientID%>').value, document.getElementById('<%=txtremark.ClientID%>').value, 0,10,1);
            } catch (ex) {
          
            }
        }
    
    function selectmode(id)
    {
        try {
            
            var category = document.getElementById(id);
            if (category.options[category.selectedIndex].value == 0)
            {
                
   
                document.getElementById('<%=btnadd.ClientID%>').value = "Add";

            }
            else if (category.options[category.selectedIndex].value == 1)
            {
                document.getElementById('<%=btnadd.ClientID%>').value = "Update";

            }
            else if (category.options[category.selectedIndex].value == 2) {
                document.getElementById('<%=btnadd.ClientID%>').value = "Search";

             }

        }
        catch (ex) {
            alert(ex);
        }
    }
   

    </script>
    <script src="js/jsdynamic.js"></script>
</asp:Content>

