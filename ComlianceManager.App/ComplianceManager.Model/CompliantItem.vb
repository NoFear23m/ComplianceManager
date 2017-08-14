

Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class CompliantItem
    Inherits ModelBase
    Implements IDataErrorInfo


    Public Sub New()
        CreationDate = Now
        LastChange = Now

        IsDeleted = False
    End Sub

    <Key>
    Public Overridable Property ID As Integer

    <Required(AllowEmptyStrings:=False, ErrorMessage:="Der Kunden-Vorname ist ein erforderliches Feld")>
    <StringLengthAttribute(50, MinimumLength:=2, ErrorMessage:="Der Kunden-Vorname muss mindestens 2 und darf maximal 50 Zeichen lang sein.")>
    Public Overridable Property CustomerFirstName As String

    <Required(AllowEmptyStrings:=False, ErrorMessage:="Der Kunden-Nachname ist ein erforderliches Feld")>
    <StringLengthAttribute(50, MinimumLength:=2, ErrorMessage:="Der Kunden-Nachname muss mindestens 2 und darf maximal 50 Zeichen lang sein.")>
    Public Overridable Property CustomerLastName As String

    <NotMapped>
    Public ReadOnly Property CustomerFullName As String
        Get
            Return CustomerFirstName & " " & CustomerLastName
        End Get
    End Property

    Public Overridable Property CustomerNumber As Integer

    <Required(AllowEmptyStrings:=False, ErrorMessage:="Die Marke ist ein erforderliches Feld und muss ausgewählt werden")>
    Public Overridable Property ComplianceBrand As enuBrand

    Public Overridable Property LicensePlate As String

    <Required(AllowEmptyStrings:=False, ErrorMessage:="Das Feld Reklamationsart ist ein erforderliches Feld")>
    Public Overridable Property ComplianceReason As Reason

    <Required(AllowEmptyStrings:=False, ErrorMessage:="Das Feld Eingangsart ist ein erforderliches Feld")>
    Public Overridable Property ComplianceEntryType As EntryType

    Public Overridable Property FinishedAt As Date?

    Public Overridable Property FinishedFrom As String




    <Required(AllowEmptyStrings:=False, ErrorMessage:="Das Erstelldatum ist ein erforderliches Feld")>
    Public Overridable Property CreationDate As DateTime
    <Required>
    Public Overridable Property CreatedByUserName As String

    <Required>
    Public Overridable Property LastChange As DateTime

    <Required>
    Public Overridable Property LastChangeByUserName As String


    Public Overridable Property ComplianceHistory As ICollection(Of HistoryItem)




    Public Overridable Property IsDeleted As Boolean

    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Try
                If MyBase.Validate Is Nothing OrElse MyBase.Validate.Count = 0 Then
                    Return Nothing
                Else
                    Return "Das Entität enthält Fehler!!"
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            Dim valRes As DataAnnotations.ValidationResult = ModelValidator.ValidateEntity(Of CompliantItem)(Me).Where(Function(v) v.MemberNames.Contains(columnName) = True).FirstOrDefault
            If valRes Is Nothing Then
                Return Nothing
            Else
                Return valRes.ErrorMessage
            End If
        End Get
    End Property

    Public Enum enuBrand
        VW = 0
        Audi = 1
        Seat = 2
        Skoda = 3
        Porsche = 4
        Other = 5
    End Enum

End Class
