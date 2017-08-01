

Imports System.ComponentModel.DataAnnotations

Public Class HistoryItem

    Public Sub New()

        CreatedBy = Now
        LastChange = Now
    End Sub

    <Key>
    Public Overridable Property ID As Integer

    <Required>
    Public Overridable Property CreationDate As DateTime

    <Required>
    Public Overridable Property CreatedBy As String

    <Required>
    Public Overridable Property LastEditedBy As String

    <Required>
    Public Overridable Property LastChange As DateTime

    <Required>
    <StringLength(50, MinimumLength:=2)>
    Public Overridable Property Title As String


    Public Overridable Property Description As String

    Public Overridable Property Attachments As ICollection(Of ComplianteAttachment)


End Class
