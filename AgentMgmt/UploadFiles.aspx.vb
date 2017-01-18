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
                            strFIleName = myFile.FileName
                            Dim intFileSize As Int64
                            intFileSize = myFile.PostedFile.ContentLength
                            Dim strSubFolder As String = ""

                            If ddl_select_sub_folder.SelectedIndex > 0 Then
                                strSubFolder = ddl_select_sub_folder.SelectedValue
                            End If

                            myFile.PostedFile.SaveAs(folderName + ddl_select_folder.SelectedValue + "/" + strSubFolder + "/" + strFIleName)

                            Dim conn As New SqlConnection
                            conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")

                            conn.Open()

                            Dim insertCommand As New SqlCommand("INSERT INTO FILE_UPLOAD ([FOLDER_NAME], [SUB_FOLDER_NAME], [FILE_PATH], [FILE_NAME], [FILE_SIZE], [CREATED_BY], [CREATED_DATE]) VALUES (@FOLDER_NAME, @SUB_FOLDER_NAME, @FILE_PATH, @FILE_NAME, @FILE_SIZE, @CREATED_BY, GETDATE())", conn)

                            insertCommand.Parameters.AddWithValue("@FOLDER_NAME", ddl_select_folder.SelectedValue)
                            insertCommand.Parameters.AddWithValue("@SUB_FOLDER_NAME", strSubFolder)
                            insertCommand.Parameters.AddWithValue("@FILE_PATH", folderName + ddl_select_folder.SelectedValue + "/" + strSubFolder + "/")
                            insertCommand.Parameters.AddWithValue("@FILE_NAME", strFIleName)
                            insertCommand.Parameters.AddWithValue("@FILE_SIZE", intFileSize)
                            insertCommand.Parameters.AddWithValue("@CREATED_BY", "")

                            insertCommand.ExecuteNonQuery()

                            conn.Close()
                        End If
                    Next i
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('File upload success');", True)
                    'lblUploadStatus.Text = "<b>" & iUploadedCnt & "</b> file(s) Uploaded."
                    'lblFailedStatus.Text = "<b>" & iFailedCnt &
                    '"</b> duplicate file(s) could not be uploaded."
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Masximum 10 files to upload');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select a file to upload');", True)
            End If
            '    If myFile.HasFile Then
            '        Dim strFIleName As String = ""
            '        strFIleName = myFile.FileName
            '        Dim intFileSize As Int64
            '        intFileSize = myFile.PostedFile.ContentLength
            '        Dim strSubFolder As String = ""

            '        If ddl_select_sub_folder.SelectedIndex > 0 Then
            '            strSubFolder = ddl_select_sub_folder.SelectedValue
            '        End If

            '        myFile.PostedFile.SaveAs(folderName + ddl_select_folder.SelectedValue + "/" + strSubFolder + strFIleName)

            '        Dim conn As New SqlConnection
            '        conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")

            '        conn.Open()

            '        'Dim updateStatement As String = SqlCommand("UPDATE TMLI_TBM_FundHouse SET FundHouseDescription=@desc, FundDefaultSelected=@default, SortOrder=@order, Active=@active WHERE SubChannelCode='" + txtCode.Text + "'", conn);

            '        Dim insertCommand As New SqlCommand("INSERT INTO FILE_UPLOAD ([FOLDER_NAME], [SUB_FOLDER_NAME], [FILE_PATH], [FILE_NAME], [FILE_SIZE], [CREATED_BY], [CREATED_DATE]) VALUES (@FOLDER_NAME, @SUB_FOLDER_NAME, @FILE_PATH, @FILE_NAME, @FILE_SIZE, @CREATED_BY, GETDATE())", conn)

            '        insertCommand.Parameters.AddWithValue("@FOLDER_NAME", ddl_select_folder.SelectedValue)
            '        insertCommand.Parameters.AddWithValue("@SUB_FOLDER_NAME", strSubFolder)
            '        insertCommand.Parameters.AddWithValue("@FILE_PATH", folderName + ddl_select_folder.SelectedValue + "/" + strSubFolder + "/")
            '        insertCommand.Parameters.AddWithValue("@FILE_NAME", strFIleName)
            '        insertCommand.Parameters.AddWithValue("@FILE_SIZE", intFileSize)
            '        insertCommand.Parameters.AddWithValue("@CREATED_BY", "")

            '        insertCommand.ExecuteNonQuery()

            '        conn.Close()

            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('File upload success');", True)
            '    Else
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select a file to upload');", True)
            '    End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error upload file');", True)
        End Try
        FillGrid()
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

                            Dim query As String = "SELECT FILE_PATH, FOLDER_NAME, SUB_FOLDER_NAME, FILE_NAME FROM FILE_UPLOAD WHERE ID = @ID"
                            Dim command1 As New SqlCommand(query, conn)
                            conn.Open()
                            command1.Parameters.AddWithValue("@ID", id)
                            Dim reader1 As SqlDataReader = command1.ExecuteReader()

                            While reader1.Read()
                                filePath = (reader1("FILE_PATH").ToString())
                                folder = (reader1("FOLDER_NAME").ToString())
                                subFolder = (reader1("SUB_FOLDER_NAME").ToString())
                                fileName = (reader1("FILE_NAME").ToString())
                            End While

                            reader1.Close()
                            conn.Close()

                            Dim deleteStatement As String = (Convert.ToString("DELETE FROM [FILE_UPLOAD] WHERE [ID]='") & id) + "'"
                            Dim command As New SqlCommand(deleteStatement)
                            If objDBCom.ExecuteSQL(deleteStatement) Then
                                If fileName <> "" Then
                                    File.Delete(filePath + fileName)
                                Else
                                    Directory.Delete(folderName + folder + "/" + subFolder, True)
                                End If
                            Else
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error delete data');", True)
                            End If
                        Catch ex As Exception
                            'LogWriter.WriteLog(Me.[GetType]().FullName + " : " + ex.Message)
                        End Try
                    End If
                End If
            End If
        Next
        FillGrid()
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
        Else
            Try
                ' Determine whether the directory exists.
                If Directory.Exists(folderName + txt_folder_name.Text.Trim) Then
                    'Console.WriteLine("That path exists already.")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder already exists');", True)
                Else
                    Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

                    Dim insertFolderStatement1 As String = "INSERT INTO FILE_UPLOAD ([FOLDER_NAME]) VALUES (@FOLDER_NAME_1)"
                    objDBCom.Parameters.AddWithValue("@FOLDER_NAME_1", txt_folder_name.Text.Trim)

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
    End Sub

    Protected Sub btn_create_sub_folder_Click(sender As Object, e As EventArgs) Handles btn_create_sub_folder.Click
        If ddl_select_folder.SelectedIndex <= 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select folder');", True)
        ElseIf txt_sub_folder_name.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Sub folder name cannot be empty');", True)
        Else
            Try
                ' Determine whether the directory exists.
                If Directory.Exists(folderName + ddl_select_folder.SelectedItem.Text + "\" + txt_sub_folder_name.Text.Trim) Then
                    'Console.WriteLine("That path exists already.")
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Sub folder already exists');", True)
                Else
                    Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

                    Dim insertFolderStatement1 As String = "INSERT INTO FILE_UPLOAD ([SUB_FOLDER_NAME], [FOLDER_NAME]) VALUES (@SUB_FOLDER_NAME_1, @FOLDER_NAME)"
                    objDBCom.Parameters.AddWithValue("@SUB_FOLDER_NAME_1", txt_sub_folder_name.Text.Trim)
                    objDBCom.Parameters.AddWithValue("@FOLDER_NAME", ddl_select_folder.SelectedItem.Text)

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
    End Sub

    Protected Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        FillGrid()
    End Sub
End Class