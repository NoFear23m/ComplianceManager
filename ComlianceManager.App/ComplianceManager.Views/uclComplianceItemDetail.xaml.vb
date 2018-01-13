Imports ComplianceManager.Model.CompliantItem

Public Class uclComplianceItemDetail
    Private Sub uclComplianceItemDetail_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.cmbBrands.ItemsSource = [Enum].GetValues(GetType(enuBrand)).Cast(Of enuBrand)()
    End Sub
End Class
