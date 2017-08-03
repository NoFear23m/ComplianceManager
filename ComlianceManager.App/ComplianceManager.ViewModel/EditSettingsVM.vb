Imports System.ComponentModel
Imports System.Data.Entity
Imports SPS.ViewModel.Infrastructure

Public Class EditSettingsVM
    Inherits ViewModelBase

    Private _users As BindingList(Of Model.Setting)


    Public Sub New()



    End Sub

    Public Sub New(db As Context.CompContext)
        db.Users.ToList
        _users = db.Settings.Local.ToBindingList


    End Sub


    Public Property Settings As BindingList(Of Model.Setting)
        Get
            Return _users
        End Get
        Set(value As BindingList(Of Model.Setting))
            _users = value
            RaisePropertyChanged("Settings")
        End Set
    End Property


End Class
