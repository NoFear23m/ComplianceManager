﻿Imports System.ComponentModel
Imports System.Windows.Input
Imports SPS.ViewModel.Infrastructure

Public Class NewComplianceItemVM
    Inherits ViewModelBase
    Implements IDataErrorInfo



    Public _compItem As Model.CompliantItem


    Public Sub New()
        _compItem = New Model.CompliantItem
        Init()
    End Sub

    Public Sub New(item As Model.CompliantItem)
        _compItem = item
        Init()
    End Sub

    Private Sub Init()
        Using db As New Context.CompContext
            _partnerNetUrl = db.Settings.Where(Function(s) s.Key = "ParnterNetURL").FirstOrDefault.Value
            AllAviableReasons = db.Resons.Where(Function(d) d.IsDeleted = False).ToList
            AllAviableEntryTypes = db.EntryTypes.Where(Function(d) d.IsDeleted = False).ToList
            DownloadDestFolder = db.Settings.Where(Function(s) s.Key = "AttachmentsPath").First.Value
        End Using
        ComplianceReason = AllAviableReasons.FirstOrDefault
        ComplianceEntryType = AllAviableEntryTypes.FirstOrDefault
        ComplianceBrand = Model.CompliantItem.enuBrand.VW

    End Sub


    Private _downloadDestFolder As String
    Public Property DownloadDestFolder() As String
        Get
            Return _downloadDestFolder
        End Get
        Set(ByVal value As String)
            _downloadDestFolder = value
        End Set
    End Property

    Private _partnerNetUrl As String
    Public Property PertnerNetUrl() As String
        Get
            Return _partnerNetUrl
        End Get
        Set(ByVal value As String)
            _partnerNetUrl = value
            RaisePropertyChanged("PertnerNetUrl")
        End Set
    End Property

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
        End Set
    End Property


    Public Property CustomerLastName As String
        Get
            Return _compItem.CustomerLastName
        End Get
        Set(value As String)
            _compItem.CustomerLastName = value
            RaisePropertyChanged("CustomerLastName")
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


    Public Property CreationDate As Date
        Get
            Return _compItem.CreationDate
        End Get
        Set(value As Date)
            _compItem.CreationDate = value
            RaisePropertyChanged("CreationDate")
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












#Region "Validation"
    Public checkValidation As Boolean = True

    Public Function IsValid() As Boolean
        If Not checkValidation Then Return True
        Dim ret As List(Of DataAnnotations.ValidationResult) = CheckForValidationErrors()
        Return ret Is Nothing OrElse ret.Count = 0
    End Function



    Public ReadOnly Property ValidationErrors As List(Of DataAnnotations.ValidationResult)
        Get
            Dim ret As List(Of DataAnnotations.ValidationResult) = CheckForValidationErrors()
            RaisePropertyChanged("IsValid")
            'RaisePropertyChanged("CustomerFirstName")
            'RaisePropertyChanged("CustomerLastName")
            Return ret
        End Get
    End Property


    Private Function CheckForValidationErrors() As List(Of DataAnnotations.ValidationResult)
        If _compItem Is Nothing Or Not checkValidation Then Return New List(Of DataAnnotations.ValidationResult)
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
            If Not checkValidation Then Return Nothing
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
