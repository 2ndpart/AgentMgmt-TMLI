Imports System.Data.SqlClient
Imports System.IO

Public Class UploadFiles
    Inherits System.Web.UI.Page

    Dim webRootPath As String = ConfigurationManager.AppSettings("ServerRootPathTMLI").ToString()
    'Dim webRootPath As String = ConfigurationManager.AppSettings("ServerRootPathIFC").ToString()
    Dim conf As String = ConfigurationManager.AppSettings("ProductRoot").ToString()
    Dim folderName As String = Path.Combine(webRootPath, conf)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If (Session("UserName") = String.Empty) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please re-login through Login page.');window.location='../TMLI_MPOS/Login.aspx';", True)
            End If
            FillGrid()
            loadFolder()
            loadSubFolder()
        Else
            '
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If ddl_select_folder.SelectedIndex <= 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select folder');", True)
        ElseIf txt_version.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version cannot be empty');", True)
        Else
            Try
                If myFile.HasFile Then
                    Dim iUploadedCnt As Integer = 0
                    Dim iFailedCnt As Integer = 0
                    Dim hfc As HttpFileCollection = Request.Files

                    'lblFileList.Text = "Select <b>" & hfc.Count & "</b> file(s)"

                    If hfc.Count <= 10 Then             ' 10 FILES RESTRICTION.
                        For i As Integer = 0 To hfc.Count - 1
                            Dim hpf As HttpPostedFile = hfc(i)
                            If hpf.ContentLength > 0 Then
                                Dim strFIleName As String = ""
                                strFIleName = hpf.FileName
                                Dim intFileSize As Int64
                                intFileSize = hpf.ContentLength
                                Dim strSubFolder As String = ""

                                If ddl_select_sub_folder.SelectedIndex > 0 Then
                                    strSubFolder = ddl_select_sub_folder.SelectedItem.Text + "\"
                                End If

                                myFile.PostedFile.SaveAs(folderName + ddl_select_folder.SelectedItem.Text + "\" + strSubFolder + strFIleName)

                                Dim conn As New SqlConnection
                                conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")

                                conn.Open()

                                Dim insertCommand As New SqlCommand("INSERT INTO FILE_UPLOAD ([FOLDER_NAME], [FOLDER_PARENT_ID], [FOLDER_SELF_ID], [SUB_FOLDER_NAME], [FILE_PATH], [FILE_NAME], [FILE_SIZE], [FILE_VERSION], [UPLOAD_BY], [UPLOAD_DATE]) VALUES (@FOLDER_NAME, @FOLDER_PARENT_ID, @FOLDER_SELF_ID, @SUB_FOLDER_NAME, @FILE_PATH, @FILE_NAME, @FILE_SIZE, @FILE_VERSION, @UPLOAD_BY, GETDATE())", conn)

                                insertCommand.Parameters.AddWithValue("@FOLDER_NAME", ddl_select_folder.SelectedItem.Text)
                                insertCommand.Parameters.AddWithValue("@FOLDER_PARENT_ID", ddl_select_folder.SelectedValue)
                                insertCommand.Parameters.AddWithValue("@FOLDER_SELF_ID", IIf(ddl_select_sub_folder.SelectedIndex > 0, ddl_select_sub_folder.SelectedValue, 0))
                                insertCommand.Parameters.AddWithValue("@SUB_FOLDER_NAME", strSubFolder.Replace("\", ""))
                                insertCommand.Parameters.AddWithValue("@FILE_PATH", folderName + ddl_select_folder.SelectedItem.Text + "\" + strSubFolder)
                                insertCommand.Parameters.AddWithValue("@FILE_NAME", strFIleName)
                                insertCommand.Parameters.AddWithValue("@FILE_SIZE", intFileSize)
                                insertCommand.Parameters.AddWithValue("@FILE_VERSION", txt_version.Text.Trim)
                                insertCommand.Parameters.AddWithValue("@UPLOAD_BY", "")

                                insertCommand.ExecuteNonQuery()

                                conn.Close()
                            End If
                        Next i
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('File upload success');", True)
                        FillGrid()
                        'lblUploadStatus.Text = "<b>" & iUploadedCnt & "</b> file(s) Uploaded."
                        'lblFailedStatus.Text = "<b>" & iFailedCnt &
                        '"</b> duplicate file(s) could not be uploaded."
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Masximum 10 files to upload');", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select a file to upload');", True)
                End If
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error upload file');", True)
            End Try
        End If
    End Sub

    Private Sub FillGrid()
        Try
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

            Using cmd As New SqlCommand
                Dim cmdText As String = "SELECT [ID], [FOLDER_NAME], [SUB_FOLDER_NAME], [FILE_NAME], [FILE_VERSION], [FILE_SIZE], [UPLOAD_BY], [UPLOAD_DATE] FROM [FILE_UPLOAD] WHERE [FOLDER_NAME] LIKE '%" + txt_search.Text + "%' OR [SUB_FOLDER_NAME] LIKE '%" + txt_search.Text + "%' OR [FILE_NAME] LIKE '%" + txt_search.Text + "%'"

                cmd.CommandText = cmdText

                cmd.Connection = objDBCom.Connection

                Dim dt As New DataTable()
                Using sda As New SqlDataAdapter(cmd)
                    sda.Fill(dt)
                    dgvUploadFiles.DataSource = dt
                    dgvUploadFiles.DataBind()
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

        Dim folderSelfId As String = ""
        Dim folderParentId As String = ""
        Dim folder As String = ""
        Dim subFolder As String = ""
        Dim filePath As String = ""
        Dim fileName As String = ""


        For Each row As GridViewRow In dgvUploadFiles.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chkDelete As CheckBox = DirectCast(row.Cells(0).FindControl("ItemCheckBox"), CheckBox)

                If chkDelete IsNot Nothing Then
                    If chkDelete.Checked Then
                        Dim id As String = dgvUploadFiles.DataKeys(row.RowIndex)(0).ToString()

                        Try
                            Dim conn As New SqlConnection
                            conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")

                            Dim query As String = "SELECT FOLDER_PARENT_ID, FOLDER_SELF_ID, FILE_PATH, FOLDER_NAME, SUB_FOLDER_NAME, FILE_NAME FROM FILE_UPLOAD WHERE ID = @ID"
                            Dim command1 As New SqlCommand(query, conn)
                            conn.Open()
                            command1.Parameters.AddWithValue("@ID", id)
                            Dim reader1 As SqlDataReader = command1.ExecuteReader()

                            While reader1.Read()
                                folderParentId = (reader1("FOLDER_PARENT_ID").ToString())
                                folderSelfId = (reader1("FOLDER_SELF_ID").ToString())
                                filePath = (reader1("FILE_PATH").ToString())
                                folder = (reader1("FOLDER_NAME").ToString())
                                subFolder = (reader1("SUB_FOLDER_NAME").ToString())
                                fileName = (reader1("FILE_NAME").ToString())
                            End While

                            reader1.Close()
                            conn.Close()

                            If fileName <> "" Then
                                Dim deleteStatement As String = (Convert.ToString("DELETE FROM [FILE_UPLOAD] WHERE [ID]='") & id) + "'"
                                Dim command As New SqlCommand(deleteStatement)

                                If objDBCom.ExecuteSQL(deleteStatement) Then
                                    File.Delete(filePath + fileName)
                                Else
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error delete data');", True)
                                End If
                            ElseIf subFolder <> "" Then
                                Dim deleteStatement As String = (Convert.ToString("DELETE FROM [FILE_UPLOAD] WHERE [ID]='") & id) + "'"
                                Dim command As New SqlCommand(deleteStatement)

                                Dim deleteStatement2 As String = (Convert.ToString("DELETE FROM [FILE_UPLOAD] WHERE FOLDER_PARENT_ID <> 0 AND [FOLDER_SELF_ID]='") & folderSelfId) + "'"
                                Dim command2 As New SqlCommand(deleteStatement2)

                                Dim deleteStatement3 As String = (Convert.ToString("DELETE FROM [FILE_UPLOAD_FOLDER] WHERE FOLDER_LEVEL = 2 AND [FOLDER_SELF_ID]='") & folderSelfId) + "'"
                                Dim command3 As New SqlCommand(deleteStatement3)

                                If objDBCom.ExecuteSQL(deleteStatement) Then
                                    If objDBCom.ExecuteSQL(deleteStatement2) Then
                                        If objDBCom.ExecuteSQL(deleteStatement3) Then
                                            If Directory.Exists(folderName + folder + "\" + subFolder) Then
                                                Directory.Delete(folderName + folder + "\" + subFolder, True)
                                            End If
                                        End If
                                    End If
                                Else
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error delete data');", True)
                                End If
                            ElseIf folder <> "" Then
                                Dim deleteStatement As String = (Convert.ToString("DELETE FROM [FILE_UPLOAD] WHERE [ID]='") & id) + "'"
                                Dim command As New SqlCommand(deleteStatement)

                                Dim deleteStatement2 As String = (Convert.ToString("DELETE FROM [FILE_UPLOAD] WHERE [FOLDER_PARENT_ID] <> 0 AND [FOLDER_PARENT_ID]='") & folderSelfId) + "'"
                                Dim command2 As New SqlCommand(deleteStatement2)

                                Dim deleteStatement3 As String = (Convert.ToString("DELETE FROM [FILE_UPLOAD_FOLDER] WHERE FOLDER_LEVEL = 1 AND [FOLDER_SELF_ID]='") & folderSelfId) + "'"
                                Dim command3 As New SqlCommand(deleteStatement3)

                                Dim deleteStatement4 As String = (Convert.ToString("DELETE FROM [FILE_UPLOAD_FOLDER] WHERE [FOLDER_PARENT_ID] <> 0 AND FOLDER_LEVEL = 2 AND [FOLDER_PARENT_ID]='") & folderSelfId) + "'"
                                Dim command4 As New SqlCommand(deleteStatement4)

                                If objDBCom.ExecuteSQL(deleteStatement) Then
                                    If objDBCom.ExecuteSQL(deleteStatement2) Then
                                        If objDBCom.ExecuteSQL(deleteStatement3) Then
                                            If objDBCom.ExecuteSQL(deleteStatement4) Then
                                                Directory.Delete(folderName + folder, True)
                                            End If
                                        End If
                                    End If
                                Else
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error delete data');", True)
                                End If
                            End If

                        Catch ex As Exception
                            'LogWriter.WriteLog(Me.[GetType]().FullName + " : " + ex.Message)
                        End Try
                    End If
                End If
            End If
        Next
        FillGrid()
        loadFolder()
        loadSubFolder()
    End Sub

    Private Sub loadFolder()
        Try
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")
            Dim adapter As New SqlDataAdapter("SELECT [FOLDER_SELF_ID], [FOLDER_NAME] FROM [FILE_UPLOAD_FOLDER] WHERE [FOLDER_LEVEL] = 1", conn)
            'WHERE [FOLDER_NAME]='" & ddl_select_folder.SelectedValue & "' ORDER BY SUB_FOLDER_NAME", conn)
            Dim dSource As New DataTable()
            adapter.Fill(dSource)
            ddl_select_folder.ClearSelection()
            ddl_select_folder.DataSource = dSource
            ddl_select_folder.DataTextField = "FOLDER_NAME"
            ddl_select_folder.DataValueField = "FOLDER_SELF_ID"
            ddl_select_folder.DataBind()
            ddl_select_folder.Items.Insert(0, "--Select--")
        Catch ex As Exception
            'LogWriter.WriteLog(Me.[GetType]().FullName + " : " + ex.Message)
        End Try
    End Sub

    Private Sub loadSubFolder()
        Try
            Dim conn As New SqlConnection
            conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")
            Dim adapter As New SqlDataAdapter("SELECT [FOLDER_SELF_ID], [FOLDER_NAME] FROM [FILE_UPLOAD_FOLDER] WHERE [FOLDER_LEVEL] = 2", conn)
            'WHERE [FOLDER_NAME]='" & ddl_select_folder.SelectedValue & "' ORDER BY SUB_FOLDER_NAME", conn)
            Dim dSource As New DataTable()
            adapter.Fill(dSource)
            ddl_select_sub_folder.ClearSelection()
            ddl_select_sub_folder.DataSource = dSource
            ddl_select_sub_folder.DataTextField = "FOLDER_NAME"
            ddl_select_sub_folder.DataValueField = "FOLDER_SELF_ID"
            ddl_select_sub_folder.DataBind()
            ddl_select_sub_folder.Items.Insert(0, "--Select--")
        Catch ex As Exception
            'LogWriter.WriteLog(Me.[GetType]().FullName + " : " + ex.Message)
        End Try
    End Sub

    Protected Sub btn_create_folder_Click(sender As Object, e As EventArgs) Handles btn_create_folder.Click
        If txt_folder_name.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder name cannot be empty');", True)
        ElseIf txt_folder_name.Text.Trim <> "" Then
            If Not FolderNameIsOK(txt_folder_name.Text.Trim) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder name cannot contain special char');", True)
            Else
                Try
                    ' Determine whether the directory exists.
                    If Directory.Exists(folderName + txt_folder_name.Text.Trim) Then
                        'Console.WriteLine("That path exists already.")
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder already exists');", True)
                    Else
                        Dim conn As New SqlConnection
                        conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")

                        Dim query As String = "SELECT * FROM FILE_UPLOAD_FOLDER WHERE FOLDER_LEVEL = 1"

                        Dim count As Integer = 0
                        Dim folderSelfId As Integer = 1

                        Dim command1 As New SqlCommand(query, conn)
                        conn.Open()

                        count = command1.ExecuteScalar()

                        If count > 0 Then
                            Dim query2 As String = "SELECT MAX(FOLDER_SELF_ID) + 1 AS FOLDER_SELF_ID FROM FILE_UPLOAD_FOLDER WHERE FOLDER_LEVEL = 1"
                            Dim command2 As New SqlCommand(query2, conn)
                            Dim reader1 As SqlDataReader = command2.ExecuteReader()
                            While reader1.Read()
                                folderSelfId = (reader1("FOLDER_SELF_ID"))
                            End While
                            reader1.Close()
                        End If

                        conn.Close()

                        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

                        Dim insertFolderStatement1 As String = "INSERT INTO FILE_UPLOAD ([FOLDER_PARENT_ID], [FOLDER_SELF_ID], [FOLDER_NAME]) VALUES (0, " + folderSelfId.ToString() + ", @FOLDER_NAME_1)"
                        objDBCom.Parameters.AddWithValue("@FOLDER_NAME_1", txt_folder_name.Text.Trim)

                        Dim insertFolderStatement2 As String = "INSERT INTO FILE_UPLOAD_FOLDER ([FOLDER_NAME], [FOLDER_SELF_ID], [FOLDER_LEVEL]) VALUES (@FOLDER_NAME_2, " + folderSelfId.ToString() + ", 1)"
                        objDBCom.Parameters.AddWithValue("@FOLDER_NAME_2", txt_folder_name.Text.Trim)

                        If objDBCom.ExecuteSQL(insertFolderStatement1) Then
                            If objDBCom.ExecuteSQL(insertFolderStatement2) Then
                                Directory.CreateDirectory(folderName + txt_folder_name.Text.Trim)
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder successfully created');", True)
                                loadFolder()
                                FillGrid()
                                txt_folder_name.Text = String.Empty
                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                            End If
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                        End If
                    End If

                Catch ex As Exception
                    'Console.WriteLine("The process failed: {0}.", ex.ToString())
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                End Try
            End If
        End If
    End Sub

    Protected Sub btn_create_sub_folder_Click(sender As Object, e As EventArgs) Handles btn_create_sub_folder.Click
        If ddl_select_folder.SelectedIndex <= 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select folder');", True)
        ElseIf txt_sub_folder_name.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Sub folder name cannot be empty');", True)
        ElseIf txt_sub_folder_name.Text.Trim <> "" Then
            If Not FolderNameIsOK(txt_sub_folder_name.Text.Trim) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder name cannot contain special char');", True)
            Else
                Try
                    ' Determine whether the directory exists.
                    If Directory.Exists(folderName + ddl_select_folder.SelectedItem.Text + "\" + txt_sub_folder_name.Text.Trim) Then
                        'Console.WriteLine("That path exists already.")
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Sub folder already exists');", True)
                    Else
                        Dim conn As New SqlConnection
                        conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")

                        Dim query As String = "SELECT * FROM FILE_UPLOAD_FOLDER WHERE FOLDER_LEVEL = 2"

                        Dim count As Integer = 0
                        Dim folderSelfId As Integer = 1

                        Dim command1 As New SqlCommand(query, conn)
                        conn.Open()

                        count = command1.ExecuteScalar()

                        If count > 0 Then
                            Dim query2 As String = "SELECT MAX(FOLDER_SELF_ID) + 1 AS FOLDER_SELF_ID FROM FILE_UPLOAD_FOLDER WHERE FOLDER_LEVEL = 2"
                            Dim command2 As New SqlCommand(query2, conn)
                            Dim reader1 As SqlDataReader = command2.ExecuteReader()
                            While reader1.Read()
                                folderSelfId = (reader1("FOLDER_SELF_ID"))
                            End While
                            reader1.Close()
                        End If

                        conn.Close()

                        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

                        Dim insertFolderStatement1 As String = "INSERT INTO FILE_UPLOAD ([FOLDER_PARENT_ID], [FOLDER_SELF_ID], [SUB_FOLDER_NAME], [FOLDER_NAME]) VALUES (" + ddl_select_folder.SelectedValue + ", " + folderSelfId.ToString() + ", @SUB_FOLDER_NAME_1, @FOLDER_NAME)"
                        objDBCom.Parameters.AddWithValue("@SUB_FOLDER_NAME_1", txt_sub_folder_name.Text.Trim)
                        objDBCom.Parameters.AddWithValue("@FOLDER_NAME", ddl_select_folder.SelectedItem.Text)

                        Dim insertFolderStatement2 As String = "INSERT INTO FILE_UPLOAD_FOLDER ([FOLDER_NAME], [FOLDER_SELF_ID], [FOLDER_LEVEL], [FOLDER_PARENT_ID]) VALUES (@SUB_FOLDER_NAME_2, " + folderSelfId.ToString() + ", 2, " + ddl_select_folder.SelectedValue + ")"
                        objDBCom.Parameters.AddWithValue("@SUB_FOLDER_NAME_2", txt_sub_folder_name.Text.Trim)

                        If objDBCom.ExecuteSQL(insertFolderStatement1) Then
                            If objDBCom.ExecuteSQL(insertFolderStatement2) Then
                                Directory.CreateDirectory(folderName + ddl_select_folder.SelectedItem.Text + "\" + txt_sub_folder_name.Text.Trim)
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder successfully created');", True)
                                loadSubFolder()
                                FillGrid()
                                txt_sub_folder_name.Text = String.Empty
                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                            End If
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                        End If
                    End If

                Catch ex As Exception
                    'Console.WriteLine("The process failed: {0}.", ex.ToString())
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                End Try
            End If
        End If
    End Sub

    Protected Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        FillGrid()
    End Sub
    Public Shared Function FolderNameIsOK(ByVal folderName As String) As Boolean
        Dim reserved As Char() = Path.GetInvalidFileNameChars()

        For Each c As Char In reserved
            If folderName.Contains(c) Then
                Return False
            End If
        Next

        Return True
    End Function
End Class