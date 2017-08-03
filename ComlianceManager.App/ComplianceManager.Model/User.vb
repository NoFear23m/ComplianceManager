Imports System.ComponentModel.DataAnnotations

Public Class User

    Public Sub New()

        IsAdmin = False
        IsActive = True
        IsFirstLogin = True
    End Sub

    <Key>
    Public Overridable Property ID As Integer

    <Required>
    Public Overridable Property UserName As String
    <Required>
    Public Overridable Property FullName As String
    <Required>
    Public Overridable Property IsAdmin As Boolean
    <Required>
    Public Overridable Property IsActive As String
    <Required>
    Public Overridable Property IsFirstLogin As Boolean

End Class