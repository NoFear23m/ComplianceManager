Imports System.ComponentModel.DataAnnotations

Public Class ModelValidation(Of T As ModelBase)

    Public Function Validate(entity As T) As IEnumerable(Of ValidationResult)
        Dim validationResults = New List(Of ValidationResult)()
        Dim validationContext = New ValidationContext(entity, Nothing, Nothing)
        Validator.TryValidateObject(entity, validationContext, validationResults, True)
        Return validationResults
    End Function

End Class