Public Class SPAJSubmission
    Inherits System.Web.UI.Page

    Dim whereClause As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim basicQuery As String = "SELECT SPAJCode, SubmittedDate, ProductName, PolisOwner FROM TMLI_TBT_SPAJ_ESUBMISSION ORDER BY SubmittedDate"
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim table As New DataTable
        objDBCom.ExecuteSQL(table, basicQuery)

        GridViewSubmission.DataSource = table
        GridViewSubmission.DataBind()
    End Sub

    Protected Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        Dim basicQuery As String = "SELECT TBM_SPAJ_NUMBER.SPAJCode,TBM_SPAJ_NUMBER.SubmittedDate, TBM_SPAJ_NUMBER.ProductName,TBC_SPAJ_NUMBER_AGENT.AgentCode, Agent_profile.AgentName from TBM_SPAJ_NUMBER " & _
                               " INNER JOIN TBC_SPAJ_NUMBER_AGENT on TBM_SPAJ_NUMBER.PACKCode = TBC_SPAJ_NUMBER_AGENT.PACKCode" & _
                               " INNER JOIN Agent_profile ON TBC_SPAJ_NUMBER_AGENT.AgentCode = Agent_profile.AgentCode" & _
                               " WHERE TBM_SPAJ_NUMBER.Status = 'Submitted' "
        Dim orderByClause As String = " ORDER BY TBM_SPAJ_NUMBER.SubmittedDate DESC"
        Dim filter As String = ddlSearch.SelectedValue
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        If ddlSearch.SelectedValue = "AgentName" Then
            whereClause = " AND " & ddlSearch.SelectedValue & " = '" & txt_search.Text & "'"
        Else
            whereClause = " AND " & ddlSearch.SelectedValue & " = " & txt_search.Text
        End If

        Dim advanceQuery As String = basicQuery & whereClause & orderByClause

        Dim table As New DataTable
        objDBCom.ExecuteSQL(table, advanceQuery)

        GridViewSubmission.DataSource = table
        GridViewSubmission.DataBind()
    End Sub
End Class