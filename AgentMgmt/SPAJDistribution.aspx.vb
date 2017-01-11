Imports System.Threading
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Security.Cryptography
Public Class SPAJDistribution
    Inherits System.Web.UI.Page

    Dim basicQuery As String = "SELECT DISTINCT [TBC_SPAJ_NUMBER_AGENT].AssignDate, [TBM_SPAJ_NUMBER].PackCode, [TBC_SPAJ_NUMBER_AGENT].AgentCode, [Agent_profile].AgentName FROM [TBM_SPAJ_NUMBER]" & _
                                                 " INNER JOIN [TBC_SPAJ_NUMBER_AGENT] ON [TBM_SPAJ_NUMBER].PACKCode = [TBC_SPAJ_NUMBER_AGENT].PACKCode" & _
                                                 " INNER JOIN [Agent_profile] ON [TBC_SPAJ_NUMBER_AGENT].[AgentCode] = [Agent_profile].AgentCode"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)

        
        Dim table As New DataTable
        objDBCom.ExecuteSQL(table, basicQuery)

        spajGrid.DataSource = table
        spajGrid.DataBind()
        objDBCom.Dispose()

    End Sub

    Protected Sub spajGrid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles spajGrid.SelectedIndexChanged
        Dim selectedRow As GridViewRow = spajGrid.SelectedRow

        Dim selectedAgentCode As String = selectedRow.Cells(2).Text
        Dim redirectLink As String = "~/SPAJDistributionDetails.aspx?agentCode=" & selectedAgentCode
        Response.Redirect(redirectLink)

    End Sub

    Protected Sub spajGrid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles spajGrid.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(spajGrid, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim startDate As String = Nothing
        Dim endDate As String = Nothing
        If txtEndDate.Text <> Nothing And txtStartDate.Text <> Nothing Then
            startDate = txtStartDate.Text
            endDate = txtEndDate.Text
            basicQuery = basicQuery & " WHERE [TBC_SPAJ_NUMBER_AGENT].AssignDate BETWEEN '" & startDate & "' AND '" & endDate & "'"
        End If

        If txtFilter.Text <> Nothing Then
            If txtStartDate.Text <> Nothing And txtEndDate.Text <> Nothing Then
                startDate = txtStartDate.Text
                endDate = txtEndDate.Text
                basicQuery = basicQuery & " WHERE [TBM_SPAJ_NUMBER].PackCode = " & txtFilter.Text & " OR [TBC_SPAJ_NUMBER_AGENT].AgentCode = " & txtFilter.Text & _
                        " AND [TBC_SPAJ_NUMBER_AGENT].AssignDate BETWEEN '" & startDate & "' AND '" & endDate & "'"

            Else
                basicQuery = basicQuery & " WHERE [TBM_SPAJ_NUMBER].PackCode = " & txtFilter.Text & " OR [TBC_SPAJ_NUMBER_AGENT].AgentCode = " & txtFilter.Text
            End If
        End If
        

        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim table As New DataTable
        objDBCom.ExecuteSQL(table, basicQuery)

        spajGrid.DataSource = table
        spajGrid.DataBind()
        objDBCom.Dispose()
    End Sub
End Class