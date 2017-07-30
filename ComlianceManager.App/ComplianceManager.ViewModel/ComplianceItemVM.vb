Imports SPS.ViewModel.Infrastructure

Public Class ComplianceItemVM
    Inherits ViewModelBase


    Private _compItem As Model.CompliantItem


    Public Sub New()

    End Sub

    Public Sub New(item As Model.CompliantItem)
        _compItem = item
    End Sub



    Public ReadOnly Property ID As Integer
        Get
            Return _compItem.ID
        End Get
    End Property


    Public Property CustomerFirstName As String
        Get
            Return _compItem.CustomerFirstName
        End Get
        Set(value As String)
            _compItem.CustomerFirstName = value
            RaisePropertyChanged("CustomerFirstName")
        End Set
    End Property


    Public Property CustomerLastName As String
        Get
            Return _compItem.CustomerLastName
        End Get
        Set(value As String)
            _compItem.CustomerLastName = value
            RaisePropertyChanged("CustomerLastName")
        End Set
    End Property

    Public Property CustomerNumber As Integer
        Get
            Return _compItem.CustomerNumber
        End Get
        Set(value As Integer)
            _compItem.CustomerNumber = value
            RaisePropertyChanged("CustomerNumber")
        End Set
    End Property


    Public Property ComplianceBrand As Model.CompliantItem.enuBrand
        Get
            Return _compItem.ComplianceBrand
        End Get
        Set(value As Model.CompliantItem.enuBrand)
            _compItem.ComplianceBrand = value
            RaisePropertyChanged("ComplianceBrand")
        End Set
    End Property

    Public Property LicensePlate As String
        Get
            Return _compItem.LicensePlate
        End Get
        Set(value As String)
            _compItem.LicensePlate = value
            RaisePropertyChanged("LicensePlate")
        End Set
    End Property


    Public Property ComplianceReason As Model.Reason
        Get
            Return _compItem.ComplianceReason
        End Get
        Set(value As Model.Reason)
            _compItem.ComplianceReason = value
            RaisePropertyChanged("ComplianceReason")
        End Set
    End Property


    Public Property ComplianceEntryType As Model.EntryType
        Get
            Return _compItem.ComplianceEntryType
        End Get
        Set(value As Model.EntryType)
            _compItem.ComplianceEntryType = value
            RaisePropertyChanged("ComplianceEntryType")
        End Set
    End Property


    Public Property FinishedAt As Date?
        Get
            Return _compItem.FinishedAt
        End Get
        Set(value As Date?)
            _compItem.FinishedAt = value
            RaisePropertyChanged("FinishedAt")
        End Set
    End Property


    Public ReadOnly Property CreationDate As DateTime
        Get
            Return _compItem.CreationDate
        End Get
    End Property

    Public ReadOnly Property CreatedByUserName As String
        Get
            Return _compItem.CreatedByUserName
        End Get
    End Property

    Public ReadOnly Property LastChange As DateTime
        Get
            Return _compItem.LastChange
        End Get
    End Property

    Public ReadOnly Property LastChangeByUserName As String
        Get
            Return _compItem.LastChangeByUserName
        End Get
    End Property



    Public Property ComplianceHistory As ICollection(Of Model.HistoryItem)
        Get
            Return _compItem.ComplianceHistory
        End Get
        Set(value As ICollection(Of Model.HistoryItem))
            _compItem.ComplianceHistory = value
        End Set
    End Property













End Class
