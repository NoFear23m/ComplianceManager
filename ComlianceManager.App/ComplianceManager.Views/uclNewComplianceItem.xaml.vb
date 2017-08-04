Public Class uclNewComplianceItem


    Private Sub ButtonAbbrechen_Click(sender As Object, e As RoutedEventArgs)
        Dim win As Window = Window.GetWindow(Me)
        win.DialogResult = False
    End Sub

    Private Sub ButtonAnlegen_Click(sender As Object, e As RoutedEventArgs)
        Dim win As Window = Window.GetWindow(Me)
        win.DialogResult = True
    End Sub

    Private Sub Abruf_Click(sender As Object, e As RoutedEventArgs)

        Dim win As New WindowGetPartnerNet
        Dim vm As ViewModel.NewComplianceItemVM = Me.DataContext

        win.wb.Navigate(vm.PertnerNetUrl)
        If win.ShowDialog() = True Then


            vm.CustomerFirstName = Split(win.Kundendaten(0), " ")(0)
            vm.CustomerLastName = ""
            For i As Integer = 1 To Split(win.Kundendaten(0), " ").Count - 1
                vm.CustomerLastName += Split(win.Kundendaten(0), " ")(i) & " "

            Next


            Dim hi As New Model.HistoryItem
            vm._compItem.ComplianceHistory = New List(Of Model.HistoryItem)
            hi.CreatedBy = "Partner.Net Abruf"
            hi.CreationDate = Now
            hi.LastChange = Now
            hi.LastEditedBy = "Partner.Net Abruf"
            hi.Title = "Kundenanliegen"
            hi.Description = win.historyText
            vm._compItem.ComplianceHistory.Add(hi)

            'Select Case win.Marke
            '    Case "V"
            '        vm.ComplianceBrand = Model.CompliantItem.enuBrand.VW
            '    Case "S"
            '        vm.ComplianceBrand = Model.CompliantItem.enuBrand.Skoda
            '    Case "A"
            '        vm.ComplianceBrand = Model.CompliantItem.enuBrand.Audi
            '    Case "P"
            '        vm.ComplianceBrand = Model.CompliantItem.enuBrand.Porsche
            '    Case Else
            '        vm.ComplianceBrand = Model.CompliantItem.enuBrand.Other
            'End Select




        End If






    End Sub
End Class
