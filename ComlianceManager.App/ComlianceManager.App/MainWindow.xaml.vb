
Imports ComplianceManager.ViewModel

Class MainWindow
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded



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
