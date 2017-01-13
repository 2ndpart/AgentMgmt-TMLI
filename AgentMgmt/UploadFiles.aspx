<%@ Page Title="" MasterPageFile="~/Site.Master" Language="vb" AutoEventWireup="false" CodeBehind="UploadFiles.aspx.vb" Inherits="AgentMgmt.UploadFiles" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
    .style1
        {
            width: 100%;
        }
    </style>

    <script type="text/javascript">
        function showWait()
        {
            if ($get('myFile').value.length > 0)
            {
                $get('UpdateProgress1').style.display = 'block';
            }
        }

        function CheckAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "blue";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "white";
                        }
                        else {
                            row.style.backgroundColor = "#f2f6fc";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }

        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "blue";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "white";
                }
                else {
                    row.style.backgroundColor = "#f2f6fc";
                }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

            function deleteButtonAct() {

            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>   
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" /> 
        </Triggers>
        <ContentTemplate>
            <h2>                
                Upload Files</h2>
            <table class="nav-justified">
                <tr>
                    <td>Select Folder</td>
                    <td>
                        <asp:DropDownList ID="ddl_select_folder" runat="server" class="form-control" Width="300px">
                            <asp:ListItem Value="CompanyInformation">Company Information</asp:ListItem>
                            <asp:ListItem Value="ProductInformation">Product Information</asp:ListItem>
                            <asp:ListItem Value="TrainingInformation">Training Information</asp:ListItem>                            
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>                    
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Select File</td>
                    <td>
                        <asp:FileUpload ID="myFile" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClientClick="showWait();"/>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </td>
                </tr>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <asp:Label ID="lblWait" runat="server" BackColor="#507CD1" 
                            Font-Bold="True" ForeColor="White" 
                            Text="Please wait ... Uploading file"></asp:Label>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </table>
            <br />
            <asp:GridView ID="dgvUploadFiles" runat="server" CellPadding="4" class="table" ForeColor="#333333" 
                GridLines="None" AutoGenerateColumns="false" DataKeyNames="ID">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <input id="CheckboxAll" type="checkbox" onclick="CheckAll(this)" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="ItemCheckBox" runat="server" onclick="Check_Click(this)" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:commandfield selecttext="Edit" ControlStyle-Font-Underline showselectbutton="True" />--%>
                    <asp:BoundField DataField="FOLDER_NAME" HeaderText="Folder Name" />
                    <asp:BoundField DataField="FILE_NAME" HeaderText="File Name" />
                    <asp:BoundField DataField="FILE_SIZE" HeaderText="File Size" />                                                                       
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    </asp:Content>