
Imports System.Data.Entity
Imports ComplianceManager.ViewModel
Imports MahApps.Metro.Controls


Class MainWindow


    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded


        Try
            Database.SetInitializer(New MigrateDatabaseToLatestVersion(Of ComplianceManager.Context.CompContext, ComplianceManager.Context.Migrations.Configuration)())
        Catch ex As Exception
            MsgBox("Fehler beim Migrieren")
        End Try

        Try
            Using db As New ComplianceManager.Context.CompContext
                db.Database.CreateIfNotExists()

                If Not db.Settings.Any Then
                    Debug.WriteLine("Keine Settings vorhanden!!!!!!")

                End If

                If Not db.Resons.Any Then
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Noch nicht bewertbar", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Reparatur/ServiceQualität", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Kundenbehandlung", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Termingestalltung", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Garantie/Kulanzabwicklung", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Preis-/Leistungsgestalltung", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Teileversorgung", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Produktqualität", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Fremdverschulden", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Rostgaratieabwicklung", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Lob", .ReasonDescription = ""})
                    db.Resons.Add(New ComplianceManager.Model.Reason With {.ReasonTitle = "Fremdverschulden", .ReasonDescription = ""})
                End If

                If Not db.EntryTypes.Any Then
                    db.EntryTypes.Add(New ComplianceManager.Model.EntryType With {.EntryTitle = "Kundenreklamation", .EntryDescription = ""})
                    db.EntryTypes.Add(New ComplianceManager.Model.EntryType With {.EntryTitle = "Spectra", .EntryDescription = ""})
                    db.EntryTypes.Add(New ComplianceManager.Model.EntryType With {.EntryTitle = "Persönlich", .EntryDescription = ""})
                    db.EntryTypes.Add(New ComplianceManager.Model.EntryType With {.EntryTitle = "Telefonisch", .EntryDescription = ""})
                    db.EntryTypes.Add(New ComplianceManager.Model.EntryType With {.EntryTitle = "E-Mail", .EntryDescription = ""})
                    db.EntryTypes.Add(New ComplianceManager.Model.EntryType With {.EntryTitle = "Social-Media", .EntryDescription = ""})
                    db.EntryTypes.Add(New ComplianceManager.Model.EntryType With {.EntryTitle = "Brief", .EntryDescription = ""})
                    db.EntryTypes.Add(New ComplianceManager.Model.EntryType With {.EntryTitle = "PIA Kundenfeedback", .EntryDescription = ""})
                    db.EntryTypes.Add(New ComplianceManager.Model.EntryType With {.EntryTitle = "KUZU-U2", .EntryDescription = ""})

                End If

                If Not db.Settings.Any Then
                    db.Settings.Add(New ComplianceManager.Model.Setting() With {.Key = "AttachmentsPath", .Title = "Der Pfad wo die Attachment gespeichert werden sollen.", .Value = "D:\Attachments\"})
                    db.Settings.Add(New ComplianceManager.Model.Setting() With {.Key = "ParnterNetURL", .Title = "Die URL des Partner.Net Fensters mit den Reklamationen", .Value = "https://www.auto-partner.net/portal/at/thirdparty?directlink=KU_REK&noFramesMode=true"})
                End If


                db.SaveChanges()

            End Using
        Catch ex As Exception
            MsgBox("Fehler beim erstellen der ersten einträge")
        End Try

        Dim test As String = Environment.UserName

        Dispatcher.Invoke(Sub() init())


    End Sub


    Private Sub init()
        Dim mvm As New MainVM()
        If Not mvm.IsUserInDB Then
            MsgBox("Sie wurden als Benutzer nicht in der Datenbank hinterlegt und haben sohin keinen Zugriff auf diese Resourcen. Bitte wenden Sie sich an den Serviceleiter.", MsgBoxStyle.OkOnly, "Programmende")
            ShutDown()
        End If


        mvm.IsFisrtLogin()

        Me.DataContext = mvm
    End Sub



    Private Sub ShutDown()
        My.Application.ShutdownMode = ShutdownMode.OnExplicitShutdown
        My.Application.Shutdown()
    End Sub

    Private Sub MainWindow_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized


    End Sub

    Private Sub MenuItem_Click(sender As Object, e As RoutedEventArgs)
        Process.Start("https://github.com/NoFear23m/ComplianceManager")
    End Sub

    Private Sub MenuItem1_Click(sender As Object, e As RoutedEventArgs)
        Process.Start("https://github.com/NoFear23m/ComplianceManager/wiki")
    End Sub

    Private Sub MenuItem2_Click(sender As Object, e As RoutedEventArgs)
        Process.Start(My.Application.Info.DirectoryPath)
    End Sub
End Class
