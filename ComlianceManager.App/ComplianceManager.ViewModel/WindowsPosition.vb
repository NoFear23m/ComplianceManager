Imports System.Windows

Public Class WindowsPosition
    Inherits SPS.ViewModel.Infrastructure.ViewModelBase


    Public Sub New()
        Me.Left = 300
        Me.Top = 300
    End Sub

    Public Sub New(left As Double, top As Double)
        Me.Left = left
        Me.Top = top
    End Sub

    Public Sub New(winSize As Point)
        Me.Left = winSize.X
        Me.Top = winSize.Y
    End Sub

    Public Sub New(context As Context.CompContext, windowName As String)
        Dim currUser As Model.User = context.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).FirstOrDefault
        Dim sett As Model.UserSetting = currUser.UserSettings.Where(Function(s) s.Title = windowName & "_Position").FirstOrDefault
        If sett Is Nothing Then
            Top = 300
            Left = 300
        Else
            Top = Split(sett.Value, ";")(0)
            Left = Split(sett.Value, ";")(1)
            State = Split(sett.Value, ";")(2)
        End If
    End Sub


    Private _left As Double
    Public Property Left() As Double
        Get
            Return _left
        End Get
        Set(ByVal value As Double)
            _left = value
            RaisePropertyChanged("Left")
        End Set
    End Property


    Private _top As Double
    Public Property Top() As Double
        Get
            Return _top
        End Get
        Set(ByVal value As Double)
            _top = value
            RaisePropertyChanged("Top")
        End Set
    End Property


    Private _state As String = "Normal"
    Public Property State() As String
        Get
            Return _state
        End Get
        Set(ByVal value As String)
            _state = value
            RaisePropertyChanged("State")
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return String.Format("{0};{1};{2}", Top, Left, State.ToString)
    End Function
End Class
