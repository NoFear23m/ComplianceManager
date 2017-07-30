

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class CompliantItem

    Public Sub New()
        CreationDate = Now
        LastChange = Now

        IsDeleted = False
    End Sub

    <Key>
    Public Overridable Property ID As Integer

    <Required>
    <StringLengthAttribute(50, MinimumLength:=2)>
    Public Overridable Property CustomerFirstName As String

    <Required>
    <StringLengthAttribute(50, MinimumLength:=2)>
    Public Overridable Property CustomerLastName As String

    <NotMapped>
    Public ReadOnly Property CustomerFullName As String
        Get
            Return CustomerFirstName & " " & CustomerLastName
        End Get
    End Property

    Public Overridable Property CustomerNumber As Integer

    <Required>
    Public Overridable Property ComplianceBrand As enuBrand

    Public Overridable Property LicensePlate As String

    <Required>
    Public Overridable Property ComplianceReason As Reason

    <Required>
    Public Overridable Property ComplianceEntryType As EntryType

    Public Overridable Property FinishedAt As Date?


    <Required>
    Public Overridable Property CreationDate As DateTime
    <Required>
    Public Overridable Property CreatedByUserName As String

    <Required>
    Public Overridable Property LastChange As DateTime

    <Required>
    Public Overridable Property LastChangeByUserName As String


    Public Overridable Property ComplianceHistory As ICollection(Of HistoryItem)




    Public Overridable Property IsDeleted As Boolean


    Public Enum enuBrand
        VW = 0
        Audi = 1
        Seat = 2
        Skoda = 3
        Porsche = 4
        Other = 5
    End Enum

End Class
