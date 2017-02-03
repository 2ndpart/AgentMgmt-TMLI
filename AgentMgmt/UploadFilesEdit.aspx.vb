Imports System.Data.SqlClient
Imports System.IO

Public Class UploadFilesEdit
    Inherits System.Web.UI.Page

    Dim webRootPath As String = ConfigurationManager.AppSettings("ServerRootPathTMLI").ToString()
    'Dim webRootPath As String = ConfigurationManager.AppSettings("ServerRootPathIFC").ToString()
    Dim conf As String = ConfigurationManager.AppSettings("ProductRoot").ToString()
    Dim folderName As String = Path.Combine(webRootPath, conf)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim ID As String = Request.QueryString("ID")

            If ID IsNot Nothing Then
                Try
                    Dim conn As New SqlConnection
                    conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")

                    Dim query As String = "SELECT FOLDER_NAME, SUB_FOLDER_NAME, FILE_NAME, FOLDER_SELF_ID FROM FILE_UPLOAD WHERE ID = @ID"
                    Dim command1 As New SqlCommand(query, conn)
                    conn.Open()
                    command1.Parameters.AddWithValue("@ID", ID)
                    Dim reader1 As SqlDataReader = command1.ExecuteReader()

                    While reader1.Read()
                        txt_folder_name.Text = (reader1("FOLDER_NAME").ToString())
                        txt_sub_folder_name.Text = (reader1("SUB_FOLDER_NAME").ToString())
                        txt_file_name.Text = (reader1("FILE_NAME").ToString())
                        Session("folderSelfId") = (reader1("FOLDER_SELF_ID").ToString())
                        Session("oldFolder") = (reader1("FOLDER_NAME").ToString())
                        Session("oldSubFolder") = (reader1("SUB_FOLDER_NAME").ToString())
                        Session("oldFileName") = (reader1("FILE_NAME").ToString())
                    End While

                    reader1.Close()
                    conn.Close()

                    If txt_file_name.Text <> "" Then
                        Session("sts") = 3
                        txt_file_name.ReadOnly = True
                        btnSave.Visible = False
                    ElseIf txt_sub_folder_name.Text <> "" Then
                        Session("sts") = 2
                        txt_sub_folder_name.ReadOnly = False
                        btnURL.Visible = False
                    ElseIf txt_folder_name.Text <> "" Then
                        Session("sts") = 1
                        txt_folder_name.ReadOnly = False
                        btnURL.Visible = False
                    Else

                    End If
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error edit data');", True)
                End Try
            End If
        End If
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("~/UploadFiles.aspx")
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Session("sts") = 1 Then
            If txt_folder_name.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Folder name cannot be empty');", True)
            Else
                Try
                    FileSystem.Rename(folderName + Session("oldFolder"), folderName + txt_folder_name.Text)

                    Dim conn As New SqlConnection
                    conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")
                    conn.Open()

                    Dim updateStatement1 As New SqlCommand("UPDATE FILE_UPLOAD SET FOLDER_NAME=@FOLDER_NAME, UPDATED_BY=@UPDATED_BY, UPDATED_DATE=GETDATE() WHERE FOLDER_SELF_ID=@FOLDER_SELF_ID AND FOLDER_PARENT_ID = 0", conn)

                    Dim updateStatement2 As New SqlCommand("UPDATE FILE_UPLOAD_FOLDER SET FOLDER_NAME=@FOLDER_NAME_2 WHERE FOLDER_LEVEL = 1 AND FOLDER_SELF_ID=" + Session("folderSelfId"), conn)

                    Dim updateStatement3 As New SqlCommand("UPDATE FILE_UPLOAD SET FOLDER_NAME=@FOLDER_NAME_3, UPDATED_BY=@UPDATED_BY_3, UPDATED_DATE=GETDATE() WHERE FOLDER_PARENT_ID=@FOLDER_PARENT_ID", conn)

                    updateStatement1.Parameters.AddWithValue("@FOLDER_SELF_ID", Session("folderSelfId"))
                    updateStatement1.Parameters.AddWithValue("@FOLDER_NAME", txt_folder_name.Text)
                    updateStatement1.Parameters.AddWithValue("@UPDATED_BY", "")

                    updateStatement2.Parameters.AddWithValue("@FOLDER_NAME_2", txt_folder_name.Text)
                    updateStatement2.Parameters.AddWithValue("@UPDATED_BY_2", "")

                    updateStatement3.Parameters.AddWithValue("@FOLDER_PARENT_ID", Session("folderSelfId"))
                    updateStatement3.Parameters.AddWithValue("@FOLDER_NAME_3", txt_folder_name.Text)
                    updateStatement3.Parameters.AddWithValue("@UPDATED_BY_3", "")

                    updateStatement1.ExecuteNonQuery()
                    updateStatement2.ExecuteNonQuery()
                    updateStatement3.ExecuteNonQuery()

                    conn.Close()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Edit sub folder success');", True)
                    Response.Redirect("~\UploadFiles.aspx")
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error edit sub folder');", True)
                End Try
            End If
        ElseIf Session("sts") = 2 Then
            If txt_sub_folder_name.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Sub folder name cannot be empty');", True)
            Else
                Try
                    FileSystem.Rename(folderName + Session("oldFolder") + "\" + Session("oldSubFolder"), folderName + Session("oldFolder") + "\" + txt_sub_folder_name.Text)

                    Dim conn As New SqlConnection
                    conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")
                    conn.Open()

                    Dim updateStatement1 As New SqlCommand("UPDATE FILE_UPLOAD SET SUB_FOLDER_NAME=@SUB_FOLDER_NAME, UPDATED_BY=@UPDATED_BY_1, UPDATED_DATE=GETDATE() WHERE FOLDER_SELF_ID=@FOLDER_SELF_ID AND FOLDER_PARENT_ID <> 0", conn)

                    Dim updateStatement2 As New SqlCommand("UPDATE FILE_UPLOAD_FOLDER SET FOLDER_NAME=@FOLDER_NAME_2 WHERE FOLDER_LEVEL = 2 AND FOLDER_SELF_ID=" + Session("folderSelfId"), conn)

                    updateStatement1.Parameters.AddWithValue("@FOLDER_SELF_ID", Session("folderSelfId"))
                    updateStatement1.Parameters.AddWithValue("@SUB_FOLDER_NAME", txt_sub_folder_name.Text)
                    updateStatement1.Parameters.AddWithValue("@FOLDER_NAME_1", Session("oldFolder"))
                    updateStatement1.Parameters.AddWithValue("@UPDATED_BY_1", "")

                    updateStatement2.Parameters.AddWithValue("@FOLDER_NAME_2", txt_sub_folder_name.Text)
                    updateStatement2.Parameters.AddWithValue("@UPDATED_BY_2", "")

                    updateStatement1.ExecuteNonQuery()
                    updateStatement2.ExecuteNonQuery()

                    conn.Close()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Edit sub folder success');", True)
                    Response.Redirect("~\UploadFiles.aspx")
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error edit sub folder');", True)
                End Try
            End If
        ElseIf Session("sts") = 3 Then
            If txt_file_name.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('File name cannot be empty');", True)
            Else
                Try
                    FileSystem.Rename(folderName + Session("oldFolder") + "\" + Session("oldSubFolder") + "\" + Session("oldFileName"), folderName + Session("oldFolder") + "\" + Session("oldSubFolder") + "\" + txt_file_name.Text.Trim)

                    Dim conn As New SqlConnection
                    conn.ConnectionString = ConfigurationManager.AppSettings("POSWeb_SQLConn")
                    conn.Open()

                    Dim updateStatement As New SqlCommand("UPDATE FILE_UPLOAD SET FILE_NAME=@FILE_NAME, UPDATED_BY=@UPDATED_BY, UPDATED_DATE=GETDATE() WHERE ID=@ID", conn)

                    updateStatement.Parameters.AddWithValue("@ID", Request.QueryString("ID"))
                    updateStatement.Parameters.AddWithValue("@FILE_NAME", txt_file_name.Text)
                    updateStatement.Parameters.AddWithValue("@UPDATED_BY", "")

                    updateStatement.ExecuteNonQuery()

                    conn.Close()

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Edit sub folder success');", True)
                    Response.Redirect("~\UploadFiles.aspx")
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error edit sub folder');", True)
                End Try
            End If
        End If
    End Sub

    Protected Sub btnURL_Click(sender As Object, e As EventArgs) Handles btnURL.Click
        Try
            Dim serverURL = "https://tmconnect.tokiomarine-life.co.id/"
            Dim folderRootURL = "ProductRoot/"
            Dim fileFolderURL = txt_folder_name.Text.Trim + "/"
            Dim fileSubFolderURL = txt_sub_folder_name.Text.Trim + "/"
            Dim filenameURL = txt_file_name.Text.Trim

            Dim fullURL = serverURL + folderRootURL + fileFolderURL + fileSubFolderURL + filenameURL

            Page.ClientScript.RegisterStartupScript(Me.[GetType](), "OpenPDFScript", "window.open(' " + fullURL + "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Error view file');", True)
        End Try
    End Sub
End Class