
Imports System.ComponentModel.DataAnnotations

Public Class EntryType

    Public Sub New()
        IsDeleted = False
    End Sub

    <Key>
    Public Overridable Property ID As Integer

    <Required>
    Public Overridable Property EntryTitle As String

    Public Overridable Property EntryDescription As String

    Public Overridable Property IsDeleted As Boolean


    Public Overridable Property ComplianceItems As ICollection(Of CompliantItem)

End Class
