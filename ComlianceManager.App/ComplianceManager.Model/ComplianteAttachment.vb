

Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Public Class ComplianteAttachment
    Inherits ModelBase
    Implements IDataErrorInfo


    Public Sub New()
        IsDeleted = False
        CreationDate = Now
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
            Dim valRes As DataAnnotations.ValidationResult = ModelValidator.ValidateEntity(Of ComplianteAttachment)(Me).Where(Function(v) v.MemberNames.Contains(columnName) = True).FirstOrDefault
            If valRes Is Nothing Then
                Return Nothing
            Else
                Return valRes.ErrorMessage
            End If
        End Get
    End Property
End Class
