Imports System.IO
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports AjaxControlToolkit

Public Class DropDownTable
    Inherits System.Web.UI.Page
    Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(CStr(POSWeb.POSWeb_SQLConn))
    Dim dt As New DataTable
    Dim dtable As New DataTable
    Dim x As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("UserName") = String.Empty) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please re-login through Login page.');window.location='../TMLI_MPOS/Login.aspx';", True)
        End If
        Panel1.Visible = False
        Panel2.Visible = False
        btn_add.Visible = False
        btn_edit.Visible = False
        btn_save.Visible = False
        btn_clear.Visible = False
        btn_back.Visible = False
        btn_EdBack.Visible = False
        'btn_delete.Visible = False
        If Not Me.IsPostBack Then
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
            Dim dt As New DataTable
            Dim sqls As String = ""
            sqls &= "select name from sys.tables where name like 'TMLI_eProposal%' or name = 'eProposal_AnnualIncome' or name = 'TMLI_Data_Cabang' or name = 'TMLI_Channel' or name = 'TMLI_Insured_Fin_Document' or name = 'TMLI_Payment_Method_Rules'"
            sqls &= " or name like 'TMLI_Score%' ORDER BY name ASC"


            objDBCom.ExecuteSQL(dt, sqls)
            objDBCom.Dispose()

            'dt.Rows(2).Delete()
            'dt.Rows(4).Delete()
            'dt.Rows(8).Delete()
            'dt.Rows(12).Delete()
            'dt.Rows(13).Delete()

            dt.Rows(3).Delete()
            dt.Rows(5).Delete()
            dt.Rows(9).Delete()
            dt.Rows(13).Delete()
            dt.Rows(14).Delete()

            ddlNumberOfRows.DataSource = dt
            ddlNumberOfRows.DataTextField = "name"
            ddlNumberOfRows.DataValueField = "name"
            ddlNumberOfRows.DataBind()
            ddlNumberOfRows.Items.Insert(0, New ListItem("--Select TableName--", "0"))
        End If
    End Sub

    Protected Sub btn_Import_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Import.Click
        Response.Redirect("ImportTable.aspx")
    End Sub

    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub

    Protected Sub btn_dl_Click(sender As Object, e As EventArgs) Handles btn_dl.Click
        Dim sql As String = "Select TOP 5 * from " + ddlNumberOfRows.SelectedItem.Text + ""
        objDBCom.ExecuteSQL(dt, sql)
        objDBCom.Dispose()
        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Data_Cabang") Then
            dt.Columns.Remove("AlamatCabang")
            dt.Columns.Remove("IbuKota")
            dt.Columns.Remove("DATIII")
            dt.Columns.Remove("KodePos")
            dt.Columns.Remove("Propinsi")
            dt.Columns.Remove("SLJJ")
            dt.Columns.Remove("Telepon")
            dt.Columns.Remove("Fax")
            dt.Columns.Remove("StatusRollOut")
            dt.Columns.Remove("TanggalRollOut")
            dt.Columns.Remove("StatusPrioritas")
            dt.Columns.Remove("JumlahCabang")
        ElseIf ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_DataReferral") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_kodepos") Then
            dt.Columns.Remove("ID")
        ElseIf ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Title") Then
            dt.Columns.Remove("id")
        End If

        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Picture_Other_Type") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Picture_Payment_Type") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Age") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Annual_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Gender") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Occupation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Referral") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Source_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Status") Then
        Else
            dt.Columns.Remove("IsNew")
            dt.Columns.Remove("IsUpdate")
            dt.Columns.Remove("RowStatus")
        End If

        Dim csv As String = String.Empty
        For Each col As DataColumn In dt.Columns
            csv += col.ColumnName + ","c

        Next

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" + ddlNumberOfRows.SelectedItem.Text + "_Templates.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"
        Response.Output.Write(csv)
        Response.Flush()
        Response.End()
    End Sub



    Protected Sub BindGridWithStatus(ByVal pageIndex As Integer)
        Panel1.Visible = False
        Panel2.Visible = True
        Panel3.Visible = True
        Dim SearchText As String = ""

        Dim con As String = String.Empty
        For Each item As ListItem In chkStatus.Items
            con += If(item.Selected, String.Format("'{0}',", item.Value), String.Empty)
        Next

        If Not String.IsNullOrEmpty(con) Then
            con = String.Format("{0}", con.Substring(0, con.Length - 1))
        End If

        Dim sqlquery As String = "Select count(*) from " + ddlNumberOfRows.SelectedItem.Text + " where Status IN (" + con + ")"
        Dim sqlquery3 As String = "Select count(*) from " + ddlNumberOfRows.SelectedItem.Text + ""
        Dim sqlquery2 As String = "Select count(*) from " + ddlNumberOfRows.SelectedItem.Text + ""
        Dim temptable As New DataTable
        If ViewState("SearchText") Is Nothing Then
            SearchText = ""
        Else
            SearchText = ViewState("SearchText")
        End If

        Dim condition As String = String.Empty
        For Each item As ListItem In chkStatus.Items
            condition += If(item.Selected, String.Format("'{0}',", item.Value), String.Empty)
        Next

        If Not String.IsNullOrEmpty(condition) Then
            condition = String.Format("{0}", condition.Substring(0, condition.Length - 1))
        End If

        Dim table2 As New DataSet
        objDBCom.AddParameter("@stringToFind", SqlDbType.VarChar, SearchText)
        objDBCom.AddParameter("@schema", SqlDbType.VarChar, "dbo")
        objDBCom.AddParameter("@table", SqlDbType.VarChar, ddlNumberOfRows.SelectedItem.Text)
        objDBCom.AddParameter("@Status", SqlDbType.VarChar, condition)
        objDBCom.AddParameter("@Page", SqlDbType.VarChar, pageIndex)
        objDBCom.AddParameter("@Row", SqlDbType.VarChar, ddlPaging.SelectedItem.Text)
        objDBCom.ExecuteProcedure(table2, "TMLI_FindStringInTable")

        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_kodepos") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Age") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Annual_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Gender") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Occupation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Referral") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Source_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Payment_Method_Rules") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Insured_Fin_Document") Then
            objDBCom.ExecuteSQL(temptable, sqlquery2)
        Else
            If chkStatus.SelectedValue = "" Then
                objDBCom.ExecuteSQL(temptable, sqlquery3)
            Else
                objDBCom.ExecuteSQL(temptable, sqlquery)
            End If

        End If

        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Picture_Other_Type") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Picture_Payment_Type") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Channel") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Data_Cabang") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Nationality") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_OCCP") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Relation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Religion") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Payment_Method") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Bank") Then

        ElseIf ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Age") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Annual_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Gender") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Referral") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Source_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Status") Then

        ElseIf ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Occupation") Then
            table2.Tables(0).Columns.Remove("item_time")
            table2.Tables(0).Columns.Remove("OccpClass")
        ElseIf ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Payment_Method_Rules") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Insured_Fin_Document") Then

        Else
            table2.Tables(0).Columns.Remove("IsNew")
            table2.Tables(0).Columns.Remove("IsUpdate")
            table2.Tables(0).Columns.Remove("RowStatus")
        End If
        GridView1.DataSource = table2.Tables(0)
        ViewState("Table") = table2.Tables(0)
        GridView1.DataBind()
        Dim recordCount As Integer = 0
        If SearchText = "" Then
            recordCount = Convert.ToInt32(temptable.Rows(0)(0).ToString())
        Else
            recordCount = table2.Tables(1).Rows.Count
        End If
        Me.PopulatePager(recordCount, pageIndex)
        objDBCom.Dispose()
    End Sub

    Protected Sub btn_export_Click(sender As Object, e As EventArgs) Handles btn_export.Click
        Dim sql As String = "select * from " + ddlNumberOfRows.SelectedItem.Text + ""
        Dim sb As New StringBuilder()
        objDBCom.ExecuteSQL(dt, sql)
        objDBCom.Dispose()

        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_DataReferral") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_kodepos") Then
            dt.Columns.Remove("ID")
        ElseIf ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Title") Then
            dt.Columns.Remove("id")
        End If

        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Picture_Other_Type") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Picture_Payment_Type") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Channel") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Data_Cabang") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Nationality") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_OCCP") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Relation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Religion") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Payment_Method") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Bank") Then

        ElseIf ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Age") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Annual_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Gender") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Occupation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Referral") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Source_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Status") Then

        Else
            dt.Columns.Remove("IsNew")
            dt.Columns.Remove("IsUpdate")
            dt.Columns.Remove("RowStatus")
        End If

        For i As Integer = 0 To dt.Columns.Count - 1
            sb.Append(dt.Columns(i))
            If i < dt.Columns.Count - 1 Then
                sb.Append(",")
            End If
        Next
        sb.AppendLine()
        For Each dr As DataRow In dt.Rows
            For i As Integer = 0 To dt.Columns.Count - 1
                If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_SourceIncome") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Channel") Then
                    If i = 0 Then
                        sb.Append(String.Format("=""{0}""", dr(i).ToString()))
                    Else
                        sb.Append(dr(i).ToString())
                    End If
                ElseIf ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Data_Cabang") Then
                    If i = 3 Or i = 7 Then
                        sb.Append(String.Format("=""{0}""", dr(i).ToString()))
                    Else
                        sb.Append(dr(i).ToString())
                    End If
                Else
                    sb.Append(dr(i).ToString())
                End If

                If i < dt.Columns.Count - 1 Then
                    sb.Append(",")
                End If
            Next
            sb.AppendLine()
        Next


        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=" + ddlNumberOfRows.SelectedItem.Text + ".csv")
        Response.Charset = ""
        Response.ContentType = "application/text"
        Response.Output.Write(sb)
        Response.Flush()
        Response.End()
    End Sub

    Protected Sub btn_Search_Click(sender As Object, e As EventArgs) Handles btn_Search.Click

        ViewState("SearchText") = txt_sR.Text

        BindGridWithStatus(1)
        Panel2.Visible = True
        btn_cancel.Visible = True
    End Sub

    Protected Sub ddlNumberOfRows_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNumberOfRows.SelectedIndexChanged
        If Not txt_sR.Text.Equals("") Then
            ViewState("SearchText") = ""
            txt_sR.Text = String.Empty
            btn_cancel.Visible = False
            btn_Search.Visible = True
        End If

        ViewState("Table") = ""
        chkStatus.Visible = True
        lbl_state.Visible = False

        If ddlNumberOfRows.SelectedItem.Text.Equals("--Select TableName--") Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please select a table.');", True)
            lbl_search_key.Visible = False
            txt_sR.Visible = False
            btn_addnew.Visible = False
            btn_Search.Visible = False
        Else
            If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_kodepos") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Age") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Annual_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Gender") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Occupation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Referral") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Source_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Status") Then
                BindGridWithStatus(1)
                chkStatus.Visible = False
                lbl_state.Visible = True
            Else
                BindGridWithStatus(1)
            End If

            Panel2.Visible = True
            lbl_search_key.Visible = True
            txt_sR.Visible = True
            btn_Search.Visible = True
        End If

        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Channel") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Data_Cabang") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Nationality") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_OCCP") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Relation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Religion") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Payment_Method") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Bank") Then
            btn_addnew.Visible = False
        Else
            btn_addnew.Visible = True
        End If

    End Sub


    Private Sub GenerateTextbox()
        Dim sql1 As String = "SELECT COLUMN_NAME FROM information_schema.COLUMNS where TABLE_NAME = '" + ddlNumberOfRows.SelectedItem.Text + "' AND COLUMN_NAME != 'IsNew' AND COLUMN_NAME != 'IsUpdate' AND COLUMN_NAME != 'RowStatus' AND COLUMN_NAME != 'id' AND COLUMN_NAME != 'ID' AND COLUMN_NAME != 'AlamatCabang' AND COLUMN_NAME != 'IbuKota' AND COLUMN_NAME != 'DATIII'" +
        "AND COLUMN_NAME != 'KodePos' AND COLUMN_NAME != 'Propinsi' AND COLUMN_NAME != 'SLJJ' " +
        "AND COLUMN_NAME != 'Telepon' AND COLUMN_NAME != 'Fax' AND COLUMN_NAME != 'StatusRollOut'" +
        "AND COLUMN_NAME != 'TanggalRollOut' AND COLUMN_NAME != 'StatusPrioritas' AND COLUMN_NAME != 'JumlahCabang'"
        Dim dt1 As New DataTable

        Dim extra As String = " AND COLUMN_NAME != 'item_time'"
        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Occupation") Then
            sql1 = sql1 + extra
        End If

        objDBCom.ExecuteSQL(dt1, sql1)

        dt.Columns.Add("Label")
        dt.Columns.Add("TextBox")

        Dim count As Integer = dt1.Rows.Count
        For i As Integer = 0 To count - 1
            dt.Rows.Add(Regex.Replace(dt1.Rows(i)(0).ToString(), "((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0"), "")
        Next

        Me.Repeater1.DataSource = dt
        Me.Repeater1.DataBind()

        Dim query As String = "select distinct Wilayah, Kanwil from Data_Cabang"
        Dim Wdt As New DataTable
        objDBCom.ExecuteSQL(Wdt, query)
        objDBCom.Dispose()

        For Each item In Repeater1.Items
            Dim DDL As New DropDownList
            Dim DDK As New DropDownList
            Dim LBL As New Label
            Dim TXT As New TextBox
            Dim DXD As New TextBox
            DDL = DirectCast(item.FindControl("DropDownList1"), DropDownList)
            DDK = DirectCast(item.FindControl("DropDownList2"), DropDownList)
            LBL = DirectCast(item.FindControl("lbl_name"), Label)
            TXT = DirectCast(item.FindControl("txtTextBox1"), TextBox)
            DXD = DirectCast(item.FindControl("txtDate"), TextBox)

            DDK.DataSource = Wdt
            DDK.DataValueField = "Wilayah"
            DDK.DataTextField = "Wilayah"
            DDK.DataBind()

            'e.Item.DataItem.ToString() = "Status"
            If LBL.Text.Equals("Status") Then
                TXT.Visible = False
                DDL.Visible = True
            ElseIf LBL.Text.Equals("item_time") Then
                LBL.Visible = False
                TXT.Visible = False
            ElseIf LBL.Text.Equals("Date Of Birth") Then
                TXT.Visible = False
                DXD.Visible = True
            ElseIf LBL.Text.Equals("Kanwil") Then
                TXT.Attributes.Add("readonly", "readonly")
                TXT.Text = Wdt.Rows(DDK.SelectedIndex)(1)
            ElseIf LBL.Text.Equals("Wilayah") Then
                TXT.Visible = False
                DDK.Visible = True
            End If
        Next

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sql2 As String = "SELECT COLUMN_NAME FROM information_schema.COLUMNS where TABLE_NAME = '" + ddlNumberOfRows.SelectedItem.Text + "' AND COLUMN_NAME != 'IsNew' AND COLUMN_NAME != 'IsUpdate' AND COLUMN_NAME != 'RowStatus' AND COLUMN_NAME != 'AlamatCabang' AND COLUMN_NAME != 'IbuKota' AND COLUMN_NAME != 'DATIII'" +
        "AND COLUMN_NAME != 'KodePos' AND COLUMN_NAME != 'Propinsi' AND COLUMN_NAME != 'SLJJ' " +
        "AND COLUMN_NAME != 'Telepon' AND COLUMN_NAME != 'Fax' AND COLUMN_NAME != 'StatusRollOut' " +
        "AND COLUMN_NAME != 'TanggalRollOut' AND COLUMN_NAME != 'StatusPrioritas' AND COLUMN_NAME != 'JumlahCabang' " +
        "AND COLUMN_NAME != 'OccpClass'"

        Dim extra As String = " AND COLUMN_NAME != 'Status' AND COLUMN_NAME != 'item_time'"
        Dim num As Integer = 0
        Dim dt1 As New DataTable

        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Occupation") Then
            sql2 = sql2 + extra
        End If

        objDBCom.ExecuteSQL(dt1, sql2)
        objDBCom.Dispose()

        dt.Columns.Add("Label")
        dt.Columns.Add("TextBox")

        Dim count As Integer = dt1.Rows.Count
        For i As Integer = 0 To count - 1
            dt.Rows.Add(Regex.Replace(dt1.Rows(i)(0).ToString(), "((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0"), GridView1.SelectedRow.Cells(num).Text)
            num += 1
        Next

        Me.Repeater1.DataSource = dt
        Me.Repeater1.DataBind()

        For Each item In Repeater1.Items
            Dim TXT As New TextBox
            TXT = DirectCast(item.FindControl("txtTextBox1"), TextBox)
            TXT.Attributes.Add("readonly", "readonly")
            If TXT.Text = "&nbsp;" Then
                TXT.Text = String.Empty
            End If
        Next

        Panel1.Visible = True
        Panel2.Visible = False
        Panel3.Visible = False
        hideedit()
        btn_back.Visible = True
        'btn_delete.Visible = True
    End Sub

    Protected Sub GridView1_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."

            If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_DataReferral") Then
                e.Row.Cells(4).Text = Convert.ToDateTime((CType(e.Row.DataItem, DataRowView))("DateOfBirth")).ToString("yyyy-MM-dd")
            End If
        End If
    End Sub

    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        BindGridWithStatus(e.NewPageIndex)

        Panel2.Visible = True
    End Sub


    Protected Sub btn_addnew_Click(sender As Object, e As EventArgs) Handles btn_addnew.Click
        Panel1.Visible = True
        Panel2.Visible = False
        Panel3.Visible = False
        btn_clear.Visible = True
        btn_add.Visible = True
        btn_EdBack.Visible = True
        GenerateTextbox()
        ViewState("Addnew") = "1"
    End Sub

    Protected Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        Panel1.Visible = False
        Panel2.Visible = True
        Panel3.Visible = True
        ViewState.Clear()
    End Sub

    Protected Sub btn_edit_Click(sender As Object, e As EventArgs) Handles btn_edit.Click
        btn_edit.Visible = False
        btn_save.Visible = True
        btn_EdBack.Visible = True
        Panel1.Visible = True
        Panel3.Visible = False
        Dim i As Integer = 0

        Dim query As String = "select distinct Wilayah, Kanwil from TMLI_Data_Cabang"
        Dim Wdt As New DataTable
        objDBCom.ExecuteSQL(Wdt, query)
        objDBCom.Dispose()

        For Each item In Repeater1.Items
            Dim DDL As New DropDownList
            Dim DDK As New DropDownList
            Dim LBL As New Label
            Dim TXT As New TextBox
            Dim DXD As New TextBox
            DDL = DirectCast(item.FindControl("DropDownList1"), DropDownList)
            DDK = DirectCast(item.FindControl("DropDownList2"), DropDownList)
            LBL = DirectCast(item.FindControl("lbl_name"), Label)
            TXT = DirectCast(item.FindControl("txtTextBox1"), TextBox)
            DXD = DirectCast(item.FindControl("txtDate"), TextBox)
            'e.Item.DataItem.ToString() = "Status"

            DDK.DataSource = Wdt
            DDK.DataValueField = "Wilayah"
            DDK.DataTextField = "Wilayah"
            DDK.DataBind()

            If LBL.Text.Equals("Status") Then
                TXT.Visible = False
                DDL.Visible = True
                If TXT.Text = "A" Then
                    DDL.SelectedIndex = 0
                Else
                    DDL.SelectedIndex = 1
                End If
            ElseIf LBL.Text.Equals("Date Of Birth") Then
                DXD.Text = TXT.Text
                TXT.Visible = False
                DXD.Visible = True
            ElseIf LBL.Text.Equals("item_time") Then
                DXD.Text = TXT.Text
                TXT.Visible = False
                DXD.Visible = True
            ElseIf LBL.Text.Equals("Wilayah") Then
                TXT.Visible = False
                DDK.Visible = True
                DDK.SelectedValue = TXT.Text
            End If


            If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Identification") Then
                If LBL.Text.Equals("Data Identifier") Then
                    TXT.Attributes.Add("readonly", "readonly")
                ElseIf LBL.Text.Equals("Identity Desc") Then
                    TXT.Attributes.Remove("readonly")
                End If
            ElseIf ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_DataReferral") Then
                If i = 0 Then
                    TXT.Attributes.Add("readonly", "readonly")
                ElseIf LBL.Text.Equals("NIP") Then
                    TXT.Attributes.Add("readonly", "readonly")
                Else
                    TXT.Attributes.Remove("readonly")
                End If
            Else
                If i > 0 Then
                    TXT.Attributes.Remove("readonly")
                Else
                    TXT.Attributes.Add("readonly", "readonly")
                End If
            End If

            DDL.Attributes.Remove("readonly")
            'DDL.Items.Insert(1, New ListItem("Active", "A"))
            'DDL.Items.Insert(2, New ListItem("Inactive", "I"))
            i += 1
        Next
        ViewState("Edit") = "1"
    End Sub

    Protected Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Dim B As String = ""
        Dim C As String = ""
        Dim Count As Integer = 0
        Dim ScriptComment As String = ""
        Dim ScriptComment2 As String = ""
        Dim aftercomment As String = ""
        Dim aftercomment1 As String = ""
        Dim temphead As String = "Update TMLI_Agent_Profile set "
        Dim tempbody As String = ""
        Dim tempend As String = ""

        For Each item In Repeater1.Items
            Dim DDL As New DropDownList
            Dim DDK As New DropDownList
            Dim LBL As New Label
            Dim TXT As New TextBox
            Dim DXD As New TextBox
            Dim value As String
            Dim name As String
            DDL = DirectCast(item.FindControl("DropDownList1"), DropDownList)
            DDK = DirectCast(item.FindControl("DropDownList2"), DropDownList)
            LBL = DirectCast(item.FindControl("lbl_name"), Label)
            TXT = DirectCast(item.FindControl("txtTextBox1"), TextBox)
            DXD = DirectCast(item.FindControl("txtDate"), TextBox)

            If LBL.Text.Equals("Status") Then
                value = DDL.SelectedValue.ToString()
            ElseIf LBL.Text.Equals("Date Of Birth") Then
                value = DXD.Text.ToString()
            ElseIf LBL.Text.Equals("item_time") Then
                value = DXD.Text.ToString()
            ElseIf LBL.Text.Equals("Wilayah") Then
                value = DDK.SelectedValue.ToString()
            Else
                If String.IsNullOrEmpty(TXT.Text.ToString()) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Field cannot be empty');", True)
                    Panel1.Visible = True
                    btn_save.Visible = True
                    btn_back.Visible = True
                    Exit Sub
                Else
                    value = TXT.Text.ToString()
                End If

            End If

            name = LBL.Text.ToString().Replace(" ", "")

            If Count = 0 Then
                C = " Where " + name + " = '" + value + "'"
                If name.Equals("ChannelCode") Then
                    tempend = C
                End If
            Else
                If String.IsNullOrEmpty(B) Then
                    B = "set " + name + " = '" + value + "' "
                    If name.Equals("ChannelName") Then
                        tempbody = name + " = '" + value + "'"
                    End If
                Else
                    B = B + "," + name + " = '" + value + "' "
                End If
            End If

            Count += 1
        Next
        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Age") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Annual_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Gender") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Occupation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Referral") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Source_Income") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Score_Prospect_Status") Then

        Else
            B = B + ", IsUpdate = getdate(), RowStatus = 'U'"
        End If
        'B = B + ",IsUpdate = getdate(), RowStatus = 'U'"
        Dim A As String = "Update " + ddlNumberOfRows.SelectedItem.Text + " "

        ScriptComment = A + B + C
        ScriptComment2 = A + C
        aftercomment1 = temphead + tempbody + tempend
        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Channel") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Data_Cabang") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Nationality") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_OCCP") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Relation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Religion") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Payment_Method") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Bank") Then
            If objDBCom.ExecuteSQL(ScriptComment) Then
                objDBCom.ExecuteSQL(aftercomment1)
                BindGridWithStatus(1)
                updatemaster()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Update Successfully.');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Update Failed.');", True)
                Panel1.Visible = True
                Panel2.Visible = False
                btn_save.Visible = True
                btn_back.Visible = True
            End If
        Else
            If objDBCom.ExecuteSQL(ScriptComment) Then
                objDBCom.ExecuteSQL(aftercomment1)
                BindGridWithStatus(1)
                updatemaster()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Update Successfully.');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Update Failed.');", True)
                Panel1.Visible = True
                Panel2.Visible = False
                btn_save.Visible = True
                btn_back.Visible = True
            End If
        End If

        objDBCom.Dispose()
        ViewState.Clear()
    End Sub

    Protected Sub btn_add_Click(sender As Object, e As EventArgs) Handles btn_add.Click
        Dim list As New List(Of String)()
        Dim list2 As New List(Of String)()
        Dim value As String
        Dim count As Integer = 0
        Dim i As Integer = 0
        Dim name As String = ""

        For Each item In Repeater1.Items
            Dim DDL As New DropDownList
            Dim DDK As New DropDownList
            Dim LBL As New Label
            Dim TXT As New TextBox
            Dim DXD As New TextBox

            DDL = DirectCast(item.FindControl("DropDownList1"), DropDownList)
            DDK = DirectCast(item.FindControl("DropDownList2"), DropDownList)
            LBL = DirectCast(item.FindControl("lbl_name"), Label)
            TXT = DirectCast(item.FindControl("txtTextBox1"), TextBox)
            DXD = DirectCast(item.FindControl("txtDate"), TextBox)

            If LBL.Text.Equals("Status") Then
                value = DDL.SelectedValue.ToString()
                TXT.Text = value
            ElseIf LBL.Text.Equals("Date Of Birth") Then
                value = DXD.Text.ToString()
                TXT.Text = value
            ElseIf LBL.Text.Equals("item_time") Then
                value = DateTime.Now
                TXT.Text = value
            ElseIf LBL.Text.Equals("Wilayah") Then
                value = DDK.SelectedValue.ToString()
                TXT.Text = value
            Else
                If String.IsNullOrEmpty(TXT.Text.ToString()) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Field cannot be empty');", True)
                    btn_add.Visible = True
                    btn_clear.Visible = True
                    btn_back.Visible = True
                    Panel1.Visible = True
                    Exit Sub
                Else
                    value = TXT.Text.ToString()
                End If
            End If

            name = LBL.Text.ToString().Replace(" ", "")

            If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_DataReferral") Then
                If count = 0 Then
                    Dim sql1 As String = "Select * from " + ddlNumberOfRows.SelectedItem.Text + ""
                    objDBCom.ExecuteSQL(dt, sql1)
                    For Each row In dt.Rows
                        If dt.Rows(i)(1).ToString.Equals(TXT.Text) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Code already existed.');", True)
                            btn_add.Visible = True
                            btn_clear.Visible = True
                            btn_back.Visible = True
                            Panel1.Visible = True
                            Exit Sub
                        End If
                        i += 1
                    Next
                End If
            Else
                If count = 0 Then
                    Dim sql1 As String = "Select * from " + ddlNumberOfRows.SelectedItem.Text + ""
                    objDBCom.ExecuteSQL(dt, sql1)
                    For Each row In dt.Rows
                        If dt.Rows(i)(0).ToString.Equals(TXT.Text) Then
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Code already existed.');", True)
                            btn_add.Visible = True
                            btn_clear.Visible = True
                            btn_back.Visible = True
                            Panel1.Visible = True
                            Exit Sub
                        End If
                        i += 1
                    Next
                End If
            End If

            list.Add(name)
            list2.Add(TXT.Text)
            count += 1
        Next

        Dim result As String = [String].Join(",", list.ToArray())
        Dim result2 As String = [String].Join("','", list2.ToArray())
        result2 = [String].Concat("'", result2, "'")
        Dim sql As String = "Insert Into " + ddlNumberOfRows.SelectedItem.Text + " (" + result + ", IsNew, IsUpdate, RowStatus) values (" + result2 + ", GETDATE(), GETDATE(), 'A')"
        Dim sql2 As String = "Insert Into " + ddlNumberOfRows.SelectedItem.Text + " (" + result + ") values (" + result2 + ")"
        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Channel") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Data_Cabang") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Nationality") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_OCCP") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Relation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Religion") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Payment_Method") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Bank") Then
            If objDBCom.ExecuteSQL(sql2) Then
                BindGridWithStatus(1)
                updatemaster()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Data Successfully added.');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Add Failed.');", True)
                Panel1.Visible = True
                Panel2.Visible = False
                btn_clear.Visible = True
                btn_add.Visible = True
                btn_back.Visible = True
            End If
        Else
            If objDBCom.ExecuteSQL(sql) Then
                BindGridWithStatus(1)
                updatemaster()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Data Successfully added.');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Add Failed.');", True)
                Panel1.Visible = True
                Panel2.Visible = False
                btn_clear.Visible = True
                btn_add.Visible = True
                btn_back.Visible = True
            End If
        End If
        objDBCom.Dispose()
        ViewState.Clear()
    End Sub

    Protected Sub btn_clear_Click(sender As Object, e As EventArgs) Handles btn_clear.Click
        For Each item In Repeater1.Items
            Dim DDL As New DropDownList
            Dim TXT As New TextBox

            DDL = DirectCast(item.FindControl("DropDownList1"), DropDownList)
            TXT = DirectCast(item.FindControl("txtTextBox1"), TextBox)

            TXT.Text = ""
            DDL.SelectedIndex = 0
        Next

        btn_add.Visible = True
        btn_clear.Visible = True
        btn_back.Visible = True
        Panel1.Visible = True
        Panel3.Visible = False
    End Sub


    Private Sub updatemaster()
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(CStr(POSWeb.POSWeb_SQLConn))
        Dim Z As String = "UPDATE TMLI_Master_Info set VersionNo = VersionNo + 1, UpdatedDate = GETDATE() where TableName = '" + ddlNumberOfRows.SelectedItem.Text + "'"
        Dim Y As String = "UPDATE TMLI_Master_Info set VersionNo = VersionNo + 1, UpdatedDate = GETDATE() where TableName = 'TMLI_Master_Info'"
        objDBCom.ExecuteSQL(Z)
        objDBCom.ExecuteSQL(Y)
    End Sub


    Protected Sub SortRecords(ByVal sender As Object, ByVal e As GridViewSortEventArgs) Handles GridView1.Sorting
        Dim sortExpression As String = e.SortExpression
        Dim direction As String = String.Empty
        If SortDirection = SortDirection.Ascending Then
            SortDirection = SortDirection.Descending
            direction = " DESC"
        Else
            SortDirection = SortDirection.Ascending
            direction = " ASC"
        End If
        If Not ViewState("Table") Is Nothing Then
            ViewState("Table").DefaultView.Sort = sortExpression & direction
            GridView1.DataSource = ViewState("Table")
            GridView1.DataBind()
            Panel2.Visible = True
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

    Protected Sub Status_select(sender As Object, e As EventArgs) Handles chkStatus.SelectedIndexChanged
        If Not ViewState("SearchText") Is Nothing Then
            btn_cancel.Visible = True
        End If
        Me.BindGridWithStatus(1)
    End Sub

    Public Sub DDK_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim Edt As New DataTable

        Dim DDK As New DropDownList
        DDK = DirectCast(sender, DropDownList)
        Dim query2 As String = "Select distinct Kanwil from TMLI_Data_Cabang where Wilayah = '" + DDK.SelectedValue.ToString() + "'"
        objDBCom.ExecuteSQL(Edt, query2)

        For Each item In Repeater1.Items
            Dim DDL As New DropDownList
            Dim LBL As New Label
            Dim TXT As New TextBox
            DDL = DirectCast(item.FindControl("DropDownList1"), DropDownList)
            LBL = DirectCast(item.FindControl("lbl_name"), Label)
            TXT = DirectCast(item.FindControl("txtTextBox1"), TextBox)

            If LBL.Text.Equals("Kanwil") Then
                TXT.Text = Edt.Rows(0)(0).ToString()
            End If
        Next

        objDBCom.Dispose()
        Panel1.Visible = True
        If Not ViewState("Addnew") = Nothing Then
            btn_add.Visible = True
            btn_back.Visible = True
            btn_clear.Visible = True
        ElseIf Not ViewState("Edit") = Nothing Then
            btn_save.Visible = True
            btn_back.Visible = True
        End If

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
        Me.BindGridWithStatus(pageIndex)
    End Sub

    Protected Sub PageSize_Changed(sender As Object, e As EventArgs) Handles ddlPaging.SelectedIndexChanged
        Me.BindGridWithStatus(1)
    End Sub

    Protected Sub btn_cancel_Click(sender As Object, e As EventArgs) Handles btn_cancel.Click
        txt_sR.Text = ""
        ViewState("SearchText") = ""
        Me.BindGridWithStatus(1)
        Panel2.Visible = True
        btn_cancel.Visible = False
    End Sub

    Protected Sub btn_EdBack_Click(sender As Object, e As EventArgs) Handles btn_EdBack.Click
        Panel1.Visible = False
        Panel2.Visible = True
        Panel3.Visible = True
        ViewState.Clear()
    End Sub

    Private Sub hideedit()
        If ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Channel") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Data_Cabang") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Marital_Status") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Nationality") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_OCCP") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Relation") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_eProposal_Religion") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Payment_Method") Or ddlNumberOfRows.SelectedItem.Text.Equals("TMLI_Bank") Then
            btn_edit.Visible = False
        Else
            btn_edit.Visible = True
        End If
    End Sub
End Class