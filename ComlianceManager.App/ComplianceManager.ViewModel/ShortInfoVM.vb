Imports System.Collections.ObjectModel
Imports SPS.ViewModel.Infrastructure

Public Class ShortInfoVM
    Inherits ViewModelBase




    Public Sub New

    End Sub



    Private _latestAddedComplianceItem As Model.CompliantItem
    Public Property LatestAddedComplianceItem() As Model.CompliantItem
        Get
            Return _latestAddedComplianceItem
        End Get
        Set(ByVal value As Model.CompliantItem)
            _latestAddedComplianceItem = value
            RaisePropertyChanged("LatestAddedComplianceItem")
        End Set
    End Property

    Private _latestChangedComplianceItem As Model.CompliantItem
    Public Property LatestChangedComplianceItem() As Model.CompliantItem
        Get
            Return _latestChangedComplianceItem
        End Get
        Set(ByVal value As Model.CompliantItem)
            _latestChangedComplianceItem = value
            RaisePropertyChanged("LatestChangedComplianceItem")
        End Set
    End Property


    Private _openComplianceItems As Integer
    Public Property OpenComplianceItems() As Integer
        Get
            Return _openComplianceItems
        End Get
        Set(ByVal value As Integer)
            _openComplianceItems = value
            RaisePropertyChanged("OpenComplianceItems")
        End Set
    End Property


    Private _lastThreeComplianceItems As ObservableCollection(Of Model.CompliantItem)

    Public Property LastThreeComplianceItems() As ObservableCollection(Of Model.CompliantItem)
        Get
            Return _lastThreeComplianceItems
        End Get
        Set(ByVal value As ObservableCollection(Of Model.CompliantItem))
            _lastThreeComplianceItems = value
            RaisePropertyChanged("LastThreeComplianceItems")
        End Set
    End Property


End Class
