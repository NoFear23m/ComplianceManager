Imports ComplianceManager.Model
Imports SPS.ViewModel.Infrastructure

Public Class HistoryItemVM
    Inherits ViewModelBase

    Private _historyItem As Model.HistoryItem


    Public Sub New()
        _historyItem = New Model.HistoryItem
    End Sub


    Public Sub New(hItem As Model.HistoryItem)
        _historyItem = hItem

    End Sub


    Public Overridable Property CreationDate As DateTime
        Get
            Return _historyItem.CreationDate
        End Get
        Set(value As DateTime)
            _historyItem.CreationDate = value
            RaisePropertyChanged("CreationDate")
        End Set
    End Property


    Public Overridable Property CreatedBy As String
        Get
            Return _historyItem.CreatedBy
        End Get
        Set(value As String)
            _historyItem.CreatedBy = value
            RaisePropertyChanged("CreatedBy")
        End Set
    End Property


    Public Overridable Property LastEditedBy As String
        Get
            Return _historyItem.LastEditedBy
        End Get
        Set(value As String)
            _historyItem.LastEditedBy = value
            RaisePropertyChanged("LastEditedBy")
        End Set
    End Property


    Public Overridable Property LastChange As DateTime
        Get
            Return _historyItem.LastChange
        End Get
        Set(value As DateTime)
            _historyItem.LastChange = value
            RaisePropertyChanged("LastChange")
        End Set
    End Property

    Public Overridable Property Title As String
        Get
            Return _historyItem.Title
        End Get
        Set(value As String)
            _historyItem.Title = value
            RaisePropertyChanged("Title")
        End Set
    End Property

    Public Overridable Property Description As String
        Get
            Return _historyItem.Description
        End Get
        Set(value As String)
            _historyItem.Description = value
            RaisePropertyChanged("Description")
        End Set
    End Property

    Public Overridable Property Attachments As ICollection(Of ComplianteAttachment)
        Get
            Return _historyItem.Attachments
        End Get
        Set(value As ICollection(Of ComplianteAttachment))
            _historyItem.Attachments = value
            RaisePropertyChanged("Attachments")
        End Set
    End Property







End Class
