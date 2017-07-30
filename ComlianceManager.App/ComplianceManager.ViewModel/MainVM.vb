Imports System.Data.Entity
Imports System.Windows.Input
Imports SPS.ViewModel.Infrastructure


Public Class MainVM
    Inherits ViewModelBase


    Public Sub New()

        Using settDb As New Context.CompContext
            AllSettings = settDb.Settings.ToList

        End Using



        ShortInfoVm = New ShortInfoVM
        ComplianceItemsVm = New ComplianteItemsVM
        DetailComplianceInfoVm = New ComplianceItemVM
        StatusVm = New StatusVM


    End Sub


    Friend Property AllSettings As List(Of Model.Setting)




    Private _shortInfoVm As ShortInfoVM
    Public Property ShortInfoVm() As ShortInfoVM
        Get
            Return _shortInfoVm
        End Get
        Set(ByVal value As ShortInfoVM)
            _shortInfoVm = value
            RaisePropertyChanged("ShortInfoVm")
        End Set
    End Property


    Private _complianceItemsVm As ComplianteItemsVM
    Public Property ComplianceItemsVm() As ComplianteItemsVM
        Get
            Return _complianceItemsVm
        End Get
        Set(ByVal value As ComplianteItemsVM)
            _complianceItemsVm = value
            RaisePropertyChanged("ComplianceItemsVm")
        End Set
    End Property


    Private _detailComplianceInfoVm As ComplianceItemVM
    Public Property DetailComplianceInfoVm() As ComplianceItemVM
        Get
            Return _detailComplianceInfoVm
        End Get
        Set(ByVal value As ComplianceItemVM)
            _detailComplianceInfoVm = value
            RaisePropertyChanged("DetailComplianceInfoVm")
        End Set
    End Property


    Private _statusVm As StatusVM
    Public Property StatusVm() As StatusVM
        Get
            Return _statusVm
        End Get
        Set(ByVal value As StatusVM)
            _statusVm = value
            RaisePropertyChanged("StatusVm")
        End Set
    End Property







#Region "Commands"


    Private _createNewItemCommand As ICommand
    Public Property CreateNewItemCommand() As ICommand
        Get
            If _createNewItemCommand Is Nothing Then _createNewItemCommand = New RelayCommand(AddressOf CreateNewItemCommand_Execute, AddressOf CreateNewItemCommand_CanExecute)
            Return _createNewItemCommand
        End Get
        Set(ByVal value As ICommand)
            _createNewItemCommand = value
            RaisePropertyChanged("CreateNewItemCommand")
        End Set
    End Property

    Private Function CreateNewItemCommand_CanExecute(obj As Object) As Boolean
        Return True
    End Function

    Private Sub CreateNewItemCommand_Execute(obj As Object)
        MsgBox("Neues Item")
    End Sub


#End Region


End Class
