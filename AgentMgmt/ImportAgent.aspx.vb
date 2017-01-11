Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net
Imports System.Net.Mail
Imports Microsoft.VisualBasic.FileIO
Imports System.Security.Cryptography

Public Class ImportAgent
    Inherits System.Web.UI.Page
    Dim table As New DataTable
    Dim table2 As New DataTable
    Dim table3 As New DataTable
    Dim dtcloned As New DataTable
    Dim erdb As New DataTable
    Dim ms As New Encryption
    Dim re As Integer = 0
    Dim x As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("UserName") = String.Empty) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please re-login through Login page.');window.location='../TMLI_MPOS/Login.aspx';", True)
        End If
        erdb.Columns.Add("AgentCode")
    End Sub

    'Protected Sub upload(ByVal sender As Object, ByVal e As EventArgs)

    'End Sub

    Protected Sub btn_upload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_upload.Click
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim isSuccess As Boolean = False
        Dim c As Integer = 0
        Dim dts As New DataTable
        Dim csvPath As String = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName)
        FileUpload1.SaveAs(csvPath)
        Dim dt As New DataTable()

        If File.Exists(csvPath) Then
            Using parser As New TextFieldParser(csvPath)
                ' set the parser variables
                parser.TextFieldType = FieldType.Delimited
                parser.SetDelimiters(",")
                Dim firstLine As Boolean = True
                While Not parser.EndOfData
                    'Processing row
                    Dim fields As String() = parser.ReadFields()
                    ' get the column headers
                    If firstLine Then
                        For Each val As String In fields
                            RTrim(val)
                            dt.Columns.Add(val)
                        Next
                        firstLine = False
                        Continue While
                    End If
                    ' get the row data
                    dt.Rows.Add(fields)

                    Dim i As Integer = 0
                    Dim j As DataRow = dt.NewRow()


                    Dim sql2 As String = "select distinct AdminCode, AdminPass from TMLI_Admin_profile"

                    objDBCom.ExecuteSQL(table2, sql2)

                    For Each cell As String In fields
                        dt.Rows(dt.Rows.Count - 1)(i) = cell
                        If i = 6 Then
                            dt.Rows(dt.Rows.Count - 1)(i) = "A"
                        ElseIf i = 11 Then
                            dt.Rows(dt.Rows.Count - 1)(i) = ms.EncryptString(dt.Rows(dt.Rows.Count - 1)(i), "1234567891123456")
                        ElseIf i = 12 Then
                            dt.Rows(dt.Rows.Count - 1)(i) = DateTime.Parse(dt.Rows(dt.Rows.Count - 1)(i))
                        ElseIf i = 13 Then
                            dt.Rows(dt.Rows.Count - 1)(i) = DateTime.Parse(dt.Rows(dt.Rows.Count - 1)(i))
                            'ElseIf i = 15 Then
                            '    dt.Rows(dt.Rows.Count - 1)(i) = table.Rows(0)(0).ToString()
                            'ElseIf i = 25 Then
                            '    dt.Rows(dt.Rows.Count - 1)(i) = 0
                            'ElseIf i = 26 Then
                            '    dt.Rows(dt.Rows.Count - 1)(i) = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))
                            'ElseIf i = 27 Then
                            '    dt.Rows(dt.Rows.Count - 1)(i) = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))
                            'ElseIf i = 28 Then
                            '    dt.Rows(dt.Rows.Count - 1)(i) = 0
                            'ElseIf i = 29 Then
                            '    dt.Rows(dt.Rows.Count - 1)(i) = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))
                            'ElseIf i = 30 Then
                            '    dt.Rows(dt.Rows.Count - 1)(i) = "A"
                            '    'ElseIf i = 31 Then
                            '    '    Dim sql As String = "select AgentPassword from Agent_profile where AgentCode = '" + dt.Rows(dt.Rows.Count - 1)(14) + "'"
                            '    '    objDBCom.ExecuteSQL(table, sql)
                            '    '    If table.Rows.Count = 0 Then
                            '    '        dt.Rows(dt.Rows.Count - 1)(i) = ms.EncryptString("admin", "1234567891123456")
                            '    '    Else
                            '    '        dt.Rows(dt.Rows.Count - 1)(i) = table.Rows(0)(0).ToString()
                            '    '    End If
                            'ElseIf i = 32 Then
                            '    dt.Rows(dt.Rows.Count - 1)(i) = table2.Rows(0)(0).ToString()
                            'ElseIf i = 33 Then
                            '    dt.Rows(dt.Rows.Count - 1)(i) = table2.Rows(0)(1).ToString()
                            '    'ElseIf i = 34 Then
                            '    '    Dim sql7 As String = "select AgentStatus from Agent_profile where AgentCode = '" + dt.Rows(dt.Rows.Count - 1)(14) + "'"
                            '    '    objDBCom.ExecuteSQL(table3, sql7)
                            '    '    If table3.Rows.Count = 0 Then
                            '    '        dt.Rows(dt.Rows.Count - 1)(i) = "A"
                            '    '    Else
                            '    '        dt.Rows(dt.Rows.Count - 1)(i) = table3.Rows(0)(0).ToString()
                            '    '    End If
                        End If
                        i += 1
                    Next

                End While
            End Using
        End If


        dtCloned = dt.Clone()
        dtcloned.Columns(12).DataType = GetType(DateTime)
        dtcloned.Columns(13).DataType = GetType(DateTime)
        For Each rt As DataRow In dt.Rows
            dtCloned.ImportRow(rt)
        Next
        isSuccess = True

        If isSuccess = True Then
            Dim q1 As String = ""
            Dim x As String = ""
            q1 &= "select AgentCode, AgentEmail from Agent_profile"
            objDBCom.ExecuteSQL(dts, q1)
            For Each dtrow As DataRow In dts.Rows
                For count As Integer = 0 To dts.Rows.Count - 1
                    Dim found As Boolean = True
                    Dim rowCount1 As Integer = dtcloned.Rows.Count - 1
                    For count1 As Integer = 0 To rowCount1
                        If count1 <= rowCount1 Then
                            If dts.Rows(count)(0).ToString() = dtcloned.Rows(count1)(3).ToString() Then
                                'dtclone.Rows(count1).Delete()
                                x = count1
                                found = False
                                If found = False Then
                                    dtcloned.Rows(x).Delete()
                                    rowCount1 -= 1
                                End If
                            ElseIf dts.Rows(count)(1).ToString() = dtcloned.Rows(count1)(5).ToString() Then
                                x = count1
                                found = False
                                If found = False Then
                                    dtcloned.Rows(x).Delete()
                                    rowCount1 -= 1
                                End If
                            End If
                        End If
                    Next
                Next
            Next

            Dim conn As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
            Using con As New SqlConnection(conn)
                Using SqlBulkCopy As New SqlBulkCopy(con)
                    SqlBulkCopy.DestinationTableName = "dbo.Agent_profile"
                    con.Open()
                    SqlBulkCopy.WriteToServer(dtcloned)
                    con.Close()
                End Using
            End Using


            Dim sql As String = ""
            Dim sql2 As String = ""

            For Each r As DataRow In dtcloned.Rows
                checkavailablitity()
                sql &= "update Agent_profile set LastLogonDate = NULL, LastLogoutDate = NULL, LastSyncDate = NULL, UDID = NULL, DeviceStatus = 'A', Admin = '" + table2.Rows(0)(0).ToString() + "', AdminPassword = '" + table2.Rows(0)(1).ToString() + "' where AgentCode = '" + dtcloned.Rows(re)(3).ToString() + "'"
                sql2 &= "update Agent_profile set LicenseStartDate = dateadd(MILLISECOND, 123, dateadd(day, datediff(day, 0, LicenseStartDate), 0)), LicenseExpiryDate = dateadd(MILLISECOND, (14 * 60) + 40, dateadd(day, datediff(day, 0, LicenseExpiryDate), 0)) where AgentCode = '" + dtcloned.Rows(re)(3).ToString() + "'"
                objDBCom.ExecuteSQL(sql)
                objDBCom.ExecuteSQL(sql2)
                re += 1
            Next

            For Each email As DataRow In dtcloned.Rows
                Try
                    Dim mposemail As String = System.Configuration.ConfigurationManager.AppSettings("emailAddress").ToString()
                    Dim mpospass As String = System.Configuration.ConfigurationManager.AppSettings("emailPassword").ToString()
                    Dim useremail As String = dtcloned.Rows(c)(5).ToString()
                    Dim agentname As String = dtcloned.Rows(c)(4).ToString()
                    Dim agentcode As String = dtcloned.Rows(c)(3).ToString()
                    Dim password As String = ms.DecryptString(dtcloned.Rows(c)(11).ToString(), "1234567891123456")
                    Dim smtp As New SmtpClient()
                    Dim message As New MailMessage(mposemail, useremail)
                    message.Subject = "TMConnect Installation Invite"
                    message.Body = String.Format("Dear {0},<br /><br />Good day.<br /><br />Your account have been created and have been activated to use TMConnect." &
                                                "<br /><br />Agent Code : {1}<br />Agent Password : {2}<br /><br />Please click the link below to download TMConnect into your iPad." &
                                                "<br /><br />http://mposws.azurewebsites.net/webservices/LoginSite.aspx<br /><br />Sincerely,<br />Tokio Marine Life Indonesia", agentname, agentcode, password)
                    message.IsBodyHtml = True
                    smtp.Host = "smtp.gmail.com"
                    smtp.EnableSsl = True
                    Dim NetworkCred As New NetworkCredential(mposemail, mpospass)
                    smtp.Credentials = NetworkCred
                    smtp.Port = 587
                    smtp.Timeout = 60000
                    smtp.Send(message)
                Catch ex As Exception
                    'erdb.Rows.Add(dtcloned.Rows(c)(3).ToString())
                End Try
                c += 1
            Next

            objDBCom.Dispose()
            File.Delete(csvPath)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Row(s) successfully added');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Row(s) failed to add');", True)
        End If
    End Sub

    Private Sub checkavailablitity()
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim sql6 As String = "select AgentPassword from Agent_profile where AgentCode = '" + dtcloned.Rows(x)(14) + "'"
        Dim sql7 As String = "select AgentStatus from Agent_profile where AgentCode = '" + dtcloned.Rows(x)(14) + "'"

        objDBCom.ExecuteSQL(table, sql6)
        If Not table.Rows.Count = 0 Then
            Dim sql8 As String = "update Agent_profile set DirectSupervisorPassword = '" + table.Rows(0)(0).ToString + "' where AgentCode = '" + dtcloned.Rows(x)(3) + "'"
            objDBCom.ExecuteSQL(sql8)
        End If

        objDBCom.ExecuteSQL(table3, sql7)
        If Not table.Rows.Count = 0 Then
            Dim sql9 As String = "update Agent_profile set DirectSupervisorStatus = '" + table3.Rows(0)(0).ToString + "' where AgentCode = '" + dtcloned.Rows(x)(3) + "'"
            objDBCom.ExecuteSQL(sql9)
        Else
            Dim sql10 As String = "update Agent_profile set DirectSupervisorStatus = 'A' where AgentCode = '" + dtcloned.Rows(x)(3) + "'"
            objDBCom.ExecuteSQL(sql10)
        End If
        x += 1
    End Sub


End Class