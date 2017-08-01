
Imports ComplianceManager.ViewModel

Class MainWindow
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded





    End Sub

    Private Sub ShutDown()
        My.Application.ShutdownMode = ShutdownMode.OnExplicitShutdown
        My.Application.Shutdown()
    End Sub

    Private Sub MainWindow_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        Dim mvm As New MainVM()
        If Not mvm.IsUserInDB Then
            MsgBox("Sie wurden als Benutzer nicht in der Datenbank hinterlegt und haben sohin keinen Zugriff auf diese Resourcen. Bitte wenden Sie sich an den Serviceleiter.", MsgBoxStyle.OkOnly, "Programmende")
            ShutDown()
        End If
        Me.DataContext = mvm

    End Sub


End Class
