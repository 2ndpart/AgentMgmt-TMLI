Public Class DeviceManagement
    Inherits System.Web.UI.Page
    Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
    Dim dt As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadGrid()
            chkStatus.Items(0).Selected = True
        End If
    End Sub

    Private Sub LoadGrid()
        Dim sql As String = "select Agent_ID, UDID, Agent_Name, TMLI_Version, Last_UploadDate, Last_DownloadDate, Status, Backup_URL from TMLI_Device_Management"

        Dim condition As String = String.Empty
        For Each item As ListItem In chkStatus.Items
            condition += If(item.Selected, String.Format("'{0}',", item.Value), String.Empty)
        Next

        If Not String.IsNullOrEmpty(condition) Then
            condition = String.Format(" WHERE Status IN ({0})", condition.Substring(0, condition.Length - 1))
        Else
            condition = String.Format(" Order By Agent_Name ASC")
        End If

        objDBCom.ExecuteSQL(dt, sql & condition)
        objDBCom.Dispose()
        BindData()
    End Sub

    Protected Sub ddlPaging_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPaging.SelectedIndexChanged
        GridView1.PageSize = Int32.Parse(ddlPaging.SelectedValue)
        LoadGrid()
    End Sub

    Protected Sub chkStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkStatus.SelectedIndexChanged
        Me.LoadGrid()
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        LoadGrid()
    End Sub

    Protected Sub BindData()
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Protected Sub btn_search_Click(sender As Object, e As EventArgs) Handles btn_search.Click
        Dim sql As String = "select Agent_ID, UDID, Agent_Name, TMLI_Version, Last_UploadDate, Last_DownloadDate, Status, Backup_URL from TMLI_Device_Management"
        sql = sql + " where Agent_ID like ('%" + txt_search.Text + "%') or Agent_Name like ('%" + txt_search.Text + "%') or BLESS_Version like ('%" + txt_search.Text + "%')"

        Dim condition As String = String.Empty
        For Each item As ListItem In chkStatus.Items
            condition += If(item.Selected, String.Format("'{0}',", item.Value), String.Empty)
        Next

        If Not String.IsNullOrEmpty(condition) Then
            condition = String.Format(" and Status IN ({0})", condition.Substring(0, condition.Length - 1))
        Else
            condition = String.Format(" Order By Agent_Name ASC")
        End If

        objDBCom.ExecuteSQL(dt, sql & condition)
        objDBCom.Dispose()
        BindData()
    End Sub

    Protected Sub GridView1_Sorting(sender As Object, e As GridViewSortEventArgs) Handles GridView1.Sorting
        selectedrow()
        Dim sortExpression As String = e.SortExpression
        Dim direction As String = String.Empty
        If SortDirection = SortDirection.Ascending Then
            SortDirection = SortDirection.Descending
            direction = " DESC"
        Else
            SortDirection = SortDirection.Ascending
            direction = " ASC"
        End If
        LoadGrid()
        dt.DefaultView.Sort = sortExpression & direction
        BindData()
        populatecheckbox()
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
End Class