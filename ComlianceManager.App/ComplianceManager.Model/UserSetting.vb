﻿Imports System.ComponentModel.DataAnnotations

Public Class UserSetting
    Inherits ModelBase


    <Key>
    Public Overridable Property ID As Integer



    <Required>
    Public Overridable Property Title As String

    <Required(AllowEmptyStrings:=True)>
    Public Overridable Property Value As String
End Class
