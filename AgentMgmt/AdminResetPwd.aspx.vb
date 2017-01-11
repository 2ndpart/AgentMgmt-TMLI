Public Class AdminResetPwd
    Inherits System.Web.UI.Page
    Dim ms As New Encryption

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim s As String = Request.QueryString("UserName")
        Label1.Text = s
        If s.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Reset process failed! Please contact admin.');", True)
        End If
    End Sub

    Protected Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        If Not IsEntryValid() Then
            Exit Sub
        Else
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
            Dim sql As String = "Update TMLI_UserLogin set Password = '" + ms.EncryptString(TextBox2.Text, "1234567891123456") + "', PasswordChangeSince = getdate() where UserName = '" + Label1.Text + "'"

            If objDBCom.ExecuteSQL(sql) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password update successfully.');", True)
                TextBox2.Text = ""
                TextBox3.Text = ""
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password update failed.');", True)
            End If
        End If
    End Sub

    Private Function IsEntryValid() As Boolean

        If TextBox2.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password field cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If TextBox3.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password Confirmation field cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If Not TextBox2.Text = TextBox3.Text Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password field Not match!\nPlease insert the same value.');", True)
            Return False
            Exit Function
        End If

        Return True
    End Function

End Class