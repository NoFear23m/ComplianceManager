Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports ComplianceManager.Model
Imports Microsoft.Win32
Imports SPS.ViewModel.Infrastructure

Public Class HistoryItemVM
    Inherits ViewModelBase

    Friend _historyItem As Model.HistoryItem
    Public settPath As String
    Public saveFolder As String = settPath & "\" & Now.Year & "\"
    Private _db As Context.CompContext

    Public Event Refresh()

    Public Sub New()
        _historyItem = New Model.HistoryItem
        'Using settDb As New Context.CompContext
        '    settPath = settDb.Settings.Where(Function(s) s.Key = "AttachmentsPath").FirstOrDefault.Value
        'End Using
        'If _historyItem.Attachments Is Nothing Then _historyItem.Attachments = New List(Of ComplianteAttachment)
    End Sub


    Public Sub New(hItem As Model.HistoryItem, db As Context.CompContext)
        _historyItem = hItem
        If _historyItem.Attachments Is Nothing Then _historyItem.Attachments = New List(Of ComplianteAttachment)
        _db = db
        Using settDb As New Context.CompContext
            settPath = settDb.Settings.Where(Function(s) s.Key = "AttachmentsPath").FirstOrDefault.Value
        End Using

    End Sub

    Public Sub RefreshView()
        RaisePropertyChanged("Attachments")
        RaisePropertyChanged("Attachments")
        RaisePropertyChanged("Attachments")
        RaiseEvent Refresh()
    End Sub


    Public Property CreationDate As DateTime
        Get
            Return _historyItem.CreationDate
        End Get
        Set(value As DateTime)
            _historyItem.CreationDate = value
            RaisePropertyChanged("CreationDate")
        End Set
    End Property


    Public Property CreatedBy As String
        Get
            Return _historyItem.CreatedBy
        End Get
        Set(value As String)
            _historyItem.CreatedBy = value
            RaisePropertyChanged("CreatedBy")
        End Set
    End Property


    Public Property LastEditedBy As String
        Get
            Return _historyItem.LastEditedBy
        End Get
        Set(value As String)
            _historyItem.LastEditedBy = value
            RaisePropertyChanged("LastEditedBy")
        End Set
    End Property


    Public Property LastChange As DateTime
        Get
            Return _historyItem.LastChange
        End Get
        Set(value As DateTime)
            _historyItem.LastChange = value
            RaisePropertyChanged("LastChange")
        End Set
    End Property

    Public Property Title As String
        Get
            Return _historyItem.Title
        End Get
        Set(value As String)
            _historyItem.Title = value
            RaisePropertyChanged("Title")
        End Set
    End Property

    Public Property Description As String
        Get
            Return _historyItem.Description
        End Get
        Set(value As String)
            _historyItem.Description = value
            RaisePropertyChanged("Description")
        End Set
    End Property

    ' Private _attachments As ObservableCollection(Of ComplianteAttachment)
    Public Property Attachments As ICollection(Of ComplianteAttachment)
        Get
            Return _historyItem.Attachments
        End Get
        Set(value As ICollection(Of ComplianteAttachment))
            _historyItem.Attachments = value
            RaisePropertyChanged("Attachments")
        End Set
    End Property

    'Private _addItemCommand As ICommand
    'Public Property AddItemCommand() As ICommand
    '    Get
    '        If _addItemCommand Is Nothing Then _addItemCommand = New RelayCommand(AddressOf AddItemCommand_Execute, AddressOf AddItemCommand_CanExecute)
    '        Return _addItemCommand
    '    End Get
    '    Set(ByVal value As ICommand)
    '        _addItemCommand = value
    '        RaisePropertyChanged("AddItemCommand")
    '    End Set
    'End Property

    'Private Function AddItemCommand_CanExecute(obj As Object) As Boolean
    '    Return True
    'End Function

    'Private Sub AddItemCommand_Execute(obj As Object)
    '    Using settDb As New Context.CompContext
    '        Dim settPath As String = settDb.Settings.Where(Function(s) s.Key = "AttachmentsPath").FirstOrDefault.Value
    '        Dim saveFolder As String = settPath & "\" & Now.Year & "\"
    '        If Not IO.Directory.Exists(saveFolder) Then IO.Directory.CreateDirectory(saveFolder)

    '        Dim ofDiag As New Forms.OpenFileDialog
    '        ofDiag.Multiselect = True
    '        ofDiag.RestoreDirectory = True
    '        ofDiag.Title = "Wählen Sie die Dateien welches Sie hinzufügen möchten..."
    '        If Attachments Is Nothing Then Attachments = New List(Of Model.ComplianteAttachment)
    '        Attachments.Add(New ComplianteAttachment())

    '        If ofDiag.ShowDialog = Forms.DialogResult.OK Then
    '            If Attachments Is Nothing Then Attachments = New List(Of Model.ComplianteAttachment)
    '            For Each f In ofDiag.FileNames
    '                Dim fi As IO.FileInfo = New IO.FileInfo(f)
    '                Attachments.Add(New ComplianteAttachment() _
    '                                With {.Title = fi.Name, .RelativeFilePath = Now.Year & "\" & fi.Name, .CreatedBy = Environment.UserName, .LastChange = Environment.UserName})
    '            Next
    '        End If
    '    End Using
    'End Sub




    Private _editCommand As ICommand
    Public Property EditCommand() As ICommand
        Get
            If _editCommand Is Nothing Then _editCommand = New RelayCommand(AddressOf EditCommand_Execute, AddressOf EditCommand_CanExecute)
            Return _editCommand
        End Get
        Set(ByVal value As ICommand)
            _editCommand = value
            RaisePropertyChanged("EditCommand")
        End Set
    End Property

    Private Function EditCommand_CanExecute(obj As Object) As Boolean
        Return Environment.UserName = Me.CreatedBy
    End Function

    Private Sub EditCommand_Execute(obj As Object)
        ' Using db As New Context.CompContext
        Dim win As New Windows.Window
            win.Title = "Historie bearbeiten..."
        win.Width = 600
        win.Height = 350
        win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen

            win.DataContext = Me
            win.Content = New ContentPresenter With {.Content = Me}

            If win.ShowDialog Then




                If Attachments Is Nothing Then Attachments = New List(Of ComplianteAttachment)

            Dim anz As Integer = _db.SaveChanges()

            RefreshView()
            RaiseEvent Refresh()
        End If
        ' End Using
    End Sub


    Private _addAttachmentCommand As ICommand
    Public Property AddAttachmentCommand() As ICommand
        Get
            If _addAttachmentCommand Is Nothing Then _addAttachmentCommand = New RelayCommand(AddressOf AddAttachmentCommand_execute, AddressOf AddAttachmentCommand_CanExecute)
            Return _addAttachmentCommand
        End Get
        Set(ByVal value As ICommand)
            _addAttachmentCommand = value
            RaisePropertyChanged("AddAttachmentCommand")
        End Set
    End Property

    Private Function AddAttachmentCommand_CanExecute(obj As Object) As Boolean
        Return True
    End Function

    Private Sub AddAttachmentCommand_execute(obj As Object)
        'Using settDb As New Context.CompContext
        Dim settPath As String = _db.Settings.Where(Function(s) s.Key = "AttachmentsPath").FirstOrDefault.Value
        Dim saveFolder As String = settPath & "\" & Now.Year & "\"
            If Not IO.Directory.Exists(saveFolder) Then IO.Directory.CreateDirectory(saveFolder)

            Dim ofDiag As New Forms.OpenFileDialog
            ofDiag.Multiselect = True
            ofDiag.RestoreDirectory = True
            ofDiag.Title = "Wählen Sie die Dateien welches Sie hinzufügen möchten..."
            If Attachments Is Nothing Then Attachments = New List(Of Model.ComplianteAttachment)
        'Attachments.Add(New ComplianteAttachment())

        If ofDiag.ShowDialog = Forms.DialogResult.OK Then
            If Attachments Is Nothing Then Attachments = New List(Of Model.ComplianteAttachment)
            For Each f In ofDiag.FileNames
                Dim fi As IO.FileInfo = New IO.FileInfo(f)
                Attachments.Add(New ComplianteAttachment() _
                                    With {.Title = Replace(fi.Name, fi.Extension, ""), .RelativeFilePath = Now.Year & "\" & Now.Ticks & "_" & fi.Name, .CreatedBy = Environment.UserName, .LastEditedBy = Environment.UserName})
            Next
        End If

        'End Using
        RefreshView()
    End Sub



    Private _deleteAttachmentCommand As ICommand
    Public Property DeleteAttachmentcommand() As ICommand
        Get
            If _deleteAttachmentCommand Is Nothing Then _deleteAttachmentCommand = New RelayCommand(AddressOf DeleteAttachmentcommand_Execute, AddressOf DeleteAttachmentcommand_Canexecute)
            Return _deleteAttachmentCommand
        End Get
        Set(ByVal value As ICommand)
            _deleteAttachmentCommand = value
            RaisePropertyChanged("DeleteAttachmentcommand")
        End Set
    End Property

    Private Function DeleteAttachmentcommand_Canexecute(obj As Object) As Boolean
        Return obj IsNot Nothing
    End Function

    Private Sub DeleteAttachmentcommand_Execute(obj As Object)
        Attachments.Remove(Attachments.Where(Function(a) a.RelativeFilePath = DirectCast(obj, Model.ComplianteAttachment).RelativeFilePath).First)
        RefreshView()
    End Sub
End Class
