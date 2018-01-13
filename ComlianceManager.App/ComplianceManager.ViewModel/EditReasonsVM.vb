Imports System.ComponentModel
Imports System.Data.Entity
Imports SPS.ViewModel.Infrastructure

Public Class EditReasonsVM
    Inherits ViewModelBase

    Private _reasons As BindingList(Of Model.Reason)


    Public Sub New()



    End Sub

    Public Sub New(db As Context.CompContext)
        db.Resons.ToList
        _reasons = db.Resons.Local.ToBindingList


    End Sub


    Public Property Reasons As BindingList(Of Model.Reason)
        Get
            Return _reasons
        End Get
        Set(value As BindingList(Of Model.Reason))
            _reasons = value
            RaisePropertyChanged("Reasons")
        End Set
    End Property


End Class
