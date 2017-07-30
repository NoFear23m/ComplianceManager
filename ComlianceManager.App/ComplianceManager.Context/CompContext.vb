


Imports System.Data.Entity

Public Class CompContext
    Inherits DbContext


    Public Overridable Property ComplianceItems As DbSet(Of Model.CompliantItem)
    Public Overridable Property EntryTypes As DbSet(Of Model.EntryType)
    Public Overridable Property Resons As DbSet(Of Model.Reason)




End Class
