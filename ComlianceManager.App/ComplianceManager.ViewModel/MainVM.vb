Imports System.Data.Entity
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Input
Imports SPS.ViewModel.Infrastructure


Public Class MainVM
    Inherits ViewModelBase





    Public Sub New()




        Using settDb As New Context.CompContext
            AllSettings = settDb.Settings.ToList

            If settDb.Users.Any = False Then
                settDb.Users.Add(New Model.User() With {.FullName = "Unknown", .IsActive = True, .IsAdmin = True, .IsFirstLogin = False, .UserName = Environment.UserName})
                settDb.SaveChanges()
            End If



        End Using



        If IsUserInDB() Then
            IsUserAdmin = IsCurrUserAdmin()

            Using db As New Context.CompContext
                Dim users = db.Users.Include("UserSettings").ToList
                For Each u As Model.User In users
                    If u.UserSettings.Where(Function(s) s.Title = "GridHidedColumns").FirstOrDefault Is Nothing Then
                        u.UserSettings.Add(New Model.UserSetting() With {.Title = "GridHidedColumns", .Value = "FallNr;KdNr;Abgeschl. am;Abgeschl. durch;Zul. bearb. von"})
                    End If
                    If u.UserSettings.Where(Function(s) s.Title = "GridSorting").FirstOrDefault Is Nothing Then
                        u.UserSettings.Add(New Model.UserSetting() With {.Title = "GridSorting", .Value = ""})
                    End If
                    If u.UserSettings.Where(Function(s) s.Title = "GridColumnsOrder").FirstOrDefault Is Nothing Then
                        u.UserSettings.Add(New Model.UserSetting() With {.Title = "GridColumnsOrder", .Value = ""})
                    End If
                    If u.UserSettings.Where(Function(s) s.Title = "GridColumnsWidth").FirstOrDefault Is Nothing Then
                        u.UserSettings.Add(New Model.UserSetting() With {.Title = "GridColumnsWidth", .Value = ""})
                    End If
                    If u.UserSettings.Where(Function(s) s.Title = "MainWindow_Size").FirstOrDefault Is Nothing Then
                        u.UserSettings.Add(New Model.UserSetting() With {.Title = "MainWindow_Size", .Value = "600;800"})
                    End If
                    If u.UserSettings.Where(Function(s) s.Title = "MainWindow_Position").FirstOrDefault Is Nothing Then
                        u.UserSettings.Add(New Model.UserSetting() With {.Title = "MainWindow_Position", .Value = "300;300"})
                    End If
                    If u.UserSettings.Where(Function(s) s.Title = "ComplaintDetailWindow_Size").FirstOrDefault Is Nothing Then
                        u.UserSettings.Add(New Model.UserSetting() With {.Title = "ComplaintDetailWindow_Size", .Value = "600;800"})
                    End If
                    If u.UserSettings.Where(Function(s) s.Title = "ComplaintDetailWindow_Position").FirstOrDefault Is Nothing Then
                        u.UserSettings.Add(New Model.UserSetting() With {.Title = "ComplaintDetailWindow_Position", .Value = "300;300"})
                    End If

                Next

                db.SaveChanges()



                Dim winName As String = "MainWindow"
                Me.WindowsPosition = New WindowsPosition(db, winName)
                Me.WindowsSize = New WindowsSize(db, winName)
            End Using

        End If





        RefreshViews()


    End Sub

    Public Sub IsFisrtLogin()
        Using db As New Context.CompContext
            If db.Users.Where(Function(u) u.UserName = Environment.UserName).First.IsFirstLogin Then
                If MessageBox.Show("Hallo und Willkommen" & vbNewLine & "Du hast dich zum ersten mal angemeldet. Besuche doch das WIKI um dich mit dem Programm vertraut zu machen. Das WIKI findest du im Menupunkt Info.", "Willkommen beim Compliant-Manager", MessageBoxButton.OK) = MsgBoxResult.Ok Then
                    db.Users.Where(Function(u) u.UserName = Environment.UserName).First.IsFirstLogin = False
                    db.SaveChanges()
                End If
            End If
        End Using
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
            If Not Db.Users.Where(Function(u) u.UserName = Environment.UserName).Any Then Return False
            If Not Db.Users.Where(Function(u) u.UserName = Environment.UserName).First.IsAdmin Then

                Return False
            Else
                Return True
            End If
        End Using
    End Function


    Friend Sub RefreshViews()
        ShortInfoVm = New ShortInfoVM(Me)
        If ComplianceItemsVm Is Nothing Then
            ComplianceItemsVm = New ComplianteItemsVM(Me)

        Else
            ComplianceItemsVm.SaveSortingString()
            ComplianceItemsVm.Load()
        End If


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





#Region "Windows"




    Private _windowsSize As WindowsSize
    Public Property WindowsSize() As WindowsSize
        Get
            Return _windowsSize
        End Get
        Set(ByVal value As WindowsSize)
            _windowsSize = value
            RaisePropertyChanged("WindowsSize")
        End Set
    End Property

    Private _windowsPosition As WindowsPosition
    Public Property WindowsPosition() As WindowsPosition
        Get
            Return _windowsPosition
        End Get
        Set(ByVal value As WindowsPosition)
            _windowsPosition = value
            RaisePropertyChanged("WindowsPosition")
        End Set
    End Property


    Public Sub SaveBackWindowSettings()
        Using db As New Context.CompContext
            Dim currUser As Model.User = db.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).First
            currUser.UserSettings.Where(Function(s) s.Title = "MainWindow_Size").FirstOrDefault.Value = WindowsSize.ToString
            currUser.UserSettings.Where(Function(s) s.Title = "MainWindow_Position").FirstOrDefault.Value = WindowsPosition.ToString

            Dim res As Integer = db.SaveChanges()
            If res < 1 Then
                MsgBox("Fehler beim speichern der Fensterposition oder des Fenstergröße")
            End If
            Debug.WriteLine(res)
        End Using
    End Sub

#End Region











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


        Dim win As New MahApps.Metro.Controls.MetroWindow

        win.Title = "Neue Reklamation anlegen..."
        win.Width = 500
        win.Height = 300
        win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen
        win.DataContext = newComItemVM
        win.Content = New ContentPresenter With {.Content = newComItemVM, .DataContext = newComItemVM}
        If win.ShowDialog Then
            Using db As New Context.CompContext
                newComItem.ComplianceReason = db.Resons.Find(newComItem.ComplianceReason.ID)
                newComItem.ComplianceEntryType = db.EntryTypes.Find(newComItem.ComplianceEntryType.ID)
                'newComItem.CreationDate = Now
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
            Dim win As New MahApps.Metro.Controls.MetroWindow
            win.Title = "Benutzer bearbeiten..."
            win.Width = 400
            win.Height = 200
            win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen

            db.Configuration.AutoDetectChangesEnabled = True
            win.DataContext = New EditUsersVM(db)
            win.Content = New ContentPresenter With {.Content = win.DataContext}

            win.ShowDialog()

            db.SaveChanges()

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
            Dim win As New MahApps.Metro.Controls.MetroWindow
            win.Title = "Reklamationursachen bearbeiten..."
            win.Width = 400
            win.Height = 300
            win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen

            db.Configuration.AutoDetectChangesEnabled = True
            win.DataContext = New EditReasonsVM(db)
            win.Content = New ContentPresenter With {.Content = win.DataContext}

            win.ShowDialog()
            db.SaveChanges()

        End Using
    End Sub

    Private Function EditReasons_CanExecute() As Object
        Return IsUserAdmin
    End Function






    Private _editEntryTypes As ICommand
    Public Property EditEntryTypes() As ICommand
        Get
            If _editEntryTypes Is Nothing Then _editEntryTypes = New RelayCommand(AddressOf EditEntryTypes_Execute, AddressOf EditEntryTypes_CanExecute)
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
            Dim win As New MahApps.Metro.Controls.MetroWindow
            win.Title = "Reklamationsarten bearbeiten..."
            win.Width = 400
            win.Height = 300
            win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen

            db.Configuration.AutoDetectChangesEnabled = True
            win.DataContext = New EditEntryTypesVM(db)
            win.Content = New ContentPresenter With {.Content = win.DataContext}

            win.ShowDialog()
            db.SaveChanges()

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
            Dim win As New MahApps.Metro.Controls.MetroWindow
            win.Title = "Einstellungen bearbeiten..."
            win.Width = 800
            win.Height = 300
            win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen

            db.Configuration.AutoDetectChangesEnabled = True
            win.DataContext = New EditSettingsVM(db)
            win.Content = New ContentPresenter With {.Content = win.DataContext}

            win.ShowDialog()
            db.SaveChanges()

        End Using
    End Sub



#End Region


End Class
