Public Class WindowsSize
    Inherits SPS.ViewModel.Infrastructure.ViewModelBase


    Public Sub New()
        Width = 300
        Height = 300
    End Sub

    Public Sub New(width As Double, height As Double)
        Me.Width = width
        Me.Height = height
    End Sub

    Public Sub New(context As Context.CompContext, windowName As String)
        Dim currUser As Model.User = context.Users.Include("UserSettings").Where(Function(u) u.UserName = Environment.UserName).FirstOrDefault
        Dim sett As Model.UserSetting = currUser.UserSettings.Where(Function(s) s.Title = windowName & "_Size").FirstOrDefault
        If sett Is Nothing Then
            Width = 300
            Height = 300
        Else
            Width = Split(sett.Value, ";")(0)
            Height = Split(sett.Value, ";")(1)
        End If
    End Sub


    Private _width As Double
    Public Property Width() As Double
        Get
            Return _width
        End Get
        Set(ByVal value As Double)
            _width = value
            RaisePropertyChanged("Width")
        End Set
    End Property


    Private _height As Double
    Public Property Height() As Double
        Get
            Return _height
        End Get
        Set(ByVal value As Double)
            _height = value
            RaisePropertyChanged("Height")
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return String.Format("{0};{1}", Width, Height)
    End Function


End Class
