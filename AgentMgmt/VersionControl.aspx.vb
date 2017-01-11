Imports System.IO
Imports System.Net
Imports System.Text

Public Class VersionControl
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("UserName") = String.Empty) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please re-login through Login page.');window.location='../TMLI_MPOS/Login.aspx';", True)
        Else
            ShowVersion()
        End If
    End Sub

    Protected Sub btn_upgrade_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_upgrade.Click
        If Not IsEntryValid() Then
            txt_Version.Text = ""
            Exit Sub
        Else
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
            Dim q1 As String = "Insert into TMLI_Version_checker values ('" + txt_Version.Text + "', GETDATE(), '" + txt_versionDesc.Text + "')"
            If objDBCom.ExecuteSQL(q1) Then
                objDBCom.Dispose()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version successfully updated.');", True)
            Else
                objDBCom.Dispose()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version failed to update.');", True)
            End If
            txt_Version.Text = ""
            txt_versionDesc.Text = ""
            ShowVersion()
        End If
    End Sub

    Protected Sub ShowVersion()
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim sql As String = "select * from TMLI_Version_checker where VersionNo = (select MAX(VersionNo) as NewestVersion from TMLI_Version_checker)"
        Dim sql2 As String = "select VersionNo from TMLI_Version_checker order by VersionNo DESC"
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        objDBCom.ExecuteSQL(dt, sql)
        objDBCom.ExecuteSQL(dt2, sql2)
        objDBCom.Dispose()
        Rept.DataSource = dt2
        Rept.DataBind()
        lbl_version.Text = dt.Rows(0)(0).ToString()
        lbl_VersionDate.Text = dt.Rows(0)(1).ToString()
        Dim ver As Integer = 0

        For Each item In Rept.Items
            Dim link As New LinkButton
            link = DirectCast(item.FindControl("LinkButton1"), LinkButton)
            link.Attributes.Add("onclick", "javascript:window.open('VersionControlResult.aspx?VersionNo=" + dt2.Rows(ver)(0).ToString() + "', 'MsgWindow', 'width=550,height=600');")
            ver += 1
        Next
    End Sub

    Private Function IsEntryValid() As Boolean
        If txt_Version.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version field cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If txt_versionDesc.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version description cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If txt_Version.Text < lbl_version.Text Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version number cannot be lower than current version!\nPlease insert a higher value.');", True)
            Return False
            Exit Function
        End If

        Return True
    End Function


    'Protected Sub FTPUpload(sender As Object, e As EventArgs) Handles btn_upload.Click
    '    Dim ftp As String = "ftp://waws-prod-sg1-011.ftp.azurewebsites.windows.net"
    '    Dim ftpfolder As String = "/site/FTPBrochure/"
    '    Dim fileBytes As Byte() = Nothing

    '    Dim filename As String = Path.GetFileName(FileUpload1.FileName)
    '    Using FileStream As New StreamReader(FileUpload1.PostedFile.InputStream)
    '        fileBytes = Encoding.UTF8.GetBytes(FileStream.ReadToEnd())
    '        FileStream.Close()
    '    End Using

    '    Try
    '        Dim request As FtpWebRequest = DirectCast(WebRequest.Create(ftp & ftpfolder & filename), FtpWebRequest)
    '        request.Method = WebRequestMethods.Ftp.UploadFile

    '        request.Credentials = New NetworkCredential("bcalife\mpos", "!nfoC0nnect")
    '        request.ContentLength = fileBytes.Length
    '        request.UsePassive = True
    '        request.UseBinary = True
    '        request.ServicePoint.ConnectionLimit = fileBytes.Length
    '        request.EnableSsl = False

    '        Using requestStream As Stream = request.GetRequestStream()
    '            requestStream.Write(fileBytes, 0, fileBytes.Length)
    '            requestStream.Close()
    '        End Using

    '        Dim response As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Uploaded Successfully.');", True)
    '        response.Close()

    '    Catch ex As WebException
    '        Throw New Exception(TryCast(ex.Response, FtpWebResponse).StatusDescription)
    '    End Try
    'End Sub
End Class