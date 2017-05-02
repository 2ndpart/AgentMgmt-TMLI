Public Class SPAJNumberCreation
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'load data from DB
        Dim dtable As New DataTable
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(CStr(POSWeb.POSWeb_SQLConn))

        objDBCom.ExecuteSQL(dtable, "SELECT * FROM TMLI_SPAJCreationRules")
        GridView1.DataSource = dtable
        GridView1.DataBind()
    End Sub

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click

    End Sub
End Class