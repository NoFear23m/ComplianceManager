Imports System.ComponentModel.DataAnnotations

Public MustInherit Class ModelBase

    Public Overridable Function Validate() As IEnumerable(Of ValidationResult)
        Return ModelValidator.ValidateEntity(Me)
    End Function


End Class
