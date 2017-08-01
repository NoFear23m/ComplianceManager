﻿Imports System.Windows.Controls
Imports System.Windows.Input
Imports SPS.ViewModel.Infrastructure
Imports System.Data.Entity
Imports System.Collections.ObjectModel

Public Class ComplianceItemDetailVM
    Inherits ViewModelBase


    Private _compItem As Model.CompliantItem
    Private _db As Context.CompContext

    Public Sub New()
        If _db Is Nothing Then _db = New Context.CompContext
        _compItem = New Model.CompliantItem

        AllAviableReasons = _db.Resons.Where(Function(d) d.IsDeleted = False).ToList
        AllAviableEntryTypes = _db.EntryTypes.Where(Function(d) d.IsDeleted = False).ToList
        AviableUsers = _db.Users.Where(Function(u) u.IsActive = True).ToList

        ComplianceHistory = New ObservableCollection(Of HistoryItemVM)
    End Sub

    Public Sub New(item As Model.CompliantItem)
        If _db Is Nothing Then _db = New Context.CompContext
        Reload(item)
    End Sub

    Private Sub Reload(item As Model.CompliantItem)

        _compItem = _db.ComplianceItems.Include(
            Function(i) i.ComplianceEntryType).Include("ComplianceHistory").Include("ComplianceHistory.Attachments").Include(Function(i2) i2.ComplianceReason).Where(Function(c) c.ID = item.ID).FirstOrDefault


        AllAviableReasons = _db.Resons.Where(Function(d) d.IsDeleted = False).ToList
            AllAviableEntryTypes = _db.EntryTypes.Where(Function(d) d.IsDeleted = False).ToList
            AviableUsers = _db.Users.Where(Function(u) u.IsActive = True).ToList

            ComplianceHistory = New ObservableCollection(Of HistoryItemVM)
            For Each hi As Model.HistoryItem In _compItem.ComplianceHistory
            ComplianceHistory.Add(New HistoryItemVM(hi, _db))
        Next



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


    Private _complianceHistory As ObservableCollection(Of HistoryItemVM)
    Public Property ComplianceHistory As ObservableCollection(Of HistoryItemVM)
        Get
            Return _complianceHistory
        End Get
        Set(value As ObservableCollection(Of HistoryItemVM))
            _complianceHistory = value
            RaisePropertyChanged("ComplianceHistory")
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

    Private _aviableUsers As List(Of Model.User)
    Public Property AviableUsers() As List(Of Model.User)
        Get
            Return _aviableUsers
        End Get
        Set(ByVal value As List(Of Model.User))
            _aviableUsers = value
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
        Dim win As New Windows.Window
        win.Title = "Reklamations-Detailsansicht"
        win.Width = 500
        win.Height = 400
        win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen
        win.DataContext = Me
        win.Content = New ContentPresenter With {.Content = Me, .DataContext = Me}
        If win.ShowDialog Then


            'RefreshViews()
        End If
    End Sub








    Private _addHistorItemCommand As ICommand
    Public Property AddHistoryItemCommand() As ICommand
        Get
            If _addHistorItemCommand Is Nothing Then _addHistorItemCommand = New RelayCommand(AddressOf AddHistoryItemCommand_Execute, AddressOf AddHistoryItemCommand_CanExecute)
            Return _addHistorItemCommand
        End Get
        Set(ByVal value As ICommand)
            _addHistorItemCommand = value
            RaisePropertyChanged("AddHistoryItemCommand")
        End Set
    End Property

    Private Function AddHistoryItemCommand_CanExecute(obj As Object) As Boolean
        Return True
    End Function

    Private Sub AddHistoryItemCommand_Execute(obj As Object)

        Dim win As New Windows.Window
        win.Title = "Neue Historie anlegen..."
        win.Width = 600
        win.Height = 350
        win.WindowStartupLocation = Windows.WindowStartupLocation.CenterScreen
        Dim newHistVm = New HistoryItemVM(New Model.HistoryItem, _db)
        If newHistVm.Attachments Is Nothing Then newHistVm.Attachments = New List(Of Model.ComplianteAttachment)

        win.DataContext = newHistVm
        win.Content = New ContentPresenter With {.Content = newHistVm}

        win.ShowDialog()


        Dim NewHist As Model.HistoryItem = newHistVm._historyItem
                NewHist.Title = newHistVm.Title
                NewHist.Description = newHistVm.Description
                NewHist.CreationDate = Now
                newHist.CreatedBy = Environment.UserName
                newHist.LastChange = Now
                newHist.LastEditedBy = Environment.UserName

        'For Each a As Model.ComplianteAttachment In newHistVm.Attachments
        '    If NewHist.Attachments.Where(Function(i) i.ID = a.ID).FirstOrDefault IsNot Nothing Then

        '    End If
        'Next


        Dim Comp As Model.CompliantItem = _db.ComplianceItems.Find(Me.ID)
        If Comp.ComplianceHistory Is Nothing Then Comp.ComplianceHistory = New List(Of Model.HistoryItem)
                Comp.ComplianceHistory.Add(NewHist)

        _db.SaveChanges()

        Reload(_compItem)
        For Each h As HistoryItemVM In Me.ComplianceHistory
            h.RefreshView()
        Next

    End Sub
End Class