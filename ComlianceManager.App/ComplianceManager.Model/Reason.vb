

Imports System.ComponentModel.DataAnnotations

Public Class Reason

    Public Sub New()
        IsDeleted = False

    End Sub

    <Key>
    Public Overridable Property ID As Integer


    <Required>
    Public Overridable Property ReasonTitle As String

    Public Overridable Property ReasonDescription As String


    Public Overridable Property IsDeleted As String



    Public Overridable Property CompliantItems As ICollection(Of CompliantItem)

End Class
