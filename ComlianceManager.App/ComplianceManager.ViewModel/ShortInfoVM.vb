Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports SPS.ViewModel.Infrastructure

Public Class ShortInfoVM
    Inherits ViewModelBase




    Public Sub New()
        If DesignerProperties.GetIsInDesignMode(New Windows.DependencyObject) Then
            AviableComplianceItemsCount = 35
            OpenComplianceItems = 8
            LatestAddedComplianceItem = New Model.CompliantItem() With {.CustomerFirstName = "Max", .CustomerLastName = "Mustermann", .ComplianceBrand = Model.CompliantItem.enuBrand.Seat,
                .ComplianceEntryType = New Model.EntryType() With {.EntryTitle = "Garantie", .EntryDescription = ""}, .ComplianceReason = New Model.Reason() With {.ReasonTitle = "TestReason", .ReasonDescription = ""} _
                , .CreatedByUserName = "testUser", .CreationDate = Now, .CustomerNumber = 12345, .LastChange = Now, .LastChangeByUserName = "testUSer2"}
        Else
            Using db As New Context.CompContext
                LatestAddedComplianceItem = db.ComplianceItems.Where(Function(d) d.IsDeleted = False).OrderByDescending(Function(o) o.CreationDate).FirstOrDefault
                LatestChangedComplianceItem = db.ComplianceItems.Where(Function(d) d.IsDeleted = False).OrderByDescending(Function(o) o.LastChange).FirstOrDefault
                OpenComplianceItems = db.ComplianceItems.Where(Function(d) d.IsDeleted = False).Where(Function(o) o.FinishedAt Is Nothing).Count
                LastThreeComplianceItems = New ObservableCollection(Of Model.CompliantItem)(db.ComplianceItems.OrderByDescending(Function(o) o.CreationDate).Take(3).ToList)
                AviableComplianceItemsCount = db.ComplianceItems.Where(Function(o) o.IsDeleted = False).Count
            End Using

        End If


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

    Private _aviableComplianceItemsCount As Integer
    Public Property AviableComplianceItemsCount() As Integer
        Get
            Return _aviableComplianceItemsCount
        End Get
        Set(ByVal value As Integer)
            _aviableComplianceItemsCount = value
            RaisePropertyChanged("AviableComplianceItemsCount")
        End Set
    End Property

End Class
