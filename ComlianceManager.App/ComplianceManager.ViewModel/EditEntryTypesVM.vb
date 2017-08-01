Imports System.ComponentModel
Imports System.Data.Entity
Imports SPS.ViewModel.Infrastructure

Public Class EditEntryTypesVM
    Inherits ViewModelBase

    Private _types As BindingList(Of Model.EntryType)


    Public Sub New()



    End Sub

    Public Sub New(db As Context.CompContext)
        db.EntryTypes.ToList
        _types = db.EntryTypes.Local.ToBindingList


    End Sub


    Public Property Types As BindingList(Of Model.EntryType)
        Get
            Return _types
        End Get
        Set(value As BindingList(Of Model.EntryType))
            _types = value
            RaisePropertyChanged("Types")
        End Set
    End Property


End Class
