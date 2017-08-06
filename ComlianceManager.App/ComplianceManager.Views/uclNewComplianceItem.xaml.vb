Public Class uclNewComplianceItem

    Dim win As New WindowGetPartnerNet
    Dim vm As ViewModel.NewComplianceItemVM
    Dim hi As Model.HistoryItem

    Private Sub ButtonAbbrechen_Click(sender As Object, e As RoutedEventArgs)
        Dim win As Window = Window.GetWindow(Me)
        win.DialogResult = False
    End Sub

    Private Sub ButtonAnlegen_Click(sender As Object, e As RoutedEventArgs)
        Dim win As Window = Window.GetWindow(Me)
        win.DialogResult = True

    End Sub

    Private Sub Abruf_Click(sender As Object, e As RoutedEventArgs)


        vm = Me.DataContext

        win.wb.Navigate(vm.PertnerNetUrl)
        If win.ShowDialog() = True Then


            vm.CustomerFirstName = Split(win.Kundendaten(0), " ")(0)
            vm.CustomerLastName = ""
            For i As Integer = 1 To Split(win.Kundendaten(0), " ").Count - 1
                vm.CustomerLastName += Split(win.Kundendaten(0), " ")(i) & " "

            Next


            hi = New Model.HistoryItem

            hi.Attachments = New List(Of Model.ComplianteAttachment)
            vm._compItem.ComplianceHistory = New List(Of Model.HistoryItem)
            hi.CreatedBy = "Partner.Net Abruf"
            hi.CreationDate = Now
            hi.LastChange = Now
            hi.LastEditedBy = "Partner.Net Abruf"
            hi.Title = "Kundenanliegen"
            hi.Description = win.historyText
            vm._compItem.ComplianceHistory.Add(hi)

            Me.busi.IsBusy = True
            Dim tr As New Threading.Thread(AddressOf LoadAttachments)
            tr.IsBackground = True
            tr.Priority = System.Threading.ThreadPriority.Lowest
            tr.Start()
        End If






    End Sub



    Private Sub LoadAttachments()
        Try
            For Each att As String In win.AttachmentsPaths
                Dim destFolder As String = vm.DownloadDestFolder & "\" & Now.Year & "\"
                Dim attFilename As String = Mid(att, InStr(att, "filename=") + 9)
                attFilename = Now.Ticks & "_" & attFilename
                att = att.Replace("&amp;", "&")
                My.Computer.Network.DownloadFile("https://kd3.auto-partner.net" & att, destFolder & attFilename)
                hi.Attachments.Add(New Model.ComplianteAttachment() With
                                   {.CreatedBy = "Partner.Net", .CreationDate = Now, .LastChange = Now, .LastEditedBy = "Partner.Net",
                                   .RelativeFilePath = Now.Year & "\" & attFilename, .Title = Mid(att, InStr(att, "filename=") + 9), .IsDeleted = False})
            Next

        Catch ex As Exception
            MsgBox("Fehler beim herunterladen der Anhänge, bitte speichern Sie diese manuell")
        Finally
            Me.busi.Dispatcher.Invoke(Sub() Me.busi.IsBusy = False)
        End Try
    End Sub



End Class
