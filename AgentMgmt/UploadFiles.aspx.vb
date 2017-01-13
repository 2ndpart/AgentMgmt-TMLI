Imports System.Data.SqlClient
Imports System.IO

Public Class UploadFiles
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            FillGrid()
        Else

        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        'Dim webRootPath As String = ConfigurationManager.AppSettings("ServerRootPathTMLI").ToString()
        Dim webRootPath As String = ConfigurationManager.AppSettings("ServerRootPathIFC").ToString()
        Dim conf As String = ConfigurationManager.AppSettings("ProductRoot").ToString()
        Dim folderName As String = Path.Combine(webRootPath, conf)

        Try
            If myFile.HasFile Then
                Dim strFIleName As String = ""
                strFIleName = myFile.FileName
                Dim intFileSize As Int64
                intFileSize = myFile.PostedFile.ContentLength
                myFile.PostedFile.SaveAs(folderName + ddl_select_folder.SelectedValue + "/" + strFIleName)

                Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

                Dim insertStatement As String = "INSERT INTO FILE_UPLOAD ([FOLDER_NAME], [FILE_PATH], [FILE_NAME], [FILE_SIZE], [CREATED_BY], [CREATED_DATE]) VALUES (@FOLDER_NAME, @FILE_PATH, @FILE_NAME, @FILE_SIZE, @CREATED_BY, GETDATE())"
                objDBCom.Parameters.AddWithValue("@FOLDER_NAME", ddl_select_folder.SelectedValue)
                objDBCom.Parameters.AddWithValue("@FILE_PATH", folderName + ddl_select_folder.SelectedValue)
                objDBCom.Parameters.AddWithValue("@FILE_NAME", strFIleName)
                objDBCom.Parameters.AddWithValue("@FILE_SIZE", intFileSize)
                objDBCom.Parameters.AddWithValue("@CREATED_BY", "")

                objDBCom.ExecuteSQL(insertStatement)

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('File upload success');", True)

                FillGrid()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select a file to upload');", True)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FillGrid()
        Try
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

            Using cmd As New SqlCommand
                Dim cmdText As String = "SELECT [ID], [FOLDER_NAME], [FILE_NAME], [FILE_SIZE] FROM [FILE_UPLOAD]"

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
        Dim filePath As String = ""
        Dim fileName As String = ""

        If dgvUploadFiles.Rows.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('No data available');", True)
        ElseIf dgvUploadFiles.Rows.Count > 0 Then
            For Each row As GridViewRow In dgvUploadFiles.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim chkDelete As CheckBox = DirectCast(row.Cells(0).FindControl("ItemCheckBox"), CheckBox)

                    If chkDelete IsNot Nothing Then
                        If chkDelete.Checked Then
                            Dim id As String = dgvUploadFiles.DataKeys(row.RowIndex)(0).ToString()

                            Try
                                Dim conn As New SqlConnection
                                conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")

                                Dim query As String = "SELECT FILE_PATH, FILE_NAME FROM FILE_UPLOAD WHERE ID = @ID"
                                Dim command1 As New SqlCommand(query, conn)
                                conn.Open()
                                command1.Parameters.AddWithValue("@ID", id)
                                Dim reader1 As SqlDataReader = command1.ExecuteReader()

                                While reader1.Read()
                                    filePath = (reader1("FILE_PATH").ToString())
                                    fileName = (reader1("FILE_NAME").ToString())
                                End While

                                reader1.Close()
                                conn.Close()


                                Dim deleteStatement As String = (Convert.ToString("DELETE FROM [FILE_UPLOAD] WHERE [ID]='") & id) + "'"
                                Dim command As New SqlCommand(deleteStatement)
                                If objDBCom.ExecuteSQL(deleteStatement) Then
                                    File.Delete(filePath + "\" + fileName)
                                Else
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error delete data');", True)
                                End If
                            Catch ex As Exception
                                'LogWriter.WriteLog(Me.[GetType]().FullName + " : " + ex.Message)
                            End Try
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select data to delete');", True)
                        End If
                    End If
                End If
            Next
            FillGrid()
        End If
    End Sub
End Class