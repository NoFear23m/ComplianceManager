

Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Public Class HistoryItem
    Inherits ModelBase
    Implements IDataErrorInfo


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
    <StringLength(250, MinimumLength:=2)>
    Public Overridable Property Title As String


    Public Overridable Property Description As String

    Public Overridable Property Attachments As ICollection(Of ComplianteAttachment)

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
            Dim valRes As DataAnnotations.ValidationResult = ModelValidator.ValidateEntity(Of HistoryItem)(Me).Where(Function(v) v.MemberNames.Contains(columnName) = True).FirstOrDefault
            If valRes Is Nothing Then
                Return Nothing
            Else
                Return valRes.ErrorMessage
            End If
        End Get
    End Property
End Class
