Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.FileIO
Imports System.Threading.Tasks

Public Class ImportTable
    Inherits System.Web.UI.Page
    Dim overall As Integer = 0
    Dim insertLog As New Dictionary(Of String, Integer)()
    'Dim connection As New SqlConnection("SERVER=.;DATABASE=MobilePOS;UID=sa;PWD=sasa")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Session("UserName") = String.Empty) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Please re-login through Login page.');window.location='../TMLI_MPOS/Login.aspx';", True)
        End If
        If Not Me.IsPostBack Then
            Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
            Dim dt As New DataTable
            Dim sqls As String = ""
            sqls &= "select name from sys.tables where name like 'TMLI_eProposal%' or name = 'TMLI_kodepos'"
            sqls &= " or name like 'TMLI_Picture%' or name like 'TMLI_Score%' ORDER BY name ASC"

            objDBCom.ExecuteSQL(dt, sqls)
            objDBCom.Dispose()

            For Each row As DataRow In dt.Rows
                If row("name").ToString().Equals("TMLI_eProposal_Marital_Status") Or row("name").ToString().Equals("TMLI_eProposal_Nationality") Or row("name").ToString().Equals("TMLI_eProposal_OCCP") Or row("name").ToString().Equals("TMLI_eProposal_Relation") Or row("name").ToString().Equals("TMLI_eProposal_Religion") Then
                    row.Delete()
                End If
            Next

            ddlTable.DataSource = dt
            ddlTable.DataTextField = "name"
            ddlTable.DataValueField = "name"
            ddlTable.DataBind()
            ddlTable.Items.Insert(0, New ListItem("--Select TableName--", "0"))
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim isSuccess As Boolean = False
        Dim csvPath As String = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName)
        FileUpload1.SaveAs(csvPath)
        Dim dt As New DataTable()

        If File.Exists(csvPath) Then
            Using parser As New TextFieldParser(csvPath)
                ' set the parser variables
                parser.TextFieldType = FieldType.Delimited
                parser.SetDelimiters(",")
                Dim firstLine As Boolean = True
                While Not parser.EndOfData
                    'Processing row
                    Dim fields As String() = parser.ReadFields()
                    ' get the column headers
                    If firstLine Then
                        For Each val As String In fields
                            RTrim(val)
                            dt.Columns.Add(val)
                        Next
                        firstLine = False
                        Continue While
                    End If
                    ' get the row data
                    dt.Rows.Add(fields)

                    If ddlTable.SelectedItem.Text = "TMLI_Data_Cabang" Then
                        Dim i As Integer = 0
                        For Each cell As String In fields
                            dt.Rows(dt.Rows.Count - 1)(i) = cell
                            If i = 19 Then
                                dt.Rows(dt.Rows.Count - 1)(i) = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))
                            End If
                            i += 1
                        Next
                    End If

                End While
            End Using
            If ddlTable.SelectedItem.Text = "DataReferral" Or ddlTable.SelectedItem.Text = "TMLI_kodepos" Then
                Dim extra As DataColumn = dt.Columns.Add("ID", System.Type.[GetType]("System.Boolean"))
                extra.SetOrdinal(0)
                'dt.Columns.Add("Details")
                'dt.Columns.Add("Status")
                'Dim c As Integer = 0
                'For Each r As DataRow In dt.Rows
                '    dt.Rows(c)(6) = "A"
                '    c += 1
                'Next
            ElseIf ddlTable.SelectedItem.Text = "TMLI_eProposal_Title" Then
                Dim extra As DataColumn = dt.Columns.Add("id", System.Type.[GetType]("System.Boolean"))
                extra.SetOrdinal(0)
            End If
        End If

        'Dim SQL As String = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + ddlTable.SelectedItem.Text + "' and COLUMN_NAME != 'IsNew' and COLUMN_NAME != 'IsUpdate' and COLUMN_NAME != 'RowStatus'"
        Dim dtcloned As New DataTable
        dtcloned = dt.Clone()
        For Each col As DataColumn In dt.Columns
            'If col.ColumnName.Equals("TanggalRollOut") Then
            '    dtcloned.Columns(19).DataType = GetType(DateTime)
            If col.ColumnName.Equals("DateOfBirth") Then
                dtcloned.Columns(4).DataType = GetType(DateTime)
                'ElseIf col.ColumnName.Equals("OccpClass") Then
                '    dtcloned.Columns(2).DataType = GetType(Integer)
            ElseIf col.ColumnName.Equals("kodeposID") Then
                dtcloned.Columns(1).DataType = GetType(Integer)
            End If
        Next
        For Each rt As DataRow In dt.Rows
            If IsDBNull(rt("DateOfBirth")) Or IsNothing(rt("DateOfBirth")) Or rt("DateOfBirth") = "" Then
                rt("DateOfBirth") = "1/1/1900"
            End If
            dtcloned.ImportRow(rt)
        Next
        isSuccess = True

        If isSuccess = True Then
            Dim query As String = "select * from " + ddlTable.SelectedItem.Text + ""
            Dim dt1 As New DataTable
            Dim dt2 As New DataTable
            Dim x As Integer = 0
            objDBCom.ExecuteSQL(dt1, query)
            overall = dt1.Rows.Count
            If ddlTable.SelectedItem.Text = "TMLI_Data_Cabang" Then
                For count As Integer = 0 To dt1.Rows.Count - 1
                    Dim found As Boolean = True
                    Dim rowCount1 As Integer = dtcloned.Rows.Count - 1
                    For count1 As Integer = 0 To rowCount1
                        If count1 <= rowCount1 Then
                            If dt1.Rows(count)(7).ToString() = dtcloned.Rows(count1)(7).ToString() Then
                                'dtclone.Rows(count1).Delete()
                                x = count1
                                found = False
                                If found = False Then
                                    dtcloned.Rows(x).Delete()
                                    rowCount1 -= 1
                                End If
                            End If
                        End If
                    Next
                Next
            ElseIf ddlTable.SelectedItem.Text = "TMLI_kodepos" Or ddlTable.SelectedItem.Text = "TMLI_eProposal_Title" Then
                For count As Integer = 0 To dt1.Rows.Count - 1
                    Dim found As Boolean = True
                    Dim rowCount As Integer = dtcloned.Rows.Count - 1
                    For count1 As Integer = 0 To rowCount
                        If count1 <= rowCount Then
                            If dt1.Rows(count)(1).ToString() = dtcloned.Rows(count1)(1).ToString() Then
                                'dtclone.Rows(count1).Delete()
                                x = count1
                                found = False
                                If found = False Then
                                    dtcloned.Rows(x).Delete()
                                    rowCount -= 1
                                End If
                            End If
                        End If
                    Next
                Next
            ElseIf ddlTable.SelectedItem.Text = "TMLI_DataReferral" Then
                Try
                    dtcloned.Columns.Remove("ID")
                    'Dim po As New ParallelOptions()
                    'po.MaxDegreeOfParallelism = 5
                    'Parallel.ForEach(dtcloned.AsEnumerable, Sub(dr As DataRow)
                    '                                            InsertDataIntoDB(dr)
                    '                                        End Sub)
                    For Each dRow As DataRow In dtcloned.Rows
                        InsertDataIntoDB(dRow)
                    Next
                Catch ex As Exception

                End Try

            Else
                For count As Integer = 0 To dt1.Rows.Count - 1
                    Dim found As Boolean = True
                    Dim rowCount As Integer = dtcloned.Rows.Count - 1
                    For count1 As Integer = 0 To rowCount
                        If count1 <= rowCount Then
                            If dt1.Rows(count)(0).ToString() = dtcloned.Rows(count1)(0).ToString() Then
                                'dtclone.Rows(count1).Delete()
                                x = count1
                                found = False
                                If found = False Then
                                    dtcloned.Rows(x).Delete()
                                    rowCount -= 1
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End If

        If Not ddlTable.SelectedItem.Text.Equals("TMLI_DataReferral") Then
            Dim conn As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
            Using con As New SqlConnection(conn)
                Using SqlBulkCopy As New SqlBulkCopy(con)
                    SqlBulkCopy.DestinationTableName = ddlTable.SelectedItem.Text
                    con.Open()
                    SqlBulkCopy.WriteToServer(dtcloned)
                    con.Close()
                End Using
            End Using
        End If

        Dim RC As Integer = 0
        If ddlTable.SelectedItem.Text = "TMLI_DataReferral" Or ddlTable.SelectedItem.Text = "TMLI_Picture_Payment_Type" Or ddlTable.SelectedItem.Text = "TMLI_Picture_Other_Type" Then
        Else
            If overall = 0 Then
                For Each rt As DataRow In dtcloned.Rows
                    Dim query3 As String = "update " + ddlTable.SelectedItem.Text + " set TanggalRollOut = NULL, Status = 'A', IsNew = GETDATE(), RowStatus = 'N' where  " + dtcloned.Columns(0).ColumnName + " = '" + dtcloned.Rows(RC)(0).ToString() + "'"
                    Dim query2 As String = "update " + ddlTable.SelectedItem.Text + " set Status = 'A', IsNew = GETDATE(), IsUpdate = GETDATE(), RowStatus = 'N' where  " + dtcloned.Columns(1).ColumnName + " = '" + dtcloned.Rows(RC)(1).ToString() + "'"
                    Dim query4 As String = "update " + ddlTable.SelectedItem.Text + " set Status = 'I', IsNew = GETDATE(), IsUpdate = GETDATE(), RowStatus = 'N' where  " + dtcloned.Columns(1).ColumnName + " = '" + dtcloned.Rows(RC)(1).ToString() + "'"
                    If ddlTable.SelectedItem.Text = "TMLI_Data_Cabang" Then
                        objDBCom.ExecuteSQL(query3)
                    ElseIf ddlTable.SelectedItem.Text = "TMLI_DataReferral" Then
                        If dtcloned.Rows(RC)(5).ToString().Equals("ACTIVE") Or dtcloned.Rows(RC)(5).ToString().Equals("A") Then
                            objDBCom.ExecuteSQL(query2)
                        Else
                            objDBCom.ExecuteSQL(query4)
                        End If
                    Else
                        objDBCom.ExecuteSQL(query2)
                    End If
                    RC += 1
                Next
            End If
        End If


        updatemasterinfo()
        objDBCom.Dispose()
        File.Delete(csvPath)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallAlertmsg", "alert('Row(s) successfully added');", True)
    End Sub

    Protected Sub updatemasterinfo()
        Dim objDBCom3 As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim q1 As String = ""
        Dim q2 As String = ""
        q1 &= "UPDATE TMLI_Master_Info set VersionNo = VersionNo + 1, UpdatedDate = GETDATE() where TableName = '" + ddlTable.SelectedItem.Text + "'"
        q2 &= "UPDATE TMLI_Master_Info set VersionNo = VersionNo + 1, UpdatedDate = GETDATE() where TableName = 'TMLI_Master_Info'"
        objDBCom3.ExecuteSQL(q1)
        objDBCom3.ExecuteSQL(q2)
        objDBCom3.Dispose()
    End Sub

    Protected Function updatesub(ByVal NIP As String, ByVal Name As String, ByVal Name2 As String, ByVal BP As String, ByVal BP2 As String, ByVal DOB As Date, ByVal DOB2 As Date, ByVal Details As String, ByVal Details2 As String, ByVal stat1 As String, ByVal stat2 As String)
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        If stat1.ToString().Equals("ACTIVE") Or stat1.ToString().Equals("A") Then
            stat1 = "A"
        Else
            stat1 = "I"
        End If

        If Not stat1 = stat2 Or BP = BP2 Or Details = Details2 Or Name = Name2 Or DOB = DOB2 Then
            Dim up As String = "Update TMLI_DataReferral set BirthPlace = '" + BP.ToString() + "', Details = '" + Details.ToString() + "', Status = '" + stat1.ToString() + "', IsUpdate = GETDATE() where NIP = '" + NIP.ToString() + "'"
            objDBCom.ExecuteSQL(up)
            objDBCom.Dispose()
        End If
    End Function

    Private Sub InsertDataIntoDB(dataToInsert As DataRow)
        Dim objDBCom As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim objDBCom2 As New MySQLDBComponent.MySQLDBComponent(POSWeb.POSWeb_SQLConn)
        Dim insertStatement As String = "INSERT INTO TMLI_DataReferral ([NIP],[Name],[BirthPlace],[DateOfBirth],[Details],[Status], IsNew, IsUpdate) VALUES (@nip,@name,@birthPlace,@dateOfBirth,@details,@status, GETDATE(), GETDATE())"
        objDBCom.Parameters.AddWithValue("@nip", dataToInsert("NIP"))
        objDBCom.Parameters.AddWithValue("@name", dataToInsert("Name"))
        objDBCom.Parameters.AddWithValue("@birthPlace", dataToInsert("BirthPlace"))
        objDBCom.Parameters.AddWithValue("@dateOfBirth", dataToInsert("DateOfBirth"))
        objDBCom.Parameters.AddWithValue("@details", dataToInsert("Details"))
        objDBCom.Parameters.AddWithValue("@status", dataToInsert("Status"))
        Try
            Dim affectedRecord = objDBCom.ExecuteSQL(insertStatement)
            objDBCom.Dispose()
            If affectedRecord = False Then
                Dim updateCommandString As String = "UPDATE TMLI_DataReferral SET [Name]=@name, [BirthPlace]=@birthPlace,[DateOfBirth]=@dateOfBirth,[Details]=@details,[Status]=@status, IsUpdate = GETDATE() WHERE NIP=@nip"
                objDBCom2.Parameters.AddWithValue("@nip", dataToInsert("NIP"))
                objDBCom2.Parameters.AddWithValue("@name", dataToInsert("Name"))
                objDBCom2.Parameters.AddWithValue("@birthPlace", dataToInsert("BirthPlace"))
                objDBCom2.Parameters.AddWithValue("@dateOfBirth", dataToInsert("DateOfBirth"))
                objDBCom2.Parameters.AddWithValue("@details", dataToInsert("Details"))
                objDBCom2.Parameters.AddWithValue("@status", dataToInsert("Status"))
                Dim affectedURecord = objDBCom2.ExecuteSQL(updateCommandString)
                objDBCom2.Dispose()
                If affectedURecord = False Then

                End If
            End If
            insertLog.Add(dataToInsert("NIP").ToString(), affectedRecord)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btn_back_Click(sender As Object, e As EventArgs) Handles btn_back.Click
        Response.Redirect("DropDownTable.aspx")
    End Sub
End Class