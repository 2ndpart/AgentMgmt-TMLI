Imports System.Data.SqlClient
Imports System.IO

Public Class UploadFilesEdit
    Inherits System.Web.UI.Page

    Dim webRootPath As String = ConfigurationManager.AppSettings("ServerRootPathTMLI").ToString()
    Dim conf As String = ConfigurationManager.AppSettings("ProductRoot").ToString()
    Dim folderName As String = Path.Combine(webRootPath, conf)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim ID As String = Request.QueryString("ID")

            If ID IsNot Nothing Then
                Try
                    Dim conn As New SqlConnection
                    conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")

                    Dim query As String = "SELECT FOLDER_NAME, SUB_FOLDER_NAME, FILE_NAME FROM FILE_UPLOAD WHERE ID = @ID"
                    Dim command1 As New SqlCommand(query, conn)
                    conn.Open()
                    command1.Parameters.AddWithValue("@ID", ID)
                    Dim reader1 As SqlDataReader = command1.ExecuteReader()

                    While reader1.Read()
                        txt_folder_name.Text = (reader1("FOLDER_NAME").ToString())
                        txt_sub_folder_name.Text = (reader1("SUB_FOLDER_NAME").ToString())
                        txt_file_name.Text = (reader1("FILE_NAME").ToString())
                        Session("Folder") = (reader1("FOLDER_NAME").ToString())
                        Session("oldSubFolder") = (reader1("SUB_FOLDER_NAME").ToString())
                        Session("oldFileName") = (reader1("FILE_NAME").ToString())
                    End While

                    reader1.Close()
                    conn.Close()

                    If txt_file_name.Text <> "" Then
                        Session("sts") = 2
                        txt_sub_folder_name.Enabled = False
                    Else
                        Session("sts") = 1
                        txt_file_name.Enabled = False
                    End If
                Catch ex As Exception
                    'LogWriter.WriteLog(Me.[GetType]().FullName + " : " + ex.Message)
                End Try
            End If
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/UploadFiles.aspx")
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Session("sts") = 1 Then
            If txt_sub_folder_name.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder name cannot be empty');", True)
            Else
                Try
                    FileSystem.Rename(folderName + Session("Folder") + "\" + Session("oldSubFolder"), folderName + Session("Folder") + "\" + txt_sub_folder_name.Text)

                    Dim conn As New SqlConnection
                    conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")
                    conn.Open()

                    Dim updateStatement As New SqlCommand("UPDATE FILE_UPLOAD SET SUB_FOLDER_NAME=@SUB_FOLDER_NAME, UPDATED_BY=@UPDATED_BY, UPDATED_DATE=GETDATE() WHERE ID=@ID", conn)

                    updateStatement.Parameters.AddWithValue("@ID", Request.QueryString("ID"))
                    updateStatement.Parameters.AddWithValue("@SUB_FOLDER_NAME", txt_sub_folder_name.Text)
                    updateStatement.Parameters.AddWithValue("@UPDATED_BY", "")

                    updateStatement.ExecuteNonQuery()

                    conn.Close()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Edit sub folder success');", True)
                    Response.Redirect("~\UploadFiles.aspx")
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error edit sub folder');", True)
                End Try
            End If
        ElseIf Session("sts") = 2 Then
            If txt_file_name.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('File name cannot be empty');", True)
            Else
                Try
                    FileSystem.Rename(folderName + Session("Folder") + "\" + Session("oldSubFolder") + "\" + Session("oldFileName"), folderName + Session("Folder") + "\" + Session("oldSubFolder") + "\" + txt_file_name.Text)

                    Dim conn As New SqlConnection
                    conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")
                    conn.Open()

                    Dim updateStatement As New SqlCommand("UPDATE FILE_UPLOAD SET FILE_NAME=@FILE_NAME, UPDATED_BY=@UPDATED_BY, UPDATED_DATE=GETDATE() WHERE ID=@ID", conn)

                    updateStatement.Parameters.AddWithValue("@ID", Request.QueryString("ID"))
                    updateStatement.Parameters.AddWithValue("@FILE_NAME", txt_file_name.Text)
                    updateStatement.Parameters.AddWithValue("@UPDATED_BY", "")

                    updateStatement.ExecuteNonQuery()

                    conn.Close()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Edit file success');", True)
                    Response.Redirect("~\UploadFiles.aspx")
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error edit file');", True)
                End Try
            End If
        End If
    End Sub
End Class