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

        End Using

        





        Dim mw As New MainWindow
        mw.Show


    End Sub

    ' Ereignisse auf Anwendungsebene wie Startup, Exit und DispatcherUnhandledException
    ' können in dieser Datei verarbeitet werden.

End Class
