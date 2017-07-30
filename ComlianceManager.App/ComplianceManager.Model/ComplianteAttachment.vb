

Imports System.ComponentModel.DataAnnotations

Public Class ComplianteAttachment

    Public Sub New()
        IsDeleted = False
        CreatedBy = Now
        LastChange = Now
    End Sub


    <Key>
    Public Overridable Property ID As Integer

    <Required>
    <StringLength(50)>
    Public Overridable Property Title As String

    <Required>
    Public Overridable Property RelativeFilePath As String


    Public Overridable Property IsDeleted As Boolean

    <Required>
    Public Overridable Property CreationDate As DateTime

    <Required>
    Public Overridable Property CreatedBy As String

    <Required>
    Public Overridable Property LastEditedBy As String

    <Required>
    Public Overridable Property LastChange As DateTime

 

End Class
