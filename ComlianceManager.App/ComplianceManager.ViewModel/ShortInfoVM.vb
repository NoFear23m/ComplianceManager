Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Windows.Input
Imports SPS.ViewModel.Infrastructure

Public Class ShortInfoVM
    Inherits ViewModelBase


    Private _mainVm As MainVM


    Public Sub New()
        AviableComplianceItemsCount = 35
        OpenComplianceItems = 8
        LatestAddedComplianceItem = New Model.CompliantItem() With {.CustomerFirstName = "Max", .CustomerLastName = "Mustermann", .ComplianceBrand = Model.CompliantItem.enuBrand.Seat,
            .ComplianceEntryType = New Model.EntryType() With {.EntryTitle = "Garantie", .EntryDescription = ""}, .ComplianceReason = New Model.Reason() With {.ReasonTitle = "TestReason", .ReasonDescription = ""} _
            , .CreatedByUserName = "testUser", .CreationDate = Now, .CustomerNumber = 12345, .LastChange = Now, .LastChangeByUserName = "testUSer2"}

    End Sub


    Public Sub New(mainVM As MainVM)
        If DesignerProperties.GetIsInDesignMode(New Windows.DependencyObject) Then
            AviableComplianceItemsCount = 35
            OpenComplianceItems = 8
            LatestAddedComplianceItem = New Model.CompliantItem() With {.CustomerFirstName = "Max", .CustomerLastName = "Mustermann", .ComplianceBrand = Model.CompliantItem.enuBrand.Seat,
                .ComplianceEntryType = New Model.EntryType() With {.EntryTitle = "Garantie", .EntryDescription = ""}, .ComplianceReason = New Model.Reason() With {.ReasonTitle = "TestReason", .ReasonDescription = ""} _
                , .CreatedByUserName = "testUser", .CreationDate = Now, .CustomerNumber = 12345, .LastChange = Now, .LastChangeByUserName = "testUSer2"}
        Else
            _mainVm = mainVM
            Using db As New Context.CompContext
                LatestAddedComplianceItem = db.ComplianceItems.Where(Function(d) d.IsDeleted = False).OrderByDescending(Function(o) o.CreationDate).FirstOrDefault
                LatestChangedComplianceItem = db.ComplianceItems.Where(Function(d) d.IsDeleted = False).OrderByDescending(Function(o) o.LastChange).FirstOrDefault
                OpenComplianceItems = db.ComplianceItems.Where(Function(d) d.IsDeleted = False).Where(Function(o) o.FinishedAt Is Nothing).Count
                LastThreeComplianceItems = New ObservableCollection(Of Model.CompliantItem)(db.ComplianceItems.Where(Function(c) c.IsDeleted = False).OrderByDescending(Function(o) o.CreationDate).Take(3).ToList)
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



    Private _ShowOpenedCommand As ICommand
    Public Property ShowOpenedCommand() As ICommand
        Get
            If _ShowOpenedCommand Is Nothing Then _ShowOpenedCommand = New RelayCommand(AddressOf ShowOpenedCommand_Execute, AddressOf ShowOpenedCommand_CanExecute)
            Return _ShowOpenedCommand
        End Get
        Set(ByVal value As ICommand)
            _ShowOpenedCommand = value
        End Set
    End Property

    Private Function ShowOpenedCommand_CanExecute(obj As Object) As Boolean
        Return OpenComplianceItems > 0
    End Function

    Private Sub ShowOpenedCommand_Execute(obj As Object)
        _mainVm.ComplianceItemsVm.ShowOnlyOpenItems = True
        For Each c As ComplianceItemVM In _mainVm.ComplianceItemsVm.ComplianceItems
            c.IsMarked = False
            If c.FinishedAt Is Nothing Then
                c.IsMarked = True


            End If
        Next
    End Sub





    Private _ShowAllCommand As ICommand
    Public Property ShowAllCommand() As ICommand
        Get
            If _ShowAllCommand Is Nothing Then _ShowAllCommand = New RelayCommand(AddressOf ShowAllCommand_Execute, AddressOf ShowAllCommand_CanExecute)
            Return _ShowAllCommand
        End Get
        Set(ByVal value As ICommand)
            _ShowAllCommand = value
            RaisePropertyChanged("ShowAllCommand")
        End Set
    End Property

    Private Function ShowAllCommand_CanExecute(obj As Object) As Boolean
        Return AviableComplianceItemsCount > 0
    End Function

    Private Sub ShowAllCommand_Execute(obj As Object)
        _mainVm.ComplianceItemsVm.ShowOnlyOpenItems = False
        For Each c As ComplianceItemVM In _mainVm.ComplianceItemsVm.ComplianceItems
            c.IsMarked = False
            If c.Deleted = False Then
                c.IsMarked = True
            End If
        Next
    End Sub



    Private _OpenLastAdded As ICommand
    Public Property OpenLastAdded() As ICommand
        Get
            If _OpenLastAdded Is Nothing Then _OpenLastAdded = New RelayCommand(AddressOf OpenLastAdded_Execute, AddressOf OpenLastAdded_CanExecute)
            Return _OpenLastAdded
        End Get
        Set(ByVal value As ICommand)
            _OpenLastAdded = value
            RaisePropertyChanged("OpenLastAdded")
        End Set
    End Property

    Private Function OpenLastAdded_CanExecute(obj As Object) As Boolean
        Return LatestAddedComplianceItem IsNot Nothing
    End Function

    Private Sub OpenLastAdded_Execute(obj As Object)
        Dim ci As New ComplianceItemVM(LatestAddedComplianceItem, _mainVm.ComplianceItemsVm._context, _mainVm.ComplianceItemsVm)
        ci.ShowDetailsCommand.Execute(Nothing)
    End Sub


    Private _OpenLastChanged As ICommand
    Public Property OpenLastChanged() As ICommand
        Get
            If _OpenLastChanged Is Nothing Then _OpenLastChanged = New RelayCommand(AddressOf OpenLastChanged_Execute, AddressOf OpenLastChanged_CanExecute)
            Return _OpenLastChanged
        End Get
        Set(ByVal value As ICommand)
            _OpenLastChanged = value
            RaisePropertyChanged("OpenLastChanged")
        End Set
    End Property

    Private Function OpenLastChanged_CanExecute(obj As Object)
        Return LatestChangedComplianceItem IsNot Nothing
    End Function

    Private Sub OpenLastChanged_Execute(obj As Object)
        Dim ci As New ComplianceItemVM(LatestChangedComplianceItem, _mainVm.ComplianceItemsVm._context, _mainVm.ComplianceItemsVm)
        ci.ShowDetailsCommand.Execute(Nothing)
    End Sub




    Private _OpenAITem As ICommand
    Public Property OpenAITem() As ICommand
        Get
            If _OpenAITem Is Nothing Then _OpenAITem = New RelayCommand(AddressOf OpenAITem_Execute, AddressOf OpenAITem_CanExecute)
            Return _OpenAITem
        End Get
        Set(ByVal value As ICommand)
            _OpenAITem = value
            RaisePropertyChanged("OpenAITem")
        End Set
    End Property

    Private Function OpenAITem_CanExecute(obj As Object) As Boolean
        Return obj IsNot Nothing
    End Function

    Private Sub OpenAITem_Execute(obj As Object)
        Dim ci As New ComplianceItemVM(obj, _mainVm.ComplianceItemsVm._context, _mainVm.ComplianceItemsVm)
        ci.ShowDetailsCommand.Execute(Nothing)
    End Sub
End Class
