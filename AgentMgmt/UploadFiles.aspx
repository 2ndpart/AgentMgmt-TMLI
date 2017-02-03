<%@ Page Title="" MasterPageFile="~/Site.Master" Language="vb" AutoEventWireup="false" CodeBehind="UploadFiles.aspx.vb" Inherits="AgentMgmt.UploadFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .RowFormDetail 
        {
            display: none;
        }
        .auto-style1 {
            height: 20px;
        }
        .auto-style5 {
            height: 20px;
            width: 490px;
        }
        .auto-style8 {
            width: 490px;
        }
        .auto-style9 {
            height: 30px;
        }
        .auto-style10 {
            width: 490px;
            height: 30px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function ()
        {
            
        });        

        function toggleFormDetail(stringRowSelectedJavaScriptClass, stringRowGroupJavaScriptClass)
        {
            var stringRowGroupJQueryClass = "." + stringRowGroupJavaScriptClass;
            var stringRowSelectedJQueryClass = "." + stringRowSelectedJavaScriptClass;
            
            $(stringRowGroupJQueryClass).css("display", "none");
            $(stringRowSelectedJQueryClass).css("display", "table-row");
        }

        function togglefileupload()
        {

            $('.fileupload').css("display", "table-row");
        }

        function toggleCreateSubFolder()
        {
            $('.RowCreateSubFolder').css("display", "table-row");
        }

        function toggleFreshStart()
        {
            $('.FreshStart').css("display", "table-row");
        }

        function toggleSemiFreshStart()
        {
            $('.SemiFreshStart').css("display", "table-row");
        }

        function toggleReStart()
        {
            $('.ReStart').css("display", "table-row");
        }

        function toggleSelectFolder()
        {
            $('.RowFormSelectFolder').css("display", "table-row");
        }

        function togglesecondbutton() {

            $('.secondbutton').css("display", "table-row");
            $('.folderrow').css("display", "table-row");
        }

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
                        row.style.backgroundColor = "#c7c8c9";
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
                row.style.backgroundColor = "#c7c8c9";
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
            <asp:PostBackTrigger ControlID="btnUpload2" />
            <asp:PostBackTrigger ControlID="btnUpload3" />
        </Triggers>
        <ContentTemplate>
            <h2>E - Library</h2>
            <table class="nav-justified">
                <tr>
                    <td width="240">Search</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txt_search" runat="server" class="form-control" Width="320" style="float:left;"></asp:TextBox>
                        <asp:Button ID="btn_search" runat="server" Text="Search" class="btn btn-info" Height="30px" />
                    </td>
                </tr>
                <tr>                    
                    <td>&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                </tr>
                 <tr>                    
                    <td>                        
                        &nbsp;</td>
                     <td class="auto-style8">                         
                         <input type="button" id="btnAddFolder" value="Create New" onclick="toggleFormDetail('RowFormFolder', 'RowFormDetail'); toggleCreateSubFolder(); togglefileupload(); toggleFreshStart()"/>
                         <input type="button" id="btnAddFile" value="Existing"  onclick="toggleFormDetail('RowFormFile', 'RowFormDetail'); togglesecondbutton()"/>
                         <asp:Button ID="btnDelete" runat="server" Text="Delete" />
                     </td>
                </tr>
                <tr>                    
                    <td class="auto-style1"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr class="folderrow RowFormDetail">
                    <td class="auto-style9">Select Folder</td>
                    <td class="auto-style10">
                        <asp:DropDownList ID="ddl_select_folder" runat="server" class="form-control" Width="300px" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                 <tr class="folderrow RowFormDetail">                    
                    <td>&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                </tr>
                <tr class="secondbutton RowFormDetail">                    
                    <td>&nbsp;</td>
                    <td class="auto-style8">
                        <input type="button" id="btnCreateSub" value="Create New Subfolder" onclick="toggleFormDetail('RowFormSubFile', 'RowFormDetail'); togglefileupload(); togglesecondbutton(); toggleCreateSubFolder(); toggleSemiFreshStart()"/>
                        <input type="button" id="btnExisting" value="Existing" onclick="toggleFormDetail('RowFormSubFile', 'RowFormDetail'); togglefileupload(); togglesecondbutton(); toggleSelectFolder(); toggleReStart()"/>
                        <%--<ajaxToolkit:AjaxFileUpload ID="myFile" runat="server" MaximumNumberOfFiles="10" />--%>
                    </td>
                </tr>                            
                <tr class="RowFormSubFile RowFormDetail">                    
                    <td>&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                </tr>
                <tr class="RowFormSelectFolder RowFormDetail">                    
                    <td>Select Sub Folder</td>
                    <td class="auto-style8">
                        
                        <asp:DropDownList ID="ddl_select_sub_folder" runat="server" class="form-control" Width="300px">
                        </asp:DropDownList>
                    </td>
                </tr>  
                <tr class="RowFormSelectFolder RowFormDetail">                    
                    <td>&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                </tr>
                <tr class="RowFormFolder RowFormDetail">                    
                    <td class="auto-style9">Create Folder Name</td>
                    <td class="auto-style10"><asp:TextBox ID="txt_folder_name" runat="server" class="form-control" Width="300px" style="float:left;" ></asp:TextBox>
                    </td>
                </tr>
                <tr class="RowFormFolder RowFormDetail">                    
                    <td>&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                </tr>
                <tr class="RowCreateSubFolder RowFormDetail">                    
                    <td>Create Sub Folder Name</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txt_sub_folder_name" runat="server" class="form-control" Width="300px" style="float:left;" ></asp:TextBox>
                    </td>
                </tr>
                <tr class="RowCreateSubFolder RowFormDetail">                    
                    <td class="auto-style1"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr class="fileupload RowFormDetail">
                    <td>Select File</td>
                    <td class="auto-style8">
                        <asp:FileUpload ID="myFile" runat="server" multiple="true" Width="300px" style="float:left;" class="form-control"/>
                        <asp:TextBox ID="txt_version" runat="server" class="form-control" Width="160px" style="float:left;" placeholder="File Version"></asp:TextBox>
                        </td>
                </tr>
                <tr class="fileupload RowFormDetail">
                    <td>&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                </tr>
                <tr class="fileupload RowFormDetail">                    
                    <td>&nbsp;</td>
                    <td class="auto-style8">
                        <asp:FileUpload ID="myFile0" runat="server" class="form-control" multiple="true" style="float:left;" Width="300px" />
                        <asp:TextBox ID="txt_version0" runat="server" class="form-control" placeholder="File Version" style="float:left;" Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="fileupload RowFormDetail">                    
                    <td>&nbsp;</td>
                    <td class="auto-style8">
                        &nbsp;</td>
                </tr>
                <tr class="fileupload RowFormDetail">                    
                    <td class="auto-style1"></td>
                    <td class="auto-style5">
                        <asp:FileUpload ID="myFile1" runat="server" class="form-control" multiple="true" style="float:left;" Width="300px" />
                        <asp:TextBox ID="txt_version1" runat="server" class="form-control" placeholder="File Version" style="float:left;" Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="fileupload RowFormDetail">
                    <td>&nbsp;</td>
                    <td class="auto-style8">                        
                        &nbsp;</td>
                </tr>
                <tr class="fileupload RowFormDetail">
                    <td>&nbsp;</td>                    
                    <td class="auto-style8">
                        <asp:FileUpload ID="myFile2" runat="server" class="form-control" multiple="true" style="float:left;" Width="300px" />
                        <asp:TextBox ID="txt_version2" runat="server" class="form-control" placeholder="File Version" style="float:left;" Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="fileupload RowFormDetail">                    
                    <td>&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                </tr>
                <tr class="fileupload RowFormDetail">
                    <td>&nbsp;</td>
                    <td class="auto-style8">
                        <asp:FileUpload ID="myFile3" runat="server" class="form-control" multiple="true" style="float:left;" Width="300px" />
                        <asp:TextBox ID="txt_version3" runat="server" class="form-control" placeholder="File Version" style="float:left;" Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="fileupload RowFormDetail">
                    <td>&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                </tr>
                <tr class="fileupload RowFormDetail">
                    <td>&nbsp;</td>
                    <td class="auto-style8">
                        <asp:FileUpload ID="myFile4" runat="server" class="form-control" multiple="true" style="float:left;" Width="300px" />
                        <asp:TextBox ID="txt_version4" runat="server" class="form-control" placeholder="File Version" style="float:left;" Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="fileupload RowFormDetail">
                    <td class="auto-style1"></td>
                    <td class="auto-style5"></td>
                </tr>
                <tr class="FreshStart RowFormDetail">
                    <td>
                        &nbsp;</td>
                    <td class="auto-style8">
                        <asp:Button ID="btnUpload" runat="server" OnClientClick="showWait();" Text="Upload" Width="61px" />
                        &nbsp;&nbsp;&nbsp;
                        </td>
                </tr>
                <tr class="SemiFreshStart RowFormDetail">
                    <td>
                        &nbsp;</td>
                    <td class="auto-style8">
                        <asp:Button ID="btnUpload2" runat="server" OnClientClick="showWait();" Text="Upload" Width="61px" />
                        &nbsp;&nbsp;&nbsp;
                        </td>
                </tr>
                <tr class="ReStart RowFormDetail">
                    <td>
                        &nbsp;</td>
                    <td class="auto-style8">
                        <asp:Button ID="btnUpload3" runat="server" OnClientClick="showWait();" Text="Upload" Width="61px" />
                        &nbsp;&nbsp;&nbsp;
                        </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="auto-style8">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style8">&nbsp;</td>
                </tr>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                    AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <asp:Label ID="lblWait" runat="server" BackColor="#507CD1" 
                            Font-Bold="True" ForeColor="White" 
                            Text=""></asp:Label>
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
                    <asp:BoundField DataField="SUB_FOLDER_NAME" HeaderText="Sub Folder Name" />
                    <asp:BoundField DataField="FILE_NAME" HeaderText="File Name" />
                    <asp:BoundField DataField="FILE_VERSION" HeaderText="File Version" />
                    <asp:BoundField DataField="FILE_SIZE" HeaderText="File Size" />
                    <asp:BoundField DataField="UPLOAD_BY" HeaderText="Upload By" />
                    <asp:BoundField DataField="UPLOAD_DATE" HeaderText="Upload Date" DataFormatString="{0:dd-MMM-yyyy}"/>
                    <asp:hyperlinkfield text="View"
                    DataNavigateUrlFields="ID"
                    DataNavigateUrlFormatString ="UploadFilesEdit.aspx?ID={0}"           
                    headertext=""
                    target="_self" />                                                                       
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