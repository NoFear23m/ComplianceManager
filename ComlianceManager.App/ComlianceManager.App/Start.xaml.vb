Imports System.Data.Entity
Imports System.Runtime.Remoting.Contexts

Public Class Start
    Private Sub Start_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim win As New MainWindow
        Dispatcher.Invoke(Sub() win.Show())
        Me.Close()
    End Sub
End Class
