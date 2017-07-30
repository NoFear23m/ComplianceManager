Imports System.ComponentModel.DataAnnotations

Public Class Setting

    <Key>
    Public Overridable Property ID As Integer

    <Required>
    Public Overridable Property Key As String

    <Required>
    Public Overridable Property Title As String

    <Required>
    Public Overridable Property Value As String

End Class