Imports System.Net
Imports System.Net.Mail
Imports System.IO
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit
Imports System.Text.RegularExpressions

Public Class AddUser
    Inherits System.Web.UI.Page
    Dim ms As New Encryption


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            If (Session("UserName") = String.Empty) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please re-login through Login page.');window.location='../TMLI_MPOS/Login.aspx';", True)
            ElseIf (Session("Roles") = "User") Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('You do not have the access right for this page');window.location='../TMLI_MPOS/AgentProfile.aspx';", True)
            End If
        End If
        Panel1.Visible = False
        txt_password.Attributes("type") = "password"
        txt_cpassword.Attributes("type") = "password"
        loadUser()
        disableadd()
        hidebtn()
    End Sub

    Private Sub loadUser()
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim dt As New DataTable
        Dim sql As String = "SELECT * FROM TMLI_UserLogin where Roles != 'SuperAdministrator'"
        Dim sql2 As String = "SELECT * FROM TMLI_UserLogin where Roles = 'User'"

        If Session("Roles").Equals("Administrator") Then
            objDBCom.ExecuteSQL(dt, sql2)
        ElseIf Session("Roles").Equals("SuperAdministrator") Then
            objDBCom.ExecuteSQL(dt, sql)
        End If

        objDBCom.Dispose()

        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Private Sub hidebtn()
        If Not Session("Roles") = String.Empty Then
            If Session("Roles").Equals("User") Then
                btn_addnew.Visible = False
            Else
                btn_addnew.Visible = True
            End If
        End If
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim row As GridViewRow = GridView1.SelectedRow
        Dim dt As New DataTable
        Dim sql1 As String = "select * from TMLI_UserLogin where UserName = '" + row.Cells(2).Text + "'"
        objDBCom.ExecuteSQL(dt, sql1)
        objDBCom.Dispose()
        txt_username.Text = dt.Rows(0)(1).ToString
        txt_password.Text = ms.DecryptString(dt.Rows(0)(2).ToString, "1234567891123456")
        txt_email.Text = dt.Rows(0)(3).ToString
        txt_name.Text = dt.Rows(0)(4).ToString
        txt_Department.Text = dt.Rows(0)(6).ToString

        Dim s As String = ""
        Dim arr As String() = Regex.Split(dt.Rows(0)(7).ToString(), ",")
        For Each s In arr
            For Each item As ListItem In ModulList.Items
                If item.Value = s Then
                    item.Selected = True
                End If
            Next
        Next

        If dt.Rows(0)(5).ToString().Equals("Administrator") Then
            lbl_Modules.Visible = False
            ModulList.Visible = False
        Else
            lbl_Modules.Visible = True
            ModulList.Visible = True
        End If

        Panel1.Visible = True
        Panel2.Visible = False

        If Session("Roles").Equals("User") Then
            btn_del.Visible = False
        ElseIf Session("Roles").Equals("Administrator") Then
            btn_del.Visible = True
        ElseIf Session("Roles").Equals("SuperAdministrator") Then
            btn_del.Visible = True
            lbl_roles.Visible = False
            ddl_roles.Visible = False
        Else
            btn_del.Visible = True
        End If

        btn_nback.Visible = True
        btn_back.Visible = False
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btn_addnew_Click(sender As Object, e As EventArgs) Handles btn_addnew.Click
        Panel1.Visible = True
        Panel2.Visible = False
        enableadd()
        lbl_Modules.Visible = True
        ModulList.Visible = True

        If Session("Roles").Equals("SuperAdministrator") Then
            lbl_roles.Visible = True
            ddl_roles.Visible = True
        Else
            lbl_roles.Visible = False
            ddl_roles.Visible = False
        End If

        btn_nback.Visible = False
        btn_back.Visible = True
    End Sub

    Protected Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        Panel1.Visible = False
        Panel2.Visible = True
        cleartext()
        disableadd()
    End Sub

    Private Sub enableadd()
        'TextBox1.Enabled = True
        'TextBox2.Enabled = True
        'TextBox3.Enabled = True
        txt_username.Attributes.Remove("readonly")
        txt_password.Attributes.Remove("readonly")
        txt_cpassword.Attributes.Remove("readonly")
        txt_cpassword.Visible = True
        txt_email.Visible = True
        'TextBox4.Enabled = True
        txt_email.Attributes.Remove("readonly")
        txt_name.Attributes.Remove("readonly")
        txt_Department.Attributes.Remove("readonly")
        'DropDownList1.Enabled = True
        'ddlroles.Attributes.Remove("readonly")
        Label1.Visible = True
        btn_add.Visible = True
        btn_del.Visible = False
    End Sub

    Private Sub disableadd()
        txt_username.Attributes.Add("readonly", "readonly")
        txt_password.Attributes.Add("readonly", "readonly")
        txt_cpassword.Attributes.Add("readonly", "readonly")
        txt_email.Attributes.Add("readonly", "readonly")
        txt_name.Attributes.Add("readonly", "readonly")
        txt_Department.Attributes.Add("readonly", "readonly")
        Label1.Visible = False
        btn_add.Visible = False
        btn_del.Visible = False
        txt_cpassword.Visible = False
    End Sub

    Private Sub cleartext()
        txt_username.Text = ""
        txt_password.Text = ""
        txt_cpassword.Text = ""
        txt_email.Text = ""
        txt_name.Text = ""
        txt_Department.Text = ""
        For Each item As ListItem In ModulList.Items
            item.Selected = False
        Next
    End Sub

    Protected Sub btn_del_Click(sender As Object, e As EventArgs) Handles btn_del.Click
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim sql2 As String = "delete from TMLI_UserLogin where UserName = '" + txt_username.Text + "'"
        If objDBCom.ExecuteSQL(sql2) Then
            objDBCom.Dispose()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('User successfully deleted'); window.location='AddUser.aspx';", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('User failed to delete');", True)
        End If
    End Sub

    Protected Sub btn_add_Click(sender As Object, e As EventArgs) Handles btn_add.Click
        Dim condition As String = String.Empty

        For Each item As ListItem In ModulList.Items
            condition += If(item.Selected, String.Format("{0},", item.Value), String.Empty)
        Next

        If Not String.IsNullOrEmpty(condition) Then
            condition = String.Format("{0}", condition.Substring(0, condition.Length - 1))
        End If

        Dim roles As String = ""
        If Session("Roles").Equals("SuperAdministrator") Then
            roles = ddl_roles.SelectedItem.Text
        Else
            roles = "User"
        End If

        If Not Is_EntryValid() Then
            Panel1.Visible = True
            enableadd()
            'cleartext()
            Exit Sub
        Else

        End If

        If Is_EntryDuplicate() Then
            Panel1.Visible = True
            enableadd()
            'cleartext()

            Exit Sub
        Else
            Dim isSuccess As Boolean = False
            Dim dt As New DataTable
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
            objDBCom.AddParameter("@username", SqlDbType.VarChar, txt_username.Text)
            objDBCom.AddParameter("@Userpassword", SqlDbType.VarChar, ms.EncryptString(txt_password.Text, "1234567891123456"))
            objDBCom.AddParameter("@email", SqlDbType.VarChar, txt_email.Text)
            objDBCom.AddParameter("@name", SqlDbType.VarChar, txt_name.Text)
            objDBCom.AddParameter("@roles", SqlDbType.VarChar, roles)
            objDBCom.AddParameter("@department", SqlDbType.VarChar, txt_Department.Text)
            objDBCom.AddParameter("@Modules", SqlDbType.VarChar, condition)
            If objDBCom.ExecuteProcedure(dt, "TMLI_Admin_Profile_User_Add") Then
                isSuccess = True
                'GetCheckedValue()
            End If

            objDBCom.Dispose()

            If isSuccess Then
                Try
                    Dim mposemail As String = System.Configuration.ConfigurationManager.AppSettings("emailAddress").ToString()
                    Dim mpospass As String = System.Configuration.ConfigurationManager.AppSettings("emailPassword").ToString()
                    Dim username As String = txt_username.Text
                    Dim password As String = txt_password.Text
                    Dim email As String = txt_email.Text
                    Dim smtp As New SmtpClient()
                    Dim message As New MailMessage(mposemail, email)
                    message.Subject = "BLESS MPOS Server"

                    message.Body = String.Format("Good day.<br /><br />Your account have been created to use TMLI TMConnect" &
                                                "<br /><br />Username : {0}<br />Password : {1}<br /><br />Please click the link below to access the login page for TMLI TMConnect." &
                                               "<br /><br />http://mposws.azurewebsites.net/mpostmli/Login.aspx<br /><br />Sincerely,<br />Tokio Marine Life Indonesia", username, password)
                    message.IsBodyHtml = True
                    smtp.Host = "smtp.gmail.com"
                    smtp.EnableSsl = True
                    Dim NetworkCred As New NetworkCredential(mposemail, mpospass)
                    smtp.Credentials = NetworkCred
                    smtp.Port = 587
                    smtp.Timeout = 60000
                    smtp.Send(message)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('User successfully registered');window.location='AddUser.aspx';", True)
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Email failed to send out, please check if email is valid.');window.location='AddUser.aspx';", True)
                End Try
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Failed to register user.');", True)
            End If
        End If
        cleartext()
        lbl_Modules.Visible = False
        ModulList.Visible = False
    End Sub

    Private Function Is_EntryValid() As Boolean
        If txt_username.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Username cannot be empty\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If txt_password.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password cannot be empty\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If txt_cpassword.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password Confirmation cannot be empty\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If txt_email.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Email cannot be empty\nPlease insert a value.');", True)
            Return False
            Exit Function
        End If

        If Not txt_password.Text = txt_cpassword.Text Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Password not match!\nPlease insert same value as password field.');", True)
            Return False
            Exit Function
        End If

        Return True
    End Function

    Private Function Is_EntryDuplicate() As Boolean
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim dt As New DataTable
        Dim sql3 As String = "SELECT * from TMLI_UserLogin where UserName = '" + txt_name.Text + "' or UserEmail = '" + txt_email.Text + "'"
        Dim isFound As Boolean = False

        objDBCom.ExecuteSQL(dt, sql3)
        objDBCom.Dispose()
        If dt.Rows.Count > 0 Then
            isFound = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Username and/or Email already exist.\nPlease use another username or email.');window.location='AddUser.aspx';", True)
        End If

        dt.Dispose()

        Return isFound
    End Function

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        loadUser()
    End Sub

    Protected Sub BindGrid2()
        Dim table2 As New DataTable
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim sql4 As String = "SELECT * from TMLI_UserLogin where Roles = 'User'"
        objDBCom.ExecuteSQL(table2, sql4)
        objDBCom.Dispose()

        GridView1.DataSource = table2
        GridView1.DataBind()
    End Sub

    Protected Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        BindGrid()
        btn_Cancel.Visible = True
    End Sub

    Protected Sub BindGrid()
        Dim table2 As New DataTable
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

        objDBCom.AddParameter("@stringToFind", SqlDbType.VarChar, txt_search.Text)
        objDBCom.AddParameter("@schema", SqlDbType.VarChar, "dbo")
        objDBCom.AddParameter("@table", SqlDbType.VarChar, "TMLI_UserLogin")
        objDBCom.AddParameter("@Status", SqlDbType.VarChar, "")
        objDBCom.AddParameter("@Page", SqlDbType.VarChar, "1")
        objDBCom.AddParameter("@Row", SqlDbType.VarChar, ddlPaging.SelectedItem.Text)
        objDBCom.ExecuteProcedure(table2, "TMLI_FindStringInTable")
        objDBCom.Dispose()

        For Each dr As DataRow In table2.Rows
            If dr("Roles").Equals("SuperAdministrator") Then
                dr.Delete()
            End If
        Next

        GridView1.DataSource = table2
        GridView1.DataBind()
    End Sub

    Protected Sub ddlPaging_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlPaging.SelectedIndexChanged
        GridView1.PageSize = Int32.Parse(ddlPaging.SelectedValue.ToString())
        BindGrid2()
    End Sub

    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        BindGrid2()
    End Sub

    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim sortExpression As String = e.SortExpression
        Dim direction As String = String.Empty
        If SortDirection = SortDirection.Ascending Then
            SortDirection = SortDirection.Descending
            direction = " DESC"
        Else
            SortDirection = SortDirection.Ascending
            direction = " ASC"
        End If
        Dim table As DataTable = Me.GetData()
        If Not table Is Nothing Then
            table.DefaultView.Sort = sortExpression & direction
            GridView1.DataSource = table
            GridView1.DataBind()
        End If
    End Sub

    Public Property SortDirection() As SortDirection
        Get
            If ViewState("SortDirection") Is Nothing Then
                ViewState("SortDirection") = SortDirection.Ascending
            End If
            Return DirectCast(ViewState("SortDirection"), SortDirection)
        End Get
        Set(ByVal value As SortDirection)
            ViewState("SortDirection") = value
        End Set
    End Property

    Private Function GetData() As DataTable
        Dim table2 As New DataTable
        Dim sql4 As String = "SELECT * from TMLI_UserLogin where Roles = 'User'"
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        objDBCom.ExecuteSQL(table2, sql4)
        objDBCom.Dispose()
        Return table2
    End Function

    Protected Sub btn_nback_Click(sender As Object, e As EventArgs) Handles btn_nback.Click
        Panel1.Visible = False
        Panel2.Visible = True
        cleartext()
        disableadd()
    End Sub

    Protected Sub btn_Cancel_Click(sender As Object, e As EventArgs) Handles btn_Cancel.Click
        txt_search.Text = ""
        BindGrid()
        btn_Cancel.Visible = False
    End Sub

    Protected Sub ddl_roles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddl_roles.SelectedIndexChanged
        If ddl_roles.SelectedItem.Text.Equals("Administrator") Then
            lbl_Modules.Visible = False
            ModulList.Visible = False
        Else
            lbl_Modules.Visible = True
            ModulList.Visible = True
        End If
        Panel1.Visible = True
        Panel2.Visible = False
        enableadd()
    End Sub

    'Private Sub GetCheckedValue()
    '    Dim con As String = ""
    '    Dim non As String = ""
    '    For Each item As ListItem In CheckBoxList1.Items
    '        con += If(item.Selected, String.Format("{0},", item.Value), String.Empty)
    '        non += If(item.Selected = False, String.Format("{0},", item.Value), String.Empty)
    '    Next

    '    If Not String.IsNullOrEmpty(con) Then
    '        con = String.Format("{0}", con.Substring(0, con.Length - 1))
    '    End If

    '    If Not String.IsNullOrEmpty(non) Then
    '        non = String.Format("{0}", non.Substring(0, non.Length - 1))
    '    End If
    '    'Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
    '    'Dim insert As String = "insert into TMLI_UserAccess values ()"

    'End Sub
End Class