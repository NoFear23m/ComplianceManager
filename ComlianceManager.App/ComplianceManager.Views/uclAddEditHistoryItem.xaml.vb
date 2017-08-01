﻿Imports System.Collections.ObjectModel
Imports System.ComponentModel
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
                                    With {.Title = Replace(fi.Name, fi.Extension, ""), .RelativeFilePath = Now.Year & "\" & Now.Ticks & "_" & fi.Name, .CreatedBy = Environment.UserName, .LastEditedBy = Environment.UserName}
                VM.Attachments.Add(newAtt)

                VM.RefreshView()
                Me.DataContext = Nothing
                Me.DataContext = VM
            Next
            VM.RefreshView()
        End If

    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Dim win As Window = Window.GetWindow(Me)
        win.DialogResult = True
    End Sub

    Private Sub uclAddEditHistoryItem_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim VM As ViewModel.HistoryItemVM = Me.DataContext
        AddHandler VM.Refresh, AddressOf Refresh
        AddHandler Window.GetWindow(Me).Closing, AddressOf closing
    End Sub

    Private Sub closing(sender As Object, e As CancelEventArgs)
        Dim VM As ViewModel.HistoryItemVM = Me.DataContext
        Dim message As String = ""

        If String.IsNullOrEmpty(VM.Title) Then
            message += "Es muss ein titel angegeben werden." & vbNewLine
        End If

        If message.Length > 0 Then
            e.Cancel = True
            MessageBox.Show(message, "Bitte alle Felder ausfüllen...", MessageBoxButton.OK, MessageBoxImage.Information)

        End If

    End Sub

    Private Sub Refresh()
        Dim VM As ViewModel.HistoryItemVM = Me.DataContext
        Me.DataContext = Nothing
        Me.DataContext = VM
    End Sub
End Class