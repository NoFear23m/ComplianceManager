Public Class uclHistoryListItem

    Private Sub uclAddEditHistoryItem_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim VM As ViewModel.HistoryItemVM = Me.DataContext
        AddHandler VM.Refresh, AddressOf Refresh

    End Sub


    Private Sub Refresh()
        Try
            Dim VM As ViewModel.HistoryItemVM = Me.DataContext
            Me.DataContext = Nothing
            Me.DataContext = VM
        Catch ex As Exception

        End Try
    End Sub



End Class
