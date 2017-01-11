Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.IO

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class BackgroundImageService
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()>
    Public Function GetAllBackgroundImage() As DataTable
        Dim dTable As New DataTable
        dTable.TableName = "BackgroundImage"
        dTable.Columns.Add("FileName")
        dTable.Columns.Add("FileBase64String")

        Try
            Dim fileLocation As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BackgroundImage")
            Dim files As String() = Directory.GetFiles(fileLocation)

            For Each file As String In files
                Dim dr As DataRow = dTable.NewRow

                Dim fileBytes As Byte() = System.IO.File.ReadAllBytes(file)
                Dim base64StringFile As String = Convert.ToBase64String(fileBytes)
                Dim fileName As String = Path.GetFileName(file)

                dr("FileName") = fileName
                dr("FileBase64String") = base64StringFile

                dTable.Rows.Add(dr)
            Next
        Catch ex As Exception

        End Try
        Return dTable
    End Function
End Class