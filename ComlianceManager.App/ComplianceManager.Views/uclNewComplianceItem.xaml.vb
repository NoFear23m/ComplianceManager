Public Class uclNewComplianceItem


    Private Sub ButtonAbbrechen_Click(sender As Object, e As RoutedEventArgs)
        Dim win As Window = Window.GetWindow(Me)
        win.DialogResult = False
    End Sub

    Private Sub ButtonAnlegen_Click(sender As Object, e As RoutedEventArgs)
        Dim win As Window = Window.GetWindow(Me)
        win.DialogResult = True
    End Sub
End Class
