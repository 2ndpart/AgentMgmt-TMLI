<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UploadPhoto.aspx.vb" Inherits="AgentMgmt.UploadPhoto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
        function ShowImagePreviewFileUpload1(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview1.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
        }

        function ShowImagePreviewFileUpload2(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview2.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
        }

         function ShowImagePreviewFileUpload3(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview3.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
         }

         function ShowImagePreviewFileUpload4(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview4.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
         }

         function ShowImagePreviewFileUpload5(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview5.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
         }

         function ShowImagePreviewFileUpload6(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview6.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
         }

         function ShowImagePreviewFileUpload7(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview7.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
         }

         function ShowImagePreviewFileUpload8(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview8.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
         }

         function ShowImagePreviewFileUpload9(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview9.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
         }

         function ShowImagePreviewFileUpload10(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPreview10.ClientID%>').prop('src', e.target.result)
                        .width(240)
                        .height(150);
                };
                reader.readAsDataURL(input.files[0]);
                }
        }
    </script>
    <div>
    
        <br />
        <br />
        <h1>Please select images for uploading into Server</h1><br />
        <br />
        <br />
        <table class="auto-style1">
            <tr>
                <td >
                    <br />
                    <asp:Image runat="server" ID="imgPreview1" Height="72px" Width="98px"/>
                    <br />
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="423px" onchange="ShowImagePreviewFileUpload1(this);"/>
                     
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="This image applied for Login Page"></asp:Label>
                    <br />
                </td>
               
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Image runat="server" ID="imgPreview2" Height="72px" Width="98px"/>
                    <br />
                    <asp:FileUpload ID="FileUpload2" runat="server" Width="423px" onchange="ShowImagePreviewFileUpload2(this);" />
                     
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="This image applied for first landing page"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Image runat="server" ID="imgPreview3" Height="72px" Width="98px"/>
                    <br />
                    <asp:FileUpload ID="FileUpload3" runat="server" Width="424px" onchange="ShowImagePreviewFileUpload3(this);"/>
                     
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="This image applied for second landing page"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Image runat="server" ID="imgPreview4" Height="72px" Width="98px"/>
                    <br />
                    <asp:FileUpload ID="FileUpload4" runat="server" Width="423px" onchange="ShowImagePreviewFileUpload4(this);"/>
                     
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="This image applied for third landing page"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Image runat="server" ID="imgPreview5" Height="72px" Width="98px"/>
                    <br />
                    <asp:FileUpload ID="FileUpload5" runat="server" Width="423px" onchange="ShowImagePreviewFileUpload5(this);"/>
                     
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="This image applied for prospect module page"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Image runat="server" ID="imgPreview6" Height="72px" Width="98px"/>
                    <br />
                    <asp:FileUpload ID="FileUpload6" runat="server" Width="423px" onchange="ShowImagePreviewFileUpload6(this);"/>
                     
                </td>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="This image applied for prospect form header"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Image runat="server" ID="imgPreview7" Height="72px" Width="98px"/>
                    <br />
                    <asp:FileUpload ID="FileUpload7" runat="server" Width="423px" onchange="ShowImagePreviewFileUpload7(this);"/>
                     
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="This image applied for SI module page"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Image runat="server" ID="imgPreview8" Height="72px" Width="98px"/>
                    <br />
                    <asp:FileUpload ID="FileUpload8" runat="server" Width="423px" onchange="ShowImagePreviewFileUpload8(this);"/>
                     
                </td>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="This image applied for SI form header"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Image runat="server" ID="imgPreview9" Height="72px" Width="98px"/>
                    <br />
                    <asp:FileUpload ID="FileUpload9" runat="server" Width="423px" onchange="ShowImagePreviewFileUpload9(this);"/>
                     
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="This image applied for SPAJ module page"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Image runat="server" ID="imgPreview10" Height="72px" Width="98px" onchange="ShowImagePreviewFileUpload10(this);"/>
                    <br />
                    <asp:FileUpload ID="FileUpload10" runat="server" Width="423px" />
                     
                </td>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="This image applied for SPAJ form header"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload Image" class="btn btn-primary"/>
                </td>
                <td></td>
            </tr>
        </table>
    
    </div>
</asp:Content>
