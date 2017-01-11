Imports System.Net
Imports System.Net.Mail
Imports System.Threading
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography

Public Class AgentProfile
    Inherits System.Web.UI.Page
    Dim dtg As New DataTable
    Dim ms As New Encryption
    Dim directdt As New DataTable
    Dim encrypt As New Encryption

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DisableAll()
        pnl_showDetails.Visible = False
        If Not Me.IsPostBack Then
            If (Session("UserName") = String.Empty) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please re-login through Login page.');window.location='../TMLI_MPOS/Login.aspx';", True)
            End If
            LoadAgentProfile(1)
        End If
    End Sub

    Protected Sub LoadAgentProfile(ByVal pageIndex As Integer)
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(CStr(POSWeb.POSWeb_SQLConn))
        Dim dt As New DataSet

        Dim sql As String = "SELECT * from CRAZY_DATA"
        Dim condition As String = String.Empty
        Dim search As String = String.Empty

        'For Each item As ListItem In chkStatus.Items
        '    condition += If(item.Selected, String.Format("'{0}',", item.Value), String.Empty)
        'Next

        For Each oItem As ListItem In chkStatus.Items
            If oItem.Selected Then
                condition += oItem.Value + ","
            End If
        Next

        If condition = "" Then condition = "Active,Terminate,Suspend,Resign"

        'If Not String.IsNullOrEmpty(condition) Then
        '    condition = String.Format("{0}", condition.Substring(0, condition.Length - 1))
        'End If

        'Dim sqlquery3 As String = "Select count(*) from CRAZY_DATA where AgentStatus IN (" + condition + ")"
        'Dim sqlquery2 As String = "Select count(*) from CRAZY_DATA where AgentStatus IN ('Active')"
        'Dim temptable As New DataTable

        objDBCom.AddParameter("@stringToFind", SqlDbType.VarChar, txt_search.Text)
        'objDBCom.AddParameter("@schema", SqlDbType.VarChar, "dbo")
        'objDBCom.AddParameter("@table", SqlDbType.VarChar, "CRAZY_DATA")
        objDBCom.AddParameter("@Status", SqlDbType.VarChar, condition)
        'objDBCom.AddParameter("@Page", SqlDbType.VarChar, pageIndex)
        'objDBCom.AddParameter("@Row", SqlDbType.VarChar, ddlPaging.SelectedItem.Text)
        objDBCom.AddParameter("@ColumnName", SqlDbType.VarChar, ddl_criteria.SelectedValue)
        'objDBCom.ExecuteProcedure(dt, "TMLI_Agent_FindStringInTable")

        objDBCom.AddParameter("@PageIndex", SqlDbType.VarChar, pageIndex)
        objDBCom.AddParameter("@PageSize", SqlDbType.VarChar, ddlPaging.SelectedItem.Text)
        objDBCom.AddParameter("@RecordCount", SqlDbType.Int, 4)
        objDBCom.Parameters("@RecordCount").Direction = ParameterDirection.Output
        objDBCom.ExecuteProcedure(dt, "AgentProfilePaging")

        'If chkStatus.SelectedValue = "" Then
        '    objDBCom.ExecuteSQL(temptable, sqlquery2)
        'Else
        '    objDBCom.ExecuteSQL(temptable, sqlquery3)
        'End If

        If dt.Tables(0).Rows.Count <> 0 Then
            ViewState("Table") = dt
        End If

        GridView1.DataSource = dt.Tables(0)
        GridView1.DataBind()

        'Dim recordCount As Integer = 0

        'If txt_search.Text = "" Then
        '    recordCount = Convert.ToInt32(temptable.Rows(0)(0).ToString())
        'Else
        '    recordCount = dt.Tables(1).Rows.Count()
        'End If
        Dim recordCount As Integer = Convert.ToInt32(objDBCom.Parameters("@RecordCount").Value)
        Me.PopulatePager(recordCount, pageIndex)
        objDBCom.Dispose()
        'txt_search.Text = ""
    End Sub

    Private Sub DisableAll()
        btn_clear.Visible = False
        txt_AccHolder.Attributes.Add("readonly", "readonly")
        txt_AccNum.Attributes.Add("readonly", "readonly")
        txt_add1.Attributes.Add("readonly", "readonly")
        txt_add2.Attributes.Add("readonly", "readonly")
        txt_add3.Attributes.Add("readonly", "readonly")
        txt_add4.Attributes.Add("readonly", "readonly")
        txt_add5.Attributes.Add("readonly", "readonly")
        txt_AddType.Attributes.Add("readonly", "readonly")
        txt_AgentStatus.Attributes.Add("readonly", "readonly")
        txt_AgentType.Attributes.Add("readonly", "readonly")
        txt_bankcode.Attributes.Add("readonly", "readonly")
        txt_channel.Attributes.Add("readonly", "readonly")
        txt_Code.Attributes.Add("readonly", "readonly")
        txt_CountryCode.Attributes.Add("readonly", "readonly")
        txt_dob.Attributes.Add("readonly", "readonly")
        txt_email.Attributes.Add("readonly", "readonly")
        txt_Fname.Attributes.Add("readonly", "readonly")
        txt_gender.Attributes.Add("readonly", "readonly")
        txt_HireDate.Attributes.Add("readonly", "readonly")
        txt_hp1.Attributes.Add("readonly", "readonly")
        txt_hp2.Attributes.Add("readonly", "readonly")
        txt_id.Attributes.Add("readonly", "readonly")
        txt_LastLogin.Attributes.Add("readonly", "readonly")
        txt_LeaderCode.Attributes.Add("readonly", "readonly")
        txt_LicenseExpiryC.Attributes.Add("readonly", "readonly")
        txt_LicenseExpiryS.Attributes.Add("readonly", "readonly")
        txt_LicenseNumC.Attributes.Add("readonly", "readonly")
        txt_LicenseNumS.Attributes.Add("readonly", "readonly")
        txt_Lname.Attributes.Add("readonly", "readonly")
        txt_marryStatus.Attributes.Add("readonly", "readonly")
        txt_Mphone.Attributes.Add("readonly", "readonly")
        txt_nationality.Attributes.Add("readonly", "readonly")
        txt_NPWP.Attributes.Add("readonly", "readonly")
        'txt_pass.Attributes.Add("readonly", "readonly")
        txt_PosCode.Attributes.Add("readonly", "readonly")
        txt_Religion.Attributes.Add("readonly", "readonly")
        txt_salution.Attributes.Add("readonly", "readonly")
        txt_TaxStatus.Attributes.Add("readonly", " readonly")
        txt_terminateDate.Attributes.Add("readonly", "readonly")
        txt_UpdateDate.Attributes.Add("readonly", "readonly")

        'txt_pass.Attributes("type") = "password"
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(1).Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(Me.GridView1, "Select$" + e.Row.RowIndex.ToString())
            e.Row.Cells(2).Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(Me.GridView1, "Select$" + e.Row.RowIndex.ToString())
            e.Row.Cells(3).Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(Me.GridView1, "Select$" + e.Row.RowIndex.ToString())
            e.Row.Cells(4).Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(Me.GridView1, "Select$" + e.Row.RowIndex.ToString())
            e.Row.Cells(5).Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(Me.GridView1, "Select$" + e.Row.RowIndex.ToString())
            e.Row.Cells(6).Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(Me.GridView1, "Select$" + e.Row.RowIndex.ToString())
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(CStr(POSWeb.POSWeb_SQLConn))
        Dim row As GridViewRow = GridView1.SelectedRow
        Dim dt As New DataTable

        Dim sql As String = "select AgentCode,AgentPassword,AgentName,LGIVNAME,CLTSEX,CLTDOB,SECUITYNO,SALUTL,NATLTY,MARRYD,ADDRTYPE
,AgentAddr1,AgentAddr2,AgentAddr3,CLTADDR04,CLTADDR05,CLTPCODE,CTRYCODE,AgentEmail,AgentContactNumber
,CLTPHONE01,CLTPHONE02,TAXMETH,XREFNO,ZRELIGN,LicenseStartDate,LicenseExpiryDate,Level,SALESUNIT,DirectSupervisorCode
,TLAGLICNO,TLICEXPDT,TLAGLICNO_S,TLICEXPDT_S,BANKKEY,BANKACOUNT,BANKACCDSC,AgentStatus,UPDATEDATE,LastLogonDate
,UDID,DeviceStatus,Admin,AdminPassword from TMLI_Agent_Profile where AgentCode = '" + row.Cells(1).Text + "'"
        objDBCom.ExecuteSQL(dt, sql)
        objDBCom.Dispose()

        If dt.Rows.Count <> 0 Then
            txt_AccHolder.Text = dt.Rows(0)(36).ToString()
            txt_AccNum.Text = dt.Rows(0)(35).ToString()
            txt_add1.Text = dt.Rows(0)(11).ToString()
            txt_add2.Text = dt.Rows(0)(12).ToString()
            txt_add3.Text = dt.Rows(0)(13).ToString()
            txt_add4.Text = dt.Rows(0)(14).ToString()
            txt_add5.Text = dt.Rows(0)(15).ToString()
            txt_AddType.Text = dt.Rows(0)(10).ToString()
            txt_AgentStatus.Text = dt.Rows(0)(37).ToString()
            txt_AgentType.Text = dt.Rows(0)(27).ToString()
            txt_bankcode.Text = dt.Rows(0)(34).ToString()
            txt_channel.Text = dt.Rows(0)(28).ToString()
            txt_Code.Text = dt.Rows(0)(0).ToString()
            txt_CountryCode.Text = dt.Rows(0)(17).ToString()
            txt_dob.Text = dt.Rows(0)(5).ToString()
            txt_email.Text = dt.Rows(0)(18).ToString()
            txt_Fname.Text = dt.Rows(0)(2).ToString()
            txt_gender.Text = dt.Rows(0)(4).ToString()
            txt_HireDate.Text = dt.Rows(0)(25).ToString()
            txt_hp1.Text = dt.Rows(0)(20).ToString()
            txt_hp2.Text = dt.Rows(0)(21).ToString()
            txt_id.Text = dt.Rows(0)(6).ToString()
            txt_LastLogin.Text = dt.Rows(0)(39).ToString()
            txt_LeaderCode.Text = dt.Rows(0)(29).ToString()
            txt_LicenseExpiryC.Text = dt.Rows(0)(31).ToString()
            txt_LicenseExpiryS.Text = dt.Rows(0)(33).ToString()
            txt_LicenseNumC.Text = dt.Rows(0)(30).ToString()
            txt_LicenseNumS.Text = dt.Rows(0)(32).ToString()
            txt_Lname.Text = dt.Rows(0)(3).ToString()
            txt_marryStatus.Text = dt.Rows(0)(9).ToString()
            txt_Mphone.Text = dt.Rows(0)(19).ToString()
            txt_nationality.Text = dt.Rows(0)(8).ToString()
            txt_NPWP.Text = dt.Rows(0)(23).ToString()
            txt_pass.Text = dt.Rows(0)(1).ToString()
            txt_PosCode.Text = dt.Rows(0)(16).ToString()
            txt_Religion.Text = dt.Rows(0)(24).ToString()
            txt_salution.Text = dt.Rows(0)(7).ToString()
            txt_TaxStatus.Text = dt.Rows(0)(22).ToString()
            txt_terminateDate.Text = dt.Rows(0)(26).ToString()
            txt_UpdateDate.Text = dt.Rows(0)(38).ToString()

            If dt.Rows(0)(41).ToString().Equals("A") Then
                ddlDevStatus.SelectedIndex = 0
            Else
                ddlDevStatus.SelectedIndex = 1
            End If

            If txt_AgentStatus.Text.Equals("Active") Then
                btn_resend.Visible = True
            Else
                btn_resend.Visible = False
            End If

            pnl_showDetails.Visible = True
            pnl_showgridview.Visible = False
        End If

    End Sub

    Protected Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        pnl_showDetails.Visible = False
        pnl_showgridview.Visible = True
        btn_clear.Visible = True
    End Sub

    Protected Sub ddlPaging_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaging.SelectedIndexChanged
        GridView1.PageSize = Int32.Parse(ddlPaging.SelectedValue.ToString())
        If Not txt_search.Text.Equals("") Then
            btn_clear.Visible = True
        End If
        LoadAgentProfile(1)
    End Sub

    Protected Sub Status_select(sender As Object, e As EventArgs) Handles chkStatus.SelectedIndexChanged
        If Not txt_search.Text.Equals("") Then
            btn_clear.Visible = True
        End If
        Me.LoadAgentProfile(1)
    End Sub

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        selectedrow()
        Dim sortingDirection As String = String.Empty
        If direction = SortDirection.Ascending Then
            direction = SortDirection.Descending
            sortingDirection = "Desc"
        Else
            direction = SortDirection.Ascending
            sortingDirection = "Asc"
        End If
        Dim sortedView As New DataView(DirectCast(ViewState("Table"), DataTable))
        sortedView.Sort = Convert.ToString(e.SortExpression + " ") & sortingDirection
        Session("objects") = sortedView
        GridView1.DataSource = sortedView
        GridView1.DataBind()
        populatecheckbox()
    End Sub

    Public Property direction() As SortDirection
        Get
            If ViewState("directionState") Is Nothing Then
                ViewState("directionState") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("directionState"), SortDirection)
        End Get
        Set
            ViewState("directionState") = Value
        End Set
    End Property

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        If Not txt_search.Text.Equals("") Then
            btn_clear.Visible = True
        Else
            btn_clear.Visible = False
        End If
        GridView1.PageIndex = e.NewPageIndex
        Me.LoadAgentProfile(e.NewPageIndex)
    End Sub

    Protected Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        btn_clear.Visible = True
        Me.LoadAgentProfile(1)
    End Sub

    Protected Sub btn_resend_Click(sender As Object, e As EventArgs) Handles btn_resend.Click
        If txt_AgentStatus.Text.Equals("Active") Then
            Try
                'Dim useremail As String = System.Configuration.ConfigurationManager.AppSettings("emailAddress").ToString()
                'Dim userpass As String = System.Configuration.ConfigurationManager.AppSettings("emailPassword").ToString()
                'Dim useremail As String = "tmconnect.no-reply@tokiomarine-life.co.id"
                'Dim newUserName As String = "tmli\tmconnect.no-reply"
                'Dim userpass As String = "tmc0nn3ct!"
                'Dim body As String = ""
                'Using sr = New StreamReader(Server.MapPath("~\EmailTemplate.html"))
                '    body = sr.ReadToEnd()
                'End Using

                'Dim ipadimage As String = "http://simpleicon.com/wp-content/uploads/ipad-portrait.png"
                'Dim desktopimage As String = "https://image.freepik.com/free-icon/desktop-button_318-25227.jpg"

                'Dim smtp As New SmtpClient()
                'Dim message As New MailMessage(useremail, txt_email.Text)

                'message.Subject = "Aktivasi Aplikasi TM Connect"
                'message.Body = String.Format(body, "https://tmconnect.tokiomarine-life.co.id/anvoaiwenvwae0v-wevjhweivnawuen12j3n12m%20asjkdfna%20sdjf%20123.html", ipadimage, "http://www.google.com",
                '    desktopimage, txt_Code.Text, encrypt.DecryptString(txt_pass.Text, "1234567891123456"))
                'message.IsBodyHtml = True
                ''message.IsBodyHtml = True
                'smtp.Host = "192.168.1.27"
                ''smtp.EnableSsl = Truek
                'smtp.DeliveryMethod = SmtpDeliveryMethod.Network
                'smtp.UseDefaultCredentials = False
                ''Dim NetworkCred As 
                'smtp.Credentials = New NetworkCredential(newUserName, userpass)
                'smtp.Port = 25
                'smtp.Timeout = 60000
                'smtp.Send(message)


                'Dim ProcessInfo As Process = Process.Start("java.exe", " -jar ""C:\Users\User\Documents\NetBeansProjects\TMConnectMailSender\dist\TMConnectMailSender.jar""")
                'Shell("java.exe -jar ""C:\Users\User\Documents\NetBeansProjects\TMConnectMailSender\dist\TMConnectMailSender.jar""")



                Dim body As String = ""
                Using sr = New StreamReader(Server.MapPath("~\EmailTemplate.html"))
                    body = sr.ReadToEnd()
                End Using

                Dim ipadimage As String = "http://simpleicon.com/wp-content/uploads/ipad-portrait.png"
                Dim desktopimage As String = "https://image.freepik.com/free-icon/desktop-button_318-25227.jpg"
                Dim messageBody As String = String.Format(body, "https://tmconnect.tokiomarine-life.co.id/anvoaiwenvwae0v-wevjhweivnawuen12j3n12m%20asjkdfna%20sdjf%20123.html", ipadimage, "http://www.google.com",
                                                      desktopimage, txt_Code.Text, encrypt.DecryptString(txt_pass.Text, "1234567891123456"))

                Dim messageId = txt_Code.Text
                Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(CStr(POSWeb.POSWeb_SQLConn))
                Dim sqlInsert As String = "INSERT INTO TMLI_SendEmail VALUES (@id,@emailTo,@content,@status)"
                Dim comm As New SqlCommand(sqlInsert)
                comm.Parameters.AddWithValue("@id", txt_Code.Text)
                comm.Parameters.AddWithValue("@emailTo", txt_email.Text)
                comm.Parameters.AddWithValue("@content", messageBody)
                comm.Parameters.AddWithValue("@status", "Sending")
                Dim result As Boolean = objDBCom.ExecuteSqlCommand(comm)

                If result Then
                    Dim processinfo As New ProcessStartInfo()
                    processinfo.WorkingDirectory = "C:\MailSender"
                    processinfo.FileName = "java.exe"
                    processinfo.Arguments = "-jar TestJar.jar " & messageId

                    Dim myProcess As New Process()
                    processinfo.UseShellExecute = False
                    processinfo.RedirectStandardOutput = True
                    myProcess.StartInfo = processinfo
                    myProcess.Start()
                    Dim myStreamReader As StreamReader = myProcess.StandardOutput
                    ' Read the standard output of the spawned process. 
                    Dim finalString As String
                    Dim myString As String
                    Do
                        myString = myStreamReader.ReadLine()
                        finalString += myString
                    Loop Until (myStreamReader.EndOfStream)
                    myProcess.WaitForExit()
                    myProcess.Close()


                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alert", "alert('Success!');", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alert", "alert('Fail on saving data to DB!');", True)
                End If

            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alert", "alert('Failed to send, please try again later.');", True)
            End Try
        Else
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alert", "alert('Failed to send due to Agent Status not active.');", True)
        End If

        pnl_showDetails.Visible = True
    End Sub

    Protected Sub btn_resetPass_Click(sender As Object, e As EventArgs) Handles btn_resetPass.Click
        Try
            Dim useremail As String = "tmconnect.no-reply@tokiomarine-life.co.id"
            Dim newUserName As String = "tmli\tmconnect.no-reply"
            Dim userpass As String = "tmc0nn3ct!"
            Dim smtp As New SmtpClient()
            Dim message As New MailMessage(useremail, txt_email.Text)
            message.Subject = "Reset TMLI TMConnect Password"
            message.Body = String.Format("Dear {0},<br /><br />Good day.<br /><br />Please click on the link below to reset your password." &
                                         "<br /><br />https://tmconnect.tokiomarine-life.co.id/TMLI_MPOS/AgentResetPwd.aspx?AgentCode=" & "{1}" &
                                         "<br /><br />Sincerely,<br />Tokio Marine Life Indonesia", txt_Lname.Text, txt_Code.Text)
            message.IsBodyHtml = True
            smtp.Host = "192.168.1.27"
            'smtp.EnableSsl = True
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network
            smtp.UseDefaultCredentials = False
            'Dim NetworkCred As 
            smtp.Credentials = New NetworkCredential(newUserName, userpass)
            smtp.Port = 25
            smtp.Timeout = 60000
            smtp.Send(message)

            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alert", "alert('Success, please check your email.');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alert", "alert('Failed to send, please try again later.');", True)
        End Try

        pnl_showDetails.Visible = True
    End Sub

    Protected Sub btn_clear_Click(sender As Object, e As EventArgs) Handles btn_clear.Click
        'btn_clear.Visible = False
        txt_search.Text = ""
        GridView1.PageIndex = 0
        Me.LoadAgentProfile(1)
    End Sub

    Private Sub selectedrow()
        Dim Selected As New ArrayList()
        For Each row As GridViewRow In GridView1.Rows
            Dim isSelected As Boolean = DirectCast(row.FindControl("chkRow"), CheckBox).Checked
            If ViewState("SELECTED_ID") IsNot Nothing Then
                Selected = DirectCast(ViewState("SELECTED_ID"), ArrayList)
            End If
            If isSelected Then
                If Not Selected.Contains(row.Cells(1).Text.ToString()) Then
                    Selected.Add(row.Cells(1).Text.ToString())
                End If
            Else
                Selected.Remove(row.Cells(1).Text.ToString())
            End If
        Next
        If Selected IsNot Nothing AndAlso Selected.Count > 0 Then
            ViewState("SELECTED_ID") = Nothing
            ViewState("SELECTED_ID") = Selected
        End If
    End Sub

    Private Sub populatecheckbox()
        Dim objAL As ArrayList = DirectCast(ViewState("SELECTED_ID"), ArrayList)
        If objAL IsNot Nothing AndAlso objAL.Count > 0 Then
            For Each row As GridViewRow In GridView1.Rows
                If objAL.Contains(row.Cells(1).Text.ToString()) Then
                    Dim chkSelect As CheckBox = DirectCast(row.FindControl("chkRow"), CheckBox)
                    chkSelect.Checked = True
                    row.Attributes.Add("class", "selected")
                End If
            Next
        End If
    End Sub

    Protected Sub btn_bulksend_Click(sender As Object, e As EventArgs) Handles btn_bulksend.Click
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        selectedrow()
        Dim Selected As New ArrayList()
        If ViewState("SELECTED_ID") IsNot Nothing Then
            Selected = DirectCast(ViewState("SELECTED_ID"), ArrayList)
            For i As Integer = 0 To Selected.Count - 1
                Dim dimtable As New DataTable
                Dim asql As String = "select AgentName, AgentEmail, AgentPassword, AgentStatus from TMLI_Agent_profile where AgentCode = '" + Selected(i).ToString() + "'"
                objDBCom.ExecuteSQL(dimtable, asql)

                If dimtable.Rows(0)(3).ToString.Equals("Active") Then
                    Try
                        Dim useremail As String = dimtable.Rows(0)(1).ToString
                        Dim agentname As String = dimtable.Rows(0)(0).ToString()
                        Dim agentcode As String = Selected(i).ToString()
                        Dim password As String = ms.DecryptString(dimtable.Rows(0)(2).ToString(), "1234567891123456")
                        Dim adduser As String = System.Configuration.ConfigurationManager.AppSettings("emailAddress").ToString()
                        Dim addpass As String = System.Configuration.ConfigurationManager.AppSettings("emailPassword").ToString()

                        Dim body As String = ""
                        Using sr = New StreamReader(Server.MapPath("~\EmailTemplate.html"))
                            body = sr.ReadToEnd()
                        End Using

                        Dim ipadimage As String = "http://simpleicon.com/wp-content/uploads/ipad-portrait.png"
                        Dim desktopimage As String = "https://image.freepik.com/free-icon/desktop-button_318-25227.jpg"

                        Dim smtp As New SmtpClient()
                        Dim message As New MailMessage(adduser, useremail)

                        message.Subject = "Welcome"
                        message.Body = String.Format(body, agentname, "TMCONNECT", "http://www.google.com", ipadimage, "http://www.google.com",
                            desktopimage, agentcode, password)
                        message.IsBodyHtml = True

                        'message.IsBodyHtml = True
                        smtp.Host = "smtp.gmail.com"
                        smtp.EnableSsl = True
                        Dim NetworkCred As New NetworkCredential(adduser, addpass)
                        smtp.Credentials = NetworkCred
                        smtp.Port = 587
                        smtp.Timeout = 60000
                        smtp.Send(message)

                        ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "alert", "alert('Success!');", True)

                    Catch ex As Exception

                    End Try
                End If
            Next
            objDBCom.Dispose()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Email successfully send.');window.location='AgentProfile.aspx';", True)
        End If
    End Sub

    Protected Sub btn_export_Click(sender As Object, e As EventArgs) Handles btn_export.Click
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim sql As String = "select AgentCode,AgentPassword,AgentName,LGIVNAME,CLTSEX,CLTDOB,SECUITYNO,SALUTL,NATLTY,MARRYD,ADDRTYPE
,AgentAddr1,AgentAddr2,AgentAddr3,CLTADDR04,CLTADDR05,CLTPCODE,CTRYCODE,AgentEmail,AgentContactNumber
,CLTPHONE01,CLTPHONE02,TAXMETH,XREFNO,ZRELIGN,LicenseStartDate,LicenseExpiryDate,Level,SALESUNIT,DirectSupervisorCode
,TLAGLICNO,TLICEXPDT,TLAGLICNO_S,TLICEXPDT_S,BANKKEY,BANKACOUNT,BANKACCDSC,AgentStatus,UPDATEDATE,LastLogonDate
,UDID,DeviceStatus,Admin,AdminPassword from TMLI_Agent_Profile"
        Dim dt As New DataTable
        Dim sb As New StringBuilder()
        objDBCom.ExecuteSQL(dt, sql)
        objDBCom.Dispose()

        For i As Integer = 0 To dt.Columns.Count - 1
            sb.Append(dt.Columns(i))
            If i < dt.Columns.Count - 1 Then
                sb.Append(",")
            End If
        Next
        sb.AppendLine()
        For Each dr As DataRow In dt.Rows
            For i As Integer = 0 To dt.Columns.Count - 1
                'If i = 10 Or i = 15 Or i = 20 Then
                '    sb.Append(String.Format("=""{0}""", dr(i).ToString()))
                'ElseIf i = 7 Or i = 8 Or i = 9 Then
                '    sb.Append(String.Format("""{0}""", dr(i).ToString()))
                'Else
                sb.Append(dr(i).ToString())
                'End If

                If i < dt.Columns.Count - 1 Then
                    sb.Append(",")
                End If
            Next
            sb.AppendLine()
        Next

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Agent_List_" + DateTime.Now + ".csv")
        Response.Charset = ""
        Response.ContentType = "application/text"
        Response.Output.Write(sb)
        Response.Flush()
        Response.End()
    End Sub

    Private Sub PopulatePager(ByVal recordCount As Integer, ByVal currentPage As Integer)
        Dim dblPageCount As Double = CType((CType(recordCount, Decimal) / Decimal.Parse(ddlPaging.SelectedValue)), Double)
        Dim pageCount As Integer = CType(Math.Ceiling(dblPageCount), Integer)
        Dim pages As New List(Of ListItem)
        If (pageCount > 0) Then
            pages.Add(New ListItem("First", "1", (currentPage > 1)))
            Dim i As Integer = 1
            If currentPage = 1 Then
                i = currentPage
            ElseIf currentPage = 2 Then
                i = currentPage
            ElseIf currentPage = 3 Then
                i = currentPage - 1
            ElseIf currentPage = 4 Then
                i = currentPage - 2
            Else
                i = currentPage - 3
            End If
            Dim limit As Integer = i + 10
            Do While (i <= pageCount And i <= limit)
                pages.Add(New ListItem(i.ToString, i.ToString, (i <> currentPage)))
                i = (i + 1)
            Loop
            pages.Add(New ListItem("Last", pageCount.ToString, (currentPage < pageCount)))
        End If
        rptPager.DataSource = pages
        rptPager.DataBind()
    End Sub

    Protected Sub Page_Changed(ByVal sender As Object, ByVal e As EventArgs)
        Dim pageIndex As Integer = Integer.Parse(CType(sender, LinkButton).CommandArgument)
        Me.LoadAgentProfile(pageIndex)
    End Sub

    Protected Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim up As String = "Update TMLI_Agent_Profile set DeviceStatus = '" + ddlDevStatus.SelectedValue + "' where AgentCode = '" + txt_Code.Text + "'"
        If objDBCom.ExecuteSQL(up) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Device Status updated.');window.location = 'AgentProfile.aspx';", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Update failed.');", True)
        End If
    End Sub
End Class