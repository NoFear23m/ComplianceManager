Imports System.Data.Entity
Imports SPS.ViewModel.Infrastructure


Public Class MainVM
    Inherits ViewModelBase


    Public Sub New()
        Database.SetInitializer(New MigrateDatabaseToLatestVersion(Of Context.CompContext, Context.Migrations.Configuration)())

        Using db As New Context.CompContext
            db.Database.CreateIfNotExists()




        End Using




    End Sub


End Class
