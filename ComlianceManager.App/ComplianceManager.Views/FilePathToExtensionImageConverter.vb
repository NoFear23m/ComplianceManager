Imports System.Globalization

Public Class FilePathToExtensionImageConverter
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        'Try
        '    Dim ext As String = Mid(value.ToString, InStrRev(value.ToString, ".") + 1)
        '    Dim img As New BitmapImage
        '    img.BeginInit()
        '    img.UriSource = New Uri("FileIcons\" & ext.ToLower & ".png", UriKind.Relative)
        '    img.EndInit()
        '    Return img
        'Catch ex As Exception
        '    Return Nothing
        'End Try

        Dim ext As String = Mid(value.ToString, InStrRev(value.ToString, ".") + 1)
        Return "..\FileIcons\" & ext.ToLower & ".png"


    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
