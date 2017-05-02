Public Class EditAdmin
    Inherits System.Web.UI.Page
    Dim ms As New Encryption
    Dim dtl As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("UserName") = String.Empty) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please re-login through Login page.');window.location='../TMLI_MPOS/Login.aspx';", True)
        ElseIf (Session("Roles") = "User") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('You do not have the access right for this page');window.location='../TMLI_MPOS/AgentProfile.aspx';", True)
        Else
            loadprofile()
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not IsEntryValid() Then
            Exit Sub
        Else
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
            Dim sql As String = "Update TMLI_UserLogin set Password = @Password, PasswordChangeSince = getdate() where UserName = @Username"
            Dim dt As New DataTable

            objDBCom.AddParameter("@Password", SqlDbType.VarChar, ms.EncryptString(txt_password.Text, "1234567891123456"))
            objDBCom.AddParameter("@Username", SqlDbType.VarChar, txt_username.Text)

            If objDBCom.ExecuteSQL(dt, sql) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password update successfully');", True)
                txt_password.Text = ""
                TextBox4.Text = ""
                txt_old.Text = ""
                loadprofile()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password update failed');", True)
            End If

        End If
    End Sub

    Private Function IsEntryValid() As Boolean
        If txt_old.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Old password field cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If txt_password.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password field cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If TextBox4.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password Confirmation field cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If Not TextBox4.Text = txt_password.Text Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password field Not match!\nPlease insert the same value.');", True)
            Return False
            Exit Function
        End If

        If Not dtl.Rows(0)(1).ToString() = ms.EncryptString(txt_old.Text, "1234567891123456") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Old password incorrecnt');", True)
            Return False
            Exit Function
        End If

        Return True
    End Function

    Protected Sub loadprofile()
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim q1 As String = "select * from TMLI_UserLogin where UserName = '" + Session("UserName").ToString + "'"

        objDBCom.ExecuteSQL(dtl, q1)
        objDBCom.Dispose()

        txt_username.Text = dtl.Rows(0)(1).ToString()
    End Sub
End Class