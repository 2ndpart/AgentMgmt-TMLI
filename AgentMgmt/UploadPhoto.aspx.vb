Imports System.IO

Public Class UploadPhoto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Directory.GetFiles(Server.MapPath("~/TemporaryImageFile/")).Length > 0 Then
            If File.Exists(Server.MapPath("~/TemporaryImageFile/LoginPane.png")) Then
                imgPreview1.ImageUrl = "~/TemporaryImageFile/LoginPane.png"
            End If

            If File.Exists(Server.MapPath("~/TemporaryImageFile/FirstLandingPage.png")) Then
                imgPreview2.ImageUrl = "~/TemporaryImageFile/FirstLandingPage.png"
            End If

            If File.Exists(Server.MapPath("~/TemporaryImageFile/SecondLandingPage.png")) Then
                imgPreview3.ImageUrl = "~/TemporaryImageFile/SecondLandingPage.png"
            End If

            If File.Exists(Server.MapPath("~/TemporaryImageFile/ThirdLandingPage.png")) Then
                imgPreview4.ImageUrl = "~/TemporaryImageFile/ThirdLandingPage.png"
            End If

            If File.Exists(Server.MapPath("~/TemporaryImageFile/ThirdLandingPage.png")) Then
                imgPreview5.ImageUrl = "~/TemporaryImageFile/ProspectModulePage.png"
            End If

            If File.Exists(Server.MapPath("~/TemporaryImageFile/ProspectFormHeader.png")) Then
                imgPreview6.ImageUrl = "~/TemporaryImageFile/ProspectFormHeader.png"
            End If

            If File.Exists(Server.MapPath("~/TemporaryImageFile/SIModulePage.png")) Then
                imgPreview7.ImageUrl = "~/TemporaryImageFile/SIModulePage.png"
            End If

            If File.Exists(Server.MapPath("~/TemporaryImageFile/SIFormHeader.png")) Then
                imgPreview8.ImageUrl = "~/TemporaryImageFile/SIFormHeader.png"
            End If

            If File.Exists(Server.MapPath("~/TemporaryImageFile/SPAJModulePage.png")) Then
                imgPreview9.ImageUrl = "~/TemporaryImageFile/SPAJModulePage.png"
            End If

            If File.Exists(Server.MapPath("~/TemporaryImageFile/SPAJFormHeader.png")) Then
                imgPreview10.ImageUrl = "~/TemporaryImageFile/SPAJFormHeader.png"
            End If
        End If
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If FileUpload1.HasFile = True Then
            FileUpload1.SaveAs(Path.Combine("C:\BackgroundImages", "LoginPage.png"))

            If FileUpload1.PostedFile IsNot Nothing Then
                FileUpload1.SaveAs(Server.MapPath("~/TemporaryImageFile/LoginPane.png"))
                imgPreview1.ImageUrl = "~/TemporaryImageFile/LoginPane.png"
            End If
        End If

        If FileUpload2.HasFile = True Then
            FileUpload2.SaveAs(Path.Combine("C:\BackgroundImages", "FirstLandingPage.png"))

            If FileUpload2.PostedFile IsNot Nothing Then
                FileUpload2.SaveAs(Server.MapPath("~/TemporaryImageFile/FirstLandingPage.png"))
                imgPreview2.ImageUrl = "~/TemporaryImageFile/FirstLandingPage.png"
            End If
        End If

        If FileUpload3.HasFile = True Then
            FileUpload3.SaveAs(Path.Combine("C:\BackgroundImages", "SecondLandingPage.png"))

            If FileUpload3.PostedFile IsNot Nothing Then
                FileUpload3.SaveAs(Server.MapPath("~/TemporaryImageFile/SecondLandingPage.png"))
                imgPreview3.ImageUrl = "~/TemporaryImageFile/SecondLandingPage.png"
            End If
        End If

        If FileUpload4.HasFile = True Then
            FileUpload4.SaveAs(Path.Combine("C:\BackgroundImages", "ThirdLandingPage.png"))

            If FileUpload4.PostedFile IsNot Nothing Then
                FileUpload4.SaveAs(Server.MapPath("~/TemporaryImageFile/ThirdLandingPage.png"))
                imgPreview4.ImageUrl = "~/TemporaryImageFile/ThirdLandingPage.png"
            End If
        End If

        If FileUpload5.HasFile = True Then
            FileUpload5.SaveAs(Path.Combine("C:\BackgroundImages", "ProspectModulePage.png"))

            If FileUpload5.PostedFile IsNot Nothing Then
                FileUpload5.SaveAs(Server.MapPath("~/TemporaryImageFile/ProspectModulePage.png"))
                imgPreview5.ImageUrl = "~/TemporaryImageFile/ProspectModulePage.png"
            End If
        End If

        If FileUpload6.HasFile = True Then
            FileUpload6.SaveAs(Path.Combine("C:\BackgroundImages", "ProspectFormHeader.png"))

            If FileUpload6.PostedFile IsNot Nothing Then
                FileUpload6.SaveAs(Server.MapPath("~/TemporaryImageFile/ProspectFormHeader.png"))
                imgPreview6.ImageUrl = "~/TemporaryImageFile/ProspectFormHeader.png"
            End If
        End If
        If FileUpload7.HasFile = True Then
            FileUpload7.SaveAs(Path.Combine("C:\BackgroundImages", "SIModulePage.png"))

            If FileUpload7.PostedFile IsNot Nothing Then
                FileUpload7.SaveAs(Server.MapPath("~/TemporaryImageFile/SIModulePage.png"))
                imgPreview7.ImageUrl = "~/TemporaryImageFile/SIModulePage.png"
            End If
        End If
        If FileUpload8.HasFile = True Then
            FileUpload8.SaveAs(Path.Combine("C:\BackgroundImages", "SIFormHeader.png"))

            If FileUpload8.PostedFile IsNot Nothing Then
                FileUpload8.SaveAs(Server.MapPath("~/TemporaryImageFile/SIFormHeader.png"))
                imgPreview8.ImageUrl = "~/TemporaryImageFile/SIFormHeader.png"
            End If
        End If
        If FileUpload9.HasFile = True Then
            FileUpload9.SaveAs(Path.Combine("C:\BackgroundImages", "SPAJModulePage.png"))

            If FileUpload9.PostedFile IsNot Nothing Then
                FileUpload9.SaveAs(Server.MapPath("~/TemporaryImageFile/SPAJModulePage.png"))
                imgPreview9.ImageUrl = "~/TemporaryImageFile/SPAJModulePage.png"
            End If
        End If
        If FileUpload10.HasFile = True Then
            FileUpload10.SaveAs(Path.Combine("C:\BackgroundImages", "SPAJFormHeader.png"))

            If FileUpload10.PostedFile IsNot Nothing Then
                FileUpload10.SaveAs(Server.MapPath("~/TemporaryImageFile/SPAJFormHeader.png"))
                imgPreview10.ImageUrl = "~/TemporaryImageFile/SPAJFormHeader.png"
            End If
        End If

        Page.ClientScript.RegisterStartupScript(Me.GetType(), "Window", "alert('File Uploaded');", True)
    End Sub
End Class