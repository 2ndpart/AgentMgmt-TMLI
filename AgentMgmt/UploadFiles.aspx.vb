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
        Dim counter As List(Of String) = New List(Of String)
        If Not CreateFolder() Then
            txt_folder_name.Text = ""
            txt_sub_folder_name.Text = ""
            ddl_select_folder.SelectedIndex = 0
            ddl_select_sub_folder.SelectedIndex = 0
            txt_version.Text = ""
            txt_version0.Text = ""
            txt_version1.Text = ""
            txt_version2.Text = ""
            txt_version3.Text = ""
            txt_version4.Text = ""
            Exit Sub
        End If

        CreateSubFolder()
        Try
            If myFile.HasFile Then
                If txt_version.Text.Equals("") Then
                    counter.Add(myFile.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile.FileName + "'');", True)
                Else
                    UploadFiles(myFile, txt_version.Text, 0)
                End If
                'Else
                '    counter.Add("Create folder successful")
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select a file to upload');", True)
            End If

            If myFile0.HasFile Then
                If txt_version0.Text.Equals("") Then
                    counter.Add(myFile0.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile0.FileName + "'');", True)
                Else
                    UploadFiles(myFile0, txt_version0.Text, 1)
                End If
            End If

            If myFile1.HasFile Then
                If txt_version1.Text.Equals("") Then
                    counter.Add(myFile1.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile1.FileName + "'');", True)
                Else
                    UploadFiles(myFile1, txt_version1.Text, 2)
                End If
            End If

            If myFile2.HasFile Then
                If txt_version2.Text.Equals("") Then
                    counter.Add(myFile2.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile2.FileName + "'');", True)
                Else
                    UploadFiles(myFile2, txt_version2.Text, 3)
                End If
            End If

            If myFile3.HasFile Then
                If txt_version3.Text.Equals("") Then
                    counter.Add(myFile3.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile3.FileName + "'');", True)
                Else
                    UploadFiles(myFile3, txt_version3.Text, 4)
                End If
            End If

            If myFile4.HasFile Then
                If txt_version4.Text.Equals("") Then
                    counter.Add(myFile4.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile4.FileName + "'');", True)
                Else
                    UploadFiles(myFile4, txt_version4.Text, 5)
                End If
            End If

            Dim result As String = String.Join(", ", counter)
            If Not result.ToString().Equals("") Then
                If myFile.HasFile Or myFile0.HasFile Or myFile1.HasFile Or myFile2.HasFile Or myFile3.HasFile Or myFile4.HasFile Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormFolder', 'RowFormDetail'); toggleCreateSubFolder(); togglefileupload(); toggleFreshStart();", True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for " + result.ToString() + "');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder successfully created');", True)
                    txt_folder_name.Text = ""
                    txt_sub_folder_name.Text = ""
                    ddl_select_folder.SelectedIndex = 0
                    ddl_select_sub_folder.SelectedIndex = 0
                    txt_version.Text = ""
                    txt_version0.Text = ""
                    txt_version1.Text = ""
                    txt_version2.Text = ""
                    txt_version3.Text = ""
                    txt_version4.Text = ""
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('File upload success');", True)
                txt_folder_name.Text = ""
                txt_sub_folder_name.Text = ""
                ddl_select_folder.SelectedIndex = 0
                ddl_select_sub_folder.SelectedIndex = 0
                txt_version.Text = ""
                txt_version0.Text = ""
                txt_version1.Text = ""
                txt_version2.Text = ""
                txt_version3.Text = ""
                txt_version4.Text = ""
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormFolder', 'RowFormDetail'); toggleCreateSubFolder(); togglefileupload(); toggleFreshStart();", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error upload file');", True)
        End Try

    End Sub

    Private Sub FillGrid()
        Try
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

            Using cmd As New SqlCommand
                Dim cmdText As String = "SELECT [ID], [FOLDER_NAME], [SUB_FOLDER_NAME], [FILE_NAME], [FILE_VERSION], [FILE_SIZE], [UPLOAD_BY], [UPLOAD_DATE] FROM [FILE_UPLOAD] WHERE [FOLDER_NAME] LIKE '%" + txt_search.Text + "%' OR [SUB_FOLDER_NAME] LIKE '%" + txt_search.Text + "%' OR [FILE_NAME] LIKE '%" + txt_search.Text + "%' ORDER BY FOLDER_NAME ASC, SUB_FOLDER_NAME ASC, FILE_NAME ASC"

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
            Dim adapter As New SqlDataAdapter("SELECT [FOLDER_SELF_ID], [FOLDER_NAME] FROM [FILE_UPLOAD_FOLDER] WHERE [FOLDER_LEVEL] = 2 AND FOLDER_PARENT_ID = (SELECT FOLDER_SELF_ID FROM FILE_UPLOAD_FOLDER WHERE FOLDER_NAME = '" + ddl_select_folder.SelectedItem.Text + "' AND FOLDER_LEVEL = 1)", conn)
            Dim adapter2 As New SqlDataAdapter("SELECT [FOLDER_SELF_ID], [FOLDER_NAME] FROM [FILE_UPLOAD_FOLDER] WHERE [FOLDER_LEVEL] = 2", conn)
            Dim dSource As New DataTable()
            If ddl_select_folder.SelectedIndex <= 0 Then
                adapter2.Fill(dSource)
            Else
                adapter.Fill(dSource)
            End If

            'WHERE [FOLDER_NAME]='" & ddl_select_folder.SelectedValue & "' ORDER BY SUB_FOLDER_NAME", conn)


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

    Function CreateFolder() As Boolean
        If txt_folder_name.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormFolder', 'RowFormDetail'); toggleCreateSubFolder(); togglefileupload(); toggleFreshStart();", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder name cannot be empty');", True)
            Return False
        ElseIf txt_folder_name.Text.Trim <> "" Then
            If Not FolderNameIsOK(txt_folder_name.Text.Trim) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormFolder', 'RowFormDetail'); toggleCreateSubFolder(); togglefileupload(); toggleFreshStart();", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder name cannot contain special char');", True)
                Return False
            Else
                Try
                    ' Determine whether the directory exists.
                    If Directory.Exists(folderName + txt_folder_name.Text.Trim) Then
                        'Console.WriteLine("That path exists already.")
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder already exists');", True)
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
                                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder successfully created');", True)
                                loadFolder()
                                FillGrid()
                                'txt_folder_name.Text = String.Empty
                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                            End If
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                        End If
                    End If
                    Return True
                Catch ex As Exception
                    'Console.WriteLine("The process failed: {0}.", ex.ToString())
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                    Return False
                End Try
            End If
        End If
    End Function

    Function CreateSubFolder() As Boolean
        'If ddl_select_folder.SelectedIndex <= 0 Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select folder');", True)
        '    Return False
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim takevalue As New DataTable
        Dim takedataquery As String
        If Not txt_folder_name.Text.Equals("") Then
            takedataquery = "select FOLDER_NAME, FOLDER_SELF_ID from FILE_UPLOAD_FOLDER WHERE FOLDER_NAME = '" + txt_folder_name.Text + "'"
        Else
            takedataquery = "select FOLDER_NAME, FOLDER_SELF_ID from FILE_UPLOAD_FOLDER WHERE FOLDER_NAME = '" + ddl_select_folder.SelectedItem.Text + "'"
        End If

        objDBCom.ExecuteSQL(takevalue, takedataquery)



        If txt_sub_folder_name.Text.Trim <> "" Then
            If Not FolderNameIsOK(txt_sub_folder_name.Text.Trim) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder name cannot contain special char');", True)
                Return False
            Else
                Try
                    ' Determine whether the directory exists.
                    If Directory.Exists(folderName + takevalue.Rows(0)(0).ToString() + "\" + txt_sub_folder_name.Text.Trim) Then
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



                        Dim insertFolderStatement1 As String = "INSERT INTO FILE_UPLOAD ([FOLDER_PARENT_ID], [FOLDER_SELF_ID], [SUB_FOLDER_NAME], [FOLDER_NAME]) VALUES (" + takevalue.Rows(0)(1).ToString() + ", " + folderSelfId.ToString() + ", @SUB_FOLDER_NAME_1, @FOLDER_NAME)"
                        objDBCom.Parameters.AddWithValue("@SUB_FOLDER_NAME_1", txt_sub_folder_name.Text.Trim)
                        objDBCom.Parameters.AddWithValue("@FOLDER_NAME", takevalue.Rows(0)(0).ToString())

                        Dim insertFolderStatement2 As String = "INSERT INTO FILE_UPLOAD_FOLDER ([FOLDER_NAME], [FOLDER_SELF_ID], [FOLDER_LEVEL], [FOLDER_PARENT_ID]) VALUES (@SUB_FOLDER_NAME_2, " + folderSelfId.ToString() + ", 2, " + takevalue.Rows(0)(1).ToString() + ")"
                        objDBCom.Parameters.AddWithValue("@SUB_FOLDER_NAME_2", txt_sub_folder_name.Text.Trim)

                        If objDBCom.ExecuteSQL(insertFolderStatement1) Then
                            If objDBCom.ExecuteSQL(insertFolderStatement2) Then
                                Directory.CreateDirectory(folderName + takevalue.Rows(0)(0).ToString() + "\" + txt_sub_folder_name.Text.Trim)
                                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder successfully created');", True)
                                loadSubFolder()
                                FillGrid()
                                'txt_sub_folder_name.Text = String.Empty
                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                                Return False
                            End If
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                            Return False
                        End If
                    End If
                    Return True
                Catch ex As Exception
                    'Console.WriteLine("The process failed: {0}.", ex.ToString())
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error creating folder');", True)
                    Return False
                End Try
            End If
        End If
    End Function

    Function UploadFiles(ByVal fileupload As FileUpload, ByVal versioning As String, ByVal count As Integer) As Boolean
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim takevalue As New DataTable
        Dim takedataquery As String
        If Not txt_folder_name.Text.Equals("") Then
            takedataquery = "select FOLDER_NAME, FOLDER_SELF_ID from FILE_UPLOAD_FOLDER WHERE FOLDER_NAME = '" + txt_folder_name.Text + "'"
        Else
            takedataquery = "select FOLDER_NAME, FOLDER_SELF_ID from FILE_UPLOAD_FOLDER WHERE FOLDER_NAME = '" + ddl_select_folder.SelectedItem.Text + "'"
        End If
        objDBCom.ExecuteSQL(takevalue, takedataquery)
        objDBCom.Dispose()

        Dim iUploadedCnt As Integer = 0
        Dim iFailedCnt As Integer = 0
        Dim hfc As HttpFileCollection = Request.Files

        Try
            Dim hpf As HttpPostedFile = hfc(count)
            If hpf.ContentLength > 0 Then
                Dim strFIleName As String = ""
                strFIleName = hpf.FileName
                Dim intFileSize As Int64
                intFileSize = hpf.ContentLength
                Dim strSubFolder As String = ""

                If ddl_select_sub_folder.SelectedIndex > 0 Then
                    strSubFolder = ddl_select_sub_folder.SelectedItem.Text + "\"
                Else
                    strSubFolder = txt_sub_folder_name.Text + "\"
                End If

                fileupload.PostedFile.SaveAs(folderName + takevalue.Rows(0)(0).ToString() + "\" + strSubFolder + strFIleName)

                Dim conn As New SqlConnection
                conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")

                conn.Open()

                Dim insertCommand As New SqlCommand("INSERT INTO FILE_UPLOAD ([FOLDER_NAME], [FOLDER_PARENT_ID], [FOLDER_SELF_ID], [SUB_FOLDER_NAME], [FILE_PATH], [FILE_NAME], [FILE_SIZE], [FILE_VERSION], [UPLOAD_BY], [UPLOAD_DATE]) VALUES (@FOLDER_NAME, @FOLDER_PARENT_ID, @FOLDER_SELF_ID, @SUB_FOLDER_NAME, @FILE_PATH, @FILE_NAME, @FILE_SIZE, @FILE_VERSION, @UPLOAD_BY, GETDATE())", conn)

                insertCommand.Parameters.AddWithValue("@FOLDER_NAME", takevalue.Rows(0)(0).ToString())
                insertCommand.Parameters.AddWithValue("@FOLDER_PARENT_ID", takevalue.Rows(0)(1).ToString())
                insertCommand.Parameters.AddWithValue("@FOLDER_SELF_ID", IIf(ddl_select_sub_folder.SelectedIndex > 0, ddl_select_sub_folder.SelectedValue, 0))
                insertCommand.Parameters.AddWithValue("@SUB_FOLDER_NAME", strSubFolder.Replace("\", ""))
                insertCommand.Parameters.AddWithValue("@FILE_PATH", folderName + takevalue.Rows(0)(0).ToString() + "\" + strSubFolder)
                insertCommand.Parameters.AddWithValue("@FILE_NAME", strFIleName)
                insertCommand.Parameters.AddWithValue("@FILE_SIZE", intFileSize)
                insertCommand.Parameters.AddWithValue("@FILE_VERSION", versioning.Trim)
                insertCommand.Parameters.AddWithValue("@UPLOAD_BY", "")

                insertCommand.ExecuteNonQuery()

                conn.Close()
            End If
            FillGrid()
            Return True
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error upload file');", True)
            Return False
        End Try

    End Function

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

    Protected Sub btnUpload2_Click(sender As Object, e As EventArgs) Handles btnUpload2.Click
        Dim counter As List(Of String) = New List(Of String)
        If ddl_select_folder.SelectedIndex <= 0 Then
            'txt_folder_name.Text = ""
            'txt_sub_folder_name.Text = ""
            'ddl_select_folder.SelectedIndex = 0
            'ddl_select_sub_folder.SelectedIndex = 0
            'txt_version.Text = ""
            'txt_version0.Text = ""
            'txt_version1.Text = ""
            'txt_version2.Text = ""
            'txt_version3.Text = ""
            'txt_version4.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormSubFile', 'RowFormDetail'); togglefileupload(); togglesecondbutton(); toggleCreateSubFolder(); toggleSemiFreshStart();", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select a folder');", True)
            Exit Sub
        ElseIf Not CreateSubFolder() Then

        End If

        Try
            If myFile.HasFile Then
                If txt_version.Text.Equals("") Then
                    counter.Add(myFile.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile.FileName + "'');", True)
                Else
                    UploadFiles(myFile, txt_version.Text, 0)
                End If
            End If

            If myFile0.HasFile Then
                If txt_version0.Text.Equals("") Then
                    counter.Add(myFile0.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile0.FileName + "'');", True)
                Else
                    UploadFiles(myFile0, txt_version0.Text, 1)
                End If
            End If

            If myFile1.HasFile Then
                If txt_version1.Text.Equals("") Then
                    counter.Add(myFile1.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile1.FileName + "'');", True)
                Else
                    UploadFiles(myFile1, txt_version1.Text, 2)
                End If
            End If

            If myFile2.HasFile Then
                If txt_version2.Text.Equals("") Then
                    counter.Add(myFile2.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile2.FileName + "'');", True)
                Else
                    UploadFiles(myFile2, txt_version2.Text, 3)
                End If
            End If

            If myFile3.HasFile Then
                If txt_version3.Text.Equals("") Then
                    counter.Add(myFile3.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile3.FileName + "'');", True)
                Else
                    UploadFiles(myFile3, txt_version3.Text, 4)
                End If
            End If

            If myFile4.HasFile Then
                If txt_version4.Text.Equals("") Then
                    counter.Add(myFile4.FileName)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile4.FileName + "'');", True)
                Else
                    UploadFiles(myFile4, txt_version4.Text, 5)
                End If
            End If

            Dim result As String = String.Join(", ", counter)
            If Not result.ToString().Equals("") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormSubFile', 'RowFormDetail'); togglefileupload(); togglesecondbutton(); toggleCreateSubFolder(); toggleSemiFreshStart();", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for " + result.ToString() + "');", True)
            Else
                txt_folder_name.Text = ""
                txt_sub_folder_name.Text = ""
                ddl_select_folder.SelectedIndex = 0
                ddl_select_sub_folder.SelectedIndex = 0
                txt_version.Text = ""
                txt_version0.Text = ""
                txt_version1.Text = ""
                txt_version2.Text = ""
                txt_version3.Text = ""
                txt_version4.Text = ""
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('File upload success');", True)
            End If

        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormSubFile', 'RowFormDetail'); togglefileupload(); togglesecondbutton(); toggleCreateSubFolder(); toggleSemiFreshStart();", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error upload file');", True)
        End Try



    End Sub

    Protected Sub btnUpload3_Click(sender As Object, e As EventArgs) Handles btnUpload3.Click
        Dim counter As List(Of String) = New List(Of String)
        If ddl_select_folder.SelectedIndex <= 0 Then
            'txt_folder_name.Text = ""
            'txt_sub_folder_name.Text = ""
            'ddl_select_folder.SelectedIndex = 0
            'ddl_select_sub_folder.SelectedIndex = 0
            'txt_version.Text = ""
            'txt_version0.Text = ""
            'txt_version1.Text = ""
            'txt_version2.Text = ""
            'txt_version3.Text = ""
            'txt_version4.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormSubFile', 'RowFormDetail'); togglefileupload(); togglesecondbutton(); toggleSelectFolder(); toggleReStart();", True)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select a folder');", True)
        Else
            Try
                If myFile.HasFile Then
                    If txt_version.Text.Equals("") Then
                        counter.Add(myFile.FileName)
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile.FileName + "'');", True)
                    Else
                        UploadFiles(myFile, txt_version.Text, 0)
                    End If
                    'Else
                    '    counter.Add("Please select a file to upload")
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select a file to upload');", True)
                End If

                If myFile0.HasFile Then
                    If txt_version0.Text.Equals("") Then
                        counter.Add(myFile0.FileName)
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile0.FileName + "'');", True)
                    Else
                        UploadFiles(myFile0, txt_version0.Text, 1)
                    End If
                End If

                If myFile1.HasFile Then
                    If txt_version1.Text.Equals("") Then
                        counter.Add(myFile1.FileName)
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile1.FileName + "'');", True)
                    Else
                        UploadFiles(myFile1, txt_version1.Text, 2)
                    End If
                End If

                If myFile2.HasFile Then
                    If txt_version2.Text.Equals("") Then
                        counter.Add(myFile2.FileName)
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile2.FileName + "'');", True)
                    Else
                        UploadFiles(myFile2, txt_version2.Text, 3)
                    End If
                End If

                If myFile3.HasFile Then
                    If txt_version3.Text.Equals("") Then
                        counter.Add(myFile3.FileName)
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile3.FileName + "'');", True)
                    Else
                        UploadFiles(myFile3, txt_version3.Text, 4)
                    End If
                End If

                If myFile4.HasFile Then
                    If txt_version4.Text.Equals("") Then
                        counter.Add(myFile4.FileName)
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for '" + myFile4.FileName + "'');", True)
                    Else
                        UploadFiles(myFile4, txt_version4.Text, 5)
                    End If
                End If

                Dim result As String = String.Join(", ", counter)
                If Not result.ToString().Equals("") Then
                    If myFile.HasFile Or myFile0.HasFile Or myFile1.HasFile Or myFile2.HasFile Or myFile3.HasFile Or myFile4.HasFile Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormSubFile', 'RowFormDetail'); togglefileupload(); togglesecondbutton(); toggleSelectFolder(); toggleReStart();", True)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please fill in the version number for " + result.ToString() + "');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormSubFile', 'RowFormDetail'); togglefileupload(); togglesecondbutton(); toggleSelectFolder(); toggleReStart();", True)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select a file to upload');", True)
                    End If
                Else
                    txt_folder_name.Text = ""
                    txt_sub_folder_name.Text = ""
                    ddl_select_folder.SelectedIndex = 0
                    ddl_select_sub_folder.SelectedIndex = 0
                    txt_version.Text = ""
                    txt_version0.Text = ""
                    txt_version1.Text = ""
                    txt_version2.Text = ""
                    txt_version3.Text = ""
                    txt_version4.Text = ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('File upload success');", True)
                End If


            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "toggleFormDetail('RowFormSubFile', 'RowFormDetail'); togglefileupload(); togglesecondbutton(); toggleSelectFolder(); toggleReStart();", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error upload file');", True)
            End Try


        End If
    End Sub

    Protected Sub ddl_select_folder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_select_folder.SelectedIndexChanged
        loadSubFolder()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "toggleFormDetail('RowFormFile', 'RowFormDetail'); togglesecondbutton();", True)
    End Sub
End Class