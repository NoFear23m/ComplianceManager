Imports System.Data.Entity
Imports System.Windows.Controls
Imports System.Windows.Input
Imports SPS.ViewModel.Infrastructure


Public Class MainVM
    Inherits ViewModelBase





    Public Sub New()



        Using settDb As New Context.CompContext
            AllSettings = settDb.Settings.ToList

        End Using



        RefreshViews()


    End Sub

    Friend Sub RefreshViews()
        ShortInfoVm = New ShortInfoVM
        ComplianceItemsVm = New ComplianteItemsVM
        DetailComplianceInfoVm = New ComplianceItemVM
        StatusVm = New StatusVM With {.UserName = Environment.UserName}
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
        Dim newComItem As New Model.CompliantItem
        Dim newComItemVM As New NewComplianceItemVM(newComItem)


        Dim win As New Windows.Window
        win.Title = "Neue Reklamation anlegen..."
        win.Width = 500
        win.Height = 250
        win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen
        win.DataContext = newComItemVM
        win.Content = New ContentPresenter With {.Content = newComItemVM, .DataContext = newComItemVM}
        If win.ShowDialog Then
            Using db As New Context.CompContext
                newComItem.ComplianceReason = db.Resons.Find(newComItem.ComplianceReason.ID)
                newComItem.ComplianceEntryType = db.EntryTypes.Find(newComItem.ComplianceEntryType.ID)
                newComItem.CreationDate = Now
                newComItem.CreatedByUserName = Environment.UserName
                newComItem.LastChange = Now
                newComItem.LastChangeByUserName = Environment.UserName

                db.ComplianceItems.Add(newComItem)
                db.SaveChanges()
            End Using
            RefreshViews()
        End If

    End Sub


#End Region


End Class
