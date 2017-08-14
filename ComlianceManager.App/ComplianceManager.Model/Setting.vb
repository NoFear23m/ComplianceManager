Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations

Public Class Setting
    Inherits ModelBase
    Implements IDataErrorInfo

    <Key>
    Public Overridable Property ID As Integer

    <Required>
    Public Overridable Property Key As String

    <Required>
    Public Overridable Property Title As String

    <Required>
    Public Overridable Property Value As String

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
            Dim valRes As DataAnnotations.ValidationResult = ModelValidator.ValidateEntity(Of Setting)(Me).Where(Function(v) v.MemberNames.Contains(columnName) = True).FirstOrDefault
            If valRes Is Nothing Then
                Return Nothing
            Else
                Return valRes.ErrorMessage
            End If
        End Get
    End Property
End Class