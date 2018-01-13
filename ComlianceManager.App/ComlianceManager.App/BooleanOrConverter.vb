Public Class BooleanOrConverter
    Implements IMultiValueConverter
    Public Function Convert(values As Object(), targetType As Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
        For Each value As Object In values
            If CBool(value) = True Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Function ConvertBack(value As Object, targetTypes As Type(), parameter As Object, culture As System.Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
        Throw New NotSupportedException()
    End Function
End Class
