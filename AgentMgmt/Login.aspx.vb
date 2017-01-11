Imports System.Net.Mail
Imports System.Net

Public Class Login
    Inherits System.Web.UI.Page
    Dim ms As New Encryption

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Clear()
        If Not Session("UserName") = Nothing Then
            Response.Redirect("Logout.aspx")
        End If
    End Sub

    Protected Sub btn_login_Click(sender As Object, e As EventArgs) Handles btn_login.Click
        Login(txt_user.Text.Trim, ms.EncryptString(txt_password.Text.Trim, "1234567891123456"))
    End Sub

    Private Sub Login(ByVal LoginID As String, ByVal Password As String)

        Dim dt As New DataTable
        Dim strSQL As String
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        objDBCom.Parameters.Clear()

        'Only allow administrator or report user level

        strSQL = " SELECT [UserName], [Password], [Roles], [PasswordChangeSince] FROM TMLI_UserLogin " &
                " WHERE [UserName] = '" & LoginID & "' Collate SQL_Latin1_General_CP1_CS_AS" &
                " AND [Password] = '" & Password & "' Collate SQL_Latin1_General_CP1_CS_AS"

        If objDBCom.ExecuteSQL(dt, strSQL) Then

        Else

        End If

        objDBCom.Dispose()

        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows

                Session("UserName") = dr("UserName")
                Session("Pwd") = dr("Password")
                Session("Roles") = dr("Roles")
                Session("PasswordChangeSince") = dr("PasswordChangeSince")

            Next
            txt_user.Text = ""
            txt_password.Text = ""
            Response.Redirect("AgentProfile.aspx")
        Else
            Session("UserName") = String.Empty
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Login failed! Invalid username or password!');", True)
        End If
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Dim username As String = txt_user.Text
        Dim email As String = ""
        Dim sql As String = "select UserEmail from TMLI_UserLogin where UserName = '" + txt_user.Text + "'"
        Dim dt As New DataTable

        If username.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please insert your username in the input field!');", True)
        Else
            Try
                Dim mposemail As String = System.Configuration.ConfigurationManager.AppSettings("emailAddress").ToString()
                Dim mpospass As String = System.Configuration.ConfigurationManager.AppSettings("emailPassword").ToString()
                Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
                objDBCom.ExecuteSQL(dt, sql)
                objDBCom.Dispose()
                email = dt.Rows(0)(0).ToString
                Dim smtp As New SmtpClient()
                Dim message As New MailMessage(mposemail, email)


                message.Subject = "Reset Password"
                message.IsBodyHtml = True
                message.Body = String.Format("Good day,<br /><br />Please click on the link below to reset your password." &
                                             "<br /><br />http://mposws.azurewebsites.net/mpostmli/AdminResetPwd.aspx?UserName=" & "{0}" &
                                             "<br /><br />Sincerely,<br />Tokio Marine Life Indonesia", username)

                smtp.Host = "smtp.gmail.com"
                smtp.EnableSsl = True
                Dim NetworkCred As New NetworkCredential(mposemail, mpospass)
                smtp.Credentials = NetworkCred
                smtp.Port = 587
                smtp.Timeout = 60000
                smtp.Send(message)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Email successfully send.');", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Email failed to send.Please check if email is valid or try again later.');", True)
            End Try

        End If
    End Sub


End Class