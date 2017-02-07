Imports System.Security.Cryptography
Imports System.IO

Public Class EditAgentAdmin
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

    Protected Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        If Not IsEntryValid() Then
            Exit Sub
        Else
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
            Dim sql As String = "Update TMLI_Agent_Profile set AdminPassword = @AdminPass where Admin = @AdminName"
            Dim dt As New DataTable

            objDBCom.AddParameter("@AdminPass", SqlDbType.VarChar, ms.EncryptString(txt_new.Text, "1234567891123456"))
            objDBCom.AddParameter("@AdminName", SqlDbType.VarChar, txt_username.Text)

            If objDBCom.ExecuteSQL(dt, sql) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password update successfully');", True)
                txt_new.Text = ""
                txt_cnew.Text = ""
                loadprofile()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password update failed');", True)
            End If
            loadprofile()
        End If
    End Sub

    Private Function IsEntryValid() As Boolean
        If txt_password.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Old password field cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If txt_new.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password field cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If txt_cnew.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password Confirmation field cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If Not txt_cnew.Text = txt_new.Text Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password field Not match!\nPlease insert the same value.');", True)
            Return False
            Exit Function
        End If

        If Not dtl.Rows(0)(1).ToString() = ms.EncryptString(txt_password.Text, "1234567891123456") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Incorrect password.');", True)
            Return False
            Exit Function
        End If

        Return True
    End Function

    Protected Sub loadprofile()
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim sql As String = "Select distinct Admin , AdminPassword from TMLI_Agent_Profile"


        objDBCom.ExecuteSQL(dtl, sql)
        objDBCom.Dispose()

        txt_username.Text = dtl.Rows(0)(0).ToString()
    End Sub


End Class