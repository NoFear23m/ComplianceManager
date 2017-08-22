Imports SPS.ViewModel.Infrastructure

Public Class StatusVM
Inherits ViewModelBase


    Public Sub New()
        Me.UserName = "Unknown"
        Me.StatusMessage = "Initialized version " & My.Application.Info.Version.ToString
    End Sub



    Private _userName As String
    Public Property UserName() As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
            RaisePropertyChanged("UserName")
        End Set
    End Property


    Private _statusMessage As String
    Public Property StatusMessage() As String
        Get
            Return _statusMessage
        End Get
        Set(ByVal value As String)
            _statusMessage = value
            RaisePropertyChanged("StatusMessage")
        End Set
    End Property





End Class
