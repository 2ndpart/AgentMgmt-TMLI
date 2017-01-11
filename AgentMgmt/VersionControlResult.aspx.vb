Public Class VersionControlResult
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim x As String = Request.QueryString("VersionNo")
        VerNo.Text = x

        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim q1 As String = "Select Version_Desc from TMLI_Version_checker where VersionNo = '" + x.ToString() + "'"
        Dim dt As New DataTable
        objDBCom.ExecuteSQL(dt, q1)
        objDBCom.Dispose()

        myText.Text = dt.Rows(0)(0).ToString()
    End Sub

End Class