Imports System.Windows.Forms

Public Class WindowGetPartnerNet

    Public wb As New Forms.WebBrowser
    Public Property Marke As String
    Public Property Kundendaten As List(Of String)
    Public Property historyText As String
    Public Property AttachmentsPaths As List(Of String)

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)

        Dim quelltext As String = wb.Document.Body.InnerHtml



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

        historyText = historyText.Replace("Ö", "\'d6")
        historyText = historyText.Replace("ö", "\'f6")
        historyText = historyText.Replace("Ü", "\'dc")
        historyText = historyText.Replace("ü", "\'fc")
        historyText = historyText.Replace("Ä", "\'c4")
        historyText = historyText.Replace("ä", "\'e4")



        historyText = "{\rtf1\ansi\ansicpg1252\uc1\htmautsp\deff2{\fonttbl{\f0\fcharset0 Times New Roman;}{\f2\fcharset0 Segoe UI;}}{\colortbl\red0\green0\blue0;\red255\green255\blue255;}\loch\hich\dbch\pard\plain\ltrpar\itap0{\lang1033\fs18\f2\cf0 \cf0\ql{\f2 {\lang3079\ltrch " & historyText
        historyText += "}\li0\ri0\sa0\sb0\fi0\ql\par}}}"


        Try
            AttachmentsPaths = New List(Of String)
            Dim attBlock As String = ""
            Dim attBlockStart As Integer = InStr(quelltext, ">Anhänge</P>")
            Dim attBlockEnd As Integer = InStr(quelltext, "<P class=orangetext>Händlerkontakt")
            attBlock = Mid(quelltext, attBlockStart, attBlockEnd - attBlockStart + 12)

            attBlock = attBlock.Replace("<TD><A title=", "$")
            Dim attBlockList As List(Of String) = attBlock.Split("$").ToList

            For Each b As String In attBlockList
                If b.Contains("href=") Then
                    Dim bLinkStart As Integer = InStr(b, "href=") + 6
                    Dim bLinkEnd As Integer = InStr(b, "target=_blank>") - 2
                    Dim bLink As String = Mid(b, bLinkStart, bLinkEnd - bLinkStart)
                    AttachmentsPaths.Add(bLink)
                End If
            Next

        Catch ex As Exception
            MsgBox("Fehler beim laden der Ahange aus dem Partner.Net." & vbNewLine & vbNewLine & ex.Message)
        End Try





        Me.DialogResult = True
    End Sub

    Private Sub WindowGetPartnerNet_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        MessageBox.Show("Bitte nur die Marke wählen und auf öffnen klicken, erste dann den Abruf starten.")

        Host.Child = wb
    End Sub

    Private Sub Button1_Click(sender As Object, e As RoutedEventArgs)
        For Each elem As HtmlElement In wb.Document.All
            If elem.GetAttribute("name") = "pNetContentFrame" Then
                Dim link = elem.GetAttribute("src")
                wb.Navigate("https://www.auto-partner.net" & link)

            End If
        Next
    End Sub
End Class
