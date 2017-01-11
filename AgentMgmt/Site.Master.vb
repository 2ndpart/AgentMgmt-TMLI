Imports System.Web
Imports System.Web.Security

Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate")
        HttpContext.Current.Response.AddHeader("Pragma", "no-cache")
        HttpContext.Current.Response.AddHeader("Expires", "0")

        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim dt As New DataTable

        If Session("Roles") = "User" Then
            Dim sql As String = "SELECT Modules from TMLI_UserLogin where Roles = 'User' and UserName = '" + Session("Username") + "'"
            objDBCom.ExecuteSQL(dt, sql)

            Dim s As String = ""
            Dim arr As String() = Regex.Split(dt.Rows(0)(0).ToString(), ",")
            For Each s In arr
                Dim li As HtmlGenericControl = DirectCast(Page.Master.FindControl("sidebar").FindControl(s), HtmlGenericControl)
                li.Visible = True
            Next
        ElseIf Session("Roles") = "Administrator" Then
            For Each li As Control In sidebar.Controls
                li.Visible = True
            Next
        ElseIf Session("Roles") = "SuperAdministrator" Then
            For Each li As Control In sidebar.Controls
                li.Visible = True
            Next
        End If

        objDBCom.Dispose()
    End Sub


    Protected Sub btn_lgout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_lgout.ServerClick
        Session("Username") = String.Empty
        Session.Clear()
        Session.Abandon()
        Session.RemoveAll()
        Response.Redirect("Login.aspx")
    End Sub
End Class