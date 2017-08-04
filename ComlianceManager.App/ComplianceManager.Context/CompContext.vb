


Imports System.Data.Entity

Public Class CompContext
    Inherits DbContext



    Public Sub New()
        MyBase.New("ComplianceDB")
    End Sub


    Public Overridable Property ComplianceItems As DbSet(Of Model.CompliantItem)
    Public Overridable Property HistoryItems As DbSet(Of Model.HistoryItem)
    Public Overridable Property EntryTypes As DbSet(Of Model.EntryType)
    Public Overridable Property Resons As DbSet(Of Model.Reason)
    Public Overridable Property Users As DbSet(Of Model.User)
    Public Overridable Property Settings As DbSet(Of Model.Setting)




End Class
