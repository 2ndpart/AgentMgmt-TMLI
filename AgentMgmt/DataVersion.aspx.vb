Public Class DataVersion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("UserName") = String.Empty) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please re-login through Login page.');window.location='../TMLI_MPOS/Login.aspx';", True)
        End If
        If Not Page.IsPostBack Then
            ShowVersion()
        End If
        btn_update.Visible = False
        lbl_Newdataversion.Visible = False
        txt_newversion.Visible = False
        txt_dataversionDesc.Attributes.Add("readonly", "readonly")
        btn_back.Visible = False
    End Sub

    Protected Sub ShowVersion()
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim sql As String = "select * from TMLI_Data_Version"
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Try
            objDBCom.ExecuteSQL(dt, sql)
            objDBCom.Dispose()
            lbl_dataversion.Text = dt.Rows(0)(1).ToString()
            lbl_dataversiondate.Text = dt.Rows(0)(2).ToString()
            txt_dataversionDesc.Text = dt.Rows(0)(3).ToString()
        Catch
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('There are currently no data in Data Version')", True)
        End Try
    End Sub

    Protected Sub btn_update_Click(sender As Object, e As EventArgs) Handles btn_update.Click
        If Not IsEntryValid() Then
            btn_edit.Visible = False
            btn_update.Visible = True
            btn_back.Visible = True
            lbl_Newdataversion.Visible = True
            txt_newversion.Visible = True
            txt_dataversionDesc.Attributes.Remove("readonly")
            'txt_dataversionDesc.Text = ""
            Exit Sub
        Else
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
            Dim q1 As String = "Update TMLI_Data_Version set Version = '" + txt_newversion.Text + "', DateUpdate = GETDATE(), Remarks = '" + txt_dataversionDesc.Text + "'"
            If objDBCom.ExecuteSQL(q1) Then
                objDBCom.Dispose()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version successfully updated.');", True)
            Else
                objDBCom.Dispose()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version failed to update.');", True)
            End If
            txt_newversion.Text = ""
            btn_edit.Visible = True
            ShowVersion()
        End If
    End Sub

    Private Function IsEntryValid() As Boolean
        If txt_newversion.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version field cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If txt_dataversionDesc.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version description cannot be empty!\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If txt_newversion.Text < lbl_dataversion.Text Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Version number cannot be lower than current version!\nPlease insert a higher value.');", True)
            Return False
            Exit Function
        End If

        Return True
    End Function

    Protected Sub btn_edit_Click(sender As Object, e As EventArgs) Handles btn_edit.Click
        btn_edit.Visible = False
        btn_update.Visible = True
        btn_back.Visible = True
        lbl_Newdataversion.Visible = True
        txt_newversion.Visible = True
        txt_dataversionDesc.Attributes.Remove("readonly")
        'txt_dataversionDesc.Text = ""
    End Sub

    Protected Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        Response.Redirect("DataVersion.aspx")
    End Sub
End Class