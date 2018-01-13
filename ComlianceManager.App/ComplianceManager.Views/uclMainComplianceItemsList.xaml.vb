Imports System.Collections.Specialized
Imports System.ComponentModel

Public Class uclMainComplianceItemsList
    Implements INotifyPropertyChanged



    Private _HiddenGridColumns As List(Of String)
    Public Property HiddenGridColumns() As List(Of String)
        Get
            Return _HiddenGridColumns
        End Get
        Set(ByVal value As List(Of String))
            _HiddenGridColumns = value
            RaisePropertie("HiddenGridColumns")
        End Set
    End Property

    Private Sub CheckComboHiddenCols_ItemSelectionChanged(sender As Object, e As Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs)
        If e.IsSelected = False Then
            Me.HiddenGridColumns.Add(DirectCast(e.Item, DataGridColumn).Header)
            HideGridColumns()
        Else
            Me.HiddenGridColumns.Remove(DirectCast(e.Item, DataGridColumn).Header)
            HideGridColumns()
        End If
        Dim vm As ViewModel.ComplianteItemsVM = Me.DataContext
        vm.HidedColumnsString = Me.HiddenGridColumns
    End Sub

    Private Sub HideGridColumns()

        For Each c As DataGridColumn In Me.CompliantGrid.Columns
            If c.Header IsNot Nothing AndAlso HiddenGridColumns.FirstOrDefault(Function(n) n.ToLower = c.Header.ToString.ToLower) IsNot Nothing Then
                c.Visibility = Windows.Visibility.Hidden
            Else
                c.Visibility = Windows.Visibility.Visible
            End If
        Next

    End Sub

    Private Sub uclMainComplianceItemsList_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If ComponentModel.DesignerProperties.GetIsInDesignMode(Me) Then Exit Sub
        Dim vm As ViewModel.ComplianteItemsVM = Me.DataContext
        AddHandler Window.GetWindow(Me).Closing, AddressOf SaveColumnOrdering


        Me.HiddenGridColumns = vm.HidedColumnsString
        'Me.HiddenGridColumns = Split(SPS.VKM.DBHelper.UserSettings.GetCurrentUserSetting("DisabledCarListColumns", Instance.CurrentLoggedinUser.ID).ToString, ";").ToList
        ''Me.AviableGridColumns = Me.ActsGrid.Columns

        For i As Integer = 0 To Me.CompliantGrid.Columns.Count - 1
            Dim HidColText As String = Me.CompliantGrid.Columns(i).Header.ToString.ToLower
            If Me.HiddenGridColumns.FirstOrDefault(Function(n) n.ToLower = HidColText) Is Nothing Then Me.CheckComboHiddenCols.SelectedItems.Add(Me.CheckComboHiddenCols.Items(i))
        Next

        Dim index As Integer = 0
        For Each col As DataGridColumn In Me.CompliantGrid.Columns
            If vm.ColumnOrder.Count > index + 1 AndAlso Not String.IsNullOrEmpty(vm.ColumnOrder(index)) Then col.DisplayIndex = vm.ColumnOrder(index)
            If vm.ColumnWidths.Count > index + 1 AndAlso Not String.IsNullOrEmpty(vm.ColumnWidths(index)) Then col.Width = New DataGridLength(vm.ColumnWidths(index))
            index += 1
        Next




        HideGridColumns()
    End Sub

    Private Sub SaveColumnOrdering(sender As Object, e As CancelEventArgs)
        Dim vm As ViewModel.ComplianteItemsVM = Me.DataContext
        Dim OrderString As New List(Of String)
        Dim WidthString As New List(Of String)
        For Each col As DataGridColumn In Me.CompliantGrid.Columns
            OrderString.Add(col.DisplayIndex.ToString)
            WidthString.Add(col.ActualWidth.ToString)
        Next
        vm.ColumnOrder = OrderString
        vm.ColumnWidths = WidthString
        vm.SaveOrderList()
        vm.SaveWidthsList()
    End Sub

    Public Sub RaisePropertie(p1 As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(p1))
    End Sub







    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Private Sub Grouping_ItemSelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim vm As ViewModel.ComplianteItemsVM = Me.DataContext
            vm.ComplianceItemsView.GroupDescriptions.Clear()

            Dim gText As String = DirectCast(DirectCast(Grouping.SelectedItem, DataGridTextColumn).Binding, Data.Binding).Path.Path
            gText = gText.Replace("FallNr", "ID")
            gText = gText.Replace("Kundenname", "FullName")
            gText = gText.Replace("KdNr", "CustomerNumber")
            gText = gText.Replace("Marke", "ComplianceBrand")
            gText = gText.Replace("Eingangsdatum", "CreationDate")
            gText = gText.Replace("Ursache der Reklamation", "ComplianceReason.ReasonTitle")
            gText = gText.Replace("Reklamationsart", "ComplianceEntryType.EntryTitle")
            gText = gText.Replace("Abgeschl. am", "FinishedAt")
            gText = gText.Replace("Abgeschl. durch", "FinishedFrom")
            gText = gText.Replace("Erstellt durch", "CreatedByUserName")
            gText = gText.Replace("Zul. bearb. von", "LastChangeByUserName")
            gText = gText.Replace("zul. bear.", "LastChange")



            vm.ComplianceItemsView.GroupDescriptions.Add(New PropertyGroupDescription(gText))
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
        End Try
    End Sub
End Class
