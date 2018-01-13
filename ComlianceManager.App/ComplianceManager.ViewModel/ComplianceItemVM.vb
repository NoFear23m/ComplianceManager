Imports System.ComponentModel
Imports System.Windows.Controls
Imports System.Windows.Input
Imports SPS.ViewModel.Infrastructure

Public Class ComplianceItemVM
    Inherits ViewModelBase
    Implements IDataErrorInfo


    Private _compItem As Model.CompliantItem
    Private _db As Context.CompContext
    Private _listVm As ComplianteItemsVM



    Public Sub New()

        _compItem = New Model.CompliantItem
        Using db As New Context.CompContext
            AllAviableReasons = db.Resons.Where(Function(d) d.IsDeleted = False).ToList
            AllAviableEntryTypes = db.EntryTypes.Where(Function(d) d.IsDeleted = False).ToList
        End Using
    End Sub

    Public Sub New(item As Model.CompliantItem, db As Context.CompContext, complListVm As ComplianteItemsVM)
        _compItem = item
        _listVm = complListVm
        _db = db
        AllAviableReasons = _db.Resons.Where(Function(d) d.IsDeleted = False).ToList
        AllAviableEntryTypes = _db.EntryTypes.Where(Function(d) d.IsDeleted = False).ToList

    End Sub

    Public Sub New(item As Model.CompliantItem, aviableReasons As List(Of Model.Reason), aviableEntryTypes As List(Of Model.EntryType))
        _compItem = item
        AllAviableEntryTypes = aviableEntryTypes
        AllAviableReasons = aviableReasons
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
            RaisePropertyChanged("FullName")
        End Set
    End Property


    Public Property CustomerLastName As String
        Get
            Return _compItem.CustomerLastName
        End Get
        Set(value As String)
            _compItem.CustomerLastName = value
            RaisePropertyChanged("CustomerLastName")
            RaisePropertyChanged("FullName")
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

    Public ReadOnly Property FullName As String
        Get
            Return _compItem.CustomerFullName
        End Get
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


    Public Property Deleted As Boolean
        Get
            Return _compItem.IsDeleted
        End Get
        Set(value As Boolean)
            _compItem.IsDeleted = value
            RaisePropertyChanged("Deleted")
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


    Public Property FinishedFrom As String
        Get
            Return _compItem.FinishedFrom

        End Get
        Set(value As String)
            _compItem.FinishedFrom = value
            RaisePropertyChanged("FinishedFrom")
        End Set
    End Property


    Public Property CreationDate As DateTime
        Get
            Return _compItem.CreationDate
        End Get
        Set(value As DateTime)
            _compItem.CreationDate = value
        End Set
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


    Private _isMarked As Boolean
    Public Property IsMarked() As Boolean
        Get
            Return _isMarked
        End Get
        Set(ByVal value As Boolean)
            _isMarked = value
            RaisePropertyChanged("IsMarked")
        End Set
    End Property




    Private _allAviableReasons As List(Of Model.Reason)
    Public Property AllAviableReasons() As List(Of Model.Reason)
        Get
            Return _allAviableReasons
        End Get
        Set(ByVal value As List(Of Model.Reason))
            _allAviableReasons = value
            RaisePropertyChanged("AllAviableReasons")
        End Set
    End Property

    Private _allAviableEntryTypes As List(Of Model.EntryType)
    Public Property AllAviableEntryTypes() As List(Of Model.EntryType)
        Get
            Return _allAviableEntryTypes
        End Get
        Set(ByVal value As List(Of Model.EntryType))
            _allAviableEntryTypes = value
            RaisePropertyChanged("AllAviableEntryTypes")
        End Set
    End Property



    Private _showdetailsCommand As ICommand
    Public Property ShowDetailsCommand() As ICommand
        Get
            If _showdetailsCommand Is Nothing Then _showdetailsCommand = New RelayCommand(AddressOf ShowDetailsCommand_Execute, AddressOf ShowDetailsCommand_canExecute)
            Return _showdetailsCommand
        End Get
        Set(ByVal value As ICommand)
            _showdetailsCommand = value
            RaisePropertyChanged("ShowDetailsCommand")
        End Set
    End Property

    Private Function ShowDetailsCommand_canExecute(obj As Object) As Boolean
        Return True
    End Function

    Private Sub ShowDetailsCommand_Execute(obj As Object)
        Dim win As New MyWindows
        win.Title = "Reklamations-Detailsansicht"
        win.DataContext = New ComplianceItemDetailVM(_compItem)
        win.Content = New ContentPresenter With {.Content = win.DataContext, .DataContext = win.DataContext}
        win.ShowDialog()



        If DirectCast(win.DataContext, ComplianceItemDetailVM).Save.CanExecute(Nothing) Then DirectCast(win.DataContext, ComplianceItemDetailVM).Save.Execute(Nothing)
        DirectCast(win.DataContext, ComplianceItemDetailVM).SaveBackWindowSettings()
        RefreshViews()
        _listVm.Load()
    End Sub

    Private Sub RefreshViews()

        ' _compItem = _db.ComplianceItems.Find(_compItem.ID)

        RaisePropertyChanged("CustomerFirstName")
        RaisePropertyChanged("FullName")
        RaisePropertyChanged("CustomerLastName")
        RaisePropertyChanged("FullName")
        RaisePropertyChanged("CustomerNumber")
        RaisePropertyChanged("ComplianceBrand")
        RaisePropertyChanged("LicensePlate")
        RaisePropertyChanged("ComplianceReason")
        RaisePropertyChanged("ComplianceEntryType")
        RaisePropertyChanged("FinishedAt")
        _listVm._mainVM.RefreshViews()
    End Sub








#Region "Validation"


    Public Function IsValid() As Boolean
        Dim ret As List(Of DataAnnotations.ValidationResult) = CheckForValidationErrors()
        Return ret Is Nothing OrElse ret.Count = 0
    End Function



    Public ReadOnly Property ValidationErrors As List(Of DataAnnotations.ValidationResult)
        Get
            Dim ret As List(Of DataAnnotations.ValidationResult) = CheckForValidationErrors()
            RaisePropertyChanged("IsValid")
            Return ret
        End Get
    End Property


    Private Function CheckForValidationErrors() As List(Of DataAnnotations.ValidationResult)
        If _compItem Is Nothing Then Return New List(Of DataAnnotations.ValidationResult)
        Dim ValRet As New List(Of DataAnnotations.ValidationResult)

        ValRet = DirectCast(_compItem, Model.CompliantItem).Validate.ToList


        Return ValRet
    End Function


    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Return "Hilfe" 'TODO
        End Get
    End Property

    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            Dim valRes As DataAnnotations.ValidationResult = ValidationErrors.Where(Function(v) v.MemberNames.Contains(columnName) = True).FirstOrDefault
            If valRes Is Nothing Then

                Return Nothing
            Else
                Return valRes.ErrorMessage
            End If
            CommandManager.InvalidateRequerySuggested()

        End Get
    End Property


#End Region






End Class
