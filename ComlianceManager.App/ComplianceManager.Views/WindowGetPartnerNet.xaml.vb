Public Class WindowGetPartnerNet


    Public Property Marke As String
    Public Property Kundendaten As List(Of String)
    Public Property historyText As String

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        Dim quelltext As String = DirectCast(wb.Document, mshtml.HTMLDocumentClass).body.innerHTML

        Dim StartKundendaten As Integer = InStr(quelltext, "<!-- Kundendaten -->")
        Dim Endundendaten As Integer = InStr(StartKundendaten, quelltext, "</TD>")

        Dim Allkundendaten As String = Mid(quelltext, StartKundendaten, Endundendaten - StartKundendaten)
        Allkundendaten = Allkundendaten.Replace("<!-- ", "")
        Allkundendaten = Allkundendaten.Replace("Kundendaten", "")
        Allkundendaten = Allkundendaten.Replace("&nbsp;", " ")
        Allkundendaten = Allkundendaten.Replace(" -->", "")
        Allkundendaten = Allkundendaten.Replace("<BR>", vbNewLine)
        Allkundendaten = Allkundendaten.Replace("Herr", vbNewLine)
        Allkundendaten = Allkundendaten.Replace("Frau", vbNewLine)
        Allkundendaten = Allkundendaten.Replace("Firma", vbNewLine)
        Allkundendaten = Allkundendaten.Replace("Familie", vbNewLine)

        Dim datenList As List(Of String) = Allkundendaten.Split(separator:=vbCrLf, options:=StringSplitOptions.RemoveEmptyEntries).ToList

        'Dim StartFzDaten As Integer = InStr(quelltext, "Marke:")
        'Marke = Mid(quelltext, StartFzDaten + 7, 3)
        'Marke = Marke.Replace("<BR", "")
        'Marke = Marke.Replace("<B", "")
        'Marke = Marke.Replace("<", "")

        Kundendaten = datenList


        Dim historyStart As Integer = InStr(quelltext, "Kundenanliegen</P>")
        Dim HistoryEnd As Integer = InStr(historyStart + 25, quelltext, "</TD>")

        historyText = Mid(quelltext, historyStart + 18, HistoryEnd - historyStart)
        historyText = historyText.Replace("<BR />", vbNewLine)
        historyText = historyText.Replace("<TR>", "")
        historyText = historyText.Replace("<TD>", "")
        historyText = historyText.Replace("</TD>", "")
        historyText = historyText.Replace("</TR>", "")
        historyText = historyText.Replace("<TD style=""PADDING-RIGHT: 10px"">", "")
        historyText = historyText.Replace("<TR>", "")
        historyText = historyText.Replace("<TR>", "")
        historyText = historyText.Replace("<TR>", "")
        historyText = historyText.Replace("<BR>", vbNewLine)

        Me.DialogResult = True
    End Sub
End Class
