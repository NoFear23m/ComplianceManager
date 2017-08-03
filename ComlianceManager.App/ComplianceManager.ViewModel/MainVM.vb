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

        If IsUserInDB() Then
            IsUserAdmin = IsCurrUserAdmin()
        End If


        RefreshViews()


    End Sub

    Public Function IsUserInDB() As Boolean
        Using Db As New Context.CompContext

            If Not Db.Users.Where(Function(u) u.UserName = Environment.UserName).Any Then

                Return False
            Else
                Return True
            End If
        End Using
    End Function


    Private IsUserAdmin As Boolean = False
    Public Function IsCurrUserAdmin() As Boolean
        Using Db As New Context.CompContext

            If Not Db.Users.Where(Function(u) u.UserName = Environment.UserName).First.IsAdmin Then

                Return False
            Else
                Return True
            End If
        End Using
    End Function


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

                'If newComItem.ComplianceHistory Is Nothing Then newComItem.ComplianceHistory = New List(Of Model.HistoryItem)
                'newComItem.ComplianceHistory.Add(New Model.HistoryItem With {.CreatedBy = Environment.UserName, .CreationDate = Now, .Title = "Neuanlage der Reklamation", .LastChange = Now, .LastEditedBy = Environment.UserName, .Description = " "})

                db.ComplianceItems.Add(newComItem)

                db.SaveChanges()
            End Using
            RefreshViews()
        End If

    End Sub





    Private _editUsers As ICommand
    Public Property EditUsers() As ICommand
        Get
            If _editUsers Is Nothing Then _editUsers = New RelayCommand(AddressOf EditUsers_Execute, AddressOf EditUsers_CanExecute)
            Return _editUsers
        End Get
        Set(ByVal value As ICommand)
            _editUsers = value
            RaisePropertyChanged("EditUsers")
        End Set
    End Property

    Private Function EditUsers_CanExecute(obj As Object) As Boolean
        Return IsUserAdmin
    End Function

    Private Sub EditUsers_Execute(obj As Object)
        Using db As New Context.CompContext
            Dim win As New Windows.Window
            win.Title = "Benutzer bearbeiten..."
            win.Width = 400
            win.Height = 200
            win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen

            db.Configuration.AutoDetectChangesEnabled = True
            win.DataContext = New EditUsersVM(db)
            win.Content = New ContentPresenter With {.Content = win.DataContext}

            win.ShowDialog()
            Debug.WriteLine(db.SaveChanges())

        End Using
    End Sub


    Private _editReasons As ICommand
    Public Property EditReasons() As ICommand
        Get
            If _editReasons Is Nothing Then _editReasons = New RelayCommand(AddressOf EditReasons_Execute, AddressOf EditReasons_CanExecute)
            Return _editReasons
        End Get
        Set(ByVal value As ICommand)
            _editReasons = value
            RaisePropertyChanged("EditReasons")
        End Set
    End Property

    Private Sub EditReasons_Execute(obj As Object)
        Using db As New Context.CompContext
            Dim win As New Windows.Window
            win.Title = "Reklamationursachen bearbeiten..."
            win.Width = 400
            win.Height = 300
            win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen

            db.Configuration.AutoDetectChangesEnabled = True
            win.DataContext = New EditreasonsVM(db)
            win.Content = New ContentPresenter With {.Content = win.DataContext}

            win.ShowDialog()
            Debug.WriteLine(db.SaveChanges())

        End Using
    End Sub

    Private Function EditReasons_CanExecute() As Object
        Return IsUserAdmin
    End Function






    Private _editEntryTypes As ICommand
    Public Property EditEntryTypes() As ICommand
        Get
            If _EditEntryTypes Is Nothing Then _EditEntryTypes = New RelayCommand(AddressOf EditEntryTypes_Execute, AddressOf EditEntryTypes_CanExecute)
            Return _editEntryTypes
        End Get
        Set(ByVal value As ICommand)
            _editEntryTypes = value
            RaisePropertyChanged("EditEntryTypes")
        End Set
    End Property

    Private Function EditEntryTypes_CanExecute(obj As Object) As Boolean
        Return IsUserAdmin
    End Function

    Private Sub EditEntryTypes_Execute(obj As Object)
        Using db As New Context.CompContext
            Dim win As New Windows.Window
            win.Title = "Reklamationsarten bearbeiten..."
            win.Width = 400
            win.Height = 300
            win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen

            db.Configuration.AutoDetectChangesEnabled = True
            win.DataContext = New EditEntryTypesVM(db)
            win.Content = New ContentPresenter With {.Content = win.DataContext}

            win.ShowDialog()
            Debug.WriteLine(db.SaveChanges())

        End Using
    End Sub





    Private _editSettings As ICommand
    Public Property EditSettings() As ICommand
        Get
            If _editSettings Is Nothing Then _editSettings = New RelayCommand(AddressOf EditSettings_Execute, AddressOf EditSettings_CanExecute)
            Return _editSettings
        End Get
        Set(ByVal value As ICommand)
            _editSettings = value
            RaisePropertyChanged("EditSettings")
        End Set
    End Property

    Private Function EditSettings_CanExecute(obj As Object) As Boolean
        Return IsUserAdmin
    End Function

    Private Sub EditSettings_Execute(obj As Object)
        Using db As New Context.CompContext
            Dim win As New Windows.Window
            win.Title = "Einstellungen bearbeiten..."
            win.Width = 400
            win.Height = 300
            win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen

            db.Configuration.AutoDetectChangesEnabled = True
            win.DataContext = New EditSettingsVM(db)
            win.Content = New ContentPresenter With {.Content = win.DataContext}

            win.ShowDialog()
            Debug.WriteLine(db.SaveChanges())

        End Using
    End Sub



#End Region


End Class
