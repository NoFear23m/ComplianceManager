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

    Public Sub New()
        _historyItem = New Model.HistoryItem
        Using settDb As New Context.CompContext
            settPath = settDb.Settings.Where(Function(s) s.Key = "AttachmentsPath").FirstOrDefault.Value
        End Using
    End Sub


    Public Sub New(hItem As Model.HistoryItem)
        _historyItem = hItem
        Using settDb As New Context.CompContext
            settPath = settDb.Settings.Where(Function(s) s.Key = "AttachmentsPath").FirstOrDefault.Value
        End Using
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
    Public Property Attachments As List(Of ComplianteAttachment)
        Get
            Return _historyItem.Attachments
        End Get
        Set(value As List(Of ComplianteAttachment))
            _historyItem.Attachments = value
            RaisePropertyChanged("Attachments")
        End Set
    End Property

    Private _addItemCommand As ICommand
    Public Property AddItemCommand() As ICommand
        Get
            If _addItemCommand Is Nothing Then _addItemCommand = New RelayCommand(AddressOf AddItemCommand_Execute, AddressOf AddItemCommand_CanExecute)
            Return _addItemCommand
        End Get
        Set(ByVal value As ICommand)
            _addItemCommand = value
            RaisePropertyChanged("AddItemCommand")
        End Set
    End Property

    Private Function AddItemCommand_CanExecute(obj As Object) As Boolean
        Return True
    End Function

    Private Sub AddItemCommand_Execute(obj As Object)
        Using settDb As New Context.CompContext
            Dim settPath As String = settDb.Settings.Where(Function(s) s.Key = "AttachmentsPath").FirstOrDefault.Value
            Dim saveFolder As String = settPath & "\" & Now.Year & "\"
            If Not IO.Directory.Exists(saveFolder) Then IO.Directory.CreateDirectory(saveFolder)

            Dim ofDiag As New Forms.OpenFileDialog
            ofDiag.Multiselect = True
            ofDiag.RestoreDirectory = True
            ofDiag.Title = "Wählen Sie die Dateien welches Sie hinzufügen möchten..."

            If ofDiag.ShowDialog = Forms.DialogResult.OK Then
                If Attachments Is Nothing Then Attachments = New List(Of Model.ComplianteAttachment)
                For Each f In ofDiag.FileNames
                    Dim fi As IO.FileInfo = New IO.FileInfo(f)
                    Attachments.Add(New ComplianteAttachment() _
                                    With {.Title = fi.Name, .RelativeFilePath = Now.Year & "\" & fi.Name, .CreatedBy = Environment.UserName, .LastChange = Environment.UserName})
                Next
                'db.SaveChanges()
            End If
        End Using
    End Sub




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
        Using db As New Context.CompContext
            Dim win As New Windows.Window
            win.Title = "Historie bearbeiten..."
            win.Width = 500
            win.Height = 250
            win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen
            db.Entry(Of HistoryItem)(_historyItem).Entity.
            Dim newHistVm = Me
            win.DataContext = newHistVm
            win.Content = New ContentPresenter With {.Content = Me}

            If win.ShowDialog Then




                If Attachments Is Nothing Then Attachments = New List(Of ComplianteAttachment)

                Dim anz As Integer = db.SaveChanges()


            End If
        End Using
    End Sub


    'Private _saveCommand As ICommand
    'Public Property SaveCommand() As ICommand
    '    Get
    '        If _saveCommand Is Nothing Then _saveCommand = New RelayCommand(AddressOf SaveCommand_Execute, AddressOf SaveCommand_CanExecute)
    '        Return _saveCommand
    '    End Get
    '    Set(ByVal value As ICommand)
    '        _saveCommand = value
    '        RaisePropertyChanged("SaveCommand")
    '    End Set
    'End Property

    'Private Function SaveCommand_CanExecute(obj As Object) As Boolean
    '    Return True
    'End Function

    'Private Sub SaveCommand_Execute(obj As Object)
    '    Using db As New Context.CompContext
    '        For Each a As Model.ComplianteAttachment In Attachments

    '            Dim currComp As Model.CompliantItem =
    '        If a.ID = 0 Then

    '        End If

    '        Next
    '    End Using
    'End Sub
End Class
