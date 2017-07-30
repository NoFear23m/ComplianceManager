Imports System.Data.Entity
Imports System.Runtime.Remoting.Contexts

Class Application
    Private Sub Application_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup


        'HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize


            Database.SetInitializer(New MigrateDatabaseToLatestVersion(Of ComplianceManager.Context.CompContext, ComplianceManager.Context.Migrations.Configuration)())

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

            db.SaveChanges()

        End Using

        





        Dim mw As New MainWindow
        mw.Show


    End Sub

    ' Ereignisse auf Anwendungsebene wie Startup, Exit und DispatcherUnhandledException
    ' können in dieser Datei verarbeitet werden.

End Class
