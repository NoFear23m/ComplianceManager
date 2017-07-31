Imports System.Collections.ObjectModel
Imports ComplianceManager.Model

Public Class uclAddEditHistoryItem
    Private Sub ItemsControl_Drop(sender As Object, e As DragEventArgs)





        If e.Data.GetDataPresent(DataFormats.FileDrop) Then

            Dim VM As ViewModel.HistoryItemVM = Me.DataContext
            If Not IO.Directory.Exists(VM.saveFolder) Then IO.Directory.CreateDirectory(VM.saveFolder)
            If VM.Attachments Is Nothing Then VM.Attachments = New List(Of Model.ComplianteAttachment)
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            For Each f In files
                Dim fi As New IO.FileInfo(f)
                Dim newAtt As New ComplianteAttachment() _
                                    With {.Title = fi.Name, .RelativeFilePath = Now.Year & "\" & fi.Name, .CreatedBy = Environment.UserName, .LastEditedBy = Environment.UserName}
                VM.Attachments.Add(newAtt)

            Next
        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim win As Window = Window.GetWindow(Me)
        win.DialogResult = True
    End Sub


End Class
