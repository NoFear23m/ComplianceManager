Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports SPS.ViewModel.Infrastructure

Public Class ComplianteItemsVM
    Inherits ViewModelBase

    Friend _mainVM As MainVM
    Friend _context As Context.CompContext
    Private showOnProps As Boolean = False

    Public Sub New()
        If _context Is Nothing Then _context = New Context.CompContext

        Load()

        showOnProps = True
    End Sub

    Public Sub New(mainVM As MainVM)
        _mainVM = mainVM
        If _context Is Nothing Then _context = New Context.CompContext

        Load()

        showOnProps = True
    End Sub

    Friend Sub Load()
        ComplianceItems = New ObservableCollection(Of ComplianceItemVM)
        _context = New Context.CompContext
        Dim itemsQuery = _context.ComplianceItems.OrderByDescending(Function(o) o.LastChange)
        If Not IncludeDeleted Then itemsQuery = itemsQuery.Where(Function(d) d.IsDeleted = False)
        If ShowOnlyOpenItems Then itemsQuery = itemsQuery.Where(Function(i) i.FinishedAt Is Nothing)

        Dim items As List(Of Model.CompliantItem) = itemsQuery.ToList

        For Each i As Model.CompliantItem In items
            ComplianceItems.Add(New ComplianceItemVM(i, _context, Me))
        Next
        ComplianceItemsView = Windows.Data.CollectionViewSource.GetDefaultView(ComplianceItems)
    End Sub


    Private _showOnlyOpenItems As Boolean = True
    Public Property ShowOnlyOpenItems() As Boolean
        Get
            Return _showOnlyOpenItems
        End Get
        Set(ByVal value As Boolean)
            _showOnlyOpenItems = value
            RaisePropertyChanged("ShowOnlyOpenItems")
            If showOnProps Then Load()
        End Set
    End Property

    Private _includeDeleted As Boolean = False
    Public Property IncludeDeleted() As Boolean
        Get
            Return _includeDeleted
        End Get
        Set(ByVal value As Boolean)
            _includeDeleted = value
            RaisePropertyChanged("IncludeDeleted")
            If showOnProps Then Load()
        End Set
    End Property


    Private _complianceItems As ObservableCollection(Of ComplianceItemVM)

    Public Property ComplianceItems() As ObservableCollection(Of ComplianceItemVM)
        Get
            Return _complianceItems
        End Get
        Set(ByVal value As ObservableCollection(Of ComplianceItemVM))
            _complianceItems = value
            RaisePropertyChanged("ComplianceItems")
        End Set
    End Property

    Private _complianceItemsView As ICollectionView
    Public Property ComplianceItemsView() As ICollectionView
        Get
            Return _complianceItemsView
        End Get
        Set(ByVal value As ICollectionView)
            _complianceItemsView = value
            RaisePropertyChanged("ComplianceItemsView")
        End Set
    End Property

    Private _filterString As String
    Public Property FilterString() As String
        Get
            Return _filterString
        End Get
        Set(ByVal value As String)
            _filterString = value
            RaisePropertyChanged("FilterString")
            ComplianceItemsView.Filter = New Predicate(Of Object)(AddressOf Filter_Queue)
        End Set
    End Property

    Private Function Filter_Queue(obj As Object) As Boolean
        Dim item As ComplianceItemVM = obj

        Return item.FullName.ToLower.Contains(FilterString.ToLower) OrElse
                item.CreatedByUserName.ToLower.Contains(FilterString.ToLower) OrElse
                item.LicensePlate.ToLower.Contains(FilterString.ToLower)

    End Function
End Class
