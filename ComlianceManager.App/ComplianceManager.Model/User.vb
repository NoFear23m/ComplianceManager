
Imports System.ComponentModel.DataAnnotations

Public Class User

    Public Sub New()
        UpdatedAt = Now
        CreatedAt = Now
        IsAdmin = False
        IsActive = True

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
    Public Overridable Property CreatedBy As String
    <Required>
    Public Overridable Property UpdatedBy As String
    <Required>
    Public Overridable Property UpdatedAt As DateTime
    <Required>
    Public Overridable Property CreatedAt As DateTime


End Class
