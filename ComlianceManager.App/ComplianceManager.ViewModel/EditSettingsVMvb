Imports System.ComponentModel
Imports System.Data.Entity
Imports SPS.ViewModel.Infrastructure

Public Class EditUsersVM
    Inherits ViewModelBase

    Private _users As BindingList(Of Model.User)


    Public Sub New()



    End Sub

    Public Sub New(db As Context.CompContext)
        db.Users.ToList
        _users = db.Users.Local.ToBindingList


    End Sub


    Public Property Users As BindingList(Of Model.User)
        Get
            Return _users
        End Get
        Set(value As BindingList(Of Model.User))
            _users = value
            RaisePropertyChanged("Users")
        End Set
    End Property


End Class
