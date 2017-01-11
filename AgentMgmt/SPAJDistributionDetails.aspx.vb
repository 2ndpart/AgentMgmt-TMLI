Public Class SPAJDistributionDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Request("agentCode") <> Nothing Then
                Dim agentCode As String = Request("agentCode")
                Dim objDBCom1 As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
                Dim detailQuery As String = "SELECT	[TBC_SPAJ_NUMBER_AGENT].AgentCode,[Agent_profile].AgentName,Count([TBM_SPAJ_NUMBER].SPAJCode) as 'AllocatedSPAJ'" & _
                                      " FROM [Agent_profile]" & _
                                      " INNER JOIN [TBC_SPAJ_NUMBER_AGENT] ON [Agent_profile].AgentCode = [TBC_SPAJ_NUMBER_AGENT].AgentCode" & _
                                      " INNER JOIN [TBM_SPAJ_NUMBER] ON [TBC_SPAJ_NUMBER_AGENT].PACKCode = [TBM_SPAJ_NUMBER].PACKCode" & _
                                      " WHERE [TBC_SPAJ_NUMBER_AGENT].AgentCode =" & agentCode & _
                                      " GROUP BY [TBC_SPAJ_NUMBER_AGENT].AgentCode,[Agent_profile].AgentName"
                Dim detailDataTable As New DataTable
                objDBCom1.ExecuteSQL(detailDataTable, detailQuery)
                objDBCom1.Dispose()

                txtAgentCode.Text = agentCode
                txtAgentName.Text = detailDataTable.Rows(0)("AgentName")
                txtAllocatedSpaj.Text = detailDataTable.Rows(0)("AllocatedSPAJ")

                Dim objDBCom2 As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
                Dim table As New DataTable
                Dim query As String = "SELECT PACKCode, AssignDate FROM TBC_SPAJ_NUMBER_AGENT WHERE AgentCode=" & agentCode
                objDBCom2.ExecuteSQL(table, query)
                objDBCom2.Dispose()
                Dim finalTable As New DataTable
                finalTable.Columns.Add("PackCode")
                finalTable.Columns.Add("AssignDate")
                finalTable.Columns.Add("Allocation")
                finalTable.Columns.Add("SPAJAllocation")

                For Each dr As DataRow In table.Rows
                    Dim addedRow As DataRow = finalTable.NewRow()
                    addedRow("PackCode") = dr("PackCode")
                    addedRow("AssignDate") = dr("AssignDate")

                    Dim innerObjCommand As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
                    Dim innerDataTable As New DataTable
                    Dim innerQuery As String = "SELECT SpajCode FROM TBM_SPAJ_NUMBER WHERE PACKCode=" & dr("PackCode")
                    innerObjCommand.ExecuteSQL(innerDataTable, innerQuery)

                    addedRow("SPAJAllocation") = innerDataTable(0)("SpajCode").ToString() & " - " & innerDataTable.Rows(innerDataTable.Rows.Count - 1)("SpajCode").ToString()
                    addedRow("Allocation") = 50

                    finalTable.Rows.Add(addedRow)
                Next

                SPAJAllocation.DataSource = finalTable
                SPAJAllocation.DataBind()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub SPAJAllocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SPAJAllocation.SelectedIndexChanged
        Dim packCode As String = SPAJAllocation.SelectedRow.Cells(0).Text

        Dim basicSubmissionQuery As String = "SELECT TBM_SPAJ_NUMBER.SubmittedDate AS 'Date', TBM_SPAJ_NUMBER.SPAJCode AS 'SPAJ #', TBM_SPAJ_NUMBER.ProductName AS 'Product', TBM_SPAJ_NUMBER.PolisOwner AS 'Nama Pemegang Polis'" & _
                                             " FROM TBM_SPAJ_NUMBER INNER JOIN TBC_SPAJ_NUMBER_AGENT" & _
                                             " ON TBM_SPAJ_NUMBER.PACKCode = TBC_SPAJ_NUMBER_AGENT.PACKCode" & _
                                             " WHERE TBM_SPAJ_NUMBER.Status = 'Submitted'"

        If packCode <> Nothing Then
            basicSubmissionQuery = basicSubmissionQuery & "AND TBC_SPAJ_NUMBER_AGENT.PACKCode =" & packCode
        End If

        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim dSource As New DataTable
        objDBCom.ExecuteSQL(dSource, basicSubmissionQuery)

        SPAJSubmission.DataSource = dSource
        SPAJSubmission.DataBind()
    End Sub

    Protected Sub SPAJAllocation_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles SPAJAllocation.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            'e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(SPAJAllocation, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
        End If
    End Sub
End Class